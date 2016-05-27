Imports System.Threading

Public Class FormAnimation

    Public ReadOnly Property Parent As FormZplay
    Public ReadOnly Property ListInvoke As PlayListAnimation

    Sub New(main As FormZplay, listInvoke As PlayListAnimation)
        Me.Parent = main
        Me.ListInvoke = listInvoke
    End Sub

    Public ReadOnly Property IsOpen As Boolean

    Private Sub __waitListClose()
        Do While ListInvoke.IsOpen
            Call Thread.Sleep(10)
        Loop
    End Sub

    Public Sub Close()
        Call __waitListClose()
    End Sub

    Public Sub Open()

    End Sub
End Class
