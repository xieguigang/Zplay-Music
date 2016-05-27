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
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Windows.Forms

Public Class FormZplay

    Dim __playListInvoke As PlayListAnimation
    Dim __formInvoke As FormAnimation

    Friend play As New libZPlay.App.ZplayMusic
    Dim WithEvents ticks As TickEvent

    Dim WithEvents buttonPrevious As ThumbnailToolBarButton
    Dim WithEvents buttonNext As ThumbnailToolBarButton
    Dim WithEvents buttonPause As ThumbnailToolBarButton

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ToolStripManager.Renderer = New ChromeUIRender
    End Sub

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

    Friend _toolTips As PictureBox
    Friend _playList As List

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(0, My.Computer.Screen.WorkingArea.Height - 10 - Height)
        __playListInvoke = New PlayListAnimation(Me)
        __formInvoke = New FormAnimation(Me, __playListInvoke)
        _progress = New ProgressIndicator With {.ParentMain = Me}
        Panel2.Controls.Add(_progress)
        _progress.Location = New Point(Scan0, Panel2.Height - 4)
        _toolTips = New PictureBox With {
            .BackgroundImage = My.Resources.tooltip,
            .Size = My.Resources.tooltip.Size,
            .Location = New Point(0, Panel2.Height - 8 - My.Resources.tooltip.Height),
            .Visible = False
        }
        Panel2.Controls.Add(_toolTips)
        picAlbumArt.SendToBack()
        _toolTips.BringToFront()
        _playList = New List
        Panel1.Controls.Add(_playList)
        _playList.Location = New Point(0, Panel1.Height - _playList.Height)

        Dim config As Config = Config.Load

        If config.lastplay.Name.FileExists OrElse
            config.lastplay.Name.DirectoryExists Then

            Call ChangePlaylist(config.GetList(AddressOf __EOList))
            Call ChangePlayback(list.ReadNext)
        End If
    End Sub

    Dim list As Playlist

    Private Sub __EOList()

    End Sub

    Public Sub ChangePlaylist(list As Playlist)
        Me.list = list
        Me.PictureBox1.BackgroundImage = list.DrawListCount

        Using config As Config = Config.Load
            config.lastplay = New NamedValue(Of ListTypes)(list.URI, list.Type)
        End Using
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
        Call ticks.Dispose()
        Call play.Dispose()
    End Sub

    Private Sub ticks_Tick(sender As libZPlay.App.ZplayMusic, cur As TStreamTime, progress As Double) Handles ticks.Tick
        Call Me.Invoke(Sub() _progress.Time = cur)
    End Sub

    Private Sub ticks_StateValidate(sender As libZPlay.App.ZplayMusic, stat As TStreamStatus) Handles ticks.StateValidate

    End Sub

    Private Sub ticks_EndOfTrack(sender As libZPlay.App.ZplayMusic) Handles ticks.EndOfTrack
        Call buttonNext_Click(Nothing, Nothing)
    End Sub

    Private Sub buttonPause_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonPause.Click
        Call PlaypauseToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub picDocker_Click(sender As Object, e As EventArgs) Handles picDocker.Click
        If __formInvoke.IsOpen Then
            Call __playListInvoke.Close()
            Call __formInvoke.Close()
        Else
            Call __formInvoke.Open()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Call Me.Close()
    End Sub

    Private Sub NextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NextToolStripMenuItem.Click

    End Sub

    Private Sub PlaypauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaypauseToolStripMenuItem.Click
        If ticks.StopStatus Then
            Call play.Playback()
        ElseIf play.status.fPause Then
            Call play.Resume()
        Else
            Call play.Pause()
        End If
    End Sub

    Private Sub PreviousToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviousToolStripMenuItem.Click

    End Sub

    Private Sub buttonNext_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonNext.Click
        Dim file As String = list.ReadNext

        If file.FileExists Then
            Call ChangePlayback(file)
        End If
    End Sub

    Private Sub buttonPrevious_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonPrevious.Click
        Dim file As String = list.ReadPrevious

        If file.FileExists Then
            Call ChangePlayback(file)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Call New FormAboutZplay().ShowDialog()
    End Sub

    Private Sub OpenFolderToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenFolderToolStripMenuItem1.Click
        Using DIR As New FolderBrowserDialog With {.ShowNewFolderButton = False}
            If DIR.ShowDialog = DialogResult.OK Then
                Call ChangePlaylist(
                    New Playlist(
                    Playlist.GetFiles(DIR.SelectedPath, False),
                    AddressOf __EOList,
                    ListTypes.DIR,
                    DIR.SelectedPath))
                Call ChangePlayback(list.ReadNext)
            End If
        End Using
    End Sub
End Class
