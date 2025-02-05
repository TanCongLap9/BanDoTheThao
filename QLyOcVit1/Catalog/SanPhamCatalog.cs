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
    public class SanPhamCatalog
    {
        public string XmlFile, SchemaFile, RowName;
        public XmlDocument Doc = new XmlDocument();
        public XmlNamespaceManager Manager;
        public List<SanPhamModel> Models = new List<SanPhamModel>();
        public string Search;

        public SanPhamCatalog()
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

            foreach (XmlElement row in Doc.DocumentElement.SelectNodes($"tbl:{RowName}", Manager))
            {
                SanPhamModel model = new SanPhamModel(row);
                Models.Add(model);
            }
        }
    }
}