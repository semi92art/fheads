SET mypath=%~dp0
echo %mypath:~0,-1%

if exist Logs (rmdir Logs /s /q)
if exist obj (rmdir obj /s /q)
if exist Temp (rmdir Temp /s /q)
if exist Library (rmdir Library /s /q)