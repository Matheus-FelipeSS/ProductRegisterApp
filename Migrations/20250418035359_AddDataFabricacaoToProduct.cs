using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductControl.Migrations
{
    public partial class AddDataFabricacaoToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataEntrega",
                table: "Products",
                newName: "DataFabricacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataFabricacao",
                table: "Products",
                newName: "DataEntrega");
        }
    }
}
