Imports libZPlay.InternalTypes

Namespace App.CUE

    Public Class ZplayCue : Inherits ZPlayInterface

        Public ReadOnly Property ZplayMusic As ZplayMusic

        ''' <summary>
        ''' 在这里需要把总位置转换为每一曲的相对位置
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property CurrentPosition As TStreamTime
            Get

            End Get
        End Property

        Public Overrides ReadOnly Property ID3v2 As TID3InfoEx

        ''' <summary>
        ''' 当前轨道的长度
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property StreamInfo As TStreamInfo

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="libzplay">libzplay.dll的文件夹的位置，默认是<see cref="Microsoft.VisualBasic.App.HOME"/></param>
        Sub New(Optional libzplay As String = Nothing)
            Call MyBase.New(libzplay)
            ZplayMusic = New ZplayMusic(__api)
        End Sub

        Public ReadOnly Property CUE As Cue
        Public ReadOnly Property CUEStream As TStreamInfo

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="uri">Track index or *.cue list file path</param>
        ''' <param name="autoStart"></param>
        ''' <returns></returns>
        ''' <remarks>除了一些需要实时进行更新的数据，所有的静态的信息都是在这里一次性的完成读取操作的</remarks>
        Public Overloads Function Playback(uri As String, Optional autoStart As Boolean = False) As Boolean
            If uri.FileExists Then
                _CUE = New Cue(uri)
                uri = uri.ParentPath & "/" & CUE.File.Name
                ZplayMusic.Playback(uri)
                _CUEStream = ZplayMusic.StreamInfo
                _ID3v2 = ZplayMusic.ID3v2
            Else
                Call PlayTrack(CTypeDynamic(Of Integer)(uri))
            End If

            Return True
        End Function

        Dim currentTrack As Track

        Public Sub PlayTrack(i As Integer)
            Dim l As TimeSpan = CUE.GetLength(i, CUEStream)
            currentTrack = CUE.Tracks(i)
            _StreamInfo = New TStreamInfo With {
                .Bitrate = CUEStream.Bitrate,
                .ChannelNumber = CUEStream.ChannelNumber,
                .Description = CUEStream.Description,
                .SamplingRate = CUEStream.SamplingRate,
                .VBR = CUEStream.VBR,
                .Length = New TStreamTime With {
                    .ms = l.TotalMilliseconds,
                    .hms = New TStreamHMSTime With {
                        .hour = l.Hours,
                        .millisecond = l.Milliseconds,
                        .minute = l.Minutes,
                        .second = l.Seconds
                    }
                }
            }
        End Sub

        Public Overrides Function Playback() As TickEvent
            Call SeeksByTime(currentTrack.GetTrackStart)
            Return MyBase.Playback()
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="p">当前音轨的百分比，需要在这里转换为总的进度</param>
        Public Overrides Sub SeeksByPercent(p As Double)
            Dim start = currentTrack.Index01
            Dim pos As Integer = StreamInfo.Length.ms * p
            start = start + New TimeSpan(pos)
            Call ZPlay.Seek(TTimeFormat.tfMillisecond,
                            New TStreamTime With {
                                .ms = start.TotalMilliseconds
                            },
                            TSeekMethod.smFromBeginning)
        End Sub
    End Class
End Namespace