Use [AdSite];

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'adsite') IS NULL EXEC(N'CREATE SCHEMA [adsite];');

GO

IF OBJECT_ID(N'[adsite].[AspNetRoles]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
END;

GO

IF OBJECT_ID(N'[adsite].[AspNetUsers]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetUsers] (
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
END;

GO


IF OBJECT_ID(N'[adsite].[Countries]') IS NULL
BEGIN
CREATE TABLE [adsite].[Countries] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Abbreviation] nvarchar(max) NOT NULL,
    [Path] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([ID])
);
END;

GO


IF OBJECT_ID(N'[adsite].[AspNetRoleClaims]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
END;

GO

IF OBJECT_ID(N'[adsite].[AspNetUserClaims]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
END;

GO


IF OBJECT_ID(N'[adsite].[AspNetUserLogins]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
END;

GO

IF OBJECT_ID(N'[adsite].[AspNetUserRoles]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
END;

GO

IF OBJECT_ID(N'[adsite].[AspNetUserTokens]') IS NULL
BEGIN
CREATE TABLE [adsite].[AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
END;

GO

IF OBJECT_ID(N'[adsite].[Categories]') IS NULL
BEGIN
CREATE TABLE [adsite].[Categories] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    [ParentId] uniqueidentifier NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Categories_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Categories_Categories_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [adsite].[Categories] ([ID]) ON DELETE NO ACTION
);
END;

GO

IF OBJECT_ID(N'[adsite].[Cities]') IS NULL
BEGIN
CREATE TABLE [adsite].[Cities] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Postcode] nvarchar(max) NOT NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Cities_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION
);
END;

GO
IF OBJECT_ID(N'[adsite].[Languages]') IS NULL
BEGIN
CREATE TABLE [adsite].[Languages] (
    [ID] uniqueidentifier NOT NULL,
    [CultureId] int NOT NULL,
    [LanguageName] nvarchar(max) NULL,
    [LanguageShortName] nvarchar(max) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Languages_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION
);
END;

GO
IF OBJECT_ID(N'[adsite].[UserRoleCountries]') IS NULL
BEGIN
CREATE TABLE [adsite].[UserRoleCountries] (
    [ID] uniqueidentifier NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    [ApplicationIdentityRoleId] nvarchar(450) NULL,
    CONSTRAINT [PK_UserRoleCountries] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_UserRoleCountries_AspNetRoles_ApplicationIdentityRoleId] FOREIGN KEY ([ApplicationIdentityRoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoleCountries_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoleCountries_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE CASCADE
);
END;

GO
IF OBJECT_ID(N'[adsite].[WebSettings]') IS NULL
BEGIN
CREATE TABLE [adsite].[WebSettings] (
    [ID] uniqueidentifier NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [FacebookSocialLink] nvarchar(max) NULL,
    [InstagramSocialLink] nvarchar(max) NULL,
    [TwitterSocialLink] nvarchar(max) NULL,
    [GooglePlusSocialLink] nvarchar(max) NULL,
    [VKSocialLink] nvarchar(max) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_WebSettings] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_WebSettings_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION
);
END;

GO
IF OBJECT_ID(N'[adsite].[Ads]') IS NULL
BEGIN
CREATE TABLE [adsite].[Ads] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [CategoryID] uniqueidentifier NOT NULL,
    [CityID] uniqueidentifier NOT NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [OwnerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Ads] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Ads_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [adsite].[Categories] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ads_Cities_CityID] FOREIGN KEY ([CityID]) REFERENCES [adsite].[Cities] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ads_Countries_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ads_AspNetUsers_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE SET NULL
);
END;
GO

IF OBJECT_ID(N'[adsite].[Localizations]') IS NULL
BEGIN
CREATE TABLE [adsite].[Localizations] (
    [ID] uniqueidentifier NOT NULL,
    [LocalizationKey] nvarchar(max) NOT NULL,
    [LocalizationValue] nvarchar(max) NOT NULL,
    [LanguageId] uniqueidentifier NOT NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Localizations] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Localizations_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Localizations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [adsite].[Languages] ([ID]) ON DELETE NO ACTION
);
END;

GO

