IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240205073054_Initial', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Actors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(120) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [PhotoUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240205200218_Actors', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Films] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(300) NOT NULL,
    [InCinemas] bit NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    [Poster] nvarchar(max) NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240214204616_Films', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [FilmActors] (
    [ActorId] int NOT NULL,
    [FilmId] int NOT NULL,
    [MainCharacter] nvarchar(max) NULL,
    [Order] int NOT NULL,
    CONSTRAINT [PK_FilmActors] PRIMARY KEY ([ActorId], [FilmId]),
    CONSTRAINT [FK_FilmActors_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmActors_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FilmGenres] (
    [GenreId] int NOT NULL,
    [FilmId] int NOT NULL,
    CONSTRAINT [PK_FilmGenres] PRIMARY KEY ([GenreId], [FilmId]),
    CONSTRAINT [FK_FilmGenres_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FilmActors_FilmId] ON [FilmActors] ([FilmId]);
GO

CREATE INDEX [IX_FilmGenres_FilmId] ON [FilmGenres] ([FilmId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240219184105_FilmActors_FilmGenres', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'Name', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[Actors]'))
    SET IDENTITY_INSERT [Actors] ON;
INSERT INTO [Actors] ([Id], [BirthDate], [Name], [PhotoUrl])
VALUES (5, '1962-01-17T00:00:00.0000000', N'Jim Carrey', NULL),
(6, '1965-04-04T00:00:00.0000000', N'Robert Downey Jr.', NULL),
(7, '1981-06-13T00:00:00.0000000', N'Chris Evans', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'Name', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[Actors]'))
    SET IDENTITY_INSERT [Actors] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'InCinemas', N'Poster', N'ReleaseDate', N'Title') AND [object_id] = OBJECT_ID(N'[Films]'))
    SET IDENTITY_INSERT [Films] ON;
INSERT INTO [Films] ([Id], [InCinemas], [Poster], [ReleaseDate], [Title])
VALUES (2, CAST(1 AS bit), NULL, '2019-04-26T00:00:00.0000000', N'Avengers: Endgame'),
(3, CAST(0 AS bit), NULL, '2019-04-26T00:00:00.0000000', N'Avengers: Infinity Wars'),
(4, CAST(0 AS bit), NULL, '2020-02-28T00:00:00.0000000', N'Sonic the Hedgehog'),
(5, CAST(0 AS bit), NULL, '2020-02-21T00:00:00.0000000', N'Emma'),
(6, CAST(0 AS bit), NULL, '2020-08-14T00:00:00.0000000', N'Wonder Woman 1984');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'InCinemas', N'Poster', N'ReleaseDate', N'Title') AND [object_id] = OBJECT_ID(N'[Films]'))
    SET IDENTITY_INSERT [Films] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
    SET IDENTITY_INSERT [Genres] ON;
INSERT INTO [Genres] ([Id], [Name])
VALUES (4, N'Aventura'),
(5, N'Animación'),
(6, N'Suspenso'),
(7, N'Romance');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
    SET IDENTITY_INSERT [Genres] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActorId', N'FilmId', N'MainCharacter', N'Order') AND [object_id] = OBJECT_ID(N'[FilmActors]'))
    SET IDENTITY_INSERT [FilmActors] ON;
