<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="SeriesGenerator._Default" %>
<!DOCTYPE html>
<html>
    <head>
        <title>Series generator</title>
        <link href="Styles/bootstrap/bootstrap.min.css" rel="stylesheet">
        <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            window.onload = function () {
                document.getElementById("results").style.display = "none";
                document.getElementById("countOfElementsTb").focus();
            }
        </script>       
        <script type="text/javascript">
            function makeCall(url, sequenceType, countOfItems) {
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8;",
                    async: true,
                    cache: false,
                    data: "{sequenceTypeValue:'" + sequenceType + "',countOfItemsValue:'" + countOfItems + "'}",
                    success: function (msg) {
                        var id = document.getElementById("sequenceId");
                        id.innerHTML = msg.d.SequenceId;
                        var type = document.getElementById("sequenceType");
                        type.innerHTML = msg.d.SequenceType;
                        var countOfElements = document.getElementById("countOfElements");
                        countOfElements.innerHTML = msg.d.CountOfElements;
                        var max = document.getElementById("maxElement");
                        max.innerHTML = msg.d.MaxElement;
                        var average = document.getElementById("averageValue");
                        average.innerHTML = msg.d.AverageElement;
                        document.getElementById("results").style.display = "block";
                    },
                    error: function (exc) {
                        alert(exc.responseText);
                    }
                });
            }
            $(function () {
                $("#generateBtn").click(function () {
                    document.getElementById("results").style.display = "none";
                    var val = $("#<%: countOfElementsTb.ClientID  %>").val();
                    if (val == null || val.trim() == "") {
                        document.getElementById('errorLabel').innerHTML = '* Please fill the field';
                    } else if (isNaN(val) || parseInt(val) < 1) {
                        document.getElementById('errorLabel').innerHTML = "* Value must be a number greater than 0";
                    }
                    else {
                        document.getElementById('errorLabel').innerHTML = "";
                        makeCall(
                            "Default.aspx/GenerateSequence",
                            $("#<%: sequenceTypeDrop.ClientID  %>").val(),
                            $("#<%: countOfElementsTb.ClientID  %>").val());   
                    }
                });
            });
        </script>
    </head>
    <body>
        <div class="container">
            <form id="Form1" class="form-horizontal" runat="server">
                <fieldset id="params">
                    <legend>Parameters</legend>
                    <div class="form-group">
                        <label for="sequenceTypeDrop" class="col-sm-offset-3 col-sm-2 control-label">Series type:</label>
                        <div class="col-sm-4">
                            <asp:DropDownList id="sequenceTypeDrop" CssClass="form-control" runat="server">
                                <asp:ListItem>Fibonacci</asp:ListItem>
                                <asp:ListItem>Bell</asp:ListItem>
                                <asp:ListItem>Catalan</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="countOfElementsTb" class="col-sm-offset-3 col-sm-2 control-label">Count of elements:</label>
                        <div class="col-sm-4">
                            <asp:TextBox id="countOfElementsTb" runat="server" CssClass="form-control"/>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label for="countOfElementsTb" ID="errorLabel" runat="server" ForeColor="Red"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-5 col-sm-4">
                            <input type="button" 
                                id="generateBtn"
                                runat="server"
                                class="btn btn-primary btn-lg pull-right"
                                value="Generate"/>
                        </div>
                    </div>
                </fieldset>
                 <fieldset id="results">
                    <legend>Results</legend>
                        <table id="generatedSequenceProperties" class="table table-curved">
                            <thead>
                              <tr>
                                <th>Sequence Id</th>
                                <th>Sequence type</th>
                                <th>Count of elements</th>
                                <th>Max element</th>
                                <th>Average value</th>
                              </tr>
                            </thead>
                            <tbody>
                              <tr>
                                  <td id="sequenceId"/>
                                  <td id="sequenceType"/>
                                  <td id="countOfElements"/>
                                  <td id="maxElement"/>
                                  <td id="averageValue"/>
                              </tr>
                            </tbody>
                        </table> 
                </fieldset>
            </form>
        </div>   
    </body>
</html>