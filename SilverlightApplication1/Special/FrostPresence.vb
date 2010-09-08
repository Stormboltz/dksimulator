'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:46 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class FrostPresence

        Inherits Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
        End Sub

        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Runes.Frost(T) Then
                Return True
            Else
                Return False
            End If
        End Function


        Function Use(ByVal T As Long) As Boolean
            Sim.BloodPresence = 0
            Sim.UnholyPresence = 0
            Sim.FrostPresence = 10 + (2.5 * Sim.Character.Talents.Talent("IFrostPresence").Value)
            Sim.Runes.UseFrost(T, False)
            Sim.CombatLog.write(T & vbTab & "Switch to Frost Presence")
            Me.HitCount = Me.HitCount + 1
            Sim._UseGCD(T, 1)

            Return True
        End Function

        Sub SetForFree()
            Sim.BloodPresence = 0
            Sim.UnholyPresence = 0
            Sim.FrostPresence = 10 + (2.5 * Sim.Character.Talents.Talent("IFrostPresence").Value)
        End Sub


    End Class
End Namespace