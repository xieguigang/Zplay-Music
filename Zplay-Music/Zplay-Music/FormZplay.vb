Imports System.ComponentModel
Imports System.Threading
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic
Imports Microsoft.Windows.Shell
Imports Microsoft.Windows.Taskbar
Imports Microsoft.Windows.Dialogs

Public Class FormZplay

    Dim __playListInvoke As PlayListAnimation
    Dim __formInvoke As FormAnimation

    Dim play As New libZPlay.App.ZplayMusic
    Dim WithEvents ticks As TickEvent

    Dim WithEvents buttonPrevious As ThumbnailToolBarButton
    Dim WithEvents buttonNext As ThumbnailToolBarButton
    Dim WithEvents buttonPause As ThumbnailToolBarButton

    Private Sub FormZplay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        buttonPrevious = New ThumbnailToolBarButton(My.Resources.start, "Previous")
        buttonNext = New ThumbnailToolBarButton(My.Resources._end, "Next")
        buttonPause = New ThumbnailToolBarButton(My.Resources.pause, "play/pause")

        TaskbarManager.Instance.ThumbnailToolBars.AddButtons(
            Handle,
            buttonPrevious,
            buttonPause,
            buttonNext)
        TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(
            Handle,
            New Rectangle(New Point(10, 517),
                          picAlbumArt.Size))
    End Sub

    Private Sub FormZplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(
            Handle,
            New Rectangle(New Point(10, 517),
                          picAlbumArt.Size))
    End Sub

    Dim WithEvents _progress As ProgressIndicator

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(0, My.Computer.Screen.WorkingArea.Height - 10 - Height)
        __playListInvoke = New PlayListAnimation(Me)
        __formInvoke = New FormAnimation(Me, __playListInvoke)
        _progress = New ProgressIndicator
        Panel2.Controls.Add(_progress)
        _progress.Location = New Point(Scan0, Panel2.Height - 4)

        Call ChangePlayback("E:\日漫\01. STYX HELIX.mp3")

    End Sub

    Public Sub ChangePlayback(file As String)
        Call play.Playback(file)

        picAlbumArt.BackgroundImage = play.AlbumArt
        lbTime.Text = play.StreamInfo.Length.FormatTime
        lbArtist.Text = play.ID3v2.Artist
        lbTitle.Text = play.ID3v2.Title

        ticks = play.Playback()

        _progress.Length = play.StreamInfo.Length.ms
    End Sub

    Private Sub btnCloselist_Click(sender As Object, e As EventArgs) Handles btnCloselist.Click
        Call __playListInvoke.Close()
    End Sub

    Private Sub btnPlayList_Click(sender As Object, e As EventArgs) Handles btnPlayList.Click
        If __playListInvoke.IsOpen Then
            Call __playListInvoke.Close()
        Else
            Call __playListInvoke.Open()
        End If
    End Sub

    Private Sub FormZplay_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Call play.Dispose()
    End Sub

    Private Sub ticks_Tick(sender As libZPlay.App.ZplayMusic, cur As TStreamTime, progress As Double) Handles ticks.Tick
        Call Me.Invoke(Sub() _progress.Time = cur)
    End Sub

    Private Sub ticks_StateValidate(sender As libZPlay.App.ZplayMusic, stat As TStreamStatus) Handles ticks.StateValidate

    End Sub

    Private Sub ticks_EndOfTrack(sender As libZPlay.App.ZplayMusic) Handles ticks.EndOfTrack

    End Sub

    Private Sub buttonPause_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonPause.Click
        If ticks.StopStatus Then
            Call play.Playback()
        ElseIf play.status.fPause Then
            Call play.Resume()
        Else
            Call play.Pause()
        End If
    End Sub

    Private Sub picDocker_Click(sender As Object, e As EventArgs) Handles picDocker.Click
        If __formInvoke.IsOpen Then
            Call __playListInvoke.Close()
            Call __formInvoke.Close()
        Else
            Call __formInvoke.Open()
        End If
    End Sub
End Class
