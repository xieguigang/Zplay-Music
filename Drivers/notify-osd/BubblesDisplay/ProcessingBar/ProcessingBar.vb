Imports Microsoft.VisualBasic.Imaging

Namespace BubblesDisplay

    Public Class ProcessingBar : Implements System.IDisposable

        ''' <summary>
        ''' 0 - 100
        ''' </summary>
        ''' <returns></returns>
        Public Property PercentageValue As Integer
            Get
                Return _percentage
            End Get
            Set(value As Integer)

                If _percentage <> value Then
                    _percentage = value
                    ' Call InvokeAnomation(Nothing, Nothing)
                End If
            End Set
        End Property

        Dim _percentage As Integer
        Dim _accomplish As Image
        Dim _render As Image
        Dim _interval As Integer

        ''' <summary>
        ''' 0 - 1000, default is 990
        ''' </summary>
        ''' <returns></returns>
        Public Property AnimationSpeed As Integer
            Get
                Return 1000 - _interval
            End Get
            Set(value As Integer)
                _interval = 1000 - value
            End Set
        End Property

        Dim _ProcAnimationInvokeUpdate As ProcessingBar.InvokeUpdateProcBar
        Dim _getWidth As Func(Of Integer)

        Sub New(InvokeUpdate As ProcessingBar.InvokeUpdateProcBar, getWidth As Func(Of Integer))
            AnimationSpeed = 990
            Me._getWidth = getWidth
            Me._ProcAnimationInvokeUpdate = InvokeUpdate
        End Sub

        Public Property BackColor As Color = Color.Black

        Public ReadOnly Property Height As Integer
            Get
                Return Me._renderHeight
            End Get
        End Property

        Dim _renderHeight As Integer

        Public Property Render As Image
            Get
                Return _render
            End Get
            Set(value As Image)
                Me._render = value

                Dim gr = New Size(8000, height:=value.Height).CreateGDIDevice()

                Dim x As Integer = 1

                Do While x < gr.Width
                    Call gr.Graphics.DrawImage(value, x, 0, value.Width, value.Height)
                    x += value.Width
                Loop

                Me._accomplish = gr.ImageResource
                Me._renderHeight = Render.Height

                '   Call Me.accomplish.Save("./tes.png")

            End Set
        End Property

        Dim _animation As Boolean

        Public Sub StartRollAnimation()
            _animation = True
            Call New Threading.Thread(AddressOf __rollAnimationThread).Start()
        End Sub

        Public Sub StopRollAnimation()
            _animation = False
        End Sub

        Private Sub __rollAnimationThread()
            Do While _animation
                Call InvokeAnimation()
                Call Threading.Thread.Sleep(_interval)
            Loop
        End Sub

        Dim _Animated As Integer

        Public Sub InvokeAnimation()
            Dim Gr As Graphics2D

            SyncLock Me._render
                Gr = New Size(Me._getWidth(), Me._render.Height).CreateGDIDevice(Me.BackColor)
            End SyncLock

            '绘制已经完成的部分
            Dim Length As Integer = (Me.PercentageValue / 100) * Me._getWidth() + 1

            _Animated += 1

            Dim accomplishRes As Image
            SyncLock Me._accomplish
                accomplishRes = Me._accomplish.Clone
            End SyncLock

            If _Animated + Length >= accomplishRes.Width Then
                _Animated = 1
            End If

            Dim res = Corping(accomplishRes, New Rectangle(New Point(_Animated + 1, 0), New Size(Length, Me.Height)))
            Call Gr.Graphics.DrawImage(res, 0, 0, res.Width, res.Height)

            Call Me._ProcAnimationInvokeUpdate(procRes:=Gr.ImageResource)
        End Sub

        Public Delegate Sub InvokeUpdateProcBar(procRes As Image)

        Public Shared Function Corping(source As Image, Rect As Rectangle) As Image
            Dim bit As Bitmap = New Bitmap(Clone(source), source.Width, source.Height)
            Dim cropBitmap As Bitmap = New Bitmap(Rect.Width, Rect.Height)
            Dim g As Graphics = Graphics.FromImage(cropBitmap)

            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.DrawImage(bit, 0, 0, Rect, GraphicsUnit.Pixel)

            Return cropBitmap
        End Function
#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Call Me.StopRollAnimation()
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace