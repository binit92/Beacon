' File Name: Tour.vb
' Tour is the list of links 
'  Tour(int) :base   - take value from the parent class 
'  private double fitness
'  Public double fitness
'  private void resetTour(int)
'  private void DetermineFitness (cities)   -cities 
'  private static void joinCities(Tour tour, int[] cityUsage, int city1, int city2)
'  private static int findNextCity(Tour parent, Tour child, Cities cityList, int[] cityUsage, int city)
'  private static bool testConnectionValid(Tour tour, Cities cityList, int[] cityUsage, int city1, int city2)
'  public static Tour Crossover(Tour parent1, Tour parent2, Cities cityList, Random rand)
'  public void Mutate(Random rand)


Imports System.Collections.Generic
Imports System.Text

Namespace tsp
    ''' <summary>
    ''' This class represents one instance of a tour through all the cities.
    ''' </summary>
    Public Class Tour
        Inherits List(Of Link)
        ''' <summary>
        ''' Constructor that takes a default capacity.
        ''' </summary>
        ''' <param name="capacity">Initial size of the tour. Should be the number of cities in the tour.</param>
        Public Sub New(ByVal capacity As Integer)
            MyBase.New(capacity)
            resetTour(capacity)
        End Sub

        ''' <summary>        ///  fitness of this tour. (private copy)        /// </summary>
        Private m_fitness As Double

        ''' <summary>    /// The fitness (total tour length) of this tour.    /// </summary>

        Public Property Fitness() As Double
            Get
                Return m_fitness
            End Get
            Set(ByVal value As Double)
                m_fitness = value
            End Set
        End Property

        ''' <summary>
        ''' Creates the tour with the correct number of cities and creates initial connections of all -1.
        ''' </summary>
        ''' <param name="numberOfCities"></param>
        Private Sub resetTour(ByVal numberOfCities As Integer)
            Me.Clear()

            Dim link As Link
            For i As Integer = 0 To numberOfCities - 1
                link = New Link()
                link.Connection1 = -1
                link.Connection2 = -1
                Me.Add(link)
            Next
        End Sub

        ''' <summary>
        ''' Determine the fitness (total length) of an individual tour.
        ''' </summary>
        ''' <param name="cities">The cities in this tour. Used to get the distance between each city.</param>
        Public Sub DetermineFitness(ByVal cities As Cities)
            Fitness = 0

            Dim lastCity As Integer = 0
            Dim nextCity As Integer = Me(0).Connection1

            For Each link As Link In Me
                Fitness += cities(lastCity).Distances(nextCity)

                ' figure out if the next city in the list is [0] or [1]
                If lastCity <> Me(nextCity).Connection1 Then
                    lastCity = nextCity
                    nextCity = Me(nextCity).Connection1
                Else
                    lastCity = nextCity
                    nextCity = Me(nextCity).Connection2
                End If
            Next
        End Sub

        ''' <summary>
        ''' Creates a link between 2 cities in a tour, and then updates the city usage.
        ''' </summary>
        ''' <param name="tour">The incomplete child tour.</param>
        ''' <param name="cityUsage">Number of times each city has been used in this tour. Is updated when cities are joined.</param>
        ''' <param name="city1">The first city in the link.</param>
        ''' <param name="city2">The second city in the link.</param>
        Private Shared Sub joinCities(ByVal tour As Tour, ByVal cityUsage As Integer(), ByVal city1 As Integer, ByVal city2 As Integer)
            ' Determine if the [0] or [1] link is available in the tour to make this link.
            If tour(city1).Connection1 = -1 Then
                tour(city1).Connection1 = city2
            Else
                tour(city1).Connection2 = city2
            End If

            If tour(city2).Connection1 = -1 Then
                tour(city2).Connection1 = city1
            Else
                tour(city2).Connection2 = city1
            End If

            cityUsage(city1) += 1
            cityUsage(city2) += 1
        End Sub


        ''' <summary>
        ''' Find a link from a given city in the parent tour that can be placed in the child tour.
        ''' If both links in the parent aren't valid links for the child tour, return -1.
        ''' </summary>
        ''' <param name="parent">The parent tour to get the link from.</param>
        ''' <param name="child">The child tour that the link will be placed in.</param>
        ''' <param name="cityList">The list of cities in this tour.</param>
        ''' <param name="cityUsage">Number of times each city has been used in the child.</param>
        ''' <param name="city">City that we want to link from.</param>
        ''' <returns>The city to link to in the child tour, or -1 if none are valid.</returns>
        Private Shared Function findNextCity(ByVal parent As Tour, ByVal child As Tour, ByVal cityList As Cities, ByVal cityUsage As Integer(), ByVal city As Integer) As Integer
            If testConnectionValid(child, cityList, cityUsage, city, parent(city).Connection1) Then
                Return parent(city).Connection1
            ElseIf testConnectionValid(child, cityList, cityUsage, city, parent(city).Connection2) Then
                Return parent(city).Connection2
            End If

            Return -1
        End Function

        ''' <summary>
        ''' Determine if it is OK to connect 2 cities given the existing connections in a child tour.
        ''' If the two cities can be connected already (witout doing a full tour) then it is an invalid link.
        ''' </summary>
        ''' <param name="tour">The incomplete child tour.</param>
        ''' <param name="cityList">The list of cities in this tour.</param>
        ''' <param name="cityUsage">Array that contains the number of times each city has been linked.</param>
        ''' <param name="city1">The first city in the link.</param>
        ''' <param name="city2">The second city in the link.</param>
        ''' <returns>True if the connection can be made.</returns>
        Private Shared Function testConnectionValid(ByVal tour As Tour, ByVal cityList As Cities, ByVal cityUsage As Integer(), ByVal city1 As Integer, ByVal city2 As Integer) As Boolean
            ' Quick check to see if cities already connected or if either already has 2 links
            If (city1 = city2) OrElse (cityUsage(city1) = 2) OrElse (cityUsage(city2) = 2) Then
                Return False
            End If

            ' A quick check to save CPU. If haven't been to either city, connection must be valid.
            If (cityUsage(city1) = 0) OrElse (cityUsage(city2) = 0) Then
                Return True
            End If

            ' Have to see if the cities are connected by going in each direction.
            For direction As Integer = 0 To 1
                Dim lastCity As Integer = city1
                Dim currentCity As Integer
                If direction = 0 Then
                    ' on first pass, use the first connection
                    currentCity = tour(city1).Connection1
                Else
                    ' on second pass, use the other connection
                    currentCity = tour(city1).Connection2
                End If
                Dim tourLength As Integer = 0
                While (currentCity <> -1) AndAlso (currentCity <> city2) AndAlso (tourLength < cityList.Count - 2)
                    tourLength += 1
                    ' figure out if the next city in the list is [0] or [1]
                    If lastCity <> tour(currentCity).Connection1 Then
                        lastCity = currentCity
                        currentCity = tour(currentCity).Connection1
                    Else
                        lastCity = currentCity
                        currentCity = tour(currentCity).Connection2
                    End If
                End While

                ' if cities are connected, but it goes through every city in the list, then OK to join.
                If tourLength >= cityList.Count - 2 Then
                    Return True
                End If

                ' if the cities are connected without going through all the cities, it is NOT OK to join.
                If currentCity = city2 Then
                    Return False
                End If
            Next

            ' if cities weren't connected going in either direction, we are OK to join them
            Return True
        End Function

        ''' <summary>
        ''' Perform the crossover operation on 2 parent tours to create a new child tour.
        ''' This function should be called twice to make the 2 children.
        ''' In the second call, the parent parameters should be swapped.
        ''' </summary>
        ''' <param name="parent1">The first parent tour.</param>
        ''' <param name="parent2">The second parent tour.</param>
        ''' <param name="cityList">The list of cities in this tour.</param>
        ''' <param name="rand">Random number generator. We pass around the same random number generator, so that results between runs are consistent.</param>
        ''' <returns>The child tour.</returns>
        Public Shared Function Crossover(ByVal parent1 As Tour, ByVal parent2 As Tour, ByVal cityList As Cities, ByVal rand As Random) As Tour
            Dim child As New Tour(cityList.Count)
            ' the new tour we are making
            Dim cityUsage As Integer() = New Integer(cityList.Count - 1) {}
            ' how many links 0-2 that connect to this city
            Dim city As Integer
            ' for loop variable
            Dim nextCity As Integer
            ' the other city in this link
            For city = 0 To cityList.Count - 1
                cityUsage(city) = 0
            Next

            ' Take all links that both parents agree on and put them in the child
            For city = 0 To cityList.Count - 1
                If cityUsage(city) < 2 Then
                    If parent1(city).Connection1 = parent2(city).Connection1 Then
                        nextCity = parent1(city).Connection1
                        If testConnectionValid(child, cityList, cityUsage, city, nextCity) Then
                            joinCities(child, cityUsage, city, nextCity)
                        End If
                    End If
                    If parent1(city).Connection2 = parent2(city).Connection2 Then
                        nextCity = parent1(city).Connection2
                        If testConnectionValid(child, cityList, cityUsage, city, nextCity) Then

                            joinCities(child, cityUsage, city, nextCity)
                        End If
                    End If
                    If parent1(city).Connection1 = parent2(city).Connection2 Then
                        nextCity = parent1(city).Connection1
                        If testConnectionValid(child, cityList, cityUsage, city, nextCity) Then
                            joinCities(child, cityUsage, city, nextCity)
                        End If
                    End If
                    If parent1(city).Connection2 = parent2(city).Connection1 Then
                        nextCity = parent1(city).Connection2
                        If testConnectionValid(child, cityList, cityUsage, city, nextCity) Then
                            joinCities(child, cityUsage, city, nextCity)
                        End If
                    End If
                End If
            Next

            ' The parents don't agree on whats left, so we will alternate between using
            ' links from parent 1 and then parent 2.

            For city = 0 To cityList.Count - 1
                If cityUsage(city) < 2 Then
                    If city Mod 2 = 1 Then
                        ' we prefer to use parent 1 on odd cities
                        nextCity = findNextCity(parent1, child, cityList, cityUsage, city)
                        If nextCity = -1 Then
                            ' but if thats not possible we still go with parent 2
                            nextCity = findNextCity(parent2, child, cityList, cityUsage, city)


                        End If
                    Else
                        ' use parent 2 instead
                        nextCity = findNextCity(parent2, child, cityList, cityUsage, city)
                        If nextCity = -1 Then
                            nextCity = findNextCity(parent1, child, cityList, cityUsage, city)
                        End If
                    End If

                    If nextCity <> -1 Then
                        joinCities(child, cityUsage, city, nextCity)

                        ' not done yet. must have been 0 in above case.
                        If cityUsage(city) = 1 Then
                            If city Mod 2 <> 1 Then
                                ' use parent 1 on even cities
                                nextCity = findNextCity(parent1, child, cityList, cityUsage, city)
                                If nextCity = -1 Then
                                    ' use parent 2 instead
                                    nextCity = findNextCity(parent2, child, cityList, cityUsage, city)
                                End If
                            Else
                                ' use parent 2
                                nextCity = findNextCity(parent2, child, cityList, cityUsage, city)
                                If nextCity = -1 Then
                                    nextCity = findNextCity(parent1, child, cityList, cityUsage, city)
                                End If
                            End If

                            If nextCity <> -1 Then
                                joinCities(child, cityUsage, city, nextCity)
                            End If
                        End If
                    End If
                End If
            Next

            ' Remaining links must be completely random.
            ' Parent's links would cause multiple disconnected loops.
            For city = 0 To cityList.Count - 1
                While cityUsage(city) < 2
                    Do
                        ' pick a random city, until we find one we can link to
                        nextCity = rand.[Next](cityList.Count)
                    Loop While Not testConnectionValid(child, cityList, cityUsage, city, nextCity)

                    joinCities(child, cityUsage, city, nextCity)
                End While
            Next

            Return child
        End Function

        ''' <summary>
        ''' Randomly change one of the links in this tour.
        ''' </summary>
        ''' <param name="rand">Random number generator. We pass around the same random number generator, so that results between runs are consistent.</param>
        Public Sub Mutate(ByVal rand As Random)
            Dim cityNumber As Integer = rand.[Next](Me.Count)
            Dim link As Link = Me(cityNumber)
            Dim tmpCityNumber As Integer

            ' Find which 2 cities connect to cityNumber, and then connect them directly
            If Me(link.Connection1).Connection1 = cityNumber Then
                ' Conn 1 on Conn 1 link points back to us.
                If Me(link.Connection2).Connection1 = cityNumber Then
                    ' Conn 1 on Conn 2 link points back to us.
                    tmpCityNumber = link.Connection2
                    Me(link.Connection2).Connection1 = link.Connection1
                    Me(link.Connection1).Connection1 = tmpCityNumber
                Else
                    ' Conn 2 on Conn 2 link points back to us.
                    tmpCityNumber = link.Connection2
                    Me(link.Connection2).Connection2 = link.Connection1
                    Me(link.Connection1).Connection1 = tmpCityNumber
                End If
            Else
                ' Conn 2 on Conn 1 link points back to us.
                If Me(link.Connection2).Connection1 = cityNumber Then
                    ' Conn 1 on Conn 2 link points back to us.
                    tmpCityNumber = link.Connection2
                    Me(link.Connection2).Connection1 = link.Connection1
                    Me(link.Connection1).Connection2 = tmpCityNumber
                Else
                    ' Conn 2 on Conn 2 link points back to us.
                    tmpCityNumber = link.Connection2
                    Me(link.Connection2).Connection2 = link.Connection1
                    Me(link.Connection1).Connection2 = tmpCityNumber

                End If
            End If

            Dim replaceCityNumber As Integer = -1
            Do
                replaceCityNumber = rand.[Next](Me.Count)
            Loop While replaceCityNumber = cityNumber
            Dim replaceLink As Link = Me(replaceCityNumber)

            ' Now we have to reinsert that city back into the tour at a random location
            tmpCityNumber = replaceLink.Connection2
            link.Connection2 = replaceLink.Connection2
            link.Connection1 = replaceCityNumber
            replaceLink.Connection2 = cityNumber

            If Me(tmpCityNumber).Connection1 = replaceCityNumber Then
                Me(tmpCityNumber).Connection1 = cityNumber
            Else
                Me(tmpCityNumber).Connection2 = cityNumber
            End If
        End Sub
    End Class
End Namespace