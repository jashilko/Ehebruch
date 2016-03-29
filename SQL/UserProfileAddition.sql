CREATE TABLE [dbo].[Figures] (
    [Id]  INT          IDENTITY (1, 1) NOT NULL,
    [Fig] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Figures(Fig) VALUES ('��������')
INSERT INTO Figures(Fig) VALUES ('����������')
INSERT INTO Figures(Fig) VALUES ('�����������')
INSERT INTO Figures(Fig) VALUES ('�������/�������')
INSERT INTO Figures(Fig) VALUES ('� ������� �����')
INSERT INTO Figures(Fig) VALUES ('���� ������ ��')
INSERT INTO Figures(Fig) VALUES ('������')



CREATE TABLE [dbo].[Smokings] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [Smoke] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Smokings (Smoke) VALUES ('�� ����')
INSERT INTO Smokings (Smoke) VALUES ('������ ���������')
INSERT INTO Smokings (Smoke) VALUES ('����')


CREATE TABLE [dbo].[Alcohols] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Alco] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Alcohols (Alco) VALUES ('�� ���')
INSERT INTO Alcohols (Alco) VALUES ('��� � ���������')
INSERT INTO Alcohols (Alco) VALUES ('����� ������ �� ��������')
INSERT INTO Alcohols (Alco) VALUES ('���������')


CREATE TABLE [dbo].[Languages]
(
	  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lang] VARCHAR(50) NOT NULL
)
INSERT INTO Languages (Lang) VALUES ('�������')
INSERT INTO Languages (Lang) VALUES ('����������')
INSERT INTO Languages (Lang) VALUES ('��������')
INSERT INTO Languages (Lang) VALUES ('���������')
INSERT INTO Languages (Lang) VALUES ('���������')
INSERT INTO Languages (Lang) VALUES ('�����������')
INSERT INTO Languages (Lang) VALUES ('�������������')
INSERT INTO Languages (Lang) VALUES ('��������')



CREATE TABLE [dbo].[Excitements] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Excit] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Excitements (Excit) VALUES ('������������ ����')
INSERT INTO Excitements (Excit) VALUES ('������ ��������� �����')
INSERT INTO Excitements (Excit) VALUES ('����������')
INSERT INTO Excitements (Excit) VALUES ('�������������')
INSERT INTO Excitements (Excit) VALUES ('������')
INSERT INTO Excitements (Excit) VALUES ('����-�������')
INSERT INTO Excitements (Excit) VALUES ('����� �� �����')
INSERT INTO Excitements (Excit) VALUES ('�������� �������� �����')
INSERT INTO Excitements (Excit) VALUES ('������ �������� �����')
INSERT INTO Excitements (Excit) VALUES ('��������� � �����')
INSERT INTO Excitements (Excit) VALUES ('����������')
INSERT INTO Excitements (Excit) VALUES ('������')
INSERT INTO Excitements (Excit) VALUES ('������� ����')
INSERT INTO Excitements (Excit) VALUES ('���� ������')
INSERT INTO Excitements (Excit) VALUES ('�������������')
INSERT INTO Excitements (Excit) VALUES ('������ ��������')
INSERT INTO Excitements (Excit) VALUES ('�������')


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