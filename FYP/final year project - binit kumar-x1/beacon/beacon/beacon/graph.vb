Imports System.Drawing.Drawing2D
Public Class graph
    Inherits System.Windows.Forms.UserControl


#Region " DECLARATIONS "

    Private _BinHeight As Integer = 100
    Private _Elements() As Integer
    Private _Algorithm As BinPackingAlgorithm = BinPackingAlgorithm.NextFit
    Private _Decreasing As Boolean = True

    Private _AutoUpdate As Boolean = True

    Private bmpGraph As Bitmap
    Private g As Graphics
    Private DrawingFont As New Font(Drawing.FontFamily.GenericSansSerif, 12, FontStyle.Regular, GraphicsUnit.Pixel)
    Private DrawingTextBrush As New SolidBrush(Me.ForeColor)
    Private DrawingBinTextBrush As New SolidBrush(Color.Blue)
    Private DrawingBinBrush As LinearGradientBrush = New LinearGradientBrush(New Rectangle(0, 0, 1, 1), Color.NavajoWhite, Color.DarkOrange, LinearGradientMode.ForwardDiagonal)
    Private DrawingPen As New Pen(Me.ForeColor)

    'This is an array of Bins.  Each Bin holds an array of Elements filling the bin
    Private Bins()() As Integer

    Private Const BORDER As Integer = 25
    Private Const BIN_WIDTH As Integer = 25


    Public Enum BinPackingAlgorithm As Integer
        NextFit
        FirstFit
        BestFit
        WorstFit
    End Enum
#End Region

#Region " PROPERTIES "

    Public Property BinHeight() As Integer
        Get
            Return _BinHeight
        End Get
        Set(ByVal Value As Integer)
            _BinHeight = Value

            If Me.AutoUpdate = True Then
                Compute()
            End If
        End Set
    End Property

    Public Property Elements() As Integer()
        Get
            Return _Elements
        End Get
        Set(ByVal Value As Integer())
            _Elements = Value

            If Me.AutoUpdate = True Then
                Compute()
            End If
        End Set
    End Property

    Public Property Algorithm() As BinPackingAlgorithm
        Get
            Return _Algorithm
        End Get
        Set(ByVal Value As BinPackingAlgorithm)
            _Algorithm = Value

            If Me.AutoUpdate = True Then
                Compute()
            End If
        End Set
    End Property

    Public Property Decreasing() As Boolean
        Get
            Return _Decreasing
        End Get
        Set(ByVal Value As Boolean)
            _Decreasing = Value

            If Me.AutoUpdate = True Then
                Compute()
            End If
        End Set
    End Property


    Public Property AutoUpdate() As Boolean
        Get
            Return _AutoUpdate
        End Get
        Set(ByVal Value As Boolean)
            _AutoUpdate = Value
        End Set
    End Property

    Public Property BinColor1() As Color
        Get
            Return Me.DrawingBinBrush.LinearColors(0)
        End Get
        Set(ByVal Value As Color)
            Me.DrawingBinBrush.LinearColors(0) = Value
            UpdateGraph()
        End Set
    End Property

    Public Property BinColor2() As Color
        Get
            Return Me.DrawingBinBrush.LinearColors(1)
        End Get
        Set(ByVal Value As Color)
            Me.DrawingBinBrush.LinearColors(1) = Value
            UpdateGraph()
        End Set
    End Property

    Public Property BinTextColor() As Color
        Get
            Return Me.DrawingBinTextBrush.Color
        End Get
        Set(ByVal Value As Color)
            Me.DrawingBinTextBrush.Color = Value
            UpdateGraph()
        End Set
    End Property


    Public Overrides Property ForeColor() As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal Value As Color)
            MyBase.ForeColor = Value
            Me.DrawingTextBrush.Color = Value
            Me.DrawingPen.Color = Value
            UpdateGraph()
        End Set
    End Property

    Public Property image() As Bitmap

        Get
            Dim bm As Bitmap = PictureBox1.Image
            Return bm
        End Get
        Set(ByVal bm As Bitmap)
            bm = PictureBox1.Image
        End Set
    End Property

#End Region

