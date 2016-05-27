Imports libZPlay.App
Imports libZPlay.InternalTypes

Public Class ProgressIndicator

    Public Property Time As TStreamTime
        Get
            Return _time
        End Get
        Set(value As TStreamTime)
            _time = value
            Me.Text = value.FormatTime
        End Set
    End Property

    Dim _time As TStreamTime

End Class
