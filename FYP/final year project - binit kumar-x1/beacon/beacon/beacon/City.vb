'  File Name : City.vb
'   City class reflects individual city 
'   public City (int ,int )
'   private point location
'   public point Location  ( getter and setter )
'   private List<double> distances
'   public  List<double> Distances  (getter and setter) 
'   private List<int> closeCities
'   private List<int> CloseCities
'   public  void FindClosetCities(int) 

Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing

Namespace tsp
    ''' <summary>city class for every single city that makes tour </summary>

    Public Class City
        ''' <summary>Constructor that provides the city location.</summary>
        ''' <param> X and Y location of city </param>

        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Location = New Point(x, y)
        End Sub


        ''' <summary >location of point type (private copy)</summary>
        Private m_location As Point


        '''<summary > The location of this city</summary>
        Public Property Location() As Point
            Get
                Return m_location
            End Get
            Set(ByVal value As Point)
                ' property setter .. 
                m_location = value
            End Set
        End Property

        ''' <summary>
        '''  distance from this city to every other city. ( private copy )
        ''' The index in this array is the number of the city linked to.
        ''' </summary>
        Private m_distances As New List(Of Double)()



        ''' <summary>
        ''' The distance from this city to every other city.
        ''' </summary>
        Public Property Distances() As List(Of Double)
            Get
                Return m_distances
            End Get
            Set(ByVal value As List(Of Double))
                m_distances = value
            End Set
        End Property

        ''' <summary>
        ''' list of the cities that are closest to this one.(private copy)
        ''' </summary>
        Private m_closeCities As New List(Of Integer)()

        ''' <summary>
        ''' A list of the cities that are closest to this one.
        ''' </summary>
        Public ReadOnly Property CloseCities() As List(Of Integer)
            Get
                Return m_closeCities
            End Get
        End Property

        ''' <summary>
        ''' Find the cities that are closest to this one.
        ''' </summary>
        ''' <param name="numberOfCloseCities">When creating the initial population of tours, this is a greater chance
        ''' that a nearby city will be chosen for a link. This is the number of nearby cities that will be considered close.</param>
        Public Sub FindClosestCities(ByVal numberOfCloseCities As Integer)
            Dim shortestDistance As Double
            Dim shortestCity As Integer = 0
            Dim dist As Double() = New Double(Distances.Count - 1) {}
            Distances.CopyTo(dist)

            If numberOfCloseCities > Distances.Count - 1 Then
                numberOfCloseCities = Distances.Count - 1
            End If

            m_closeCities.Clear()

            For i As Integer = 0 To numberOfCloseCities - 1
                shortestDistance = [Double].MaxValue
                For cityNum As Integer = 0 To Distances.Count - 1
                    If dist(cityNum) < shortestDistance Then
                        shortestDistance = dist(cityNum)
                        shortestCity = cityNum
                    End If
                Next
                m_closeCities.Add(shortestCity)
                dist(shortestCity) = [Double].MaxValue
            Next
        End Sub
    End Class
End Namespace