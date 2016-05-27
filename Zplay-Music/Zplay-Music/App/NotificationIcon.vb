'
' Created by SharpDevelop.
' User: xieguigang
' Date: 5/27/2016
' Time: 3:37 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
Imports System.Threading

Public NotInheritable Class NotificationIcon

    Public ReadOnly Property notifyIcon As NotifyIcon
    Public ReadOnly Property notificationMenu As ContextMenu

#Region "Initialize icon and menu"
    Public Sub New()
        notifyIcon = New NotifyIcon()
        notificationMenu = New ContextMenu(InitializeMenu())

        AddHandler notifyIcon.DoubleClick, AddressOf IconDoubleClick
        Dim resources As New ComponentResourceManager(GetType(NotificationIcon))
        notifyIcon.Icon = My.Resources.wmp
        notifyIcon.ContextMenu = notificationMenu
    End Sub

    Private Function InitializeMenu() As MenuItem()
        Dim menu As MenuItem() = New MenuItem() {New MenuItem("About", AddressOf menuAboutClick), New MenuItem("Exit", AddressOf menuExitClick)}
        Return menu
    End Function
#End Region

#Region "Event Handlers"
    Private Sub menuAboutClick(sender As Object, e As EventArgs)
        MessageBox.Show("About This Application")
    End Sub

    Private Sub menuExitClick(sender As Object, e As EventArgs)
        Call Program.Zplay.Close()
    End Sub

    Private Sub IconDoubleClick(sender As Object, e As EventArgs)
        MessageBox.Show("The icon was double clicked")
    End Sub
#End Region
End Class
