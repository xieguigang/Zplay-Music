Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports libZPlay.InternalTypes

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
            Return $"{ZeroFill(time.hms.minute, 2)}:{ZeroFill(time.hms.second, 2)}"
        End Function

        <Extension>
        Public Function FormatTime(time As TimeSpan) As String
            Return $"{ZeroFill(time.Minutes, 2)}:{ZeroFill(time.Seconds, 2)}"
        End Function

        <Extension>
        Public Function GetFileInfo(zplay As ZPlay, path As String) As MediaFile
            Call zplay.OpenFile(path)

            Dim file As New MediaFile With {
                .FileName = path,
                .Id3v2 = New TID3InfoEx,
                .StreamInfo = New TStreamInfo
            }

            Call zplay.LoadID3Ex(file.Id3v2, False)
            Call zplay.GetStreamInfo(file.StreamInfo)

            Call zplay.Close()

            Return file
        End Function

        ReadOnly __tagServices As New ZPlay

        <Extension>
        Public Iterator Function GetFilesInfo(files As IEnumerable(Of String)) As IEnumerable(Of MediaFile)
            SyncLock __tagServices
                For Each file As String In files
                    Yield __tagServices.GetFileInfo(file)
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