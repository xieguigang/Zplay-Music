Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Imaging

Public Class ProgressIndicator

    Public Property Length As Integer

    Public Property Time As TStreamTime
        Get
            Return _time
        End Get
        Set(value As TStreamTime)
            _time = value
            Me.Text = value.FormatTime

            If Length > 0 Then
                Dim percent As Double = value.ms / Length
                Dim w As Integer = percent * Width
                Using g = Me.Size.CreateGDIDevice(Me.BackColor)
                    Dim rect As New Rectangle(New Point, New Size(w, 4))
                    Call g.FillRectangle(Brushes.LightSkyBlue, rect)
                    BackgroundImage = g.ImageResource
                End Using
            Else
                Call VBDebugger.Warning($"{NameOf(Length)} value is not set!")
            End If
        End Set
    End Property

    Dim _time As TStreamTime

End Class
