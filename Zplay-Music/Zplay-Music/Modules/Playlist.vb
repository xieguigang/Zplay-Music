Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Imaging

''' <summary>
''' <see cref="IEnumerable(Of MediaFile)"/>
''' </summary>
Public Class Playlist : Inherits libZPlay.App.Playlist

    Sub New(files As IEnumerable(Of String), EOList As Action, type As ListTypes, URL As String)
        Call MyBase.New(files, EOList, type, URL)
    End Sub

    Sub New(files As IEnumerable(Of MediaFile), EOList As Action)
        Call MyBase.New(files, eol:=EOList)
    End Sub

    Public Function DrawListCount() As Image
        Dim s As String = _files.Count.ToString
        Dim font As New Font(FontFace.MicrosoftYaHei, 8)

        Using g As Graphics2D = My.Resources.Numbers.Size.CreateGDIDevice
            Dim sz = g.Graphics.MeasureString(s, font)
            Dim loc As New Point((g.Width - sz.Width) / 2, (g.Height - sz.Height) / 2)

            Call g.Graphics.DrawImageUnscaled(My.Resources.Numbers, New Point(-1, -1))
            Call g.Graphics.DrawString(s, font, Brushes.White, loc)

            Return g.ImageResource
        End Using
    End Function
End Class
