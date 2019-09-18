<b>Installation requirements</b>

Visual Studio 2017 or 2019<br/>
Integrated SQL in Visual studio or standalone SQL Server instance<br/>
Asp.net core 2.2 SDK<br/>
Powershell 3.0 (optional-needed for easier database creation)<br/>


<b>Installation steps</b>

1. Clone the repository from https://github.com/miroslav-tashonov/AdsSite 
2. Build and clean the project, make sure asp.net core 2.2 sdk is installed in this step 

  &nbsp;&nbsp;&nbsp;&nbsp;<b>Windows</b>: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-windows-x64-installer </br>
  &nbsp;&nbsp;&nbsp;&nbsp;<b>Mac</b>: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-macos-x64-installer </br>
  &nbsp;&nbsp;&nbsp;&nbsp;<b>Linux</b>: https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-2.2.101 </br>


3. Create Database using Visual studio's Package manager console commands (need to have powershell 3.0 in Windows)
As default project in PMC you'll need to set AdSite.Data and than execute following commands

&nbsp;&nbsp;&nbsp;&nbsp;Add-Migration init</br>
&nbsp;&nbsp;&nbsp;&nbsp;Update-Database

, or otherwise create the database manually with name AdSite

4. Execute project 

5. In order to login as an administrator use the following credentials:

&nbsp;&nbsp;&nbsp;&nbsp;Username: admin@email.com
Password: Admin123!
