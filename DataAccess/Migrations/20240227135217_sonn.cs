using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sonn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthRole");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Auths");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Auths",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Auths");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Auths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AuthRole",
                columns: table => new
                {
                    AuthsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRole", x => new { x.AuthsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AuthRole_Auths_AuthsId",
                        column: x => x.AuthsId,
                        principalTable: "Auths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthRole_RolesId",
                table: "AuthRole",
                column: "RolesId");
        }
    }
}
