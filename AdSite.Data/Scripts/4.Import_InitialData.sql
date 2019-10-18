Use AdSite; 

DECLARE @Username NVARCHAR(50);
SET @Username = 'admin@email.com';

INSERT [adsite].[Countries] ([ID], [CreatedBy],[CreatedAt], [ModifiedBy],[ModifiedAt], [Name],[Abbreviation], [Path])
VALUES (N'426d2592-890c-468b-b421-623ea36ed2b9', N'seedscript', '2019-10-18', N'seedscript', '2019-10-18', N'England', N'en', N'england' ) 

INSERT [adsite].[Languages] ([ID], [CultureId],[LanguageName], [LanguageShortName],[CountryId] )
VALUES (N'8b1f8f01-981f-4b82-9b75-988658f154f2', 1033, N'English', N'En', N'426d2592-890c-468b-b421-623ea36ed2b9' )

INSERT [adsite].[AspNetUsers]([Id], [AccessFailedCount], [ConcurrencyStamp],[Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail],[NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed],[SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'99e6e2ad-7af3-4476-a489-736b570ac84e', 0, N'038680dd-63b0-4aa4-a7a2-85a14692b641', @Username, 1, 1, NULL, @Username, @Username, N'AQAAAAEAACcQAAAAEGyMuZsaEv0I4hMjUNoGUMsUJI8IAukz6QGiJpvpG5RblpHyrtyWeN/XPPHN/d+xlg==', '', 1, N'71289faa-17c4-4230-bebf-76b7b807d474', 0, @UserName)

INSERT [adsite].[AspNetUserRoles] ([UserId], [RoleId])
VALUES (N'99e6e2ad-7af3-4476-a489-736b570ac84e', N'19151b2a-2a3e-4b55-a54b-835489bf5e42')

INSERT [adsite].[UserRoleCountries] ([ID], [ApplicationUserId], [CountryId], [ApplicationIdentityRoleId])
VALUES (N'7ac2e9bf-8fcd-43b2-b61d-dcb3a166e895', N'99e6e2ad-7af3-4476-a489-736b570ac84e', N'426d2592-890c-468b-b421-623ea36ed2b9', N'19151b2a-2a3e-4b55-a54b-835489bf5e42')