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
    Private death1 As Boolean
    Private death2 As Boolean
    Property death As Boolean
        Set(ByVal value As Boolean)
            If value = True Then
                If death1 = True Then
                    death2 = True
                Else
                    death1 = True
                End If
            Else
                If death2 = True Then
                    death2 = False
                ElseIf death1 = True Then
                    death1 = False
                End If
            End If

        End Set
        Get
            Return (death1 Or death2)
        End Get
    End Property
	
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
        If Value < 0 Then Diagnostics.Debug.WriteLine("Negative Rune")
		sim.proc.tryT104PDPS(T)
		sim.FutureEventManager.Add(AvailableTime,"Rune")
	End Sub
	Function RefillRate As Double 'Rune fraction per second
		'withoutHaste 5s per rune 
		Dim tmp As Double
		tmp = (1/5)*sim.MainStat.Haste
        If Sim.UnholyPresence = 1 Then
            tmp = tmp * (1.1 + Sim.Character.TalentUnholy.ImprovedUnholyPresence * 2.5 / 100)
        End If
        Return tmp
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
