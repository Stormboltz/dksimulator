Namespace Runes
Friend Partial Class runes
	friend BloodRune1 As Rune
	friend BloodRune2 As Rune
	friend FrostRune1 As Rune
	friend FrostRune2 As Rune
	friend UnholyRune1 As Rune
	Friend UnholyRune2 As Rune
	

	
	Protected sim As Sim
	
	Sub New(S As Sim)
		Sim = S
		If s.Cataclysm Then
			BloodRunes = New CataRune(s)
			FrostRunes = New CataRune(s)
			UnholyRunes = New CataRune(s)
		Else
			'I'm sure the sim will make the sim crash if something has been forgoten
			BloodRune1 = new rune(s)
			BloodRune2 = new rune(s)
			FrostRune1 = new rune(s)
			FrostRune2 = new rune(s)
			UnholyRune1 = new rune(s)
			UnholyRune2 = New rune(s)
			
			BloodRune1._Name = "Blood Rune 1"
			BloodRune2._Name = "Blood Rune 2"
			FrostRune1._Name = "Frost Rune 1"
			FrostRune2._Name = "Frost Rune 2"
			UnholyRune1._Name = "Unholy Rune 1"
			UnholyRune2._Name = "Unholy Rune 2"

		End If
		UnReserveFU(0)
	End Sub

	Sub SoftReset()
		If Sim.Cataclysm Then
			FillRunes
		Else
			BloodRune1.Reset()
			BloodRune2.Reset()
			FrostRune1.Reset()
			FrostRune2.Reset()
			UnholyRune1.Reset()
			UnholyRune2.Reset()
		End If

	End Sub
	
	
	
	Function RuneState() As String
		if sim.Cataclysm then return CataRuneState
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
                'Diagnostics.Debug.WriteLine ("Rune3.AvailableTime:" & (Rune3.AvailableTime)/100)
            End If
            If FrostRune2.AvailableTime <= T Then
                If FrostRune2.death = True Then
                    tmp = tmp & "D"
                Else
                    tmp = tmp & "F"
                End If
            Else
                tmp = tmp & int(-(T - FrostRune2.AvailableTime) / 100)
                'Diagnostics.Debug.WriteLine ("Rune4.AvailableTime:" & (Rune4.AvailableTime)/100)
            End If
            If UnholyRune1.AvailableTime <= T Then
                If UnholyRune1.death = True Then
                    tmp = tmp & "D"
                Else
                    tmp = tmp & "U"
                End If
            Else
                tmp = tmp & int(-(T - UnholyRune1.AvailableTime) / 100)
            End If
            If UnholyRune2.AvailableTime <= T Then
                If UnholyRune2.death = True Then
                    tmp = tmp & "D"
                Else
                    tmp = tmp & "U"
                End If
            Else
                tmp = tmp & int(-(T - UnholyRune2.AvailableTime) / 100)
            End If

            tmp = tmp & "]"
            Return tmp
        End Function


        Function RuneRefreshtime() As Integer
            If sim.UnholyPresence Then
                Return 1000 - 50 * sim.Character.talentunholy.ImprovedUnholyPresence
            Else
                Return 1000
            End If
        End Function

        Function BFU(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return cataBFU(T)
            If BloodRune1.AvailableTime <= T Or BloodRune2.AvailableTime <= T Then
                If FrostRune1.AvailableTime <= T Or FrostRune2.AvailableTime <= T Then
                    If UnholyRune1.AvailableTime <= T Or UnholyRune2.AvailableTime <= T Then
                        Return True
                    End If
                End If
            End If
        End Function


        Function GetNextUnholy(ByVal T As Long) As Long
            If sim.Cataclysm Then Return CataGetNextUnholy(T)
            Dim bArray As New collection
            If BloodRune1.AvailableTime > T And BloodRune1.death = True Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T And BloodRune2.death = True Then bArray.Add(BloodRune2.AvailableTime)
            If FrostRune1.AvailableTime > T And FrostRune1.death = True Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T And FrostRune2.death = True Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T Then bArray.Add(UnholyRune2.AvailableTime)
            If bArray.Count > 0 Then
                'bArray.Sort()
                Return bArray.Item(0)
            End If
        End Function



        Function GetNextFrost(ByVal T As Long) As Long
            If sim.Cataclysm Then Return CataGetNextFrost(T)
            Dim bArray As New collection
            If BloodRune1.AvailableTime > T And BloodRune1.death = True Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T And BloodRune2.death = True Then bArray.Add(BloodRune2.AvailableTime)
            If FrostRune1.AvailableTime > T Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T And UnholyRune1.death = True Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T And UnholyRune2.death = True Then bArray.Add(UnholyRune2.AvailableTime)
            If bArray.Count > 0 Then
                ' bArray.Sort()
                Return bArray.Item(0)
            End If
        End Function




        Function GetNextBloodCD(ByVal T As Long) As Long
            If sim.Cataclysm Then Return CataGetNextBloodCD(T)
            Dim bArray As New collection

            If BloodRune1.AvailableTime > T Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T Then bArray.Add(BloodRune2.AvailableTime)

            If FrostRune1.AvailableTime > T And FrostRune1.death = True Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T And FrostRune2.death = True Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T And UnholyRune1.death = True Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T And UnholyRune2.death = True Then bArray.Add(UnholyRune2.AvailableTime)

            If bArray.Count > 0 Then
                'bArray.Sort()
                Return bArray.Item(0)
            End If

        End Function



        Function AnyBlood(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataAnyBlood(T)
            If BloodRune1.AvailableTime <= T And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
        End Function


        Function Blood(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataBlood(T)
            If BloodRune1.AvailableTime <= T And BloodRune1.death = False And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.death = False And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
        End Function

        Function BloodOnly(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataBloodOnly(T)
            If BloodRune1.AvailableTime <= T Then Return True
            If BloodRune2.AvailableTime <= T Then Return True
        End Function


        Function FrostOnly(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataFrostOnly(T)
            If FrostRune1.AvailableTime <= T And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.reserved = False Then Return True
        End Function

        Function UnholyOnly(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataUnholyOnly(T)
            If UnholyRune1.AvailableTime <= T And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.reserved = False Then Return True
        End Function


        Function Frost(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataFrost(T)
            If BloodRune1.AvailableTime <= T And BloodRune1.death = True And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.death = True And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
        End Function

        Function Unholy(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataUnholy(T)
            If BloodRune1.AvailableTime <= T And BloodRune1.death = True And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.death = True And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.reserved = False Then Return True
        End Function

        Function FU(ByVal T As Long) As Boolean
            If sim.Cataclysm Then Return CataFU(T)
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
                Return False
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
            Return FR And UH


        End Function

        Function UseDeathBlood(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If sim.Cataclysm Then Return CataUseDeathBlood(T, Death)
            If BloodRune1.Available(T) And BloodRune1.death = True Then
                BloodRune1.Use(T, Death)
            Else
                If BloodRune2.Available(T) And BloodRune2.death = True Then
                    BloodRune2.Use(T, Death)
                End If
            End If
        End Function




        Function UseBlood(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If sim.Cataclysm Then Return CataUseBlood(T, Death)
            If BloodRune1.Available(T) And BloodRune1.death = False Then
                BloodRune1.Use(T, Death)
            Else
                If BloodRune2.Available(T) And BloodRune2.death = False Then
                    BloodRune2.Use(T, Death)
                Else
                    If BloodRune1.Available(T) Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available(T) Then
                            BloodRune2.Use(T, Death)
                        Else
                            If FrostRune1.Available(T) And FrostRune1.death = True Then
                                FrostRune1.Use(T, Death)
                            Else
                                If FrostRune2.Available(T) And FrostRune2.death = True Then
                                    FrostRune2.Use(T, Death)
                                Else
                                    If UnholyRune1.Available(T) And UnholyRune1.death = True Then
                                        UnholyRune1.Use(T, Death)
                                    Else
                                        If UnholyRune2.Available(T) And UnholyRune2.death = True Then
                                            UnholyRune2.Use(T, Death)
                                        Else
                                            Diagnostics.Debug.WriteLine("ERROR BLood RUNE")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If



        End Function
        Function UseFrost(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If sim.Cataclysm Then Return CataUseFrost(T, Death)
            If FrostRune1.Available(T) Then
                FrostRune1.Use(T, Death)
            Else
                If FrostRune2.Available(T) Then
                    FrostRune2.Use(T, Death)
                Else
                    If BloodRune1.Available(T) And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True Then
                                UnholyRune1.Use(T, Death)
                            Else
                                If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True Then
                                    UnholyRune2.Use(T, Death)
                                Else
                                    Diagnostics.Debug.WriteLine("ERROR FROST RUNE")

                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Function

        Function UseUnholy(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If sim.Cataclysm Then Return CataUseUnholy(T, Death)
            If UnholyRune1.AvailableTime <= T Then
                UnholyRune1.Use(T, Death)
            Else
                If UnholyRune2.AvailableTime <= T Then
                    UnholyRune2.Use(T, Death)
                Else
                    If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            If FrostRune1.AvailableTime <= T And FrostRune1.death = True Then
                                FrostRune1.Use(T, Death)
                            Else
                                If FrostRune2.AvailableTime <= T And FrostRune2.death = True Then
                                    FrostRune2.Use(T, Death)
                                Else
                                    Diagnostics.Debug.WriteLine("Unholy rune ERROR")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Function
        Function UseFU(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If sim.Cataclysm Then Return CataUseFU(T, Death)
            If FrostRune1.AvailableTime <= T Then
                FrostRune1.Use(T, Death)
            Else
                If FrostRune2.AvailableTime <= T Then
                    FrostRune2.Use(T, Death)
                Else
                    If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Function
                        End If
                    End If
                End If
            End If

            If UnholyRune1.AvailableTime <= T Then
                UnholyRune1.Use(T, Death)
            Else
                If UnholyRune2.AvailableTime <= T Then
                    UnholyRune2.Use(T, Death)
                Else
                    If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Function
                        End If
                    End If
                End If
            End If
        End Function

	
	
	Function ReserveFU(T As Long) As Boolean
		if sim.Cataclysm then return  CataReserveFU(T)
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
	Function UnReserveFU(T As Long) As Boolean
		if sim.Cataclysm then return CataUnReserveFU(T)
		BloodRune1.reserved=False
		BloodRune2.reserved=False
		FrostRune1.reserved=False
		FrostRune2.reserved=False
		UnholyRune1.reserved=False
		UnholyRune2.reserved=False
	End Function
	
	
	Function DRMFU( T as Long) As Boolean
		if sim.Cataclysm then return CataDRMFU(T)
		If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
		If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
		If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
		If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
		
	End Function
	
	Function RuneRefreshTheNextGCD(T As Long) As Boolean
		if sim.Cataclysm then return CataRuneRefreshTheNextGCD(T)
		Dim tmp As Long
		If sim.UnholyPresence Then
			tmp = T + 100+ sim.latency/10
		Else
			tmp = T + 150+ sim.latency/10
		End If
		If BloodRune1.AvailableTime >= T and BloodRune1.AvailableTime < tmp Then return false
		If BloodRune2.AvailableTime >= T and BloodRune2.AvailableTime < tmp Then return false
		If FrostRune1.AvailableTime >= T and FrostRune1.AvailableTime < tmp Then return false
		If FrostRune2.AvailableTime >= T and FrostRune2.AvailableTime < tmp Then return false
		If UnholyRune1.AvailableTime >= T and UnholyRune1.AvailableTime < tmp Then return false
		If UnholyRune2.AvailableTime >= T and UnholyRune2.AvailableTime < tmp Then Return False
		return true
	End Function
	
	
End Class
	
End Namespace

