using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventralalab.core {
    public class Alat {
        public Alat() { }
        public Alat(string id, string id_jenis, string nama, bool kondisi_alat, string lokasi) {
            Id = id;
            IdJenis = id_jenis;
            Nama = nama;
            KondisiAlat = kondisi_alat;
            Lokasi = lokasi;
        }
        public Alat(string id, string nama, DateTime? tanggal_mulai, DateTime? tanggal_selesai, bool kondisi_alat, string lokasi) {
            Id = id;
            Nama = nama;
            TanggalMulai = tanggal_mulai;
            TanggalSelesai = tanggal_selesai;
            KondisiAlat = kondisi_alat;
            Lokasi = lokasi;
        }
        public string Id { get; set; }
        public string IdJenis { get; set; }
        public string Nama { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalSelesai { get; set; }
        public bool KondisiAlat { get; set; }
        public string KondisiString {
            get {
                return (KondisiAlat) ? "Baik" : "Rusak";
            }
            set {

            }
        }
        public string Lokasi { get; set; }
    }
}
