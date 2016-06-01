Imports System.Data.Common
Imports System.Data.SQLite.Linq.DataMapping.Interface
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Zplay.MediaLibrary.Tables

Public Class SQLiteIO(Of T As uid)
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
        Try
            For Each x As T In Engine.Load(Of T)(SQL)
                Yield x
            Next
        Catch ex As Exception
            ex = New Exception(SQL, ex)
            Call App.LogException(ex)
        End Try
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
        Dim SQL As String = String.Format(SQL_SELECT, tableName, key)
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