IF OBJECT_ID(N'[adsite].[AdDetails]') IS NULL
BEGIN
CREATE TABLE [adsite].[AdDetails] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MainPictureThumbnailFile] varbinary(max) NULL,
    [AdID] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AdDetails] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_AdDetails_Ads_AdID] FOREIGN KEY ([AdID]) REFERENCES [adsite].[Ads] ([ID]) ON DELETE CASCADE
);
END;

GO
IF OBJECT_ID(N'[adsite].[Wishlists]') IS NULL
BEGIN
CREATE TABLE [adsite].[Wishlists] (
    [ID] uniqueidentifier NOT NULL,
    [OwnerId] nvarchar(450) NULL,
    [AdId] uniqueidentifier NOT NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Wishlists] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Wishlists_Ads_AdId] FOREIGN KEY ([AdId]) REFERENCES [adsite].[Ads] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wishlists_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wishlists_AspNetUsers_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
END;

GO

IF OBJECT_ID(N'[adsite].[AdDetailPictures]') IS NULL
BEGIN
CREATE TABLE [adsite].[AdDetailPictures] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [AdDetailID] uniqueidentifier NOT NULL,
    [File] varbinary(max) NULL,
    [Name] nvarchar(max) NULL,
    [IsMainPicture] bit NOT NULL,
    [Type] nvarchar(max) NULL,
    CONSTRAINT [PK_AdDetailPictures] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_AdDetailPictures_AdDetails_AdDetailID] FOREIGN KEY ([AdDetailID]) REFERENCES [adsite].[AdDetails] ([ID]) ON DELETE CASCADE
);
END;

GO
If IndexProperty(Object_Id('[adsite].[AdDetailPictures]'), '[IX_AdDetailPictures_AdDetailID]', 'IndexId') Is Null
Begin
CREATE INDEX [IX_AdDetailPictures_AdDetailID] ON [adsite].[AdDetailPictures] ([AdDetailID]);
end;
GO

CREATE UNIQUE INDEX [IX_AdDetails_AdID] ON [adsite].[AdDetails] ([AdID]);

GO

CREATE INDEX [IX_Ads_CategoryID] ON [adsite].[Ads] ([CategoryID]);

GO

CREATE INDEX [IX_Ads_CityID] ON [adsite].[Ads] ([CityID]);

GO

CREATE INDEX [IX_Ads_CountryID] ON [adsite].[Ads] ([CountryID]);

GO

CREATE INDEX [IX_Ads_OwnerId] ON [adsite].[Ads] ([OwnerId]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [adsite].[AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [adsite].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [adsite].[AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [adsite].[AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [adsite].[AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [adsite].[AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [adsite].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_Categories_CountryId] ON [adsite].[Categories] ([CountryId]);

GO

CREATE INDEX [IX_Categories_ParentId] ON [adsite].[Categories] ([ParentId]);

GO

CREATE UNIQUE INDEX [IX_Categories_ID_ParentId] ON [adsite].[Categories] ([ID], [ParentId]) WHERE [ParentId] IS NOT NULL;

GO

CREATE INDEX [IX_Cities_CountryId] ON [adsite].[Cities] ([CountryId]);

GO

CREATE INDEX [IX_Languages_CountryId] ON [adsite].[Languages] ([CountryId]);

GO

CREATE INDEX [IX_Localizations_CountryId] ON [adsite].[Localizations] ([CountryId]);

GO

CREATE INDEX [IX_Localizations_LanguageId] ON [adsite].[Localizations] ([LanguageId]);

GO

CREATE INDEX [IX_UserRoleCountries_ApplicationIdentityRoleId] ON [adsite].[UserRoleCountries] ([ApplicationIdentityRoleId]);

GO

CREATE INDEX [IX_UserRoleCountries_ApplicationUserId] ON [adsite].[UserRoleCountries] ([ApplicationUserId]);

GO

CREATE INDEX [IX_UserRoleCountries_CountryId] ON [adsite].[UserRoleCountries] ([CountryId]);

GO

CREATE INDEX [IX_WebSettings_CountryId] ON [adsite].[WebSettings] ([CountryId]);

GO

CREATE INDEX [IX_Wishlists_AdId] ON [adsite].[Wishlists] ([AdId]);

GO

CREATE INDEX [IX_Wishlists_CountryId] ON [adsite].[Wishlists] ([CountryId]);

GO

CREATE INDEX [IX_Wishlists_OwnerId] ON [adsite].[Wishlists] ([OwnerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191009195136_init', N'3.0.0');

GO

