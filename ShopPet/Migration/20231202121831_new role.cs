using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopPet.Migrations
{
    /// <inheritdoc />
    public partial class newrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccessAdmin",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AccessUser",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddAccount",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddCategory",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddGroup",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddProduct",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AddSupplier",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteAccount",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteCategory",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteGroup",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteProduct",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteSupplier",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditAccount",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditCategory",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditGroup",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditOrder",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditProduct",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditSupplier",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewAccount",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewCategory",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewGroup",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewOrder",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewProduct",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewSupplier",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessAdmin",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AccessUser",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddCategory",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddGroup",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddProduct",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AddSupplier",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteCategory",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteGroup",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteProduct",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteSupplier",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditCategory",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditGroup",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditOrder",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditProduct",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EditSupplier",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewCategory",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewGroup",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewOrder",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewProduct",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewSupplier",
                table: "Roles");
        }
    }
}
