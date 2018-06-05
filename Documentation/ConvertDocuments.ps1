$documents_path = 'C:\Lanser\Entwicklung\GitRepositories\SIGENCE-Scenario-Tool\Documentation'

$word_app = New-Object -ComObject Word.Application

# This filter will find .doc as well as .docx documents
Get-ChildItem -Path $documents_path -Filter *.docx | ForEach-Object {

    $document = $word_app.Documents.Open($_.FullName)

    $pdf_filename = "$($_.DirectoryName)\Generated\$($_.BaseName).pdf"
    $document.SaveAs([ref] $pdf_filename, [ref] 17)

    $xps_filename = "$($_.DirectoryName)\Generated\$($_.BaseName).xps"
    $document.SaveAs([ref] $xps_filename, [ref] 18)

    #$html_filename = "$($_.DirectoryName)\Generated\$($_.BaseName).html"
    #$document.SaveAs([ref] $html_filename, [ref] 8)

    $document.Close()
}

$word_app.Quit()