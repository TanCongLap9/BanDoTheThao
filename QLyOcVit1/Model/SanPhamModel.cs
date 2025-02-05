using QLyOcVit1.Model;
using System;
using System.Data;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace QLyOcVit1.Model
{
    public class SanPhamModel
    {
        public string MaSP, Name, Hinh, DVT;
        public int DonGia, SoLuongTon;
        public float DanhGia;

        public SanPhamModel()
        {
            
        }

        public SanPhamModel(XmlElement element)
        {

            XmlNamespaceManager manager = XmlUtils.CreateNamespaceManager(element);

            MaSP = element.SelectSingleNode("tbl:MaSP", manager).InnerText;
            Name = element.SelectSingleNode("tbl:TenSP", manager).InnerText;
            Hinh = element.SelectSingleNode("tbl:Hinh", manager).InnerText;
            DVT = element.SelectSingleNode("tbl:DVT", manager).InnerText;
            DonGia = int.Parse(element.SelectSingleNode("tbl:DonGia", manager).InnerText, CultureInfo.InvariantCulture);
            SoLuongTon = int.Parse(element.SelectSingleNode("tbl:SoLuongTon", manager).InnerText, CultureInfo.InvariantCulture);
            DanhGia = float.Parse(element.SelectSingleNode("tbl:DanhGia", manager).InnerText, CultureInfo.InvariantCulture);
        }

        public override bool Equals(object other)
        {
            return other is SanPhamModel && MaSP == ((SanPhamModel)other).MaSP;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SanPhamModel one, SanPhamModel two)
        {
            return one.MaSP == two.MaSP;
        }

        public static bool operator !=(SanPhamModel one, SanPhamModel two)
        {
            return one.MaSP != two.MaSP;
        }
    }
}