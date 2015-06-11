<%@ Page Title="Bioscopen - Pathé" Language="C#" MasterPageFile="~/inc/Pathe_admin.master" AutoEventWireup="true" CodeBehind="cinemas.aspx.cs" Inherits="Pathe.admin.cinemas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="jumbotron">
        <h1>Bioscoopoverzicht</h1>
        <p><a href="/Admin/Cinemas/Add" class="floating-action"><i class="btn btn-success btn-fab btn-raised mdi-content-add"></i></a></p>
    </div>
    <div class="row">
        <div class="col-lg-3">
            <div class="well" runat="server">
            </div>
        </div>
        <div class="col-lg-9">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Cinemas">
                    <HeaderTemplate>
                        <table class="table table-striped table-hover ">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Naam</th>
                                    <th>Adres</th>
                                    <th>Acties</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                <ItemTemplate>
                    <tr id="table_row_cinema_<%# Eval("BioscoopID") %>">
                        <td><%# Eval("BioscoopID") %></td>
                        <td><%# Eval("Naam") %></td>
                        <td><%# Eval("Adres") %> &nbsp; <%# Eval("Stad") %></td>
                        <td>
                            <i class="mdi-editor-mode-edit" title="Bewerk bioscoop"></i> &nbsp;&nbsp;&nbsp;
                            <i class="mdi-editor-insert-invitation" title="Bewerk agenda"></i> &nbsp;&nbsp;&nbsp;
                            <i class="<%# RoomCountClass( Convert.ToInt32(Eval("BioscoopID"))) %>" title="Beheer zalen" data-toggle="modal" data-target="#roomsModal" onclick="loadRooms(<%# Eval("BioscoopID") %>)"></i> &nbsp;&nbsp;&nbsp;
                            <i class="mdi-content-remove-circle" title="Verwijder bioscoop" onclick="confirmDelete(<%# Eval("BioscoopID") %>, '<%# Eval("Naam") %>')"></i> &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
            </asp:Repeater>
            
        </div>
    </div>
    <asp:SqlDataSource ID="Cinemas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM BIOSCOOP b WHERE BIOSCOOPID <> 0 ORDER BY b.Naam"></asp:SqlDataSource>


    <!-- Rooms -->
    <div id="roomsModal" class="modal fade" role="dialog">
      <div class="modal-dialog">

        <!-- Modal content-->
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">&times;</button>
            <h4 class="modal-title">Zalen beheren</h4><br /><br/>
          </div>
          <div class="modal-body" id="roomModalBody">
            Loading..
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Sluit</button>
          </div>
        </div>

      </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceholder" runat="server">
    <script type="text/javascript">
        // Ask for confirmation before removing a cinema
        function confirmDelete(cinemaID, biosNaam) {
            sweetAlert({
                title: "Weet je het zeker?",
                text: "De bioscoop <strong>" + biosNaam + "</strong> wordt permanent verwijderd",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Ja, Verwijder hem!",
                closeOnConfirm: false,
                html: true
            }, function () {
                // User confirmed, make AJAX request to delete page
                xmlhttp = new XMLHttpRequest();
                xmlhttp.open("POST", "/admin/cinemaDelete.aspx", false);
                xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xmlhttp.send("cinemaID=" + cinemaID + "&confirm=1");
                if (xmlhttp.responseText == 'success') {
                    // Deleted successfully
                    swal({
                        title: "Verwijderd!",
                        text: "De bioscoop is verwijderd",
                        type: "success",
                        timer: 2000
                    });
                    deleteRow("table_row_cinema_" + cinemaID);
                }
                else {
                    // Something went wrong, show an error
                    swal({
                        title: "Oeps!",
                        text: "Er is iets fout gegaan tijdens het verwijderen van de bioscoop, probeer het aub nogmaals.",
                        type: "error",
                        html: true
                    });
                }
            });
        }
        
        // Delete row with given ID from table
        function deleteRow(rowid) {
            var row = document.getElementById(rowid);
            row.parentNode.removeChild(row);
        }
        
        function loadRooms(cinemaID) {
            xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", "/admin/doRoom.aspx", false);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xmlhttp.send("action=load&cinemaID=" + cinemaID);

            $("#roomModalBody").html(xmlhttp.responseText);
        }
        
        function AddRoom() {
            var number = $("#numNummer").val();
            var chairs = $("#numChairs").val();
            var cinemaID = $("#cinemaID").val();
            var imax = $("#chkImax").is(':checked');
            
            console.log(imax);
            
            xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", "/admin/doRoom.aspx", false);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xmlhttp.send("action=add&cinemaID=" + cinemaID + "&number=" + number + "&chairs=" + chairs + "&imax=" + imax);
            
            if (xmlhttp.responseText == 'success') {
                swal({
                    title: "Zaal toegevoegd!",
                    text: "De zaal is toegevoegd aan de bioscoop",
                    type: "success",
                    timer: 1500
                });
                
                loadRooms(cinemaID);
            }
            else {
                swal({
                    title: "Oeps!",
                    text: xmlhttp.responseText,
                    type: "error"
                });
            }
        }
        
        function GetRoomID() {
            var roomId = 0;
            $('#roomTable tr:hover').each(function () {
                roomId = $(this).find("td:first").html();
            });
            return roomId;
        }
        
        function LoadEditRoom() {
            var roomId = GetRoomID();

            xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", "/admin/doRoom.aspx", false);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xmlhttp.send("action=info&roomID=" + roomId);
            
            var returned = JSON.parse(xmlhttp.responseText);
            
            var Id = returned.Id;
            var nummer = returned.Number;
            var chairs = returned.ChairCount;
            var cinema = returned.CinemaID;
            var Imax = returned.Imax;
            
            $("#btnAddRoom").addClass("hidden");
            $("#btnEditRoom").removeClass("hidden");
            
            $("#numNummer").val(nummer);
            $("#numChairs").val(chairs);
            $("#cinemaID").val(cinema);
            $("#roomID").val(roomId);
            $("#chkImax").prop("checked", Imax);
        }
        
        function updateRoom() {
            var number = $("#numNummer").val();
            var chairs = $("#numChairs").val();
            var cinemaID = $("#cinemaID").val();
            var roomID = $("#roomID").val();
            var imax = $("#chkImax").is(':checked');


            xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", "/admin/doRoom.aspx", false);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xmlhttp.send("action=edit&cinemaID=" + cinemaID + "&number=" + number + "&chairs=" + chairs + "&imax=" + imax + "&roomID=" + roomID);

            if (xmlhttp.responseText == 'success') {
                swal({
                    title: "Zaal Aangepast!",
                    text: "De zaal is aangepast in het systeem",
                    type: "success",
                    timer: 1500
                });

                loadRooms(cinemaID);
            }
            else {
                swal({
                    title: "Oeps!",
                    text: xmlhttp.responseText,
                    type: "error",
                    html: true
                });
            }
        }
    
    </script>
</asp:Content>
