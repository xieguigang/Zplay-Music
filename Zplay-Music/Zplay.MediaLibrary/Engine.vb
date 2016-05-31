Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Zplay.MediaLibrary.Tables

Public Class Engine
    Implements IRepository(Of Integer, Album)
    Implements IRepository(Of Integer, Artists)
    Implements IRepository(Of Integer, Genres)
    Implements IRepository(Of Integer, Music)

    Public ReadOnly Property Db As String
    Public ReadOnly Property SQLite As SQLProcedure

    Sub New(file As String)
        Db = file
        SQLite = SQLProcedure.CreateSQLTransaction(url:=Db)
    End Sub

    Public Overrides Function ToString() As String
        Return Db.ToFileURL
    End Function

    Public Function Exists(key As Integer) As Boolean Implements IRepositoryRead(Of Integer, Album).Exists
        Throw New NotImplementedException()
    End Function

    Public Function GetByKey(key As Integer) As Album Implements IRepositoryRead(Of Integer, Album).GetByKey
        Throw New NotImplementedException()
    End Function

    Public Function GetWhere(clause As Func(Of Album, Boolean)) As IReadOnlyDictionary(Of Integer, Album) Implements IRepositoryRead(Of Integer, Album).GetWhere
        Throw New NotImplementedException()
    End Function

    Public Function GetAll() As IReadOnlyDictionary(Of Integer, Album) Implements IRepositoryRead(Of Integer, Album).GetAll
        Throw New NotImplementedException()
    End Function

    Public Sub Delete(key As Integer) Implements IRepositoryWrite(Of Integer, Album).Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub AddOrUpdate(entity As Album, key As Integer) Implements IRepositoryWrite(Of Integer, Album).AddOrUpdate
        Throw New NotImplementedException()
    End Sub

    Public Function AddNew(entity As Album) As Integer Implements IRepositoryWrite(Of Integer, Album).AddNew
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_Exists(key As Integer) As Boolean Implements IRepositoryRead(Of Integer, Artists).Exists
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetByKey(key As Integer) As Artists Implements IRepositoryRead(Of Integer, Artists).GetByKey
        Throw New NotImplementedException()
    End Function

    Public Function GetWhere(clause As Func(Of Artists, Boolean)) As IReadOnlyDictionary(Of Integer, Artists) Implements IRepositoryRead(Of Integer, Artists).GetWhere
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetAll() As IReadOnlyDictionary(Of Integer, Artists) Implements IRepositoryRead(Of Integer, Artists).GetAll
        Throw New NotImplementedException()
    End Function

    Private Sub IRepositoryWrite_Delete(key As Integer) Implements IRepositoryWrite(Of Integer, Artists).Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub AddOrUpdate(entity As Artists, key As Integer) Implements IRepositoryWrite(Of Integer, Artists).AddOrUpdate
        Throw New NotImplementedException()
    End Sub

    Public Function AddNew(entity As Artists) As Integer Implements IRepositoryWrite(Of Integer, Artists).AddNew
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_Exists1(key As Integer) As Boolean Implements IRepositoryRead(Of Integer, Genres).Exists
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetByKey1(key As Integer) As Genres Implements IRepositoryRead(Of Integer, Genres).GetByKey
        Throw New NotImplementedException()
    End Function

    Public Function GetWhere(clause As Func(Of Genres, Boolean)) As IReadOnlyDictionary(Of Integer, Genres) Implements IRepositoryRead(Of Integer, Genres).GetWhere
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetAll1() As IReadOnlyDictionary(Of Integer, Genres) Implements IRepositoryRead(Of Integer, Genres).GetAll
        Throw New NotImplementedException()
    End Function

    Private Sub IRepositoryWrite_Delete1(key As Integer) Implements IRepositoryWrite(Of Integer, Genres).Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub AddOrUpdate(entity As Genres, key As Integer) Implements IRepositoryWrite(Of Integer, Genres).AddOrUpdate
        Throw New NotImplementedException()
    End Sub

    Public Function AddNew(entity As Genres) As Integer Implements IRepositoryWrite(Of Integer, Genres).AddNew
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_Exists2(key As Integer) As Boolean Implements IRepositoryRead(Of Integer, Music).Exists
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetByKey2(key As Integer) As Music Implements IRepositoryRead(Of Integer, Music).GetByKey
        Throw New NotImplementedException()
    End Function

    Public Function GetWhere(clause As Func(Of Music, Boolean)) As IReadOnlyDictionary(Of Integer, Music) Implements IRepositoryRead(Of Integer, Music).GetWhere
        Throw New NotImplementedException()
    End Function

    Private Function IRepositoryRead_GetAll2() As IReadOnlyDictionary(Of Integer, Music) Implements IRepositoryRead(Of Integer, Music).GetAll
        Throw New NotImplementedException()
    End Function

    Private Sub IRepositoryWrite_Delete2(key As Integer) Implements IRepositoryWrite(Of Integer, Music).Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub AddOrUpdate(entity As Music, key As Integer) Implements IRepositoryWrite(Of Integer, Music).AddOrUpdate
        Throw New NotImplementedException()
    End Sub

    Public Function AddNew(entity As Music) As Integer Implements IRepositoryWrite(Of Integer, Music).AddNew
        Throw New NotImplementedException()
    End Function
End Class
