Imports libZPlay.App

Public Class LibraryCard

    Dim normal As Image
    Dim highlight As Image

    Public Sub SetInfo(info As MediaFile)
        normal = Zplay.MediaLibrary.ArtVisual.DrawNormal(info)
        highlight = Zplay.MediaLibrary.ArtVisual.DrawHighlight(info)
        BackgroundImage = normal
        Size = normal.Size
    End Sub

    Private Sub LibraryCard_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        BackgroundImage = highlight
    End Sub

    Private Sub LibraryCard_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        BackgroundImage = normal
    End Sub
End Class
