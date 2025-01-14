using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sacmy.Server.Migrations
{
    /// <inheritdoc />
    public partial class addIsPdfGeneratedToOrderTrackingInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPdfGenerated",
                table: "OrderTrackingInvoice",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPdfGenerated",
                table: "OrderTrackingInvoice");
        }
    }
}
