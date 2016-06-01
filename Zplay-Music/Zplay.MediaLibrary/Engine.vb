Imports System
Imports System.Data.Common
Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports libZPlay.App
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Zplay.MediaLibrary.Tables
Imports Microsoft.VisualBasic

Public Class Engine

    Public ReadOnly Property Db As String
    Public ReadOnly Property SQLite As SQLProcedure

    Sub New(file As String)
        Dim exists As Boolean = file.FileExists

        Db = file
        SQLite = SQLProcedure.CreateSQLTransaction(url:=Db)

        If Not exists Then  ' 当数据库文件不存在的时候在建立SQLite连接之后初始化数据表
            Call InitDb()
        End If

        Music = New ObjectIO(Of Music)(SQLite)
        Albums = New ObjectIO(Of Album)(SQLite)
        Artists = New ObjectIO(Of Artists)(SQLite)
        Genres = New ObjectIO(Of Genres)(SQLite)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>这条记录在进行添加的时候是最后进行的</remarks>
    Public ReadOnly Property Music As ObjectIO(Of Music)
    Public ReadOnly Property Albums As ObjectIO(Of Album)
    Public ReadOnly Property Artists As ObjectIO(Of Artists)
    Public ReadOnly Property Genres As ObjectIO(Of Genres)

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
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Iterator Function QueryFile(query As String) As IEnumerable(Of MediaFile)
        Dim files As IEnumerable(Of Music) = Music.Fetch(SQL:=query)

        For Each file As Music In files

        Next
    End Function

    Public Overrides Function ToString() As String
        Return Db.ToFileURL
    End Function
End Class

Public Class ObjectIO(Of T As uid)
    Implements IRepository(Of Long, T)

    Public ReadOnly Property Engine As SQLProcedure
    Public ReadOnly Property tableName As String = GetType(T).GetTableName
    Public ReadOnly Property Schema As TableSchema

    Sub New(sqlite As SQLProcedure)
        Engine = sqlite
        Schema = New TableSchema(GetType(T))

        _maxID_SQL = $"SELECT MAX({NameOf(uid.uid)}) FROM {tableName};"
    End Sub

    ReadOnly _maxID_SQL As String

    Public Function GetMaxId() As Long
        Dim reader = Engine.Execute(_maxID_SQL)

        If reader.HasRows Then
            Call reader.Read()

            Dim v As Object = reader.GetValue(Scan0)

            If IsDBNull(v) Then
                Return 0
            Else
                Dim n As Long =
                    DirectCast(v, Long)
                Return n
            End If
        Else
            Return 0
        End If
    End Function

    Public Overrides Function ToString() As String
        Return Engine.ToString
    End Function

    Public Sub AddOrUpdate(entity As T, key As Long) Implements IRepositoryWrite(Of Long, T).AddOrUpdate
        If Exists(key) Then
            Call Engine.Update(entity)
        Else
            Call Engine.Insert(entity)
        End If
    End Sub

    Const SQL_DELETE As String = "DELETE FROM {0} where uid='{1}';"

    Public Sub Delete(key As Long) Implements IRepositoryWrite(Of Long, T).Delete
        Dim SQL As String = String.Format(SQL_DELETE, tableName, key)
        Call Engine.Execute(SQL)
    End Sub

    Public Iterator Function Fetch(SQL As String) As IEnumerable(Of T)
        For Each x As T In Engine.Load(Of T)(SQL)
            Yield x
        Next
    End Function

    Public Function AddNew(entity As T) As Long Implements IRepositoryWrite(Of Long, T).AddNew
        Return Engine.Insert(Schema, entity)
    End Function

    Public Function Exists(key As Long) As Boolean Implements IRepositoryRead(Of Long, T).Exists
        Return Engine.RecordExists(Schema, key.ToString)
    End Function

    Public Function GetAll() As IReadOnlyDictionary(Of Long, T) Implements IRepositoryRead(Of Long, T).GetAll
        Dim SQL As String = $"SELECT * FROM {tableName};"
        Dim reader As DbDataReader = Engine.Execute(SQL)
        Dim buf As IEnumerable(Of T) = Engine.Load(Of T)
        Dim out As Dictionary(Of Long, T) = buf.ToDictionary(Function(x) x.uid)

        Return out
    End Function

    Const SQL_SELECT As String = "SELECT * FROM {0} where uid = '{1}';"

    Public Function GetByKey(key As Long) As T Implements IRepositoryRead(Of Long, T).GetByKey
        Dim SQL As String = String.Format(SQL_SELECT, key)
        Return Engine.Load(Of T)(SQL).FirstOrDefault
    End Function

    Public Function GetWhere(clause As Func(Of T, Boolean)) As IReadOnlyDictionary(Of Long, T) Implements IRepositoryRead(Of Long, T).GetWhere
        Dim result As New Dictionary(Of Long, T)

        For Each x As T In Engine.Load(Of T)()
            If clause(x) = True Then
                Call result.Add(x.uid, x)
            End If
        Next

        Return result
    End Function
End Class