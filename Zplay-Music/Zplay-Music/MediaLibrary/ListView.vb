Imports libZPlay.App
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.SecurityString
Imports Microsoft.VisualBasic.Serialization
Imports Zplay.MediaLibrary

Public Class ListView : Implements IDisposable

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

        Dim i As Integer = 0

        For Each x In album.list
            Dim song As New SongView

            Call Me.FlowLayoutPanel1.Controls.Add(song)
            Call song.SetInfo(x, i)
            i += 1

            AddHandler song.ChangePlayback, Sub(file, index)
                                                Call __changePlaybacks()
                                                Call host.ChangePlayback(file, index)
                                            End Sub
        Next

        Call PictureBox1_SizeChanged(Nothing, Nothing)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Call __back()
    End Sub

    Private Function __changePlaybacks() As Boolean
        Dim list As New Playlist(_list.list, AddressOf host.__EOList)
        Dim md5 As String = list.ToArray.GetXml.GetMd5Hash

        list.Extension = New ExtendedProps
        list.Extension.DynamicHash.Properties.Add(NameOf(md5), md5)

        If host.list Is Nothing OrElse host.list.Extension Is Nothing Then
            Call host.ChangePlaylist(list, False)
            Return True
        Else
            Dim m As String = Scripting.ToString(
                host.list.Extension.DynamicHash.Properties.TryGetValue(NameOf(md5)))

            If Not md5.TextEquals(m) Then
                Call host.ChangePlaylist(list, False)
                Return True
            End If
        End If

        Return False
    End Function

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If __changePlaybacks() Then
            Call host.PlaybackNext()
        End If
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

        Dim backg = GDIPlusExtensions.ImageCrop(PictureBox1.BackgroundImage, btnBack.Location, btnBack.Size)
        Using gr As Graphics = Graphics.FromImage(backg)
            Call gr.DrawImage(My.Resources.arrow_back_512, 0, 0, btnBack.Width, btnBack.Height)
        End Using

        btnBack.BackgroundImage = backg
    End Sub

    Private Sub ListView_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        On Error Resume Next

        background.Dispose()
        btnBack.Dispose()
        PictureBox1.Dispose()

        For Each item As Control In FlowLayoutPanel1.Controls
            Call item.Dispose()
        Next
    End Sub
End Class

Public Class SongView : Inherits UserControl

    Dim WithEvents title As Label

    Public Event ChangePlayback(path As String, index As Integer)


    Dim file As String, index As Integer

    Public Sub SetInfo(song As MediaFile, i As Integer)
        title.Text = song.Id3v2.Title
        file = song.FileName
        index = i
    End Sub

    Private Sub SongView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Size = New Size(Parent.Width, 22)

        title = New Label
        title.Location = New Point(10, 0)
        title.ForeColor = Color.Black
        title.Width = Width

        Controls.Add(title)
    End Sub

    Private Sub SongView_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, title.MouseEnter
        BackColor = Color.LightSkyBlue
    End Sub

    Private Sub SongView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, title.MouseLeave
        BackColor = Color.White
    End Sub

    Private Sub SongView_Click(sender As Object, e As EventArgs) Handles Me.Click, title.Click
        RaiseEvent ChangePlayback(file, index)
    End Sub
End Class