﻿CREATE TABLE IF NOT EXISTS `inventory` (
  `id` varchar(64) NOT NULL,
  `id_jenis` varchar(64) NOT NULL,
  `id_peminjam` varchar(64) DEFAULT NULL,
  `tanggal_mulai` date DEFAULT NULL,
  `tanggal_selesai` date DEFAULT NULL,
  `kondisi_alat` tinyint(1) NOT NULL,
  `lokasi` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8