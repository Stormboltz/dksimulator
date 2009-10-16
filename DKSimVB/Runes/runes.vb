Namespace Runes
Friend Class runes
	friend BloodRune1 As Rune
	friend BloodRune2 As Rune
	friend FrostRune1 As Rune
	friend FrostRune2 As Rune
	friend UnholyRune1 As Rune
	friend UnholyRune2 As Rune
	Protected sim As Sim
	
	Sub New(S As Sim)
		Sim = S
		BloodRune1 = new rune(s)
		BloodRune2 = new rune(s)
		FrostRune1 = new rune(s)
		FrostRune2 = new rune(s)
		UnholyRune1 = new rune(s)
		UnholyRune2 = new rune(s)
		
		UnReserveFU(0)
		BloodRune1.AvailableTime = 0
		BloodRune1.death = False
		BloodRune2.AvailableTime = 0
		BloodRune2.death = False
		FrostRune1.AvailableTime = 0
		FrostRune1.death = False
		FrostRune2.AvailableTime = 0
		FrostRune2.death = False
		UnholyRune1.AvailableTime = 0
		UnholyRune1.death = False
		UnholyRune2.AvailableTime = 0
		UnholyRune2.death = False
	End Sub
	
	Function RuneState() As String
		Dim T As Long
		T = sim.TimeStamp
		Dim tmp As String
		tmp = "["
		
		If BloodRune1.AvailableTime <= T Then
			If BloodRune1.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - BloodRune1.AvailableTime)/100)
		End If
		If BloodRune2.AvailableTime <= T  Then
			If BloodRune2.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - BloodRune2.AvailableTime)/100)
		End If
		If FrostRune1.AvailableTime <= T Then
			If FrostRune1.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - FrostRune1.AvailableTime)/100)
			'debug.Print ("Rune3.AvailableTime:" & (Rune3.AvailableTime)/100)
		End If
		If FrostRune2.AvailableTime <= T Then
			If FrostRune2.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - FrostRune2.AvailableTime)/100)
			'debug.Print ("Rune4.AvailableTime:" & (Rune4.AvailableTime)/100)
		End If
		If UnholyRune1.AvailableTime <= T Then
			If UnholyRune1.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - UnholyRune1.AvailableTime)/100)
		End If
		If UnholyRune2.AvailableTime <= T Then
			If UnholyRune2.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - UnholyRune2.AvailableTime)/100)
		End If
		
		tmp = tmp & "]"
		return tmp
	End Function
	
	
	
	
	
	
	
	Function RuneRefreshtime As Integer
		If sim.mainstat.UnholyPresence Then
			return 1000 - 50*talentunholy.ImprovedUnholyPresence
		Else
			return 1000
		End If
	End Function
	
	Function BFU (T As Long) As Boolean
		If BloodRune1.AvailableTime <= T Or BloodRune2.AvailableTime <= T Then
			If FrostRune1.AvailableTime <= T or FrostRune2.AvailableTime <= T Then
				If UnholyRune1.AvailableTime <= T Or UnholyRune2.AvailableTime <= T Then
					return  True
				End If
			End If
		End If
	End Function
	
	
	Function GetNextUnholy(T As Long) As Long
		Dim bArray As new ArrayList
		if BloodRune1.AvailableTime > T 	And BloodRune1.death = true then bArray.Add(BloodRune1.AvailableTime)
		If BloodRune2.AvailableTime > T 	And BloodRune2.death = true Then bArray.Add(BloodRune2.AvailableTime)
		If FrostRune1.AvailableTime > T And FrostRune1.death = True	Then bArray.Add(FrostRune1.AvailableTime)
		If FrostRune2.AvailableTime > T And FrostRune2.death = True	Then bArray.Add(FrostRune2.AvailableTime)
		If UnholyRune1.AvailableTime > T Then bArray.Add(UnholyRune1.AvailableTime)
		If UnholyRune2.AvailableTime > T Then bArray.Add(UnholyRune2.AvailableTime)
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
	End Function
	
	
	
	Function GetNextFrost(T As Long) As Long
		Dim bArray As new ArrayList
		if BloodRune1.AvailableTime > T 	And BloodRune1.death = true then bArray.Add(BloodRune1.AvailableTime)
		If BloodRune2.AvailableTime > T 	And BloodRune2.death = true Then bArray.Add(BloodRune2.AvailableTime)
		If FrostRune1.AvailableTime > T Then bArray.Add(FrostRune1.AvailableTime)
		If FrostRune2.AvailableTime > T Then bArray.Add(FrostRune2.AvailableTime)
		If UnholyRune1.AvailableTime > T And UnholyRune1.death = True	Then bArray.Add(UnholyRune1.AvailableTime)
		If UnholyRune2.AvailableTime > T And UnholyRune2.death = True	Then bArray.Add(UnholyRune2.AvailableTime)
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
	End Function
	
	
	
	
	Function GetNextBloodCD(T As Long) As Long
		Dim bArray As new ArrayList
		
