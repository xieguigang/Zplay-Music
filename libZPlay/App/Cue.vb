Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization

Public Class Cue : Inherits ClassObject

    Public Property [Date] As String
    Public Property Genre As String
    Public Property Title As String
    Public Property File As NamedValue(Of String)
    Public Property Tracks As Track()

    Sub New(cue As String)
        Dim lines As String() = cue.ReadAllLines
        Dim i As New Pointer(Scan0)
        Dim s As String = Nothing
        Dim key As String

        Do While lines(++i).ShadowCopy(s).FirstOrDefault <> " "c
            If InStr(s, "REM", CompareMethod.Text) = 1 Then
                s = Mid(s, 5)
            End If
            key = s.Split.First
            s = Mid(s, key.Length + 1).Trim

            If key.TextEquals("DATE") Then
                [Date] = s.GetString
            ElseIf key.TextEquals("GENRE") Then
                Genre = s.GetString
            ElseIf key.TextEquals("TITLE") Then
                Title = s.GetString
            ElseIf key.TextEquals("FILE") Then
                Dim tokens As String() = CommandLine.GetTokens(s)
                File = New NamedValue(Of String)(
                    tokens(Scan0),
                    tokens(1))
            End If
        Loop

        Tracks = Track.TracksParser(lines.Skip(i).ToArray)
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

    Public Shared Function TracksParser(tokens As String()) As Track()

    End Function
End Class