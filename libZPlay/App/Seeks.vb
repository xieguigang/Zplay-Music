Imports libZPlay.InternalTypes

Namespace App

    Public Class Seeks : Inherits ZPlayInterface

        Sub New(api As ZPlay)
            Call MyBase.New(api)
        End Sub

        Public Sub ByPercentage(p As Double)

        End Sub

        Public Sub ByTime(time As TStreamTime)

        End Sub
    End Class
End Namespace