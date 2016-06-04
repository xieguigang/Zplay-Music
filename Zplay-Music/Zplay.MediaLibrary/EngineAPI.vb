Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports libZPlay.App
Imports Microsoft.VisualBasic

Public Module EngineAPI

    <Extension>
    Public Iterator Function GetAlbums(engine As Engine) As IEnumerable(Of Album)
        Dim all = engine.Albums.GetAll.Values

        For Each x In __getLists(Of Tables.Album)(engine, all, NameOf(Tables.Music.album))
            Yield x
        Next
    End Function

    Private Iterator Function __getLists(Of T As Tables.IndexValue)(engine As Engine, all As IEnumerable(Of T), field As String) As IEnumerable(Of Album)
        For Each album As Tables.IndexValue In all
            Dim files = engine.QueryFile($"SELECT * FROM {engine.Music.tableName} where {field} = '{album.uid}';")

            Yield New Album With {
                .list = New List(Of MediaFile)(files),
                .Name = album.value
            }
        Next
    End Function

    <Extension>
    Public Sub CreateAlbumViews(engine As Engine, ByRef target As FlowLayoutPanel, onClick As Action(Of Album))
        For Each album As Album In engine.GetAlbums.Where(Function(x) x.list.Count > 0)
            Dim view As New AlbumView With {
                .ArtImage = album.list.First.Id3v2.Picture.Bitmap,
                .Text = album.Name,
                .Info = album.list.Count & " songs..."
            }
            Call target.Controls.Add(view)
            Call Application.DoEvents()

            AddHandler view.OnClick, Sub() Call onClick(album)
        Next
    End Sub

    <Extension>
    Public Sub CreateArtistViews(engine As Engine, ByRef target As FlowLayoutPanel, onClick As Action(Of Album))
        Dim all = engine.Artists.GetAll.Values

        For Each album As Album In __getLists(Of Tables.Artists)(engine, all, NameOf(Tables.Music.artists)).Where(Function(x) x.list.Count > 0)
            Dim view As New AlbumView With {
                .ArtImage = album.list.First.Id3v2.Picture.Bitmap,
                .Text = album.Name,
                .Info = album.list.Count & " songs..."
            }
            Call target.Controls.Add(view)
            Call Application.DoEvents()

            AddHandler view.OnClick, Sub() Call onClick(album)
        Next
    End Sub

    <Extension>
    Public Sub CreateGenresViews(engine As Engine, ByRef target As FlowLayoutPanel, onClick As Action(Of Album))
        Dim all = engine.Genres.GetAll.Values

        For Each album As Album In __getLists(Of Tables.Genres)(engine, all, NameOf(Tables.Music.genres)).Where(Function(x) x.list.Count > 0)
            Dim view As New AlbumView With {
                .ArtImage = album.list.First.Id3v2.Picture.Bitmap,
                .Text = album.Name,
                .Info = album.list.Count & " songs..."
            }
            Call target.Controls.Add(view)
            Call Application.DoEvents()

            AddHandler view.OnClick, Sub() Call onClick(album)
        Next
    End Sub
End Module
