/* Справочные таблицы*/
IF not exists (select 1 from information_schema.tables where table_name = 'Alcohols')
CREATE TABLE [dbo].[Alcohols] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Alco] VARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL ,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

IF not exists (select 1 from information_schema.tables where table_name = 'Whatpartners')
CREATE TABLE [dbo].[Whatpartners] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Whatpart] VARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL,
    [Smb]      BIT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'Smokings')
CREATE TABLE [dbo].[Smokings] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [Smoke] VARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'Languages')
CREATE TABLE [dbo].[Languages] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Lang] VARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'Figures')
CREATE TABLE [dbo].[Figures] (
    [Id]  INT          IDENTITY (1, 1) NOT NULL,
    [Fig] VARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'Excitements')
CREATE TABLE [dbo].[Excitements] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Excit] VARCHAR (MAX) COLLATE SQL_Latin1_General_CP1251_CS_AS NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

/* Регионы */
IF not exists (select 1 from information_schema.tables where table_name = 'Countries')
CREATE TABLE [dbo].[Countries] (
    [Id]       INT           NOT NULL,
    [Name]     VARCHAR (128) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NOT NULL,
    [Priority] TINYINT       DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'Regions')
CREATE TABLE [dbo].[Regions] (
    [Id]        INT           NOT NULL,
    [CountryId] INT           DEFAULT ((0)) NOT NULL,
    [Name]      VARCHAR (128) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [Priority]  TINYINT       DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Regions_ToCountries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);

IF not exists (select 1 from information_schema.tables where table_name = 'Cities')
CREATE TABLE [dbo].[Cities] (
    [Id]        INT           NOT NULL,
    [CountryId] INT           DEFAULT ((0)) NOT NULL,
    [RegionId]  INT           DEFAULT ((0)) NOT NULL,
    [Name]      VARCHAR (128) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NOT NULL,
    [Priority]  TINYINT       DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_ToCountry] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]),
    CONSTRAINT [FK_City_ToRegion] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Regions] ([Id])
);

/*Пользователи и роли*/
IF not exists (select 1 from information_schema.tables where table_name = 'Roles')
CREATE TABLE [dbo].[Roles] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'UserLogins')
CREATE TABLE [dbo].[UserLogins] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [nic]          NVARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('')NOT NULL,
    [pass]         NVARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NOT NULL,
    [email]        NVARCHAR (50) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NOT NULL,
    [confirm]      BIT           NULL,
    [RoleId]       INT           NULL,
    [CreationDate] SMALLDATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserLogins_ToRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

IF not exists (select 1 from information_schema.tables where table_name = 'UserProfiles')
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
    [AvatarPath]     VARCHAR (100) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [Age]            TINYINT       NULL,
    [FigureId]       INT           NULL,
    [SmokingId]      INT           NULL,
    [AlcoholId]      INT           NULL,
    [About]          VARCHAR (MAX) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [MyIdeal]        VARCHAR (MAX) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserProfils_ToUserLogins] FOREIGN KEY ([UserLoginId]) REFERENCES [dbo].[UserLogins] ([Id])
);

IF not exists (select 1 from information_schema.tables where table_name = 'ExcitementUserProfiles')
CREATE TABLE [dbo].[ExcitementUserProfiles] (
    [Excitement_Id]  INT     NOT NULL,
    [UserProfile_Id] INT     NOT NULL,
    [Level]          TINYINT NULL,
    CONSTRAINT [PK_ExcitementUserProfiles] PRIMARY KEY CLUSTERED ([UserProfile_Id] ASC, [Excitement_Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'LanguageUserProfiles')
CREATE TABLE [dbo].[LanguageUserProfiles] (
    [Language_Id]    INT     NOT NULL,
    [UserProfile_Id] INT     NOT NULL,
    [Level]          TINYINT NULL,
    CONSTRAINT [PK_LanguageUserProfiles] PRIMARY KEY CLUSTERED ([UserProfile_Id] ASC, [Language_Id] ASC)
);

IF not exists (select 1 from information_schema.tables where table_name = 'WhatpartnerUserProfiles')
CREATE TABLE [dbo].[WhatpartnerUserProfiles] (
    [Whatpartner_Id] INT NOT NULL,
    [UserProfile_Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Whatpartner_Id] ASC, [UserProfile_Id] ASC)
);


/*Сообщения */
IF not exists (select 1 from information_schema.tables where table_name = 'Messages')
CREATE TABLE [dbo].[Messages] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [SenderId]    INT           NOT NULL,
    [RecipientId] INT           NOT NULL,
    [TextMessage] VARCHAR (MAX) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [CreatedTime] SMALLDATETIME NOT NULL,
    [Read]        BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

/*Сообщения */
IF not exists (select 1 from information_schema.tables where table_name = 'Messages')
CREATE TABLE [dbo].[Messages] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [SenderId]    INT           NOT NULL,
    [RecipientId] INT           NOT NULL,
    [TextMessage] VARCHAR (MAX) COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [CreatedTime] SMALLDATETIME NOT NULL,
    [Read]        BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
IF not exists (select 1 from information_schema.tables where table_name = 'UserFotoes')
CREATE TABLE [dbo].[UserFotoes] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Path]          VARCHAR (100) NOT NULL,
    [Descript]      TEXT          COLLATE SQL_Latin1_General_CP1251_CS_AS DEFAULT ('') NULL,
    [UploadDate]    SMALLDATETIME NULL,
    [UserProfileId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


