using System;
using QLyOcVit1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;

namespace QLyOcVit1.Catalog
{
    public class NhanVienCatalog
    {
        public string XmlFile, RowName;
        public XmlDocument Doc = new XmlDocument();
        public XmlNamespaceManager Manager;
        public List<NhanVienModel> Models = new List<NhanVienModel>();

        public NhanVienCatalog()
        {

        }

        public void Load()
        {
            UpdateTable();
        }

        public void UpdateTable()
        {
            Doc.Load(XmlFile);
            Manager = XmlUtils.CreateNamespaceManager(Doc.DocumentElement);

            foreach (XmlElement row in Doc.DocumentElement.SelectNodes($"*[local-name()='{RowName}']"))
            {
                NhanVienModel model = new NhanVienModel(row);
                Models.Add(model);
            }
        }
    }
}