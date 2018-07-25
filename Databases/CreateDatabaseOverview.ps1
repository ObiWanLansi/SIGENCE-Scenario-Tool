clear

#$database_path = 'D:\BigData\GitHub\SIGENCE-Scenario-Tool\Database'
$database_path = 'C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Databases'

Get-ChildItem -Path $database_path -Filter *.sqlite | ForEach-Object {
    
    Write-Host $_.FullName

    $markdownfile = $_.Name + ".md"

    Write-Host $markdownfile 
}