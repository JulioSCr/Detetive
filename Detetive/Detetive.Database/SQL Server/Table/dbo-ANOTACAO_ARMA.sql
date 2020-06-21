CREATE TABLE dbo.ANOTACAO_ARMA
(
  ID_ANOTACAO_ARMA INT IDENTITY(1,1) PRIMARY KEY,
  ID_JOGADOR_SALA  INT FOREIGN KEY REFERENCES JOGADOR_SALA(ID_JOGADOR_SALA) NOT NULL,
  ID_ARMA          INT FOREIGN KEY REFERENCES ARMA(ID_ARMA) NOT NULL,
  IE_ANOTADO       BIT DEFAULT 0,
  IE_ATIVO		   BIT DEFAULT 1
)