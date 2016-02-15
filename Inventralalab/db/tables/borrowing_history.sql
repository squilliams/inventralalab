CREATE TABLE IF NOT EXISTS `borrowing_history` (
  `id` varchar(64) NOT NULL,
  `id_jenis` varchar(64) NOT NULL,
  `id_inventaris` varchar(64) NOT NULL,
  `tanggal_mulai` datetime NOT NULL,
  `tanggal_selesai` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8