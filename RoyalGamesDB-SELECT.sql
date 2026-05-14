USE RoyalGames
GO

-- ==================================================== --
-- 1. CONSULTAS DE CADASTROS BÁSICOS (TABELAS DE APOIO)
-- ==================================================== --
PRINT '--- USUÁRIOS ---'
SELECT * FROM Usuario

PRINT '--- CLASSIFICAÇÕES ---'
SELECT * FROM Classificacao

PRINT '--- GÊNEROS ---'
SELECT * FROM Genero

PRINT '--- PLATAFORMAS ---'
SELECT * FROM Plataforma

-- ================================================== --
-- 2. CONSULTA DE JOGOS (COM JOIN PARA CLASSIFICAÇÃO)
-- ================================================== --
PRINT '--- LISTAGEM DE JOGOS E SEUS CRIADORES ---'
SELECT 
    J.JogoID, 
    J.Nome AS Jogo, 
    J.Preco, 
    C.Nome AS Classificacao,
    U.Nome AS Criado_Por,
    CASE WHEN J.StatusJogo = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status
FROM Jogo J
INNER JOIN Classificacao C ON J.FK_ClassificacaoID = C.ClassificacaoID
INNER JOIN Usuario U ON J.FK_UsuarioID = U.UsuarioID;

-- ======================================================== --
-- 3. CONSULTAS DE RELACIONAMENTOS (TABELAS INTERMEDIÁRIAS)
-- ======================================================== --
PRINT '--- JOGOS POR PLATAFORMA ---';
SELECT 
    J.Nome AS Jogo, 
    P.Nome AS Plataforma
FROM JogoPlataforma JP
INNER JOIN Jogo J ON JP.JogoID = J.JogoID
INNER JOIN Plataforma P ON JP.PlataformaID = P.PlataformaID;

PRINT '--- JOGOS POR GÊNERO ---';
SELECT 
    J.Nome AS Jogo, 
    G.Nome AS Genero
FROM JogoGenero JG
INNER JOIN Jogo J ON JG.JogoID = J.JogoID
INNER JOIN Genero G ON JG.GeneroID = G.GeneroID;

-- =================================== --
-- 4. RELATÓRIO CONSOLIDADO (CORRIGIDO)
-- =================================== --
PRINT '--- RELATÓRIO GERAL DE JOGOS ---';
SELECT 
    J.Nome AS Jogo,
    J.Preco,
    -- Subquery para buscar as plataformas sem duplicar
    (SELECT STRING_AGG(P.Nome, ', ') 
     FROM JogoPlataforma JP 
     JOIN Plataforma P ON JP.PlataformaID = P.PlataformaID 
     WHERE JP.JogoID = J.JogoID) AS Plataformas,
    -- Subquery para buscar os gêneros sem duplicar
    (SELECT STRING_AGG(G.Nome, ', ') 
     FROM JogoGenero JG 
     JOIN Genero G ON JG.GeneroID = G.GeneroID 
     WHERE JG.JogoID = J.JogoID) AS Generos
FROM Jogo J;

-- =================== --
-- 5. AUDITORIA E LOGS
-- =================== --
PRINT '--- LOG DE ALTERAÇÕES DE JOGOS ---';
SELECT * FROM Log_AlteracaoJogo ORDER BY DataAlteracao DESC;