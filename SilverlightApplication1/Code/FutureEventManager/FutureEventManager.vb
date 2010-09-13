'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 12:08
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Namespace Simulator
    Public Class FutureEventManager
        Inherits SimObjet
        Protected List As New Collections.Generic.List(Of FutureEvent)
        Friend Max As Integer



        Sub New(ByVal s As Sim)
            MyBase.new(s)
            _Name = "FutureEventManager"
        End Sub


        Function Add(ByVal T As Long, ByVal Ev As String, Optional ByVal ReferenceObject As Simulator.WowObjects.WowObject = Nothing) As FutureEvent
            TimeWasted.Start()
            If T < sim.TimeStamp Then
                Diagnostics.Debug.WriteLine("Try to go back in time")
            End If
            Dim FE As New FutureEvent(T, Ev, ReferenceObject)

            'List.Add(FE)
            List.Insert(GetRowToInsert(T), FE)
            'Dim myComparer As myEventComparer = New myEventComparer()
            'List.Sort(myComparer)
            TimeWasted.Pause()
            Return FE
        End Function

        Function GetRowToInsert(ByVal T As Long) As Integer

            Dim FE As FutureEvent
            Dim i As Integer
            For i = 0 To List.Count - 1
                FE = List.Item(i)
                If FE.T >= T Then Return i
            Next
            Return List.Count


        End Function

        Function GetFirst() As FutureEvent

            TimeWasted.Start()



            Dim FE As FutureEvent
            ' = (From F In List
            '  Order By F.T Ascending
            '  Select F).First

            'List.Remove(FE)
            FE = List.Item(0)
            List.Remove(FE)
            TimeWasted.Pause()
            Return FE
        End Function

        Sub Clear()
            List.Clear()
        End Sub

        Sub Remove(ByVal Ev As String)
            TimeWasted.Start()
            Dim FE As FutureEvent
            For Each FE In List
                If FE.Ev = Ev Then
                    List.Remove(FE)
                End If
            Next
            TimeWasted.Pause()
        End Sub
        Sub Remove(ByVal Ev As FutureEvent)
            TimeWasted.Start()
            List.Remove(Ev)
            TimeWasted.Pause()
        End Sub



        Sub Reschedule(ByVal Ev As String, ByVal T As Long)
            TimeWasted.Start()
            Dim FE As FutureEvent
            For Each FE In List
                If FE.Ev = Ev Then FE.T = T
            Next
            Dim myComparer As myEventComparer = New myEventComparer()
            List.Sort(myComparer)
            TimeWasted.Pause()
        End Sub

        Public Class myEventComparer
            Implements System.Collections.Generic.IComparer(Of FutureEvent)

            Function Compare(ByVal x As FutureEvent, ByVal y As FutureEvent) As Integer Implements System.Collections.Generic.IComparer(Of DKSIMVB.FutureEvent).Compare
                Dim i As Integer = 0
                i = System.Collections.Generic.Comparer(Of Long).Default.Compare(x.T, y.T)
                Return i
            End Function
        End Class

    End Class
End Namespace