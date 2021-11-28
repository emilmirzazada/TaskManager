@echo off

set datetimef=%date:~-4%-%date:~3,2%-%date:~0,2%%time%
set datetimef=%datetimef%
set datetimef=%datetimef::=-%
set datetimef=%datetimef:,=-%
set datetimef=%datetimef: =_%

REM Creating migrations
dotnet ef migrations add Migration_%datetimef% --startup-project ../TaskManager.WebUI/TaskManager.WebUI.csproj --context TaskManager.Persistence.ApplicationDbContext -o Migrations
