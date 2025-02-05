<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QuanLySanPham.aspx.cs" Inherits="QLyOcVit1.QuanLySanPham" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a id="back" href='<%= Request.RawUrl.Replace("QuanLySanPham.aspx", "XemSanPham.aspx") %>' class="btn btn-link d-inline-flex align-items-center">
        <span class="material-symbols-outlined">arrow_back</span>
        Quay về
        <span class="bg-primary underline-slide"></span>
    </a>
    <h2 id="sanphamHeading" class="text-center mb-4" runat="server">
        <%= fieldsBox.InsertMode ? "Thêm sản phẩm mới" : "Thông tin về sản phẩm" %>
    </h2>
    <form id="form1" runat="server">
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= maSP.ClientID %>">Mã sản phẩm</label>
            <input class="col-md-4 col-7 form-control" type="text" id="maSP" runat="server" placeholder="TTxxxx" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= tenSP.ClientID %>">Tên sản phẩm</label>
            <input class="col-md-4 col-7 form-control" type="text" id="tenSP" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row align-items-center">
            <label class="col-md-2 col-5 col-form-label" for="<%= hinh.ClientID %>">Hình</label>
            <input class="col-md-4 col-7 form-control" type="text" id="hinh" runat="server" onblur="autoLoadImg()" />
            <img class="col-auto" src="" id="hinhSanPham" width="64" height="64" alt="Hình" runat="server" />
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= donGia.ClientID %>">Giá</label>
            <input class="col-md-4 col-7 form-control" type="number" id="donGia" runat="server" min="0" step="1000" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= tgbh.ClientID %>">Thời gian bảo hành</label>
            <input class="col-md-3 col-4 form-control" type="number" id="tgbh" runat="server" min="0" />
            <span class="col-md-1 col-3 col-form-label">tháng</span>
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= moTa.ClientID %>">Mô tả</label>
            <input class="col-md-4 col-7 form-control" type="text" id="moTa" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= soLuongTon.ClientID %>">Số lượng tồn kho</label>
            <input class="col-md-4 col-7 form-control" type="number" id="soLuongTon" runat="server" min="0" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= xuatXu.ClientID %>">Xuất xứ</label>
            <input class="col-md-4 col-7 form-control" type="text" id="xuatXu" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= dvt.ClientID %>">DVT</label>
            <input class="col-md-4 col-7 form-control" type="text" id="dvt" runat="server" />
            <span class="col-md-6 col-12 input-error text-danger col-form-label"></span>
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= danhGia.ClientID %>">Đánh giá</label>
            <input class="col-md-4 col-7 form-control" type="number" id="danhGia" min="0" max="5" runat="server" step="0.1" />
        </div>
        <div class="row">
            <label class="col-md-2 col-5 col-form-label" for="<%= nhaSanXuat.ClientID %>">Nhà phân phối</label>
            <select class="col-md-4 col-7 form-control" id="nhaSanXuat" runat="server" datatextfield="TenNSX" datavaluefield="MaNSX"></select>
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
    <div class="modal fade" id="xacNhanLamMoi">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Thêm sản phẩm mới</h3>
                    <span class="close" data-dismiss="modal">&times;</span>
                </div>
                <div class="modal-body">
                    <p>Bạn có muốn thêm sản phẩm mới không? Các dữ liệu đang nhập sẽ bị huỷ.</p>
                </div>
                <div class="modal-footer">
                    <a href="?id=-1" class="btn btn-success">Có</a>
                    <button class="btn btn-secondary" data-dismiss="modal">Không</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function autoLoadImg() {
            $("#<%= hinhSanPham.ClientID %>").attr("src", $("#<%= hinh.ClientID %>").val());
        }
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
            for (var field of ["#<%= maSP.ClientID %>", "#<%= tenSP.ClientID %>", "#<%= donGia.ClientID %>", "#<%= tgbh.ClientID %>", "#<%= soLuongTon.ClientID %>", "#<%= dvt.ClientID %>", "#<%= danhGia.ClientID %>", "#<%= nhaSanXuat.ClientID %>"]) {
                var jq = $(field);
                if (jq.val() === "") {
                    setError(jq, "Thông tin này là bắt buộc.");
                    valid = false;
                }
            }
            if (!valid) return false;
            var jq = $("#<%= maSP.ClientID %>");
            if (!/TT\d{4}/.test(jq.val())) {
                setError(jq, "Mã sản phẩm có dạng TTxxxx, với mỗi x là mỗi chứ số.");
                valid = false;
            }
            for (var field of ["#<%= donGia.ClientID %>", "#<%= tgbh.ClientID %>", "#<%= danhGia.ClientID %>", "#<%= soLuongTon.ClientID %>"]) {
                var jq = $(field);
                if (jq.val() < 0) {
                    setError(jq, "Dữ liệu này không được là số âm.");
                    valid = false;
                }
            }
            if (!valid) return false;
            var jq = $("#<%= danhGia.ClientID %>");
            if (jq.val() > 5) {
                setError(jq, "Vui lòng đánh giá sản phẩm trên thang điểm 5.");
                valid = false;
            }
            if (!valid) return false;
            return valid;
        }
    </script>
</asp:Content>