'
' Created by SharpDevelop.
' User: e0030653
' Date: 26/03/2009
' Time: 09:42
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class DeathChill
        Friend Cd As Long
        Friend Active As Boolean
        Protected Sim As Sim



        Sub New(ByVal MySim As Sim)
            Cd = 0
            Active = False
            Sim = MySim

        End Sub
        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Character.Talents.Talent("Deathchill").Value = 1 And Cd <= T Then
                Return True
            Else
                Return False
            End If
        End Function
        Sub use(ByVal T As Long)
            Cd = T + 3 * 60 * 100
            Sim.Threat = Sim.Threat + 55
            Active = True
        End Sub
    End Class
End Namespace