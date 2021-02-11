This project represents multi-site role based ads site built with .net5.0 and Angular 11 ( with karma+jasmine as FE test framework) from scratch. Its my hobby open-source project that i use to integrate new technologies.
<br/>



![ezgif com-gif-maker](https://user-images.githubusercontent.com/3856771/107635736-edb08080-6c6b-11eb-8d98-daef55bf1fa5.gif)

<br/>

<b>Installation Steps</b>

By default the project relies on docker-compose deployment, hence you should have a containerized applications support (<a href="https://www.docker.com/products/docker-desktop">Docker Desktop</a>), but you can also alter the settings and host this project in mac, windows or linux.
<br/>
<br/>
<b>Behind the scenes :</b>
There are two docker images, one for the site and one database image. On project startup there is a seed procedure to create a new database (also into mssql container image) if it doesnt exist, a separate site for the multi-site support, localizations, language, roles and an admin account. For more customization see the optional ways for installation via the setup wizard.

<i>Optional installation steps:</i>
Download [Setup-Standalone.exe](https://drive.google.com/open?id=1hAup8B57sQO_0MtfDrsrM5roJUbpBFhj) or [Setup-FrameworkDependant.exe](https://drive.google.com/open?id=1zj904FqB3znB8qNeNzD1zfzDFvIHzHuX) that will guide you to setup this project on your windows environment. If your have any troubles setting up this project using setup, check this [instructions](Instructions.txt) for manual steps.   

<br/>

The following images represent the relations between entities and use case flow diagram. 

<br/>

![adsite-Database-ER](https://user-images.githubusercontent.com/3856771/99712166-f47f8b00-2aa2-11eb-9466-122bd01c1c1b.jpg)
<br/>
<br/>
<br/>
![ad-site-usecase](https://user-images.githubusercontent.com/3856771/99712349-390b2680-2aa3-11eb-896f-9df1aef66290.jpg)
<br/>
<br/>
Enjoy :) 
