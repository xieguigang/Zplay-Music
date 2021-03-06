﻿Imports libZPlay.InternalTypes
Imports Microsoft.VisualBasic.Imaging
Imports NotifyOsd.Framework.Balloon

Public Class Notify
    Implements IDisposable

    Dim osd As New OsdNotifier

    ReadOnly sound As String = App.LocalData & "/notify.wav"

    Sub New()
        Call My.Resources.notify.FlushStream(sound)
        Call My.Resources._default.SaveAs(AppIcon, ImageFormats.Png)
    End Sub

    Public Sub ShowNotify(tag As TID3InfoEx, albumArt As Image)
        Dim art As String = App.GetAppSysTempFile(".png")
        Dim msg As New NotifyOsd.Message With {
            .IconURL = art,
            .Title = "Now Playing",
            .Message = $"Title: {tag.Title}\nArtist: {tag.Artist}\nAlubm: {tag.Album}\n"
        }
        Call albumArt.SaveAs(art, ImageFormats.Png)
        Call osd.SendMessage(msg)
    End Sub

    Public ReadOnly Property AppIcon As String = App.GetAppSysTempFile(".png")

    Public Sub ShowMessage(s As String)
        Dim msg As New NotifyOsd.Message With {
            .IconURL = AppIcon,
            .Message = s,
            .Title = "Zplay-Music",
            .SoundURL = sound
        }
        Call osd.SendMessage(msg)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Call osd.Dispose()
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