#Region " EVENTS "

    Property SizeMode As PictureBoxSizeMode

    Private Sub Graph_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Compute()
    End Sub

    Private Sub PictueBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        'Refresh off of our stored Bitmap instead of redrawing the Graph from scratch
        If Not bmpGraph Is Nothing Then e.Graphics.DrawImage(bmpGraph, New Point(0, 0))
    End Sub

    Private Sub PictueBox1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Resize
        'Our graph size has changed, now we have to redraw it from scratch
        UpdateGraph()
    End Sub

    Private Sub Graph_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        'Clean up
        g.Dispose()
        bmpGraph.Dispose()

        DrawingFont.Dispose()
        DrawingTextBrush.Dispose()
        DrawingBinTextBrush.Dispose()
        DrawingBinBrush.Dispose()
        DrawingPen.Dispose()
    End Sub

#End Region

#Region " METHODS "

    'Calculate useing the appropriate algorithm and then draw the graph
    Public Sub Compute()
        InitDrawingSurface()

        Select Case _Algorithm
            Case BinPackingAlgorithm.NextFit
                NextFit()

            Case BinPackingAlgorithm.FirstFit
                FirstFit()

            Case BinPackingAlgorithm.BestFit
                BestFit()

            Case BinPackingAlgorithm.WorstFit
                WorstFit()
        End Select

        UpdateGraph()
    End Sub


    Private Sub NextFit()
        'Checks to make sure everything is initialized
        If Elements Is Nothing Then Exit Sub

        Dim ElementsCopy(Elements.GetUpperBound(0)) As Integer
        ReDim Bins(0)
        'Bin Number we are on, Bin Element we are on, Amount placed in the current Bin
        Dim BinNumber, BinElement, BinCount As Integer
        Dim i As Integer

        'Make a copy of the array incase we need to sort it
        DeepCopyArray(Elements, ElementsCopy)

        'Sort in descending order if needed
        If Me.Decreasing = True Then
            Array.Sort(ElementsCopy)
            Array.Reverse(ElementsCopy)
        End If

        'Declare the first element in the first Bin
        ReDim Bins(0)(0)

        'Loop through each Element and place in a Bin
        For i = 0 To ElementsCopy.GetUpperBound(0)

            'If True, move on to next Bin
            If BinCount + ElementsCopy(i) > Me.BinHeight Then
                'Remove extra, unused Element of this Bin
                ReDim Preserve Bins(BinNumber)(BinElement - 1)

                'Add another Bin
                ReDim Preserve Bins(BinNumber + 1)
                BinNumber += 1

                'Initialize first element of new bin
                ReDim Bins(BinNumber)(0)
                BinElement = 0

                BinCount = 0
            End If

            'Place task
            Bins(BinNumber)(BinElement) = ElementsCopy(i)
            'Keep track of how much is stored in this bin
            BinCount += ElementsCopy(i)

            'Add Element unless we are done
            If i < ElementsCopy.GetUpperBound(0) Then
                ReDim Preserve Bins(BinNumber)(BinElement + 1)
                BinElement += 1
            End If
        Next

        GC.Collect()
    End Sub


    Private Sub FirstFit()
        'Checks to make sure everything is initialized
        If Elements Is Nothing Then Exit Sub

        Dim ElementsCopy(Elements.GetUpperBound(0)) As Integer
        ReDim Bins(0)
        'Bin Number we are on, Bin Element we are on, Amount placed in the current Bin
        Dim BinNumber, BinElement, BinCount As Integer
        Dim i, j, k As Integer

        'Make a copy of the array incase we need to sort it
        DeepCopyArray(Elements, ElementsCopy)

        'Sort in descending order if needed
        If Me.Decreasing = True Then
            Array.Sort(ElementsCopy)
            Array.Reverse(ElementsCopy)
        End If

        'Declare the first element in the first Bin
        ReDim Bins(0)(0)

        'Loop through each Element and place in a Bin
        For i = 0 To ElementsCopy.GetUpperBound(0)
            Dim bPlaced As Boolean = False

            'Loops through each Bin to find the first available spot
            For j = 0 To BinNumber
                BinElement = Bins(j).GetUpperBound(0)

                'Count the amount placed in this Bin
                BinCount = 0
                For k = 0 To BinElement
                    BinCount += Bins(j)(k)
                Next

                If BinCount + ElementsCopy(i) <= Me.BinHeight Then
                    'There's room for this Element
                    ReDim Preserve Bins(j)(BinElement + 1)
                    Bins(j)(BinElement) = ElementsCopy(i)

                    bPlaced = True
                    Exit For
                Else
                    'There's not room for this Element in this Bin
                End If
            Next


            'There wasn't room for the Element in any existing Bin
            'Create a new Bin
            If bPlaced = False Then
                'Add another Bin
                ReDim Preserve Bins(BinNumber + 1)
                BinNumber += 1

                'Initialize first element of new bin
                ReDim Bins(BinNumber)(1)
                BinElement = 0

                Bins(BinNumber)(BinElement) = ElementsCopy(i)

            End If
        Next

        'All Elements have been place, now we go back and remove unused Elements
        For i = 0 To BinNumber
            For j = 0 To Bins(i).GetUpperBound(0)
                If Bins(i)(j) = 0 Then
                    ReDim Preserve Bins(i)(j - 1)
                End If
            Next
        Next

        GC.Collect()
    End Sub


    Private Sub BestFit()
        'Checks to make sure everything is initialized
        If Elements Is Nothing Then Exit Sub

        Dim ElementsCopy(Elements.GetUpperBound(0)) As Integer
        ReDim Bins(0)
        'Bin Number we are on, Bin Element we are on, Amount placed in the current Bin
        Dim BinNumber, BinElement, BinCount As Integer
        Dim BestBin, BestBinAmount As Integer
        Dim i, j, k As Integer

        'Make a copy of the array incase we need to sort it
        DeepCopyArray(Elements, ElementsCopy)

        'Sort in descending order if needed
        If Me.Decreasing = True Then
            Array.Sort(ElementsCopy)
            Array.Reverse(ElementsCopy)
        End If

        'Declare the first element in the first Bin
        ReDim Bins(0)(0)

        'Loop through each Element and place in a Bin
        For i = 0 To ElementsCopy.GetUpperBound(0)
            BestBin = -1
            BestBinAmount = -1

            For j = 0 To BinNumber
                BinElement = Bins(j).GetUpperBound(0)

                'Count the amount placed in this Bin
                BinCount = 0
                For k = 0 To BinElement
                    BinCount += Bins(j)(k)
                Next

                'Find the most full Bin that can hold this Element
                If BestBinAmount < BinCount AndAlso BinCount + ElementsCopy(i) <= Me.BinHeight Then
                    BestBinAmount = BinCount
                    BestBin = j
                End If
            Next


            If BestBin = -1 Then
                'There wasn't room for the Element in any existing Bin
                'Create a new Bin
                ReDim Preserve Bins(BinNumber + 1)
                BinNumber += 1

                'Initialize first element of new bin
                ReDim Bins(BinNumber)(1)
                BinElement = 0

                Bins(BinNumber)(BinElement) = ElementsCopy(i)
            Else
                'There's room for this Element in an existing Bin
                'Place Element in "Best Bin"
                BinElement = Bins(BestBin).GetUpperBound(0)
                ReDim Preserve Bins(BestBin)(BinElement + 1)
                Bins(BestBin)(BinElement) = ElementsCopy(i)
            End If
        Next

        'All Elements have been place, now we go back and remove unused Elements
        For i = 0 To BinNumber
            For j = 0 To Bins(i).GetUpperBound(0)
                If Bins(i)(j) = 0 Then
                    ReDim Preserve Bins(i)(j - 1)
                End If
            Next
        Next

        GC.Collect()
    End Sub


    Private Sub WorstFit()
        'Checks to make sure everything is initialized
        If Elements Is Nothing Then Exit Sub

        Dim ElementsCopy(Elements.GetUpperBound(0)) As Integer
        ReDim Bins(0)
        'Bin Number we are on, Bin Element we are on, Amount placed in the current Bin
        Dim BinNumber, BinElement, BinCount As Integer
        Dim WorstBin, WorstBinAmount As Integer
        Dim i, j, k As Integer

        'Make a copy of the array incase we need to sort it
        DeepCopyArray(Elements, ElementsCopy)

        'Sort in descending order if needed
        If Me.Decreasing = True Then
            Array.Sort(ElementsCopy)
            Array.Reverse(ElementsCopy)
        End If

        'Declare the first element in the first Bin
        ReDim Bins(0)(0)

        'Loop through each Element and place in a Bin
        For i = 0 To ElementsCopy.GetUpperBound(0)
            WorstBin = -1
            WorstBinAmount = Me.BinHeight + 1

            For j = 0 To BinNumber
                BinElement = Bins(j).GetUpperBound(0)

                'Count the amount placed in this Bin
                BinCount = 0
                For k = 0 To BinElement
                    BinCount += Bins(j)(k)
                Next

                'Find the least full Bin that can hold this Element
                If WorstBinAmount > BinCount AndAlso BinCount + ElementsCopy(i) <= Me.BinHeight Then
                    WorstBinAmount = BinCount
                    WorstBin = j
                End If
            Next


            If WorstBin = -1 Then
                'There wasn't room for the Element in any existing Bin
                'Create a new Bin
                ReDim Preserve Bins(BinNumber + 1)
                BinNumber += 1

                'Initialize first element of new bin
                ReDim Bins(BinNumber)(1)
                BinElement = 0

                Bins(BinNumber)(BinElement) = ElementsCopy(i)
            Else
                'There's room for this Element in an existing Bin
                'Place Element in "Best Bin"
                BinElement = Bins(WorstBin).GetUpperBound(0)
                ReDim Preserve Bins(WorstBin)(BinElement + 1)
                Bins(WorstBin)(BinElement) = ElementsCopy(i)
            End If
        Next

        'All Elements have been place, now we go back and remove unused Elements
        For i = 0 To BinNumber
            For j = 0 To Bins(i).GetUpperBound(0)
                If Bins(i)(j) = 0 Then
                    ReDim Preserve Bins(i)(j - 1)
                End If
            Next
        Next

        GC.Collect()
    End Sub



    Private Sub DeepCopyArray(ByVal ArrayStart() As Integer, ByVal ArrayEnd() As Integer)
        Dim i As Integer

        For i = 0 To ArrayStart.GetUpperBound(0)
            ArrayEnd(i) = ArrayStart(i)
        Next
    End Sub


