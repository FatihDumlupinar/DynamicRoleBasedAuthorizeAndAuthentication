using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicyRoles.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoleAuthorizeSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoleAuthorizes_AppStaticMenus_AppStaticMenuId",
                table: "AppRoleAuthorizes");

            migrationBuilder.DropIndex(
                name: "IX_AppRoleAuthorizes_AppStaticMenuId",
                table: "AppRoleAuthorizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppRoleAuthorizes_AppStaticMenuId",
                table: "AppRoleAuthorizes",
                column: "AppStaticMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoleAuthorizes_AppStaticMenus_AppStaticMenuId",
                table: "AppRoleAuthorizes",
                column: "AppStaticMenuId",
                principalTable: "AppStaticMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
