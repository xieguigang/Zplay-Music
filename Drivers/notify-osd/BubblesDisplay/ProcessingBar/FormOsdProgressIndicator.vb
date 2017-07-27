Imports Microsoft.VisualBasic.Imaging
Imports NotifyOsd
Imports NotifyOsd.BubblesDisplay
Imports NotifyOsd.Framework.Balloon

''' <summary>
''' 消息气泡会一直停留在桌面直到用户取消或者走到100
''' </summary>
Friend Class FormOsdProgressIndicator : Implements System.IDisposable

    Public Property ProcessingBar As ProcessingBar

    ''' <summary>
    ''' 0 - 100
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As Integer
        Get
            Return Me._ProcessingBar.PercentageValue
        End Get
        Set(value As Integer)
            If value <= 0 Then
                value = 1
            End If

            If value >= 100 Then
                value = 100
                '进度条已经走完了，则让气泡消失
                Me._msg.BubbleBehavior = BubbleBehaviors.AutoClose
            Else
                Me._ProcessingBar.PercentageValue = value
            End If

            Me._resWidth = Me._resNormal.Width

            Call Me._ProcessingBar.InvokeAnimation()
        End Set
    End Property

    Public Sub Increase(delta As Integer)
        Value = Value + delta
        Console.WriteLine($"{NameOf(Value)} ===> {Value }")
        Console.WriteLine($"{NameOf(delta)} ===> { delta   }")
    End Sub

#Region "Constructors"

    Sub New()
        Call MyBase.New(New OsdNotifier)
        ' This call is required by the designer.
        InitializeComponent()
        CheckForIllegalCrossThreadCalls = False
        ' Add any initialization after the InitializeComponent() call.
        Me._ProcessingBar = New ProcessingBar(AddressOf Me.InvokeUpdate, Function() Me._resWidth - 30)
        Me._ProcessingBar.Render = My.Resources.ProcessingbarRes
    End Sub
#End Region

    ''' <summary>
    ''' 用户取消了操作
    ''' </summary>
    Public Sub Cancel()
        Value = 100
        Call Me._ProcessingBar.StopRollAnimation()
        Call Me.__startFadeOut()
    End Sub

    Protected Overrides Sub __afterDrawing()
        Me._resWidth = Me._resNormal.Width
        Me._msg.BubbleBehavior = BubbleBehaviors.ProgressIndicator
        Me._ProcessingBar.StartRollAnimation()
    End Sub

#Region "为了减少CPU占用，这里只画一种状态"

    Protected _resWidth As Integer

    Public Sub InvokeUpdate(procBarRender As Image)
        Dim resNormal As Image

        On Error GoTo [Exit]

        SyncLock Me._resNormal
            resNormal = Me._resNormal.Clone
        End SyncLock

        '只从普通状态的开始绘图，到最后一起模糊处理就可以了
        Dim Gr = resNormal.Size.CreateGDIDevice(Me._ProcessingBar.BackColor)

        Me._resWidth = Gr.Width

        Call Gr.Graphics.DrawImage(resNormal, New Point)
        Call Gr.Graphics.DrawImage(procBarRender, New Point(15, Height - procBarRender.Height - 18))

        Me._ProcNormalRenderer = Gr.ImageResource
        Me._procBlurRenderer = InternalBlur(_ProcNormalRenderer)
        Me._procBlurRenderer = MessageRender.DrawFrame(Me._procBlurRenderer)

        If _Blur Then
            Me.PictureBox1.BackgroundImage = Me._procBlurRenderer
        Else
            Me.PictureBox1.BackgroundImage = Me._ProcNormalRenderer
        End If

[Exit]:
    End Sub

    Protected Overridable Function InternalBlur(res As Image) As Image
        Return GaussBlur.GaussBlur(GaussBlur.GaussBlur(GaussBlur.GaussBlur(res)))
    End Function

    Dim _ProcNormalRenderer As Image, _procBlurRenderer As Image

    Protected Overrides Function __setNormal() As Image
        Return Me._ProcNormalRenderer
    End Function

    Protected Overrides Function __setBlur() As Image
        Return Me._procBlurRenderer
    End Function

    Protected _Blur As Boolean

    ''' <summary>
    ''' 显示回正常
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overrides Sub MouseLeaveBackNormal(sender As Object, e As EventArgs)
        Me._Blur = False
        MyBase.MouseLeaveBackNormal(sender, e)
    End Sub

    Protected Overrides Sub MouseMoveEnterBlur(sender As Object, e As EventArgs)
        Me._Blur = True
        MyBase.MouseMoveEnterBlur(sender, e)
    End Sub
#End Region

    Protected Overrides Function __getParams() As RenderParameters
        Return New RenderParameters With {
            .YDelta = Me._ProcessingBar.Height + 15
        }
    End Function

    Protected Overrides Sub __afterCleanUp()
        Call Me._ProcessingBar.Dispose()
    End Sub
End Class