Imports System
Imports System.Data.Common
Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports libZPlay.App
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Zplay.MediaLibrary.Tables
Imports Microsoft.VisualBasic
Imports libZPlay.InternalTypes

''' <summary>
''' This is the zplay-Music media library database engine.
''' </summary>
Public Class Engine

    ''' <summary>
    ''' The file path of the media library database file.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Db As String
    ''' <summary>
    ''' The SQLite database engine.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SQLite As SQLProcedure

    Sub New(file As String)
        Dim exists As Boolean = file.FileExists

        Db = file
        SQLite = SQLProcedure.CreateSQLTransaction(url:=Db)

        If Not exists Then  ' 当数据库文件不存在的时候在建立SQLite连接之后初始化数据表
            Call InitDb()
        End If

        Music = New SQLiteIO(Of Music)(SQLite)
        Albums = New SQLiteIO(Of Album)(SQLite)
        Artists = New SQLiteIO(Of Artists)(SQLite)
        Genres = New SQLiteIO(Of Genres)(SQLite)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>这条记录在进行添加的时候是最后进行的</remarks>
    Public ReadOnly Property Music As SQLiteIO(Of Music)
    Public ReadOnly Property Albums As SQLiteIO(Of Album)
    Public ReadOnly Property Artists As SQLiteIO(Of Artists)
    Public ReadOnly Property Genres As SQLiteIO(Of Genres)

    ''' <summary>
    ''' 初始化SQLite数据库里面的数据表
    ''' </summary>
    Public Sub InitDb()
        Call SQLite.CreateTableFor(Of Tables.Music)()
        Call SQLite.CreateTableFor(Of Tables.Album)()
        Call SQLite.CreateTableFor(Of Tables.Artists)()
        Call SQLite.CreateTableFor(Of Tables.Genres)()
    End Sub

    Public Function ScanDIR(DIR As String, Optional recursive As Boolean = True) As MediaFile()
        Dim LQuery As IEnumerable(Of String) = Playlist.GetFiles(DIR, recursive)
        Dim list As New List(Of MediaFile)

        For Each path As String In LQuery
            list += AddFile(path)
        Next

        Return list
    End Function

    Public Function AddFile(path As String) As MediaFile
        Dim info As MediaFile = MediaFile.Create(path)

        ' 创建数据库之中的表的记录
        Dim sql As String

        sql = $"SELECT * FROM {Albums.tableName} WHERE LOWER(value) = '{LCase(info.Id3v2.Album)}' LIMIT 1;"

        Dim album As Album = Albums.Fetch(sql).FirstOrDefault      ' album exists
        If album Is Nothing Then
            album = New Album With {
                .uid = Albums.GetMaxId + 1,
                .value = info.Id3v2.Album
            }
            Call Albums.AddNew(album)
        End If

        sql = $"SELECT * FROM {Artists.tableName} WHERE LOWER(value) = '{LCase(info.Id3v2.Artist)}' LIMIT 1;"
        Dim artist As Artists = Artists.Fetch(sql).FirstOrDefault
        If artist Is Nothing Then
            artist = New Artists With {
                .uid = Artists.GetMaxId + 1,
                .value = info.Id3v2.Artist
            }
            Call Artists.AddNew(artist)
        End If

        sql = $"SELECT * FROM {Genres.tableName} WHERE LOWER(value) = '{LCase(info.Id3v2.Genre)}' LIMIT 1;"
        Dim genre As Genres = Genres.Fetch(sql).FirstOrDefault
        If genre Is Nothing Then
            genre = New Genres With {
                .uid = Genres.GetMaxId + 1,
                .value = info.Id3v2.Genre
            }
            Call Genres.AddNew(genre)
        End If

        Dim media As New Music With {
            .path = path.GetFullPath,
            .title = info.Id3v2.Title,
            .length = info.StreamInfo.Length.ms,
            .album = album.uid,
            .genres = genre.uid,
            .artists = genre.uid,
            .uid = Music.GetMaxId + 1
        }

        Call Music.AddNew(media)

        Return info
    End Function

    ''' <summary>
    ''' SQL SELECT query
    ''' </summary>
    ''' <param name="query">这个Query只针对<see cref="Music"/>表进行查询</param>
    ''' <returns></returns>
    Public Iterator Function QueryFile(query As String) As IEnumerable(Of MediaFile)
        Dim files As IEnumerable(Of Music) = Music.Fetch(SQL:=query)

        For Each file As Music In files
            Dim media As New MediaFile With {
                .FileName = file.path,
                .Id3v2 = New TID3InfoEx With {
                    .Album = __getValue(file.album, Albums),
                    .Artist = __getValue(file.artists, Artists),
                    .Genre = __getValue(file.genres, Genres),
                    .Title = file.title
                },
                .StreamInfo = New TStreamInfo With {
                    .Length = New TStreamTime With {
                        .ms = file.length
                    }
                }
            }

            Yield media
        Next
    End Function

    Private Function __getValue(Of T As IndexValue)(uid As Long, engine As SQLiteIO(Of T)) As String
        Dim x As T = engine.GetByKey(uid)
        If x Is Nothing Then
            Return ""
        Else
            Return x.value
        End If
    End Function

    Private Function __like(Of T As uid)(engine As SQLiteIO(Of T), key As String, query As String) As String
        Dim SQL As String = $"SELECT * FROM {engine.tableName} WHERE LOWER({key}) LIKE '%{LCase(query)}%';"
        Return SQL
    End Function

    Public Iterator Function QueryByTitle(title As String) As IEnumerable(Of MediaFile)
        Dim SQL As String = __like(Music, "title", title)

        For Each file As MediaFile In QueryFile(SQL)
            Yield file
        Next
    End Function

    Public Overrides Function ToString() As String
        Return Db.ToFileURL
    End Function
End Class
