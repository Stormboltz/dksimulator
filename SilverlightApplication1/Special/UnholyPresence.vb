'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:40 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class UnholyPresence
	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	Function IsAvailable(T As Long) As Boolean
        If sim.Runes.Unholy() Then
            Return True
        Else
            Return False
        End If
	End Function
	
	
	Function Use(T As Long) As Boolean
		sim.BloodPresence = 0
		sim.UnholyPresence = 1
		sim.FrostPresence = 0
		sim.Runes.UseUnholy(T,false)
		sim.combatlog.write(T  & vbtab &  "Switch to Unholy Presence")
		Me.HitCount = Me.HitCount +1
		sim._UseGCD(T, 1)
		return true
	End Function
	
End Class
