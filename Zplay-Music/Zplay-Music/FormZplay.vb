Imports System.Threading

Public Class FormZplay

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Call New Thread(AddressOf sousuo).Start()
    End Sub

    Private Sub sousuo()
        run1 = True
        run2 = False
        Call Me.Invoke(Sub() Call __sss())
    End Sub

    Dim d As Integer = 50

    ''' <summary>
    ''' 收缩
    ''' </summary>
    Private Sub __sss()
        Dim loci As Point

        Do While True
            If run2 Then
                Exit Do
            End If

            If PictureBox1.Height > 0 Then
                PictureBox1.Height -= d
                loci = PictureBox1.Location
                PictureBox1.Location = New Point(loci.X, loci.Y + d)
            Else
                PictureBox1.Height = 0
                Exit Do
            End If



            Application.DoEvents()
            Thread.Sleep(10)
        Loop
    End Sub

    Dim run1, run2 As Boolean

    ''' <summary>
    ''' 伸展
    ''' </summary>
    Sub ____ss2()
        Dim loci As Point

        Do While True
            If run1 Then
                Exit Do
            End If

            If PictureBox1.Height < 507 Then
                PictureBox1.Height += d
                loci = PictureBox1.Location
                PictureBox1.Location = New Point(loci.X, loci.Y - d)
            Else
                PictureBox1.Height = 507
                Exit Do
            End If



            Application.DoEvents()
            Thread.Sleep(10)
        Loop
    End Sub

    Public Sub s2()
        run1 = False
        run2 = True
        Call Me.Invoke(Sub() Call Me.____ss2())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Call New Thread(AddressOf s2).Start()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Location = New Point(0, My.Computer.Screen.WorkingArea.Height - 10 - Height)
    End Sub
End Class
