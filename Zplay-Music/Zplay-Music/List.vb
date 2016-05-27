Imports libZPlay.App
Imports ZplayMusic
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.ComponentModel.DataStructures

Public Class List : Implements IEnumerable(Of ListItem)

    Public Event ChangePlayback(file As String, index As Integer)

    Public Sub Add(file As MediaFile, i As Integer)
        Dim item As New ListItem(file) With {
            .Size = ListItem.sz,
            .Index = i
        }
        Call FlowLayoutPanel1.Controls.Add(item)
        Call ToolTip1.SetToolTip(item, file.FileName)

        AddHandler item.Click, AddressOf __click
    End Sub

    Public Sub AddList(list As Playlist)
        Dim i As New Pointer

        For Each file In list
            Call Add(file, ++i)
        Next
    End Sub

    Public Sub Clear()
        For Each item In Me
            Call FlowLayoutPanel1.Controls.Remove(item)
        Next
    End Sub

    Dim _nowPlaying As ListItem

    Private Sub __setNowPlaying(item As ListItem)
        If Not _nowPlaying Is Nothing Then
            _nowPlaying.IsNowPlaying = False
        End If
        _nowPlaying = item
        _nowPlaying.IsNowPlaying = True
    End Sub

    Private Sub __click(sender As Object, args As EventArgs)
        Dim item As ListItem = DirectCast(sender, ListItem)
        Call __setNowPlaying(item)
        RaiseEvent ChangePlayback(item.MediaTag.FileName, item.Index)
    End Sub

    Public Iterator Function GetEnumerator() As IEnumerator(Of ListItem) Implements IEnumerable(Of ListItem).GetEnumerator
        For Each ctrl In Me.FlowLayoutPanel1.Controls
            If TypeOf ctrl Is ListItem Then
                Yield DirectCast(ctrl, ListItem)
            End If
        Next
    End Function

    Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Yield GetEnumerator()
    End Function
End Class

Public Class ListItem : Inherits PictureBox

    Dim normal As Image
    Dim nowPlaying As Image
    Dim highlight As Image

    Dim _nowPlaying As Boolean = False

    Public Property IsNowPlaying As Boolean
        Get
            Return _nowPlaying
        End Get
        Set(value As Boolean)
            _nowPlaying = value

            If _nowPlaying Then
                BackgroundImage = nowPlaying
            Else
                BackgroundImage = normal
            End If
        End Set
    End Property

    Public Shared ReadOnly highlightFore As Color = Color.FromArgb(16, 165, 45)
    Public Shared ReadOnly sz As New Size(515, 23)

    Shared ReadOnly t As Integer = 3
    Shared ReadOnly a As Integer = 290

    Public ReadOnly Property MediaTag As MediaFile
    Public Property Index As Integer

    Shared ReadOnly tl As Integer = 50
    Shared ReadOnly al As Integer = 30

    Sub New(file As MediaFile)
        Dim font As New Font(FontFace.MicrosoftYaHei, 8)
        Dim size As SizeF
        Dim h As Integer
        Dim br As SolidBrush = Brushes.White
        Dim rw As Integer
        Dim l As Integer

        Dim ts As String = file.Id3v2.Title
        Dim [as] As String = file.Id3v2.Artist

        If Len(ts) > tl Then
            ts = Mid(ts, 1, tl) & "..."
        End If
        If Len([as]) > al Then
            [as] = Mid([as], 1, al) & "..."
        End If

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.FromArgb(27, 27, 27))
            size = g.Gr_Device.MeasureString("00:00", font)
            h = (sz.Height - size.Height) / 2
            rw = sz.Width - size.Width - 3
            l = rw
            g.Gr_Device.DrawString(ts, font, br, New Point(t, h))
            g.Gr_Device.DrawString([as], font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            normal = g.ImageResource
        End Using

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.Black)
            g.Gr_Device.DrawString(ts, font, br, New Point(t, h))
            g.Gr_Device.DrawString([as], font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            highlight = g.ImageResource
        End Using

        br = New SolidBrush(highlightFore)

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.FromArgb(27, 27, 27))
            g.Gr_Device.DrawString(ts, font, br, New Point(t, h))
            g.Gr_Device.DrawString([as], font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            nowPlaying = g.ImageResource
        End Using

        BackgroundImage = normal
        MediaTag = file
    End Sub

    Private Sub ListItem_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        BackgroundImage = highlight
    End Sub

    Private Sub ListItem_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If IsNowPlaying Then
            BackgroundImage = nowPlaying
        Else
            BackgroundImage = normal
        End If
    End Sub
End Class