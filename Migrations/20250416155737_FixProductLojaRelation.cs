using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductControl.Migrations
{
    public partial class FixProductLojaRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Lojas_LojaIdLoja",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_LojaIdLoja",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LojaIdLoja",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdLoja",
                table: "Products",
                column: "IdLoja");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Lojas_IdLoja",
                table: "Products",
                column: "IdLoja",
                principalTable: "Lojas",
                principalColumn: "IdLoja",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Lojas_IdLoja",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IdLoja",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "LojaIdLoja",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_LojaIdLoja",
                table: "Products",
                column: "LojaIdLoja");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Lojas_LojaIdLoja",
                table: "Products",
                column: "LojaIdLoja",
                principalTable: "Lojas",
                principalColumn: "IdLoja",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
