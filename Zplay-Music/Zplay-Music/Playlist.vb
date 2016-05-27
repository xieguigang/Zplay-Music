Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures

Public Class Playlist

    Dim _files As List(Of MediaFile)
    Dim p As Integer = -1
    Dim _eolist As Action

    Sub New(EOList As Action)
        _eolist = EOList
    End Sub

    Sub New(files As IEnumerator(Of String))
        _files = GetFilesInfo(files).ToList
    End Sub

    Public Function ReadNext() As String
        p += 1

        If p = _files.Count Then
            Call _eolist()
        Else
            Return _files(p).FileName
        End If

        Return Nothing
    End Function

    Public Function ReadPrevious() As String
        p -= 1
        Return _files(p).FileName
    End Function

    Public Function Reset() As String
        p = -1
        Return ReadNext()
    End Function
End Class