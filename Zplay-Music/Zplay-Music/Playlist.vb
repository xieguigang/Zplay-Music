﻿Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Imaging

Public Class Playlist : Implements IEnumerable(Of MediaFile)

    Dim _files As List(Of MediaFile)
    Dim p As Integer = -1
    Dim _eolist As Action

    Public ReadOnly Property Type As ListTypes
    Public ReadOnly Property URI As String

    Public Function DrawListCount() As Image
        Dim s As String = _files.Count.ToString
        Dim font As New Font(FontFace.MicrosoftYaHei, 8)

        Using g As GDIPlusDeviceHandle =
            My.Resources.Numbers.Size.CreateGDIDevice

            Dim sz = g.Gr_Device.MeasureString(s, font)
            Dim loc As New Point((g.Width - sz.Width) / 2, (g.Height - sz.Height) / 2)

            Call g.Gr_Device.DrawImageUnscaled(My.Resources.Numbers, New Point(0, 0))
            Call g.Gr_Device.DrawString(s, font, Brushes.White, loc)

            Return g.ImageResource
        End Using
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