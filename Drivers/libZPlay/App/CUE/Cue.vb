Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Linq
Imports libZPlay.InternalTypes

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
            Dim str As New Value(Of String)
            Dim key As String

            Do While (str = lines(++i)).FirstOrDefault <> " "c
                With str.value
                    If InStr(.ref, "REM", CompareMethod.Text) = 1 Then
                        str.value = Mid(.ref, 5)
                    End If
                    key = str.value.Split.First
                    str.value = Mid(str, key.Length + 1).Trim
                End With

                With str.value
                    If key.TextEquals("DATE") Then
                        [Date] = .GetString
                    ElseIf key.TextEquals("PERFORMER") Then
                        Performer = .GetString
                    ElseIf key.TextEquals("CATALOG") Then
                        Catalog = .GetString
                    ElseIf key.TextEquals("DISCID") Then
                        DiscId = .GetString
                    ElseIf key.TextEquals("COMMENT") Then
                        Comment = .GetString
                    ElseIf key.TextEquals("GENRE") Then
                        Genre = .GetString
                    ElseIf key.TextEquals("TITLE") Then
                        Title = .GetString
                    ElseIf key.TextEquals("FILE") Then
                        Dim tokens As String() = CommandLine.GetTokens(.ref)
                        File = New NamedValue(Of String)(
                            tokens(Scan0),
                            tokens(1))
                    End If
                End With
            Loop

            Tracks = Track.TracksParser(
            lines.Skip(CType(i, Integer) - 1) _
                .ToArray(AddressOf Trim)).ToArray
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="track"></param>
        ''' <param name="total">文件的总长度</param>
        ''' <returns></returns>
        Public Function GetLength(track As Integer, total As TStreamInfo) As TimeSpan
            Dim [next] As TimeSpan

            If track = Tracks.Length - 1 Then
                [next] = New TimeSpan(0,
                                      total.Length.hms.hour,
                                      total.Length.hms.minute,
                                      total.Length.hms.second,
                                      total.Length.hms.millisecond)
            Else
                [next] = Tracks(track + 1).Index00
            End If

            Dim cur As Track = Tracks(track)
            Dim ts = [next] - cur.Index01
            Return ts
        End Function
    End Class

    Public Class Track

        Public Property Index As Integer
        Public Property Type As String
        Public Property Title As String
        Public Property Performer As String
        Public Property Index00 As TimeSpan
        ''' <summary>
        ''' Track start
        ''' </summary>
        ''' <returns></returns>
        Public Property Index01 As TimeSpan
        Public Property ISRC As String

        Public Function GetTrackStart() As TStreamTime
            Return New TStreamTime With {
                .hms = New TStreamHMSTime With {
                    .hour = Index01.Hours,
                    .millisecond = Index01.Milliseconds,
                    .minute = Index01.Minutes,
                    .second = Index01.Seconds
                }
            }
        End Function

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