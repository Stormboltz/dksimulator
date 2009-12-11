'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 21:55
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class BoneShield
	Inherits Spells.Spell
	Friend Charge as Integer
	
	Function BuffLength() as Integer
		Return 300
	End Function
	
	
	
	Sub New(MySim as Sim)
		sim = MySim
		PreBuff
	End Sub
	
	Sub UseCharge(T as Long)
		Charge = Charge -1
		If Charge = 0 Then
			Me.ActiveUntil = T
			Charge = 0
		End If
	End Sub
	
	Sub PreBuff
		if TalentUnholy.BoneShield = 1 then
			Me.CD = 60*100
			Me.ActiveUntil = sim.TimeStamp + BuffLength*100
		end if
	End Sub
	Public Overloads Overrides Sub Init()
		MyBase.Init()
		
	End Sub
	
	
	Function Use(T as Long) As Boolean
		If TalentUnholy.BoneShield = 0 Then Return False
		If sim.runes.Unholy(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		me.CD = T + 60*100
		Me.ActiveUntil = T + BuffLength*100
		sim.runes.UseUnholy(T,False)
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		sim.RunicPower.add(10)
		sim.combatlog.write(T  & vbtab &  "Bone Shield")
		Charge = 4
		If sim.Glyph.BoneShield Then
			Charge = Charge + 1
		End If
		me.HitCount = me.HitCount +1
	End Function
	
	Function IsAvailable(T As Long) As Boolean
		If TalentUnholy.BoneShield =0 Then Return False
		If ActiveUntil > T Then Return False
		if sim.BloodTap.IsAvailable(T)=false Then Return false
		If CD > T Then Return False
		return true
	End Function
	
	Function Value(T As Long) As integer
		If ActiveUntil > T Then
			Return 1
		Else
			Return 0
		End If
	End Function
	
End Class
