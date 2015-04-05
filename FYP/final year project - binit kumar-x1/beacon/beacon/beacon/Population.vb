' File Name: Population.vb
' Population inherits the list of tours

Imports System.Collections
Imports System.Collections.Generic
Imports System.Text

Namespace tsp
    Class Population
        Inherits List(Of Tour)
        ''' <summary>
        ''' Private copy of the best tour found so far by the Genetic Algorithm.
        ''' </summary>
        Private m_bestTour As Tour = Nothing
        ''' <summary>
        ''' The best tour found so far by the Genetic Algorithm.
        ''' </summary>
        Public Property BestTour() As Tour
            Get
                Return m_bestTour
            End Get
            Set(ByVal value As Tour)
                m_bestTour = value
            End Set
        End Property

        ''' <summary>
        ''' Create the initial set of random tours.
        ''' </summary>
        ''' <param name="populationSize">Number of tours to create.</param>sn
        ''' <param name="cityList">The list of cities in this tour.</param>
        ''' <param name="rand">Random number generator. We pass around the same random number generator, so that results between runs are consistent.</param>
        ''' <param name="chanceToUseCloseCity">The odds (out of 100) that a city that is known to be close will be used in any given link.</param>
        Public Sub CreateRandomPopulation(ByVal populationSize As Integer, ByVal cityList As Cities, ByVal rand As Random, ByVal chanceToUseCloseCity As Integer)
            Dim firstCity As Integer, lastCity As Integer, nextCity As Integer

            For tourCount As Integer = 0 To populationSize - 1
                Dim tour As New Tour(cityList.Count)

                ' Create a starting point for this tour
                firstCity = rand.[Next](cityList.Count)
                lastCity = firstCity

                For city As Integer = 0 To cityList.Count - 2
                    Do
                        ' Keep picking random cities for the next city, until we find one we haven't been to.
                        If (rand.[Next](100) < chanceToUseCloseCity) AndAlso (cityList(city).CloseCities.Count > 0) Then
                            ' 75% chance will will pick a city that is close to this one
                            nextCity = cityList(city).CloseCities(rand.[Next](cityList(city).CloseCities.Count))
                        Else
                            ' Otherwise, pick a completely random city.
                            nextCity = rand.[Next](cityList.Count)
                            ' Make sure we haven't been here, and make sure it isn't where we are at now.
                        End If
                    Loop While (tour(nextCity).Connection2 <> -1) OrElse (nextCity = lastCity)

                    ' When going from city A to B, [1] on A = B and [1] on city B = A
                    tour(lastCity).Connection2 = nextCity
                    tour(nextCity).Connection1 = lastCity
                    lastCity = nextCity
                Next

                ' Connect the last 2 cities.
                tour(lastCity).Connection2 = firstCity
                tour(firstCity).Connection1 = lastCity

                tour.DetermineFitness(cityList)

                Add(tour)

                If (m_bestTour Is Nothing) OrElse (tour.Fitness < m_bestTour.Fitness) Then
                    BestTour = tour
                End If
            Next
        End Sub
    End Class
End Namespace