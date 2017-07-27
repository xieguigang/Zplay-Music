Imports Microsoft.VisualBasic.Imaging

''' <summary>
''' The message data will displayed on the osd bubble.
''' </summary>
<Serializable> Public Class Message

    ''' <summary>
    ''' The title of the message.
    ''' </summary>
    ''' <returns></returns>
    <Xml.Serialization.XmlAttribute> Public Property Title As String
    <Xml.Serialization.XmlElement> Public Property Message As String
    ''' <summary>
    ''' The icon image file path.
    ''' </summary>
    ''' <returns></returns>
    <Xml.Serialization.XmlElement> Public Property IconURL As String
    ''' <summary>
    ''' The format of the bubble sound is limited on *.wav
    ''' </summary>
    ''' <returns></returns>
    <Xml.Serialization.XmlElement> Public Property SoundURL As String
    ''' <summary>
    ''' This handle indicated that when user click on the bubble what action will be run?
    ''' </summary>
    ''' <returns></returns>
    <Xml.Serialization.XmlIgnore> Public Property CallbackHandle As Action
    ''' <summary>
    ''' The displaying behavior of the osd bubble on the screen.
    ''' </summary>
    ''' <returns></returns>
    <Xml.Serialization.XmlElement> Public Property BubbleBehavior As BubbleBehaviors

    Public ReadOnly Property Icon As Image
        Get
            If String.IsNullOrEmpty(_IconURL) OrElse Not FileIO.FileSystem.FileExists(_IconURL) Then
                Return My.Resources.UBUNTU
            Else
                Try
                    Return LoadImage(_IconURL)
                Catch ex As Exception
                    Call New Exception(IconURL, ex).PrintException
                    Return My.Resources.UBUNTU
                End Try
            End If
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Message
    End Function

    Public Function Copy(OverridesHandle As Action) As Message
        Return New Message With {
            .CallbackHandle = OverridesHandle,
            .Message = Message,
            .IconURL = IconURL,
            .Title = Title,
            .BubbleBehavior = BubbleBehavior,
            .SoundURL = SoundURL
        }
    End Function
End Class
