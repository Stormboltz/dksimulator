'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 11:37 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Targets
	Public Class Target
		Friend Debuff As Debuff
		Protected sim As Sim
		Friend BloodPlague As Diseases.BloodPlague
        Friend FrostFever As Diseases.FrostFever

		
		
		
		
		Sub New(s As Sim)
			sim = s
			Debuff = New Debuff(s)
			If sim.Targets Is Nothing Then
				sim.Targets = new Targets.TargetsManager(sim)
			End If
			sim.Targets.AllTargets.Add(Me)
			BloodPlague = new Diseases.BloodPlague(sim)
			FrostFever = New Diseases.FrostFever(sim)
		End Sub
		
		Sub Unbuff()
			Debuff.Unbuff
		End Sub
		Sub Kill()
			sim.Targets.AllTargets.Remove(Me)
			me.Finalize
		End Sub
		
		Function NumDesease() As Integer
		NumDesease = 0
			If BloodPlague.isActive(sim.TimeStamp) Then NumDesease = NumDesease + 1
			If FrostFever.isActive(sim.TimeStamp) Then NumDesease = NumDesease + 1
            If (sim.Character.Talents.Talent("EbonPlaguebringer").Value >= 1) And NumDesease >= 1 Then NumDesease = NumDesease + 1
		End Function
		
		
	End Class
End Namespace