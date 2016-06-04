Imports System.Drawing
Imports System.Windows.Forms

Public Class AlbumView

    Public Shadows Event OnClick()

    Public Property ArtImage As Image
        Get
            Return PictureBox1.BackgroundImage
        End Get
        Set(value As Image)
            PictureBox1.BackgroundImage = value
        End Set
    End Property

    Public Overrides Property Text As String
        Get
            Return Label1.Text
        End Get
        Set(value As String)
            Label1.Text = value
        End Set
    End Property

    Public Property Info As String
        Get
            Return Label2.Text
        End Get
        Set(value As String)
            Label2.Text = value
        End Set
    End Property

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter, Me.MouseEnter, Label2.MouseEnter, PictureBox1.MouseEnter
        Me.BackColor = Color.LightBlue
    End Sub

    Private Sub AlbumView_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, Label1.MouseLeave, Label2.MouseLeave, PictureBox1.MouseLeave
        Me.BackColor = Color.White
    End Sub

    Private Sub AlbumView_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick, PictureBox1.MouseClick
        RaiseEvent OnClick()
    End Sub
End Class
