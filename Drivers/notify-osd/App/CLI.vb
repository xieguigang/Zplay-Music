Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Parallel.MMFProtocol

Module CLI

    Public ReadOnly Property ProcessLock As ProcessLock

    <ExportAPI("/start",
               Info:="Starts the notify-osd services on your desktop, if the services_id is already exists in the memory mappings file names, then this startup will be ignored",
               Usage:="/start <service_id>")>
    Public Function StartServices(args As CommandLine.CommandLine) As Integer
        Dim ServiceName As String = args.Parameters?.First
        _ProcessLock = New ProcessLock(ServiceName & "-process_lock")
        If Not ProcessLock.Locked Then
            Call DaemonProcess.Start(ServiceName)
        End If

        Return 0
    End Function

    <ExportAPI("/stop",
               Info:="Stop the notify-osd services, needs a url to deletes the memory mappings file.",
               Usage:="/stop <service_id>")>
    Public Function StopServices(args As CommandLine.CommandLine) As Integer
        Dim ServiceName As String = args.Parameters?.First
        Using Client As MMFSocket = New MMFSocket(ServiceName)
            Call Client.SendMessage("-stop")
        End Using

        Return 0
    End Function

    <ExportAPI("/send_message",
               Info:="Display a user message.",
               Usage:="/send_message -svr <service_id> -msg <messages>")>
    Public Function SendMessage(args As CommandLine.CommandLine) As Integer
        Dim svrName As String = args("-service")
        Dim msg As String = args("-message")
        Using invoke As MMFSocket = New MMFSocket(svrName)
            Call invoke.SendMessage(msg)
        End Using

        Return 0
    End Function

    <ExportAPI("--about", Info:="Show the about message of this application.")>
    Public Function About() As Integer
        Dim aboutMsg As Message = New Message With {
            .Title = "notify-osd application service",
            .Message = $"Design by: xieguigang(xie.guigang@gmail.com)\nThis is a notify on screen dispalying service works like the 'notify-osd' on ubuntu linux.\n\n\n{DaemonProcess.Manual}"
        }
        Call DaemonProcess.SendMessage(aboutMsg)

        Return 0
    End Function
End Module
