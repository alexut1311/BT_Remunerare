using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTRemunerare.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedPeriodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "Month",
                table: "SalesRemunerationRules");

            _ = migrationBuilder.DropColumn(
                name: "Month",
                table: "Sales");

            _ = migrationBuilder.RenameColumn(
                name: "Year",
                table: "SalesRemunerationRules",
                newName: "PeriodId");

            _ = migrationBuilder.RenameColumn(
                name: "Year",
                table: "Sales",
                newName: "PeriodId");

            _ = migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    PeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Periods", x => x.PeriodId);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_SalesRemunerationRules_PeriodId",
                table: "SalesRemunerationRules",
                column: "PeriodId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Sales_PeriodId",
                table: "Sales",
                column: "PeriodId");

            _ = migrationBuilder.AddForeignKey(
                name: "FK_Sales_Periods_PeriodId",
                table: "Sales",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);

            _ = migrationBuilder.AddForeignKey(
                name: "FK_SalesRemunerationRules_Periods_PeriodId",
                table: "SalesRemunerationRules",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "FK_Sales_Periods_PeriodId",
                table: "Sales");

            _ = migrationBuilder.DropForeignKey(
                name: "FK_SalesRemunerationRules_Periods_PeriodId",
                table: "SalesRemunerationRules");

            _ = migrationBuilder.DropTable(
                name: "Periods");

            _ = migrationBuilder.DropIndex(
                name: "IX_SalesRemunerationRules_PeriodId",
                table: "SalesRemunerationRules");

            _ = migrationBuilder.DropIndex(
                name: "IX_Sales_PeriodId",
                table: "Sales");

            _ = migrationBuilder.RenameColumn(
                name: "PeriodId",
                table: "SalesRemunerationRules",
                newName: "Year");

            _ = migrationBuilder.RenameColumn(
                name: "PeriodId",
                table: "Sales",
                newName: "Year");

            _ = migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "SalesRemunerationRules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
