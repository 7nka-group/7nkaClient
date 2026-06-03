@echo off
REM Builds a single small self-contained .exe in publish\WebApp.exe
dotnet publish WebApp.csproj -c Release -r win-x64 -o publish
echo.
echo Done. Output: publish\WebApp.exe
pause
