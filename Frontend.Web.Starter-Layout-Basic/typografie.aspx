<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="typografie.aspx.cs" Inherits="Frontend.Web.Starter_Layout_Basic.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="sidebar">
        <ul>
            <li>verschiedene Beispiele</li>
        </ul>
    </div>
    <div class="content">
        <h1>Schriftgrösse dem dargestellten Text anpassen</h1>
            <p class="post">Rather than do this with a hard-coded number in each of the quotes, I did it with JavaScript. This allows for easier adjustments down the road and nicely keeps content and presentation separate.</p>
            <p class="post-1">Quotes on Design is using the jQuery version. MooTools example here (note some slight changes</p><<<<
            <p class="post-2">Set an object of the paragraph you are targetting</p>
            <p class="post">Rather than do this with a hard-coded number in each of the quotes, I did it with JavaScript. This allows for easier adjustments down the road and nicely keeps content and presentation separate.</p>
    </div>
    
    <!-- via http://css-tricks.com/set-font-size-based-on-word-count/ -->
    <script type="text/javascript">
        $(document).ready(function() {
            $(function() {

                var $quote = $(".post");

                var $numWords = $quote.text().split(" ").length;

                if (($numWords >= 1) && ($numWords < 10)) {
                    $quote.css("font-size", "36px");
                }
                else if (($numWords >= 10) && ($numWords < 20)) {
                    $quote.css("font-size", "32px");
                }
                else if (($numWords >= 20) && ($numWords < 30)) {
                    $quote.css("font-size", "28px");
                }
                else if (($numWords >= 30) && ($numWords < 40)) {
                    $quote.css("font-size", "24px");
                }
                else {
                    $quote.css("font-size", "20px");
                }

            });
            $(function() {

                var $quote = $(".post-1");

                var $numWords = $quote.text().split(" ").length;

                if (($numWords >= 1) && ($numWords < 10)) {
                    $quote.css("font-size", "36px");
                }
                else if (($numWords >= 10) && ($numWords < 20)) {
                    $quote.css("font-size", "32px");
                }
                else if (($numWords >= 20) && ($numWords < 30)) {
                    $quote.css("font-size", "28px");
                }
                else if (($numWords >= 30) && ($numWords < 40)) {
                    $quote.css("font-size", "24px");
                }
                else {
                    $quote.css("font-size", "20px");
                }

            });
            $(function() {

                var $quote = $(".post-2");

                var $numWords = $quote.text().split(" ").length;

                if (($numWords >= 1) && ($numWords < 10)) {
                    $quote.css("font-size", "36px");
                }
                else if (($numWords >= 10) && ($numWords < 20)) {
                    $quote.css("font-size", "32px");
                }
                else if (($numWords >= 20) && ($numWords < 30)) {
                    $quote.css("font-size", "28px");
                }
                else if (($numWords >= 30) && ($numWords < 40)) {
                    $quote.css("font-size", "24px");
                }
                else {
                    $quote.css("font-size", "20px");
                }

            });
        });
    </script>

</asp:Content>
