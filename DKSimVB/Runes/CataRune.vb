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
		if Value < 0 then debug.Print ("Negative Rune")
		sim.proc.tryT104PDPS(T)
		sim.FutureEventManager.Add(AvailableTime,"Rune")
	End Sub
	Function RefillRate As Double 'Rune fraction per second
		'withoutHaste 5s per rune 
		Dim tmp As Double
		tmp = (1/5)*sim.MainStat.Haste
		If sim.UnholyPresence = 1 Then
			tmp = tmp * (1+0.05*sim.TalentUnholy.ImprovedUnholyPresence/100)
		End If
		return tmp
	End Function
	
	Sub Refill(second As Double)
		dim tmp as Double
		tmp = RefillRate * second*100
		value = math.min(200,value+tmp)
	End Sub
	
	
	
	Function Available() As Boolean
		If value >= 100 Then
			Return True
		Else
			return false
		End If
	End Function
	
	Function AvailableTwice() As Boolean
		If value >= 100 Then
			Return True
		Else
			return false
		End If
	End Function
	
	
	Function NextAvailableTime() As Long
		'Dim tmp As Long
		if value >= 200 then return sim.TimeStamp

		return ((200-value)*RefillRate + sim.TimeStamp)
	End Function
	
	Function AvailableTime() As Long
		'Dim tmp As Long
		if value >= 100 then return sim.TimeStamp

		return ((100-value)*RefillRate + sim.TimeStamp)
	End Function
	
End Class
