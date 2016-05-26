Imports System.ComponentModel
Imports System.Threading
Imports libZPlay.App

Public Class FormZplay

    Dim __playListInvoke As PlayListAnimation
    Dim __formInvoke As FormAnimation

    Dim play As New libZPlay.App.MediaPlayer
    Dim WithEvents ticks As TickEvent

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(0, My.Computer.Screen.WorkingArea.Height - 10 - Height)
        __playListInvoke = New PlayListAnimation(Me)
        __formInvoke = New FormAnimation(Me)

        Call play.Playback("E:\116. 宇多田光Beautiful World.flac")

        PictureBox1.BackgroundImage = play.AlbumArt

        ticks = play.Playback()
    End Sub

    Private Sub btnCloselist_Click(sender As Object, e As EventArgs) Handles btnCloselist.Click, PictureBox3.Click
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
End Class
