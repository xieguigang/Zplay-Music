Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Language.C

Namespace App

    Public Module Extensions

        Public Function NotNull(ParamArray args As String()) As String
            For Each s As String In args
                If Not String.IsNullOrEmpty(s) Then
                    Return s
                End If
            Next

            Return ""
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="p">0 - 1</param>
        ''' 
        <Extension>
        Public Function TimePercentage(zplay As ZPlayInterface, p As Double) As TStreamTime
            Dim ms As Integer = p * zplay.StreamInfo.Length.ms
            Dim time As New TStreamTime With {
                .ms = ms
            }
            Return time
        End Function

        <Extension>
        Public Function FormatTime(time As TStreamTime) As String
            Return $"{FormatZero(time.hms.minute, 2)}:{FormatZero(time.hms.second, 2)}"
        End Function

        <Extension>
        Public Function FormatTime(time As TimeSpan) As String
            Return $"{FormatZero(time.Minutes, 2)}:{FormatZero(time.Seconds, 2)}"
        End Function

        <Extension>
        Public Function GetFileInfo(zplay As ZPlay, path As String, Optional decodeArt As Boolean = False) As MediaFile
            Call zplay.OpenFile(path)

            Dim file As New MediaFile With {
                .FileName = path,
                .Id3v2 = New TID3InfoEx,
                .StreamInfo = New TStreamInfo
            }

            Call zplay.LoadID3Ex(file.Id3v2, decodeArt)
            Call zplay.GetStreamInfo(file.StreamInfo)

            Call zplay.Close()

            Return file
        End Function

        ReadOnly __tagServices As New ZPlay

        ''' <summary>
        ''' Get media file ID3v2 tag data and stream information.
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        <Extension>
        Public Function GetMediaInfo(path As String, Optional decodeArt As Boolean = False) As MediaFile
            SyncLock __tagServices
                Return __tagServices.GetFileInfo(path, decodeArt)
            End SyncLock
        End Function

        <Extension>
        Public Iterator Function GetFilesInfo(files As IEnumerable(Of String), Optional decodeArt As Boolean = False) As IEnumerable(Of MediaFile)
            SyncLock __tagServices
                For Each file As String In files
                    Call Application.DoEvents()
                    Yield __tagServices.GetFileInfo(file, decodeArt)
                Next
            End SyncLock
        End Function

        <Extension>
        Public Function IsNullOrNothing(art As Bitmap) As Boolean
            If art Is Nothing Then
                Return True
            Else
                Dim sz = art.Size
                Return sz.Width = 1 AndAlso sz.Height = 1
            End If
        End Function
    End Module
End Namespace