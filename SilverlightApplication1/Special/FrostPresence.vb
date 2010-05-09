﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:46 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class FrostPresence

	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	Function IsAvailable(T As Long) As Boolean
		if sim.Runes.Frost(T) then return true
	End Function
	
	
	Function Use(T As Long) As Boolean
		sim.BloodPresence = 0
		sim.UnholyPresence = 0
		sim.FrostPresence = 1
		sim.Runes.UseFrost(T,false)
		sim.combatlog.write(T  & vbtab &  "Switch to Frost Presence")
		Me.HitCount = Me.HitCount +1
		sim._UseGCD(T, 1)

		return true
	End Function
	

End Class
