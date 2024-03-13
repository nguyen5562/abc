using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopPet.Migrations
{
    /// <inheritdoc />
    public partial class as2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "CliamValue",
                table: "Claims",
                newName: "ClaimName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimName",
                table: "Claims",
                newName: "CliamValue");

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
