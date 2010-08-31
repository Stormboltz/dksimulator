'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:40 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class BloodPresence
	Inherits Spells.Spell
	
	Sub New(S As sim)
        MyBase.New(S)
        logLevel = LogLevelEnum.Detailled
	End Sub
	
	Function IsAvailable(T As Long) As Boolean
        If sim.Runes.AnyBlood(T) Then
            Return True
        Else
            Return False
        End If

	End Function
	
	
	Function Use(T As Long) As Boolean
        sim.BloodPresence = 1
        sim.UnholyPresence = 0
		sim.FrostPresence = 0
		
		sim.Runes.UseBlood(T,false)
		sim.combatlog.write(T  & vbtab &  "Switch to Blood Presence")
		Me.HitCount = Me.HitCount +1
		sim._UseGCD(T, 1)
		return true
	End Function
	
End Class
