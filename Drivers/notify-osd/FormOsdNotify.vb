Imports System.Text

Friend Class FormOsdNotify

    ''' <summary>
    ''' Using for message dequeue
    ''' </summary>
    ''' <remarks></remarks>
    Public Event DisposeFinal()

    Dim DrawFadeAnimation As System.Action
    Dim _imgNormal, _imgHighlight As Image
    Dim WithEvents _TimerUnloadCount As New Timers.Timer(800)
    Dim WithEvents Timer1 As New Timers.Timer(1)
    Dim OsdNotifier As OsdNotifier

    ''' <summary>
    ''' 根据传入的数据，格式化字符串之后生成底图
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Message As OsdNotifier.Message
        Get
            Return Me._Message
        End Get
        Set(value As OsdNotifier.Message)
            Me._Message = value

            _imgNormal = MessageRender.DrawMessage(value)
            _imgHighlight = _imgNormal
            _imgHighlight = GaussBlur(GaussBlur(GaussBlur(_imgHighlight)))

            '绘制边框
            _imgNormal = DrawFrame(_imgNormal)
            _imgHighlight = DrawFrame(_imgHighlight)

            Me.PictureBox1.BackgroundImage = _imgNormal
            Me.Size = _imgNormal.Size

            Me.Location = New Point With {
                .X = Screen.PrimaryScreen.Bounds.Width - Me.Width - 10,
                .Y = 0.04 * Screen.PrimaryScreen.Bounds.Height}
        End Set
    End Property

    Dim _Message As OsdNotifier.Message

    Sub New(OsdNotifier As OsdNotifier)
        CheckForIllegalCrossThreadCalls = False
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.OsdNotifier = OsdNotifier
    End Sub

    Private Sub FormosdNotify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        Me.TopMost = True
        Timer1.Interval = 1
        DrawFadeAnimation = AddressOf FadeIn
        Timer1.Enabled = True
        _TimerUnloadCount.Interval = 500
        _TimerUnloadCount.Enabled = True
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        If _StartFadeOut Then Return
        PictureBox1.BackgroundImage = _imgHighlight
        Timer1.Interval = 5
        DrawFadeAnimation = AddressOf FadeBlur
        Timer1.Enabled = True
        _TimerUnloadCount.Enabled = False
    End Sub

    Private Sub FadeBlur()
        If Me.Opacity > 0.3 Then
            Me.Opacity -= 0.04
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        If _StartFadeOut Then Return
        PictureBox1.BackgroundImage = _imgNormal
        Timer1.Interval = 1
        DrawFadeAnimation = AddressOf FadeIn
        Timer1.Enabled = True
        _TimerUnloadCount.Enabled = True
        _UnloadCount = 0
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Elapsed
        Call DrawFadeAnimation()
    End Sub

    Dim _UnloadCount As Integer = 0, _StartFadeOut As Boolean = False

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles _TimerUnloadCount.Elapsed
        Call Debug.WriteLine(Me.Message.BubbleBehavior.ToString)

        If Me.Message.BubbleBehavior = OsdNotifier.BubbleBehaviorTypes.FreezUntileClick Then
            _UnloadCount = 0
            Return
        End If

        If _UnloadCount < 8 Then
            _UnloadCount += 1
        Else
            Call InvokeStartFadeOut()
        End If
    End Sub

    Private Sub InvokeStartFadeOut()
        _StartFadeOut = True
        _TimerUnloadCount.Enabled = False
        Timer1.Interval = 1
        DrawFadeAnimation = AddressOf FadeOut
        Timer1.Enabled = True
    End Sub

    ''' <summary>
    ''' Callback
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim Handle = ActionCallback

        If Not Handle Is Nothing Then
            Call Handle()
            ActionCallback = Nothing
            Call InvokeStartFadeOut()
        End If
    End Sub

    Public Property ActionCallback As Action

    Private Sub FadeIn()
        If Me.Opacity < 0.9 Then
            Me.Opacity += 0.05
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub FadeOut()
        If Me.Opacity > 0 Then
            Me.Opacity -= 0.06
        Else
            Call Me.Close()
            Timer1.Enabled = False
        End If
    End Sub
End Class
