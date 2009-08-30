'
' Created by SharpDevelop.
' User: Fabien
' Date: 16/03/2009
' Time: 22:08
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Rotation
	Friend XMLRo as new Xml.XmlDocument
	Friend MyRotation As New Collection
	
	Sub loadRotation()
		MyRotation.Clear
		RotationStep = 0
		XMLRo.Load(rotationPath)
		dim Nod as Xml.XmlNode
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Rotation").ChildNodes
			MyRotation.Add(Nod.Name)
		Next
		dim i as integer
		i = 0
		
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Runes").ChildNodes
			i=i+1
			select case i
				case 1
					if Nod.Name ="Death" then rune1.death=true
				case 2
					if Nod.Name ="Death" then rune2.death=true
				case 3
					if Nod.Name ="Death" then rune3.death=true
				case 4
					if Nod.Name ="Death" then rune4.death=true
				case 5
					if Nod.Name ="Death" then rune5.death=true
				case 6
					if Nod.Name ="Death" then rune6.death=true
			end select
		Next
		
		
		Rotate=true
	End Sub
	
	Sub DoRoration(TimeStamp As long)
		
		dim ret as Boolean
		ret = DoRoration(TimeStamp,MyRotation.Item(RotationStep+1),XMLRo.SelectSingleNode("//Rotation/Rotation/" & MyRotation.Item(RotationStep+1) ).Attributes.GetNamedItem("retry").Value )
		If ret = True Then RotationStep = RotationStep + 1
		if MyRotation.Count <= RotationStep then RotationStep=0
	End Sub
	function DoRoration(TimeStamp As Double,Ability as string,retry as integer ) as Boolean
		Select Case Ability
			Case "BloodTap"
				If BloodTap.IsAvailable(Timestamp) Then
					return BloodTap.Use(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "GhoulFrenzy"
				if ghoul.IsFrenzyAvailable(Timestamp) Then
					return ghoul.Frenzy(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "ScourgeStrike"
				If runes.FU(TimeStamp) = True Then
					return ScourgeStrike.ApplyDamage(TimeStamp)
					'debug.Print("SS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "PlagueStrike"
				If runes.Unholy(TimeStamp) Then
					return PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "Obliterate"
				If runes.FU(TimeStamp) = True Then
					
					return Obliterate.ApplyDamage(TimeStamp)
					'debug.Print("OB")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "FrostStrike"
				If FrostStrike.isAvailable(TimeStamp) = True Then
					
					return FrostStrike.ApplyDamage(TimeStamp)
					'debug.Print("FS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathStrike"
				If runes.FU(TimeStamp) Then
					
					return DeathStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodStrike"
				If runes.AnyBlood(TimeStamp) Then
					return BloodStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "HeartStrike"
				If runes.AnyBlood(TimeStamp) = True Then
					
					return Heartstrike.ApplyDamage(TimeStamp)
					'debug.Print("HS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodPlague"
				
				If BloodPlague.isActive(TimeStamp + 150) = False And runes.Unholy(TimeStamp) = True Then
					Return PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "IcyTouch"
				If runes.Frost(TimeStamp) = True Then
					
					Return IcyTouch.ApplyDamage(TimeStamp)
					'debug.Print("IT")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathCoil"
				If deathcoil.isAvailable(TimeStamp) = True Then
					
					Return deathcoil.ApplyDamage(TimeStamp,False)
					'debug.Print("DC")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodBoil"
				If runes.AnyBlood(TimeStamp) = True Then
					
					Return BloodBoil.ApplyDamage(TimeStamp)
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "Pestilance"
			Case "HowlingBlast"
				If HowlingBlast.isAvailable(TimeStamp) Then
					If proc.rime Or runes.FU(TimeStamp) Then
						runes.UnReserveFU(TimeStamp)
						Return HowlingBlast.ApplyDamage(TimeStamp)
						Exit function
					Else
						runes.ReserveFU(TimeStamp)
					End If
				Else
					if retry = 0 then return true
				End If
			Case "DeathandDecay"
				If DeathAndDecay.isAvailable(TimeStamp) Then
					Return DeathAndDecay.Apply(TimeStamp)
				End If
			Case "Pestilence"
				If runes.Blood(TimeStamp) Then
					Return Pestilence.use(TimeStamp)
				Else
					if retry = 0 then return true
				End If
		End Select
	End function
	
End Module
