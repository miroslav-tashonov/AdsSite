call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsMSBuildCmd.bat"

devenv "AdSite.sln" /ReBuild
devenv "AdSite.sln" /Command "Debug.Start"