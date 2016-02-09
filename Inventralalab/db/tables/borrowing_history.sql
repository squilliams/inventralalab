CREATE TABLE IF NOT EXISTS `borrowing_history` (
  `id` varchar(64) DEFAULT NULL,
  `id_jenis` varchar(64) DEFAULT NULL,
  `id_inventaris` varchar(64) DEFAULT NULL,
  `tanggal_mulai` datetime DEFAULT NULL,
  `tanggal_selesai` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8