/* CTRL + K > CTRL + C = Comenta */
/* CTRL + K > CTRL + U = Tira o comentário */
CREATE DATABASE RoyalGames
GO

USE RoyalGames
GO

CREATE TABLE Usuario (
	UsuarioID			INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(60) NOT NULL,
	Email				VARCHAR(150) UNIQUE NOT NULL,
	Senha				VARBINARY(32) NOT NULL,
	StatusUsuario		BIT DEFAULT 1 NOT NULL
)
GO

CREATE TABLE Classificacao (
	ClassificacaoID		INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(50)
)
GO

CREATE TABLE Jogo (
	JogoID				INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(150) UNIQUE NOT NULL,
	Preco				DECIMAL(10,2) NOT NULL,
	Imagem				VARBINARY(MAX) NOT NULL,
	Descricao			NVARCHAR(MAX) NOT NULL,
	StatusJogo			BIT DEFAULT 1 NOT NULL,
	FK_UsuarioID		INT FOREIGN KEY REFERENCES Usuario(UsuarioID),
	FK_ClassificacaoID	INT FOREIGN KEY REFERENCES Classificacao(ClassificacaoID)
)
GO

CREATE TABLE Plataforma (
	PlataformaID		INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(50)
)
GO

CREATE TABLE Genero (
	GeneroID			INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(50)
)
GO

CREATE TABLE JogoPlataforma (
		JogoID			INT NOT NULL,
		PlataformaID	INT NOT NULL,

		CONSTRAINT PK_JogoPlataforma
			PRIMARY KEY (JogoID, PlataformaID),

		CONSTRAINT FK_JogoPlataforma_Jogo
			FOREIGN KEY (JogoID)
				REFERENCES Jogo(JogoID) ON DELETE CASCADE,

		CONSTRAINT FK_JogoPlataforma_Plataforma
			FOREIGN KEY (PlataformaID)
				REFERENCES Plataforma(PlataformaID) ON DELETE CASCADE,
)
GO

CREATE TABLE JogoGenero (
		JogoID			INT NOT NULL,
		GeneroID	INT NOT NULL,

		CONSTRAINT PK_JogoGenero
			PRIMARY KEY (JogoID, GeneroID),

		CONSTRAINT FK_JogoGenero_Jogo
			FOREIGN KEY (JogoID)
				REFERENCES Jogo(JogoID) ON DELETE CASCADE,

		CONSTRAINT FK_JogoGenero_Genero
			FOREIGN KEY (GeneroID)
				REFERENCES Genero(GeneroID) ON DELETE CASCADE,
)
GO

CREATE TABLE Log_AlteracaoJogo (
	Log_AlteracaoJogoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME NOT NULL,
	NomeAnterior VARCHAR(100),
	PrecoAnterior DECIMAL(10,2),
	FK_JogoID INT FOREIGN KEY REFERENCES Jogo(JogoID)
)
GO

CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE
AS
BEGIN
	UPDATE us SET StatusUsuario = 0
	FROM Usuario us
	INNER JOIN deleted d
		ON d.UsuarioID = us.UsuarioID
END
GO

CREATE TRIGGER trg_AlteracaoJogo
ON Jogo
AFTER UPDATE
AS
BEGIN
	INSERT INTO Log_AlteracaoJogo
	(DataAlteracao, FK_JogoID, NomeAnterior, PrecoAnterior)
	SELECT GETDATE(), JogoID, Nome, Preco FROM deleted
END
GO

CREATE TRIGGER trg_ExclusaoJogo
ON Jogo
INSTEAD OF DELETE
AS
BEGIN
	UPDATE p SET StatusJogo = 0
	FROM Jogo p
	INNER JOIN deleted d
		ON d.JogoID = p.JogoID
END
GO

-- Inserindo Classificações Indicativas
INSERT INTO Classificacao (Nome) VALUES 
('Livre'), ('10+'), ('12+'), ('14+'), ('16+'), ('18+');

-- Inserindo Gêneros
INSERT INTO Genero (Nome) VALUES 
('Ação'), ('Aventura'), ('RPG'), ('Estratégia'), ('Terror'), ('Esporte'), ('FPS');

-- Inserindo Plataformas
INSERT INTO Plataforma (Nome) VALUES 
('PC'), ('PlayStation 5'), ('Xbox Series X'), ('Nintendo Switch'), ('Mobile');
GO

-- Inserindo um Administrador/Usuário
INSERT INTO Usuario (Nome, Email, Senha, StatusUsuario) 
VALUES ('Admin Royal', 'admin@royalgames.com', HASHBYTES('SHA2_256', 'admin@123'), 1);

INSERT INTO Usuario (Nome, Email, Senha, StatusUsuario) 
VALUES ('Admin', 'admin@admin.com', HASHBYTES('SHA2_256', '123'), 1);

-- Inserindo Jogos (Exemplos)
-- Nota: O campo Imagem é VARBINARY, aqui simulamos com um valor hexadecimal curto
INSERT INTO Jogo (Nome, Preco, Imagem, Descricao, StatusJogo, FK_UsuarioID, FK_ClassificacaoID)
VALUES 
('Elden Ring', 249.90, 0x123456, 'Um RPG de ação épico em um mundo de fantasia.', 1, 1, 6),
('The Legend of Zelda', 299.00, 0x789012, 'Aventura em mundo aberto com Link.', 1, 1, 1),
('Counter-Strike 2', 0.00, 0xABCDEF, 'O FPS tático mais jogado do mundo.', 1, 1, 5);
GO

-- Associando Plataformas aos Jogos
INSERT INTO JogoPlataforma (JogoID, PlataformaID) VALUES 
(1, 1), (1, 2), (1, 3), -- Elden Ring: PC, PS5, Xbox
(2, 4),                 -- Zelda: Switch
(3, 1);                 -- CS2: PC

-- Associando Gêneros aos Jogos
INSERT INTO JogoGenero (JogoID, GeneroID) VALUES 
(1, 1), (1, 3), -- Elden Ring: Ação, RPG
(2, 2),         -- Zelda: Aventura
(3, 7), (3, 1); -- CS2: FPS, Ação
GO