Friend Class runes
	friend Rune1 As Rune
	friend Rune2 As Rune
	friend Rune3 As Rune
	friend Rune4 As Rune
	friend Rune5 As Rune
	friend Rune6 As Rune
	Protected sim As Sim
	
	Sub New(S As Sim)
		Sim = S
		Rune1 = new rune(s)
		Rune2 = new rune(s)
		Rune3 = new rune(s)
		Rune4 = new rune(s)
		Rune5 = new rune(s)
		Rune6 = new rune(s)
		
		UnReserveFU(0)
		Rune1.AvailableTime = 0
		Rune1.death = False
		Rune2.AvailableTime = 0
		Rune2.death = False
		Rune3.AvailableTime = 0
		Rune3.death = False
		Rune4.AvailableTime = 0
		Rune4.death = False
		Rune5.AvailableTime = 0
		Rune5.death = False
		Rune6.AvailableTime = 0
		Rune6.death = False
	End Sub
	
	Function RuneState() As String
		Dim T As Long
		T = sim.TimeStamp
		Dim tmp As String
		tmp = "["
		
		If Rune1.AvailableTime <= T Then
			If Rune1.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - Rune1.AvailableTime)/100)
		End If
		If Rune2.AvailableTime <= T  Then
			If Rune2.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - Rune2.AvailableTime)/100)
		End If
		If Rune3.AvailableTime <= T Then
			If Rune3.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - Rune3.AvailableTime)/100)
			'debug.Print ("Rune3.AvailableTime:" & (Rune3.AvailableTime)/100)
		End If
		If Rune4.AvailableTime <= T Then
			If Rune4.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - Rune4.AvailableTime)/100)
			'debug.Print ("Rune4.AvailableTime:" & (Rune4.AvailableTime)/100)
		End If
		If Rune5.AvailableTime <= T Then
			If Rune5.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - Rune5.AvailableTime)/100)
		End If
		If Rune6.AvailableTime <= T Then
			If Rune6.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - Rune6.AvailableTime)/100)
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
		If Rune1.AvailableTime <= T Or Rune2.AvailableTime <= T Then
			If Rune3.AvailableTime <= T or Rune4.AvailableTime <= T Then
				If Rune5.AvailableTime <= T Or Rune6.AvailableTime <= T Then
					return  True
				End If
			End If
		End If
	End Function
	
	
	Function GetNextUnholy(T As Long) As Long
		Dim bArray As new ArrayList
		if Rune1.AvailableTime > T 	And Rune1.death = true then bArray.Add(Rune1.AvailableTime)
		If Rune2.AvailableTime > T 	And Rune2.death = true Then bArray.Add(Rune2.AvailableTime)
		If Rune3.AvailableTime > T And Rune3.death = True	Then bArray.Add(Rune3.AvailableTime)
		If Rune4.AvailableTime > T And Rune4.death = True	Then bArray.Add(Rune4.AvailableTime)
		If Rune5.AvailableTime > T Then bArray.Add(Rune5.AvailableTime)
		If Rune6.AvailableTime > T Then bArray.Add(Rune6.AvailableTime)
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
	End Function
	
	
	
	Function GetNextFrost(T As Long) As Long
		Dim bArray As new ArrayList
		if Rune1.AvailableTime > T 	And Rune1.death = true then bArray.Add(Rune1.AvailableTime)
		If Rune2.AvailableTime > T 	And Rune2.death = true Then bArray.Add(Rune2.AvailableTime)
		If Rune3.AvailableTime > T Then bArray.Add(Rune3.AvailableTime)
		If Rune4.AvailableTime > T Then bArray.Add(Rune4.AvailableTime)
		If Rune5.AvailableTime > T And Rune5.death = True	Then bArray.Add(Rune5.AvailableTime)
		If Rune6.AvailableTime > T And Rune6.death = True	Then bArray.Add(Rune6.AvailableTime)
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
	End Function
	
	
	
	
	Function GetNextBloodCD(T As Long) As Long
		Dim bArray As new ArrayList
		
