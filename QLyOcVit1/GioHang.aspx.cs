using System;
using QLyOcVit1.Model;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using QLyOcVit1.Catalog;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace QLyOcVit1
{
    public partial class GioHang : System.Web.UI.Page {
        protected SanPhamCatalog catalog;
        protected XmlDocument docGioHang = new XmlDocument();
        protected XmlDocument docSanPham = new XmlDocument();
        protected XmlNamespaceManager managerGioHang;
        protected XmlNamespaceManager managerSanPham;
        protected List<SanPhamModel> Models = new List<SanPhamModel>();
        protected XmlElement row;
        protected StatusBar statusBar;
        protected int tong = 0;

        protected void Page_Load(object sender, EventArgs e) {
            statusBar = new StatusBar(alertBox, alertIcon, alertText);
            if (Request.Cookies["TaiKhoan"] == null || string.IsNullOrEmpty(Request.Cookies["TaiKhoan"].Value))
                Response.Redirect("DangNhap.aspx");
            docGioHang.Load(MapPath("/data/GioHang.xml"));
            docSanPham.Load(MapPath("/data/SanPham.xml"));
            managerGioHang = new XmlNamespaceManager(docGioHang.NameTable);
            managerGioHang.AddNamespace("tbl", docGioHang.DocumentElement.NamespaceURI);
            managerSanPham = new XmlNamespaceManager(docSanPham.NameTable);
            managerSanPham.AddNamespace("tbl", docSanPham.DocumentElement.NamespaceURI);

            catalog = new SanPhamCatalog() {
                XmlFile = MapPath("/data/SanPham.xml"),
                RowName = "SanPham"
            };
            catalog.Load();

            row = (XmlElement)docGioHang.DocumentElement.SelectSingleNode($"tbl:GioHang[./@TaiKhoan='{Request.Cookies["TaiKhoan"].Value}']", managerGioHang);
            if (row == null)
            {
                row = docGioHang.CreateElement("GioHang", docGioHang.DocumentElement.NamespaceURI);
                row.SetAttribute("TaiKhoan", Request.Cookies["TaiKhoan"].Value);
                docGioHang.DocumentElement.AppendChild(row);
                docGioHang.Save(MapPath("/data/GioHang.xml"));
            }

            foreach (XmlElement maSP in row.SelectNodes("tbl:MaSP", managerGioHang)) {
                SanPhamModel model = new SanPhamModel((XmlElement)docSanPham.DocumentElement.SelectSingleNode($"tbl:SanPham[./tbl:MaSP='{maSP.InnerText}']", managerSanPham));
                Models.Add(model);
                tong += model.DonGia;
            }
        }

        protected void ThanhToan(object sender, EventArgs e)
        {
            foreach (XmlElement maSP in row.SelectNodes("tbl:MaSP", managerGioHang))
                row.RemoveChild(maSP);
            Models = new List<SanPhamModel>();
            
            docGioHang.Save(MapPath("/data/GioHang.xml"));
            statusBar.SetSuccess("Thanh toán thành công.");
        }

        protected void Xoa(object sender, EventArgs e)
        {
            row.RemoveChild(row.SelectNodes($"tbl:MaSP", managerGioHang)[int.Parse(index.Value)]);
            docGioHang.Save(MapPath("/data/GioHang.xml"));
            Models.RemoveAt(int.Parse(index.Value));

            tong = 0;
            foreach (XmlElement maSP in row.SelectNodes("tbl:MaSP", managerGioHang))
            {
                SanPhamModel model = new SanPhamModel((XmlElement)docSanPham.DocumentElement.SelectSingleNode($"tbl:SanPham[./tbl:MaSP='{maSP.InnerText}']", managerSanPham));
                tong += model.DonGia;
            }
        }
    }
}