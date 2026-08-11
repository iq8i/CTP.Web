# احفظ هذا الكود كـ Export-Context.ps1 وشغله في مسار المشروع
$OutputFile = "ProjectContext.txt"
Clear-Content $OutputFile -ErrorAction SilentlyContinue

Get-ChildItem -Path .\ -Include *.cs, *.cshtml -Recurse | 
    Where-Object { $_.FullName -notmatch "\\obj\\" -and $_.FullName -notmatch "\\bin\\" } | 
    ForEach-Object {
        Add-Content -Path $OutputFile -Value "======================================================"
        Add-Content -Path $OutputFile -Value "File: $($_.FullName.Replace($PWD.Path, ''))"
        Add-Content -Path $OutputFile -Value "======================================================"
        Get-Content $_.FullName | Add-Content -Path $OutputFile
        Add-Content -Path $OutputFile -Value "`n`n"
    }

Write-Host "تم تجميع ملفات المشروع في: $OutputFile"