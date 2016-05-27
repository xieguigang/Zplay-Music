<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlaybackControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnPrevious = New System.Windows.Forms.PictureBox()
        Me.btnPlay = New System.Windows.Forms.PictureBox()
        Me.btnNext = New System.Windows.Forms.PictureBox()
        CType(Me.btnPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPlay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrevious
        '
        Me.btnPrevious.BackgroundImage = Global.ZplayMusic.My.Resources.Resources.Previous
        Me.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrevious.Location = New System.Drawing.Point(0, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(42, 42)
        Me.btnPrevious.TabIndex = 0
        Me.btnPrevious.TabStop = False
        '
        'btnPlay
        '
        Me.btnPlay.BackgroundImage = Global.ZplayMusic.My.Resources.Resources.Play1
        Me.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPlay.Location = New System.Drawing.Point(48, 0)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(42, 42)
        Me.btnPlay.TabIndex = 1
        Me.btnPlay.TabStop = False
        '
        'btnNext
        '
        Me.btnNext.BackgroundImage = Global.ZplayMusic.My.Resources.Resources._Next
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.Location = New System.Drawing.Point(96, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(42, 42)
        Me.btnNext.TabIndex = 2
        Me.btnNext.TabStop = False
        '
        'PlaybackControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.btnPrevious)
        Me.Name = "PlaybackControl"
        Me.Size = New System.Drawing.Size(138, 42)
        CType(Me.btnPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPlay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnPrevious As PictureBox
    Friend WithEvents btnPlay As PictureBox
    Friend WithEvents btnNext As PictureBox
End Class
