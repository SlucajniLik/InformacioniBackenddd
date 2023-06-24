using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformacioniBackand.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Postava",
                table: "Igraci",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postava",
                table: "Igraci");
        }
    }
}
