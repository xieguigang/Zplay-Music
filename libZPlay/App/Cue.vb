Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization

Public Class Cue : Inherits ClassObject

    Public Property [Date] As String
    Public Property Genre As String
    Public Property Title As String
    Public Property File As NamedValue(Of String)
    Public Property Tracks As Track()

    Sub New(cue As String)

    End Sub
End Class

Public Class Track

    Public Property Index As Integer
    Public Property Type As String
    Public Property Title As String
    Public Property Performer As String
    Public Property Index00 As TimeSpan
    Public Property Index01 As TimeSpan

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class