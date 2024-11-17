using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_TechFix.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageDataAttributeFromSupplierProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "SupplierProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "SupplierProducts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
