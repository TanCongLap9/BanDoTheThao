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

namespace QLyOcVit1
{
    public partial class DangKy : System.Web.UI.Page
    {
        private FieldsBox fieldsBox;
        private StatusBar statusBar;
        private XmlDocument DocNhanVien = new XmlDocument();
        private XmlNamespaceManager NhanVienManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.Get("TaiKhoan") != null && !string.IsNullOrEmpty(Request.Cookies.Get("TaiKhoan").Value))
                Response.Redirect("SanPham.aspx");
            statusBar = new StatusBar(alertBox, alertIcon, alertText);

            fieldsBox = new FieldsBox(MapPath("/data/KhachHang.xml"))
            {
                page = this,
                Fields = new[] {
                    new FieldModel("MaKH", maKH, "@MaKH"),
                    new FieldModel("TenKH", tenKH, "@TenKH"),
                    new FieldModel("GioiTinh", gioiTinh),
                    new FieldModel("Email", email),
                    new FieldModel("SDT", sdt),
                    new FieldModel("NgaySinh", ngaySinh),
                    new FieldModel("TaiKhoan", taiKhoan),
                    new FieldModel("MatKhau", matKhau)
                },
                UpdateButton = luu,
                RowName = "KhachHang",
                SchemaFile = MapPath("/data/KhachHang.xsd"),
                StatusBar = statusBar,
                Index = -1
            };
            fieldsBox.Updating += ValidateInput;
            fieldsBox.Updated += OnSuccess;
            fieldsBox.Load();

            DocNhanVien.Load(MapPath("/data/NhanVien.xml"));
            NhanVienManager = XmlUtils.CreateNamespaceManager(DocNhanVien.DocumentElement);

            List<string> cacMaKH = new List<string>();
            foreach (XmlNode maKHNode in fieldsBox.Doc.DocumentElement.SelectNodes("//@MaKH", fieldsBox.Manager))
                cacMaKH.Add(maKHNode.InnerText);
            maKH.Value = GoiYMaKH(cacMaKH);
        }

        private void OnSuccess(object sender, EventArgs e)
        {
            statusBar.SetSuccess("Tạo tài khoản thành công. Vui lòng nhấn nút Quay về để đăng nhập.");
        }

        private void ValidateInput(object sender, CancelEventArgs e)
        {
            if (fieldsBox.InsertMode)
            {
                foreach (XmlElement taiKhoan in fieldsBox.Doc.DocumentElement.SelectNodes("tbl:KhachHang/tbl:TaiKhoan", fieldsBox.Manager))
                    if (this.taiKhoan.Value == taiKhoan.InnerText)
                    {
                        statusBar.SetError($"Tài khoản này đã có người sử dụng. Vui lòng nhập tài khoản khác.");
                        e.Cancel = true;
                        return;
                    }
                foreach (XmlElement taiKhoan in DocNhanVien.DocumentElement.SelectNodes("tbl:NhanVien/tbl:TaiKhoan", NhanVienManager))
                    if (this.taiKhoan.Value == taiKhoan.InnerText)
                    {
                        statusBar.SetError($"Tài khoản này đã có người sử dụng. Vui lòng nhập tài khoản khác.");
                        e.Cancel = true;
                        return;
                    }
            }
        }

        private string GoiYMaKH(List<string> cacMaKH)
        {
            List<int> cacMaSo = new List<int>();
            int goiY = 0, _;
            cacMaKH.Sort();
            foreach (string maKH in cacMaKH)
                if (int.TryParse(maKH, out _))
                    cacMaSo.Add(int.Parse(maKH));
            foreach (int maso in cacMaSo) if (goiY == maso) goiY++;
            return string.Format("{0:d}", goiY);
        }
    }
}