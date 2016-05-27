Imports libZPlay.InternalTypes

Namespace App

    Public Class ZplayCue : Inherits ZPlayInterface

        Public ReadOnly Property ZplayMusic As ZplayMusic

        Public Overrides ReadOnly Property CurrentPosition As TStreamTime
            Get

            End Get
        End Property

        Public Overrides ReadOnly Property ID3v2 As TID3InfoEx

        Public Overrides ReadOnly Property StreamInfo As TStreamInfo

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="libzplay">libzplay.dll的文件夹的位置，默认是<see cref="Microsoft.VisualBasic.App.HOME"/></param>
        Sub New(Optional libzplay As String = Nothing)
            Call MyBase.New(libzplay)
            ZplayMusic = New ZplayMusic(__api)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="uri"></param>
        ''' <param name="autoStart"></param>
        ''' <returns></returns>
        ''' <remarks>除了一些需要实时进行更新的数据，所有的静态的信息都是在这里一次性的完成读取操作的</remarks>
        Public Overloads Function Playback(uri As String, Optional autoStart As Boolean = False) As Boolean
            Dim cue As New Cue(uri)

            uri = uri.ParentPath & "/" & cue.File.Name
            ZplayMusic.Playback(uri)
            _StreamInfo = ZplayMusic.StreamInfo
            _ID3v2 = ZplayMusic.ID3v2

            Return True
        End Function
    End Class
End Namespace