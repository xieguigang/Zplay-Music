Imports libZPlay.App
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Imaging

Namespace App

    Public Enum ListTypes
        M3u
        Cue
        DIR
    End Enum

    ''' <summary>
    ''' 默认是顺序播放
    ''' </summary>
    Public Enum PlaybackModes
        ''' <summary>
        ''' 默认是顺序播放
        ''' </summary>
        Order
        Shuffle
        LoopOne
        LoopList
    End Enum

    'Public Interface IPlaylist : Inherits IEnumerable(Of MediaFile)
    '    Property Mode As PlaybackModes
    '    Function IndexOf(file As String) As Integer
    '    Function ReadNext() As String
    '    Function ReadPrevious() As String
    '    Function Reset() As String
    'End Interface

    Public Class Playlist : Inherits Playlist(Of MediaFile)

        Public ReadOnly Iterator Property FileList As IEnumerable(Of String)
            Get
                For Each file In Me._files
                    Yield file.FileName
                Next
            End Get
        End Property

        Sub New(files As IEnumerable(Of String), EOList As Action, type As ListTypes, URL As String)
            Call MyBase.New(GetFilesInfo(files, True), EOList, type, URL)
        End Sub

        Public Function IndexOf(file As String) As Integer
            Dim i As Integer = 0

            For Each mediaFile In _files
                If file.GetFullPath.TextEquals(mediaFile.FileName.GetFullPath) Then
                    Return i
                Else
                    i += 1
                End If
            Next

            Return -1
        End Function

        Shared ReadOnly __fileTypes As String() = {"*.mp3", "*.wav", "*.flac", "*.ape", "*.ogg"}

        Public Shared Function GetFiles(DIR As String, recurve As Boolean) As IEnumerable(Of String)
            If recurve Then
                Return ls - l - r - wildcards(__fileTypes) <= DIR
            Else
                Return ls - l - wildcards(__fileTypes) <= DIR
            End If
        End Function
    End Class

    Public MustInherit Class Playlist(Of T)
        Implements IEnumerable(Of T)

        Protected _files As List(Of T)

        Dim p As Integer = -1
        Dim _eolist As Action
        Dim _mode As PlaybackModes

        Public ReadOnly Property Type As ListTypes
        Public ReadOnly Property URI As String
        Public Property Mode As PlaybackModes
            Get
                Return _mode
            End Get
            Set(value As PlaybackModes)
                _mode = value
                If Mode = PlaybackModes.Shuffle Then
                    If _orders Is Nothing Then
                        _orders = _files.ToArray
                    End If
                    _files = New List(Of T)(_files.Randomize)
                ElseIf Mode = PlaybackModes.Order Then
                    If Not _orders Is Nothing Then
                        _files = New List(Of T)(_orders)
                    End If
                End If
            End Set
        End Property

        Dim _orders As T()

        Public ReadOnly Property Index As Integer
            Get
                Return p
            End Get
        End Property

        Public Sub SetCurrentRead(index As Integer)
            p = index
        End Sub

        Sub New(files As IEnumerable(Of T),
                eoList As Action,
                type As ListTypes,
                uri As String)

            _eolist = eoList
            _Type = type
            _URI = uri
            _files = files.ToList
        End Sub

        Public Sub Clear()
            _files.Clear()
            p = -1
        End Sub

        Public Function ReadNext() As T
            p += 1

            If p >= _files.Count Then
                Call _eolist()
            Else
                Return _files(p)
            End If

            Return Nothing
        End Function

        Public Function ReadPrevious() As T
            p -= 1

            If p = -1 Then
                p = 0
            End If

            If p = _files.Count Then
                p = _files.Count - 2
            End If

            Return _files(p)
        End Function

        ''' <summary>
        ''' Reset the list pointer to the list start location. 
        ''' </summary>
        ''' <returns></returns>
        Public Function Reset() As T
            p = -1
            Return ReadNext()
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            For Each x In _files
                Yield x
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace