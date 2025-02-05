<%@ Page Title="Đăng ký" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="QLyOcVit1.DangKy" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a id="back" href="DangNhap.aspx" class="btn btn-link d-inline-flex align-items-center">
        <span class="material-symbols-outlined">arrow_back</span>
        Quay về
        <span class="bg-primary underline-slide"></span>
    </a>
    <h2 class="text-center mb-4" runat="server">Đăng ký</h2>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <input type="hidden" id="maKH" runat="server" class="form-control" />
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="tenKH">Họ tên</label>
                    <input class="col-md-3 col-6 form-control" type="text" id="tenKH" runat="server" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="gioiTinh">Giới tính</label>
                    <select class="col-md-3 col-6 form-control" id="gioiTinh" runat="server">
                        <option value="false" selected="selected">Nam</option>
                        <option value="true">Nữ</option>
                    </select>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="email">Email</label>
                    <input class="col-md-3 col-6 form-control" type="text" id="email" runat="server" placeholder="khachhang@email.com" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="sdt">SĐT</label>
                    <input class="col-md-3 col-6 form-control" type="text" id="sdt" runat="server" placeholder="0901234567" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="ngaySinh">Ngày sinh</label>
                    <input class="col-md-3 col-6 form-control" type="date" id="ngaySinh" runat="server" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="taiKhoan">Tài khoản</label>
                    <input class="col-md-3 col-6 form-control" type="text" id="taiKhoan" runat="server" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="row">
                    <label class="col-md-3 col-6 col-form-label" for="matKhau">Mật khẩu</label>
                    <input class="col-md-3 col-6 form-control" type="password" id="matKhau" runat="server" />
                    <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
                </div>
                <div class="d-flex justify-content-center my-3">
                    <button id="luu" class="btn btn-primary d-none" runat="server">
                        <span class="material-symbols-outlined mr-1">edit</span>
                        Tạo
                    </button>

                    <button id="luu_fake" type="button" class="btn btn-primary btn-lg d-flex align-items-center" onclick="confirmInsert()">
                        <span class="material-symbols-outlined mr-1">edit</span>
                        Tạo
                    </button>
                </div>
            </div>
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
    <script>
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
            $(".input-error").text("");
            for (var field of ["#<%= tenKH.ClientID %>", "#<%= email.ClientID %>", "#<%= sdt.ClientID %>", "#<%= ngaySinh.ClientID %>", "#<%= taiKhoan.ClientID %>", "#<%= matKhau.ClientID %>"]) {
                var jq = $(field);
                if (jq.val() === "") {
                    setError(jq, "Thông tin này là bắt buộc.");
                    valid = false;
                }
            }
            if (!valid) return false;
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
            var jq = $("#<%= matKhau.ClientID %>");
            if (!/\w{5,}/.test(jq.val())) {
                setError(jq, "Vui lòng ghi mật khẩu với ít nhất 5 ký tự chữ thường, CHỮ HOA hoặc số liên tiếp.");
                valid = false;
            }
            if (!valid) return false;
            return valid;
        }
    </script>
</asp:Content>