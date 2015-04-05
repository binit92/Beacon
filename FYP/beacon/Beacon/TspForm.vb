Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO
Imports System.Globalization
Imports Beacon.tsp


'Namespace tsp
Imports System.Data.SqlClient
Imports System.Drawing.Imaging

'Imports System.Configuration

Public Class TspForm
    Dim g As Graphics
    Dim wh As New Pen(Color.White, 3)

    ''' <summary>
    ''' The list of cities where we are trying to find the best tour.
    ''' </summary>
    Private cityList As New Cities()

    ''' <summary>
    ''' The class that does all the work in the TSP algorithm.
    ''' If this is not null, then the algorithm is still running.
    ''' </summary>
    Private tsp As Global.Beacon.tsp.Tsp

    ''' <summary>
    ''' The image that we draw the tour on.
    ''' </summary>
    Private cityImage As Image

    ''' <summary>
    ''' The graphics object for the image that we draw the tour on.
    ''' </summary>
    Private cityGraphics As Graphics

    ''' <summary>
    ''' Delegate for the thread that runs the TSP algorithm.
    ''' We use a separate thread so the GUI can redraw as the algorithm runs.
    ''' </summary>
    ''' <param name="sender">Object that generated this event.</param>
    ''' <param name="e">Event arguments.</param>
    Public Delegate Sub DrawEventHandler(ByVal sender As [Object], ByVal e As TspEventArgs)


    ' Default constructor.

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' TSP algorithm raised an event that a new best tour was found.
    ''' We need to do an invoke on the GUI thread before doing any draw code.
    ''' </summary>
    ''' <param name="sender">Object that generated this event.</param>
    ''' <param name="e">Event arguments.</param>
    Private Sub tsp_foundNewBestTour(ByVal sender As Object, ByVal e As TspEventArgs)
        If Me.InvokeRequired Then
            Try
                Me.Invoke(New DrawEventHandler(AddressOf DrawTour), New Object() {sender, e})
                Return
                ' This will fail when run as a control in IE due to a security exception.
            Catch generatedExceptionName As Exception
            End Try
        End If

        DrawTour(sender, e)
    End Sub

    ''' <summary>
    ''' A new "best" tour from the TSP algorithm has been received.
    ''' Draw the tour on the form, and update a couple of status labels.
    ''' </summary>
    ''' <param name="sender">Object that generated this event.</param>
    ''' <param name="e">Event arguments.</param>
    Public Sub DrawTour(ByVal sender As Object, ByVal e As TspEventArgs)
        Me.lastFitnessValue.Text = Math.Round(e.BestTour.Fitness, 2).ToString(CultureInfo.CurrentCulture)
        Me.lastIterationValue.Text = e.Generation.ToString(CultureInfo.CurrentCulture)

        If cityImage Is Nothing Then
            cityImage = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            cityGraphics = Graphics.FromImage(cityImage)
        End If

        Dim lastCity As Integer = 0
        Dim nextCity As Integer = e.BestTour(0).Connection1

        cityGraphics.FillRectangle(Brushes.White, 0, 0, cityImage.Width, cityImage.Height)
        For Each city As City In e.CityList
            ' Draw a rectangle for the city.
            cityGraphics.DrawRectangle(Pens.White, city.Location.X - 2, city.Location.Y - 2, 5, 5)

            ' Draw the line connecting the city.
            cityGraphics.DrawLine(Pens.Red, cityList(lastCity).Location, cityList(nextCity).Location)

            ' figure out if the next city in the list is [0] or [1]
            If lastCity <> e.BestTour(nextCity).Connection1 Then
                lastCity = nextCity
                nextCity = e.BestTour(nextCity).Connection1
            Else
                lastCity = nextCity
                nextCity = e.BestTour(nextCity).Connection2
            End If
        Next

        Me.PictureBox1.Image = cityImage

        If e.Complete Then
            StartButton.Text = "Begin"

        End If
    End Sub

    ''' <summary>
    ''' Draw just the list of cities.
    ''' </summary>
    ''' <param name="cityList">The list of cities to draw.</param>
    Private Sub DrawCityList(ByVal cityList As tsp.Cities)
        Dim cityImage As Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim graphics__1 As Graphics = Graphics.FromImage(cityImage)

        For Each city As tsp.City In cityList

            ' Draw a Rectangle  for the city.
            graphics__1.DrawRectangle(Pens.White, city.Location.X - 2, city.Location.Y - 2, 5, 5)
        Next

        Me.PictureBox1.Image = cityImage

        updateCityCount()
    End Sub

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        ' we are already running, so tell the tsp thread to halt.
        If tsp IsNot Nothing Then
            tsp.Halt = True
            Return
        End If

        Dim populationSize As Integer = 0
        Dim maxGenerations As Integer = 0
        Dim mutation As Integer = 0
        Dim groupSize As Integer = 0
        Dim numberOfCloseCities As Integer = 0
        Dim chanceUseCloseCity As Integer = 0
        Dim seed As Integer = 0

        Try
            populationSize = Convert.ToInt32(populationSizeTextBox.Text, CultureInfo.CurrentCulture)
            maxGenerations = Convert.ToInt32(maxGenerationTextBox.Text, CultureInfo.CurrentCulture)
            mutation = Convert.ToInt32(mutationTextBox.Text, CultureInfo.CurrentCulture)
            groupSize = Convert.ToInt32(groupSizeTextBox.Text, CultureInfo.CurrentCulture)
            numberOfCloseCities = Convert.ToInt32(NumberCloseCitiesTextBox.Text, CultureInfo.CurrentCulture)
            chanceUseCloseCity = Convert.ToInt32(CloseCityOddsTextBox.Text, CultureInfo.CurrentCulture)
            seed = Convert.ToInt32(randomSeedTextBox.Text, CultureInfo.CurrentCulture)
        Catch generatedExceptionName As FormatException
        End Try

        If populationSize <= 0 Then
            MessageBox.Show("You must specify a Population Size", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If maxGenerations <= 0 Then
            MessageBox.Show("You must specify a Maximum Number of Generations", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If (mutation < 0) OrElse (mutation > 100) Then
            MessageBox.Show("Mutation must be between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If (groupSize < 2) OrElse (groupSize > populationSize) Then
            MessageBox.Show("You must specify a Group (Neighborhood) Size between 2 and the population size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If (numberOfCloseCities < 3) OrElse (numberOfCloseCities >= cityList.Count) Then
            MessageBox.Show("The number of nearby cities to evaluate for the greedy initial populations must be more than 3 and less than the total number of cities.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If (chanceUseCloseCity < 0) OrElse (chanceUseCloseCity > 95) Then
            MessageBox.Show("The odds of using a nearby city when creating the initial population must be between 0% - 95%.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If seed < 0 Then
            MessageBox.Show("You must specify a Seed for the Random Number Generator", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If
        If cityList.Count < 5 Then
            MessageBox.Show("You must either load a City List file, or click the map to place at least 5 cities. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            Return
        End If

        Me.StartButton.Text = "Stop"

        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf BeginTsp))
    End Sub

    ''' <summary>
    ''' Starts up the TSP class.
    ''' This function executes on a thread pool thread.
    ''' </summary>
    ''' <param name="stateInfo">Not used</param>
    Private Sub BeginTsp(ByVal stateInfo As [Object])
        ' Assume the StartButton_Click did all the error checking
        Dim populationSize As Integer = Convert.ToInt32(populationSizeTextBox.Text, CultureInfo.CurrentCulture)
        Dim maxGenerations As Integer = Convert.ToInt32(maxGenerationTextBox.Text, CultureInfo.CurrentCulture)


        Dim mutation As Integer = Convert.ToInt32(mutationTextBox.Text, CultureInfo.CurrentCulture)
        Dim groupSize As Integer = Convert.ToInt32(groupSizeTextBox.Text, CultureInfo.CurrentCulture)
        Dim seed As Integer = Convert.ToInt32(randomSeedTextBox.Text, CultureInfo.CurrentCulture)
        Dim numberOfCloseCities As Integer = Convert.ToInt32(NumberCloseCitiesTextBox.Text, CultureInfo.CurrentCulture)
        Dim chanceUseCloseCity As Integer = Convert.ToInt32(CloseCityOddsTextBox.Text, CultureInfo.CurrentCulture)

        cityList.CalculateCityDistances(numberOfCloseCities)

        tsp = New Global.Beacon.tsp.Tsp()
        '  AddHandler Obj.Ev_Event, AddressOf EventHandler
        AddHandler tsp.foundNewBestTour, AddressOf tsp_foundNewBestTour

        ' tsp.foundNewBestTour += New Tsp.NewBestTourEventHandler(tsp_foundNewBestTour)
        tsp.Begin(populationSize, maxGenerations, groupSize, mutation, seed, chanceUseCloseCity, _
         cityList)

        tsp = Nothing
    End Sub

    Private Sub updateCityCount()
        Me.NumberCitiesValue.Text = cityList.Count.ToString()
    End Sub


    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If tsp IsNot Nothing Then
            'StatusLabel.Text = "Cannot alter city list while running"
            'StatusLabel.ForeColor = Color.Red
            Return
        End If
        '  showcoordinates()
        Dim coordinates As Point = PictureBox1.PointToClient(Cursor.Position)
        ListBox1.Items.Add(coordinates)
        cityList.Add(New City(e.X, e.Y))
        DrawCityList(cityList)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        Dim file_name As String = Application.ExecutablePath
        file_name = file_name.Substring(0, file_name.LastIndexOf("\bin")) & "\test2."

        ' Get a Bitmap.
        Dim bm As Bitmap = PictureBox1.Image

        If ComboBox1.SelectedValue = "JPEG" Then
            bm.Save(file_name & "jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        ElseIf ComboBox1.SelectedValue = "BMP" Then
            bm.Save(file_name & "bmp", System.Drawing.Imaging.ImageFormat.Bmp)
        End If


    End Sub

    Private Sub TspForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Jpeg")
        ComboBox1.Items.Add("Bmp")
        ComboBox1.Items.Add("Gif")
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub btnSavePicture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePicture.Click
        ' Compose the picture's base file name.
        Dim file_name As String = Application.ExecutablePath
        file_name = file_name.Substring(0, file_name.LastIndexOf("\bin")) & "\test."

        ' Get a Bitmap.
        Dim bm As Bitmap = PictureBox1.Image

        ' Save the picture as a bitmap, JPEG, and GIF.
        bm.Save(file_name & "bmp", _
            System.Drawing.Imaging.ImageFormat.Bmp)
        bm.Save(file_name & "jpg", _
            System.Drawing.Imaging.ImageFormat.Jpeg)
        bm.Save(file_name & "gif", _
            System.Drawing.Imaging.ImageFormat.Gif)

        MsgBox("Ok")
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            showcoordinates()
        ElseIf CheckBox1.Checked = False Then

        End If
    End Sub
    Public Sub showcoordinates()
        Dim dashValues As Single() = {5, 2, 5, 2}
        Dim blackPen As New Pen(Color.Black, 0.2)
        blackPen.DashPattern = dashValues

        g = PictureBox1.CreateGraphics
        Dim w As Integer = (PictureBox1.Width) / 20
        Dim h As Integer = (PictureBox1.Height) / 20
        Dim x, y As Integer

        While x < PictureBox1.Width
            g.DrawLine(blackPen, x, 0, x, PictureBox1.Height)
            x += w

        End While
        While y < PictureBox1.Height
            g.DrawLine(blackPen, 0, y, PictureBox1.Width, y)
            y += h

        End While
    End Sub
    Public Sub Image2Byte(ByRef NewImage As Image, ByRef ByteArr() As Byte)
        '
        Dim ImageStream As System.IO.MemoryStream
        Try
            ReDim ByteArr(0)
            If NewImage IsNot Nothing Then
                ImageStream = New System.IO.MemoryStream
                NewImage.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Function SaveImage() As Integer
        ' Dim d1 As DateTime = DateTime.Now()
        Dim d1 = "2010-03-01 2:05:06 PM"
        '-----------------------------------------------------------------
        If Not (PictureBox1.Image) Is Nothing Then
            Dim sfd As New SaveFileDialog
            sfd.Title = "Save your Image picture as...."
            sfd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
            sfd.Filter = "Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp"

            Dim result As DialogResult = sfd.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then
                If sfd.FileName <> String.Empty Then
                    'Set to Jpeg by default.
                    Dim MyImageFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                    If sfd.FileName.ToString.ToUpper.EndsWith("JPG") Then
                        MyImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                    ElseIf sfd.FileName.ToString.ToUpper.EndsWith("BMP") Then
                        MyImageFormat = System.Drawing.Imaging.ImageFormat.Bmp
                    End If
                    PictureBox1.Image.Save(sfd.FileName, MyImageFormat)

                    ' -- save in database
                    Dim myconnection As SqlConnection
                    myconnection = New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=db_beacon;Integrated Security=True;")

                    Dim CMD As New SqlClient.SqlCommand("insert into tsp (tsp_image) values(@Img)", myconnection)
                    Dim filename As String = sfd.FileName.ToString
                    'Dim filename1 As String = sfd.InitialDirectory.ToString + sfd.FileName.ToString
                    MessageBox.Show(filename)
                    CMD.Parameters.Add("@Img", SqlDbType.VarBinary, Integer.MaxValue).Value = ReadFile(filename)
                    myconnection.Open()
                    CMD.ExecuteNonQuery()
                    If CMD.ExecuteNonQuery Then
                        MessageBox.Show(" Successfull Image Entry ")
                    End If
                    myconnection.Close()
                    myconnection.Dispose()
                End If
            End If
        Else
            MessageBox.Show("Tour Diagram is empty")
        End If

        '-------------------------------------------------------------------------
        
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
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveImage()



    End Sub


    ''  Data Source=C:\Users\r0G3R b1NNy\documents\visual studio 2010\Projects\Beacon\Beacon\Database1.sdf
    'Dim myconnection As SqlConnection
    'Dim sql As String
    '' Dim item As Image = New Bitmap(PictureBox1.Image)
    ''Dim ImageByteArr() As Byte = {0}
    ''Image2Byte(PictureBox1.Image, ImageByteArr)
    ''Byte2Image(PictureBox2.Image, ImageByteArr)
    'Dim d1 As DateTime = DateTime.Now()
    'Dim adapter As New SqlDataAdapter

    'myconnection = New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=db_beacon;Integrated Security=True;")

    ''myconnection.Open()
    ''sql = "INSERT INTO tsp VALUES( @item, @d1 )"

    ''adapter.InsertCommand = New SqlCommand(Sql, myconnection)
    ''adapter.InsertCommand.ExecuteNonQuery()
    ''MsgBox("Image Inserted Successfully ")

    ''myconnection.Close()

    'Dim str As String
    'Dim fstream As FileStream
    'Dim imgdata As Byte()
    'Dim data As Byte()
    'Dim finfo As FileInfo
    'Dim file_name As String = Application.ExecutablePath
    'file_name = file_name.Substring(0, file_name.LastIndexOf("\bin")) & "\test."
    ''Dim bm As Bitmap = PictureBox1.Image

    ' '' Save the picture as a bitmap, JPEG, and GIF.
    ''bm.Save(file_name & "bmp", _
    ''    System.Drawing.Imaging.ImageFormat.Bmp)

    'finfo = New FileInfo(file_name)
    'Dim numbyte As Long
    'Dim br As BinaryReader
    'numbyte = finfo.Length
    'fstream = New FileStream(file_name, FileMode.Open, FileAccess.Read)
    'br = New BinaryReader(fstream)
    'data = br.ReadBytes(numbyte)
    'imgdata = data

    'Dim cmd As New SqlCommand
    'str = "insert into tsp values(@op, @i)"
    'cmd = New SqlCommand(str, myconnection)

    'cmd.Parameters.Add(New SqlParameter("i", SqlDbType.Image))
    'cmd.Parameters("i").Value = imgdata
    'cmd.Parameters.Add(New SqlParameter("op", SqlDbType.Char, 200))
    'cmd.Parameters("op").Value = d1
    'cmd.ExecuteNonQuery()
    'MsgBox("Image Inserted In DataBase..", MsgBoxStyle.Information)
    'Me.Close()
    ' End Sub


    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If CheckBox1.Checked = True Then
            showcoordinates()
        End If

    End Sub
End Class

