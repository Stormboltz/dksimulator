Public Class TimeWastedAnaliser

    Friend TimeWastedCol As New List(Of TimeWasted)


    Sub New()

    End Sub

    Sub Report()
#If DEBUG Then
        Dim L As Long
        For Each T As TimeWasted In (From tw In TimeWastedCol
                                     Where tw.Total <> 0
                                     Order By tw.Total Descending)
            Diagnostics.Debug.WriteLine(T.SimObject.Name & ": " & T.Total / 100000 & "ms")
            L += T.Total
        Next
#End If

    End Sub


    Class TimeWasted
        Friend Total As Long
        Friend LastStart As Long
        Protected sim As Simulator.Sim
        Protected Friend SimObject As Simulator.SimObjet
        Friend Name As String

        Sub New(ByVal SimObject As Simulator.SimObjet)
            Me.SimObject = SimObject
            Me.sim = SimObject.sim
            sim.TimeWastedAnaliser.TimeWastedCol.Add(Me)
        End Sub


        Sub Start()
#If DEBUG Then
            If LastStart <> 0 Then
                Diagnostics.Debug.WriteLine("TimeWasted: Start without prior ending")
            End If
            LastStart = Now.Ticks
#End If
        End Sub
        Sub Pause()
#If DEBUG Then

            Total += Now.Ticks - LastStart
            LastStart = 0
#End If
        End Sub

    End Class



End Class
