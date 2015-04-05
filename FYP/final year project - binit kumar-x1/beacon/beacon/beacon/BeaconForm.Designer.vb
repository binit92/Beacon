<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BeaconForm
    Inherits System.Windows.Forms.Form
    Public Sub New()

        'MessageBox("Hello")
        ' This call is required by the designer.
        '' Sets the UI culture to French (France).
        ''  Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("fr-FR")
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BeaconForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RouteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlannerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadPlannerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoaderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeLanuageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EngishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HindiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FrenchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.RouteToolStripMenuItem, Me.LoadPlannerToolStripMenuItem, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        '
        'ExitToolStripMenuItem
        '
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        '
        'RouteToolStripMenuItem
        '
        Me.RouteToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlannerToolStripMenuItem, Me.ReportsToolStripMenuItem})
        resources.ApplyResources(Me.RouteToolStripMenuItem, "RouteToolStripMenuItem")
        Me.RouteToolStripMenuItem.Name = "RouteToolStripMenuItem"
        '
        'PlannerToolStripMenuItem
        '
        Me.PlannerToolStripMenuItem.Name = "PlannerToolStripMenuItem"
        resources.ApplyResources(Me.PlannerToolStripMenuItem, "PlannerToolStripMenuItem")
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        resources.ApplyResources(Me.ReportsToolStripMenuItem, "ReportsToolStripMenuItem")
        '
        'LoadPlannerToolStripMenuItem
        '
        Me.LoadPlannerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoaderToolStripMenuItem})
        resources.ApplyResources(Me.LoadPlannerToolStripMenuItem, "LoadPlannerToolStripMenuItem")
        Me.LoadPlannerToolStripMenuItem.Name = "LoadPlannerToolStripMenuItem"
        '
        'LoaderToolStripMenuItem
        '
        Me.LoaderToolStripMenuItem.Name = "LoaderToolStripMenuItem"
        resources.ApplyResources(Me.LoaderToolStripMenuItem, "LoaderToolStripMenuItem")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeLanuageToolStripMenuItem, Me.AboutToolStripMenuItem})
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        '
        'ChangeLanuageToolStripMenuItem
        '
        Me.ChangeLanuageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EngishToolStripMenuItem, Me.HindiToolStripMenuItem, Me.FrenchToolStripMenuItem})
        Me.ChangeLanuageToolStripMenuItem.Name = "ChangeLanuageToolStripMenuItem"
        resources.ApplyResources(Me.ChangeLanuageToolStripMenuItem, "ChangeLanuageToolStripMenuItem")
        '
        'EngishToolStripMenuItem
        '
        resources.ApplyResources(Me.EngishToolStripMenuItem, "EngishToolStripMenuItem")
        Me.EngishToolStripMenuItem.Name = "EngishToolStripMenuItem"
        '
        'HindiToolStripMenuItem
        '
        resources.ApplyResources(Me.HindiToolStripMenuItem, "HindiToolStripMenuItem")
        Me.HindiToolStripMenuItem.Name = "HindiToolStripMenuItem"
        '
        'FrenchToolStripMenuItem
        '
        resources.ApplyResources(Me.FrenchToolStripMenuItem, "FrenchToolStripMenuItem")
        Me.FrenchToolStripMenuItem.Name = "FrenchToolStripMenuItem"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'BeaconForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpButton = True
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "BeaconForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RouteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadPlannerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeLanuageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EngishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HindiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FrenchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlannerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoaderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
