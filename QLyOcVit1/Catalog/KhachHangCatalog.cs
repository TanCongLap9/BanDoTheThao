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
    public class KhachHangCatalog
    {
        public string XmlFile, SchemaFile, RowName;
        public XmlDocument Doc = new XmlDocument();
        public XmlNamespaceManager Manager;
        public List<KhachHangModel> Models = new List<KhachHangModel>();
        public string Search;

        public KhachHangCatalog()
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
                KhachHangModel model = new KhachHangModel(row);
                Models.Add(model);
            }
        }
    }
}