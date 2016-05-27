<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormZplay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormZplay))
        Me.picDocker = New System.Windows.Forms.PictureBox()
        Me.btnCloselist = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbTitle = New System.Windows.Forms.Label()
        Me.lbArtist = New System.Windows.Forms.Label()
        Me.lbTime = New System.Windows.Forms.Label()
        Me.picAlbumArt = New System.Windows.Forms.PictureBox()
        Me.btnPlayList = New System.Windows.Forms.PictureBox()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PreviousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlaypauseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlaybackModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShuffleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoopsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFolderToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenPlaylistToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picDocker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCloselist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.picAlbumArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPlayList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picDocker
        '
        Me.picDocker.BackgroundImage = Global.ZplayMusic.My.Resources.Resources.Docker_Expand
        Me.picDocker.Location = New System.Drawing.Point(541, 507)
        Me.picDocker.Name = "picDocker"
        Me.picDocker.Size = New System.Drawing.Size(22, 115)
        Me.picDocker.TabIndex = 4
        Me.picDocker.TabStop = False
        '
        'btnCloselist
        '
        Me.btnCloselist.BackgroundImage = CType(resources.GetObject("btnCloselist.BackgroundImage"), System.Drawing.Image)
        Me.btnCloselist.Location = New System.Drawing.Point(507, 0)
        Me.btnCloselist.Name = "btnCloselist"
        Me.btnCloselist.Size = New System.Drawing.Size(34, 21)
        Me.btnCloselist.TabIndex = 5
        Me.btnCloselist.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.Controls.Add(Me.btnCloselist)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(541, 507)
        Me.Panel1.TabIndex = 6
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.BackgroundImage = Global.ZplayMusic.My.Resources.Resources.Numbers
        Me.PictureBox1.Location = New System.Drawing.Point(479, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 28)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.lbTitle)
        Me.Panel2.Controls.Add(Me.lbArtist)
        Me.Panel2.Controls.Add(Me.lbTime)
        Me.Panel2.Controls.Add(Me.picAlbumArt)
        Me.Panel2.Controls.Add(Me.btnPlayList)
        Me.Panel2.Location = New System.Drawing.Point(0, 507)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(541, 115)
        Me.Panel2.TabIndex = 7
        '
        'lbTitle
        '
        Me.lbTitle.AutoSize = True
        Me.lbTitle.BackColor = System.Drawing.Color.Black
        Me.lbTitle.ForeColor = System.Drawing.Color.White
        Me.lbTitle.Location = New System.Drawing.Point(106, 10)
        Me.lbTitle.Name = "lbTitle"
        Me.lbTitle.Size = New System.Drawing.Size(39, 13)
        Me.lbTitle.TabIndex = 4
        Me.lbTitle.Text = "Label1"
        '
        'lbArtist
        '
        Me.lbArtist.AutoSize = True
        Me.lbArtist.BackColor = System.Drawing.Color.Black
        Me.lbArtist.ForeColor = System.Drawing.Color.DimGray
        Me.lbArtist.Location = New System.Drawing.Point(106, 32)
        Me.lbArtist.Name = "lbArtist"
        Me.lbArtist.Size = New System.Drawing.Size(39, 13)
        Me.lbArtist.TabIndex = 3
        Me.lbArtist.Text = "Label1"
        '
        'lbTime
        '
        Me.lbTime.AutoSize = True
        Me.lbTime.BackColor = System.Drawing.Color.Black
        Me.lbTime.ForeColor = System.Drawing.Color.DimGray
        Me.lbTime.Location = New System.Drawing.Point(106, 58)
        Me.lbTime.Name = "lbTime"
        Me.lbTime.Size = New System.Drawing.Size(39, 13)
        Me.lbTime.TabIndex = 2
        Me.lbTime.Text = "Label1"
        '
        'picAlbumArt
        '
        Me.picAlbumArt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAlbumArt.Location = New System.Drawing.Point(10, 10)
        Me.picAlbumArt.Name = "picAlbumArt"
        Me.picAlbumArt.Size = New System.Drawing.Size(90, 90)
        Me.picAlbumArt.TabIndex = 1
        Me.picAlbumArt.TabStop = False
        '
        'btnPlayList
        '
        Me.btnPlayList.BackColor = System.Drawing.Color.Black
        Me.btnPlayList.BackgroundImage = CType(resources.GetObject("btnPlayList.BackgroundImage"), System.Drawing.Image)
        Me.btnPlayList.Location = New System.Drawing.Point(455, 0)
        Me.btnPlayList.Name = "btnPlayList"
        Me.btnPlayList.Size = New System.Drawing.Size(23, 28)
        Me.btnPlayList.TabIndex = 0
        Me.btnPlayList.TabStop = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Zplay-Music"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreviousToolStripMenuItem, Me.PlaypauseToolStripMenuItem, Me.NextToolStripMenuItem, Me.PlaybackModeToolStripMenuItem, Me.ToolStripMenuItem2, Me.OpenFolderToolStripMenuItem1, Me.OpenPlaylistToolStripMenuItem, Me.ToolStripMenuItem1, Me.AboutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 192)
        '
        'PreviousToolStripMenuItem
        '
        Me.PreviousToolStripMenuItem.Image = CType(resources.GetObject("PreviousToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PreviousToolStripMenuItem.Name = "PreviousToolStripMenuItem"
        Me.PreviousToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.PreviousToolStripMenuItem.Text = "Previous"
        '
        'PlaypauseToolStripMenuItem
        '
        Me.PlaypauseToolStripMenuItem.Image = CType(resources.GetObject("PlaypauseToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PlaypauseToolStripMenuItem.Name = "PlaypauseToolStripMenuItem"
        Me.PlaypauseToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.PlaypauseToolStripMenuItem.Text = "Play/pause"
        '
        'NextToolStripMenuItem
        '
        Me.NextToolStripMenuItem.Image = CType(resources.GetObject("NextToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NextToolStripMenuItem.Name = "NextToolStripMenuItem"
        Me.NextToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.NextToolStripMenuItem.Text = "Next"
        '
        'PlaybackModeToolStripMenuItem
        '
        Me.PlaybackModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrderToolStripMenuItem, Me.ShuffleToolStripMenuItem, Me.LoopsToolStripMenuItem})
        Me.PlaybackModeToolStripMenuItem.Name = "PlaybackModeToolStripMenuItem"
        Me.PlaybackModeToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.PlaybackModeToolStripMenuItem.Text = "Playback Mode"
        '
        'OrderToolStripMenuItem
        '
        Me.OrderToolStripMenuItem.CheckOnClick = True
        Me.OrderToolStripMenuItem.Name = "OrderToolStripMenuItem"
        Me.OrderToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.OrderToolStripMenuItem.Text = "Order"
        '
        'ShuffleToolStripMenuItem
        '
        Me.ShuffleToolStripMenuItem.CheckOnClick = True
        Me.ShuffleToolStripMenuItem.Name = "ShuffleToolStripMenuItem"
        Me.ShuffleToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.ShuffleToolStripMenuItem.Text = "Shuffle"
        '
        'LoopsToolStripMenuItem
        '
        Me.LoopsToolStripMenuItem.CheckOnClick = True
        Me.LoopsToolStripMenuItem.Name = "LoopsToolStripMenuItem"
        Me.LoopsToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.LoopsToolStripMenuItem.Text = "Loops"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(173, 6)
        '
        'OpenFolderToolStripMenuItem1
        '
        Me.OpenFolderToolStripMenuItem1.Name = "OpenFolderToolStripMenuItem1"
        Me.OpenFolderToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.OpenFolderToolStripMenuItem1.Text = "Open Folder"
        '
        'OpenPlaylistToolStripMenuItem
        '
        Me.OpenPlaylistToolStripMenuItem.Name = "OpenPlaylistToolStripMenuItem"
        Me.OpenPlaylistToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.OpenPlaylistToolStripMenuItem.Text = "Open Playlist"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(173, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.AboutToolStripMenuItem.Text = "About Zplay-Music"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'FormZplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGreen
        Me.ClientSize = New System.Drawing.Size(563, 622)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.picDocker)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormZplay"
        Me.Opacity = 0.95R
        Me.Text = "Zplay-Music"
        Me.TransparencyKey = System.Drawing.Color.LightGreen
        CType(Me.picDocker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCloselist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.picAlbumArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPlayList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picDocker As PictureBox
    Friend WithEvents btnCloselist As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnPlayList As PictureBox
    Friend WithEvents picAlbumArt As PictureBox
    Friend WithEvents lbTime As Label
    Friend WithEvents lbTitle As Label
    Friend WithEvents lbArtist As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents PreviousToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlaypauseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlaybackModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OrderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShuffleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoopsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFolderToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenPlaylistToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents PictureBox1 As PictureBox
End Class
