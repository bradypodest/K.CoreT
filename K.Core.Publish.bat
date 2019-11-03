color B

del  .PublishFiles\*.*   /s /q

dotnet restore

dotnet build

cd K.Core

dotnet publish -o ..\K.Core\bin\Debug\netcoreapp2.2\

md ..\.PublishFiles

xcopy ..\K.Core\bin\Debug\netcoreapp2.2\*.* ..\.PublishFiles\ /s /e 

echo "Successfully!!!! ^ please see the file .PublishFiles"

cmd