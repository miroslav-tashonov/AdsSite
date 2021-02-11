<h2><a id="user-content-books-general-info" class="anchor" aria-hidden="true" href="#books-general-info"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a><g-emoji class="g-emoji" alias="books" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f4da.png">📚</g-emoji> General info</h2>

This project represents responsive multi-site role based ads site built with .net5.0 and Angular 11 ( with karma+jasmine as FE test framework) from scratch. Its my hobby open-source project that i use to integrate new technologies.
<br/>



![ezgif com-gif-maker](https://user-images.githubusercontent.com/3856771/107635736-edb08080-6c6b-11eb-8d98-daef55bf1fa5.gif)

<br/>

<h2><a id="user-content-signal_strength-technologies" class="anchor" aria-hidden="true" href="#signal_strength-technologies"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a><g-emoji class="g-emoji" alias="signal_strength" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f4f6.png">📶</g-emoji> Technologies</h2>
<li><a href="https://devblogs.microsoft.com/dotnet/announcing-net-5-0/" rel="nofollow">NET 5.0</a></li>
<li><a href="https://jwt.io/introduction" rel="nofollow">JWT Bearer Authentication</a></li>
<li><a href="https://material.angular.io/" rel="nofollow">Angular v11</a></li>
<li><a href="https://codecraft.tv/courses/angular/unit-testing/jasmine-and-karma/" rel="nofollow">Jasmine+Karma</a></li>
<li><a href="https://www.microsoft.com/en-us/sql-server/sql-server-2019" rel="nofollow">MSSQL</a></li>
<li><a href="https://nunit.org/" rel="nofollow">Nunit/NSubstitute</a></li>
<li><a href="https://www.docker.com/" rel="nofollow">Docker</a></li>

<br/>
<h2><a id="user-content-floppy_disk-setup" class="anchor" aria-hidden="true" href="#floppy_disk-setup"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a><g-emoji class="g-emoji" alias="floppy_disk" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f4be.png">💾</g-emoji> Setup</h2>

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
