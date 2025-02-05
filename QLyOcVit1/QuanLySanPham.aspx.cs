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
    public partial class QuanLySanPham : System.Web.UI.Page
    {
        public FieldsBox fieldsBox;
        public StatusBar statusBar;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                DataSet dSetNSX = new DataSet();
                dSetNSX.ReadXml(MapPath("/data/NhaSanXuat.xml"));
                nhaSanXuat.DataSource = dSetNSX.Tables[0];
                nhaSanXuat.DataBind();

                if (Request.Cookies["QuanTri"] == null || Request.Cookies["QuanTri"].Value != "1")
                    Response.Redirect(Request.RawUrl.Replace("QuanLySanPham.aspx", "XemSanPham.aspx"));
            }
            statusBar = new StatusBar(alertBox, alertIcon, alertText);
            fieldsBox = new FieldsBox(MapPath("/data/SanPham.xml"))
            {
                page = this,
                Fields = new[] {
                    new FieldModel("MaSP", maSP),
                    new FieldModel("TenSP", tenSP),
                    new FieldModel("Hinh", hinh),
                    new FieldModel("Hinh", hinhSanPham),
                    new FieldModel("DonGia", donGia),
                    new FieldModel("TGBH", tgbh),
                    new FieldModel("MoTa", moTa),
                    new FieldModel("SoLuongTon", soLuongTon),
                    new FieldModel("XuatXu", xuatXu),
                    new FieldModel("DVT", dvt),
                    new FieldModel("DanhGia", danhGia),
                    new FieldModel("MaNSX", nhaSanXuat)
                },
                ReadOnlyFields = new List<HtmlInputText> { maSP },
                UpdateButton = luu,
                RowName = "SanPham",
                SchemaFile = MapPath("/data/SanPham.xsd"),
                StatusBar = statusBar
            };
            XmlNode currentRow = fieldsBox.Doc.DocumentElement.SelectSingleNode($"tbl:SanPham[./tbl:MaSP='{Request.QueryString["id"]}']", fieldsBox.Manager);
            fieldsBox.Index = fieldsBox.GetIndexFromElement(currentRow);
            fieldsBox.Updating += ValidateInput;
            fieldsBox.Load();
        }

        private void ValidateInput(object sender, CancelEventArgs e)
        {
            if (fieldsBox.InsertMode)
            {
                List<string> cacMaSP = new List<string>();
                foreach (XmlNode maSPNode in fieldsBox.Doc.DocumentElement.SelectNodes("//tbl:MaSP", fieldsBox.Manager))
                    cacMaSP.Add(maSPNode.InnerText);
                if (cacMaSP.Contains(maSP.Value))
                {
                    statusBar.SetError($"Mã sản phẩm bị trùng với sản phẩm khác. Vui lòng nhập mã khác. Gợi ý: {GoiYMaSP(cacMaSP)}");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private string GoiYMaSP(List<string> cacMaSP)
        {
            List<int> cacMaSo = new List<int>();
            int goiY = 0, _;
            cacMaSP.Sort();
            foreach (string maSP in cacMaSP)
                if (int.TryParse(maSP.Substring(2, 4), out _))
                    cacMaSo.Add(int.Parse(maSP.Substring(2, 4)));
            foreach (int maso in cacMaSo) if (goiY == maso) goiY++;
            return string.Format("TT{0:d4}", goiY);
        }
    }
}