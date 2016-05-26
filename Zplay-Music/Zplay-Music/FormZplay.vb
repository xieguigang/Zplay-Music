Imports System.Threading

Public Class FormZplay

    Dim __playListInvoke As PlayListAnimation

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(0, My.Computer.Screen.WorkingArea.Height - 10 - Height)
        __playListInvoke = New PlayListAnimation(Me)
    End Sub
End Class
