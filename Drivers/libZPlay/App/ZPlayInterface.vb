Imports System.Drawing
Imports libZPlay.InternalTypes

Namespace App

    Public MustInherit Class ZPlayInterface

        Protected ReadOnly __api As ZPlay

        Public ReadOnly Property ZPlay As ZPlay
            Get
                Return __api
            End Get
        End Property

        Protected Sub New(api As ZPlay)
            __api = api
        End Sub

        Protected Sub New(libzplay As String)
            If Not String.IsNullOrEmpty(libzplay) Then

            End If

            __api = New ZPlay
        End Sub

        Public MustOverride ReadOnly Property CurrentPosition As TStreamTime
        Public MustOverride ReadOnly Property ID3v2 As TID3InfoEx

        Public Overridable ReadOnly Property AlbumArt As Bitmap
            Get
                Return ID3v2.Picture.Bitmap
            End Get
        End Property

        Protected _uri As String

        Public Property PlaybackURI As String
            Get
                Return _uri
            End Get
            Protected Set(value As String)
                _uri = value
            End Set
        End Property

        Public MustOverride ReadOnly Property StreamInfo As TStreamInfo

        Public ReadOnly Property status As TStreamStatus
            Get
                Dim s As New TStreamStatus
                Call __api.GetStatus(s)
                Return s
            End Get
        End Property

        Dim __validState As Action = AddressOf __null

        Private Shared Sub __null()
            ' DO NOTHING
        End Sub

        Private Function __initEvents() As TickEvent
            Dim args As New TickEvent(Me)
            __validState = AddressOf args.ValidStatus
            Return args
        End Function

        Public Overridable Function Playback() As TickEvent
            Call __api.StartPlayback()
            Return __initEvents()
        End Function

        Public Sub Pause()
            Call __api.PausePlayback()
            Call __validState()
        End Sub

        Public Sub [Resume]()
            Call __api.ResumePlayback()
            Call __validState()
        End Sub

        Public Sub [Stop]()
            Call __api.StopPlayback()
            Call __validState()
        End Sub

        ''' <summary>
        ''' hms
        ''' </summary>
        ''' <param name="time"></param>
        Public Sub SeeksByTime(time As TStreamTime)
            Call ZPlay.Seek(TTimeFormat.tfHMS, time, TSeekMethod.smFromBeginning)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="p">0 - 1</param>
        Public Overridable Sub SeeksByPercent(p As Double)
            Call ZPlay.Seek(TTimeFormat.tfMillisecond,
                            TimePercentage(p),
                            TSeekMethod.smFromBeginning)
        End Sub
    End Class
End Namespace