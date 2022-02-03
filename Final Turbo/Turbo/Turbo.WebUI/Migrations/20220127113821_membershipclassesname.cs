using Microsoft.EntityFrameworkCore.Migrations;

namespace Turbo.WebUI.Migrations
{
    public partial class membershipclassesname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "TurboUserTokens",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "TurboUsers",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "TurboUserRoles",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "TurboUserLogins",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "TurboUserClaims",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "TurboRoles",
                newSchema: "Membership");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "TurboRoleClaims",
                newSchema: "Membership");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Membership",
                table: "TurboUserRoles",
                newName: "IX_TurboUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Membership",
                table: "TurboUserLogins",
                newName: "IX_TurboUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Membership",
                table: "TurboUserClaims",
                newName: "IX_TurboUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Membership",
                table: "TurboRoleClaims",
                newName: "IX_TurboRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboUserTokens",
                schema: "Membership",
                table: "TurboUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboUsers",
                schema: "Membership",
                table: "TurboUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboUserRoles",
                schema: "Membership",
                table: "TurboUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboUserLogins",
                schema: "Membership",
                table: "TurboUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboUserClaims",
                schema: "Membership",
                table: "TurboUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboRoles",
                schema: "Membership",
                table: "TurboRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurboRoleClaims",
                schema: "Membership",
                table: "TurboRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurboRoleClaims_TurboRoles_RoleId",
                schema: "Membership",
                table: "TurboRoleClaims",
                column: "RoleId",
                principalSchema: "Membership",
                principalTable: "TurboRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurboUserClaims_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserClaims",
                column: "UserId",
                principalSchema: "Membership",
                principalTable: "TurboUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurboUserLogins_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserLogins",
                column: "UserId",
                principalSchema: "Membership",
                principalTable: "TurboUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurboUserRoles_TurboRoles_RoleId",
                schema: "Membership",
                table: "TurboUserRoles",
                column: "RoleId",
                principalSchema: "Membership",
                principalTable: "TurboRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurboUserRoles_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserRoles",
                column: "UserId",
                principalSchema: "Membership",
                principalTable: "TurboUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurboUserTokens_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserTokens",
                column: "UserId",
                principalSchema: "Membership",
                principalTable: "TurboUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurboRoleClaims_TurboRoles_RoleId",
                schema: "Membership",
                table: "TurboRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TurboUserClaims_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TurboUserLogins_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_TurboUserRoles_TurboRoles_RoleId",
                schema: "Membership",
                table: "TurboUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TurboUserRoles_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TurboUserTokens_TurboUsers_UserId",
                schema: "Membership",
                table: "TurboUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboUserTokens",
                schema: "Membership",
                table: "TurboUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboUsers",
                schema: "Membership",
                table: "TurboUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboUserRoles",
                schema: "Membership",
                table: "TurboUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboUserLogins",
                schema: "Membership",
                table: "TurboUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboUserClaims",
                schema: "Membership",
                table: "TurboUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboRoles",
                schema: "Membership",
                table: "TurboRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurboRoleClaims",
                schema: "Membership",
                table: "TurboRoleClaims");

            migrationBuilder.RenameTable(
                name: "TurboUserTokens",
                schema: "Membership",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "TurboUsers",
                schema: "Membership",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TurboUserRoles",
                schema: "Membership",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "TurboUserLogins",
                schema: "Membership",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "TurboUserClaims",
                schema: "Membership",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "TurboRoles",
                schema: "Membership",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "TurboRoleClaims",
                schema: "Membership",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_TurboUserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TurboUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TurboUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TurboRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
