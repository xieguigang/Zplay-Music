Imports libZPlay
Imports libZPlay.App
Imports libZPlay.App.CUE
Imports Zplay.MediaLibrary

Module Module1

    Sub Main()

        Dim n As New Zplay.MediaLibrary.Tables.Music With {.title = RandomDouble(), .artists = 1, .uid = RandomDouble() * Integer.MaxValue}
        Dim engine As New Engine("x:\test.db")
        '
        '     Dim result = engine.AddFile("E:\游戏原声\pastoral landscape\みかん箱,Foxtail-Grass Studio - Romanstone.mp3")

        Call engine.ScanDIR("E:\游戏原声\pastoral landscape", True)

        '   Call engine.Music.AddNew(n)

        Dim alll = engine.Music.GetAll

        Dim files = engine.QueryByTitle("の").ToArray


        Dim cue As New Cue("C:\Users\xieguigang\Source\Repos\Zplay-Music\media\TEST-LACM-14397.cue")
        Dim play As New ZplayCue

        Call play.Playback("E:\日漫\[160511] TVアニメ「マクロスΔ」OP／ED／挿入歌「一度だけの恋なら／ルンがピカッと光ったら／いけないボーダー\一度だけの恋なら&ルンがピカッと光ったら／ワルキューレ.cue")
        Call play.Playback()

        Pause()
    End Sub
End Module
