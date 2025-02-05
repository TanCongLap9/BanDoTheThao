using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLyOcVit1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool coTaiKhoan = Request.Cookies["TaiKhoan"] != null && !string.IsNullOrEmpty(Request.Cookies["TaiKhoan"].Value);
            bool quyenQuanTri = Request.Cookies["QuanTri"] != null && Request.Cookies["QuanTri"].Value == "1";
            dangNhap.Visible = !coTaiKhoan;
            dangXuat.Visible = coTaiKhoan;
            quanLy.Visible = quyenQuanTri;
        }

        protected string GetAccountName()
        {
            HttpCookie cookieTaiKhoan = Request.Cookies["TaiKhoan"];
            if (cookieTaiKhoan == null || string.IsNullOrEmpty(cookieTaiKhoan.Value)) return "Khách";
            XmlDocument docKhachHang = new XmlDocument();
            XmlDocument docNhanVien = new XmlDocument();
            docKhachHang.Load(MapPath("/data/KhachHang.xml"));
            docNhanVien.Load(MapPath("/data/NhanVien.xml"));

            XmlNamespaceManager managerKhachHang = XmlUtils.CreateNamespaceManager(docKhachHang.DocumentElement);
            XmlNamespaceManager managerNhanVien = XmlUtils.CreateNamespaceManager(docNhanVien.DocumentElement);

            XmlNode khachHang = docKhachHang.DocumentElement.SelectSingleNode($"tbl:KhachHang[./tbl:TaiKhoan='{cookieTaiKhoan.Value}']", managerKhachHang);
            if (khachHang != null) return khachHang.SelectSingleNode("@TenKH").InnerText;
            XmlNode nhanVien = docNhanVien.DocumentElement.SelectSingleNode($"tbl:NhanVien[./tbl:TaiKhoan='{cookieTaiKhoan.Value}']", managerNhanVien);
            if (nhanVien != null) return nhanVien.SelectSingleNode("tbl:TenNV", managerNhanVien).InnerText;

            return "Người lạ";
        }
    }
}