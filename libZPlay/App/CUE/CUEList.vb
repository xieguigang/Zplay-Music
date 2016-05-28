Namespace App.CUE

    Public Class CUEList : Inherits Playlist(Of Track)

        Public ReadOnly Property CUE As Cue

        Sub New(cue As Cue, eol As Action)
            Call MyBase.New(cue.Tracks,
                            eol,
                            ListTypes.Cue,
                            cue.File.Name)
        End Sub
    End Class
End Namespace
