Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Parallel.Tasks

Namespace App

    Public Class TickEvent : Implements IDisposable

        ReadOnly __player As ZplayMusic
        ReadOnly _timer As New UpdateThread(PerSecond, AddressOf __triggerEvent)

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="cur"></param>
        ''' <param name="progress">进度条的百分比</param>
        Public Event Tick(sender As ZplayMusic, cur As TStreamTime, progress As Double)
        Public Event StateValidate(sender As ZplayMusic, stat As TStreamStatus)
        Public Event EndOfTrack(sender As ZplayMusic)

        Public ReadOnly Property StreamInfo As TStreamInfo
            Get
                Return __player.StreamInfo
            End Get
        End Property

        Sub New(api As ZplayMusic)
            __player = api
            _timer.Start()
        End Sub

        Const PerSecond As Integer = 1000

        Private Sub __triggerEvent()
            Dim cur As TStreamTime = __player.CurrentPosition
            Dim progress As Double = cur.ms / __player.StreamInfo.Length.ms

            If __stopState(cur) Then
                _timer.Stop()
                RaiseEvent EndOfTrack(__player)
            End If

            RaiseEvent Tick(__player, cur, progress)
        End Sub

        Private Function __stopState(cur As TStreamTime) As Boolean
            If cur.ms <> 0 Then
                Return False
            Else
                Dim st As TStreamStatus = __player.status
                Return st.fPlay = False AndAlso st.fPause = False
            End If
        End Function

        Public Function StopStatus() As Boolean
            Return __stopState(__player.CurrentPosition)
        End Function

        Friend Sub ValidStatus()
            RaiseEvent StateValidate(__player, __player.status)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    Call _timer.Stop()
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