using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iştakip.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birims",
                columns: table => new
                {
                    BirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birims", x => x.BirimId);
                });

            migrationBuilder.CreateTable(
                name: "Durums",
                columns: table => new
                {
                    DurumId1 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurumAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durums", x => x.DurumId1);
                });

            migrationBuilder.CreateTable(
                name: "YetkiTurs",
                columns: table => new
                {
                    YetkiTurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiTurAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YetkiTurs", x => x.YetkiTurId);
                });

            migrationBuilder.CreateTable(
                name: "Personels",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelKullanıcıAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelBirimId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirimId = table.Column<int>(type: "int", nullable: true),
                    PersonelYetkiTurId = table.Column<int>(type: "int", nullable: true),
                    YetkiTurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personels", x => x.PersonelId);
                    table.ForeignKey(
                        name: "FK_Personels_Birims_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birims",
                        principalColumn: "BirimId");
                    table.ForeignKey(
                        name: "FK_Personels_YetkiTurs_YetkiTurId",
                        column: x => x.YetkiTurId,
                        principalTable: "YetkiTurs",
                        principalColumn: "YetkiTurId");
                });

            migrationBuilder.CreateTable(
                name: "Iss",
                columns: table => new
                {
                    IsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsBaslık = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAcıklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPersonelId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    PersonelBirimId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDurumId = table.Column<int>(type: "int", nullable: true),
                    DurumId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iss", x => x.IsId);
                    table.ForeignKey(
                        name: "FK_Iss_Durums_DurumId1",
                        column: x => x.DurumId1,
                        principalTable: "Durums",
                        principalColumn: "DurumId1");
                    table.ForeignKey(
                        name: "FK_Iss_Personels_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personels",
                        principalColumn: "PersonelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iss_DurumId1",
                table: "Iss",
                column: "DurumId1");

            migrationBuilder.CreateIndex(
                name: "IX_Iss_PersonelId",
                table: "Iss",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_BirimId",
                table: "Personels",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_YetkiTurId",
                table: "Personels",
                column: "YetkiTurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iss");

            migrationBuilder.DropTable(
                name: "Durums");

            migrationBuilder.DropTable(
                name: "Personels");

            migrationBuilder.DropTable(
                name: "Birims");

            migrationBuilder.DropTable(
                name: "YetkiTurs");
        }
    }
}
