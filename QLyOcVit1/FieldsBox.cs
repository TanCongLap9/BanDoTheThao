using QLyOcVit1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;

namespace QLyOcVit1
{
    public class FieldsBox
    {
        public FieldModel[] Fields = new FieldModel[0];
        public List<HtmlInputText> ReadOnlyFields;
        private HtmlButton _UpdateButton, _DeleteButton;
        public int Index;
        public string XmlFile, SchemaFile, RowName;
        public XmlDocument Doc = new XmlDocument();
        public StatusBar StatusBar;
        public DataTable Table;
        public XmlModel CurrentDict;
        public Page page;
        public XmlNamespaceManager Manager;
        public XmlNodeList Rows;

        public bool InsertMode
        {
            get
            {
                if (ReadOnlyFields != null)
                {
                    foreach (HtmlInputText input in ReadOnlyFields)
                        if (!input.Disabled) return true;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public HtmlButton UpdateButton
        {
            get { return _UpdateButton; }
            set {
                if (_UpdateButton != null) _UpdateButton.ServerClick -= Update;
                if (value != null) value.ServerClick += Update;
                _UpdateButton = value;
            }
        }

        public HtmlButton DeleteButton
        {
            get { return _DeleteButton; }
            set
            {
                if (_DeleteButton != null) _DeleteButton.ServerClick -= Delete;
                if (value != null) value.ServerClick += Delete;
                _DeleteButton = value;
            }
        }

        public FieldsBox(string XmlFile)
        {
            this.XmlFile = XmlFile;
            Doc.Load(XmlFile);
            Manager = XmlUtils.CreateNamespaceManager(Doc.DocumentElement);
        }

        public int GetIndexFromElement(XmlNode element)
        {
            XmlNodeList nodeList = Doc.DocumentElement.SelectNodes($"tbl:{RowName}", Manager);
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList[i];
                if (node == element) return i;
            }
            return -1;
        }

        public void Update(object sender = null, EventArgs e = null)
        {
            CancelEventArgs ce = new CancelEventArgs();
            if (Updating != null) Updating.Invoke(this, ce);
            if (ce.Cancel) return;

            try
            {
                if (InsertMode)
                {
                    XmlElement row = Doc.CreateElement(RowName, Doc.DocumentElement.NamespaceURI);
                    foreach (FieldModel field in Fields)
                    {
                        if (field.Control is HtmlImage) continue; // <img> are for display, not fields
                        if (field.XPath.StartsWith("@")) // Attribute
                        {
                            row.SetAttribute(field.Name, GetValue(field));
                        }
                        else
                        {
                            XmlElement elementField = Doc.CreateElement(field.Name, Doc.DocumentElement.NamespaceURI);
                            elementField.InnerText = GetValue(field);
                            row.AppendChild(elementField);
                        }
                    }
                    Doc.DocumentElement.AppendChild(row);
                }
                else
                {
                    foreach (FieldModel field in Fields)
                    {
                        if (field.Control is HtmlImage) continue; // <img> are for display, not fields
                        Rows[Index].SelectSingleNode(field.XPath, Manager).InnerText = GetValue(field);
                    }
                }
            }
            catch (Exception exc)
            {
                StatusBar.SetError("Có lỗi xảy ra: " + exc.Message);
                return;
            }

            Doc.Save(XmlFile);
            StatusBar.SetSuccess(InsertMode ? "Thêm dữ liệu thành công." : "Chỉnh sửa dữ liệu thành công.");
            UpdateTable();
            if (Updated != null) Updated.Invoke(this, new EventArgs());
        }

        public void Delete(object sender = null, EventArgs e = null)
        {
            CancelEventArgs ce = new CancelEventArgs();
            if (Deleting != null) Deleting.Invoke(this, ce);
            if (ce.Cancel) return;

            try
            {
                Doc.DocumentElement.RemoveChild(Rows[Index]);
            }
            catch (Exception exc)
            {
                StatusBar.SetError("Có lỗi xảy ra: " + exc.Message);
                return;
            }

            Doc.Save(XmlFile);
            StatusBar.SetSuccess("Xoá dữ liệu thành công.");
            UpdateTable();
            if (Deleted != null) Deleted.Invoke(this, new EventArgs());
        }

        public void Load()
        {
            UpdateTable();
            if (page.IsPostBack) return;
            FillFields();
        }

        public void UpdateTable()
        {
            DataSet dSet = new DataSet();
            dSet.ReadXmlSchema(SchemaFile);
            dSet.ReadXml(XmlFile, XmlReadMode.ReadSchema);

            Table = dSet.Tables[0];

            Rows = Doc.DocumentElement.SelectNodes($"tbl:{RowName}", Manager);
            if (Rows[Index] != null) CurrentDict = new XmlModel(Rows[Index]);
        }

        public void Clear(object sender = null, EventArgs e = null)
        {
            if (ReadOnlyFields != null)
                foreach (HtmlInputText ctl in ReadOnlyFields)
                    ctl.Disabled = false;
            foreach (FieldModel field in Fields)
            {
                HtmlControl ctl = field.Control;
                if (ctl is HtmlSelect) ((HtmlSelect)ctl).SelectedIndex = 0;
                else if (ctl is HtmlInputGenericControl) ((HtmlInputGenericControl)ctl).Value = "";
                else if (ctl is HtmlImage) { }
                else if (ctl is HtmlInputHidden) ((HtmlInputHidden)ctl).Value = "";
                else if (ctl is HtmlInputText) ((HtmlInputText)ctl).Value = "";
                else if (ctl is HtmlGenericControl) ((HtmlGenericControl)ctl).InnerText = "";
            }
        }

        public void OnSelected(object sender = null, EventArgs e = null)
        {
            
        }

        public void FillFields(object sender = null, EventArgs e = null)
        {
            if (Index < 0) // If index is negative, go to insert mode
            {
                Clear(this, new EventArgs());
                return;
            }
            if (ReadOnlyFields != null)
                foreach (HtmlInputText ctl in ReadOnlyFields)
                    ctl.Disabled = true;
            foreach (FieldModel model in Fields)
                SetValue(model, Rows[Index], model.XPath);
        }

        public string GetValue(FieldModel model)
        {
            if (model.Control is HtmlSelect) return ((HtmlSelect)model.Control).Value;
            else if (model.Control is HtmlInputGenericControl) return ((HtmlInputGenericControl)model.Control).Value;
            else if (model.Control is HtmlImage) return ((HtmlImage)model.Control).Src;
            else if (model.Control is HtmlInputHidden) return ((HtmlInputHidden)model.Control).Value;
            else if (model.Control is HtmlGenericControl) return ((HtmlGenericControl)model.Control).InnerText;
            else if (model.Control is HtmlInputText) return ((HtmlInputText)model.Control).Value;
            return "";
        }

        public void SetValue(FieldModel model, XmlNode element, string xpath)
        {
            string value = element.SelectSingleNode(xpath, Manager).InnerText;
            if (model.Control is HtmlSelect)
                ((HtmlSelect)model.Control).Value = value;
            else if (model.Control is HtmlInputGenericControl)
                ((HtmlInputGenericControl)model.Control).Value = value;
            else if (model.Control is HtmlImage)
                ((HtmlImage)model.Control).Src = value;
            else if (model.Control is HtmlInputHidden)
                ((HtmlInputHidden)model.Control).Value = value;
            else if (model.Control is HtmlInputCheckBox)
                ((HtmlInputCheckBox)model.Control).Checked = bool.Parse(value);
            else if (model.Control is HtmlInputRadioButton)
                ((HtmlInputRadioButton)model.Control).Checked = bool.Parse(value);
            else if (model.Control is HtmlInputText)
                ((HtmlInputText)model.Control).Value = value;
            else if (model.Control is HtmlGenericControl)
                ((HtmlGenericControl)model.Control).InnerText = value;
        }

        public event CancelEventHandler Updating, Deleting;
        public event EventHandler Updated, Deleted;
    }
}