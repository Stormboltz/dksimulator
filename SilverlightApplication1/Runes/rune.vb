'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 22/09/2009
' Heure: 16:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Runes
	
    Public Class rune 'Deprecated
        Inherits Supertype

        Friend reserved As Boolean
        Friend death As Boolean
        Friend BTuntil As Long
        'Protected Sim As Sim

        Friend AvailableTime As Long

        Sub SetAvailableTime(ByVal T As Long)
            If AvailableTime > 0 Then
                If AvailableTime > T Then uptime -= (Math.Min(AvailableTime, sim.NextReset) - T)
            End If
            AvailableTime = T
        End Sub

        Sub Reset()
            AvailableTime = 0
            death = False
            reserved = False
        End Sub


        Sub New(ByVal S As Sim)
            sim = S
            Reset()
        End Sub

        Sub Use(ByVal T As Long, ByVal D As Boolean)
            Dim NextAvailable As Long
            death = D
            HitCount += 1
            If BTuntil > T Then death = True
            If AvailableTime <> 0 Then
                If T - AvailableTime <= 200 Then
                    NextAvailable = AvailableTime + RuneRefreshtime()
                Else
                    '	Uptime += T - AvailableTime - 200
                    NextAvailable = T + 800
                End If
            Else
                NextAvailable = T + RuneRefreshtime()
            End If
            uptime += RuneRefreshtime()
            If NextAvailable > sim.NextReset Then
                uptime -= NextAvailable - sim.NextReset
            End If

            AvailableTime = NextAvailable

            sim.proc.tryT104PDPS(T)
            sim.FutureEventManager.Add(AvailableTime, "Rune")
        End Sub

        Function RuneRefreshtime() As Integer
            If sim.UnholyPresence Then
                Return 1000 - 50 * sim.Character.TalentUnholy.ImprovedUnholyPresence
            Else
                Return 1000
            End If
        End Function
        Function Available(ByVal T As Long) As Boolean
            If AvailableTime <= T Then Return True
            Return False
        End Function
    End Class
End Namespace
