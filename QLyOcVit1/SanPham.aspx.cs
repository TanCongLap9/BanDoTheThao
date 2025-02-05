using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLyOcVit1.Model;
using QLyOcVit1.Catalog;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace QLyOcVit1
{
    public partial class SanPham : System.Web.UI.Page {
        public SanPhamCatalog catalog;
        public List<SanPhamModel> results = new List<SanPhamModel>();

        protected void Page_Load(object sender, EventArgs e) {
            catalog = new SanPhamCatalog() {
                XmlFile = MapPath("/data/SanPham.xml"),
                RowName = "SanPham"
            };
            catalog.Load();

            for (int i = 0; i < catalog.Models.Count; i++)
            {
                var model = catalog.Models[i];
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    // Tìm mã, tên sản phẩm không phân biệt hoa thường, dấu
                    string searchString = new Regex("[\u0300-\u036f]").Replace(Request.QueryString["q"].ToLower().Normalize(NormalizationForm.FormD).Replace("đ", "d"), "");
                    string tenSP = new Regex("[\u0300-\u036f]").Replace(model.Name.ToLower().Normalize(NormalizationForm.FormD).Replace("đ", "d"), "");
                    string maSP = new Regex("[\u0300-\u036f]").Replace(model.MaSP.ToLower().Normalize(NormalizationForm.FormD).Replace("đ", "d"), "");
                    if (!tenSP.Contains(searchString) && maSP != searchString) continue;
                }
                results.Add(model);
            }
        }
    }
}