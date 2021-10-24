using Microsoft.EntityFrameworkCore.Migrations;

namespace AShop_Data.Migrations
{
    public partial class UpdateOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
