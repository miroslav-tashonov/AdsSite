Use AdSite;  

INSERT [adsite].[Countries] ([ID], [CreatedBy],[CreatedAt], [ModifiedBy],[ModifiedAt], [Name],[Abbreviation], [Path])
VALUES (@ID, @CreatedBy, @CreatedAt, @ModifiedBy, @ModifiedAt, @Name, @Abbreviation, @Path ) 

INSERT [adsite].[Languages] ([ID], [CultureId],[LanguageName], [LanguageShortName],[CountryId] )
VALUES (@ID, @CultureId, @LanguageName, @LanguageShortName, @CountryId )

INSERT [adsite].[AspNetUsers]([Id], [AccessFailedCount], [ConcurrencyStamp],[Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail],[NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed],[SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (@Id, @AccessFailedCount, @ConcurrencyStamp, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEnd, @NormalizedEmail, @NormalizedUserName, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)

INSERT [adsite].[AspNetUserRoles] ([UserId], [RoleId])
VALUES (@UserId, @RoleId)

INSERT [adsite].[UserRoleCountries] ([ID], [ApplicationUserId], [CountryId], [ApplicationIdentityRoleId])
VALUES (@ID, @ApplicationUserId, @CountryId, @ApplicationIdentityRoleId)