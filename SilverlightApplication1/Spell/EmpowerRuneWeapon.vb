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

        Overrides Sub Use()
            If CD > sim.TimeStamp Then Return
            CD = sim.TimeStamp + (5 * 60 * 100)

            sim.Runes.BloodRune1.Activate()
            sim.Runes.BloodRune2.Activate()
            sim.Runes.FrostRune1.Activate()
            sim.Runes.FrostRune2.Activate()
            sim.Runes.UnholyRune1.Activate()
            sim.Runes.UnholyRune2.Activate()
            sim.RunicPower.add(25)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "EmpowerRuneWeapon")
            sim._UseGCD(sim.TimeStamp, 1)

        End Sub

    End Class

End Namespace