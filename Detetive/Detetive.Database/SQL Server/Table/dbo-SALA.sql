CREATE TABLE dbo.SALA
(
  ID_SALA         INT IDENTITY(1000,1) PRIMARY KEY,
  DT_CRIACAO      DATETIME NOT NULL,
  IE_ATIVO        BIT DEFAULT 1
)