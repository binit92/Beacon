Imports System.Drawing
Imports System.Drawing.Imaging.ImageFormat
Imports System.Data.SqlClient
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

    Public Sub capture1()
        Dim graph As Graphics = Nothing
        Try
            Dim bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            graph = Graphics.FromImage(bmp)
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size)
            graph.Save()
            Dim file_name As String = Application.ExecutablePath
            file_name = file_name.Substring(0, file_name.LastIndexOf("\bin")) & "\test."

            bmp.Save(file_name & "bmp", _
            System.Drawing.Imaging.ImageFormat.Bmp)
            '   SaveImage(bmp)
            MessageBox.Show("Image saved in bin folder successfully")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCapture.Click
        capture1()
      
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveImage
    End Sub
    Function SaveImage() As Integer


        Dim graph As Graphics = Nothing
        Try
            Dim bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            graph = Graphics.FromImage(bmp)
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size)
            graph.Save()
            Dim file_name As String = "C:\Beacon"
            file_name = file_name + "\test."

            bmp.Save(file_name & "bmp", _
            System.Drawing.Imaging.ImageFormat.Bmp)
            '   SaveImage(bmp)
        Catch ex As Exception
        End Try
        'Dim d1 As DateTime = DateTime.Now()
        'Dim d1 = "2010-03-01 2:05:06 PM"
        '-----------------------------------------------------------------
        'If Not (PictureBox1.Image) Is Nothing Then
        '    Dim sfd As New SaveFileDialog
        '    sfd.Title = "Save your Image picture as...."
        '    sfd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        '    sfd.Filter = "Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp"

        '    Dim result As DialogResult = sfd.ShowDialog
        '    If result = Windows.Forms.DialogResult.OK Then
        '        If sfd.FileName <> String.Empty Then
        '            'Set to Jpeg by default.
        '            Dim MyImageFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
        '            If sfd.FileName.ToString.ToUpper.EndsWith("JPG") Then
        '                MyImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
        '            ElseIf sfd.FileName.ToString.ToUpper.EndsWith("BMP") Then
        '                MyImageFormat = System.Drawing.Imaging.ImageFormat.Bmp
        '            End If
        '            PictureBox1.Image.Save(sfd.FileName, MyImageFormat)

        ' -- save in database
   
        Dim filename As String = "C:\Beacon\test.bmp"
        ' -- save in database
        Dim myconnection As New SqlConnection
        '------------------

        myconnection.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=db_beacon;Integrated Security=True;"
        Dim cmd1 As SqlCommand
        Dim ra As Integer

        Try
            myconnection.Open()
            cmd1 = New SqlCommand("INSERT INTO clp (clp_image) VALUES ('" & filename & "')", myconnection)
            ra = cmd1.ExecuteNonQuery()
            MsgBox("Category Added!!", MsgBoxStyle.Information, "Record Added = " & ra)
        Catch ex As Exception
            MsgBox("Category Failed to add!!", MsgBoxStyle.Critical)
        End Try


        'myconnection = New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=db_beacon;Integrated Security=True;")

        'Dim CMD As New SqlClient.SqlCommand("insert into clp (tsp_image) values(@filename)", myconnection)

        ''Dim filename1 As String = sfd.InitialDirectory.ToString + sfd.FileName.ToString
        'MessageBox.Show(filename)
        'CMD.Parameters.Add("filename")
        'CMD.Parameters.Add("@Img", SqlDbType.VarBinary, Integer.MaxValue).Value = ReadFile(filename)
        'myconnection.Open()
        'CMD.ExecuteNonQuery()
        'If CMD.ExecuteNonQuery Then
        '    MessageBox.Show(" Successfull Image Entry ")
        'End If
        'myconnection.Close()
        'myconnection.Dispose()

    End Function

    Private Function ReadFile(ByVal sPath As String) As Byte()
        Dim data As Byte() = Nothing
        Dim fInfo As New IO.FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length
        Using fStream As New IO.FileStream(sPath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim br As New IO.BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
        End Using
        Return data
    End Function
End Class