INSERT INTO [FilmActors] ([ActorId], [FilmId], [MainCharacter], [Order])
VALUES (5, 4, N'Dr. Ivo Robotnik', 1),
(6, 2, N'Tony Stark', 1),
(6, 3, N'Tony Stark', 1),
(7, 2, N'Steve Rogers', 2),
(7, 3, N'Steve Rogers', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActorId', N'FilmId', N'MainCharacter', N'Order') AND [object_id] = OBJECT_ID(N'[FilmActors]'))
    SET IDENTITY_INSERT [FilmActors] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FilmId', N'GenreId') AND [object_id] = OBJECT_ID(N'[FilmGenres]'))
    SET IDENTITY_INSERT [FilmGenres] ON;
INSERT INTO [FilmGenres] ([FilmId], [GenreId])
VALUES (2, 4),
(3, 4),
(4, 4),
(6, 4),
(2, 6),
(3, 6),
(5, 6),
(6, 6),
(5, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FilmId', N'GenreId') AND [object_id] = OBJECT_ID(N'[FilmGenres]'))
    SET IDENTITY_INSERT [FilmGenres] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240219205117_SeedData', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FilmGenres] ADD [CinemaRoomId] int NULL;
GO

ALTER TABLE [FilmActors] ADD [CinemaRoomId] int NULL;
GO

CREATE TABLE [CinemaRooms] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(120) NOT NULL,
    CONSTRAINT [PK_CinemaRooms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FilmsCinemaRooms] (
    [FilmId] int NOT NULL,
    [CinemaRoomId] int NOT NULL,
    CONSTRAINT [PK_FilmsCinemaRooms] PRIMARY KEY ([FilmId], [CinemaRoomId]),
    CONSTRAINT [FK_FilmsCinemaRooms_CinemaRooms_CinemaRoomId] FOREIGN KEY ([CinemaRoomId]) REFERENCES [CinemaRooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmsCinemaRooms_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FilmGenres_CinemaRoomId] ON [FilmGenres] ([CinemaRoomId]);
GO

CREATE INDEX [IX_FilmActors_CinemaRoomId] ON [FilmActors] ([CinemaRoomId]);
GO

CREATE INDEX [IX_FilmsCinemaRooms_CinemaRoomId] ON [FilmsCinemaRooms] ([CinemaRoomId]);
GO

ALTER TABLE [FilmActors] ADD CONSTRAINT [FK_FilmActors_CinemaRooms_CinemaRoomId] FOREIGN KEY ([CinemaRoomId]) REFERENCES [CinemaRooms] ([Id]);
GO

ALTER TABLE [FilmGenres] ADD CONSTRAINT [FK_FilmGenres_CinemaRooms_CinemaRoomId] FOREIGN KEY ([CinemaRoomId]) REFERENCES [CinemaRooms] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240221185112_NewCinemaRoomTable', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FilmActors] DROP CONSTRAINT [FK_FilmActors_CinemaRooms_CinemaRoomId];
GO

ALTER TABLE [FilmGenres] DROP CONSTRAINT [FK_FilmGenres_CinemaRooms_CinemaRoomId];
GO

DROP INDEX [IX_FilmGenres_CinemaRoomId] ON [FilmGenres];
GO

DROP INDEX [IX_FilmActors_CinemaRoomId] ON [FilmActors];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FilmGenres]') AND [c].[name] = N'CinemaRoomId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FilmGenres] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [FilmGenres] DROP COLUMN [CinemaRoomId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FilmActors]') AND [c].[name] = N'CinemaRoomId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FilmActors] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [FilmActors] DROP COLUMN [CinemaRoomId];
GO

ALTER TABLE [CinemaRooms] ADD [Location] geography NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240221205929_CinemaRoomLocation', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] ON;
INSERT INTO [CinemaRooms] ([Id], [Location], [Name])
VALUES (4, geography::Parse('POINT (-69.9118804 18.4826214)'), N'Sambil');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] ON;
INSERT INTO [CinemaRooms] ([Id], [Location], [Name])
VALUES (5, geography::Parse('POINT (-69.856427 18.506934)'), N'Megacentro');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] ON;
INSERT INTO [CinemaRooms] ([Id], [Location], [Name])
VALUES (6, geography::Parse('POINT (-73.986227 40.730898)'), N'Village East Cinema');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[CinemaRooms]'))
    SET IDENTITY_INSERT [CinemaRooms] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240223161655_CinemaRoomData', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240225052633_IdentityTables', N'6.0.2');
GO

COMMIT;
GO