'		if Rune1.AvailableTime > T 	And Rune1.death = false then bArray.Add(Rune1.AvailableTime)
'		If Rune2.AvailableTime > T 	And Rune2.death = False Then bArray.Add(Rune2.AvailableTime)
		if Rune1.AvailableTime > T 	then bArray.Add(Rune1.AvailableTime)
		If Rune2.AvailableTime > T 	Then bArray.Add(Rune2.AvailableTime)
		
		If Rune3.AvailableTime > T And Rune3.death = True	Then bArray.Add(Rune3.AvailableTime)
		If Rune4.AvailableTime > T And Rune4.death = True	Then bArray.Add(Rune4.AvailableTime)
		If Rune5.AvailableTime > T And Rune5.death = True	Then bArray.Add(Rune5.AvailableTime)
		If Rune6.AvailableTime > T And Rune6.death = True	Then bArray.Add(Rune6.AvailableTime)
		
		If bArray.Count > 0 Then
			bArray.Sort()
			return bArray.Item(0)
		End If
		
	End Function
	
	
	
	Function AnyBlood(T as long) As Boolean
		If Rune1.AvailableTime <= T And Rune1.reserved=false Then return  True
		If Rune2.AvailableTime <= T And Rune2.reserved=false Then return  True
		If Rune3.AvailableTime <= T And Rune3.death = True and Rune3.reserved=false Then return  True
		If Rune4.AvailableTime <= T And Rune4.death = True and Rune4.reserved=false Then return  True
		If Rune5.AvailableTime <= T And Rune5.death = True and Rune5.reserved=false Then return  True
		If Rune6.AvailableTime <= T And Rune6.death = True and Rune6.reserved=false Then return  True
	End Function
	
	
	Function Blood(T as long) As Boolean
		If Rune1.AvailableTime <= T And Rune1.death = False and Rune1.reserved=false Then return  True
		If Rune2.AvailableTime <= T And Rune2.death = False and Rune2.reserved=false Then return  True
		If Rune3.AvailableTime <= T And Rune3.death = True and Rune3.reserved=false Then return  True
		If Rune4.AvailableTime <= T And Rune4.death = True and Rune4.reserved=false Then return  True
		If Rune5.AvailableTime <= T And Rune5.death = True and Rune5.reserved=false Then return  True
		If Rune6.AvailableTime <= T And Rune6.death = True and Rune6.reserved=false Then return  True
	End Function
	
	Function BloodOnly(T as long) As Boolean
		If Rune1.AvailableTime <= T Then return  True
		If Rune2.AvailableTime <= T Then return  True
	End Function
	
	
	Function FrostOnly(T As Long) As Boolean
		If Rune3.AvailableTime <= T and Rune3.reserved=false Then return True
		If Rune4.AvailableTime <= T and Rune4.reserved=false Then return True
	End Function
	
	Function UnholyOnly(T as long) As Boolean
		If Rune5.AvailableTime <= T and Rune5.reserved=false Then return True
		If Rune6.AvailableTime <= T and Rune6.reserved=false Then return True
	End Function
	
	
	Function Frost(T as long) As Boolean
		If Rune1.AvailableTime <= T And Rune1.death = True and Rune1.reserved=false Then return  True
		If Rune2.AvailableTime <= T And Rune2.death = True and Rune2.reserved=false Then return True
		If Rune3.AvailableTime <= T and Rune3.reserved=false Then return True
		If Rune4.AvailableTime <= T and Rune4.reserved=false Then return True
		If Rune5.AvailableTime <= T And Rune5.death = True and Rune5.reserved=false Then return True
		If Rune6.AvailableTime <= T And Rune6.death = True and Rune6.reserved=false Then return True
	End Function
	
	Function Unholy(T as long) As Boolean
		If Rune1.AvailableTime <= T And Rune1.death = True and Rune1.reserved=false Then return  True
		If Rune2.AvailableTime <= T And Rune2.death = True and Rune2.reserved=false Then return True
		If Rune3.AvailableTime <= T And Rune3.death = True and Rune3.reserved=false Then return True
		If Rune4.AvailableTime <= T And Rune4.death = True and Rune4.reserved=false Then return True
		If Rune5.AvailableTime <= T and Rune5.reserved=false Then return True
		If Rune6.AvailableTime <= T and Rune6.reserved=false Then return True
	End Function
	
	Function FU(T As long) As Boolean
		Dim UH As Boolean
		Dim Rune1reserved As Boolean
		Dim Rune2reserved As Boolean
		If Rune3.AvailableTime <= T Then
			UH = True
		Else
			If Rune4.AvailableTime <= T Then
				UH = True
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					Rune1reserved = True
					UH = True
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
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
		
		If Rune5.AvailableTime <= T Then
			FR = True
		Else
			If Rune6.AvailableTime <= T Then
				FR = True
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True And Rune1reserved = False Then
					FR = True
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True And Rune2reserved = False Then
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
		If Rune1.Available(T) And Rune1.death = False Then
			Rune1.Use(T,Death)
		Else
			If Rune2.Available(T) And Rune2.death = False Then
				Rune2.Use(T,Death)
			Else
				If Rune1.Available(T) Then
					Rune1.Use(T,Death)
				Else
					If Rune2.Available(T) Then
						Rune2.Use(T,Death)
					Else
						If Rune3.Available(T) And Rune3.death = True Then
							Rune3.Use(T,Death)
						Else
							If Rune4.Available(T) And Rune4.death = True Then
								Rune4.Use(T,Death)
							Else
								If Rune5.Available(T) And Rune5.death = True Then
									Rune5.Use(T,Death)
								Else
									If Rune6.Available(T) And Rune6.death = True Then
										Rune6.Use(T,Death)
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
		If Rune3.Available(T) Then
			Rune3.Use(T,Death)
		Else
			If Rune4.Available(T) Then
				Rune4.Use(T,Death)
			Else
				If Rune1.Available(T) And Rune1.death = True Then
					Rune1.Use(T,Death)
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						Rune2.Use(T,Death)
					Else
						If Rune5.AvailableTime <= T And Rune5.death = True Then
							Rune5.Use(T,Death)
						Else
							If Rune6.AvailableTime <= T And Rune6.death = True Then
								Rune6.Use(T,Death)
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
		If Rune5.AvailableTime <= T Then
			Rune5.Use(T,Death)
		Else
			If Rune6.AvailableTime <= T Then
				Rune6.Use(T,Death)
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					Rune1.Use(T,Death)
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						rune2.Use(T,Death)
					Else
						If Rune3.AvailableTime <= T And Rune3.death = True Then
							rune3.Use(T,Death)
						Else
							If Rune4.AvailableTime <= T And Rune4.death = True Then
								rune4.Use(T,Death)
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
		If Rune3.AvailableTime <= T Then
			rune3.Use(T,Death)
		Else
			If Rune4.AvailableTime <= T Then
				rune4.Use(T,Death)
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					rune1.Use(T,Death)
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						rune2.Use(T,Death)
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
				End If
			End If
		End If
		
		If Rune5.AvailableTime <= T Then
			rune5.Use(T,Death)
		Else
			If Rune6.AvailableTime <= T Then
				rune6.Use(T,Death)
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					rune1.Use(T,Death)
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						rune2.Use(T,Death)
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
		If Rune3.AvailableTime <= T Then
			Rune3.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If Rune4.AvailableTime <= T Then
			Rune4.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If Rune1.AvailableTime <= T And Rune1.death = True Then
			Rune1.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If Rune2.AvailableTime <= T And Rune2.death = True Then
			Rune2.reserved =  True
			FReserved = True
			goto ReserveU
		end if
		If FReserved = False Then
			i = T
			Do Until FReserved = True
				If Rune3.AvailableTime <= i Then
					Rune3.reserved =  True
					goto ReserveU
				End If
				If Rune4.AvailableTime <= i Then
					Rune4.reserved =  True
					goto ReserveU
				End If
				If Rune1.AvailableTime <= i And Rune1.death = True Then
					Rune1.reserved =  True
					goto ReserveU
				End If
				If Rune2.AvailableTime <= i And Rune2.death = True Then
					Rune2.reserved =  True
					goto ReserveU
				End If
				i = i + 10
			Loop
		End If
		
		ReserveU:
		
		' Reserve U
		
		If Rune5.AvailableTime <= T Then
			Rune5.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If Rune6.AvailableTime <= T Then
			Rune6.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If Rune1.AvailableTime <= T And Rune1.death = True Then
			Rune1.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If Rune2.AvailableTime <= T And Rune2.death = True Then
			Rune2.reserved =  True
			FReserved = True
			goto ToEnd
		end if
		If FReserved = False Then
			i = T
			Do Until FReserved = True
				If Rune5.AvailableTime <= i Then
					Rune5.reserved =  True
					goto ToEnd
				End If
				If Rune6.AvailableTime <= i Then
					Rune6.reserved =  True
					goto ToEnd
				End If
				If Rune1.AvailableTime <= i And Rune1.death = True Then
					Rune1.reserved =  True
					goto ToEnd
				End If
				If Rune2.AvailableTime <= i And Rune2.death = True Then
					Rune2.reserved =  True
					goto ToEnd
				End If
				i = i + 100
			Loop
		End If
		ToEnd:
	End Function
	Function UnReserveFU(T As long) as Boolean
		Rune1.reserved=False
		Rune2.reserved=False
		Rune3.reserved=False
		Rune4.reserved=False
		Rune5.reserved=False
		Rune6.reserved=False
	End Function
	
	
	Function DRMFU( T as Long) As Boolean
		
		If (Rune3.AvailableTime <= T And Rune3.death = False) And (Rune5.AvailableTime <= T And Rune5.death = False) Then Return True
		If (Rune3.AvailableTime <= T And Rune3.death = False) And (Rune6.AvailableTime <= T And Rune6.death = False) Then Return True
		If (Rune4.AvailableTime <= T And Rune4.death = False) And (Rune5.AvailableTime <= T And Rune5.death = False) Then Return True
		If (Rune4.AvailableTime <= T And Rune4.death = False) And (Rune6.AvailableTime <= T And Rune6.death = False) Then Return True
		
	End Function
	
End Class
	

