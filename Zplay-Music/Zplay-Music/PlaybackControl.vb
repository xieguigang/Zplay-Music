Public Class PlaybackControl

    Public Event PlaybackNext()
    Public Event PlaybackPrevious()
    Public Event PlaybackPause()
    Public Event PlaybackPlay()

    Dim _paused As Boolean

    Public Property Paused As Boolean
        Get
            Return _paused
        End Get
        Set(value As Boolean)
            _paused = value

            If _paused Then

            Else

            End If
        End Set
    End Property

    Private Sub btnNext_MouseEnter(sender As Object, e As EventArgs) Handles btnNext.MouseEnter
        btnNext.BackgroundImage = My.Resources.Next_highlight
    End Sub

    Private Sub btnNext_MouseLeave(sender As Object, e As EventArgs) Handles btnNext.MouseLeave
        btnNext.BackgroundImage = My.Resources._Next
    End Sub

    Private Sub btnNext_MouseUp(sender As Object, e As MouseEventArgs) Handles btnNext.MouseUp
        RaiseEvent PlaybackNext()
    End Sub

    Private Sub btnPlay_MouseEnter(sender As Object, e As EventArgs) Handles btnPlay.MouseEnter
        btnPlay.BackgroundImage = My.Resources.Play_highlight
    End Sub

    Private Sub btnPlay_MouseLeave(sender As Object, e As EventArgs) Handles btnPlay.MouseLeave
        btnPlay.BackgroundImage = My.Resources.Play1
    End Sub

    Private Sub btnPlay_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPlay.MouseUp
        If Paused Then
            RaiseEvent PlaybackPlay()
        Else
            RaiseEvent PlaybackPause()
        End If
    End Sub

    Private Sub btnPrevious_MouseEnter(sender As Object, e As EventArgs) Handles btnPrevious.MouseEnter
        btnPrevious.BackgroundImage = My.Resources.Previous_highlight
    End Sub

    Private Sub btnPrevious_MouseLeave(sender As Object, e As EventArgs) Handles btnPrevious.MouseLeave
        btnPrevious.BackgroundImage = My.Resources.Previous
    End Sub

    Private Sub btnPrevious_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPrevious.MouseUp
        RaiseEvent PlaybackPrevious()
    End Sub
End Class
