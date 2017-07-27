Imports Microsoft.VisualBasic.Parallel.ParallelExtension
Imports NotifyOsd.BubblesDisplay

Namespace Framework.Balloon

    ''' <summary>
    ''' For adjust the numeric value
    ''' </summary>
    Public Class ValueAdjuster

        Public Property PercentageValue As Integer
            Get
                Return Me._adjustBar.Value
            End Get
            Set(value As Integer)
                Me._adjustBar.Value = value
            End Set
        End Property

        Dim _adjustBar As FormOsdValueAdjuster
        Dim _invokeThread As Threading.Thread

        Public Sub Show()
            If _invokeThread Is Nothing Then
                _invokeThread = RunTask(AddressOf _adjustBar.ShowDialog)
            End If
        End Sub

        Public ReadOnly Property ProcBar As ProcessingBar
            Get
                Return Me._adjustBar.ProcessingBar
            End Get
        End Property

#Region "Constructors"

        Sub New(msg As Message, Up As AdjustInvoke, Down As AdjustInvoke, valueChanged As AdjustInvoke)
            Me._adjustBar = New FormOsdValueAdjuster(Up, Down, valueChanged)
            Me._adjustBar.Message = msg
        End Sub

        Sub New(MSG As Message, Up As AdjustInvoke, Down As AdjustInvoke)
            Me._adjustBar = New FormOsdValueAdjuster(Up, Down)
            Me._adjustBar.Message = MSG
        End Sub

        ''' <summary>
        ''' Public Delegate Sub AdjustInvoke(value As Integer)
        ''' </summary>
        ''' <param name="MSG"></param>
        ''' <param name="ValueChanged">Public Delegate Sub AdjustInvoke(value As Integer)</param>
        Sub New(MSG As Message, ValueChanged As AdjustInvoke)
            Me._adjustBar = New FormOsdValueAdjuster(ValueChanged)
            Me._adjustBar.Message = MSG
        End Sub
#End Region
    End Class
End Namespace