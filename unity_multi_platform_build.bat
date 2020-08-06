:: Title:            unity_multi_platform_build.bat
:: Description:      Builds unity projects for multiple platforms and uploads the webgl build
::                   to the repo's gh-pages branch
:: Requirements:     Windows environment
::                   7z and git must be in the path environment variable
::                   This batch script must be at the root of the project
::                   Needs to find BuildScript.cs in the file subtree
::                   UNITY_HOME needs to point to the appropriate installed version of Unity
::                   GITHUB_TOKEN must be available in the environment, or set manually here
::                   The project which you want to build can't be open
:: Author:           azarrias
:: Usage:            unity_multi_platform_build.bat
::================================================================================

@echo off
setlocal enabledelayedexpansion

::Use this to locate Unity installations in the system drive, if you need
::pushd %SYSTEMDRIVE%\
::dir unity.exe /b /s
::popd

if exist "%~dp0build" (
  pushd "%~dp0build"
  for /F "delims=" %%i in ('dir /b') do (
    for %%j in (%%i) do (
	  if exist %%~sj\nul (
	    echo Deleted folder - "%~dp0build\%%i"
	    rmdir "%%i" /s/q
	  ) else (
	    echo Deleted file - "%~dp0build\%%i"
	    del "%%i"
	  )
	)
  )
  popd
)

echo Building...
set UNITY_HOME=C:\Program Files\Unity\Hub\Editor\2017.4.40f1\Editor\Unity.exe
"%UNITY_HOME%" -quit -batchmode -executeMethod BuildScript.BuildAll

pushd build
for /d %%G in ("*win_x*") do call :CreateZip %%~nxG
for /d %%G in ("*lin_x*") do call :CreateTarGz %%~nxG
for /d %%G in ("*webgl*") do (
  call :CreateZip %%~nxG
  call :CommitWeb %%~nxG
)
popd
exit /b %ERRORLEVEL%

:CreateZip
echo Packing %~1
7z.exe a %~1.zip %~1
exit /b 0

:CreateTarGz
echo Packing %~1
7z.exe a -ttar -so %~1.tar %~1 | 7z.exe a -si %~1.tar.gz
exit /b 0

:CommitWeb
echo Pushing %~1 to %git_repo% gh-pages branch
for /f %%i in ('git config --get remote.origin.url') do set git_remote=%%i
set "x=%git_remote:github.com=" & set "git_repo=%"
pushd %~1
git init
git add .
git commit -m "Deploy %~1 to github pages"
git push --force --quiet "https://%GITHUB_TOKEN%@github.com%git_repo%" master:gh-pages
popd
exit /b 0

endlocal