using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kendaraan.Models
{
    public class KendaraanModel
    {
        [Key] public string NoRegistrasi { get; set; } = null!;

        public int No { get; set; }

        [Required] public string? NamaPemilik { get; set; }

        [Required] public string? Alamat { get; set; }

        public string? MerkKendaraan { get; set; }

        public int? TahunPembuatan { get; set; }

        public int? Kapasitas { get; set; }

        public string? Warna { get; set; }

        public string? BahanBakar { get; set; }
    }
}
