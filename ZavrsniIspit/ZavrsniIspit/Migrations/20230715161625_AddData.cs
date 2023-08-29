using Microsoft.EntityFrameworkCore.Migrations;

namespace ZavrsniIspit.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ribarnice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    GodinaOtvaranja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ribarnice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ribe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sorta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MestoUlova = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    DostupnaKolicina = table.Column<int>(type: "int", nullable: false),
                    RibarnicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ribe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ribe_Ribarnice_RibarnicaId",
                        column: x => x.RibarnicaId,
                        principalTable: "Ribarnice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ribarnice",
                columns: new[] { "Id", "GodinaOtvaranja", "Naziv" },
                values: new object[] { 1, 2015, "Dunav doo" });

            migrationBuilder.InsertData(
                table: "Ribarnice",
                columns: new[] { "Id", "GodinaOtvaranja", "Naziv" },
                values: new object[] { 2, 2012, "Tisa str" });

            migrationBuilder.InsertData(
                table: "Ribarnice",
                columns: new[] { "Id", "GodinaOtvaranja", "Naziv" },
                values: new object[] { 3, 2015, "Sveza riba" });

            migrationBuilder.InsertData(
                table: "Ribe",
                columns: new[] { "Id", "Cena", "DostupnaKolicina", "MestoUlova", "RibarnicaId", "Sorta" },
                values: new object[,]
                {
                    { 2, 860.0, 30, "Dunav", 1, "Saran" },
                    { 5, 950.0, 15, "Dunav", 1, "Smudj" },
                    { 3, 1300.0, 10, "Tisa", 2, "Som" },
                    { 1, 1100.0, 20, "Ribnjak Bager", 3, "Smudj" },
                    { 4, 780.0, 12, "Ribnjak Ecka", 3, "Saran" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ribe_RibarnicaId",
                table: "Ribe",
                column: "RibarnicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ribe");

            migrationBuilder.DropTable(
                name: "Ribarnice");
        }
    }
}
