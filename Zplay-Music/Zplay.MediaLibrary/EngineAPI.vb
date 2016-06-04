Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports libZPlay.App
Imports Microsoft.VisualBasic

Public Module EngineAPI

    <Extension>
    Public Iterator Function GetAlbums(engine As Engine) As IEnumerable(Of Album)
        Dim all = engine.Albums.GetAll.Values

        For Each album As Tables.Album In all
            Dim files = engine.QueryFile($"SELECT * FROM {engine.Music.tableName} where album = '{album.uid}';")

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
End Module
