'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 03/08/2009
' Heure: 14:32
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Bloodlust
	
	Friend Cd as Long
	Private ActiveUntil As Long
	Protected Sim as Sim
	Sub New(S As Sim)
		Sim = S
		cd = 0
        ActiveUntil = 0

	End Sub
	
	Function IsAvailable(T As Long) As Boolean
		if  sim.Character.Buff.Bloodlust = 0 then return false
        If Cd <= T Then
            Return True
        Else
            Return False
        End If
	End Function

	Function IsActive(T as Long) as Boolean
        If ActiveUntil > T Then
            Return True
        Else
            Return False
        End If

	End Function
	
	Sub use(T As Long)
		CD = T + 10*60*100
		ActiveUntil = T + 4000
	End Sub
End Class
