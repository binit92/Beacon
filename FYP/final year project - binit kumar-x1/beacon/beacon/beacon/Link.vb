' File Name: Link.vb
'  Link  - an individual link between 2 cities at a time ( unique in every single tour) 
'  private int connection1
'  private int Connection1 ( getter and setter )
'  private int connection2
'  private int Connection2 ( getter and setter )

Imports System.Collections.Generic
Imports System.Text

Namespace tsp
    ''' <summary>
    ''' An individual link between 2 cities in a tour.
    ''' This city connects to 2 other cities.
    ''' </summary>
    ''' 
    Public Class Link
        ''' <summary>
        ''' Connection to the first city. ( private copy) 
        ''' </summary>
        Private m_connection1 As Integer
        ''' <summary>
        ''' Connection to the first city.
        ''' </summary>
        Public Property Connection1() As Integer
            Get
                Return m_connection1
            End Get
            Set(ByVal value As Integer)
                m_connection1 = value


            End Set
        End Property

        ''' <summary>
        ''' Connection to the second city. ( private copy )
        ''' </summary>
        Private m_connection2 As Integer
        ''' <summary>
        ''' Connection to the second city.
        ''' </summary>
        Public Property Connection2() As Integer
            Get
                Return m_connection2
            End Get
            Set(ByVal value As Integer)
                m_connection2 = value
            End Set
        End Property
    End Class
End Namespace