Imports System.Data.Linq.Mapping
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Serialization

Namespace Tables

    Public MustInherit Class uid
        Implements IKeyedEntity(Of Integer)

        <Column(Name:="uid", DbType:="int", IsPrimaryKey:=True)>
        Public Property uid As Integer Implements IKeyedEntity(Of Integer).Key

    End Class

    Public Class IndexValue : Inherits uid

        <Column(Name:="value", DbType:="varchar(2048)")>
        Public Property value As String
    End Class

    <Table(Name:="albums")>
    Public Class Album : Inherits IndexValue
    End Class

    <Table(Name:="artists")>
    Public Class Artists : Inherits IndexValue
    End Class

    <Table(Name:="genres")>
    Public Class Genres : Inherits IndexValue
    End Class

    <Table(Name:="file")>
    Public Class Music : Inherits uid

        <Column(Name:=NameOf(title), DbType:="varchar(512)")>
        Public Property title As String
        <Column(Name:=NameOf(artists), DbType:="varchar(512)")>
        Public Property artists As Integer
        <Column(Name:=NameOf(album), DbType:="varchar(512)")>
        Public Property album As Integer
        <Column(Name:=NameOf(genres), DbType:="varchar(512)")>
        Public Property genres As Integer
        ''' <summary>
        ''' Ticks of the time duration
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Column(Name:=NameOf(length), DbType:="varchar(512)")>
        Public Property length As Integer

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
