'   File Name :Cities.vb
'   Cities class  inherits the list of cities 
'  public void CalculateCityDistances (int)
'  public void OpenCityList(string)


Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.IO
Imports System.Globalization
Imports Microsoft.VisualBasic


Namespace tsp
    ''' <summary>
    ''' contains list of cities
    ''' Each city has a location and the distance information to every other city.
    ''' </summary>
    Public Class Cities
        Inherits List(Of City)
        ''' <summary>
        ''' Determine the distances between each city.
        ''' </summary>
        ''' <param name="numberOfCloseCities">When creating the initial population of tours, this is a greater chance
        ''' that a nearby city will be chosen for a link. This is the number of nearby cities that will be considered close.</param>
        Public Sub CalculateCityDistances(ByVal numberOfCloseCities As Integer)
            For Each city As City In Me
                city.Distances.Clear()

                For i As Integer = 0 To Count - 1
                    ' derived from Pythagoraus theorem
                    city.Distances.Add(Math.Sqrt(Math.Pow(CDbl(city.Location.X - Me(i).Location.X), 2.0) + Math.Pow(CDbl(city.Location.Y - Me(i).Location.Y), 2.0)))
                Next
            Next

            For Each city As City In Me
                city.FindClosestCities(numberOfCloseCities)
            Next
        End Sub


        Public Sub OpenCityList(ByVal fileName As String)
            Dim cityDS As New DataSet()

            Try
                Me.Clear()

                cityDS.ReadXml(fileName)

                Dim cities As DataRowCollection = cityDS.Tables(0).Rows

                For Each city As DataRow In cities
                    Me.Add(New City(Convert.ToInt32(city("X"), CultureInfo.CurrentCulture), Convert.ToInt32(city("Y"), CultureInfo.CurrentCulture)))
                Next
            Finally
                cityDS.Dispose()
            End Try
        End Sub
    End Class
End Namespace
