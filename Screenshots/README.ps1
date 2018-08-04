$file = ".\README.md"

"# Here Are Some Screenshot's From The Little SIGENCE Scenario Tool" | Set-Content $file

Get-ChildItem -Path $documents_path -Filter *.png | ForEach-Object {
    
    "![Sorry, But Here Should Be A Screenshot To See](" + $_.Name + ")" | Add-Content $file

    # ![Sorry, but here should be a Screenshot :-(](Screenshots/MainApplication.jpg "Screenshot from the MainWindow.")
}

