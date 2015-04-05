Public Class ClpForm
    Inherits System.Windows.Forms.Form

    Private bLoaded As Boolean = False

    Private Sub Calculate()
        If bLoaded = False Then Exit Sub

        Graph1.BinHeight = CType(nudBinHeight.Value, Integer)
        Graph1.Decreasing = cbDecreasing.Checked
        Graph1.Algorithm = CType(cbBinPackingAlgorithm.SelectedIndex, graph.BinPackingAlgorithm)

        Dim strElements() As String = txtElements.Text.Split(" "c)
        Dim intElements(strElements.GetUpperBound(0)) As Integer
        Dim bSuccess As Boolean = True
        Dim i As Integer

        For i = 0 To strElements.GetUpperBound(0)
            If IsNumeric(strElements(i)) Then
                intElements(i) = CType(strElements(i), Integer)
            Else
                bSuccess = False
            End If
        Next

        If bSuccess Then
            Graph1.Elements = intElements

            Graph1.Compute()
        End If
    End Sub

    Private Sub cbDecreasing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDecreasing.CheckedChanged
        Calculate()
    End Sub

    Private Sub nudBinHeight_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudBinHeight.ValueChanged
        Calculate()
    End Sub

    Private Sub txtElements_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtElements.TextChanged
        Calculate()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bLoaded = True
        Calculate()
    End Sub

    Private Sub cbBinPackingAlgorithm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBinPackingAlgorithm.SelectedIndexChanged
        Calculate()
    End Sub

End Class