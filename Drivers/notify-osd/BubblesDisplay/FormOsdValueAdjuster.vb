Friend Class FormOsdValueAdjuster

    Dim Up As AdjustInvoke, Down As AdjustInvoke, ValueChanged As AdjustInvoke

#Region "Constructors"

    Sub New(Up As AdjustInvoke, Down As AdjustInvoke, ValueChanged As AdjustInvoke)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.ValueChanged = ValueChanged
        Me.Up = Up
        Me.Down = Down

    End Sub

    Sub New(Up As AdjustInvoke, Down As AdjustInvoke)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Up = Up
        Me.Down = Down
    End Sub

    Sub New(ValueChanged As AdjustInvoke)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.ValueChanged = ValueChanged
    End Sub
#End Region

    Protected Overrides Sub __afterDrawing()
        Me._resWidth = Me._resNormal.Width
        Me._msg.BubbleBehavior = BubbleBehaviors.ValueAdjustments
        Me.ProcessingBar.StopRollAnimation()
        Me.ProcessingBar.InvokeAnimation()
    End Sub

    Protected Overrides Sub BubbleMouseWheel(sender As Object, e As MouseEventArgs)

        Me._unloadCount = 0

        Call Me.Increase(e.Delta / 100)

        If e.Delta > 0 Then
            If Not Me.Up Is Nothing Then
                Call Me.Up(Value)
            End If
        Else
            If Not Me.Down Is Nothing Then
                Call Me.Down(Value)
            End If
        End If

        If Not Me.ValueChanged Is Nothing Then
            Call Me.ValueChanged(Me.Value)
        End If
    End Sub

    Protected Overrides Function InternalBlur(res As Image) As Image
        Return GaussBlur.GaussBlur(res)
    End Function

    Protected Overrides Sub MouseMoveEnterBlur(sender As Object, e As EventArgs)
        _Blur = True
        If _startFadeOut Then Return
        PictureBox1.BackgroundImage = __setBlur()
        _animationInvoker.Interval = 5
        _animationInvoker.Enabled = True
        _timerUnloadCount.Enabled = False
    End Sub

End Class

''' <summary>
''' 
''' </summary>
''' <param name="value">1 - 100</param>
Public Delegate Sub AdjustInvoke(value As Integer)
