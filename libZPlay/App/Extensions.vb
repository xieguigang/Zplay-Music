Imports System.Runtime.CompilerServices
Imports libZPlay.InternalTypes

Namespace App

    Public Module Extensions

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="p">0 - 1</param>
        ''' 
        <Extension>
        Public Function TimePercentage(zplay As ZplayMusic, p As Double) As TStreamTime
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
    End Module
End Namespace