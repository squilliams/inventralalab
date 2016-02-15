CREATE TABLE IF NOT EXISTS `borrowers` (
  `id` varchar(64) NOT NULL,
  `nama` varchar(256) NOT NULL,
  `no_telp` varchar(12) NOT NULL,
  `jenis` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;