'		if Rune1.AvailableTime > T 	And Rune1.death = false then bArray.Add(Rune1.AvailableTime)
'		If Rune2.AvailableTime > T 	And Rune2.death = False Then bArray.Add(Rune2.AvailableTime)
		if BloodRune1.AvailableTime > T 	then bArray.Add(BloodRune1.AvailableTime)
		If BloodRune2.AvailableTime > T 	Then bArray.Add(BloodRune2.AvailableTime)
		
		If FrostRune1.AvailableTime > T And FrostRune1.death = True	Then bArray.Add(FrostRune1.AvailableTime)
		If FrostRune2.AvailableTime > T And FrostRune2.death = True	Then bArray.Add(FrostRune2.AvailableTime)
		If UnholyRune1.AvailableTime > T And UnholyRune1.death = True	Then bArray.Add(UnholyRune1.AvailableTime)
		If UnholyRune2.AvailableTime > T And UnholyRune2.death = True	Then bArray.Add(UnholyRune2.AvailableTime)
		
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
		
	End Function
	
	
	
	Function AnyBlood(T as long) As Boolean
		If BloodRune1.AvailableTime <= T And BloodRune1.reserved=false Then return  True
		If BloodRune2.AvailableTime <= T And BloodRune2.reserved=false Then return  True
		If FrostRune1.AvailableTime <= T And FrostRune1.death = True and FrostRune1.reserved=false Then return  True
		If FrostRune2.AvailableTime <= T And FrostRune2.death = True and FrostRune2.reserved=false Then return  True
		If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True and UnholyRune1.reserved=false Then return  True
		If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True and UnholyRune2.reserved=false Then return  True
	End Function
	
	
	Function Blood(T as long) As Boolean
		If BloodRune1.AvailableTime <= T And BloodRune1.death = False and BloodRune1.reserved=false Then return  True
		If BloodRune2.AvailableTime <= T And BloodRune2.death = False and BloodRune2.reserved=false Then return  True
		If FrostRune1.AvailableTime <= T And FrostRune1.death = True and FrostRune1.reserved=false Then return  True
		If FrostRune2.AvailableTime <= T And FrostRune2.death = True and FrostRune2.reserved=false Then return  True
		If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True and UnholyRune1.reserved=false Then return  True
		If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True and UnholyRune2.reserved=false Then return  True
	End Function
	
	Function BloodOnly(T as long) As Boolean
		If BloodRune1.AvailableTime <= T Then return  True
		If BloodRune2.AvailableTime <= T Then return  True
	End Function
	
	
	Function FrostOnly(T As Long) As Boolean
		If FrostRune1.AvailableTime <= T and FrostRune1.reserved=false Then return True
		If FrostRune2.AvailableTime <= T and FrostRune2.reserved=false Then return True
	End Function
	
	Function UnholyOnly(T as long) As Boolean
		If UnholyRune1.AvailableTime <= T and UnholyRune1.reserved=false Then return True
		If UnholyRune2.AvailableTime <= T and UnholyRune2.reserved=false Then return True
	End Function
	
	
	Function Frost(T as long) As Boolean
		If BloodRune1.AvailableTime <= T And BloodRune1.death = True and BloodRune1.reserved=false Then return  True
		If BloodRune2.AvailableTime <= T And BloodRune2.death = True and BloodRune2.reserved=false Then return True
		If FrostRune1.AvailableTime <= T and FrostRune1.reserved=false Then return True
		If FrostRune2.AvailableTime <= T and FrostRune2.reserved=false Then return True
		If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True and UnholyRune1.reserved=false Then return True
		If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True and UnholyRune2.reserved=false Then return True
	End Function
	
	Function Unholy(T as long) As Boolean
		If BloodRune1.AvailableTime <= T And BloodRune1.death = True and BloodRune1.reserved=false Then return  True
		If BloodRune2.AvailableTime <= T And BloodRune2.death = True and BloodRune2.reserved=false Then return True
		If FrostRune1.AvailableTime <= T And FrostRune1.death = True and FrostRune1.reserved=false Then return True
		If FrostRune2.AvailableTime <= T And FrostRune2.death = True and FrostRune2.reserved=false Then return True
		If UnholyRune1.AvailableTime <= T and UnholyRune1.reserved=false Then return True
		If UnholyRune2.AvailableTime <= T and UnholyRune2.reserved=false Then return True
	End Function
	
	Function FU(T As long) As Boolean
		Dim UH As Boolean
		Dim Rune1reserved As Boolean
		Dim Rune2reserved As Boolean
		If FrostRune1.AvailableTime <= T Then
			UH = True
		Else
			If FrostRune2.AvailableTime <= T Then
				UH = True
			Else
				If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
					Rune1reserved = True
					UH = True
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
						UH = True
						Rune2reserved = True
					Else
						UH = False
					End If
				End If
			End If
		End If
		
		If UH = False Then
			Rune1reserved = False
			Rune2reserved = False
			return False
			Exit Function
		End If
		
		Dim FR As Boolean
		
		If UnholyRune1.AvailableTime <= T Then
			FR = True
		Else
			If UnholyRune2.AvailableTime <= T Then
				FR = True
			Else
				If BloodRune1.AvailableTime <= T And BloodRune1.death = True And Rune1reserved = False Then
					FR = True
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True And Rune2reserved = False Then
						FR = True
					Else
						FR = False
					End If
					
				End If
				
			End If
		End If
		Rune1reserved = False
		Rune2reserved = False
		return FR And UH
		
		
	End Function
	Function UseBlood(T as long,Death as Boolean) As Boolean
		If BloodRune1.Available(T) And BloodRune1.death = False Then
			BloodRune1.Use(T,Death)
		Else
			If BloodRune2.Available(T) And BloodRune2.death = False Then
				BloodRune2.Use(T,Death)
			Else
				If BloodRune1.Available(T) Then
					BloodRune1.Use(T,Death)
				Else
					If BloodRune2.Available(T) Then
						BloodRune2.Use(T,Death)
					Else
						If FrostRune1.Available(T) And FrostRune1.death = True Then
							FrostRune1.Use(T,Death)
						Else
							If FrostRune2.Available(T) And FrostRune2.death = True Then
								FrostRune2.Use(T,Death)
							Else
								If UnholyRune1.Available(T) And UnholyRune1.death = True Then
									UnholyRune1.Use(T,Death)
								Else
									If UnholyRune2.Available(T) And UnholyRune2.death = True Then
										UnholyRune2.Use(T,Death)
									Else
										debug.Print ("ERROR BLood RUNE")
									End If
								End If
							End If
						End If
					End If
				End If
			End If
		End If
		
		
		
	End Function
	Function UseFrost(T as long,Death as Boolean) As Boolean
		If FrostRune1.Available(T) Then
			FrostRune1.Use(T,Death)
		Else
			If FrostRune2.Available(T) Then
				FrostRune2.Use(T,Death)
			Else
				If BloodRune1.Available(T) And BloodRune1.death = True Then
					BloodRune1.Use(T,Death)
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
						BloodRune2.Use(T,Death)
					Else
						If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True Then
							UnholyRune1.Use(T,Death)
						Else
							If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True Then
								UnholyRune2.Use(T,Death)
							Else
								debug.Print ("ERROR FROST RUNE")
							End If
						End If
					End If
				End If
			End If
		End If
	End Function
	
	Function UseUnholy(T as long,Death as Boolean) As Boolean
		If UnholyRune1.AvailableTime <= T Then
			UnholyRune1.Use(T,Death)
		Else
			If UnholyRune2.AvailableTime <= T Then
				UnholyRune2.Use(T,Death)
			Else
				If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
					BloodRune1.Use(T,Death)
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
						BloodRune2.Use(T,Death)
					Else
						If FrostRune1.AvailableTime <= T And FrostRune1.death = True Then
							FrostRune1.Use(T,Death)
						Else
							If FrostRune2.AvailableTime <= T And FrostRune2.death = True Then
								FrostRune2.Use(T,Death)
							Else
								debug.Print("Unholy rune ERROR")
							End If
						End If
					End If
				End If
			End If
		End If
	End Function
	Function UseFU(T as long,Death as Boolean) As Boolean
		If FrostRune1.AvailableTime <= T Then
			FrostRune1.Use(T,Death)
		Else
			If FrostRune2.AvailableTime <= T Then
				FrostRune2.Use(T,Death)
			Else
				If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
					BloodRune1.Use(T,Death)
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
						BloodRune2.Use(T,Death)
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
				End If
			End If
		End If
		
		If UnholyRune1.AvailableTime <= T Then
			UnholyRune1.Use(T,Death)
		Else
			If UnholyRune2.AvailableTime <= T Then
				UnholyRune2.Use(T,Death)
			Else
				If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
					BloodRune1.Use(T,Death)
				Else
					If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
						BloodRune2.Use(T,Death)
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
				End If
			End If
		End If
	End Function
