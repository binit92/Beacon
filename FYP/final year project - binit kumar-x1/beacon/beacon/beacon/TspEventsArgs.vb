' File Name: TspEventArgs.vb

Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing

Namespace tsp
    ''' <summary>
    ''' Event arguments when the TSP class wants the GUI to draw a tour.
    ''' </summary>
    Public Class TspEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' Default Constructor.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Constructor that sets all the properties.
        ''' </summary>
        ''' <param name="cityList">The list of cities to draw.</param>
        ''' <param name="bestTour">The tour that connects all the cities.</param>
        ''' <param name="generation">Which generation is this.</param>
        ''' <param name="complete">Is this the last update before we are done.</param>
        Public Sub New(ByVal cityList As Cities, ByVal bestTour As Tour, ByVal generation As Integer, ByVal complete As Boolean)
            Me.m_cityList = cityList
            Me.m_bestTour = bestTour
            Me.m_generation = generation
            Me.m_complete = complete
        End Sub

        ''' <summary>Private copy of the list of cities.</summary>
        Private m_cityList As Cities
        ''' <summary>Public property for list of cities.</summary>
        Public ReadOnly Property CityList() As Cities
            Get
                Return m_cityList
            End Get
        End Property

        ''' <summary>Private copy of the tour of the cities.</summary>
        Private m_bestTour As Tour
        ''' <summary>Public property for the tour of the cities.</summary>
        Public ReadOnly Property BestTour() As Tour
            Get
                Return m_bestTour
            End Get
        End Property

        ''' <summary>Private copy for which generation this tour came from.</summary>
        Private m_generation As Integer
        ''' <summary>Public property for which generation this tour came from.</summary>
        Public Property Generation() As Integer
            Get
                Return m_generation
            End Get
            Set(ByVal value As Integer)
                m_generation = value
            End Set
        End Property

        ''' <summary>Private copy indicating if the TSP algorithm is complete.</summary>
        Private m_complete As Boolean = False
        ''' <summary>Public property indicating if the TSP algorithm is complete.</summary>
        Public Property Complete() As Boolean
            Get
                Return m_complete
            End Get
            Set(ByVal value As Boolean)
                m_complete = value
            End Set
        End Property
    End Class
End Namespace
