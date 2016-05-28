Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization
Imports Microsoft.VisualBasic.Linq

Namespace App.CUE

    ''' <summary>
    ''' Parser for cue list
    ''' </summary>
    Public Class Cue : Inherits ClassObject

        Public Property [Date] As String
        Public Property Genre As String
        Public Property Title As String
        Public Property DiscId As String
        Public Property Comment As String
        Public Property Catalog As String
        Public Property Performer As String
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
                ElseIf key.TextEquals("PERFORMER") Then
                    Performer = s.GetString
                ElseIf key.TextEquals("CATALOG") Then
                    Catalog = s.GetString
                ElseIf key.TextEquals("DISCID") Then
                    DiscId = s.GetString
                ElseIf key.TextEquals("COMMENT") Then
                    Comment = s.GetString
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

            Tracks = Track.TracksParser(
            lines.Skip(CType(i, Integer) - 1) _
                .ToArray(AddressOf Trim)).ToArray
        End Sub
    End Class

    Public Class Track

        Public Property Index As Integer
        Public Property Type As String
        Public Property Title As String
        Public Property Performer As String
        Public Property Index00 As TimeSpan
        Public Property Index01 As TimeSpan
        Public Property ISRC As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

        Public Function HaveIndexed() As Boolean
            Return Index00.TotalMilliseconds > 0 OrElse
                Index01.TotalMilliseconds > 0
        End Function

        Public Function HaveValue() As Boolean
            Return HaveIndexed() OrElse
                Not String.IsNullOrEmpty(Title) OrElse
                Not String.IsNullOrEmpty(Performer) OrElse
                Index > 0 OrElse
                Not String.IsNullOrEmpty(Type)
        End Function

        Public Shared Iterator Function TracksParser(tokens As String()) As IEnumerable(Of Track)
            Dim track As New Track
            Dim key As String
            Dim ts As String()
            Dim idx As Integer

            For Each s As String In tokens
                key = s.Split.First
                s = Mid(s, key.Length + 1).Trim

                If key.TextEquals("Title") Then
                    track.Title = s.GetString
                ElseIf key.TextEquals("Performer") Then
                    track.Performer = s.GetString
                ElseIf key.TextEquals("Index") Then
                    ts = s.Split
                    idx = CTypeDynamic(Of Integer)(ts(Scan0))
                    s = ts(1)

                    If idx = 0 Then
                        track.Index00 = __timeParser(s)
                    Else
                        track.Index01 = __timeParser(s)
                    End If
                ElseIf key.TextEquals("Track") Then
                    If track.HaveValue Then
                        Yield track
                        track = New Track
                    End If

                    ts = s.Split
                    track.Index = CTypeDynamic(Of Integer)(ts(Scan0))
                    track.Type = ts(1)
                ElseIf key.TextEquals("ISRC") Then
                    track.ISRC = s.GetString
                Else
                    Throw New NotImplementedException
                End If
            Next

            Yield track
        End Function

        Private Shared Function __timeParser(s As String) As TimeSpan
            Dim tokens As String() = s.Split(":"c)
            Return New TimeSpan(0, 0,
                CTypeDynamic(Of Integer)(tokens(0)),
                CTypeDynamic(Of Integer)(tokens(1)),
                CTypeDynamic(Of Integer)(tokens(2)))
        End Function
    End Class
End Namespace