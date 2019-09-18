Installation requirements

Visual Studio 2017 or 2019
Integrated SQL in Visual studio or standalone SQL Server instance
Asp.net core 2.2 SDK
Powershell 3.0 (optional-needed for easier database creation)


Installation steps

1. Clone the repository from https://github.com/miroslav-tashonov/AdsSite 
2. Build and clean the project, make sure asp.net core 2.2 sdk is installed in this step 

Windows: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-windows-x64-installer
Mac: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-macos-x64-installer
Linux: https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-2.2.101


3. Create Database using Visual studio's Package manager console commands (need to have powershell 3.0 in Windows)
As default project in PMC you'll need to set AdSite.Data and than execute following commands

Add-Migration init
Update-Database

, or create database manually with name AdSite

4. Execute project 

5. In order to login as an administrator use the following credentials:

Username: admin@email.com
Password: Admin123!
