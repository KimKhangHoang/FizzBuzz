using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRuleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_GameRules_GameRuleId",
                table: "Rule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.RenameTable(
                name: "Rule",
                newName: "Rules");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_GameRuleId",
                table: "Rules",
                newName: "IX_Rules_GameRuleId");

            migrationBuilder.AlterColumn<int>(
                name: "GameRuleId",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rules",
                table: "Rules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_GameRules_GameRuleId",
                table: "Rules",
                column: "GameRuleId",
                principalTable: "GameRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_GameRules_GameRuleId",
                table: "Rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rules",
                table: "Rules");

            migrationBuilder.RenameTable(
                name: "Rules",
                newName: "Rule");

            migrationBuilder.RenameIndex(
                name: "IX_Rules_GameRuleId",
                table: "Rule",
                newName: "IX_Rule_GameRuleId");

            migrationBuilder.AlterColumn<int>(
                name: "GameRuleId",
                table: "Rule",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_GameRules_GameRuleId",
                table: "Rule",
                column: "GameRuleId",
                principalTable: "GameRules",
                principalColumn: "Id");
        }
    }
}
