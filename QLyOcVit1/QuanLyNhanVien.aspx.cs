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
    public partial class QuanLyNhanVien : System.Web.UI.Page
    {
        public FieldsBox fieldsBox;
        public StatusBar statusBar;
        public string password;
        private XmlDocument DocKhachHang = new XmlDocument();
        private XmlNamespaceManager KhachHangManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["QuanTri"] == null || Request.Cookies["QuanTri"].Value != "1")
                    Response.Redirect("SanPham.aspx");
                matKhau.Value = "";
            }
            statusBar = new StatusBar(alertBox, alertIcon, alertText);
            fieldsBox = new FieldsBox(MapPath("/data/NhanVien.xml"))
            {
                page = this,
                Fields = new[] {
                    new FieldModel("MaNV", maNV, "@MaNV"),
                    new FieldModel("TenNV", tenNV),
                    new FieldModel("GioiTinh", gioiTinh),
                    new FieldModel("Email", email),
                    new FieldModel("SDT", sdt),
                    new FieldModel("NgaySinh", ngaySinh),
                    new FieldModel("TaiKhoan", taiKhoan),
                    new FieldModel("MatKhau", matKhau)
                },
                ReadOnlyFields = new List<HtmlInputText> { maNV },
                UpdateButton = luu,
                DeleteButton = xoa,
                RowName = "NhanVien",
                SchemaFile = MapPath("/data/NhanVien.xsd"),
                StatusBar = statusBar
            };

            XmlNode currentRow = fieldsBox.Doc.DocumentElement.SelectSingleNode($"tbl:NhanVien[@MaNV='{Request.QueryString["id"]}']", fieldsBox.Manager);
            fieldsBox.Index = fieldsBox.GetIndexFromElement(currentRow);
            fieldsBox.Updating += ValidateInput;
            fieldsBox.Deleted += OnDeleted;
            fieldsBox.Load();

            DocKhachHang.Load(MapPath("/data/KhachHang.xml"));
            KhachHangManager = XmlUtils.CreateNamespaceManager(DocKhachHang.DocumentElement);

            if (fieldsBox.Index >= 0) password = fieldsBox.Table.Rows[fieldsBox.Index].Field<string>("MatKhau");
        }

        protected void OnDeleted(object sender, EventArgs e)
        {
            Response.Redirect("NhanVien.aspx");
        }

        protected void ValidateInput(object sender, CancelEventArgs e)
        {
            if (fieldsBox.InsertMode)
            {
                List<string> cacMaNV = new List<string>();
                foreach (XmlNode maNVNode in fieldsBox.Doc.DocumentElement.SelectNodes("//@MaNV", fieldsBox.Manager))
                    cacMaNV.Add(maNVNode.InnerText);
                if (cacMaNV.Contains(maNV.Value))
                {
                    statusBar.SetError($"Mã nhân viên bị trùng với nhân viên khác. Vui lòng nhập mã khác. Gợi ý: {GoiYMaNV(cacMaNV)}");
                    e.Cancel = true;
                    return;
                }
                foreach (XmlElement taiKhoan in DocKhachHang.DocumentElement.SelectNodes("tbl:KhachHang/tbl:TaiKhoan", KhachHangManager))
                    if (this.taiKhoan.Value == taiKhoan.InnerText)
                    {
                        statusBar.SetError($"Tài khoản này đã có người sử dụng. Vui lòng nhập tài khoản khác.");
                        e.Cancel = true;
                        return;
                    }
                foreach (XmlElement taiKhoan in fieldsBox.Doc.DocumentElement.SelectNodes("tbl:NhanVien/tbl:TaiKhoan", fieldsBox.Manager))
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

        private string GoiYMaNV(List<string> cacMaNV)
        {
            List<int> cacMaSo = new List<int>();
            int goiY = 0, _;
            cacMaNV.Sort();
            foreach (string maNV in cacMaNV)
                if (int.TryParse(maNV, out _))
                    cacMaSo.Add(int.Parse(maNV));
            foreach (int maso in cacMaSo) if (goiY == maso) goiY++;
            return string.Format("{0}", goiY);
        }
    }
}