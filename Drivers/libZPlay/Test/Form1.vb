Imports libZPlay.App
Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Serialization
Imports Zplay.MediaLibrary

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim engine As New Engine("x:\test.db")
        '
        '     Dim result = engine.AddFile("E:\游戏原声\pastoral landscape\みかん箱,Foxtail-Grass Studio - Romanstone.mp3")
        Call engine.ScanDIR("E:\日漫\傳頌之物", True)
        Call engine.ScanDIR("E:\游戏原声\pastoral landscape", True)

        '   Call engine.Music.AddNew(n)

        Dim alll = engine.Music.GetAll

        Dim files = engine.QueryByTitle("の").ToArray
    End Sub
End Class
