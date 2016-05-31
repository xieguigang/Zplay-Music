Imports libZPlay
Imports libZPlay.App
Imports libZPlay.App.CUE
Imports Zplay.MediaLibrary

Module Module1

    Sub Main()

        Dim n As New Zplay.MediaLibrary.Tables.Music With {.title = RandomDouble(), .artists = 1}
        Dim engine As New Engine("x:\test.db")

        Call engine.Music.AddNew(n)

        Dim cue As New Cue("C:\Users\xieguigang\Source\Repos\Zplay-Music\media\TEST-LACM-14397.cue")
        Dim play As New ZplayCue

        Call play.Playback("E:\日漫\[160511] TVアニメ「マクロスΔ」OP／ED／挿入歌「一度だけの恋なら／ルンがピカッと光ったら／いけないボーダー\一度だけの恋なら&ルンがピカッと光ったら／ワルキューレ.cue")
        Call play.Playback()

        Pause()
    End Sub
End Module
