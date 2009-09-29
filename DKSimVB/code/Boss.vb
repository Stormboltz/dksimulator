﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 29/09/2009
' Heure: 09:57
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Boss
	Friend NextHit  As Long
	Private Avoidance as Double
	Private Sim As Sim
	Private Speed As Integer
	
	
	
	Sub New(S as Sim)
		NextHit = 0
		Sim = S
		LoadTankOptions
		
	End Sub
	
	Function ApplyDamage(T as Long) As Boolean
		Dim RNGHit As Double
		RNGHit = sim.RandomNumberGenerator.RNGTank
		NextHit = T+Speed
		If RNGHit > Avoidance Then
			'Boss hit
			sim.BoneShield.UseCharge(T)
		Else
			'Boss miss
			sim.RuneStrike.trigger = true
		End If
		sim.proc.TryScentOfBlood(T)
	End Function
	
	Sub LoadTankOptions()
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		try
			doc.Load("TankConfig.xml")
			Avoidance = doc.SelectSingleNode("//config/Stats/txtFBAvoidance").InnerText / 100
			Speed = doc.SelectSingleNode("//config/Stats/txtFPBossSwing").InnerText * 100
		Catch
			MsgBox ("Error retriving Tank parameters")
		End Try
		
	End Sub
	
End Class