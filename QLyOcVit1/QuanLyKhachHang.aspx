<%@ Page Title="Quản lý khách hàng" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QuanLyKhachHang.aspx.cs" Inherits="QLyOcVit1.QuanLyKhachHang" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a id="back" href='KhachHang.aspx' class="btn btn-link d-inline-flex align-items-center">
        <span class="material-symbols-outlined">arrow_back</span>
        Quay về
        <span class="bg-primary underline-slide"></span>
    </a>
    <h2 id="sanphamHeading" class="text-center mb-4" runat="server">
        <%= fieldsBox.InsertMode ? "Thêm khách hàng mới" : "Thông tin về khách hàng" %>
    </h2>
    <form id="form1" runat="server">
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= maKH.ClientID %>">Mã khách hàng</label>
            <input class="col-md-4 col-7 form-control" type="text" id="maKH" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= tenKH.ClientID %>">Họ tên khách hàng</label>
            <input class="col-md-4 col-7 form-control" type="text" id="tenKH" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row align-items-center">
            <label class="col-md-2 col-5 col-form-label" for="<%= gioiTinh.ClientID %>">Giới tính</label>
            <select class="col-md-4 col-7 form-control" id="gioiTinh" runat="server">
                <option value="false" selected="selected">Nam</option>
                <option value="true">Nữ</option>
            </select>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= email.ClientID %>">Email</label>
            <input class="col-md-4 col-7 form-control" type="text" id="email" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= sdt.ClientID %>">SĐT</label>
            <input class="col-md-4 col-7 form-control" type="text" id="sdt" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= ngaySinh.ClientID %>">Ngày sinh</label>
            <input class="col-md-4 col-7 form-control" type="date" id="ngaySinh" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= taiKhoan.ClientID %>">Tài khoản</label>
            <input class="col-md-4 col-7 form-control" type="text" id="taiKhoan" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= matKhau.ClientID %>">Mật khẩu mới</label>
            <input class="col-md-4 col-7 form-control" type="password" id="matKhau" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="d-flex justify-content-center my-3">
            <button id="moi_fake" type="button" class="btn btn-success btn-lg d-flex align-items-center mx-1" data-toggle="modal" data-target="#xacNhanLamMoi">
                <span class="material-symbols-outlined mr-1">add</span>
                Mới
            </button>
        
            <button id="luu" class="btn btn-primary btn-lg d-none" runat="server">
                <span class="material-symbols-outlined mr-1">edit</span>
                Lưu
            </button>

            <button type="button" id="luu_fake" class="btn btn-primary btn-lg d-flex align-items-center mx-1" onclick="confirmInsert()">
                <span class="material-symbols-outlined mr-1">edit</span>
                Lưu
            </button>
        
            <button id="xoa" class="btn btn-danger btn-lg d-none" runat="server">
                <span class="material-symbols-outlined mr-1">delete</span>
                Xoá
            </button>

            <button id="xoa_fake" type="button" class="btn btn-danger btn-lg d-flex align-items-center mx-1 <%= fieldsBox.InsertMode ? "disabled" : "" %>" data-toggle="modal" data-target="#xacNhanXoa">
                <span class="material-symbols-outlined mr-1">delete</span>
                Xoá
            </button>
        </div>
    </form>
    <div id="alertBox" class="alert" runat="server" visible="false">
        <div class="alert-dismissible">
            <a class="close" href="#" data-dismiss="alert"></a>
        </div>
        <div class="d-flex align-items-center">
            <span id="alertIcon" class="material-symbols-outlined mr-2" runat="server"></span>
            <span id="alertText" runat="server"></span>
        </div>
    </div>
    <div class="modal fade" id="xacNhanXoa">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Xoá khách hàng</h3>
                    <span class="close" data-dismiss="modal">&times;</span>
                </div>
                <div class="modal-body">
                    <p>Bạn có muốn xoá khách hàng này không?</p>
                </div>
                <div class="modal-footer">
                    <button onclick="$('#<%= xoa.ClientID %>').click()" class="btn btn-success">Có</button>
                    <button class="btn btn-secondary" data-dismiss="modal">Không</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="xacNhanLamMoi">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Thêm khách hàng mới</h3>
                    <span class="close" data-dismiss="modal">&times;</span>
                </div>
                <div class="modal-body">
                    <p>Bạn có muốn thêm khách hàng mới không? Các dữ liệu đang nhập sẽ bị huỷ.</p>
                </div>
                <div class="modal-footer">
                    <a href="?id=-1" class="btn btn-success">Có</a>
                    <button class="btn btn-secondary" data-dismiss="modal">Không</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        var insertMode = <%= fieldsBox.InsertMode ? "true" : "false" %>;
        function confirmInsert() {
            if (!validateInput()) return;
            $("#<%= luu.ClientID %>").click();
        }
        function clearStatus() {
            $(".input-error").text("");
        }
        function setError(jq, text) {
            jq.parent().find(".input-error").text(text);
        }
        function validateInput() {
            var valid = true;
            clearStatus();
            for (var field of ["#<%= maKH.ClientID %>", "#<%= tenKH.ClientID %>", "#<%= email.ClientID %>", "#<%= sdt.ClientID %>", "#<%= ngaySinh.ClientID %>", "#<%= taiKhoan.ClientID %>"]) {
                var jq = $(field);
                if (jq.val() === "") {
                    setError(jq, "Thông tin này là bắt buộc.");
                    valid = false;
                }
            }
            var jq = $("#<%= matKhau.ClientID %>");
            if (insertMode && jq.val() === "") {
                setError(jq, "Thông tin này là bắt buộc để tạo tài khoản mới.");
                valid = false;
            }
            if (!valid) return false;

            var jq = $("#<%= maKH.ClientID %>");
            if (isNaN(Number(jq.val()))) {
                setError(jq, "Mã khách hàng có dạng là số.");
                valid = false;
            }
            var jq = $("#<%= email.ClientID %>");
            if (!/^\w+@\w+\.\w+$/.test(jq.val())) {
                setError(jq, "Vui lòng ghi email đúng dạng.");
                valid = false;
            }
            var jq = $("#<%= sdt.ClientID %>");
            if (!/^\d{8,11}$/.test(jq.val())) {
                setError(jq, "Vui lòng ghi số điện thoại từ 8 tới 11 chữ số.");
                valid = false;
            }
            if (!valid) return false;

            return valid;
        }
    </script>
</asp:Content>