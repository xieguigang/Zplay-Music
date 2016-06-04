Imports libZPlay.App
Imports Zplay.MediaLibrary

Public Class FormLibrary

    Private Sub FormLibrary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using engine As New Engine(Config.MediaLibrary)
            Call engine.CreateAlbumViews(ViewAlbums)
        End Using
    End Sub
End Class