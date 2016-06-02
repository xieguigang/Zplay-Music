Imports libZPlay.App
Imports Zplay.MediaLibrary

Public Class FormLibrary

    Private Sub FormLibrary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using engine As New Engine(Config.MediaLibrary)
            Dim files = engine.FetchAll

            For Each file As MediaFile In files
                Dim card As New LibraryCard
                Call card.SetInfo(file)
                Call Me.FlowLayoutPanel1.Controls.Add(card)
            Next
        End Using
    End Sub
End Class