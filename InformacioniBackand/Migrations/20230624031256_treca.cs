using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformacioniBackand.Migrations
{
    public partial class treca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pozicija",
                table: "Igraci",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pozicija",
                table: "Igraci");
        }
    }
}
