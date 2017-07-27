
''' <summary>
''' 编程的API接口
''' </summary>
Public Class Services : Implements System.IDisposable

    Dim Socket As Microsoft.VisualBasic.MMFProtocol.MMFSocket

    Public ReadOnly Property ServicesSocket As MMFProtocol.MMFSocket
        Get
            Return New MMFProtocol.MMFSocket(Socket.URI)
        End Get
    End Property

    Sub New(uri As String)
        Me.Socket = New MMFProtocol.MMFSocket(uri)
    End Sub

    Public Shared Function Start(serviceName As String, Optional ProcessExe As String = "./notify_osd.exe", Optional WelcomeMessage As String = "") As Services
        Dim i = Process.Start(FileIO.FileSystem.GetFileInfo(ProcessExe).FullName, $"-start ""{serviceName}""").Id

        Call Threading.Thread.Sleep(2500)

        Dim Services As Services = New Services(serviceName)

        If Not String.IsNullOrEmpty(WelcomeMessage) Then
            Call Services.SendMessage("Windows Notify Osd Services", WelcomeMessage)
        End If

        Return Services
    End Function

    Public Overridable Sub [Stop]()
        Call Socket.SendMessage("-stop")
        Call Socket.Free
    End Sub

    Public Overridable Sub SendMessage(Title As String, Message As String, Optional IconUrl As String = "")
        Call Socket.SendMessage(String.Format("-sendmessage -title ""{0}"" -message ""{1}"" -icon ""{2}""", Title, Message, IconUrl))
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Services.Notify_osd://{0}", Socket.URI)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Call [Stop]()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
