'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class BloodTap
	Inherits Spells.Spell
	
	Sub New(s As Sim)
		MyBase.New(s)
	End Sub
	Function IsAvailable(T as long) As Boolean
		If T >= cd Then
			return true
		End If
	End Function
	
	
	
	Function Use(T As long) As Boolean
		cd = t + 6000
		'Two Blood Runes available or Two Blood runes unavailable, it will convert one of them to a Death Rune and make it available.
		If sim.runes.BloodRune1.death =False And sim.runes.BloodRune2.death =False  Then
			If sim.Runes.BloodRune1.Available(T) = sim.Runes.BloodRune2.Available(T) Then
				sim.Runes.BloodRune1.AvailableTime = T
				sim.Runes.BloodRune1.death = True
				sim.Runes.BloodRune1.BTuntil = T + 2000
				GoTo Out
			Else
				'One Blood Rune available and One Blood Rune unavailable, it will convert the available rune to a Death Rune and leave the other unavailable.
				If sim.Runes.BloodRune1.Available(T) Then
					sim.Runes.BloodRune1.AvailableTime = T
					sim.Runes.BloodRune1.death = True
					sim.Runes.BloodRune1.BTuntil = T + 2000
					GoTo Out
				Else
					sim.Runes.BloodRune2.AvailableTime = T
					sim.Runes.BloodRune2.death = True
					sim.Runes.BloodRune2.BTuntil = T + 2000
					GoTo Out
				End If
			End If
		End If
		
		'Two Death Runes and one or both Death Runes are unavailable, it will make one Death Rune available.
	
		If sim.runes.BloodRune1.death and sim.runes.BloodRune2.death  Then
			If sim.Runes.BloodRune1.Available(T) Then
				sim.Runes.BloodRune2.AvailableTime = T
				sim.Runes.BloodRune2.death = True
				sim.Runes.BloodRune2.BTuntil = T + 2000
				goto Out
			Else
				sim.Runes.BloodRune1.AvailableTime = T
				sim.Runes.BloodRune1.death = True
				sim.Runes.BloodRune1.BTuntil = T + 2000
				goto Out
			End If
		End If
			
		'One Blood Rune and one Death Rune 	and one or both are available, it will make the unavailable rune available and convert the Blood Rune to a Death Rune.
		'One Blood Rune and one Death rune and both are unavailable, it will make the Blood Rune available and convert it to a Death Rune.
		
		If sim.runes.BloodRune1.death <> sim.runes.BloodRune2.death Then
			If sim.runes.BloodRune1.death = True Then
				sim.runes.BloodRune2.death = True
				sim.Runes.BloodRune2.BTuntil = T + 2000
			Else
				sim.Runes.BloodRune1.death = True
				sim.Runes.BloodRune1.BTuntil = T + 2000
			End If
			If sim.Runes.BloodRune1.Available(T) Then
				sim.Runes.BloodRune2.AvailableTime = T
				goto Out
			Else
				sim.Runes.BloodRune1.AvailableTime = T
				goto Out
			End If
		End If
		
		
		Debug.Print("BT Warning case not managed")
				
		
		Out:
		sim.combatlog.write(T  & vbtab &  "Blood Tap")
		sim.RunicPower.add(10)
		Me.HitCount = Me.HitCount + 1
		sim.NextFreeGCD = T+1
		return true
	End Function
	
	Function UseWithCancelBT(T As long) As Boolean
		cd = t + 6000
		If sim.Runes.BloodRune1.AvailableTime > T And sim.runes.BloodRune1.death = False Then
			sim.Runes.BloodRune1.AvailableTime = T
			sim.Runes.BloodRune1.death = True
			'sim.Runes.BloodRune1.BTuntil = T + 2000
		Else
			sim.Runes.BloodRune2.AvailableTime = T
			sim.Runes.BloodRune2.death = True
			'sim.Runes.BloodRune2.BTuntil = T + 2000
		End If
		sim.combatlog.write(T  & vbtab &  "Blood Tap with Cancel Aura")
		sim.RunicPower.add(10)
		Me.HitCount = Me.HitCount + 1
		sim.NextFreeGCD = T+1
		return true
	End Function
	
		
End Class
