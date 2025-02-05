<%@ Page Title="Cửa hàng bán đồ thể thao Trí Hoà" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="QLyOcVit1._default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="cuaHang" class="carousel slide" data-ride="carousel">
        <ul class="carousel-indicators">
            <li class="active" data-slide-to="0" data-target="#cuaHang"></li>
            <li data-slide-to="1" data-target="#cuaHang"></li>
            <li data-slide-to="2" data-target="#cuaHang"></li>
            <li data-slide-to="3" data-target="#cuaHang"></li>
        </ul>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="https://atpsoftware.vn/wp-content/uploads//2018/11/coppy.jpg" width="100%" height="500" alt="Cửa hàng bán đồ thể thao" />
            </div>
        
            <div class="carousel-item">
                <img src="https://top10tphcm.com/wp-content/uploads/2019/02/top-cua-hang-ban-dung-cu-the-thao-chuyen-nghiep-tai-Da-Nang.jpg" width="100%" height="500" alt="Cửa hàng bán đồ thể thao" />
            </div>

            <div class="carousel-item">
                <img src="https://www.thethaothientruong.vn/uploads/2022/dung-cu-the-thao-nam-dinh.jpg" width="100%" height="500" alt="Cửa hàng bán đồ thể thao" />
            </div>

            <div class="carousel-item">
                <img src="https://suno.vn/blog/wp-content/uploads/2015/09/sportshop1.jpg" width="100%" height="500" alt="Cửa hàng bán đồ thể thao" />
            </div>
        </div>
        <a class="carousel-control-prev" href="#cuaHang" data-slide="prev">
            <span class="carousel-control-prev-icon"></span>
        </a>
        <a class="carousel-control-next" href="#cuaHang" data-slide="next">
            <span class="carousel-control-next-icon"></span>
        </a>
    </div>
    <h4 class="mt-5">Cửa hàng bán đồ thể thao Trí Hoà - Nơi đam mê thể thao được chắp cánh</h4>
    <p>Bạn đang tìm kiếm trang phục thể thao chất lượng cao để nâng tầm hiệu suất tập luyện? Hay đơn giản là muốn sở hữu những phụ kiện thể thao thời thượng để thể hiện cá tính? Hãy đến với cửa hàng Trí Hoà, nơi đam mê thể thao được chắp cánh!</p>
    <p>Tại cửa hàng Trí Hoà, chúng tôi bán các sản phẩm chuyên về thể thao với đa dạng các thương hiệu, mẫu mã và đặc biệt nhất là các sản phẩm của chúng tôi được nhập chính hãng với mức giá phải chăng, phù hợp với thị trường.</p>
    <p>Không những vậy, với đội ngũ nhân viên nhiệt tình, am hiểu sâu về các dụng cụ thể thao và sẵn sàng tư vấn và đưa ra lời khuyên tới khách hàng, cửa hàng của chúng tôi hứa hẹn đưa ra những lựa chọn phù hợp nhất cho các khách hàng khác nhau.</p>
    <h4>Sứ mệnh</h4>
    <p>Để cửa hàng Trí Hoà trở thành <b>nơi đam mê thể thao được chắp cánh</b>, cửa hàng chúng tôi luôn cung cấp những sản phẩm thể thao với đa dạng các thương hiệu, mẫu mã và được nhập chính hãng với mức giá phải chăng, bảo đảm mang lại sự hài lòng tốt nhất cho khách hàng.</p>
    <p>Đội ngũ nhân viên của chúng tôi luôn nhiệt tình, am hiểu sâu về các dụng cụ thể thao, hứa hẹn đưa ra những lời khuyên tốt nhất phù hợp với nhu cầu cho các khách hàng khác nhau.</p>
    <h4>Hướng dẫn sử dụng</h4>
    <p>Khi quý khách kéo thanh cuộn lên trên đầu trang, trên cùng của quý khách là phần danh mục. Đây là khu vực mà quý khách có thể điều hướng qua các mục khác nhau.</p>
    <p>Website của chúng tôi có một số mục nổi bật như sau:</p>
    <ol>
        <li>Mục <a href="SanPham.aspx">Sản phẩm</a>: Mục này liệt kê các sản phẩm được bán tại cửa hàng. Quý khách có thể xem thông tin về sản phẩm và đặt hàng. Quý khách cần phải có tài khoản trước khi đặt hàng.</li>
        <li>Mục <a href="GioHang.aspx">Giỏ hàng</a>: Mục này là nơi các sản phẩm được đặt và chờ được thanh toán. Quý khách có thể thanh toán hoặc xoá một số sản phẩm không muốn. Quý khách phải đăng nhập tài khoản trước khi sử dụng mục này.</li>
        <li>Mục <a href="DangNhap.aspx">Đăng nhập</a>: Mục này cho phép quý khách đăng nhập vào cửa hàng bằng tài khoản của quý khách. Nếu không có tài khoản, quý khách có thể đăng ký tài khoản (xem bên dưới).</li>
        <li>Mục <a href="DangKy.aspx">Đăng ký</a>: Mục này cho phép quý khách tạo tài khoản và cho phép quý khách đặt hàng. Quý khách điền các thông tin cần thiết để tạo tài khoản.</li>
    </ol>
    <p class="mt-4"><i>Cảm ơn quý khách đã ghé qua cửa hàng của chúng tôi. Mua sắm vui vẻ!</i></p>
</asp:Content>