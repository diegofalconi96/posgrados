<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actas_grado.aspx.cs" Inherits="POSG_V1.Pages.Actas_grado" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">ACTAS DE GRADO</h1>
    <br />
    <div class="input-group" id="barra-busqueda">
        <div class="form-outline">
            <asp:DropDownList ID="ddlPeriodoAcademico" runat="server" CssClass="form-control">
                <asp:ListItem Text="Selecciona el Período Académico" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-outline">
            <asp:DropDownList ID="ddlNombreMaestria" runat="server" CssClass="form-control">
                <asp:ListItem Text="Selecciona la maestría" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-outline">
            <asp:DropDownList ID="ddlPresentaDocumentacion" runat="server" CssClass="form-control">
                <asp:ListItem Text="Selecciona si presenta documentación" Value=""></asp:ListItem>
                <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                <asp:ListItem Text="No" Value="False"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-group-append">
            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClientClick="return validarFiltros();" OnClick="btnFiltrar_Click" />

        </div>
    </div>
    <br />
    <div class="table-responsive">
                        <asp:Button ID="btn_guardar_agencia" runat="server" Text="Guardar" CssClass="btn btn-success"  />
                <asp:Button ID="btn_editar_agencia" runat="server" Text="Editar" CssClass="btn btn-primary"  />
                <asp:Button ID="btn_eliminar_agencia" runat="server" Text="Eliminar" CssClass="btn btn-danger"  />
           
        <asp:GridView ID="tablaActa" runat="server" AutoGenerateColumns="false" CssClass="display nowrap dataTable dtr-inline custom-table">
            <Columns>
                <asp:BoundField DataField="strId_act" HeaderText="ID" />
                <asp:BoundField DataField="strNomb_per" HeaderText="Nombres" />
                <asp:BoundField DataField="strApellidop_per" HeaderText="Apellido Paterno" />
                <asp:BoundField DataField="strApellidom_per" HeaderText="Apellido Materno" />
                <asp:BoundField DataField="strNombreMaestria_ins" HeaderText="Nombre de Maestría" />
                <asp:BoundField DataField="strPeriodoAcademico_act" HeaderText="Periodo Académico" />
                <asp:TemplateField HeaderText="Presenta Documentación">
                

                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("bitPresentaDocumentacion_act")) ? "Sí" : "No" %>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:BoundField DataField=  HeaderText="acta 1" />
                <asp:BoundField DataField="" HeaderText="Acta 2" />



            </Columns>
        </asp:GridView>
    </div>
    <script>
    function validarFiltros() {
        var ddlPeriodoAcademico = document.getElementById('<%= ddlPeriodoAcademico.ClientID %>');
        var ddlNombreMaestria = document.getElementById('<%= ddlNombreMaestria.ClientID %>');

        if (ddlPeriodoAcademico.value === '' || ddlNombreMaestria.value === '') {
            alert('Por favor, selecciona un Período Académico y una Maestría');
            return false; 
        }

        return true; 
    }
</script>

</asp:Content>
