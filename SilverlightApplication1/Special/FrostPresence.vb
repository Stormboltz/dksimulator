'
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
        MyBase.New(S)
        logLevel = LogLevelEnum.Detailled
	End Sub
	
	Function IsAvailable(T As Long) As Boolean
        If sim.Runes.Frost(T) Then
            Return True
        Else
            Return False
        End If
	End Function
	
	
	Function Use(T As Long) As Boolean
		sim.BloodPresence = 0
		sim.UnholyPresence = 0
        sim.FrostPresence = 10 + (2.5 * sim.Character.Talents.Talent("IFrostPresence").Value)
		sim.Runes.UseFrost(T,false)
		sim.combatlog.write(T  & vbtab &  "Switch to Frost Presence")
		Me.HitCount = Me.HitCount +1
		sim._UseGCD(T, 1)

		return true
	End Function

    Sub SetForFree()
        sim.BloodPresence = 0
        sim.UnholyPresence = 0
        sim.FrostPresence = 10 + (2.5 * sim.Character.Talents.Talent("IFrostPresence").Value)
    End Sub


End Class
