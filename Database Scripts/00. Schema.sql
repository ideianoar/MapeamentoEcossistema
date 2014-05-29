--CREATE DATABASE MapeamentoEcossistema
--GO

--USE MapeamentoEcossistema
--GO

-- Usuários com acesso ao sistema.
CREATE TABLE Users (
	Id INT IDENTITY NOT NULL,
	AccessToken VARCHAR(50) NOT NULL,
	Name VARCHAR(200) NULL,
	CONSTRAINT PK_Users PRIMARY KEY (Id))

-- Questionário (ex.: questionário de startup ou de investidor).
CREATE TABLE Questionnaires (
	Id INT IDENTITY NOT NULL,
	Name VARCHAR(200) NOT NULL,
	Alias VARCHAR(200) NOT NULL,
	Description VARCHAR(2000) NULL,
	CONSTRAINT PK_Questionnaires PRIMARY KEY (Id))

-- Grupos de perguntas (ex.: dentro do questionário de perguntas, temos primeiro um
-- formulário com dados iniciais e depois um grupo com perguntas de checkpoint).
CREATE TABLE QuestionGroups (
	Id INT IDENTITY NOT NULL,
	QuestionnaireId INT NOT NULL,
	Name VARCHAR(200) NOT NULL,
	Description VARCHAR(2000) NULL,
	SortOrder SMALLINT NOT NULL,
	CONSTRAINT PK_QuestionGroups PRIMARY KEY (Id),
	CONSTRAINT FK_QuestionGroups_Questionnaires FOREIGN KEY (QuestionnaireId) REFERENCES Questionnaires(Id))

-- Tabela base (generalização/especialização) para cadastro de perguntas.
CREATE TABLE Questions (
	Id INT IDENTITY NOT NULL,
	QuestionGroupId INT NOT NULL,
	Title VARCHAR(2000) NOT NULL,
	SortOrder SMALLINT NOT NULL,
	CONSTRAINT PK_Questions PRIMARY KEY (Id),
	CONSTRAINT FK_Questions_QuestionGroups FOREIGN KEY (QuestionGroupId) REFERENCES QuestionGroups(Id))

-- Especialização: Perguntas de checkpoint (para o questionário de startups).
CREATE TABLE CheckpointQuestions (
	Id INT NOT NULL,
	PrimaryPlaceholderText VARCHAR(500) NULL,
	SecondaryPlaceholderText VARCHAR(500) NULL,
	IsPrimaryQuestionMandatory BIT NOT NULL,
	IsSecondaryQuestionMandatory BIT NOT NULL,
	ExampleText VARCHAR(2000) NULL,
	ProblemText VARCHAR(2000) NULL,
	CONSTRAINT PK_CheckpointQuestions PRIMARY KEY (Id),
	CONSTRAINT FK_CheckpointQuestions_Questions FOREIGN KEY (Id) REFERENCES Questions(Id))

-- Especialização: Perguntas de formulário.
CREATE TABLE FormQuestions (
	Id INT NOT NULL,
	FieldType SMALLINT NOT NULL /* Conforme enum da aplicação */,
	IsMandatory BIT NOT NULL,
	CONSTRAINT PK_FormQuestions PRIMARY KEY (Id),
	CONSTRAINT FK_FormQuestions_Questions FOREIGN KEY (Id) REFERENCES Questions(Id))

-- Tabela base (generalização/especialização) para cadastro de respostas.
CREATE TABLE Responses (
	Id INT IDENTITY NOT NULL,
	QuestionId INT NOT NULL,
	UserId INT NOT NULL,
	SessionId UNIQUEIDENTIFIER NOT NULL, /* Um mesmo usuário pode responder ao mesmo questionário várias vezes. Este campo serve para agrupar as respostas no momento de enviar o formulário. */
	CreatedDateTime DATETIME NOT NULL,
	CONSTRAINT PK_Responses PRIMARY KEY (Id),
	CONSTRAINT FK_Responses_Questions FOREIGN KEY (QuestionId) REFERENCES Questions(Id),
	CONSTRAINT FK_Responses_Users FOREIGN KEY (UserId) REFERENCES Users(Id))

-- Especialização: Respostas do usuário para perguntas de checkpoint.
CREATE TABLE CheckpointResponses (
	Id INT NOT NULL,
	YesNoFlag BIT NOT NULL,
	PrimaryResponseText VARCHAR(4000) NULL,
	SecondaryResponseText VARCHAR(4000) NULL,
	CONSTRAINT PK_CheckpointResponses PRIMARY KEY (Id),
	CONSTRAINT FK_CheckpointResponses_Responses FOREIGN KEY (Id) REFERENCES Responses(Id))

-- Repostas do usuário para perguntas de formulário.
CREATE TABLE FormResponses (
	Id INT NOT NULL,
	ResponseText VARCHAR(4000) NULL,
	CONSTRAINT PK_FormResponses PRIMARY KEY (Id),
	CONSTRAINT FK_FormResponses_Responses FOREIGN KEY (Id) REFERENCES Responses(Id))
