using Microsoft.EntityFrameworkCore.Migrations;

namespace AShop_Data.Migrations
{
    public partial class UpdateOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "OrderHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_CreatedByUserId",
                table: "OrderHeader",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_CreatedByUserId",
                table: "OrderHeader",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_CreatedByUserId",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_CreatedByUserId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "OrderHeader");
        }
    }
}
