This project represents multi-site role based ads site built with .net5.0 from scratch. 

<b>Installation Steps</b>

Clone and run, that's it :) 
<br/>
<br/>
The default access URL is http://localhost:5050/
<br/>
By default the project relies on docker-compose deployment, hence you should have a containerized applications support (<a href="https://www.docker.com/products/docker-desktop">Docker Desktop</a>), but you can also alter you settings and host this project in mac, windows or linux.
<br/>
<br/>
<b>Behind the scenes :</b>
There are two docker images, one for the site and one database image. On project startup there is a seed procedure to create a new database (also into mssql container image) if it doesnt exist, a separate site for the multi-site support, localizations, roles and an admin account. For more customization see the optional ways for installation via the setup wizard.


Optional installation steps:
Download [Setup-Standalone.exe](https://drive.google.com/open?id=1hAup8B57sQO_0MtfDrsrM5roJUbpBFhj) or [Setup-FrameworkDependant.exe](https://drive.google.com/open?id=1zj904FqB3znB8qNeNzD1zfzDFvIHzHuX) that will guide you to setup this project on your windows environment 
<br/>
<br/>
Note: If your have any troubles setting up this project using setup, check this [instructions](Instructions.txt) for manual steps.   

<br/>
<br/>

[Use case diagram](https://github.com/miroslav-tashonov/AdsSite/blob/master/AdSite/wwwroot/img/ad-site-usecase.jpg) shows how different roles can use the system
<br/>
[Entity Relationship diagram](https://github.com/miroslav-tashonov/AdsSite/blob/master/AdSite/wwwroot/img/adsite-Database-ER.jpg) of the database

<br/>
Screenshots : 
<br />
<br />
<p float="left">
  <img src="https://i.imgur.com/uNocJ40.png" width=100>
  <img src="https://i.imgur.com/0riw0kK.png" width=100>
  <img src="https://i.imgur.com/GSmfrlw.png" width=100>
</p>

