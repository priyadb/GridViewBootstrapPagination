<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridViewBootstrapPagination.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bootstrap Pagination for GridView</title>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery.bootpag.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // init bootpag
            var count = GetTotalPageCount();
            $('#page-selection').bootpag(
                {
                    total:count
                }).on("page", function (event, num) {
                    GetGridData(num);
            });
        });

        function GetGridData(num) {
        
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetRevenueDetail",
                data: "{ \"pagenumber\":" + num + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    bindGrid(data.d);
                },
                error: function (xhr, status, err) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);

                }
            });
        }

        function bindGrid(data) {
            $("[id*=gvBSPagination] tr").not(":first").not(":last").remove();
            var table1 = $('[id*=gvBSPagination]');
            var firstRow = "$('[id*=gvBSPagination] tr:first-child')";
            for (var i = 0; i < data.length; i++) {

                var rowNew = $("<tr><td></td><td></td><td></td><td></td></tr>");
                rowNew.children().eq(0).text(data[i].country);
                rowNew.children().eq(1).text(data[i].revenue);
                rowNew.children().eq(2).text(data[i].salesmanager);
                rowNew.children().eq(3).text(data[i].year);
                rowNew.insertBefore($("[id*=gvBSPagination] tr:last-child"));
            }
        }

        function GetTotalPageCount() {
            var mytempvar = 0;
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetTotalPageCount",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async:false,
                success: function (data) {
                    mytempvar=data.d;
                    
                },
                error: function (xhr, status, err) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);

                }
            });
            return mytempvar;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:670px;margin-left:auto;margin-right:auto;">
        <h2 style="text-align:center;">Bootstrap Pagination for ASP.NET GridView</h2>
        <p style="text-align:center;">Demo by Priya Darshini - Tutorial @ <a href="">Programmingfree</a></p> 
        <asp:GridView ID="gvBSPagination" runat="server" CssClass="table table-striped table-bordered table-condensed" Width="660px" AllowPaging="true" PageSize="5" OnPreRender="gvBSPagination_PreRender">
            <PagerTemplate>
                <div id="page-selection" class="pagination-centered"></div>
            </PagerTemplate>
        </asp:GridView>
        <div id="content"></div>    

    </div>
    </form>
</body>
</html>
