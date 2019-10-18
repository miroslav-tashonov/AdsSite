if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\" call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsMSBuildCmd.bat"
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Proffesional\" call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Proffesional\Common7\Tools\VsMSBuildCmd.bat"
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\" call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsMSBuildCmd.bat"

msbuild -t:rebuild AdSite.sln
devenv "AdSite.sln" /Run