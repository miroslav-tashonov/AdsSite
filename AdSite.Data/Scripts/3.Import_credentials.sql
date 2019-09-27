declare @username nvarchar
declare @password nvarchar
set @username= 'user'
set @password= 'Admin123!'
declare @now datetime
set @now= GETDATE()
exec aspnet_Membership_CreateUser 'AdSite',@username,@password, '','user@mail.com'
EXEC aspnet_Roles_CreateRole 'AdSite', 'Admin'
EXEC aspnet_Roles_CreateRole 'AdSite', 'User'
EXEC aspnet_Roles_CreateRole 'AdSite', 'Viewer'
EXEC aspnet_UsersInRoles_AddUsersToRoles 'AdSite', @password, 'Admin', 8 