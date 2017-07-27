Imports System.Threading

Public Class FormAnimation

    Public ReadOnly Property Parent As FormZplay
    Public ReadOnly Property ListInvoke As PlayListAnimation

    Sub New(main As FormZplay, listInvoke As PlayListAnimation)
        Me.Parent = main
        Me.ListInvoke = listInvoke
    End Sub

    Public ReadOnly Property IsOpen As Boolean
        Get
            Return Parent.Location.X <> x
        End Get
    End Property

    Dim x As Integer = -(563 - 22)

    Private Sub __waitListClose()
        Do While ListInvoke.IsOpen
            Call Thread.Sleep(10)
            Call Application.DoEvents()
        Loop
    End Sub

    Dim delta As Integer = 20

    Public Sub Close()
        Call __waitListClose()

        Do While True

            If Parent.Location.X > x Then
                Parent.Location = New Point(Parent.Location.X - delta, Parent.Location.Y)
            Else
                Parent.Location = New Point(x, Parent.Location.Y)
                Exit Do
            End If

            Call Application.DoEvents()
            Call Thread.Sleep(2)
        Loop

        Parent.picDocker.BackgroundImage = My.Resources.Docker_Shrinkage
    End Sub

    Public Sub Open()
        Do While True

            If Parent.Location.X < 0 Then
                Parent.Location = New Point(Parent.Location.X + delta, Parent.Location.Y)
            Else
                Parent.Location = New Point(0, Parent.Location.Y)
                Exit Do
            End If

            Call Application.DoEvents()
            Call Thread.Sleep(2)
        Loop

        Parent.picDocker.BackgroundImage = My.Resources.Docker_Expand
    End Sub
End Class
