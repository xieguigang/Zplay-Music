Imports NotifyOsd.GaussBlur

Public Module Program

    Sub Main()
        'Call Threading.Thread.Sleep(2 * 1000)
        'Call New NotifyOsd.Framework.Balloon.OsdNotifier().SendMessage("test ok!", "test ok!", "", Nothing)

        'Dim gg As Byte(,) = New Byte(500, 500) {}
        ''  Dim iii = TestPointer(100, 100, gg)
        'Pause()

        Dim msg As New NotifyOsd.Message With {
            .Title = "进度条气泡测试(示例代码)",
            .Message = "\n  \n 
Dim proBar = New ProgressBar(msg, New Point)
Call proBar.Show()

proBar.ProgressBar.AnimationSpeed = 995

For i As Integer = 1 To 100
    proBar.PercentageValue = i
    Call Threading.Thread.Sleep(300)
Next"
        }
        Using proBar = New NotifyOsd.Framework.Balloon.ProgressBar(msg, New Point)
            Call proBar.Show()

            proBar.ProgressBar.AnimationSpeed = 995

            For i As Integer = 1 To 100
                proBar.PercentageValue = i
                Call Threading.Thread.Sleep(30)
            Next

        End Using

        Call App.Exit()

        '        msg = New NotifyOsd.Message With {
        '            .Title = "调节测试(在气泡上滚动鼠标滚轮)",
        '            .Message = "这个消息气泡可以用作为音量调节或者数值调节的工具

        'Dim valueAdjust As New ValueAdjuster(msg, AddressOf adjustInvoke)
        'valueAdjust.Show()
        'valueAdjust.PercentageValue = 50
        '"
        '        }
        '        Dim valueAdjust As New NotifyOsd.Framework.Balloon.ValueAdjuster(msg, AddressOf adjustInvoke)
        '        valueAdjust.Show()
        '        valueAdjust.PercentageValue = 50


        '        Call Console.ReadLine()

    End Sub

    Private Sub adjustInvoke(x As Integer)
        Call Console.WriteLine($"{NameOf(x)} ==>= {x}")
    End Sub
End Module
