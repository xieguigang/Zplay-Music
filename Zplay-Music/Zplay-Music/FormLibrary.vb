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
            primary:=Primary.Blue300,
            darkPrimary:=Primary.Blue300,
            lightPrimary:=Primary.Blue300,
            accent:=Accent.Amber100,
            textShade:=TextShade.WHITE)
    End Sub

    Dim host As FormZplay

    Sub New(host As FormZplay)
        Call Me.New
        Me.host = host
        ListView1.host = host
    End Sub

    Private Sub FormLibrary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using engine As New Engine(Config.MediaLibrary)
            Call engine.CreateAlbumViews(ViewAlbums, Sub(album) Call ShowInfo(album, 1))
            Call engine.CreateArtistViews(ViewsArtists, Sub(album) Call ShowInfo(album, 0))
            Call engine.CreateGenresViews(viewsGenres, Sub(album) Call ShowInfo(album, 2))
        End Using
    End Sub

    Private Sub ShowInfo(album As Album, index As Integer)
        MaterialTabControl1.SelectedIndex = 3
        Call ListView1.ShowData(album, Sub()
                                           MaterialTabControl1.SelectedIndex = index
                                       End Sub)
    End Sub
End Class