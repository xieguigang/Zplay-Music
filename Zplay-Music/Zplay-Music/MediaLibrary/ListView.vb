Imports libZPlay.App
Imports Microsoft.VisualBasic.Imaging
Imports Zplay.MediaLibrary

Public Class ListView

    Dim __back As Action
    Dim _list As Album

    Public host As FormZplay
    Dim background As Image

    Public Sub ShowData(album As Album, back As Action)
        Label1.Text = album.Name
        PictureBox1.BackgroundImage = CType(album.GetViewImage.Clone, Image)
        PictureBox2.BackgroundImage = album.GetViewImage

        __back = back
        _list = album
        background = PictureBox1.BackgroundImage

        Call Me.FlowLayoutPanel1.Controls.Clear()

        For Each x In album.list
            Dim song As New SongView

            Call Me.FlowLayoutPanel1.Controls.Add(song)
            Call song.SetInfo(x)
        Next

        Call PictureBox1_SizeChanged(Nothing, Nothing)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
        Call __back()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Call host.ChangePlaylist(New Playlist(_list.list, AddressOf host.__EOList), False)
        Call host.PlaybackNext()
    End Sub

    Private Sub PictureBox1_SizeChanged(sender As Object, e As EventArgs) Handles PictureBox1.SizeChanged
        If background Is Nothing Then
            Return
        End If

        Dim sz = PictureBox1.Size
        Dim max As Integer = {sz.Width, sz.Height}.Max

        Using gr As GDIPlusDeviceHandle = New Size(max, max).CreateGDIDevice
            Call gr.Graphics.DrawImage(background, 0, 0, max, max)
            PictureBox1.BackgroundImage = gr.ImageResource
        End Using
    End Sub
End Class

Public Class SongView : Inherits UserControl

    Dim WithEvents title As Label

    Public Sub SetInfo(song As MediaFile)
        title.Text = song.Id3v2.Title
    End Sub

    Private Sub SongView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Size = New Size(Parent.Width, 20)
        Controls.Add(title)
        title = New Label
        title.Location = New Point(10, 5)
        title.ForeColor = Color.Black
    End Sub

    Private Sub SongView_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, title.MouseEnter
        BackColor = Color.LawnGreen
    End Sub

    Private Sub SongView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, title.MouseLeave
        BackColor = Color.White
    End Sub
End Class