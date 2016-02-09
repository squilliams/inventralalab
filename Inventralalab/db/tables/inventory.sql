CREATE TABLE IF NOT EXISTS `inventory` (
  `id` varchar(64) DEFAULT NULL,
  `id_jenis` varchar(64) DEFAULT NULL,
  `id_peminjam` varchar(64) NOT NULL,
  `tanggal_mulai` datetime NOT NULL,
  `tanggal_selesai` datetime NOT NULL,
  `kondisi_alat` tinyint(1) DEFAULT NULL,
  `lokasi` varchar(64) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8