using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLyOcVit1.Model;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Globalization;

namespace QLyOcVit1
{
    public partial class XemSanPham : System.Web.UI.Page {
        public FieldsBox fieldsBox;
        public XmlDocument DocNhaSanXuat = new XmlDocument();
        public XmlNamespaceManager manager;
        public string NhaSanXuat;
        private StatusBar statusBar;

        protected void Page_Load(object sender, EventArgs e)
        {
            DocNhaSanXuat.Load(MapPath("/data/NhaSanXuat.xml"));
            manager = XmlUtils.CreateNamespaceManager(DocNhaSanXuat.DocumentElement);

            chinhSua.Visible = (Request.Cookies["QuanTri"] != null) && (Request.Cookies["QuanTri"].Value == "1");

            statusBar = new StatusBar(alertBox, alertIcon, alertText);

            fieldsBox = new FieldsBox(MapPath("/data/SanPham.xml"))
            {
                page = this,
                Fields = new FieldModel[0],
                RowName = "SanPham",
                SchemaFile = MapPath("/data/SanPham.xsd")
            };
            XmlNode currentRow = fieldsBox.Doc.DocumentElement.SelectSingleNode($"tbl:SanPham[./tbl:MaSP='{Request.QueryString["id"]}']", fieldsBox.Manager);
            if (currentRow == null) Response.Redirect("SanPham.aspx");
            fieldsBox.Index = fieldsBox.GetIndexFromElement(currentRow);

            fieldsBox.Load();

            if (!IsPostBack)
            {
                mua.Disabled = fieldsBox.CurrentDict.GetInt("SoLuongTon") == 0;
                chinhSua.HRef += Request.QueryString["id"];
            }
            XmlNode nsx = DocNhaSanXuat.DocumentElement.SelectSingleNode($"tbl:NhaSanXuat[./tbl:MaNSX='{fieldsBox.CurrentDict["MaNSX"]}']/tbl:TenNSX", manager);
            NhaSanXuat = nsx == null ? null : nsx.InnerText;
        }

        protected void Mua(object sender, EventArgs e)
        {
            if (Request.Cookies["TaiKhoan"] == null || string.IsNullOrEmpty(Request.Cookies["TaiKhoan"].Value))
                Response.Redirect("DangNhap.aspx");
            XmlDocument docGioHang = new XmlDocument();
            docGioHang.Load(MapPath("/data/Giohang.xml"));
            XmlNamespaceManager managerGioHang = new XmlNamespaceManager(docGioHang.NameTable);
            managerGioHang.AddNamespace("tbl", docGioHang.DocumentElement.NamespaceURI);
            
            XmlElement row = (XmlElement)docGioHang.DocumentElement.SelectSingleNode($"tbl:GioHang[./@TaiKhoan='{Request.Cookies["TaiKhoan"].Value}']", managerGioHang);
            if (row == null)
            {
                row = docGioHang.CreateElement("GioHang", docGioHang.DocumentElement.NamespaceURI);
                row.SetAttribute("TaiKhoan", Request.Cookies["TaiKhoan"].Value);
                docGioHang.DocumentElement.AppendChild(row);
            }
            for (int i = 1; i <= int.Parse(soLuongMua.Value); i++)
            {
                XmlElement maSP = docGioHang.CreateElement("MaSP", docGioHang.DocumentElement.NamespaceURI);
                maSP.InnerText = fieldsBox.Table.Rows[fieldsBox.Index].Field<string>("MaSP");
                row.AppendChild(maSP);
            }
            docGioHang.Save(MapPath("/data/GioHang.xml"));
            statusBar.SetSuccess("Đặt sản phẩm thành công. Nhấn vào mục Giỏ hàng để biết các sản phẩm đã đặt.");
        }
    }
}