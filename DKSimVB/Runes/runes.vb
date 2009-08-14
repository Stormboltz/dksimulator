Friend Module runes
	Function RuneRefreshtime As Integer
		If mainstat.UnholyPresence Then
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
		If Rune1.AvailableTime <= T And Rune1.death = False Then
			If T - Rune1.AvailableTime <= 200 Then
				Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
			Else
				Rune1.AvailableTime = T + RuneRefreshtime
			End If
			If death Then Rune1.death = True
		Else
			If Rune2.AvailableTime <= T And Rune2.death = False Then
				
				If T - Rune2.AvailableTime <= 200 Then
					Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
				Else
					Rune2.AvailableTime = T + RuneRefreshtime
				End If
				If death Then Rune2.death = True
			Else
				If Rune1.AvailableTime <= T Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					
					If death Then Rune1.death = True
				Else
					If Rune2.AvailableTime <= T Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						If death Then Rune2.death = True
					Else
						If Rune3.AvailableTime <= T And Rune3.death = True Then
							If T - Rune3.AvailableTime <= 200 Then
								Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
							Else
								Rune3.AvailableTime = T + RuneRefreshtime
							End If
							rune3.death=False
						Else
							If Rune4.AvailableTime <= T And Rune4.death = True Then
								If T - Rune4.AvailableTime <= 200 Then
									Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
								Else
									Rune4.AvailableTime = T + RuneRefreshtime
								End If
								
								rune4.death=False
								If death Then Rune2.death=true
							Else
								If Rune5.AvailableTime <= T And Rune5.death = True Then
									If T - Rune5.AvailableTime <= 200 Then
										Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
									Else
										Rune5.AvailableTime = T + RuneRefreshtime
									End If
									rune5.death=False
									If death Then Rune5.death=true
								Else
									If Rune6.AvailableTime <= T And Rune6.death = True Then
										If T - Rune6.AvailableTime <= 200 Then
											Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
										Else
											Rune6.AvailableTime = T + RuneRefreshtime
										End If
										
										rune6.death=False
										If death Then Rune6.death=true
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
		If Rune3.AvailableTime <= T Then
			If T - Rune3.AvailableTime <= 200 Then
				Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
			Else
				Rune3.AvailableTime = T + RuneRefreshtime
			End If
			rune3.death=False
			If death Then Rune3.death=true
		Else
			If Rune4.AvailableTime <= T Then
				If T - Rune4.AvailableTime <= 200 Then
					Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
				Else
					Rune4.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune4.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					Rune1.AvailableTime = T+RuneRefreshtime
					rune1.death=False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						Rune2.AvailableTime = T+RuneRefreshtime
						rune2.death=False
						If death Then Rune2.death=true
					Else
						If Rune5.AvailableTime <= T And Rune5.death = True Then
							If T - Rune5.AvailableTime <= 200 Then
								Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
							Else
								Rune5.AvailableTime = T + RuneRefreshtime
							End If
							rune5.death=False
							If death Then Rune5.death=true
						Else
							If Rune6.AvailableTime <= T And Rune6.death = True Then
								If T - Rune6.AvailableTime <= 200 Then
									Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
								Else
									Rune6.AvailableTime = T + RuneRefreshtime
								End If
								
								rune6.death=False
								If death Then Rune6.death=true
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
			If T - Rune5.AvailableTime <= 200 Then
				Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
			Else
				Rune5.AvailableTime = T + RuneRefreshtime
			End If
			If death Then Rune5.death=true
		Else
			If Rune6.AvailableTime <= T Then
				If T - Rune6.AvailableTime <= 200 Then
					Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
				Else
					Rune6.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune6.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					Rune1.death=False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						rune2.death=False
						If death Then Rune2.death=True
					Else
						If Rune3.AvailableTime <= T And Rune3.death = True Then
							If T - Rune3.AvailableTime <= 200 Then
								Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
							Else
								Rune3.AvailableTime = T + RuneRefreshtime
							End If
							rune3.death=False
							rune3.death=False
							If death Then Rune3.death=true
						Else
							If Rune4.AvailableTime <= T And Rune4.death = True Then
								If T - Rune4.AvailableTime <= 200 Then
									Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
								Else
									Rune4.AvailableTime = T + RuneRefreshtime
								End If
								
								rune4.death=False
								If death Then Rune4.death=true
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
			If T - Rune3.AvailableTime <= 200 Then
				Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
			Else
				Rune3.AvailableTime = T + RuneRefreshtime
			End If
			rune3.death=False
			If death Then Rune3.death=true
		Else
			If Rune4.AvailableTime <= T Then
				If T - Rune4.AvailableTime <= 200 Then
					Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
				Else
					Rune4.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune4.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					Rune1.death = False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						Rune2.death = False
						If death Then Rune2.death=true
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
					
				End If
			End If
		End If
		
		If Rune5.AvailableTime <= T Then
			If T - Rune5.AvailableTime <= 200 Then
				Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
			Else
				Rune5.AvailableTime = T + RuneRefreshtime
			End If
			If death Then Rune5.death=true
		Else
			If Rune6.AvailableTime <= T Then
				If T - Rune6.AvailableTime <= 200 Then
					Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
				Else
					Rune6.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune6.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					Rune1.death = False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						Rune2.death = False
						If death Then Rune2.death=true
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
					
				End If
			End If
		End If
	End Function
	Function UseFU(T as long,Death as Boolean,UseReservation as Boolean) As Boolean
		If Rune3.AvailableTime <= T Then
			If T - Rune3.AvailableTime <= 200 Then
				Rune3.AvailableTime = Rune3.AvailableTime + RuneRefreshtime
			Else
				Rune3.AvailableTime = T + RuneRefreshtime
			End If
			rune3.death=False
			If death Then Rune3.death=true
		Else
			If Rune4.AvailableTime <= T Then
				If T - Rune4.AvailableTime <= 200 Then
					Rune4.AvailableTime = Rune4.AvailableTime + RuneRefreshtime
				Else
					Rune4.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune4.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					Rune1.death = False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						Rune2.death = False
						If death Then Rune2.death=true
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
					
				End If
			End If
		End If
		
		If Rune5.AvailableTime <= T Then
			If T - Rune5.AvailableTime <= 200 Then
				Rune5.AvailableTime = Rune5.AvailableTime + RuneRefreshtime
			Else
				Rune5.AvailableTime = T + RuneRefreshtime
			End If
			If death Then Rune5.death=true
		Else
			If Rune6.AvailableTime <= T Then
				If T - Rune6.AvailableTime <= 200 Then
					Rune6.AvailableTime = Rune6.AvailableTime + RuneRefreshtime
				Else
					Rune6.AvailableTime = T + RuneRefreshtime
				End If
				
				If death Then Rune6.death=true
			Else
				If Rune1.AvailableTime <= T And Rune1.death = True Then
					If T - Rune1.AvailableTime <= 200 Then
						Rune1.AvailableTime = Rune1.AvailableTime + RuneRefreshtime
					Else
						Rune1.AvailableTime = T + RuneRefreshtime
					End If
					Rune1.death = False
					If death Then Rune1.death=true
				Else
					If Rune2.AvailableTime <= T And Rune2.death = True Then
						If T - Rune2.AvailableTime <= 200 Then
							Rune2.AvailableTime = Rune2.AvailableTime + RuneRefreshtime
						Else
							Rune2.AvailableTime = T + RuneRefreshtime
						End If
						Rune2.death = False
						If death Then Rune2.death=true
					Else
						Debug.Print ("ERRRRRROOOOORRRR FU @ :" & T)
						Exit Function
					End If
					
				End If
			End If
		End If
	End Function
	
	
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
	Sub Init()
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
	
	Function DRMFU( T as Long) As Boolean
		
		If (Rune3.AvailableTime <= T And Rune3.death = False) And (Rune5.AvailableTime <= T And Rune5.death = False) Then Return True
		If (Rune3.AvailableTime <= T And Rune3.death = False) And (Rune6.AvailableTime <= T And Rune6.death = False) Then Return True
		If (Rune4.AvailableTime <= T And Rune4.death = False) And (Rune5.AvailableTime <= T And Rune5.death = False) Then Return True
		If (Rune4.AvailableTime <= T And Rune4.death = False) And (Rune6.AvailableTime <= T And Rune6.death = False) Then Return True
		
	End Function
	
End Module
	

