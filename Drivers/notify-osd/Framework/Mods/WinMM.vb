Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData

<[Namespace]("winmm.dll")>
Public Module WinMM

    <ImportsConstant> Public Const SND_APPLICATION = &H80 ' look for application specific association
    <ImportsConstant> Public Const SND_ALIAS = &H10000 ' name is a WIN.INI [sounds] entry
    <ImportsConstant> Public Const SND_ALIAS_ID = &H110000 ' name is a WIN.INI [sounds] entry identifier
    <ImportsConstant> Public Const SND_ASYNC = &H1 ' play asynchronously
    <ImportsConstant> Public Const SND_FILENAME = &H20000 ' name is a file name
    <ImportsConstant> Public Const SND_LOOP = &H8 ' loop the sound until next sndPlaySound
    <ImportsConstant> Public Const SND_MEMORY = &H4 ' lpszSoundName points to a memory file
    <ImportsConstant> Public Const SND_NODEFAULT = &H2 ' silence not default, if sound not found
    <ImportsConstant> Public Const SND_NOSTOP = &H10 ' don't stop any currently playing sound
    <ImportsConstant> Public Const SND_NOWAIT = &H2000 ' don't wait if the driver is busy
    <ImportsConstant> Public Const SND_PURGE = &H40 ' purge non-static events for task
    <ImportsConstant> Public Const SND_RESOURCE = &H40004 ' name is a resource name or atom
    <ImportsConstant> Public Const SND_SYNC = &H0 ' play synchronously (default)

    <ExportAPI("PlaySoundA")>
    Public Declare Function PlaySound Lib "winmm.dll" Alias "PlaySoundA" (lpszName As String, hModule As Integer, dwFlags As Integer) As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="FileName">*.wav file path.(API只能够播放wav文件)</param>
    Public Sub PlaySound(FileName As String)
        Call WinMM.PlaySound(FileName, 0, SND_FILENAME)
    End Sub
End Module
