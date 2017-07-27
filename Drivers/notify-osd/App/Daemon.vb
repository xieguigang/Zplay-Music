Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports System.Text.Encoding
Imports Microsoft.VisualBasic.MMFProtocol
Imports Microsoft.VisualBasic.CommandLine
Imports NotifyOsd.Framework.Balloon

''' <summary>
''' Daemon module of the notification services.(这个是主进程)
''' </summary>
Module DaemonProcess

    Dim _continuesThread As Boolean = True
    Dim _controller As Interpreter = New Interpreter(GetType(DaemonProcess))
    Dim _osdNotifier As OsdNotifier = New OsdNotifier

    Dim WithEvents _serviceSocket As MMFSocket

    Public ReadOnly Property Manual As String
        Get
            Return DaemonProcess._controller.SDKdocs
        End Get
    End Property

    Public Sub SendMessage(msg As Message)
        Call _osdNotifier.SendMessage(msg)
    End Sub

    <ExportAPI("-Stop", Info:="Stops this notify-osd services instance")>
    Public Function ExitThread() As Integer
        _continuesThread = False
        Return 0
    End Function

    <ExportAPI("-SendMessage",
               Info:="Displays a user message.",
               Usage:="-SendMessage -title <title> -message <message> -icon <icon_url>")>
    Public Function SendMessage(args As CommandLine) As Integer
        If args Is Nothing Then
            Return -1
        Else
            Call $"New message ""{args.CLICommandArgvs}""".__DEBUG_ECHO
        End If

        Dim Osd_Message As Message =
            New Message With {
                .Title = args("-title"),
                .Message = args("-message"),
                .IconURL = args("-icon")
        }
        Call DaemonProcess._osdNotifier.SendMessage(Osd_Message)

        Return 0
    End Function

    <ExportAPI("-about")>
    Public Function About() As Integer
        Return CLI.About
    End Function

    Private Sub __display(data As Byte())
        Call _controller.Execute(Unicode.GetString(data))
    End Sub

    Public Sub Start(ServiceId As String)
        Dim Cycle As Integer = 0

        DaemonProcess._serviceSocket = New MMFSocket(ServiceId, AddressOf __display)

        Call $"Notify-OSD services start!".__DEBUG_ECHO

        Do While _continuesThread
            Call Threading.Thread.Sleep(100)

            If Cycle > 10 * 60 * 2 Then
                Cycle = 0
                Call FlushMemory()
            Else
                Cycle += 1
            End If
        Loop

        Call DaemonProcess._serviceSocket.Free
    End Sub
End Module
