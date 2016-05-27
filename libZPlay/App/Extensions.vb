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
    End Module
End Namespace