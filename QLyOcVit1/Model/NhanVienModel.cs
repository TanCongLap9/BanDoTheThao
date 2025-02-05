using QLyOcVit1.Model;
using System;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace QLyOcVit1.Model
{
    public class NhanVienModel
    {
        public string MaNV, Name, Email, SDT, TaiKhoan, MatKhau;
        public DateTime NgaySinh;
        public bool GioiTinh;

        public NhanVienModel()
        {
            
        }

        public NhanVienModel(XmlElement element)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(element.OwnerDocument.NameTable);
            manager.AddNamespace("tbl", element.NamespaceURI);

            MaNV = element.SelectSingleNode("@MaNV", manager).InnerText;
            Name = element.SelectSingleNode("tbl:TenNV", manager).InnerText;
            GioiTinh = bool.Parse(element.SelectSingleNode("tbl:GioiTinh", manager).InnerText);
            Email = element.SelectSingleNode("tbl:Email", manager).InnerText;
            SDT = element.SelectSingleNode("tbl:SDT", manager).InnerText;
            TaiKhoan = element.SelectSingleNode("tbl:TaiKhoan", manager).InnerText;
            MatKhau = element.SelectSingleNode("tbl:MatKhau", manager).InnerText;
        }

        public override bool Equals(object other)
        {
            return other is NhanVienModel && MaNV == ((NhanVienModel)other).MaNV;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(NhanVienModel one, NhanVienModel two)
        {
            return one.MaNV == two.MaNV;
        }

        public static bool operator !=(NhanVienModel one, NhanVienModel two)
        {
            return one.MaNV != two.MaNV;
        }
    }
}