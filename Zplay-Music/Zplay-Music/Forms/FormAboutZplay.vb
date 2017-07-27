Public Class FormAboutZplay

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub FormAboutZplay_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Text &= vbTab & App.Version
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Call Process.Start(LinkLabel1.Text)
    End Sub
End Class