REM To build and run the app in a service, perform the following steps:

REM This must be run from the project folder under an admin account
REM the project folder is :\Team2\Users\amato-1\Angular\DataCenterWeb\DataCenterWebApp

REM	stop the service
sc stop DataCenterWebApp

REM	delete the service
sc delete DataCenterWebApp

REM Go to the project folder:
cd C:\Angular\DataCenterWebXfer\DataCenterWebApp

REM Create the publish folder
mkdir c:\Angular\publish

REM Publish the application to the publish folder, targeting a self-contained app runing under Windows
dotnet publish --output c:\angular\publish --self-contained -r win7-x64

REM create the service
sc create DataCenterWebApp binPath= "c:\Angular\publish\DataCenterWebApp.exe"

REM	start the service
sc start DataCenterWebApp


REM In a browser, navigate to http://localhost:5000
REM To stop the service, use the command: sc stop DataCenterWebApp
REM To delete the service, use the command: sc delete DataCenterWebApp



