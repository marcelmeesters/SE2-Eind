<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loadRooms.aspx.cs" Inherits="Pathe.admin.loadRooms" %>

<hr/>
<h4>Zaal toevoegen</h4>
<form>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-group col-lg-4">
                <input class="form-control floating-label" id="numNummer" type="number" placeholder="Zaalnummer" data-hint="Het zaalnummer, dient uniek te zijn" required/>
                <br/>
            </div>
            <div class="form-group col-lg-4 col-lg-offset-1">
                <input class="form-control floating-label" id="numChairs" type="number" placeholder="Aantal stoelen" data-hint="Hoeveel stoelen zijn er in deze zaal?" required/>
                <br/>
            </div>
            <div class="form-group col-lg-2 col-lg-offset-1">
                <label>
                    <input type="checkbox" id="chkImax" /> IMAX
                </label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <center>
                <span class="btn btn-success btn-lg" onclick="AddRoom()">Zaal toevoegen</span>
            </center>
        </div>
    </div>
</form>