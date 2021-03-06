CREATE TABLE dbo.CRIME 
(
  ID_CRIME        INT IDENTITY(1,1) PRIMARY KEY,
  ID_SUSPEITO     INT FOREIGN KEY REFERENCES SUSPEITO(ID_SUSPEITO) NOT NULL,
  ID_ARMA         INT FOREIGN KEY REFERENCES ARMA(ID_ARMA) NOT NULL,
  ID_LOCAL        INT FOREIGN KEY REFERENCES LOCAL(ID_LOCAL) NOT NULL,
  ID_JOGADOR_SALA INT FOREIGN KEY REFERENCES JOGADOR_SALA(ID_JOGADOR_SALA) NULL,
  ID_SALA		  INT FOREIGN KEY REFERENCES SALA(ID_SALA) NOT NULL,
  IE_ATIVO        BIT DEFAULT 1
)