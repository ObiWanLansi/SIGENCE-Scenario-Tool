$file = ".\README.md"

"# Here Are Some Screenshot's From The Little SIGENCE Scenario Tool" | Set-Content $file

Get-ChildItem -Path $documents_path -Filter *.png | ForEach-Object {
    
    #"" | Add-Content $file

    "" | Add-Content $file
    "![Sorry, But Here Should Be A Screenshot To See](" + $_.Name + ")" | Add-Content $file
    "" | Add-Content $file
    "*" + $_.Name + " (" + $_.LastWriteTime.ToShortDateString() + ", " + $_.LastWriteTime.ToShortTimeString() + ")*" | Add-Content $file
    "<hr/>" | Add-Content $file
}

