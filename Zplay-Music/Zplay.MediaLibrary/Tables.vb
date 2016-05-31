Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Serialization

Namespace Tables

    Public MustInherit Class uid
        Implements IKeyedEntity(Of Integer)

        Public Property uid As Integer Implements IKeyedEntity(Of Integer).Key

    End Class

    Public Class IndexValue : Inherits uid

        Public Property value As String
    End Class

    Public Class Album : Inherits IndexValue
    End Class

    Public Class Artists : Inherits IndexValue
    End Class

    Public Class Genres : Inherits IndexValue
    End Class

    Public Class Music : Inherits uid
        Public Property title As String
        Public Property artists As Integer
        Public Property album As Integer
        Public Property genres As Integer
        ''' <summary>
        ''' Ticks of the time duration
        ''' </summary>
        ''' <returns></returns>
        Public Property length As Integer

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
