Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Imaging

Namespace App

    Public Enum ListTypes
        M3u
        Cue
        DIR
    End Enum

    Public Class Playlist : Implements IEnumerable(Of MediaFile)

        Protected _files As List(Of MediaFile)
        Dim p As Integer = -1
        Dim _eolist As Action

        Public ReadOnly Property Type As ListTypes
        Public ReadOnly Property URI As String

        Public ReadOnly Property Index As Integer
            Get
                Return p
            End Get
        End Property

        Public Function IndexOf(file As String) As Integer
            Dim i As Integer = 0

            For Each mediaFile In _files
                If file.GetFullPath.TextEquals(mediaFile.FileName.GetFullPath) Then
                    Return i
                Else
                    i += 1
                End If
            Next

            Return -1
        End Function

        Public Sub SetCurrentRead(index As Integer)
            p = index
        End Sub

        Sub New(files As IEnumerable(Of String), EOList As Action, type As ListTypes, URL As String)
            _files = GetFilesInfo(files).ToList
            _eolist = EOList
            _Type = type
            _URI = URL
        End Sub

        Public Sub Clear()
            _files.Clear()
            p = -1
        End Sub

        Public Function ReadNext() As String
            p += 1

            If p >= _files.Count Then
                Call _eolist()
            Else
                Return _files(p).FileName
            End If

            Return Nothing
        End Function

        Public Function ReadPrevious() As String
            p -= 1

            If p = -1 Then
                p = 0
            End If

            Return _files(p).FileName
        End Function

        Public Function Reset() As String
            p = -1
            Return ReadNext()
        End Function

        Shared ReadOnly __fileTypes As String() = {"*.mp3", "*.wav", "*.flac", "*.ape", "*.ogg"}

        Public Shared Function GetFiles(DIR As String, recurve As Boolean) As IEnumerable(Of String)
            If recurve Then
                Return ls - l - r - wildcards(__fileTypes) <= DIR
            Else
                Return ls - l - wildcards(__fileTypes) <= DIR
            End If
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of MediaFile) Implements IEnumerable(Of MediaFile).GetEnumerator
            For Each x In _files
                Yield x
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace