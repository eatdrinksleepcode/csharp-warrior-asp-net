powershell.exe -NoProfile -ExecutionPolicy unrestricted -command ".\build.ps1 %1;exit $LASTEXITCODE"

if %ERRORLEVEL% == 0 GOTO :EOF

GOTO CASE_%ERRORLEVEL%

:CASE_11
	set "buildMessage=Build Failed Due to Compilation Errors"
	GOTO TEAM_CITY

:TEAM_CITY
echo ##teamcity[buildStatus status='FAILURE' text='%buildMessage%']
exit /B %ERRORLEVEL%