#End Region

#Region " DRAWINGS "

    Private Sub UpdateGraph()
        InitDrawingSurface()
        DrawBins(DrawDemarcations())

        PictureBox1.Refresh()
    End Sub

    Private Sub InitDrawingSurface()
        If Me.Width = 0 Or Me.Height = 0 Then Exit Sub

        bmpGraph = New Bitmap(Me.Width, Me.Height)
        g = Graphics.FromImage(bmpGraph)
        g.Clear(Me.BackColor)

        PictureBox1.Size = bmpGraph.Size
    End Sub

    'Returns how wide the text was on the left hand side of the graph as well as draws teh Demarcations
    Private Function DrawDemarcations() As Integer
        Dim TextHeight As Integer = CType(g.MeasureString(Me.BinHeight.ToString, DrawingFont).Height, Integer)

        '100% of BinHeight
        g.DrawString(Me.BinHeight.ToString, DrawingFont, DrawingTextBrush, BORDER, BORDER - 8)
        '75% of BinHeight
        g.DrawString((Me.BinHeight * 0.75).ToString, DrawingFont, DrawingTextBrush, BORDER, CType((Me.Height - BORDER * 2) * 0.25, Integer) + BORDER - TextHeight + 6)
        '50% of BinHeight
        g.DrawString((Me.BinHeight * 0.5).ToString, DrawingFont, DrawingTextBrush, BORDER, CType((Me.Height - BORDER * 2) * 0.5, Integer) + BORDER - TextHeight + 6)
        '25% of BinHeight
        g.DrawString((Me.BinHeight * 0.25).ToString, DrawingFont, DrawingTextBrush, BORDER, CType((Me.Height - BORDER * 2) * 0.75, Integer) + BORDER - TextHeight + 6)
        '0% of BinHeight
        g.DrawString("0", DrawingFont, DrawingTextBrush, BORDER, Me.Height - BORDER - TextHeight + 5)

        'If there's decimals, Width2 will be longer, else, Width1 will be
        Dim Width1 As Integer = CType(g.MeasureString(Me.BinHeight.ToString, DrawingFont).Width, Integer)
        Dim Width2 As Integer = CType(g.MeasureString((Me.BinHeight * 0.75).ToString, DrawingFont).Width, Integer)

        Return Math.Max(Width1, Width2) + 5 + BORDER
    End Function

    Private Sub DrawBins(ByVal StartX As Integer)
        Dim BinPixelHeight As Integer = Me.Height - BORDER * 2
        Dim X1, X2, Y1, Y2 As Integer
        Dim i As Integer

        Y1 = BORDER
        Y2 = Me.Height - BORDER

        If Bins Is Nothing Then Exit Sub

        'Draws the vertical lines that make up the bins
        For i = 0 To Bins.GetUpperBound(0) + 1
            X1 = StartX + (BIN_WIDTH * i)
            X2 = X1

            g.DrawLine(DrawingPen, X1, Y1, X2, Y2)
        Next

        Dim j As Integer
        Dim BinValue As Integer
        Dim TotalBinHeight, TotalPixelBinHeight As Integer

        For i = 0 To Bins.GetUpperBound(0) + 1
            X1 = StartX + (BIN_WIDTH * i)
            X2 = X1 + BIN_WIDTH

            'Draws the Bins
            If i < Bins.GetUpperBound(0) + 1 Then

                'Draws the gradient
                TotalBinHeight = 0
                For j = 0 To Bins(i).GetUpperBound(0)
                    TotalBinHeight += Bins(i)(j)
                Next

                TotalPixelBinHeight = CType(TotalBinHeight / Me.BinHeight * BinPixelHeight, Integer)

                If TotalPixelBinHeight > 0 Then
                    'Draws the Bin gradient
                    DrawingBinBrush = New LinearGradientBrush(New Rectangle(X1 + 1, Me.Height - BORDER - TotalPixelBinHeight, BIN_WIDTH - 1, TotalPixelBinHeight), Me.BinColor1, Me.BinColor2, LinearGradientMode.ForwardDiagonal)
                    DrawingBinBrush.WrapMode = WrapMode.TileFlipXY
                    g.FillRectangle(DrawingBinBrush, New Rectangle(X1 + 1, Me.Height - BORDER - TotalPixelBinHeight, BIN_WIDTH - 1, TotalPixelBinHeight))


                    Dim LastY As Integer
                    For j = 0 To Bins(i).GetUpperBound(0)
                        BinValue += Bins(i)(j)
                        Y1 = CType(BinValue / Me.BinHeight * BinPixelHeight, Integer)
                        Y1 = Me.Height - Y1 - BORDER
                        Y2 = Y1

                        If j = 0 Then LastY = Me.Height - BORDER

                        'Draws the horizontal lines
                        g.DrawLine(DrawingPen, X1, Y1, X2, Y2)

                        'Draws the Element value
                        Dim TextSize As SizeF = g.MeasureString(Bins(i)(j).ToString, DrawingFont)
                        g.DrawString(Bins(i)(j).ToString, DrawingFont, Me.DrawingBinTextBrush, CType(X1 + (BIN_WIDTH / 2) - (TextSize.Width / 2), Integer), CType(Y1 + ((LastY - Y1) / 2) - (TextSize.Height / 2), Integer))
                        LastY = Y1
                    Next

                    'Draws the Bin Number
                    g.DrawString((i + 1).ToString, DrawingFont, DrawingTextBrush, CType(X1 + (BIN_WIDTH / 2) - (g.MeasureString((i + 1).ToString, DrawingFont).Width / 2), Integer), Me.Height - BORDER)

                    'Draws the Bin Count
                    g.DrawString(TotalBinHeight.ToString, DrawingFont, DrawingBinTextBrush, CType(X1 + (BIN_WIDTH / 2) - (g.MeasureString(TotalBinHeight.ToString, DrawingFont).Width / 2), Integer), 5)

                    BinValue = 0
                End If
            End If
        Next

        'Bottom line
        g.DrawLine(DrawingPen, StartX, Me.Height - BORDER, X1, Me.Height - BORDER)
    End Sub

#End Region

   

End Class
