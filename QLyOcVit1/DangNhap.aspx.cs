using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLyOcVit1
{
    public partial class DangNhap : System.Web.UI.Page
    {
        private XmlDocument DocKhachHang = new XmlDocument();
        private XmlDocument DocNhanVien = new XmlDocument();
        private StatusBar statusBar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.Get("TaiKhoan") != null && !string.IsNullOrEmpty(Request.Cookies.Get("TaiKhoan").Value))
                Response.Redirect("SanPham.aspx");
            DocKhachHang.Load(MapPath("/data/KhachHang.xml"));
            DocNhanVien.Load(MapPath("/data/NhanVien.xml"));
            statusBar = new StatusBar(alertBox, alertIcon, alertText);
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            foreach (XmlElement element in DocKhachHang.DocumentElement.SelectNodes("*[local-name()='KhachHang']"))
                if (username.Value == element.SelectSingleNode("*[local-name()='TaiKhoan']").InnerText && password.Value == element.SelectSingleNode("*[local-name()='MatKhau']").InnerText) {
                    Response.Cookies.Set(new HttpCookie("TaiKhoan", username.Value) { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Set(new HttpCookie("QuanTri", "0") { Expires = DateTime.Now.AddDays(7) });
                    Response.Redirect("SanPham.aspx");
                }
            foreach (XmlElement element in DocNhanVien.DocumentElement.SelectNodes("*[local-name()='NhanVien']"))
                if (username.Value == element.SelectSingleNode("*[local-name()='TaiKhoan']").InnerText && password.Value == element.SelectSingleNode("*[local-name()='MatKhau']").InnerText)
                {
                    Response.Cookies.Set(new HttpCookie("TaiKhoan", username.Value) { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Set(new HttpCookie("QuanTri", "1") { Expires = DateTime.Now.AddDays(7) });
                    Response.Redirect("SanPham.aspx");
                }
            statusBar.SetError("Tài khoản hoặc mật khẩu nhập không đúng.");
        }
    }
}