'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 17:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class CataRune
	Friend Value As Integer
	Friend reserved As Boolean
	Friend death As Boolean
	
	
	Friend BTuntil as Long
	Protected Sim As Sim
	
	
	Sub New(S As Sim)
		Me.reserved=False
		Me.death=False
		Me.value=200
		Sim = S
	End Sub
	
	Sub Use(T As Long, D As Boolean)
		death = D
		If BTuntil > T Then death = True
		Value = Value - 100
		sim.proc.tryT104PDPS(T)
		sim.FutureEventManager.Add(AvailableTime,"Rune")
	End Sub
	Function RefillRate As Double 'Rune fraction per second
		'withoutHaste 5s per rune 
		Dim tmp As Double
		tmp = (1/5)*sim.MainStat.Haste
		return tmp
	End Function
	
	
	Function Available(T As Long) As Boolean
		If value >= 100 Then
			Return True
		Else
			return false
		End If
	End Function
	Function AvailableTime As Long
		Dim tmp As Long
		return ((100-value)*RefillRate + T)
	End Function
	
End Class
