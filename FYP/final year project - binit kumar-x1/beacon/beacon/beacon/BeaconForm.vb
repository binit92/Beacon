Imports System.Globalization
Imports System.ComponentModel
Imports System.Resources
Imports System.Threading
Imports System.Windows.Forms


Public Class BeaconForm

#Region "Methods"

    Private Sub SetLocale(ByVal locale_name As String)
        Static setting_locale As Boolean = False

        If setting_locale Then Exit Sub
        If Not Me.Created Then Exit Sub
        If Thread.CurrentThread.CurrentCulture.Name = locale_name Then Exit Sub

        setting_locale = True

        ' Make a CultureInfo.
        Dim culture_info As New CultureInfo(locale_name)

        ' Make the thread use this locale.
        Thread.CurrentThread.CurrentUICulture = culture_info
        Thread.CurrentThread.CurrentCulture = culture_info

        ' Reload the form.
        If Me.components IsNot Nothing Then Me.components.Dispose()
        Me.Controls.Clear()
        Me.InitializeComponent()

        setting_locale = False
    End Sub
#End Region



#Region " EVENTS"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    ' Reload the form using this locale.

    Private Sub HindiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HindiToolStripMenuItem.Click
        SetLocale("hi-IN")
    End Sub

    Private Sub FrenchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrenchToolStripMenuItem.Click
        SetLocale("fr-FR")
    End Sub

    Private Sub EngishToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EngishToolStripMenuItem.Click
        SetLocale("en")
    End Sub
#End Region

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub PlannerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlannerToolStripMenuItem.Click
        UnloadAll()
        Dim newchildform As New TspForm
        newchildform.MdiParent = Me
        ' (here 'me' is form1 where u r writing this code) 
        newchildform.Show()
    End Sub

    Private Sub LoaderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoaderToolStripMenuItem.Click
        UnloadAll()
        Dim newchildform As New ClpForm
        newchildform.MdiParent = Me
        ' (here 'me' is form1 where u r writing this code) 
        newchildform.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportsToolStripMenuItem.Click

        UnloadAll()
        Dim newchildform As New tspReports
        newchildform.MdiParent = Me
        ' (here 'me' is form1 where u r writing this code) 
        newchildform.Show()
    End Sub

    Public Sub UnloadAll()

        'Dim f As Integer
        'f = Forms.Count
        'Do While f > 0
        '    Unload(Forms(f - 1))
        '    If f = Forms.Count Then Exit Do
        '    f = f - 1
        'Loop
        For i = My.Application.OpenForms.Count - 1 To 1 Step -1
            My.Application.OpenForms(i).Hide()
            ' Unload(My.Application.OpenForms(i))
        Next i
    End Sub
    'close all sub forms

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        UnloadAll()
        AboutBox1.Show()
    End Sub
End Class