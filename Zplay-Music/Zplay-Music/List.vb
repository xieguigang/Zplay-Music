Imports libZPlay.App
Imports ZplayMusic
Imports Microsoft.VisualBasic.Imaging

Public Class List
    Implements IEnumerable(Of ListItem)

    Public Sub Add(file As MediaFile)
        Dim item As New ListItem(file) With {
            .Size = ListItem.sz
        }
        Call FlowLayoutPanel1.Controls.Add(item)
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
    Public Shared ReadOnly sz As New Size(529, 23)

    Shared ReadOnly t As Integer = 5
    Shared ReadOnly a As Integer = 180
    Shared ReadOnly l As Integer = 230

    Sub New(file As MediaFile)
        Dim font As New Font(FontFace.MicrosoftYaHei, 8)
        Dim size As SizeF
        Dim h As Integer
        Dim br As SolidBrush = Brushes.White

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.FromArgb(27, 27, 27))
            size = g.Gr_Device.MeasureString(NameOf(ListItem), font)
            h = (sz.Height - size.Height) / 2
            g.Gr_Device.DrawString(file.Id3v2.Title, font, br, New Point(t, h))
            g.Gr_Device.DrawString(file.Id3v2.Artist, font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            normal = g.ImageResource
        End Using

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.Black)
            g.Gr_Device.DrawString(file.Id3v2.Title, font, br, New Point(t, h))
            g.Gr_Device.DrawString(file.Id3v2.Artist, font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            nowPlaying = g.ImageResource
        End Using

        br = New SolidBrush(highlightFore)

        Using g As GDIPlusDeviceHandle = sz.CreateGDIDevice(Color.FromArgb(27, 27, 27))
            g.Gr_Device.DrawString(file.Id3v2.Title, font, br, New Point(t, h))
            g.Gr_Device.DrawString(file.Id3v2.Artist, font, br, New Point(a, h))
            g.Gr_Device.DrawString(file.StreamInfo.Length.FormatTime, font, br, New Point(l, h))
            highlight = g.ImageResource
        End Using
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