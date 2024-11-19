using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kendaraan.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialKendaraan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kendaraans",
                columns: table => new
                {
                    NoRegistrasi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    NamaPemilik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerkKendaraan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TahunPembuatan = table.Column<int>(type: "int", nullable: true),
                    Kapasitas = table.Column<int>(type: "int", nullable: true),
                    Warna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BahanBakar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kendaraans", x => x.NoRegistrasi);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kendaraans");
        }
    }
}
