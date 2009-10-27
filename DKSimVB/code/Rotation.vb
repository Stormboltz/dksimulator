'
' Created by SharpDevelop.
' User: Fabien
' Date: 16/03/2009
' Time: 22:08
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Rotation
	Friend XMLRo as new Xml.XmlDocument
	Friend MyRotation As New Collection
	Friend MyIntro as new Collection
	Private Runes As runes.runes
	Friend IntroStep as integer
	Private sim as Sim
	Sub New(S As Sim)
		sim = S
		Runes = sim.Runes
	End Sub
	Sub loadRotation()
		MyRotation.Clear
		
		sim.RotationStep = 0
		XMLRo.Load(sim.rotationPath)
		Dim Nod As Xml.XmlNode
		try
			For Each Nod In XMLRo.SelectSingleNode("//Rotation/Intro").ChildNodes
				try
					MyIntro.Add(Nod.Name,Nod.Name)
				Catch
					MyIntro.Add(Nod.Name)
				end try
			Next
		Catch
		End Try
			
		
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Rotation").ChildNodes
			try
				MyRotation.Add(Nod.Name,Nod.Name)
			Catch
				MyRotation.Add(Nod.Name)
			end try
		Next
		dim i as integer
		i = 0
		
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Runes").ChildNodes
			i=i+1
			select case i
				case 1
					if Nod.Name ="Death" then sim.Runes.BloodRune1.death=true
				case 2
					if Nod.Name ="Death" then sim.Runes.BloodRune2.death=true
				case 3
					if Nod.Name ="Death" then sim.Runes.FrostRune1.death=true
				case 4
					if Nod.Name ="Death" then sim.Runes.FrostRune2.death=true
				case 5
					if Nod.Name ="Death" then sim.Runes.UnholyRune1.death=true
				case 6
					if Nod.Name ="Death" then sim.Runes.UnholyRune2.death=true
			end select
		Next
		
		
		sim.Rotate=true
	End Sub
	
	Sub DoRoration(TimeStamp As long)
		Dim ret As Boolean
		
		If MyIntro.Count > 0 and IntroStep < MyIntro.Count    Then
			ret = DoRoration(TimeStamp,MyIntro.Item(IntroStep+1),XMLRo.SelectSingleNode("//Rotation/Intro/" & MyIntro.Item(IntroStep+1) ).Attributes.GetNamedItem("retry").Value )
			If ret = True Then IntroStep = IntroStep + 1
			exit sub
		End If
		
		
		ret = DoRoration(TimeStamp,MyRotation.Item(sim.RotationStep+1),XMLRo.SelectSingleNode("//Rotation/Rotation/" & MyRotation.Item(sim.RotationStep+1) ).Attributes.GetNamedItem("retry").Value )
		If ret = True Then sim.RotationStep = sim.RotationStep + 1
		if MyRotation.Count <= sim.RotationStep then sim.RotationStep=0
	End Sub
	function DoRoration(TimeStamp As Double,Ability as string,retry as integer ) as Boolean
		Select Case Ability
			Case "BloodTap"
				If sim.BloodTap.IsAvailable(Timestamp) Then
					return sim.BloodTap.Use(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "BoneShield"
				if sim.BoneShield.IsAvailable(Timestamp) Then
					return sim.BoneShield.Use(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "GhoulFrenzy"
				if sim.Frenzy.IsFrenzyAvailable(Timestamp) Then
					return sim.Frenzy.Frenzy(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "ScourgeStrike"
				If runes.FU(TimeStamp) = True Then
					return sim.ScourgeStrike.ApplyDamage(TimeStamp)
					'debug.Print("SS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "PlagueStrike"
				If runes.Unholy(TimeStamp) Then
					return sim.PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "Obliterate"
				If runes.FU(TimeStamp) = True Then
					return sim.Obliterate.ApplyDamage(TimeStamp)
					'debug.Print("OB")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "FrostStrike"
				If sim.FrostStrike.isAvailable(TimeStamp) = True Then
					
					return sim.FrostStrike.ApplyDamage(TimeStamp)
					'debug.Print("FS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathStrike"
				If runes.FU(TimeStamp) Then
					
					return sim.DeathStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodStrike"
				If runes.AnyBlood(TimeStamp) Then
					If sim.BoneShieldUsageStyle = 1 Then
						If sim.BoneShield.IsAvailable(TimeStamp) Then
							sim.BoneShield.Use(TimeStamp)
							Return True
						End If
						If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
							sim.UnbreakableArmor.Use(TimeStamp)
							return true
						End If
					End If
					return sim.BloodStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "HeartStrike"
				If runes.AnyBlood(TimeStamp) = True Then
					
					return sim.Heartstrike.ApplyDamage(TimeStamp)
					'debug.Print("HS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodPlague"
				
				If sim.BloodPlague.isActive(TimeStamp + 150) = False And runes.Unholy(TimeStamp) = True Then
					Return sim.PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "IcyTouch"
				If runes.Frost(TimeStamp) = True Then
					
					Return sim.IcyTouch.ApplyDamage(TimeStamp)
					'debug.Print("IT")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathCoil"
				If sim.deathcoil.isAvailable(TimeStamp) = True Then
					
					Return sim.deathcoil.ApplyDamage(TimeStamp,False)
					'debug.Print("DC")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodBoil"
				If runes.AnyBlood(TimeStamp) = True Then
					If sim.BoneShieldUsageStyle = 3 Then
						If sim.BoneShield.IsAvailable(TimeStamp) Then
							sim.BoneShield.Use(TimeStamp)
							return true
						End If
						If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
							sim.UnbreakableArmor.Use(TimeStamp)
							return true
						End If
					End If
					
					Return sim.BloodBoil.ApplyDamage(TimeStamp)
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "HowlingBlast"
				If sim.HowlingBlast.isAvailable(TimeStamp) Then
					If sim.proc.rime Or runes.FU(TimeStamp) Then
						runes.UnReserveFU(TimeStamp)
						Return sim.HowlingBlast.ApplyDamage(TimeStamp)
						Exit function
					Else
						runes.ReserveFU(TimeStamp)
					End If
				Else
					if retry = 0 then return true
				End If
			Case "DeathandDecay"
				If sim.DeathAndDecay.isAvailable(TimeStamp) Then
					Return sim.DeathAndDecay.Apply(TimeStamp)
				End If
			Case "Pestilence"
				If runes.AnyBlood(TimeStamp) Then
					Return sim.Pestilence.use(TimeStamp)
				Else
					if retry = 0 then return true
				End If
			Case "Horn"
				If sim.Horn.isAvailable(TimeStamp) Then
					Return sim.Horn.use(TimeStamp)
				Else
					if retry = 0 then return true
				End If
		End Select
	End function
	Sub intro()
		
		
	End Sub
End Class
 