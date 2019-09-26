<b>Installation requirements</b>

Visual Studio 2017/2019 or VS Code<br/>
Integrated SQL in Visual studio (windows only) or standalone SQL Server instance on Mac, Windows or Linux<br/>
Asp.net core 2.2 SDK<br/><br/>

Optional: Powershell 3.0 <br/>


<b>Installation steps</b>

1. Clone the repository from https://github.com/miroslav-tashonov/AdsSite 
2. Build and clean the project, make sure asp.net core 2.2 sdk is installed in this step 

  &nbsp;&nbsp;&nbsp;&nbsp;<b>Windows</b>: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-windows-x64-installer </br>
  &nbsp;&nbsp;&nbsp;&nbsp;<b>Mac</b>: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-macos-x64-installer </br>
  &nbsp;&nbsp;&nbsp;&nbsp;<b>Linux</b>: https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-2.2.101 </br>


3. Create Database using Visual studio's Package manager console commands (need to have powershell 3.0 in Windows)
As default project in PMC you'll need to set AdSite.Data and than execute the following commands

&nbsp;&nbsp;&nbsp;&nbsp;Add-Migration init</br>
&nbsp;&nbsp;&nbsp;&nbsp;Update-Database

or otherwise create the database manually with name AdSite

4. Execute or host the project (execute via IISExpress, host with Kestrel)
<br/>
<br/>

<b>Following use case diagram shows how different roles can use the system</b>
<br/>
<br/>
![alt text](https://github.com/miroslav-tashonov/AdsSite/blob/master/AdSite/wwwroot/img/ad-site-usecase.jpg) 


<b>Entity Relationship diagram of the database</b>
<br/>
<br/>
![alt text](https://github.com/miroslav-tashonov/AdsSite/blob/master/AdSite/wwwroot/img/adsite-Database-ER.jpg) 
