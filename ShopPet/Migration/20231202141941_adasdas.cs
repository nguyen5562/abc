using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopPet.Migrations
{
    /// <inheritdoc />
    public partial class adasdas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRoleGroups_ApplicationGroups_GroupId",
                table: "ApplicationRoleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRoleGroups_Roles_RoleId",
                table: "ApplicationRoleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroups_ApplicationGroups_GroupId",
                table: "AppUserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationRoleGroups",
                table: "ApplicationRoleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationGroups",
                table: "ApplicationGroups");

            migrationBuilder.RenameTable(
                name: "ApplicationRoleGroups",
                newName: "AppRoleGroups");

            migrationBuilder.RenameTable(
                name: "ApplicationGroups",
                newName: "AppGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationRoleGroups_GroupId",
                table: "AppRoleGroups",
                newName: "IX_AppRoleGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRoleGroups",
                table: "AppRoleGroups",
                columns: new[] { "RoleId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppGroups",
                table: "AppGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoleGroups_AppGroups_GroupId",
                table: "AppRoleGroups",
                column: "GroupId",
                principalTable: "AppGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoleGroups_Roles_RoleId",
                table: "AppRoleGroups",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroups_AppGroups_GroupId",
                table: "AppUserGroups",
                column: "GroupId",
                principalTable: "AppGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoleGroups_AppGroups_GroupId",
                table: "AppRoleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AppRoleGroups_Roles_RoleId",
                table: "AppRoleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroups_AppGroups_GroupId",
                table: "AppUserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoleGroups",
                table: "AppRoleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppGroups",
                table: "AppGroups");

            migrationBuilder.RenameTable(
                name: "AppRoleGroups",
                newName: "ApplicationRoleGroups");

            migrationBuilder.RenameTable(
                name: "AppGroups",
                newName: "ApplicationGroups");

            migrationBuilder.RenameIndex(
                name: "IX_AppRoleGroups_GroupId",
                table: "ApplicationRoleGroups",
                newName: "IX_ApplicationRoleGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationRoleGroups",
                table: "ApplicationRoleGroups",
                columns: new[] { "RoleId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationGroups",
                table: "ApplicationGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRoleGroups_ApplicationGroups_GroupId",
                table: "ApplicationRoleGroups",
                column: "GroupId",
                principalTable: "ApplicationGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRoleGroups_Roles_RoleId",
                table: "ApplicationRoleGroups",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroups_ApplicationGroups_GroupId",
                table: "AppUserGroups",
                column: "GroupId",
                principalTable: "ApplicationGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
