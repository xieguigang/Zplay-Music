Imports libZPlay.App
Imports Zplay.MediaLibrary

Public Class ListView

    Dim __back As Action
    Dim _list As Album

    Public host As FormZplay

    Public Sub ShowData(album As Album, back As Action)
        Label1.Text = album.Name
        PictureBox1.BackgroundImage = album.GetViewImage
        PictureBox2.BackgroundImage = album.GetViewImage

        __back = back
        _list = album

        Call Me.FlowLayoutPanel1.Controls.Clear()

        For Each x In album.list
            Dim song As New SongView

            Call Me.FlowLayoutPanel1.Controls.Add(song)
            Call song.SetInfo(x)
        Next
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Call __back()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Call host.ChangePlaylist(New Playlist(_list.list, AddressOf host.__EOList), False)
        Call host.PlaybackNext()
    End Sub
End Class

Public Class SongView : Inherits UserControl

    Dim title As New Label

    Public Sub SetInfo(song As MediaFile)
        title.Text = song.Id3v2.Title
    End Sub

    Private Sub SongView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Size = New Size(Parent.Width, 20)
        Controls.Add(title)
        title.Location = New Point(10, 5)
        title.ForeColor = Color.Black
    End Sub

    Private Sub SongView_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        BackColor = Color.LawnGreen
    End Sub

    Private Sub SongView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        BackColor = Color.White
    End Sub
End Class