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
    public partial class QuanLyKhachHang : System.Web.UI.Page
    {
        public FieldsBox fieldsBox;
        public StatusBar statusBar;
        public string password;
        private XmlDocument DocNhanVien = new XmlDocument();
        private XmlNamespaceManager NhanVienManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["QuanTri"] == null || Request.Cookies["QuanTri"].Value != "1")
                    Response.Redirect("SanPham.aspx");
                matKhau.Value = "";
            }
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
                ReadOnlyFields = new List<HtmlInputText> { maKH },
                UpdateButton = luu,
                DeleteButton = xoa,
                RowName = "KhachHang",
                SchemaFile = MapPath("/data/KhachHang.xsd"),
                StatusBar = statusBar
            };

            XmlNode currentRow = fieldsBox.Doc.DocumentElement.SelectSingleNode($"tbl:KhachHang[@MaKH='{Request.QueryString["id"]}']", fieldsBox.Manager);
            fieldsBox.Index = fieldsBox.GetIndexFromElement(currentRow);
            fieldsBox.Updating += ValidateInput;
            fieldsBox.Deleted += OnDeleted;
            fieldsBox.Load();

            DocNhanVien.Load(MapPath("/data/NhanVien.xml"));
            NhanVienManager = XmlUtils.CreateNamespaceManager(DocNhanVien.DocumentElement);

            if (fieldsBox.Index >= 0) password = fieldsBox.Table.Rows[fieldsBox.Index].Field<string>("MatKhau");
        }

        protected void OnDeleted(object sender, EventArgs e)
        {
            Response.Redirect("KhachHang.aspx");
        }

        private void ValidateInput(object sender, CancelEventArgs e)
        {
            if (fieldsBox.InsertMode)
            {
                List<string> cacMaKH = new List<string>();
                foreach (XmlNode maKHNode in fieldsBox.Doc.DocumentElement.SelectNodes("//@MaKH", fieldsBox.Manager))
                    cacMaKH.Add(maKHNode.InnerText);
                if (cacMaKH.Contains(maKH.Value))
                {
                    statusBar.SetError($"Mã khách hàng bị trùng với khách hàng khác. Vui lòng nhập mã khác. Gợi ý: {GoiYMaKH(cacMaKH)}");
                    e.Cancel = true;
                    return;
                }
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
            else if (matKhau.Value == "")
                matKhau.Value = password;
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
            return string.Format("{0}", goiY);
        }
    }
}