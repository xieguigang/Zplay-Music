Imports System.Threading
Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Imaging

Public Class ProgressIndicator

    Public Property Length As Integer
    Public Property ParentMain As FormZplay

    Public Property Time As TStreamTime
        Get
            Return _time
        End Get
        Set(value As TStreamTime)
            _time = value
            Me.Text = value.FormatTime

            If Length > 0 Then
                Dim percent As Double = value.ms / Length
                Dim w As Integer = percent * Width
                Using g = Me.Size.CreateGDIDevice(Me.BackColor)
                    Dim rect As New Rectangle(New Point, New Size(w, 4))
                    Call g.FillRectangle(Brushes.LightSkyBlue, rect)
                    BackgroundImage = g.ImageResource
                End Using
            Else
                Call VBDebugger.Warning($"{NameOf(Length)} value is not set!")
            End If
        End Set
    End Property

    Dim _time As TStreamTime

    Private Sub ProgressIndicator_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If Length > 0 Then
            ParentMain._toolTips.Visible = True
        End If
    End Sub

    Private Sub ProgressIndicator_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If Length > 0 Then
            ParentMain._toolTips.Visible = False
        End If
    End Sub

    ReadOnly tooltipFont As New Font(FontFace.Verdana, 8)
    ReadOnly ttWidth As Integer = My.Resources.tooltip.Width / 2

    Private Sub ProgressIndicator_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Length = 0 Then
            Return
        Else
            ParentMain._toolTips.Location =
                New Point(e.X - ttWidth, ParentMain._toolTips.Location.Y)
        End If

        Dim x As Integer = e.X
        Dim p As Double = x / Width
        Dim s As String = TimeSpan.FromMilliseconds(p * Length).FormatTime

        Using g As Graphics2D = ParentMain._toolTips.Size.CreateGDIDevice
            Dim sz = g.Graphics.MeasureString(s, tooltipFont)

            Call g.Graphics.DrawImage(My.Resources.tooltip, New Rectangle(New Point, My.Resources.tooltip.Size))
            Call g.DrawString(s, tooltipFont, Brushes.White, New Point((ParentMain._toolTips.Width - sz.Width) / 2, 1))
            ParentMain._toolTips.BackgroundImage = g.ImageResource
        End Using
    End Sub

    Private Sub ProgressIndicator_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Length = 0 Then
            Return
        Else
            ParentMain._toolTips.Location =
                New Point(e.X - ttWidth, ParentMain._toolTips.Location.Y)
        End If

        Dim x As Integer = e.X
        Dim p As Double = x / Width ' 长度的百分比

        ParentMain.play.SeeksByPercent(p)
    End Sub
End Class
