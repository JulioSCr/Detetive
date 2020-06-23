INSERT INTO ARMA (DS_ARMA, DS_CAMINHO_IMAGEM, IE_ATIVO) VALUES 
	  ('Caixa de giz', '~/Content/Imagens/CartasArmas/imgCartaCaixaDeGiz.svg', 1)
	, ('Monitor', '~/Content/Imagens/CartasArmas/imgCartaMonitor.svg', 1)
	, ('Teclado', '~/Content/Imagens/CartasArmas/imgCartaTeclado.svg', 1)
	, ('Projetor', '~/Content/Imagens/CartasArmas/imgCartaProjetor.svg', 1) 
	, ('Lista de exercícios', '~/Content/Imagens/CartasArmas/imgCartaListaDeExercicios.svg', 1) 
	, ('Pendrive', '~/Content/Imagens/CartasArmas/imgCartaPendrive.svg', 1)
	, ('Cadeira quebrada', '~/Content/Imagens/CartasArmas/imgCartaCadeiraQuebrada.svg', 1)
	, ('Pomba', '~/Content/Imagens/CartasArmas/imgCartaPomba.svg', 1)

INSERT INTO "LOCAL" (DS_LOCAL, DS_CAMINHO_IMAGEM, NR_LINHA_1, NR_COLUNA_1, NR_LINHA_2, NR_COLUNA_2, IE_ATIVO) VALUES 
	  ('Santhiago', '~/Content/Imagens/CartasLocais/imgCartaSanthiago.svg', 1, 1, 2, 2, 1)
	, ('Centro Acadêmico', '~/Content/Imagens/CartasLocais/imgCartaCentroAcademico.svg', 1, 1, 2, 2, 1)
	, ('Ginásio', '~/Content/Imagens/CartasLocais/imgCartaGinasio.svg', 1, 1, 2, 2, 1)
	, ('Prédio A', '~/Content/Imagens/CartasLocais/imgCartaPredioA.svg', 1, 1, 2, 2, 1)
	, ('Prédio B', '~/Content/Imagens/CartasLocais/imgCartaPredioB.svg', 1, 1, 2, 2, 1)
	, ('Praça', '~/Content/Imagens/CartasLocais/imgCartaPraca.svg', 1, 1, 2, 2, 1)
	, ('Etec', '~/Content/Imagens/CartasLocais/imgCartaEtec.svg', 1, 1, 2, 2, 1)
	, ('Cantina A/B', '~/Content/Imagens/CartasLocais/imgCartaCatinaAB.svg', 1, 1, 2, 2, 1)
	, ('Auditório', '~/Content/Imagens/CartasLocais/imgCartaAuditorio.svg', 1, 1, 2, 2, 1)

UPDATE "LOCAL" SET NR_LINHA_1 = 1, NR_COLUNA_1 = 7, NR_LINHA_2 = 11, NR_COLUNA_2 = 18 WHERE ID_LOCAL = 4
UPDATE "LOCAL" SET NR_LINHA_1 = 11, NR_COLUNA_1 = 9, NR_LINHA_2 = 18, NR_COLUNA_2 = 15 WHERE ID_LOCAL = 5
UPDATE "LOCAL" SET NR_LINHA_1 = 1, NR_COLUNA_1 = 27, NR_LINHA_2 = 10, NR_COLUNA_2 = 33 WHERE ID_LOCAL = 1
UPDATE "LOCAL" SET NR_LINHA_1 = 20, NR_COLUNA_1 = 16, NR_LINHA_2 = 26, NR_COLUNA_2 = 25 WHERE ID_LOCAL = 6
UPDATE "LOCAL" SET NR_LINHA_1 = 20, NR_COLUNA_1 = 9, NR_LINHA_2 = 26, NR_COLUNA_2 = 15 WHERE ID_LOCAL = 7
UPDATE "LOCAL" SET NR_LINHA_1 = 20, NR_COLUNA_1 = 1, NR_LINHA_2 = 26, NR_COLUNA_2 = 7 WHERE ID_LOCAL = 8
UPDATE "LOCAL" SET NR_LINHA_1 = 12, NR_COLUNA_1 = 27, NR_LINHA_2 = 26, NR_COLUNA_2 = 33 WHERE ID_LOCAL = 2
UPDATE "LOCAL" SET NR_LINHA_1 = 3, NR_COLUNA_1 = 18, NR_LINHA_2 = 9, NR_COLUNA_2 = 25 WHERE ID_LOCAL = 9
UPDATE "LOCAL" SET NR_LINHA_1 = 9, NR_COLUNA_1 = 18, NR_LINHA_2 = 18, NR_COLUNA_2 = 25 WHERE ID_LOCAL = 3

INSERT INTO SUSPEITO (ID_LOCAL, DS_SUSPEITO, DS_CAMINHO_IMAGEM, IE_ATIVO) VALUES 
	    (1, 'Reitor', '~/Content/Imagens/CartasSuspeitos/imgCartaReitor.svg', 1)
	  , (1, 'Diretora', '~/Content/Imagens/CartasSuspeitos/imgCartaDiretora.svg', 1)
	  , (1, 'Professora', '~/Content/Imagens/CartasSuspeitos/imgCartaProfessora.svg', 1)
	  , (1, 'Estudante', '~/Content/Imagens/CartasSuspeitos/imgCartaEstudante.svg', 1)
	  , (1, 'Zelador', '~/Content/Imagens/CartasSuspeitos/imgCartaZelador.svg', 1)
	  , (1, 'Policial', '~/Content/Imagens/CartasSuspeitos/imgCartaPolicial.svg', 1)
	  , (1, 'Repóter', '~/Content/Imagens/CartasSuspeitos/imgCartaReporter.svg', 1)
	  , (1, 'Bibliotecária','~/Content/Imagens/CartasSuspeitos/imgCartaBibliotecaria.svg', 1)

INSERT INTO LOCAL_PORTA (ID_LOCAL, NR_LINHA, NR_COLUNA, DS_DIRECAO, IE_ATIVO) VALUES
		(4, 14, 6, 'direita', 1)
	  , (5, 14, 9, 'esquerda', 1)
	  , (1, 9, 29, 'baixo', 1)
	  , (6, 25, 16, 'esquerda', 1)
	  , (6, 20, 23, 'cima', 1)
	  , (7, 24, 9, 'esquerda', 1)
	  , (7, 22, 14, 'direita', 1)
	  , (8, 20, 1, 'cima', 1)
	  , (2, 12, 31, 'cima', 1)
	  , (2, 24, 27, 'esquerda', 1)
	  , (9, 4, 18, 'esquerda', 1)
	  , (9, 3, 24, 'cima', 1)
	  , (3, 17, 18, 'esquerda', 1)