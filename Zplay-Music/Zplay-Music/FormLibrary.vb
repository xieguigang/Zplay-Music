Imports libZPlay.App
Imports Microsoft.VisualBasic.Windows.Forms
Imports Zplay.MediaLibrary

Public Class FormLibrary

    ReadOnly materialSkinManager As MaterialSkinManager

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Initialize MaterialSkinManager
        materialSkinManager = MaterialSkinManager.Instance
        materialSkinManager.AddFormToManage(Me)
        materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT
        materialSkinManager.ColorScheme = New ColorScheme(
            primary:=Primary.LightGreen300,
            darkPrimary:=Primary.LightGreen300,
            lightPrimary:=Primary.LightGreen300,
            accent:=Accent.LightBlue400,
            textShade:=TextShade.BLACK)
    End Sub

    Dim host As FormZplay

    Sub New(host As FormZplay)
        Call Me.New
        Me.host = host
        ListView1.host = host
    End Sub

    Private Sub FormLibrary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using engine As New Engine(Config.MediaLibrary)
            Call engine.CreateAlbumViews(ViewAlbums, Sub(album As Album)
                                                         MaterialTabControl1.SelectedIndex = 3
                                                         Call ListView1.ShowData(album, Sub()
                                                                                            MaterialTabControl1.SelectedIndex = 1
                                                                                        End Sub)
                                                     End Sub)
        End Using
    End Sub
End Class