USE AdSite;

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

CREATE TABLE [adsite].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

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

GO

CREATE TABLE [adsite].[Countries] (
    [ID] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Abbreviation] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [adsite].[AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [adsite].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [adsite].[AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [adsite].[AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [adsite].[AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

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

GO

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

GO

CREATE TABLE [adsite].[Languages] (
    [ID] uniqueidentifier NOT NULL,
    [CultureId] int NOT NULL,
    [LanguageName] nvarchar(max) NULL,
    [LanguageShortName] nvarchar(max) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Languages_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [adsite].[UserRoleCountry] (
    [ID] uniqueidentifier NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [CountryId] uniqueidentifier NOT NULL,
    [ApplicationIdentityRoleId] nvarchar(450) NULL,
    CONSTRAINT [PK_UserRoleCountry] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_UserRoleCountry_AspNetRoles_ApplicationIdentityRoleId] FOREIGN KEY ([ApplicationIdentityRoleId]) REFERENCES [adsite].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoleCountry_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [adsite].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoleCountry_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [adsite].[Countries] ([ID]) ON DELETE CASCADE
);

GO

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

GO

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

GO

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

GO

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

GO

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

GO

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

GO

CREATE INDEX [IX_AdDetailPictures_AdDetailID] ON [adsite].[AdDetailPictures] ([AdDetailID]);

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

CREATE INDEX [IX_UserRoleCountry_ApplicationIdentityRoleId] ON [adsite].[UserRoleCountry] ([ApplicationIdentityRoleId]);

GO

CREATE INDEX [IX_UserRoleCountry_ApplicationUserId] ON [adsite].[UserRoleCountry] ([ApplicationUserId]);

GO

CREATE INDEX [IX_UserRoleCountry_CountryId] ON [adsite].[UserRoleCountry] ([CountryId]);

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
VALUES (N'20190926232406_new', N'3.0.0');

GO

CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

CREATE PROCEDURE dbo.aspnet_UsersInRoles_AddUsersToRoles
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END                                       

