Imports System.Text
Imports NotifyOsd.Framework.Balloon

Friend Class FormOsdNotify

    ''' <summary>
    ''' Using for message dequeue
    ''' </summary>
    ''' <remarks></remarks>
    Public Event DisposeFinal()

    Protected __fadeAnimation As Action
    Protected _resNormal, _resHLBlur As Image
    Protected WithEvents _timerUnloadCount As New Timers.Timer(800)
    Protected WithEvents _animationInvoker As New Timers.Timer(1)
    Protected _osdNotifier As OsdNotifier

    ''' <summary>
    ''' 根据传入的数据，格式化字符串之后生成底图
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property Message As Message
        Get
            Return Me._msg
        End Get
        Set(value As Message)
            Me._msg = value
            Me.Text = value.Title
            Call __invokeDrawing()
        End Set
    End Property

    Protected Sub __invokeDrawing()
        _resNormal = MessageRender.DrawMessage(Me._msg, Me.__getParams)
        _resHLBlur = _resNormal
        _resHLBlur = GaussBlur.GaussBlur(GaussBlur.GaussBlur(GaussBlur.GaussBlur(_resHLBlur)))

        '绘制边框
        _resNormal = MessageRender.DrawFrame(_resNormal)
        _resHLBlur = MessageRender.DrawFrame(_resHLBlur)

        Me.PictureBox1.BackgroundImage = _resNormal
        Me.Size = _resNormal.Size
        Me.Location = New Point With {
            .X = Screen.PrimaryScreen.Bounds.Width - Me.Width - 10 + ScreenOffSet.X,
            .Y = 0.04 * Screen.PrimaryScreen.Bounds.Height + ScreenOffSet.Y
        }

        Call __afterDrawing()
    End Sub

    Public Property ScreenOffSet As Point = New Point

    ''' <summary>
    ''' 是否允许鼠标的移动会阻止消息气泡的消失
    ''' </summary>
    ''' <returns></returns>
    Public Property EnableBubbleFadingMouseEvent As Boolean = True

    Protected Overridable Function __getParams() As RenderParameters
        Return New RenderParameters
    End Function

    Protected _msg As Message

    Sub New(OsdNotifier As OsdNotifier)
        CheckForIllegalCrossThreadCalls = False
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._osdNotifier = OsdNotifier
    End Sub

    Protected Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CheckForIllegalCrossThreadCalls = False
        Me._osdNotifier = New OsdNotifier
    End Sub

    Private Sub FormosdNotify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        Me.TopMost = True
        _animationInvoker.Interval = 1
        __fadeAnimation = AddressOf __fadeIn
        _animationInvoker.Enabled = True
        _timerUnloadCount.Interval = 500
        _timerUnloadCount.Enabled = True

        Me.InvokeLostFocus(Me, Nothing)
    End Sub

    Protected Overridable Sub MouseMoveEnterBlur(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        If _startFadeOut Then Return
        PictureBox1.BackgroundImage = __setBlur()
        _animationInvoker.Interval = 5
        __fadeAnimation = AddressOf FadeBlur
        _animationInvoker.Enabled = True
        _timerUnloadCount.Enabled = False
    End Sub

    Private Sub FadeBlur()
        If Me.Opacity > 0.3 Then
            Me.Opacity -= 0.04
        Else
            _animationInvoker.Enabled = False
        End If
    End Sub

    Protected Overridable Function __setBlur() As Image
        Return Me._resHLBlur
    End Function

    Protected Overridable Function __setNormal() As Image
        Return Me._resNormal
    End Function

    Protected Overridable Sub MouseLeaveBackNormal(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        If _startFadeOut Then Return
        PictureBox1.BackgroundImage = __setNormal()
        _animationInvoker.Interval = 1
        __fadeAnimation = AddressOf __fadeIn
        _animationInvoker.Enabled = True
        _timerUnloadCount.Enabled = True
        _unloadCount = 0
    End Sub

    Private Sub BubbleFadeAnimation(sender As Object, e As EventArgs) Handles _animationInvoker.Elapsed
        Call __fadeAnimation()
    End Sub

    Protected _unloadCount As Integer = 0, _startFadeOut As Boolean = False

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles _timerUnloadCount.Elapsed
        Call Debug.WriteLine(Me.Message.BubbleBehavior.ToString)

        If Me.Message.BubbleBehavior = BubbleBehaviors.FreezUntileClick OrElse
            Me._msg.BubbleBehavior = BubbleBehaviors.ProgressIndicator Then    '必须要走到100才可以，所以到100之后修改Message里面的这个属性为AutoClose即可
            _unloadCount = 0
            Return
        End If

        If _unloadCount < 8 Then
            _unloadCount += 1
        Else
            Call __startFadeOut()
        End If
    End Sub

    ''' <summary>
    ''' 消息气泡从这里开始消失
    ''' </summary>
    Protected Overridable Sub __startFadeOut()
        _startFadeOut = True
        _timerUnloadCount.Enabled = False
        _animationInvoker.Interval = 1
        __fadeAnimation = AddressOf __fadeOut
        _animationInvoker.Enabled = True
    End Sub

    ''' <summary>
    ''' Callback
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overridable Sub BubbleClick(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim Handle = ActionCallback

        If Me._msg.BubbleBehavior = BubbleBehaviors.ProgressIndicator Then
            '必须要走到100才可以

        End If

        If Not Handle Is Nothing Then
            Call Handle()
            ActionCallback = Nothing
            Call __startFadeOut()
        End If
    End Sub

    Public Property ActionCallback As Action

    Private Sub __fadeIn()
        If Me.Opacity < 0.9 Then
            Me.Opacity += 0.05
        Else
            _animationInvoker.Enabled = False
        End If
    End Sub

    Private Sub __fadeOut()
        If Me.Opacity > 0 Then
            Me.Opacity -= 0.06
        Else
            Call __afterCleanUp()
            Call Me.Close()
            _animationInvoker.Enabled = False
        End If
    End Sub

#Region "Needs Overrides"

    Protected Overridable Sub __afterCleanUp()
        'Base class do nothing
    End Sub

    Protected Overridable Sub BubbleMouseWheel(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseWheel
        'Base class do nothing
    End Sub

    Protected Overridable Sub __afterDrawing()
        'Base class do nothing
    End Sub
#End Region

End Class
