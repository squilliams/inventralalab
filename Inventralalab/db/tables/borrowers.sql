CREATE TABLE IF NOT EXISTS `borrowers` (
  `id` varchar(64) DEFAULT NULL,
  `nama` varchar(256) DEFAULT NULL,
  `no_telp` varchar(12) DEFAULT NULL,
  `jenis` varchar(32) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8