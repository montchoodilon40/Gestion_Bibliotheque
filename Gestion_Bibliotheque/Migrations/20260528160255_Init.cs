using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Bibliotheque.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "livres",
                columns: table => new
                {
                    id_livres = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    auteur = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categorie = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantite = table.Column<int>(type: "int", nullable: false),
                    disponibilite = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livres", x => x.id_livres);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membres",
                columns: table => new
                {
                    id_membres = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom_membres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prenom_membres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_membres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tel_membres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    adresse_membres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membres", x => x.id_membres);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Abonnements",
                columns: table => new
                {
                    id_abonnement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_debutabonnement = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    type_abonnement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_finabonnement = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    id_membres = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnements", x => x.id_abonnement);
                    table.ForeignKey(
                        name: "FK_Abonnements_membres_id_membres",
                        column: x => x.id_membres,
                        principalTable: "membres",
                        principalColumn: "id_membres",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    id_emprunt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_livres = table.Column<int>(type: "int", nullable: false),
                    id_membres = table.Column<int>(type: "int", nullable: false),
                    date_emprunt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_retourprevue = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_retoureffective = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    statut = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.id_emprunt);
                    table.ForeignKey(
                        name: "FK_Emprunts_livres_id_livres",
                        column: x => x.id_livres,
                        principalTable: "livres",
                        principalColumn: "id_livres",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprunts_membres_id_membres",
                        column: x => x.id_membres,
                        principalTable: "membres",
                        principalColumn: "id_membres",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Penalites",
                columns: table => new
                {
                    id_penalite = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_penalite = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    raison_penalite = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    montant_penalite = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    id_emprunt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalites", x => x.id_penalite);
                    table.ForeignKey(
                        name: "FK_Penalites_Emprunts_id_emprunt",
                        column: x => x.id_emprunt,
                        principalTable: "Emprunts",
                        principalColumn: "id_emprunt",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Abonnements_id_membres",
                table: "Abonnements",
                column: "id_membres");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_id_livres",
                table: "Emprunts",
                column: "id_livres");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_id_membres",
                table: "Emprunts",
                column: "id_membres");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_id_emprunt",
                table: "Penalites",
                column: "id_emprunt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnements");

            migrationBuilder.DropTable(
                name: "Penalites");

            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "livres");

            migrationBuilder.DropTable(
                name: "membres");
        }
    }
}
