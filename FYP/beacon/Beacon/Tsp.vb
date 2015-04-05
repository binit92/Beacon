' File Name: Tsp.vb
'

Imports System.Collections.Generic
Imports System.Windows.Forms
' windows form
Imports System.Text
Imports System.Data
Imports System.Drawing

Namespace tsp
    ''' <summary>
    ''' This class performs the Travelling Salesman Problem algorithm.
    ''' </summary>
    Class Tsp
        ''' <summary>
        ''' Delegate used to raise an event when a new best tour is found.
        ''' </summary>
        ''' <param name="sender">Object that generated this event.</param>
        ''' <param name="e">Event arguments. Contains information about the best tour.</param>
        Public Delegate Sub NewBestTourEventHandler(ByVal sender As [Object], ByVal e As TspEventArgs)

        ''' <summary>
        ''' Event fired when a new best tour is found.
        ''' </summary>
        Public Event foundNewBestTour As NewBestTourEventHandler

        ''' <summary>
        ''' Random number generator object.
        ''' We allow the GUI to set the seed for the random number generator to assist in debugging.
        ''' This allows errors to be easily reproduced.
        ''' </summary>
        Private rand As Random

        ''' <summary>
        ''' The list of cities. This is only used to calculate the distances between the cities.
        ''' </summary>
        Private cityList As Cities

        ''' <summary>
        ''' The complete list of all the tours.
        ''' </summary>
        Private population As Population

        ''' <summary>
        ''' Private copy of a flag that will stop the TSP from calculating any more generations.
        ''' </summary>
        Private m_halt As Boolean = False
        ''' <summary>
        ''' The GUI sets this flag to true to stop the TSP algorithm and allow the Begin() function to return.
        ''' </summary>
        Public Property Halt() As Boolean
            Get
                Return m_halt
            End Get
            Set(ByVal value As Boolean)
                m_halt = value
            End Set
        End Property

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Starts the TSP algorithm.
        ''' To stop before all generations are calculated, set <see cref="Halt"/> to true.
        ''' </summary>
        ''' <param name="populationSize">Number of random tours to create before starting the algorithm.</param>
        ''' <param name="maxGenerations">Number of times to perform the crossover operation before stopping.</param>
        ''' <param name="groupSize">Number of tours to examine in each generation. Top 2 are chosen as the parent tours whose children replace the worst 2 tours in the group.</param>
        ''' <param name="mutation">Odds that a child tour will be mutated..</param>
        ''' <param name="seed">Seed for the random number generator.</param>
        ''' <param name="chanceToUseCloseCity">The odds (out of 100) that a city that is known to be close will be used in any given link.</param>
        ''' <param name="cityList">List of cities in the tour.</param>
        Public Sub Begin(ByVal populationSize As Integer, ByVal maxGenerations As Integer, ByVal groupSize As Integer, ByVal mutation As Integer, ByVal seed As Integer, ByVal chanceToUseCloseCity As Integer, _
         ByVal cityList As Cities)
            rand = New Random(seed)

            Me.cityList = cityList

            population = New Population()
            population.CreateRandomPopulation(populationSize, cityList, rand, chanceToUseCloseCity)

            displayTour(population.BestTour, 0, False)

            Dim foundNewBestTour As Boolean = False
            Dim generation As Integer
            For generation = 0 To maxGenerations - 1
                If Halt Then
                    ' GUI has requested we exit.
                    Exit For
                End If
                foundNewBestTour = makeChildren(groupSize, mutation)

                If foundNewBestTour Then
                    displayTour(population.BestTour, generation, False)
                End If
            Next

            displayTour(population.BestTour, generation, True)
        End Sub

        ''' <summary>
        ''' Randomly select a group of tours from the population. 
        ''' The top 2 are chosen as the parent tours.
        ''' Crossover is performed on these 2 tours.
        ''' The childred tours from this process replace the worst 2 tours in the group.
        ''' </summary>
        ''' <param name="groupSize">Number of tours in this group.</param>
        ''' <param name="mutation">Odds that a child will be mutated.</param>
        Private Function makeChildren(ByVal groupSize As Integer, ByVal mutation As Integer) As Boolean
            Dim tourGroup As Integer() = New Integer(groupSize - 1) {}
            Dim tourCount As Integer, i As Integer, topTour As Integer, childPosition As Integer, tempTour As Integer

            ' pick random tours to be in the neighborhood city group
            ' we allow for the same tour to be included twice
            For tourCount = 0 To groupSize - 1
                tourGroup(tourCount) = rand.[Next](population.Count)
            Next

            ' bubble sort on the neighborhood city group
            For tourCount = 0 To groupSize - 2
                topTour = tourCount
                For i = topTour + 1 To groupSize - 1
                    If population(tourGroup(i)).Fitness < population(tourGroup(topTour)).Fitness Then
                        topTour = i
                    End If
                Next

                If topTour <> tourCount Then
                    tempTour = tourGroup(tourCount)
                    tourGroup(tourCount) = tourGroup(topTour)
                    tourGroup(topTour) = tempTour
                End If
            Next

            Dim foundNewBestTour As Boolean = False

            ' take the best 2 tours, do crossover, and replace the worst tour with it
            childPosition = tourGroup(groupSize - 1)
            population(childPosition) = Tour.Crossover(population(tourGroup(0)), population(tourGroup(1)), cityList, rand)
            If rand.[Next](100) < mutation Then
                population(childPosition).Mutate(rand)
            End If
            population(childPosition).DetermineFitness(cityList)

            ' now see if the first new tour has the best fitness
            If population(childPosition).Fitness < population.BestTour.Fitness Then
                population.BestTour = population(childPosition)
                foundNewBestTour = True
            End If

            ' take the best 2 tours (opposite order), do crossover, and replace the 2nd worst tour with it
            childPosition = tourGroup(groupSize - 2)
            population(childPosition) = Tour.Crossover(population(tourGroup(1)), population(tourGroup(0)), cityList, rand)
            If rand.[Next](100) < mutation Then
                population(childPosition).Mutate(rand)
            End If
            population(childPosition).DetermineFitness(cityList)

            ' now see if the second new tour has the best fitness
            If population(childPosition).Fitness < population.BestTour.Fitness Then
                population.BestTour = population(childPosition)
                foundNewBestTour = True
            End If

            Return foundNewBestTour
        End Function

        ''' <summary>
        ''' Raise an event to the GUI listener to display a tour.
        ''' </summary>
        ''' <param name="bestTour">The best tour the algorithm has found so far.</param>
        ''' <param name="generationNumber">How many generations have been performed.</param>
        ''' <param name="complete">Is the TSP algorithm complete.</param>
        Private Sub displayTour(ByVal bestTour As Tour, ByVal generationNumber As Integer, ByVal complete As Boolean)
            RaiseEvent foundNewBestTour(Me, New TspEventArgs(cityList, bestTour, generationNumber, complete))
        End Sub
    End Class
End Namespace