'	Function UseFU(T as long,Death as Boolean,UseReservation as Boolean) As Boolean
'		If Rune3.AvailableTime <= T Then
'			If T - Rune3.AvailableTime <= 300 and Rune3.AvailableTime <> 0 Then
'				Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
'			Else
'				Rune3.AvailableTime = T + RuneRefreshtime
'			End If
'			rune3.death=False
'			If death Then Rune3.death=true
'		Else
'			If Rune4.AvailableTime <= T Then
'				If T - Rune4.AvailableTime <= 300 and Rune4.AvailableTime <> 0 Then
'					Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
'				Else
'					Rune4.AvailableTime = T + RuneRefreshtime
'				End If
'				Rune4.death=false
'				If death Then Rune4.death=true
'			Else
'				If Rune1.AvailableTime <= T And Rune1.death = True Then
'					If T - Rune1.AvailableTime <= 300 and Rune1.AvailableTime <> 0 Then
'						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
'					Else
'						Rune1.AvailableTime = T + RuneRefreshtime
'					End If
'					Rune1.death = False
'					If death Then Rune1.death=true
'				Else
'					If Rune2.AvailableTime <= T And Rune2.death = True Then
'						If T - Rune2.AvailableTime <= 300 and Rune2.AvailableTime <> 0  Then
'							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
'						Else
'							Rune2.AvailableTime = T + RuneRefreshtime
'						End If
'						Rune2.death = False
'						If death Then Rune2.death=true
'					Else
'						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
'						Exit Function
'					End If
'					
'				End If
'			End If
'		End If
'		
'		If Rune5.AvailableTime <= T Then
'			If T - Rune5.AvailableTime <= 300 and Rune5.AvailableTime <> 0 Then
'				Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
'			Else
'				Rune5.AvailableTime = T + RuneRefreshtime
'			End If
'			If death Then Rune5.death=true
'		Else
'			If Rune6.AvailableTime <= T Then
'				If T - Rune6.AvailableTime <= 300 and Rune6.AvailableTime <> 0 Then
'					Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
'				Else
'					Rune6.AvailableTime = T + RuneRefreshtime
'				End If
'				Rune6.death=false
'				If death Then Rune6.death=true
'			Else
'				If Rune1.AvailableTime <= T And Rune1.death = True Then
'					If T - Rune1.AvailableTime <= 300 and Rune1.AvailableTime <> 0  Then
'						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
'					Else
'						Rune1.AvailableTime = T + RuneRefreshtime
'					End If
'					Rune1.death = False
'					If death Then Rune1.death=true
'				Else
'					If Rune2.AvailableTime <= T And Rune2.death = True Then
'						If T - Rune2.AvailableTime <= 300 and Rune2.AvailableTime <> 0 Then
'							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
'						Else
'							Rune2.AvailableTime = T + RuneRefreshtime
'						End If
'						Rune2.death = False
'						If death Then Rune2.death=true
'					Else
'						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
'						Exit Function
'					End If
'					
'				End If
'			End If
'		End If
'	End Function
	
	
	Function ReserveFU(T As long) as Boolean
		' Reserve F
		Dim FReserved As Boolean
		
		dim i as Double
		If FrostRune1.AvailableTime <= T Then
			FrostRune1.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If FrostRune2.AvailableTime <= T Then
			FrostRune2.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
			BloodRune1.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
			BloodRune2.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If FReserved = False Then
			i = T
			Do Until FReserved = True
				If FrostRune1.AvailableTime <= i Then
					FrostRune1.reserved =  True
					goto ReserveU
				End If
				If FrostRune2.AvailableTime <= i Then
					FrostRune2.reserved =  True
					goto ReserveU
				End If
				If BloodRune1.AvailableTime <= i And BloodRune1.death = True Then
					BloodRune1.reserved =  True
					goto ReserveU
				End If
				If BloodRune2.AvailableTime <= i And BloodRune2.death = True Then
					BloodRune2.reserved =  True
					goto ReserveU
				End If
				i = i + 10
			Loop
		End If
		
		ReserveU:
		
		' Reserve U
		
		If UnholyRune1.AvailableTime <= T Then
			UnholyRune1.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If UnholyRune2.AvailableTime <= T Then
			UnholyRune2.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
			BloodRune1.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
			BloodRune2.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If FReserved = False Then
			i = T
			Do Until FReserved = True
				If UnholyRune1.AvailableTime <= i Then
					UnholyRune1.reserved =  True
					goto ToEnd
				End If
				If UnholyRune2.AvailableTime <= i Then
					UnholyRune2.reserved =  True
					goto ToEnd
				End If
				If BloodRune1.AvailableTime <= i And BloodRune1.death = True Then
					BloodRune1.reserved =  True
					goto ToEnd
				End If
				If BloodRune2.AvailableTime <= i And BloodRune2.death = True Then
					BloodRune2.reserved =  True
					goto ToEnd
				End If
				i = i + 100
			Loop
		End If
		ToEnd:
	End Function
	Function UnReserveFU(T As long) as Boolean
		BloodRune1.reserved=False
		BloodRune2.reserved=False
		FrostRune1.reserved=False
		FrostRune2.reserved=False
		UnholyRune1.reserved=False
		UnholyRune2.reserved=False
	End Function
	
	
	Function DRMFU( T as Long) As Boolean
		
		If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
		If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
		If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
		If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
		
	End Function
	
End Class
	
End Namespace

