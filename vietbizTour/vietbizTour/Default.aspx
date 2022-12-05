<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="michealSpa._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="vi" xml:lang="vi">
<head runat="server">
    <title></title>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <link href="/images/icon/favicon.png" rel="shortcut icon" type="image/vnd.microsoft.icon" />
</head>
<body>
    <asp:Label ID="idm" runat="server" Visible="false" Text="00"></asp:Label>
    <asp:Label ID="type" runat="server" Text="-1" Visible="false"></asp:Label>
    <asp:Label ID="slv" runat="server" Visible="false"></asp:Label>
    <asp:Literal ID="i" Visible="false" runat="server"></asp:Literal>

    <asp:Literal ID="lblHotline" Visible="false" runat="server"></asp:Literal>
    <asp:Literal ID="lblEmail" Visible="false" runat="server"></asp:Literal>
    <asp:Literal ID="lblSlogan" Visible="false" runat="server"></asp:Literal>
    <asp:Literal ID="lblFbN" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="lblFbL" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="lblFbID" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="lblTitle" Visible="false" runat="server"></asp:Literal>

    <div id="pnMain" runat="server">

        <link href="/styles/bootstrap.min.css" rel="stylesheet" />
        <link href="/styles/font.css" rel="stylesheet" />
        <link href="/styles/font-awesome.min.css" rel="stylesheet" />
        <link href="/scripts/owlcarousel/owl.carousel.css" rel="stylesheet" />
        <link href="/scripts/owlcarousel/owl.theme.default.min.css" rel="stylesheet" />
        <link href="/scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
        <link href="/scripts/bxslider/jquery.bxslider.css" rel="stylesheet" />
        <link href="/styles/style.css" rel="stylesheet" />
        <link href="/styles/mobile.css" rel="stylesheet" />

        <script src="/scripts/jquery.min.js"></script>
        <script src="/scripts/bootstrap.min.js"></script>
        <script src="/scripts/owlcarousel/owl.carousel.js"></script>
        <script src="/scripts/fancybox/jquery.fancybox.js"></script>
        <script src="/scripts/bxslider/jquery.bxslider.min.js"></script>
        <script src="/scripts/jquery.validate.min.js"></script>
        <script src="/scripts/script.min.js"></script>

        <div class="head">
            <div class="container">
                <div id="nav-icon4">
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
                <a href="/" class="logo">
                    <img src="/images/icon/logo.png" alt="logo" /></a>
                <div class="nav-bar" id="mynavbar">
                    <ul class="nav-main">
                        <li class="<%= IIf(idm.Text = "01", "ac", "") %>"><a href="/">Trang chủ</a></li>
                        <asp:Repeater ID="rptNavMain" runat="server">
                            <ItemTemplate>
                                <li class="<%# IIf(Eval("TotalM") <> 0, "parent", "") %> <%# IIf(Left(idm.Text,2) = Left(Eval("IDM"),2),"ac","") %>">
                                    <a href="<%# Eval("URL") %>"><%#Eval("NameM") %></a><%# IIF(Eval("TotalM") <> "0","<i class='icon_accoridion_expand'></i>","") %>
                                    <ul class="sub-nav mobile-dropdown" id="subnavmain" runat="server" visible="false">
                                        <asp:Repeater ID="rptSub" runat="server">
                                            <ItemTemplate>
                                                <li><a href="<%#Eval("URL") %>"><%#Eval("NameM") %></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>

            </div>
        </div>

        <div class="fix_head clearfix show-mobile"></div>
        <% If Request("id") = "" Then%>

        <div class="slider">
            <div class="owl-carousel owl-slider">
                <asp:Repeater ID="rptSlider" runat="server">
                    <ItemTemplate>
                        <img class="imgs" src="/images/media/<%#Eval("Img")%>" alt="<%#Replace(Eval("Title"),"""","")%>" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="slider-content-search">
                <h2 class="h3 text-center mgb20">Micheal Beauty hướng tới dịch vụ chất lượng và chuyên nghiệp</h2>
                <form action="/tim-kiem.html" class="frm-search clearfix mgb15" method="get">
                    <input type="text" id="key" name="key" autocomplete="off" onkeyup="SuggestSearch(this);" placeholder="Tìm kiếm dịch vụ" />
                    <button type="submit"><i class="fa fa-search"></i></button>
                </form>
            </div>
            <div class="down navigation">
                <a href="#section2">
                    <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABmJLR0QA/wD/AP+gvaeTAAAAcElEQVQ4je3SMQ6CUBCE4Q9uQ0OBjQkVoTEhsfCInMhbaPQQj2ZfKT4lJhb8zWazM7NbLD8mvRPUWzfsAf8YMGN8oR1jvkqPB87R50c64Ymh5KoDbrhEwIQ7jiXmTBchKWr3iTnT4Ir2G3Om2mIuYgE68A7v0Sci4gAAAABJRU5ErkJggg==" /><br>
                    <span>Xem thêm</span>
                </a>
            </div>
        </div>


        <div class="clearfix"></div>
        <div class="bg-home-1">
            <div class="container">
                <asp:Literal ID="lblItemPr" Visible="false" runat="server"></asp:Literal>
                <h2 class="main-heading center">Dịch vụ nổi bật</h2>
                <div style="margin: auto;max-width: 400px; padding-bottom: 30px">
                    <img src="images/icon/line-1.gif" style="width: 100%"/>
                </div>
                <div class="owl-carousel owl-theme owl3">
                    <asp:Repeater ID="rptProHot" runat="server">
                        <ItemTemplate>
                            <div class="news-item clearfix mgb30">
                                <div class="thumb iZoom">
                                    <a href="/<%#Eval("Name1P") %>-id2<%#Eval("IDP") %>.html">
                                        <img src="/images/service/<%#Eval("ImgP") %>" alt="<%#Eval("NameP")%>"/>
                                    </a>
                                </div>
                                <div class="content mgb20">
                                    <a href="/<%#Eval("Name1P")%>-id2<%#Eval("IDP")%>.html">
                                        <h3 class="clamp clamp2"><%#Eval("NameP")%></h3>
                                    </a>
                                    <p class="clamp clamp3"><%#Eval("ContentP") %></p>
                                </div>
                                <div class="feature clearfix">
                                    <div class="feature-item f-left">
                                        <strong>Thời lượng</strong>
                                        <span><%#Eval("completionTimeP") %></span>
                                    </div>
                                    <div class="feature-item f-right">
                                        <strong>Chi phí</strong>
                                        <span><%# IIf(Eval("Price") = "", "Liên hệ", Eval("Price")) %></span>
                                    </div>
                                </div>
                                <div class="bottom pdb10">
                                    <div class="detail">
                                        <a href="/<%#Eval("Name1P") %>-id2<%#Eval("IDP") %>.html">Xem chi tiết</a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div>
        <div class="bg-home-2 dd">
            <div class="container">
                <h2 class="main-heading center">Khóa học đào tạo</h2>
                <div style="margin: auto;max-width: 400px;padding-bottom: 30px">
                    <img src="images/icon/line-1.gif" style="width: 100%"/>
                </div>
                <div class="owl-carousel owl-theme owl4">
                    <asp:Repeater ID="rptPrDD" runat="server">
                        <ItemTemplate>
                            <div class="dd-item">
                                <a href="<%#Eval("Name") %>" target="_blank">
                                    <div class="dd-thumb iZoom">
                                        <img src="/images/media/<%#Eval("Img") %>" class="bdr10" alt="<%#Eval("Name") %>" />
                                    </div>
                                    <h4><%#Eval("Title") %></h4>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="bg-home-3 abs">
            <div class="container">
                <h2 class="main-heading  center">Về chúng tôi</h2>
                <div style="margin: auto;max-width: 400px;padding-bottom: 30px">
                    <img src="images/icon/line-1.gif" style="width: 100%"/>
                </div>
                <div class="row mgb30">
                    <div class="col-sm-8">
                        <div class="home-container home-block pdb30 pdt30">
                            <asp:Repeater ID="rptAbout" runat="server">
                                <ItemTemplate>
                                    <%#Eval("Head") %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="col-sm-4 pdt20">
                        <div class="fb-page" data-href="<%= lblFbL.Text %>" data-small-header="false" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true">
                            <blockquote cite="<%= lblFbL.Text %>" class="fb-xfbml-parse-ignore"><a href="<%= lblFbL.Text %>"><%= lblFbN.Text %></a></blockquote>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bg-home-2">
            <div class="container">
                <h2 class="main-heading center">Bài viết - Tin tức</h2>
                <div style="margin: auto;max-width: 400px;padding-bottom: 30px">
                    <img src="images/icon/line-1.gif" style="width: 100%"/>
                </div>
                <div class="owl-carousel owl-theme owl3">
                    <asp:Repeater ID="rptNewsHot" runat="server">
                        <ItemTemplate>
                            <div class='news-item'>
                                <div class="thumb iZoom">
                                    <a href='/<%#Eval("Name1N") %>-id3<%#Eval("IDN") %>.html'>
                                        <img class="bdr10" src='/images/news/<%#Eval("ImgN") %>' alt='<%#Eval("NameN")%>' /></a>
                                </div>
                                <div class="content">
                                    <a href='/<%#Eval("Name1N") %>-id3<%#Eval("IDN") %>.html'>
                                        <h4 class="clamp clamp2"><%#Eval("NameN")%></h4>
                                    </a>
                                    <p class="clamp clamp3"><%# Eval("ContentN") %></p>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div>
        <form id="frmDk" method="post" action="/ajax/register.aspx" class="form-register">
            <div class="container">
                <h3 class="b-title">Đăng kí nhận tư vấn dịch vụ miễn phí</h3>
                <div class="row" style="max-width: 800px; margin: auto;">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <input type="text" name="hoten" id="txthoten" placeholder="Họ và tên" autocomplete="off" value="" class="form-control" />
                        </div>
                        <div class="form-group">
                            <input type="text" name="dienthoai" id="txtdienthoai" placeholder="Số điện thoại" autocomplete="off" value="" class="form-control" />
                        </div>
                        <div class="form-group">
                            <input type="text" name="email" id="txtemail" placeholder="Địa chỉ email" autocomplete="off" value="" class="form-control" />
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <textarea name="noidung" id="txtnoidung" placeholder="Cho chúng tôi biết yêu cầu của quý khách" rows="3" style="height: 105px;" class="form-control"></textarea>
                        </div>
                        <div class="form-group">
                            <input id="btnSubmit" type="submit" class="btn btn-block" value="Gửi ngay" />
                        </div>
                    </div>
                </div>
            </div>
        </form>

        <div class="bg-home-4 bg-home-reviews">
           <div class="container">
                <h2 class="main-heading center">Nhận xét của khách hàng</h2>
               <div style="margin: auto;max-width: 400px;padding-bottom: 30px">
                    <img src="images/icon/line-1.gif" style="width: 100%"/>
                </div>
                <div class="owl-carousel owl-theme owl-reviews">
                    <asp:repeater id="rptReviews" runat="server">
                        <itemtemplate>
                            <div class="dg-item">
                                    <div class="dg-thumb shadowed izoom">
                                        <img src="/images/media/<%#Eval("img")%>" class="bdr10" alt="facebook"/>
                                    </div>
                                    <h5><%#Eval("Title") %></h5>
                                    <div class="reviews-item-rating">
                                        <span class="fa fa-star" style="color: orange;"></span>
                                        <span class="fa fa-star" style="color: orange;"></span>
                                        <span class="fa fa-star" style="color: orange;"></span>
                                        <span class="fa fa-star" style="color: orange;"></span>
                                        <span class="fa fa-star" style="color: orange;"></span>
                                    </div>
                                    <p class="clamp clamp3" style="text-transform: capitalize;"><%#Eval("ContentA") %></p>
                            </div>
                        </itemtemplate>
                    </asp:repeater>
                </div>
            </div>
        </div>
        <% Else%>

        <div class="banner_top" style="background: url(/images/bg/138.jpg);">
            <% If Left(Request("id"), 1) <> "3" Then%>
            <div class="container">
                <ul class="beadcrumb">
                    <asp:Literal ID="lblLink" runat="server"></asp:Literal>
                </ul>
                <h1 class=" main-heading topic"><% = lblTitle.Text %></h1>

            </div>
            <% End If%>
        </div>
        <asp:Repeater ID="rptProD2" runat="server">
            <ItemTemplate>
                <div class="pr-detailt">
                    <div class="container">
                        <h1 class="main-heading  left mgb30"><%#Eval("NameP") %></h1>
                        <div class="row">
                            <div class="col-sm-6 mgb30">
                                <div class="info__bg"> <img src="/images/project/<%#Eval("Img1P") %>" alt="<%#Eval("NameP") %>" /></div>
                                <div class="info__btn--group">
                                    <div class="group__litem">
                                        <img src="images/icon/img.png" />
                                    </div>
                                    <div class="group__litem">
                                        <a data-fancybox href="<%# Eval(" VideoP") %>"> <img src="images/icon/video.png" /></a>
                                    </div>
<%--                                    <div class="share-post mgb30">
                                        <div ><strong style="font-size: 18px;">Chia sẻ</strong></div>
                                        <ul class="social">
                                            <li>
                                                <img src="/images/icon/fb.png">
                                            </li>
                                            <li>
                                                <img src="/images/icon/twitter.png">
                                            </li>
                                        </ul>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-sm-6 mgb30">
                                <h2 class="mgb30"><strong>Tổng quan</strong></h2>
                                <strong>
                                    <%#Eval("ContentP")%>
                                </strong>
                                <div class='feature clearfix mgb0' style="max-width:300px;border:none;">
                                    <div class="feature-item f-left">
                                        <strong>Thời lượng</strong>
                                        <span>
                                            <%#Eval("completionTimeP") %>
                                        </span>
                                    </div>
                                     <div class="feature-item f-right price">
                                        <strong>Chi phí</strong>
                                        <strong style="font-size:22px;color:#ff0000;">
                                            <%# IIf(Eval("Price") = "", "Liên hệ", Eval("Price")) %>
                                        </strong>
                                    </div>
                                </div>
                                <div class="detail book mgb30"><a href="#">Đặt ngay</a></div>
                                <div class="contacts">
                                    <div class="mgb20"><strong>Bạn cần hỗ trợ?</strong></div>
                                    <form method="post">
                                        <input type="text" autocomplete="off" placeholder="Tên của bạn" required="required"
                                            class="form-control mgb10 input-lg bdr20" id="name" name="name" />
                                        <input type="text" autocomplete="off" placeholder="Số điện thoại" required="required"
                                            class="form-control input-lg bdr20 mgb20" id="phone" name="phone" />
                                        <button type="submit" class=" btn btn-success btn-lg btn-block bdr20 btn-phone">Gửi yêu cầu tư vấn</button>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <%--TabUI--%>
                        <div class="tabs">
                            <a class="tab-item active">
                                Chi tiết
                            </a>
                            <div class="line"></div>
                        </div>
                          <!-- Tab content -->
                        <div class="tab-content">
                            <div class="tab-pane active">
                                <%#Eval("TextHome") %>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <%--Tabs - JS--%>
<%--        <script type="text/javascript">
            $(document).ready(function () {

                var panes = $$('.tab-pane');

                $('.tab-item').click(function () {
                    var index = $(".tab-item").index(this);
                    var pane = panes[index];

                    $('.tab-item.active').removeClass('active');
                    $('.tab-pane.active').removeClass('active');

                    $(this).addClass('active');
                    pane.addClass('active');
                });
            });
        </script>--%>

        <asp:Repeater ID="rptNewsDt" runat="server">
            <ItemTemplate>
                <div class="container" >
                    <div class="row">
                        <div class="col-md-9">
                             <div class="detail-post">
                                <div class="title-post shadowed">
                                    <h1><%#Eval("NameN") %></h1>
                                </div>
                                <div class="content-post content-new mgb30"><%#Eval("DescN")%></div>
                                <div class="share-post text-center ngb30">
                                    <div><strong style="font-size: 18px;">Chia sẻ</strong></div>
                                    <ul class="social">
                                        <li>
                                            <img src="/images/icon/fb.png"></li>
                                        <li>
                                            <img src="/images/icon/twitter.png"></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="suggest-service">
                                <div class="detail-post-service">
                                    <span class="fa fa-home"></span>
                                    DỊCH VỤ NỎI BẬT
                                </div>
                                <asp:Repeater ID="rptSuggestService" runat="server">
                                    <ItemTemplate>
                                    <div class="row service-items">
                                        <div class="service-item">
                                            <a href="/<%#Eval("Name1P") %>-id2<%#Eval("IDP") %>.html">
                                                <img class="service-item-img lazyloaded" src="/images/service/<%#Eval("ImgP") %>" alt="<%#Eval("NameP") %>"/>
                                                <h5 class="service-item-heading"><%#Eval("NameP") %></h5>
                                            </a>
                                        </div>
                                    </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="container">
            <% i.Text = 0%>
            <h3 class="main-heading center">Bạn có thể quan tâm</h3>
            <asp:Repeater ID="rptPro" runat="server">
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <% i.Text = i.Text + 1%>
                    <div class="col-md-4 col-sm-6">
                        <div class='news-item bg-gray clearfix mgb30' style="height: 535px;">
                            <div class="thumb iZoom">
                                <a href='/<%#Eval("Name1P") %>-id2<%#Eval("IDP") %>.html'>
                                    <img src='/images/project/<%#Eval("ImgP") %>' alt='<%#Eval("NameP")%>' /></a>
                            </div>
                            <div class='content mgb10'>
                                <a href='/<%#Eval("Name1P")%>-id2<%#Eval("IDP")%>.html'>
                                    <h3 class="clamp clamp2"><%#Eval("NameP")%></h3>
                                </a>
                                <p class="clamp clamp3"><%#Eval("ContentP") %></p>
                            </div>
                            <div class='feature clearfix'>
                                <div class="feature-item f-left">
                                    <strong>Thời lượng</strong>
                                    <span><%# IIf(Eval("completionTimeP") = "0", "Liên hệ", Eval("completionTimeP")) %></span>
                                </div>
                                <div class="feature-item f-right">
                                    <strong>Chi phi</strong>
                                    <span><%# IIf(Eval("Price") = "0", "Giá liên hệ", Eval("Price")) %></span>
                                </div>
                            </div>
                            <div class='bottom'>
                                <div class='detail'><a href="/<%#Eval("Name1P") %>-id2<%#Eval("IDP") %>.html">Xem chi tiết</a></div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Repeater ID="rptNews" runat="server">
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-sm-4">
                        <div class='news-item bg-gray mgb30'>
                            <div class="thumb iZoom">
                                <a href='/<%#Eval("Name1N") %>-id3<%#Eval("IDN") %>.html'>
                                    <img class="bdr10" src='/images/news/<%#Eval("ImgN") %>' alt='<%#Eval("NameN")%>' /></a>
                            </div>
                            <div class="content">
                                <a href='/<%#Eval("Name1N") %>-id3<%#Eval("IDN") %>.html'>
                                    <h4 class="clamp clamp2"><%#Eval("NameN")%></h4>
                                </a>
                                <p class="clamp clamp3"><%# Eval("ContentN") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>

            <asp:Repeater ID="rptIntroM" runat="server">
                <ItemTemplate>
                    <div class="detail-post">
                        <div class="content-post"><%#Eval("DescM")%></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnDk" runat="server" Visible="false">
                <form id="frmDk1" method="post" action="/ajax/contact.aspx">
                    <h4>Thông tin liên hệ</h4>
                    <div class="form-group">
                        <input type="text" name="hoten" id="txthoten1" placeholder="Họ và tên" autocomplete="off" value="" class="form-control" />
                    </div>
                    <div class="form-group">
                        <input type="text" name="dienthoai" id="txtdienthoai1" placeholder="Số điện thoại" autocomplete="off" value="" class="form-control" />
                    </div>
                    <div class="form-group">
                        <input type="text" name="email" id="txtemail1" placeholder="Địa chỉ email" value="" autocomplete="off" class="form-control" />
                    </div>
                    <div class="form-group">
                        <textarea name="noidung" id="txtnoidung1" placeholder="Cho chúng tôi biết yêu cầu của quý khách" autocomplete="off" rows="3" style="height: 95px;" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <input id="btnSubmit1" type="submit" class="btn btn-block" value="Gửi" />
                    </div>
                </form>
            </asp:Panel>

            <div class="page-nummber">
                <asp:Literal runat="server" ID="lblPage" />
            </div>
        </div>
        <% End If%>
        <div class="footer">
            <div class="container">
                <asp:Repeater ID="rptFooter" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-sm-5">

                                <div class="content-post"><%#Eval("Footer") %></div>

                            </div>
                            <div class="col-sm-3 pdt30">
                                <strong>Theo dõi chúng tôi</strong>
                                <ul class="social mgb20">
                                    <li>
                                        <img src="/images/icon/fb.png" />
                                    </li>
                                    <li>
                                        <img src="/images/icon/insta.png" />
                                    </li>
                                    <li>
                                        <img src="/images/icon/pin.png" />
                                    </li>
                                    <li>
                                        <img src="/images/icon/twitter.png" />
                                    </li>
                                </ul>

                            </div>
                            <div class="col-sm-4 pdt30">
                                <%#Eval("Contact") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="fb-root"></div>
        <script type="text/javascript" async>
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = 'https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.11';
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));
        </script>
    </div>
    <div id="pnProject" runat="server" visible="false">
        <link href="/duan/boostrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

        <link href="/duan/main-style.css" rel="stylesheet" />
        <link href="/duan/nam.css" rel="stylesheet" />
        <script src="/duan/jquery.min.js"></script>
        <script src="/duan/bootstrap.min.js"></script>
        <script src="/duan/requiredjs.js"></script>
        <script src="/duan/jquery.bxslider.min.js"></script>
        <script src="/duan/requiredjs2.js"></script>
        <script src="/duan/fancybox/jquery.fancybox.js"></script>
        <link href="/duan/fancybox/jquery.fancybox.css" rel="stylesheet" />

        <asp:Literal ID="lblLogo" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="lblVideoP" Visible="false" runat="server"></asp:Literal>
        <header class="page-header navbar navbar-inverse">
            <div class="container-cus">
                <div class="navbar-header">
                    <div class="logo-wrapper visible-xs visible-sm">
                        <div class="logo pull-left">
                            <a href="javascript:;" class="logo btn-scroll">
                                <img class="img-responsive" src="/images/icon/logo.png" alt="" title="" />
                            </a>
                        </div>
                    </div>
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="collapse navbar-collapse navbar-ex1-collapse SFUFuturaBook">
                    <div class="row">
                        <div class="col-md-2 hidden-xs hidden-sm">
                            <a href="/" class="logo">
                                <img class="img-responsive logo1" src="/images/icon/logo.png" alt="" title="" />
                                <img class="img-responsive logo2" src="/images/icon/logo.png" alt="" title="" />
                            </a>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-10">
                            <ul class="nav-menu nav navbar-nav navbar-nav-right">
                                <li class="first home menu-link"><a data-link="section_banner">Trang chủ</a></li>
                                <asp:Repeater ID="rptMenuLink" runat="server">
                                    <ItemTemplate>
                                        <li class="menu-link"><a data-link="section_<%#Eval("AutoM") %>"><%#Eval("NameM") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%--<li class="menu-link"><a data-link="section_tienich">Tiện ích</a></li>--%>
                                <li class="menu-link"><a data-link="section_img">VIDEO - Hình ảnh</a></li>
                                <asp:Repeater ID="rptMenuLink2" runat="server">
                                    <ItemTemplate>
                                        <li class="menu-link"><a data-link="section_<%#Eval("AutoM") %>"><%#Eval("NameM") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <li class="menu-link"><a data-link="section_register">LIÊN HỆ</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </header>
        <h1 class="hidden"><% = Page.Title %></h1>
        <div id="section_banner" class="animatedParent animateOnce">
            <div class="bxslider">
                <asp:Repeater ID="rptSliderP" runat="server">
                    <ItemTemplate>
                        <div class='item' data-title='<div class="box-slider doanimation fadeInRightShort delay-1000"><strong><%#Eval("Title") %></strong><span><%#Eval("Name") %></span><p><%#Eval("ContentA") %></p></div>' style='background-image: url("/images/media/duan/<%# Eval("Img") %>")'></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="slider-content animatedParent">
            </div>
            <a class="fancybox fancy-video btn btn-video" data-fancybox href="<% = lblVideoP.Text %>">
                <img src="/duan/icon-video.png?v=100" alt="Xem video" />
            </a>
        </div>
        <asp:Repeater ID="rptSectionAbout" runat="server">
            <ItemTemplate>
                <div id="section_<%#Eval("AutoM") %>" class="section-about animatedParent animateOnce">
                    <div class="container">
                        <h2 class="title"><%# Eval("NameMH").ToString.Replace("-", "-<span>") %></span></h2>
                        <div class="content-post"><%#Eval("DescM") %></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div id="section_tienich" class="section-utilities">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box-utilities text-center">
                            <div class="wrap-title">
                                <h2 class="title">CUỘC SỐNG HIỆN ĐẠI  -<span> TIỆN NGHI -<span> VĂN MINH</span></span></h2>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Repeater ID="rptUtilitiesTienIch" runat="server">
                        <ItemTemplate>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                <div class="utility-block">
                                    <div class="image">
                                        <img src="/images/media/duan/<%#Eval("Img") %>" class="img-responsive center-block" alt="<%#Eval("Title") %>" />
                                    </div>
                                    <div class="title"><span><%#Eval("Title") %></span></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>

        <div class="section-picture360">
            <div id="main360">
                <div class=" gutter-0 row-eq-height">

                    <div class="col-sm-6 div360">
                        <div id="section_video" class="inner">
                            <a data-fancybox href="<% = lblVideoP.Text %>">
                                <img src="/duan/view360.png" class="img-responsive" alt="Xem video" />
                            </a>
                            <h3><strong>VIDEO GIỚI THIỆU DỰ ÁN</strong> </h3>
                        </div>
                    </div>


                    <div class="col-sm-6 div360 div360-2">
                        <div id="section_img" class="inner">
                            <a data-fancybox="ajax" data-src="/ajax/images.ashx?pid=<%= Request("id").Substring(1) %>" data-type="ajax" href="javascript:;">
                                <img src="/duan/icon-thuvien.png?v=100" class="img-responsive" alt="thư viện hình ảnh" />
                            </a>
                            <h3><strong>HÌNH ẢNH CHI TIẾT DỰ ÁN</strong> </h3>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <asp:Repeater ID="rptSectionAbout2" runat="server">
            <ItemTemplate>
                <div id="section_<%#Eval("AutoM") %>" class="section-about animatedParent animateOnce">
                    <div class="container">
                        <h3 class="title"><%# Eval("NameMH").ToString.Replace("-", "-<span>") %></span></h3>
                        <div class="content-post"><%#Eval("DescM") %></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="section-texthome">
            <div class="container">
                <asp:Repeater ID="rptTextHome" runat="server">
                    <ItemTemplate>
                        <div class="content-post"><%#Eval("TextHome") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="section_register">
            <div class="wrap-register">
                <div class="container">
                    <div class="row">
                        <div class="col-xs-12 col-md-10  col-lg-6 col-md-offset-1 col-lg-offset-3">
                            <div class="contact-form">
                                <h2 class="title">Thông tin liên lạc</h2>
                                <div class="row">
                                    <form accept-charset='UTF-8' action='/ajax/register.aspx' class='contact-form' method='post'>
                                        <input name='form_type' type='hidden' value='contact'>
                                        <input name='utf8' type='hidden' value='✓'>
                                        <div class="item col-xs-12 col-sm-6">
                                            <input type="text" id="txtName" name="hoten" class="textbox txtcontactName" placeholder="HỌ VÀ TÊN" data-validation-error-msg="Không được để trống" data-validation="required" required />
                                        </div>
                                        <div class="item col-xs-12 col-sm-6">
                                            <input type="text" id="txtMobile" name="dienthoai" class="textbox txtcontactMobile" placeholder="SỐ ĐIỆN THOẠI" data-validation-error-msg="Không được để trống" data-validation="required" required />
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <input type="text" id="txtEmail" name="email" class="textbox txtcontactEmail" data-validation="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,63}$" data-validation-error-msg="Email sai định dạng" placeholder="EMAIL" />
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <input type="text" id="txtAddress" name="diachi" class="textbox txtcontactAddress" placeholder="ĐỊA CHỈ LIÊN HỆ" data-validation-error-msg="Không được để trống" data-validation="required" required />
                                        </div>
                                        <div class="col-xs-12 col-sm-12">
                                            <textarea id="txtMessage" name="noidung" class="textbox textarea txtcontactMessage" placeholder="GỬI YÊU CẦU CỦA BẠN" data-validation-error-msg="Không được để trống" data-validation="required" required></textarea>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-xs-12 col-sm-3 text-center">
                                            <button id="cmdSend" class="cmdContactSend bt-send" type="submit">Gửi</button>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Literal ID="Youtube" Visible="false" runat="server"></asp:Literal>
        </div>
        <footer id="footer">
            <div class="container">
                <asp:Repeater ID="rptFooterP" runat="server">
                    <ItemTemplate>
                        <div class="content-post"><%#Eval("Footer") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </footer>
        <script src="/duan/homepage.js"></script>
        <script>
            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) {
                    return null;
                } else {
                    return results[1] || 0;
                }
            }
        </script>
    </div>
    <link href="/styles/alo.css" rel="stylesheet" />

    <div class="hotline-phone-ring-wrap show-desktop">
        <div class="hotline-phone-ring">
            <div class="hotline-phone-ring-circle"></div>
            <div class="hotline-phone-ring-circle-fill"></div>
            <div class="hotline-phone-ring-img-circle">
                <a href="tel:<% = lblHotline.Text.Replace(".", "") %>" rel="nofollow" class="pps-btn-img">
                    <img src="/images/icon/ring-phone.png" alt="Hotline" width="50"></a>
            </div>
        </div>
        <div class="hotline-bar"><a href="tel:<% = lblHotline.Text.Replace(".", "") %>" rel="nofollow"><span class="text-hotline">Hotline <% = lblHotline.Text %></span> </a></div>
    </div>

    <div class="bottom-contact show-mobile">
        <ul>
            <li><a id="chatzalo" href="http://zalo.me/0902456979" rel="nofollow">
                <img class=" lazyloaded" src="/images/icon/widget_m_icon_zalo.svg"><br>
                <span>Zalo</span> </a></li>
            <li><a id="chatfb" href="https://www.messenger.com/t/badiland.com.vn" rel="nofollow">
                <img class=" lazyloaded" src="/images/icon/widget_m_icon_facebook.svg"><br>
                <span>Messenger</span> </a></li>
            <li><a id="goidien" href="tel:<% = lblHotline.Text.Replace(".", "") %>" rel="nofollow">
                <img class=" lazyloaded" src="/images/icon/widget_m_icon_click_to_call.svg"><br>
                <span>Gọi ngay</span> </a></li>
        </ul>
    </div>

    <div id="dLogin" class="shadowed popup">
        <div class="popup-heading main-heading center">Đăng nhập</div>
        <div class="popup-body">
            <form action="/login" method="post">
                <input id="tendangnhhap" name="tendangnhhap" type="text" required="required" placeholder="Tài khoản login" class="form-control input-lg bdr15 mgb10" />
                <input id="matkhau" name="matkhau" type="password" required="required" placeholder="Mật khẩu" class="form-control input-lg bdr15 mgb20" />
                <input id="dangnhap" class="btn btn-primary btn-block btn-lg bdr20 shadow" type="submit" value="Đăng nhập" />
            </form>
        </div>
    </div>
    <div id="dOverlay"></div>
    <div id="back-to-top" class="scroll-top" style="display: block;"></div>
</body>
</html>
