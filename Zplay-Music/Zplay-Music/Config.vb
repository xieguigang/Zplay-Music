Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Serialization

Public Class Config : Implements IDisposable

    Public Property lastplay As NamedValue(Of ListTypes)

    Public Shared ReadOnly Property DefaultFile As String = App.HOME & "/config.json"

    Public Function GetList(eop As Action) As Playlist
        If lastplay.x = ListTypes.DIR Then
            Return New Playlist(
                Playlist.GetFiles(lastplay.Name, False),
                eop,
                ListTypes.DIR,
                lastplay.Name)
        Else
            Throw New NotImplementedException
        End If
    End Function

    Public Shared Function Load() As Config
        Try
            Return LoadJsonFile(Of Config)(DefaultFile)
        Catch ex As Exception
            Dim json As New Config
            Call json.Save()
            Return json
        End Try
    End Function

    Sub Save()
        Call Me.GetJson.SaveTo(DefaultFile, Encodings.UTF8.GetEncodings)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Call Save()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

Public Enum ListTypes
    Playlist
    DIR
End Enum