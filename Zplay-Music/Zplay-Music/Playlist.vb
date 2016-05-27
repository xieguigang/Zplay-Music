﻿Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language.UnixBash

Public Class Playlist : Implements IEnumerable(Of MediaFile)

    Dim _files As List(Of MediaFile)
    Dim p As Integer = -1
    Dim _eolist As Action

    Sub New(files As IEnumerable(Of String), EOList As Action)
        _files = GetFilesInfo(files).ToList
        _eolist = EOList
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

        If p = -1 Then
            p = 0
        End If

        Return _files(p).FileName
    End Function

    Public Function Reset() As String
        p = -1
        Return ReadNext()
    End Function

    Public Shared Function GetFiles(DIR As String, recurve As Boolean) As IEnumerable(Of String)
        If recurve Then
            Return ls - l - r - wildcards("*.mp3", "*.wav", "*.flac", "*.ape") <= DIR
        Else
            Return ls - l - wildcards("*.mp3", "*.wav", "*.flac", "*.ape") <= DIR
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
