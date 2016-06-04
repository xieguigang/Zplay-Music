Imports System.Drawing
Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Language

Public Class Album : Inherits ClassObject

    Public Property Name As String
    Public Property list As List(Of MediaFile)

    Public Function GetViewImage() As Image
        For Each item In list
            If item.HaveAlbumArt Then
                Return item.Id3v2.Picture.Bitmap
            End If
        Next

        Return Nothing
    End Function
End Class
