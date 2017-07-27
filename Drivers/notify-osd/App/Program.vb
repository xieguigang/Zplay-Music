Imports System.Text.Encoding
Imports System.Text
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft
Imports Microsoft.VisualBasic.Parallel

''' <summary>
''' 假若需要进行编程处理的话，从本模块的<see cref="Run(String)"/>方法启动线程
''' </summary>
Public Module Program

    ''' <summary>
    ''' 从这里开始运行主进程
    ''' </summary>
    ''' <returns></returns>
    Public Function Main() As Integer
        Return GetType(CLI).RunCLI(App.CommandLine)
    End Function

    ''' <summary>
    ''' 线程的内存映射文件名称
    ''' </summary>
    ''' <param name="uri"></param>
    Public Sub Run(uri As String)
        Call RunTask(Sub() Call CLI.StartServices($"-start {uri}"))
    End Sub
End Module
