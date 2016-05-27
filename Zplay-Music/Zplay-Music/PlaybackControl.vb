Public Class PlaybackControl

    Private Sub btnNext_MouseEnter(sender As Object, e As EventArgs) Handles btnNext.MouseEnter
        btnNext.BackgroundImage = My.Resources.Next_highlight
    End Sub

    Private Sub btnNext_MouseLeave(sender As Object, e As EventArgs) Handles btnNext.MouseLeave
        btnNext.BackgroundImage = My.Resources._Next
    End Sub

    Private Sub btnNext_MouseUp(sender As Object, e As MouseEventArgs) Handles btnNext.MouseUp

    End Sub

    Private Sub btnPlay_MouseEnter(sender As Object, e As EventArgs) Handles btnPlay.MouseEnter
        btnPlay.BackgroundImage = My.Resources.Play_highlight
    End Sub

    Private Sub btnPlay_MouseLeave(sender As Object, e As EventArgs) Handles btnPlay.MouseLeave
        btnPlay.BackgroundImage = My.Resources.Play1
    End Sub

    Private Sub btnPlay_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPlay.MouseUp

    End Sub

    Private Sub btnPrevious_MouseEnter(sender As Object, e As EventArgs) Handles btnPrevious.MouseEnter
        btnPrevious.BackgroundImage = My.Resources.Previous_highlight
    End Sub

    Private Sub btnPrevious_MouseLeave(sender As Object, e As EventArgs) Handles btnPrevious.MouseLeave
        btnPrevious.BackgroundImage = My.Resources.Previous
    End Sub

    Private Sub btnPrevious_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPrevious.MouseUp

    End Sub
End Class
