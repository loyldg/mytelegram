$version="0.8.821"
$currentDir=(Get-Item .).FullName
$parentFolder=(Get-Item $currentDir).Parent
$outputRootFolder=Join-Path $parentFolder "out" $version 
$sourceRootFolder=Join-Path $parentFolder "./source/src"

$messengerOutputFolder=Join-Path $outputRootFolder "messenger"
$messengerGrpcServiceOutputFolder=Join-Path $outputRootFolder "messenger-grpc-service"
$smsSenderOutputFolder=Join-Path $outputRootFolder "sms-sender"

Write-Output "Current dir:$currentDir"
Write-Output "OutputFolder is:$outputRootFolder"

function CreateFolderIfNotExists([System.String] $folder){
    if(![System.IO.Directory]::Exists($folder)){
        Write-Output "Create new folder:$folder"
        [System.IO.Directory]::CreateDirectory($folder)
    }
}

function Build-Server([System.String]$srcFolder,[System.String] $outputFolder) {
    Set-Location $sourceRootFolder
    Set-Location $srcFolder
    dotnet publish -r win-x64 -c Release
    CreateFolderIfNotExists $outputFolder
    Copy-Item -Force -Path "./bin/Release/net6.0/win-x64/publish/*" -Include appsettings.json,*.key,*.exe,*.dll $outputFolder
}

Set-Location ../source/
#dotnet restore ./MyTelegram.sln
Build-Server "./MyTelegram.MessengerServer.Abp" $messengerOutputFolder
Build-Server "./MyTelegram.MessengerServer.GrpcService" $messengerGrpcServiceOutputFolder
Build-Server "./MyTelegram.SmsSender" $smsSenderOutputFolder
Set-Location $currentDir


