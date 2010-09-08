'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/5/2009
' Heure: 1:38 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class EmpowerRuneWeapon
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
        End Sub

        Function Use(ByVal T As Long) As Boolean
            If CD > T Then Return False
            CD = T + (5 * 60 * 100)

            Sim.Runes.BloodRune1.Activate()
            Sim.Runes.BloodRune2.Activate()
            Sim.Runes.FrostRune1.Activate()
            Sim.Runes.FrostRune2.Activate()
            Sim.Runes.UnholyRune1.Activate()
            Sim.Runes.UnholyRune2.Activate()
            Sim.RunicPower.add(25)
            Sim.CombatLog.write(T & vbTab & "EmpowerRuneWeapon")
            Sim._UseGCD(T, 1)
            Return True
        End Function

    End Class

End Namespace