Imports libZPlay.App

Public Class Playlist

    Dim _files As Dictionary(Of String, MediaFile)

    Sub New(files As IEnumerator(Of String))

    End Sub

    Public Function ReadNext() As String

    End Function
End Class