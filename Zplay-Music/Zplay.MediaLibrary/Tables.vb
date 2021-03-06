﻿Imports System.Data.Linq.Mapping
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Tables

    Public MustInherit Class uid
        Implements IKeyedEntity(Of Long)

        <Column(Name:="uid", DbType:="int", IsPrimaryKey:=True)>
        Public Property uid As Long Implements IKeyedEntity(Of Long).Key

    End Class

    Public Class IndexValue : Inherits uid

        <Column(Name:="value", DbType:="varchar(256)")>
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

        <Column(Name:=NameOf(title), DbType:="varchar(256)")>
        Public Property title As String
        <Column(Name:=NameOf(artists), DbType:="int")>
        Public Property artists As Long
        <Column(Name:=NameOf(album), DbType:="int")>
        Public Property album As Long
        <Column(Name:=NameOf(genres), DbType:="int")>
        Public Property genres As Long
        ''' <summary>
        ''' Ticks of the time duration
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Column(Name:=NameOf(length), DbType:="int")>
        Public Property length As Long
        <Column(Name:=NameOf(path), DbType:="varchar(512)")>
        Public Property path As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
