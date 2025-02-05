using QLyOcVit1.Model;
using System;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace QLyOcVit1.Model
{
    public class KhachHangModel
    {
        public string MaKH, Name, Email, SDT, TaiKhoan, MatKhau;
        public DateTime NgaySinh;
        public bool GioiTinh;

        public KhachHangModel()
        {
            
        }

        public KhachHangModel(XmlElement element)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(element.OwnerDocument.NameTable);
            manager.AddNamespace("tbl", element.NamespaceURI);

            MaKH = element.SelectSingleNode("@MaKH", manager).InnerText;
            Name = element.SelectSingleNode("@TenKH", manager).InnerText;
            GioiTinh = bool.Parse(element.SelectSingleNode("tbl:GioiTinh", manager).InnerText);
            Email = element.SelectSingleNode("tbl:Email", manager).InnerText;
            SDT = element.SelectSingleNode("tbl:SDT", manager).InnerText;
            TaiKhoan = element.SelectSingleNode("tbl:TaiKhoan", manager).InnerText;
            MatKhau = element.SelectSingleNode("tbl:MatKhau", manager).InnerText;
        }

        public override bool Equals(object other)
        {
            return other is KhachHangModel && MaKH == ((KhachHangModel)other).MaKH;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(KhachHangModel one, KhachHangModel two)
        {
            return one.MaKH == two.MaKH;
        }

        public static bool operator !=(KhachHangModel one, KhachHangModel two)
        {
            return one.MaKH != two.MaKH;
        }
    }
}