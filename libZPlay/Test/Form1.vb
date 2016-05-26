Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Serialization

Public Class Form1
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Dim play As New libZPlay.App.MediaPlayer

        Call play.Playback("E:\116. 宇多田光Beautiful World.flac")

        Me.BackgroundImage = play.AlbumArt

        ticks = play.Playback()
        '  play.SeeksByPercent(0.96)
    End Sub

    Private Sub ticks_Tick(sender As MediaPlayer, cur As TStreamTime, p As Double) Handles ticks.Tick
        Try
            Call Me.Invoke(Sub() Text = $"[{Math.Round(p * 100, 2)}%] " & cur.GetJson)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ticks_StateValidate(sender As MediaPlayer, stat As TStreamStatus) Handles ticks.StateValidate
        MsgBox(stat.GetJson)
    End Sub

    Private Sub ticks_EndOfTrack(sender As MediaPlayer) Handles ticks.EndOfTrack
        MsgBox(sender.status.GetJson)
    End Sub

    Dim WithEvents ticks As TickEvent
End Class
