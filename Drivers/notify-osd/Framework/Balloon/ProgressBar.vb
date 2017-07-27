Imports Microsoft.VisualBasic.Parallel
Imports NotifyOsd.BubblesDisplay

Namespace Framework.Balloon

    Public Class ProgressBar : Implements IDisposable

        Public Property PercentageValue As Integer
            Get
                Return Me._procBubble.Value
            End Get
            Set(value As Integer)
                Me._procBubble.Value = value
            End Set
        End Property

        Public ReadOnly Property ProgressBar As ProcessingBar
            Get
                Return Me._procBubble.ProcessingBar
            End Get
        End Property

        Public Sub Cancel()
            Call Me._procBubble.Cancel()
        End Sub

        Dim _invokeThread As Threading.Thread

        Public Sub ShowMessage(Percentage As Integer, MSG As Message)
            Me._procBubble.Value = Percentage
            Me._procBubble.Message = MSG
        End Sub

        Public Sub ShowMessage(Percentage As Integer, MSG As String)
            Me._procBubble.Value = Percentage
            Me._procBubble.Message = New Message With {
                .Message = MSG,
                .Title = Me._procBubble.Message.Title,
                .BubbleBehavior = Me._procBubble.Message.BubbleBehavior,
                .CallbackHandle = Me._procBubble.Message.CallbackHandle,
                .IconURL = Me._procBubble.Message.IconURL,
                .SoundURL = Me._procBubble.Message.SoundURL
            }
        End Sub

        Public Sub Show()
            If _invokeThread Is Nothing Then
                _invokeThread = RunTask(AddressOf Me._procBubble.ShowDialog)
            End If
        End Sub

        Dim _procBubble As FormOsdProgressIndicator

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="msg"></param>
        ''' <param name="screenOffset">气泡在屏幕上面的偏移的位置</param>
        Sub New(msg As Message, screenOffset As Point)
            Me._procBubble = New FormOsdProgressIndicator()
            Me._procBubble.ScreenOffSet = screenOffset
            Me._procBubble.Message = msg
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    Call Me._procBubble.Dispose()
                    Call Me._invokeThread.Abort()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace