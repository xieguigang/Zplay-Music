Imports Microsoft.VisualBasic.Imaging

''' <summary>
''' 假若使用默认的配置数据直接使用构造函数就可以了
''' </summary>
Public Class RenderParameters
    Public Property TitleFont As Font = New Font(FontFace.MicrosoftYaHei, 9, FontStyle.Bold)
    Public Property YDelta As Integer = 0
    Public Property MessageFont As Font = New Font(FontFace.MicrosoftYaHei, 8)
    Public Property IconSize As Integer = 48
End Class