CREATE TABLE [dbo].[Figures] (
    [Id]  INT          IDENTITY (1, 1) NOT NULL,
    [Fig] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Figures(Fig) VALUES ('Стройное')
INSERT INTO Figures(Fig) VALUES ('Подтянутое')
INSERT INTO Figures(Fig) VALUES ('Мускулистое')
INSERT INTO Figures(Fig) VALUES ('Обычное/Среднее')
INSERT INTO Figures(Fig) VALUES ('В хорошей форме')
INSERT INTO Figures(Fig) VALUES ('Пара лишних кг')
INSERT INTO Figures(Fig) VALUES ('Пышное')



CREATE TABLE [dbo].[Smokings] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [Smoke] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Smokings (Smoke) VALUES ('Не курю')
INSERT INTO Smokings (Smoke) VALUES ('Иногда покуриваю')
INSERT INTO Smokings (Smoke) VALUES ('Курю')


CREATE TABLE [dbo].[Alcohols] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Alco] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Alcohols (Alco) VALUES ('Не пью')
INSERT INTO Alcohols (Alco) VALUES ('Пью в компаниях')
INSERT INTO Alcohols (Alco) VALUES ('Люблю выпить по выходным')
INSERT INTO Alcohols (Alco) VALUES ('Алкоголик')


CREATE TABLE [dbo].[Languages]
(
	  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lang] VARCHAR(50) NOT NULL
)
INSERT INTO Languages (Lang) VALUES ('Русский')
INSERT INTO Languages (Lang) VALUES ('Английский')
INSERT INTO Languages (Lang) VALUES ('Немецкий')
INSERT INTO Languages (Lang) VALUES ('Китайский')
INSERT INTO Languages (Lang) VALUES ('Испанский')
INSERT INTO Languages (Lang) VALUES ('Французский')
INSERT INTO Languages (Lang) VALUES ('Португальский')
INSERT INTO Languages (Lang) VALUES ('Арабский')



CREATE TABLE [dbo].[Excitements] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Excit] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Excitements (Excit) VALUES ('Традиционный секс')
INSERT INTO Excitements (Excit) VALUES ('Слегка необычные вкусы')
INSERT INTO Excitements (Excit) VALUES ('Подчинение')
INSERT INTO Excitements (Excit) VALUES ('Доминирование')
INSERT INTO Excitements (Excit) VALUES ('Фетиши')
INSERT INTO Excitements (Excit) VALUES ('Секс-игрушки')
INSERT INTO Excitements (Excit) VALUES ('Ванна на двоих')
INSERT INTO Excitements (Excit) VALUES ('Получать оральные ласки')
INSERT INTO Excitements (Excit) VALUES ('Давать оральные ласки')
INSERT INTO Excitements (Excit) VALUES ('Разговоры о сексе')
INSERT INTO Excitements (Excit) VALUES ('Связывание')
INSERT INTO Excitements (Excit) VALUES ('Шлепки')
INSERT INTO Excitements (Excit) VALUES ('Ролевые игры')
INSERT INTO Excitements (Excit) VALUES ('Секс втроем')
INSERT INTO Excitements (Excit) VALUES ('Агрессивность')
INSERT INTO Excitements (Excit) VALUES ('Долгая прелюдия')
INSERT INTO Excitements (Excit) VALUES ('Поцелуи')


CREATE TABLE [dbo].[LanguageUserProfiles]
(
	[Language_Id] INT NOT NULL,
	[UserProfile_Id] INT NOT NULL, 
	[Level] TINYINT NULL, 
    CONSTRAINT [PK_LanguageUserProfiles] PRIMARY KEY ([UserProfile_Id], [Language_Id]) 

)

CREATE TABLE [dbo].[ExcitementUserProfiles]
(
	[Excitement_Id] INT NOT NULL,
	[UserProfile_Id] INT NOT NULL, 
	[Level] TINYINT NULL, 
    CONSTRAINT [PK_ExcitementUserProfiles] PRIMARY KEY ([UserProfile_Id], [Excitement_Id]) 

);


CREATE TABLE [dbo].[Whatpartners] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Whatpart] VARCHAR (50) NOT NULL,
    [Smb] BIT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[WhatpartnerUserProfils]
(
	[Whatpartner_Id] INT NOT NULL , 
    [UserProfile_Id] INT NOT NULL, 
    PRIMARY KEY ([Whatpartner_Id], [UserProfile_Id])

);

/*
CREATE TABLE [dbo].[UserProfiles] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [dateOfBirthday] SMALLDATETIME NULL,
    [CountryId]      INT           NOT NULL,
    [RegionId]       INT           NOT NULL,
    [CityId]         INT           NOT NULL,
    [wish]           SMALLINT      NULL,
    [height]         SMALLINT      NULL,
    [weight]         SMALLINT      NULL,
    [sex]            BIT           NOT NULL,
    [LastUpdateDate] SMALLDATETIME NULL,
    [UserLoginId]    INT           NOT NULL,
    [AvatarPath]     VARCHAR (100) NULL,
    [Age]            BIT           NULL,
    [FigureId]       INT           NULL,
    [SmokingId]      INT           NULL,
    [AlcoholId]      INT           NULL,
    [About]          VARCHAR (MAX) NULL,
    [MyIdeal]        VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserProfils_ToUserLogins] FOREIGN KEY ([UserLoginId]) REFERENCES [dbo].[UserLogins] ([Id]),
    CONSTRAINT [FK_UserProfils_ToAlcohol] FOREIGN KEY ([AlcoholId]) REFERENCES [dbo].[Alcohols] ([Id]),
    CONSTRAINT [FK_UserProfils_ToFigures] FOREIGN KEY ([FigureId]) REFERENCES [dbo].[Figures] ([Id]),
    CONSTRAINT [FK_UserProfils_ToSmokings] FOREIGN KEY ([SmokingId]) REFERENCES [dbo].[Smokings] ([Id])
);

*/