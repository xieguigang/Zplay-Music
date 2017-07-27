Imports System.Threading

Public Class PlayListAnimation

    Public ReadOnly Property Parent As FormZplay

    Sub New(main As FormZplay)
        Parent = main
    End Sub

    Private Sub __startCloseList()
        run1Close = True
        runOpen = False
        Call Parent.Invoke(Sub() Call __closing())
    End Sub

    Dim delta As Integer = 80

    ''' <summary>
    ''' 收缩
    ''' </summary>
    Private Sub __closing()
        Dim loci As Point

        Do While True
            If runOpen Then
                Exit Do
            End If

            If Parent.Panel1.Height > 0 Then
                Parent.Panel1.Height -= delta
                loci = Parent.Panel1.Location
                Parent.Panel1.Location = New Point(loci.X, loci.Y + delta)
            Else
                Parent.Panel1.Height = 0
                Exit Do
            End If

            Call Thread.Sleep(10)
            Call Application.DoEvents()
        Loop
    End Sub

    Dim run1Close, runOpen As Boolean

    ''' <summary>
    ''' 伸展
    ''' </summary>
    Sub __runOpen()
        Dim loci As Point

        Do While True
            If run1Close Then
                Exit Do
            End If

            If Parent.Panel1.Height < 507 Then
                Parent.Panel1.Height += delta
                loci = Parent.Panel1.Location
                Parent.Panel1.Location = New Point(loci.X, loci.Y - delta)
            Else
                Parent.Panel1.Height = 507
                Exit Do
            End If

            Call Thread.Sleep(10)
            Call Application.DoEvents()
        Loop
    End Sub

    Public ReadOnly Property IsOpen As Boolean
        Get
            Return Parent.Panel1.Height > 0
        End Get
    End Property

    Private Sub __startOpenList()
        run1Close = False
        runOpen = True
        Call Parent.Invoke(Sub() Call Me.__runOpen())
    End Sub

    Public Sub Close()
        Call New Thread(AddressOf __startCloseList).Start()
    End Sub

    Public Sub Open()
        Call New Thread(AddressOf __startOpenList).Start()
    End Sub
End Class
