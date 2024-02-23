$version="0.18.223"
$currentDir=(Get-Item .).FullName
$parentFolder=(Get-Item $currentDir).Parent
$outputRootFolder=Join-Path $parentFolder "out" $version 
$sourceRootFolder=Join-Path $parentFolder "./source/src"

$gatewayServerOutputFolder=Join-Path $outputRootFolder "gateway"
$messengerCommandServerOutputFolder=Join-Path $outputRootFolder "messenger-command"
$messengerQueryServerOutputFolder=Join-Path $outputRootFolder "messenger-query"
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
	$sourceFolder = Join-Path $sourceRootFolder $srcFolder
    dotnet publish $sourceFolder -c Release -o $outputFolder
    Get-ChildItem -Path $outputFolder *.pdb | ForEach-Object { Remove-Item -Path $_.FullName }
}

# Set-Location ../source/
#dotnet restore ./MyTelegram.sln
Build-Server "./MyTelegram.GatewayServer" $gatewayServerOutputFolder
Build-Server "./MyTelegram.Messenger.CommandServer" $messengerCommandServerOutputFolder
Build-Server "./MyTelegram.Messenger.QueryServer" $messengerQueryServerOutputFolder
Build-Server "./MyTelegram.MessengerServer.GrpcService" $messengerGrpcServiceOutputFolder
Build-Server "./MyTelegram.SmsSender" $smsSenderOutputFolder
Set-Location $currentDir


