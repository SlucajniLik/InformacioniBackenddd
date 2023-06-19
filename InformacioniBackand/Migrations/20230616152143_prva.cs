using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformacioniBackand.Migrations
{
    public partial class prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LozinkaKljuc = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTima = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menazder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LozinkaKljuc = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menazder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaKlupi",
                columns: table => new
                {
                    IdIgraca = table.Column<int>(type: "int", nullable: false),
                    IdUtakmice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Navijac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LozinkaKljuc = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusReg = table.Column<bool>(type: "bit", nullable: true),
                    IdTima = table.Column<int>(type: "int", nullable: true),
                    BrojClanskeKarte = table.Column<int>(type: "int", nullable: true),
                    DatumIstekaRoka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navijac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rezultati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sezona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrOdigranihSusreta = table.Column<int>(type: "int", nullable: false),
                    BrPobeda = table.Column<int>(type: "int", nullable: false),
                    BrNeresenih = table.Column<int>(type: "int", nullable: false),
                    BrIzgubljenih = table.Column<int>(type: "int", nullable: false),
                    BrBodova = table.Column<int>(type: "int", nullable: false),
                    IdTima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezultati", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOsnivanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMenadzera = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uplata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suma = table.Column<int>(type: "int", nullable: false),
                    IdNavijaca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UprvojPostavi",
                columns: table => new
                {
                    IdIgraca = table.Column<int>(type: "int", nullable: false),
                    IdUtakmice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Utakmica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vreme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rezultat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrZutihKartona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrCrvenihKartona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTima1 = table.Column<int>(type: "int", nullable: true),
                    IdTima2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZahteviZaProduzenjeClastva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZahteva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusZahteva = table.Column<int>(type: "int", nullable: false),
                    IdNavijaca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahteviZaProduzenjeClastva", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Menazder");

            migrationBuilder.DropTable(
                name: "NaKlupi");

            migrationBuilder.DropTable(
                name: "Navijac");

            migrationBuilder.DropTable(
                name: "Rezultati");

            migrationBuilder.DropTable(
                name: "Tim");

            migrationBuilder.DropTable(
                name: "Uplata");

            migrationBuilder.DropTable(
                name: "UprvojPostavi");

            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.DropTable(
                name: "ZahteviZaProduzenjeClastva");
        }
    }
}
