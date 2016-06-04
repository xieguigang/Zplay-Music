<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLibrary
    Inherits Microsoft.VisualBasic.Windows.Forms.Controls.MaterialForm

    'Form overrides dispose to clean up the component list.
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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.MaterialTabSelector1 = New Microsoft.VisualBasic.Windows.Forms.Controls.MaterialTabSelector()
        Me.MaterialTabControl1 = New Microsoft.VisualBasic.Windows.Forms.Controls.MaterialTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ViewAlbums = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ListView1 = New ZplayMusic.ListView()
        Me.MaterialSingleLineTextField1 = New Microsoft.VisualBasic.Windows.Forms.Controls.MaterialSingleLineTextField()
        Me.MaterialTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(7, 6)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(749, 618)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'MaterialTabSelector1
        '
        Me.MaterialTabSelector1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MaterialTabSelector1.BaseTabControl = Me.MaterialTabControl1
        Me.MaterialTabSelector1.Depth = 0
        Me.MaterialTabSelector1.Location = New System.Drawing.Point(0, 64)
        Me.MaterialTabSelector1.MouseState = Microsoft.VisualBasic.Windows.Forms.MouseState.HOVER
        Me.MaterialTabSelector1.Name = "MaterialTabSelector1"
        Me.MaterialTabSelector1.Size = New System.Drawing.Size(779, 32)
        Me.MaterialTabSelector1.TabIndex = 0
        Me.MaterialTabSelector1.Text = "MaterialTabSelector1"
        '
        'MaterialTabControl1
        '
        Me.MaterialTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MaterialTabControl1.Controls.Add(Me.TabPage1)
        Me.MaterialTabControl1.Controls.Add(Me.TabPage2)
        Me.MaterialTabControl1.Controls.Add(Me.TabPage3)
        Me.MaterialTabControl1.Controls.Add(Me.TabPage4)
        Me.MaterialTabControl1.Depth = 0
        Me.MaterialTabControl1.Location = New System.Drawing.Point(1, 97)
        Me.MaterialTabControl1.MouseState = Microsoft.VisualBasic.Windows.Forms.MouseState.HOVER
        Me.MaterialTabControl1.Name = "MaterialTabControl1"
        Me.MaterialTabControl1.SelectedIndex = 0
        Me.MaterialTabControl1.Size = New System.Drawing.Size(770, 656)
        Me.MaterialTabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.FlowLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(762, 630)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Artists"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ViewAlbums)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(746, 591)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Albums"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ViewAlbums
        '
        Me.ViewAlbums.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewAlbums.AutoScroll = True
        Me.ViewAlbums.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ViewAlbums.BackColor = System.Drawing.Color.White
        Me.ViewAlbums.Location = New System.Drawing.Point(6, 6)
        Me.ViewAlbums.Name = "ViewAlbums"
        Me.ViewAlbums.Size = New System.Drawing.Size(810, 394)
        Me.ViewAlbums.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(746, 591)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Genres"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.ListView1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(746, 591)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Playlist"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Location = New System.Drawing.Point(7, 3)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(490, 477)
        Me.ListView1.TabIndex = 0
        '
        'MaterialSingleLineTextField1
        '
        Me.MaterialSingleLineTextField1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MaterialSingleLineTextField1.BackColor = System.Drawing.SystemColors.Control
        Me.MaterialSingleLineTextField1.Depth = 0
        Me.MaterialSingleLineTextField1.ForeColor = System.Drawing.Color.White
        Me.MaterialSingleLineTextField1.Hint = "Enter to Search"
        Me.MaterialSingleLineTextField1.Location = New System.Drawing.Point(502, 68)
        Me.MaterialSingleLineTextField1.MaxLength = 32767
        Me.MaterialSingleLineTextField1.MouseState = Microsoft.VisualBasic.Windows.Forms.MouseState.HOVER
        Me.MaterialSingleLineTextField1.Name = "MaterialSingleLineTextField1"
        Me.MaterialSingleLineTextField1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.MaterialSingleLineTextField1.SelectedText = ""
        Me.MaterialSingleLineTextField1.SelectionLength = 0
        Me.MaterialSingleLineTextField1.SelectionStart = 0
        Me.MaterialSingleLineTextField1.Size = New System.Drawing.Size(263, 23)
        Me.MaterialSingleLineTextField1.TabIndex = 2
        Me.MaterialSingleLineTextField1.TabStop = False
        Me.MaterialSingleLineTextField1.UseSystemPasswordChar = False
        '
        'FormLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(773, 757)
        Me.Controls.Add(Me.MaterialSingleLineTextField1)
        Me.Controls.Add(Me.MaterialTabControl1)
        Me.Controls.Add(Me.MaterialTabSelector1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FormLibrary"
        Me.Text = "Zplay-Music Media Library"
        Me.MaterialTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents MaterialTabSelector1 As Microsoft.VisualBasic.Windows.Forms.Controls.MaterialTabSelector
    Friend WithEvents MaterialTabControl1 As Microsoft.VisualBasic.Windows.Forms.Controls.MaterialTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents MaterialSingleLineTextField1 As Microsoft.VisualBasic.Windows.Forms.Controls.MaterialSingleLineTextField
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents ViewAlbums As FlowLayoutPanel
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents ListView1 As ListView
End Class
