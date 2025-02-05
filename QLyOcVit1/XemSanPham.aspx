<%@ Page Title="Thông tin về sản phẩm" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="XemSanPham.aspx.cs" Inherits="QLyOcVit1.XemSanPham" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a id="back" href="SanPham.aspx" class="btn btn-link d-inline-flex align-items-center">
        <span class="material-symbols-outlined">arrow_back</span>
        Quay về
        <span class="bg-primary underline-slide"></span>
    </a>
    <h2 class="text-center mb-4">Thông tin về sản phẩm</h2>
    <div class="row">
        <div class="col-md-3 col-12 d-flex align-items-center">
            <img src="<%= fieldsBox.CurrentDict["Hinh"] %>" alt="Hình" class="img-thumbnail" style="aspect-ratio: 1;" />
        </div>
        <div class="col-md-9 col-12">
            <h4><%= fieldsBox.CurrentDict["TenSP"] %></h4>
            <p class="text-danger h5"><%= (int.Parse(fieldsBox.CurrentDict["DonGia"])).ToString("n0") %>₫ / 1 <%= fieldsBox.CurrentDict["DVT"] %></p>
            <p class='<%= fieldsBox.CurrentDict.GetInt("SoLuongTon") > 0 ? "text-success" : "text-danger" %>'>
                <%= fieldsBox.CurrentDict.GetInt("SoLuongTon") > 0 ? $"Còn {fieldsBox.CurrentDict["SoLuongTon"]} sản phẩm" : "Đã hết hàng" %>
            </p>
            <p><%= fieldsBox.CurrentDict.GetInt("TGBH") > 0 ? $"Bảo hành: {fieldsBox.CurrentDict.GetInt("TGBH")} tháng" : "Bảo hành: Không"%></p>
            <p class="d-flex align-items-center">
                <span>Đánh giá: </span>
                <span class="text-warning d-inline-flex">
                 <% for (int j = 1; j <= 5; j++) {
                        if (fieldsBox.CurrentDict.GetFloat("DanhGia") >= j) { %>
                            <span class='material-symbols-outlined-filled'>star</span>
                 <%     }
                        else if (fieldsBox.CurrentDict.GetFloat("DanhGia") >= j - 0.5) { %>
                            <span class='material-symbols-outlined-filled'>star_half</span>
                 <%     }
                        else { %>
                            <span class='material-symbols-outlined'>star</span>
                 <%     }
                    } %>
                </span>
                <span>(<%= fieldsBox.CurrentDict["DanhGia"] %>)</span>
            </p>
            <p>Xuất xứ: <%= fieldsBox.CurrentDict["XuatXu"] %></p>
            <p>Nhà phân phối: <%= NhaSanXuat %></p>
            <p class="row">
                <label class="col-auto col-form-label" for="<%= soLuongMua.ClientID %>" form="formMua">Số lượng cần mua: </label>
                <input class="col-2 form-control" type="number" id="soLuongMua" runat="server" value="1" min="1" form="formMua" />
            </p>
        </div>
    </div>
    <h4>Mô tả</h4>
    <p><%= fieldsBox.CurrentDict["MoTa"] %></p>
    <form id="formMua" class="d-flex justify-content-center my-3" runat="server">
        <button id="mua" class="btn btn-success btn-lg d-flex align-items-center mx-1" runat="server" onserverclick="Mua">
            <span class="material-symbols-outlined mr-1">shopping_cart</span>
            Mua
        </button>

        <a id="chinhSua" href='QuanLySanPham.aspx?id=' class="btn btn-primary btn-lg d-flex align-items-center mx-1" runat="server">
            <span class="material-symbols-outlined mr-1">build</span>
            Sửa
        </a>
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
</asp:Content>