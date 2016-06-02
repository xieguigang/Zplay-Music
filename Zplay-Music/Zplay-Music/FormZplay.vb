Imports System.ComponentModel
Imports System.Threading
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Browser = System.Diagnostics.Process
Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic
Imports Microsoft.Windows.Shell
Imports Microsoft.Windows.Taskbar
Imports Microsoft.Windows.Dialogs
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Windows.Forms
Imports Microsoft.VisualBasic.Imaging
Imports Zplay.MediaLibrary
Imports Microsoft.VisualBasic.Parallel.Tasks

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
        Call FormZplay_SizeChanged(Nothing, Nothing)
    End Sub

    Private Sub FormZplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim d As Integer = 5
        Dim sz = picAlbumArt.Size
        sz = New Size(sz.Width + d * 2, sz.Height + d * 2)
        TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(
            Handle,
            New Rectangle(New Point(10 - d, 517 - d), sz))
    End Sub

    Dim WithEvents _progress As ProgressIndicator
    Dim _osd As New Notify

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
        picAlbumArt.BackgroundImage = My.Resources._default
        picAlbumArt.BackColor = Color.Black

        Dim config As Config = Config.Load

        If config.lastPlaylist.Name.FileExists OrElse
            config.lastPlaylist.Name.DirectoryExists Then

            Call ChangePlaylist(config.GetList(AddressOf __EOList))
            Call ChangePlayback(list.ReadNext?.FileName, True)
        End If

        Select Case config.playbackMode
            Case PlaybackModes.Order
                OrderToolStripMenuItem.Checked = True
            Case PlaybackModes.Shuffle
                ShuffleToolStripMenuItem.Checked = True
            Case PlaybackModes.LoopList
                LoopsToolStripMenuItem.Checked = True
        End Select
    End Sub

    Dim list As Playlist

    Private Sub __EOList()
        Call play.Stop()

        If list.Count = 0 Then
            Call _osd.ShowMessage("Media files not found!")
        Else
            Call list.Reset()
        End If
    End Sub

    Public Sub ChangePlaylist(list As Playlist)
        Me.list = list
        Me.PictureBox1.BackgroundImage = list.DrawListCount

        Using config As Config = Config.Load
            config.lastPlaylist = New NamedValue(Of ListTypes)(list.URI, list.Type)
            list.Mode = config.playbackMode
        End Using

        Call List1.Clear()
        Call List1.AddList(list)

        Dim task = Function() As Boolean
                       Using engine As New Engine(Config.MediaLibrary)
                           Dim msg As New NotifyOsd.Message With {
                               .Title = "Zplay-Music Imports",
                               .Message = "Imports from " & list.URI,
                               .IconURL = _osd.AppIcon,
                               .BubbleBehavior = NotifyOsd.BubbleBehaviors.ProgressIndicator
                           }
                           Dim proBar As New NotifyOsd.Framework.Balloon.ProgressBar(msg, New Point)
                           Call proBar.Show()

                           proBar.ProgressBar.AnimationSpeed = 995

                           Dim setProgress = Sub(i As Integer)
                                                 proBar.PercentageValue = i
                                             End Sub
                           Call engine.AddFiles(list, setProgress)
                       End Using

                       Call Thread.Sleep(3 * 1000)
                       Call _osd.ShowNotify(play.ID3v2, picAlbumArt.BackgroundImage)

                       Return True
                   End Function

        Call _importsTask.Enqueue(task)
    End Sub

    Dim _importsTask As New TaskQueue(Of Boolean)

    Public Sub ChangePlayback(file As String, mute As Boolean)
        If file Is Nothing Then
            Return
        End If

        Call play.Playback(file)

        picAlbumArt.BackgroundImage = play.AlbumArt
        lbTime.Text = play.StreamInfo.Length.FormatTime
        lbArtist.Text = play.ID3v2.Artist
        lbTitle.Text = play.ID3v2.Title

        ticks = play.Playback()

        _progress.Length = play.StreamInfo.Length.ms
        List1.SetNowplaying(list.IndexOf(file))
        PlaybackControl1.Paused = False

        If play.AlbumArt.IsNullOrNothing Then
            picAlbumArt.BackgroundImage = My.Resources._default
        End If

        If Not mute Then
            Call _osd.ShowNotify(play.ID3v2, picAlbumArt.BackgroundImage)
        End If
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
        Call _osd.Dispose()
        Call _importsTask.Dispose()
    End Sub

    Private Sub ticks_Tick(sender As libZPlay.App.ZplayMusic, cur As TStreamTime, progress As Double) Handles ticks.Tick
        Call Me.Invoke(Sub() _progress.Time = cur)
    End Sub

    Private Sub ticks_StateValidate(sender As libZPlay.App.ZplayMusic, stat As TStreamStatus) Handles ticks.StateValidate
        Call Me.Invoke(Sub() PlaybackControl1.Paused = stat.fPause)
    End Sub

    Private Sub ticks_EndOfTrack(sender As libZPlay.App.ZplayMusic) Handles ticks.EndOfTrack
        Call Me.Invoke(Sub() Call buttonNext_Click(Nothing, Nothing))
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

        Me.Panel2.BackColor = Color.Black
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Call Me.Close()
    End Sub

    Private Sub NextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NextToolStripMenuItem.Click
        Call PlaybackControl1_PlaybackNext()
    End Sub

    Private Sub PlaypauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaypauseToolStripMenuItem.Click
        Call PlaybackControl1_PlaybackPlay()
    End Sub

    Private Sub PreviousToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviousToolStripMenuItem.Click
        Call PlaybackControl1_PlaybackPrevious()
    End Sub

    Private Sub buttonNext_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonNext.Click
        Call PlaybackControl1_PlaybackNext()
    End Sub

    Private Sub buttonPrevious_Click(sender As Object, e As ThumbnailButtonClickedEventArgs) Handles buttonPrevious.Click
        Call PlaybackControl1_PlaybackPrevious()
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
                Call ChangePlayback(list.ReadNext?.FileName, True)
            End If
        End Using
    End Sub

    Private Sub List1_ChangePlayback(file As String, index As Integer) Handles List1.ChangePlayback
        Call ChangePlayback(file, False)
        Call list.SetCurrentRead(index)
    End Sub

    Private Sub picAlbumArt_Click(sender As Object, e As EventArgs) Handles picAlbumArt.Click
        Dim tmp As String = App.GetAppSysTempFile(".png")
        Call picAlbumArt.BackgroundImage.SaveAs(tmp, ImageFormats.Png)
        Call Browser.Start(tmp)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Call List1.Clear()
    End Sub

    Private Sub PlaybackControl1_PlaybackNext() Handles PlaybackControl1.PlaybackNext
        Dim file As String = list.ReadNext?.FileName

        If file.FileExists Then
            Call ChangePlayback(file, False)
        End If
    End Sub

    Private Sub PlaybackControl1_PlaybackPlay() Handles PlaybackControl1.PlaybackPlay
        If ticks.StopStatus Then
            Call play.Playback()
        ElseIf play.status.fPause Then
            Call play.Resume()
        Else
            Call play.Pause()
        End If
    End Sub

    Private Sub PlaybackControl1_PlaybackPrevious() Handles PlaybackControl1.PlaybackPrevious
        Dim file As String = list.ReadPrevious.FileName

        If file.FileExists Then
            Call ChangePlayback(file, False)
        End If
    End Sub

    Private Sub OpenLibraryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenLibraryToolStripMenuItem.Click
        Call New FormLibrary().Show()
    End Sub

    Private Sub OrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderToolStripMenuItem.Click
        ShuffleToolStripMenuItem.Checked = False
        LoopsToolStripMenuItem.Checked = False
        Call __setMode(PlaybackModes.Order)
    End Sub

    Private Sub ShuffleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShuffleToolStripMenuItem.Click
        OrderToolStripMenuItem.Checked = False
        LoopsToolStripMenuItem.Checked = False
        Call __setMode(PlaybackModes.Shuffle)
    End Sub

    Private Sub LoopsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopsToolStripMenuItem.Click
        OrderToolStripMenuItem.Checked = False
        ShuffleToolStripMenuItem.Checked = False
        Call __setMode(PlaybackModes.LoopList)
    End Sub

    Private Sub __setMode(mode As PlaybackModes)
        If Not list Is Nothing Then
            list.Mode = mode
        End If

        Using cfg As Config = Config.Load
            cfg.playbackMode = mode
        End Using
    End Sub
End Class
