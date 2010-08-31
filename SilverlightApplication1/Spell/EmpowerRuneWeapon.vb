'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/5/2009
' Heure: 1:38 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class EmpowerRuneWeapon
	Inherits Spells.Spell
	
	Sub New(S As sim)
        MyBase.New(S)
        logLevel = LogLevelEnum.Detailled
	End Sub
	
	Function Use(T As Long) As Boolean
		if CD > T then return false
        CD = T + (5 * 60 * 100)

        sim.Runes.BloodRune1.Activate()
        sim.Runes.BloodRune2.Activate()
        sim.Runes.FrostRune1.Activate()
        sim.Runes.FrostRune2.Activate()
        sim.Runes.UnholyRune1.Activate()
        sim.Runes.UnholyRune2.Activate()
		sim.RunicPower.add(25)
		sim.combatlog.write(T  & vbtab &  "EmpowerRuneWeapon"  )
		sim._UseGCD(T, 1)
		return true
	End Function
	
End Class

