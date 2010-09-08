'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:40 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class BloodPresence
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
        End Sub

        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Runes.AnyBlood(T) Then
                Return True
            Else
                Return False
            End If

        End Function


        Function Use(ByVal T As Long) As Boolean
            Sim.BloodPresence = 1
            Sim.UnholyPresence = 0
            Sim.FrostPresence = 0

            Sim.Runes.UseBlood(T, False)
            Sim.CombatLog.write(T & vbTab & "Switch to Blood Presence")
            Me.HitCount = Me.HitCount + 1
            Sim._UseGCD(T, 1)
            Return True
        End Function

    End Class
End Namespace