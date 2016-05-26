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
        Public Function TimePercentage(zplay As MediaPlayer, p As Double) As TStreamTime
            Dim ms As Integer = p * zplay.StreamInfo.Length.ms
            Dim time As New TStreamTime With {
                .ms = ms
            }
            Return time
        End Function
    End Module
End Namespace