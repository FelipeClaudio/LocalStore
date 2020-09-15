# The guid and code below are inspired in the following article: https://medium.com/swlh/generating-code-coverage-reports-in-dotnet-core-bb0c8cca66
# 
# How to generate test coverage reports:
#  1) This is script MUST be executed inside project's test folder
#  2) Install dotnet-reportgenerator as global tool using following command: dotnet tool install --global dotnet-reportgenerator-globaltool
#  3) Install Microsoft.CodeCoverage package using following command inside package manager console: Install-Package Microsoft.CodeCoverage -Version 16.6.1
#  4) Check that .runsettings file exists in test project's root folder
#  5) Run this script in a powershell console
#  6) Open results by clicking on ,\TestResults\coveragereport\index.html
#  7) For new report generations, only steps 5 and 6 are necessary

#OBS: Remember to add TestResults folder to .gitignore file

param($testProjectPath=$pwd)

try {
    $testSettingsPath = "$testProjectPath\.runsettings"
    $testResultsFolder = "$testProjectPath\TestResults"
    
    Remove-Item -path $testResultsFolder -recurse
    New-Item -ItemType "directory" -Path $testResultsFolder

    dotnet test $testProjectPath --settings:$testSettingsPath 
    $recentCoverageFile = Get-ChildItem -File -Filter *.coverage -Path $testResultsFolder -Name -Recurse | Select-Object -First 1;
    write-host 'Test Completed'  -ForegroundColor Green

    $comand = "$env:USERPROFILE\.nuget\packages\microsoft.codecoverage\16.8.0-preview-20200812-03\build\netstandard1.0\CodeCoverage\CodeCoverage.exe analyze  /output:$testResultsFolder\MyTestOutput.coveragexml  $testResultsFolder'\'$recentCoverageFile"
    iex $comand
    write-host 'CoverageXML Generated'  -ForegroundColor Green

    dotnet $env:USERPROFILE\.dotnet\tools\.store\dotnet-reportgenerator-globaltool\4.6.1\dotnet-reportgenerator-globaltool\4.6.1\tools\netcoreapp3.0\any\ReportGenerator.dll "-reports:$testResultsFolder\MyTestOutput.coveragexml" "-targetdir:$testResultsFolder\coveragereport"
    write-host 'CoverageReport Published'  -ForegroundColor Green
}

catch {
    write-host "Caught an exception:" -ForegroundColor Red
    write-host "Exception Type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
    write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
}