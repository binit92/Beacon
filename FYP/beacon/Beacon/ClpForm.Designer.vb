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
        Me.cbBinPackingAlgorithm = New System.Windows.Forms.ComboBox()
        Me.lblElements = New System.Windows.Forms.Label()
        Me.cbDecreasing = New System.Windows.Forms.CheckBox()
        Me.lblBinHeight = New System.Windows.Forms.Label()
        Me.nudBinHeight = New System.Windows.Forms.NumericUpDown()
        Me.txtElements = New System.Windows.Forms.TextBox()
        Me.Graph1 = New Global.Beacon.graph()
        CType(Me.nudBinHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbBinPackingAlgorithm
        '
        Me.cbBinPackingAlgorithm.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cbBinPackingAlgorithm.Items.AddRange(New Object() {"Next Fit", "First Fit", "Best Fit", "Worst Fit"})
        Me.cbBinPackingAlgorithm.Location = New System.Drawing.Point(719, 321)
        Me.cbBinPackingAlgorithm.Name = "cbBinPackingAlgorithm"
        Me.cbBinPackingAlgorithm.Size = New System.Drawing.Size(80, 21)
        Me.cbBinPackingAlgorithm.TabIndex = 13
        Me.cbBinPackingAlgorithm.Text = "Next Fit"
        '
        'lblElements
        '
        Me.lblElements.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblElements.AutoSize = True
        Me.lblElements.Location = New System.Drawing.Point(734, 47)
        Me.lblElements.Name = "lblElements"
        Me.lblElements.Size = New System.Drawing.Size(50, 13)
        Me.lblElements.TabIndex = 12
        Me.lblElements.Text = "Elements"
        '
        'cbDecreasing
        '
        Me.cbDecreasing.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cbDecreasing.Location = New System.Drawing.Point(776, 289)
        Me.cbDecreasing.Name = "cbDecreasing"
        Me.cbDecreasing.Size = New System.Drawing.Size(83, 24)
        Me.cbDecreasing.TabIndex = 8
        Me.cbDecreasing.Text = "Decreasing"
        '
        'lblBinHeight
        '
        Me.lblBinHeight.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblBinHeight.AutoSize = True
        Me.lblBinHeight.Location = New System.Drawing.Point(665, 275)
        Me.lblBinHeight.Name = "lblBinHeight"
        Me.lblBinHeight.Size = New System.Drawing.Size(56, 13)
        Me.lblBinHeight.TabIndex = 11
        Me.lblBinHeight.Text = "Bin Height"
        '
        'nudBinHeight
        '
        Me.nudBinHeight.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.nudBinHeight.Location = New System.Drawing.Point(659, 291)
        Me.nudBinHeight.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudBinHeight.Name = "nudBinHeight"
        Me.nudBinHeight.Size = New System.Drawing.Size(69, 20)
        Me.nudBinHeight.TabIndex = 10
        Me.nudBinHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudBinHeight.Value = New Decimal(New Integer() {80, 0, 0, 0})
        '
        'txtElements
        '
        Me.txtElements.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtElements.Location = New System.Drawing.Point(659, 63)
        Me.txtElements.Multiline = True
        Me.txtElements.Name = "txtElements"
        Me.txtElements.Size = New System.Drawing.Size(200, 200)
        Me.txtElements.TabIndex = 9
        Me.txtElements.Text = "26 57 18 8 45 16 22 29 5 11 8 27 54 13 17 21 63 14 16 45 6 32 57 24 18 27 54 35 1" & _
            "2 43 36 72 14 28 3 11 46 27 42 59 26 41 15 41 68"
        '
        'Graph1
        '
        Me.Graph1.Algorithm = Global.Beacon.graph.BinPackingAlgorithm.NextFit
        Me.Graph1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Graph1.AutoUpdate = False
        Me.Graph1.BinColor1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Graph1.BinColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Graph1.BinHeight = 80
        Me.Graph1.BinTextColor = System.Drawing.Color.Blue
        Me.Graph1.Decreasing = False
        Me.Graph1.Elements = Nothing
        Me.Graph1.Location = New System.Drawing.Point(6, 6)
        Me.Graph1.Name = "Graph1"
        Me.Graph1.Size = New System.Drawing.Size(837, 402)
        Me.Graph1.TabIndex = 0
        '
        'ClpForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(893, 424)
        Me.Controls.Add(Me.cbBinPackingAlgorithm)
        Me.Controls.Add(Me.lblElements)
        Me.Controls.Add(Me.cbDecreasing)
        Me.Controls.Add(Me.lblBinHeight)
        Me.Controls.Add(Me.nudBinHeight)
        Me.Controls.Add(Me.txtElements)
        Me.Controls.Add(Me.Graph1)
        Me.Name = "ClpForm"
        Me.Text = "ClpForm"
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
End Class
