call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsMSBuildCmd.bat"

msbuild -t:rebuild AdSite.sln
devenv "AdSite.sln" /Run