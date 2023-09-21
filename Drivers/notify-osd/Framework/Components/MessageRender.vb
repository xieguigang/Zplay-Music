Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging

Module MessageRender

    ''' <summary>
    ''' Drawing the 8 parts of the image frame into one piece
    ''' </summary>
    ''' <param name="ImageData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DrawFrame(ImageData As Image) As Image
        Dim resIMG As Image = DirectCast(ImageData.Clone, Image)

        Using GraphicDevice As Graphics = Graphics.FromImage(resIMG)
            Dim BottomHeight As Integer = My.Resources.NotificationMainPanel_bottom.Height,
                   TopHeight As Integer = My.Resources.NotificationMainPanel_top.Height

            GraphicDevice.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_top, 7, 0, ImageData.Width - 14, TopHeight)
            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_top_LEFT, 0, 0, 7, 7)
            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_top_RIGHT, ImageData.Width - 7, 0, 7, 7)
            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_bottom, 7, ImageData.Height - BottomHeight, ImageData.Width - 14, BottomHeight)
            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_bottom_LEFT, 0, ImageData.Height - 7, 7, 7)
            Call GraphicDevice.DrawImage(My.Resources.NotificationMainPanel_bottom_Right, ImageData.Width - 7, ImageData.Height - 7, 7, 7)

            Call GraphicDevice.DrawImage(My.Resources.PanelFrame, 0, TopHeight, My.Resources.PanelFrame.Width, ImageData.Height - TopHeight - BottomHeight)
            Call GraphicDevice.DrawImage(My.Resources.PanelFrame, ImageData.Width - My.Resources.PanelFrame.Width, TopHeight, My.Resources.PanelFrame.Width, ImageData.Height - TopHeight - BottomHeight)

            Return resIMG
        End Using
    End Function

    ''' <summary>
    ''' drawing the message title, message string and the message icon
    ''' </summary>
    ''' <param name="MSG"></param>
    ''' <returns></returns>
    ''' <remarks>函数依靠字符串中含有多少个回车符来判断绘制的位置</remarks>
    Public Function DrawMessage(MSG As Message, Renderer As RenderParameters) As Image
        Try
            Return __drawInner(MSG, Renderer)
        Catch ex As Exception
            ex = New Exception("[Message] " & MSG.GetJson, ex)
            ex = New Exception("[Parameters] " & Renderer.GetJson, ex)

            Dim exMsg As String = ErrorLog.BugsFormatter(ex)
            Call FileIO.FileSystem.WriteAllText(App.HOME & "/notify-osd.log", exMsg & vbCrLf, append:=True)

            Return Nothing
        End Try
    End Function

    ReadOnly _textMeasures As Graphics = Graphics.FromImage(My.Resources.UBUNTU)
    ReadOnly _minSize As New Size(352, 73)

    Private Function __drawInner(msg As Message, params As RenderParameters) As Image
        Dim title As String = If(String.IsNullOrEmpty(msg.Title), Now.ToString, msg.Title.Replace("\n", vbCrLf)),
            message As String = If(String.IsNullOrEmpty(msg.Message), "", msg.Message.Replace("\n", vbCrLf))
        Dim titleSize As SizeF = _textMeasures.MeasureString(title, params.TitleFont)
        Dim msgSize As SizeF = _textMeasures.MeasureString(message, params.MessageFont)
        Dim margins As New Size(15, 5)
        Dim grSize As New Point With {
            .X = Math.Max(titleSize.Width, msgSize.Width) + params.IconSize + margins.Width * 3,
            .Y = margins.Height * 2 + 5 + titleSize.Height + msgSize.Height + 25
        }

        If grSize.Y < 76 Then
            grSize = New Point(grSize.X, 76)
        End If
        If grSize.X < 352 Then
            grSize = New Point(352, grSize.Y)
        End If

        Using grDraw As Graphics2D = New Size(grSize.X, grSize.Y + params.YDelta) _
            .CreateGDIDevice(Color.Transparent)

            grDraw.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            '绘制表面纹理
            Call grDraw.Graphics.FillRegion(Brushes.Black, New Region(New Rectangle(New Point, grSize)))
            '绘制消息内容
            grDraw.Graphics.DrawImage(msg.Icon, 15, 15, params.IconSize, params.IconSize)
            grDraw.Graphics.DrawString(title, params.TitleFont, Brushes.WhiteSmoke, New Point(78, 15))
            grDraw.Graphics.DrawString(message, params.MessageFont, Brushes.White, New Point(78, 5 + 15 + titleSize.Height))

            Return grDraw.ImageResource
        End Using
    End Function
End Module
