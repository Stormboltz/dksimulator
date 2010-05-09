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
		MyBase.New(s)
	End Sub
	
	Function Use(T As Long) As Boolean
		if CD > T then return false
		CD = T + (5*60 * 100)
		if sim.Runes.BloodRune1.AvailableTime > T Then sim.Runes.BloodRune1.SetAvailableTime(T)
		if sim.Runes.BloodRune2.AvailableTime > T Then sim.Runes.BloodRune2.SetAvailableTime(T)
		if sim.Runes.FrostRune1.AvailableTime > T Then sim.Runes.FrostRune1.SetAvailableTime(T)
		if sim.Runes.FrostRune2.AvailableTime > T Then sim.Runes.FrostRune2.SetAvailableTime(T)
		if sim.Runes.UnholyRune1.AvailableTime > T Then sim.Runes.UnholyRune1.SetAvailableTime(T)
		if sim.Runes.UnholyRune2.AvailableTime > T Then sim.Runes.UnholyRune2.SetAvailableTime(T)
		sim.RunicPower.add(25)
		sim.combatlog.write(T  & vbtab &  "EmpowerRuneWeapon"  )
		sim._UseGCD(T, 1)
		return true
	End Function
	
End Class

