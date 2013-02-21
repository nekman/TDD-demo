<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SogetiDemo.TDD.BankApp.Web.Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <title>Sogeti Jönköping - TDD Demo</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-responsive.min.css" rel="stylesheet" />

    <!-- Le styles -->
    <style>
        body {
            padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
        }
    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="../assets/js/html5shiv.js"></script>
    <![endif]-->

</head>

<body>

    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="brand" href="#">BankApp - Sogeti TDD Demo</a>
                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li class="active"><a href="#">Bank accounts</a></li>
                        <li><a href="#about">About</a></li>
                        <li><a href="#logout">Logout</a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>

    <div class="container">

        <h1>Accounts</h1>
        
        <form id="form1" runat="server">
            
            <div id="error" class="alert alert-error" style="display: none;">
                <b>Error</b> Could not change the account balance
            </div>
            
            <table class="table table-condensed">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Balance</th>
                        <th>Withdraw / Deposit</th>
                        <th></th>
                    </tr>
                </thead>
                <asp:Repeater ID="BankAccountRepeater" ItemType="SogetiDemo.TDD.BankApp.Common.Entities.BankAccount" runat="server">
                    <ItemTemplate>
                        <tr id="<%# Item.Id %>">
                            <td>
                                <%# Item.Id %>
                            </td>
                            <td>
                                <%# Item.Name %>
                            </td>
                            <td class="balance">
                                <%# Item.Balance %>
                            </td>
                            <td>
                                <asp:TextBox placeholder="Change..." style="width:60px;" runat="server"/>
                            </td>
                             <td>
                                <button class="btn btn-primary">Apply</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </form>

    </div>
    <!-- /container -->

    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        // Ugly. This not how I normally would do...
        
        (function($) {
            $('.btn.btn-primary').on('click', function (e) {
                e.preventDefault();

                var $this = $(this),
                    $parent = $this.parent().parent(), //tr
                    $input = $parent.find('input[type=text]').first(),
                    $balance = $parent.find('td.balance').first(), //td.balance
                    
                    id = $parent.attr('id'),
                    amount = $input.val();
                
                $.ajax({
                    url: '/Handlers/BankAccountHandler.ashx',
                    data: JSON.stringify({
                        id: id,
                        amount: amount || 0 
                    }),
                    type: 'post',
                    dataType: 'json'
                })
                .done(function (data) {
                    if (data.HasError) {
                        $('#error').fadeIn(1000, function() {
                            $(this).fadeOut(4000);
                        });

                        return;
                    }

                    $balance.text(data.Balance);
                    $parent.hide().fadeIn();
                })
                .error(console.log);
            });
        }(window.jQuery)) 
       
    </script>
</body>
</html>
