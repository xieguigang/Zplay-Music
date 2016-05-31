Imports System.Data.Common
Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Zplay.MediaLibrary.Tables

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

    Public Overrides Function ToString() As String
        Return Db.ToFileURL
    End Function
End Class

Public Class ObjectIO(Of T As uid)
    Implements IRepository(Of Integer, T)

    Public ReadOnly Property Engine As SQLProcedure
    Public ReadOnly Property tableName As String = GetType(T).GetTableName

    Sub New(sqlite As SQLProcedure)
        Engine = sqlite
    End Sub

    Public Overrides Function ToString() As String
        Return Engine.ToString
    End Function

    Public Sub AddOrUpdate(entity As T, key As Integer) Implements IRepositoryWrite(Of Integer, T).AddOrUpdate
        Throw New NotImplementedException()
    End Sub

    Const SQL_DELETE As String = "DELETE FROM {0} where uid='{1}';"

    Public Sub Delete(key As Integer) Implements IRepositoryWrite(Of Integer, T).Delete
        Dim SQL As String = String.Format(SQL_DELETE, tableName, key)
        Call Engine.Execute(SQL)
    End Sub

    Public Function AddNew(entity As T) As Integer Implements IRepositoryWrite(Of Integer, T).AddNew
        Return Engine.Insert(entity)
    End Function

    Public Function Exists(key As Integer) As Boolean Implements IRepositoryRead(Of Integer, T).Exists
        Throw New NotImplementedException()
    End Function

    Public Function GetAll() As IReadOnlyDictionary(Of Integer, T) Implements IRepositoryRead(Of Integer, T).GetAll
        Dim SQL As String = $"SELECT * FROM {tableName};"
        Dim reader As DbDataReader = Engine.Execute(SQL)

    End Function

    Const SQL_SELECT As String = "SELECT * FROM {0} where uid = '{1}';"

    Public Function GetByKey(key As Integer) As T Implements IRepositoryRead(Of Integer, T).GetByKey
        Dim SQL As String = String.Format(SQL_SELECT, key)

    End Function

    Public Function GetWhere(clause As Func(Of T, Boolean)) As IReadOnlyDictionary(Of Integer, T) Implements IRepositoryRead(Of Integer, T).GetWhere
        Throw New NotImplementedException()
    End Function
End Class