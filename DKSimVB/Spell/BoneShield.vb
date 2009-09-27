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
	
	Sub New(MySim as Sim)
		Init
		sim = MySim
	End Sub
	
	
	Public Overloads Overrides Sub Init()
		MyBase.Init()
		if TalentUnholy.BoneShield = 1 then
			Me.CD = 60*100
			Me.ActiveUntil = 60*100
		end if
	End Sub
	
	
	Function Use(T as Long) As Boolean
		If TalentUnholy.BoneShield =0 Then Return False
		If sim.runes.Unholy(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		me.CD = T + 60*100
		Me.ActiveUntil = T + 60*100
		sim.runes.UseUnholy(T,False)
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		sim.RunicPower.add(10)
		sim.combatlog.write(T  & vbtab &  "Bone Shield")
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
