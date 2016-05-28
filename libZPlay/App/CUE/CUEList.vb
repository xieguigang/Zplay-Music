Namespace App.CUE

    Public Class CUEList : Inherits Playlist(Of Track)

        Sub New(cue As Cue, eol As Action)
            Call MyBase.New(cue.Tracks,
                            eol,
                            ListTypes.Cue,
                            cue.File.Name)
        End Sub
    End Class
End Namespace
