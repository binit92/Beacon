<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClpForm
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClpForm))
        Me.cbBinPackingAlgorithm = New System.Windows.Forms.ComboBox()
        Me.lblElements = New System.Windows.Forms.Label()
        Me.cbDecreasing = New System.Windows.Forms.CheckBox()
        Me.lblBinHeight = New System.Windows.Forms.Label()
        Me.nudBinHeight = New System.Windows.Forms.NumericUpDown()
        Me.txtElements = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Graph1 = New Beacon.graph()
        CType(Me.nudBinHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbBinPackingAlgorithm
        '
        resources.ApplyResources(Me.cbBinPackingAlgorithm, "cbBinPackingAlgorithm")
        Me.cbBinPackingAlgorithm.Items.AddRange(New Object() {resources.GetString("cbBinPackingAlgorithm.Items"), resources.GetString("cbBinPackingAlgorithm.Items1"), resources.GetString("cbBinPackingAlgorithm.Items2"), resources.GetString("cbBinPackingAlgorithm.Items3")})
        Me.cbBinPackingAlgorithm.Name = "cbBinPackingAlgorithm"
        '
        'lblElements
        '
        resources.ApplyResources(Me.lblElements, "lblElements")
        Me.lblElements.Name = "lblElements"
        '
        'cbDecreasing
        '
        resources.ApplyResources(Me.cbDecreasing, "cbDecreasing")
        Me.cbDecreasing.Name = "cbDecreasing"
        '
        'lblBinHeight
        '
        resources.ApplyResources(Me.lblBinHeight, "lblBinHeight")
        Me.lblBinHeight.Name = "lblBinHeight"
        '
        'nudBinHeight
        '
        resources.ApplyResources(Me.nudBinHeight, "nudBinHeight")
        Me.nudBinHeight.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudBinHeight.Name = "nudBinHeight"
        Me.nudBinHeight.Value = New Decimal(New Integer() {80, 0, 0, 0})
        '
        'txtElements
        '
        resources.ApplyResources(Me.txtElements, "txtElements")
        Me.txtElements.Name = "txtElements"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'btnCapture
        '
        resources.ApplyResources(Me.btnCapture, "btnCapture")
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Graph1
        '
        Me.Graph1.Algorithm = Beacon.graph.BinPackingAlgorithm.NextFit
        resources.ApplyResources(Me.Graph1, "Graph1")
        Me.Graph1.AutoUpdate = False
        Me.Graph1.BinColor1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Graph1.BinColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Graph1.BinHeight = 80
        Me.Graph1.BinTextColor = System.Drawing.Color.Blue
        Me.Graph1.Decreasing = False
        Me.Graph1.Elements = Nothing
        Me.Graph1.image = Nothing
        Me.Graph1.Name = "Graph1"
        Me.Graph1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        '
        'ClpForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCapture)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbBinPackingAlgorithm)
        Me.Controls.Add(Me.lblElements)
        Me.Controls.Add(Me.cbDecreasing)
        Me.Controls.Add(Me.lblBinHeight)
        Me.Controls.Add(Me.nudBinHeight)
        Me.Controls.Add(Me.txtElements)
        Me.Controls.Add(Me.Graph1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClpForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.nudBinHeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Graph1 As graph
    Friend WithEvents cbBinPackingAlgorithm As System.Windows.Forms.ComboBox
    Friend WithEvents lblElements As System.Windows.Forms.Label
    Friend WithEvents cbDecreasing As System.Windows.Forms.CheckBox
    Friend WithEvents lblBinHeight As System.Windows.Forms.Label
    Friend WithEvents nudBinHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtElements As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCapture As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
