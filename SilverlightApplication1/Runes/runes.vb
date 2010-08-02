Namespace Runes
Friend Partial Class runes
        Protected sim As Sim
        Friend BloodRune1 As CataRune
        Friend FrostRune1 As CataRune
        Friend UnholyRune1 As CataRune
        Friend BloodRune2 As CataRune
        Friend FrostRune2 As CataRune
        Friend UnholyRune2 As CataRune

        Friend BloodRunes As RunePair
        Friend FrostRunes As RunePair
        Friend UnholyRunes As RunePair
	
        Sub New(ByVal S As Sim)
            sim = S
            BloodRune1 = New CataRune(S)
            BloodRune2 = New CataRune(S)
            FrostRune1 = New CataRune(S)
            FrostRune2 = New CataRune(S)
            UnholyRune1 = New CataRune(S)
            UnholyRune2 = New CataRune(S)
            BloodRune1._Name = "Blood Rune 1"
            BloodRune2._Name = "Blood Rune 2"
            FrostRune1._Name = "Frost Rune 1"
            FrostRune2._Name = "Frost Rune 2"
            UnholyRune1._Name = "Unholy Rune 1"
            UnholyRune2._Name = "Unholy Rune 2"

            BloodRunes = New RunePair(BloodRune1, BloodRune2)
            FrostRunes = New RunePair(FrostRune1, FrostRune2)
            UnholyRunes = New RunePair(UnholyRune1, UnholyRune2)
        End Sub

        Sub SoftReset()
            BloodRune1.Reset()
            BloodRune2.Reset()
            FrostRune1.Reset()
            FrostRune2.Reset()
            UnholyRune1.Reset()
            UnholyRune2.Reset()
            FillRunes()
        End Sub
	
        Sub FillRunes()
            BloodRunes.Refill(0.1)
            FrostRunes.Refill(0.1)
            UnholyRunes.Refill(0.1)
            sim.FutureEventManager.Add(sim.TimeStamp + 10, "RuneFill")
        End Sub

	
	Function RuneState() As String
            Dim tmp As String = ""
            tmp = tmp & "[" & BloodRune1.Value & ":" & BloodRune1.Death_Report
            tmp = tmp & ":" & BloodRune2.Value & ":" & BloodRune2.Death_Report
            tmp = tmp & ":" & FrostRune1.Value & ":" & FrostRune1.Death_Report
            tmp = tmp & ":" & FrostRune2.Value & ":" & FrostRune2.Death_Report
            tmp = tmp & ":" & UnholyRune1.Value & ":" & UnholyRune1.Death_Report
            tmp = tmp & ":" & UnholyRune2.Value & ":" & UnholyRune2.Death_Report
            tmp = tmp & ":" & sim.RunicPower.GetValue & ":" & sim.RunicPower.MaxValue
            tmp = tmp & "]"
            Return tmp
        End Function


        Function BF() As Boolean
            Return (BloodRunes.Available And FrostRunes.Available)
        End Function
        Sub UseBF(ByVal T As Long, ByVal Death As Boolean)
            'use Blood
            If BloodRune1.Available And BloodRune1.death = False Then
                BloodRune1.Use(T, Death)
            ElseIf BloodRune2.Available And BloodRune2.death = False Then
                BloodRune2.Use(T, Death)
            ElseIf BloodRune1.Available Then
                BloodRune1.Use(T, Death)
            ElseIf BloodRune2.Available Then
                BloodRune2.Use(T, Death)
            Else
                Diagnostics.Debug.WriteLine("BF ERROR")
            End If

Frost:
            'Use Frost
            If FrostRune1.Available And FrostRune1.death = False Then
                FrostRune1.Use(T, Death)
            ElseIf FrostRune2.Available And FrostRune2.death = False Then
                FrostRune2.Use(T, Death)
            ElseIf FrostRune1.Available Then
                FrostRune1.Use(T, Death)
            ElseIf FrostRune2.Available Then
                FrostRune2.Use(T, Death)
            Else
                Diagnostics.Debug.WriteLine("BF ERROR")
            End If
        End Sub
        Function BFU(Optional ByVal T As Long = -1) As Boolean
            If T = -1 Then
                If BloodRune1.Available Or BloodRune2.Available Then
                    If FrostRune1.Available Or FrostRune2.Available Then
                        If UnholyRune1.Available Or UnholyRune2.Available Then
                            Return True
                        End If
                    End If
                End If
                Return False
            Else
                If BloodRune1.AvailableTime <= T Or BloodRune2.AvailableTime <= T Then
                    If FrostRune1.AvailableTime <= T Or FrostRune2.AvailableTime <= T Then
                        If UnholyRune1.AvailableTime <= T Or UnholyRune2.AvailableTime <= T Then
                            Return True
                        End If
                    End If
                End If
                Return False
            End If


        End Function
        Function GetNextUnholy(ByVal T As Long) As Long
            Dim bArray As New Collections.Generic.List(Of Long)
            If BloodRune1.AvailableTime > T And BloodRune1.death = True Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T And BloodRune2.death = True Then bArray.Add(BloodRune2.AvailableTime)
            If FrostRune1.AvailableTime > T And FrostRune1.death = True Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T And FrostRune2.death = True Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T Then bArray.Add(UnholyRune2.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            Else
                Return T
            End If

        End Function
        Function GetNextFrost(ByVal T As Long) As Long

            Dim bArray As New Collections.Generic.List(Of Long)
            If BloodRune1.AvailableTime > T And BloodRune1.death = True Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T And BloodRune2.death = True Then bArray.Add(BloodRune2.AvailableTime)
            If FrostRune1.AvailableTime > T Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T And UnholyRune1.death = True Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T And UnholyRune2.death = True Then bArray.Add(UnholyRune2.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            End If
            Return T
        End Function
        Function GetNextBloodCD(ByVal T As Long) As Long

            Dim bArray As New Collections.Generic.List(Of Long)
            If BloodRune1.AvailableTime > T Then bArray.Add(BloodRune1.AvailableTime)
            If BloodRune2.AvailableTime > T Then bArray.Add(BloodRune2.AvailableTime)
            If FrostRune1.AvailableTime > T And FrostRune1.death = True Then bArray.Add(FrostRune1.AvailableTime)
            If FrostRune2.AvailableTime > T And FrostRune2.death = True Then bArray.Add(FrostRune2.AvailableTime)
            If UnholyRune1.AvailableTime > T And UnholyRune1.death = True Then bArray.Add(UnholyRune1.AvailableTime)
            If UnholyRune2.AvailableTime > T And UnholyRune2.death = True Then bArray.Add(UnholyRune2.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            End If
            Return T
        End Function
        Function AnyBlood(ByVal T As Long) As Boolean
            If BloodRune1.AvailableTime <= T And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
            Return False
        End Function
        Function Blood(ByVal T As Long) As Boolean

            If BloodRune1.AvailableTime <= T And BloodRune1.death = False And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.death = False And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
            Return False
        End Function
        Function BloodOnly(ByVal T As Long) As Boolean

            If BloodRune1.AvailableTime <= T Then Return True
            If BloodRune2.AvailableTime <= T Then Return True
            Return False
        End Function
        Function FrostOnly(ByVal T As Long) As Boolean
            If FrostRune1.AvailableTime <= T And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.reserved = False Then Return True
            Return False
        End Function
        Function UnholyOnly(ByVal T As Long) As Boolean

            If UnholyRune1.AvailableTime <= T And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.reserved = False Then Return True
            Return False
        End Function
        Function Frost(ByVal T As Long) As Boolean
            If BloodRune1.AvailableTime <= T And BloodRune1.death = True And BloodRune1.reserved = False Then Return True
            If BloodRune2.AvailableTime <= T And BloodRune2.death = True And BloodRune2.reserved = False Then Return True
            If FrostRune1.AvailableTime <= T And FrostRune1.reserved = False Then Return True
            If FrostRune2.AvailableTime <= T And FrostRune2.reserved = False Then Return True
            If UnholyRune1.AvailableTime <= T And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.AvailableTime <= T And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
            Return False
        End Function

        Function Unholy() As Boolean
            If BloodRune1.Available And BloodRune1.death = True And BloodRune1.reserved = False Then Return True
            If BloodRune2.Available And BloodRune2.death = True And BloodRune2.reserved = False Then Return True
            If FrostRune1.Available And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.Available And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.Available Then Return True
            If UnholyRune2.Available Then Return True
            Return False
        End Function

        Function FU(ByVal T As Long) As Boolean
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
        Sub UseDeathBlood(ByVal T As Long, ByVal Death As Boolean)
            If BloodRune1.Available(T) And BloodRune1.death = True Then
                BloodRune1.Use(T, Death)
            Else
                If BloodRune2.Available(T) And BloodRune2.death = True Then
                    BloodRune2.Use(T, Death)
                End If
            End If
        End Sub


        Sub UseBlood(ByVal T As Long, ByVal Death As Boolean)
            If BloodRune1.Available And BloodRune1.death = False Then
                BloodRune1.Use(T, Death)
            Else
                If BloodRune2.Available And BloodRune2.death = False Then
                    BloodRune2.Use(T, Death)
                Else
                    If BloodRune1.Available Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available Then
                            BloodRune2.Use(T, Death)
                        Else
                            If FrostRune1.Available And FrostRune1.death = True Then
                                FrostRune1.Use(T, Death)
                            Else
                                If FrostRune2.Available And FrostRune2.death = True Then
                                    FrostRune2.Use(T, Death)
                                Else
                                    If UnholyRune1.Available And UnholyRune1.death = True Then
                                        UnholyRune1.Use(T, Death)
                                    Else
                                        If UnholyRune2.Available And UnholyRune2.death = True Then
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



        End Sub
        Sub UseFrost(ByVal T As Long, ByVal Death As Boolean)
           

            If FrostRune1.Available Then
                FrostRune1.Use(T, Death)
            Else
                If FrostRune2.Available Then
                    FrostRune2.Use(T, Death)
                Else
                    If BloodRune1.Available And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            If UnholyRune1.Available And UnholyRune1.death = True Then
                                UnholyRune1.Use(T, Death)
                            Else
                                If UnholyRune2.Available And UnholyRune2.death = True Then
                                    UnholyRune2.Use(T, Death)
                                Else
                                    Diagnostics.Debug.WriteLine("ERROR FROST RUNE")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Sub UseUnholy(ByVal T As Long, ByVal Death As Boolean)
            

            If UnholyRune1.Available Then
                UnholyRune1.Use(T, Death)
            Else
                If UnholyRune2.Available Then
                    UnholyRune2.Use(T, Death)
                Else
                    If BloodRune1.Available And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            If FrostRune1.Available And FrostRune1.death = True Then
                                FrostRune1.Use(T, Death)
                            Else
                                If FrostRune2.Available And FrostRune2.death = True Then
                                    FrostRune2.Use(T, Death)
                                Else
                                    Diagnostics.Debug.WriteLine("Unholy rune ERROR")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Sub UseFU(ByVal T As Long, ByVal Death As Boolean)
            If FrostRune1.Available Then
                FrostRune1.Use(T, Death)
            Else
                If FrostRune2.Available Then
                    FrostRune2.Use(T, Death)
                Else
                    If BloodRune1.Available And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If UnholyRune1.Available Then
                UnholyRune1.Use(T, Death)
            Else
                If UnholyRune2.Available Then
                    UnholyRune2.Use(T, Death)
                Else
                    If BloodRune1.Available And BloodRune1.death = True Then
                        BloodRune1.Use(T, Death)
                    Else
                        If BloodRune2.Available And BloodRune2.death = True Then
                            BloodRune2.Use(T, Death)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End Sub



        '        Function ReserveFU(ByVal T As Long) As Boolean
        '            ' Reserve F
        '            Dim FReserved As Boolean

        '            Dim i As Double
        '            If FrostRune1.AvailableTime <= T Then
        '                FrostRune1.reserved = True
        '                FReserved = True
        '                GoTo ReserveU
        '            End If
        '            If FrostRune2.AvailableTime <= T Then
        '                FrostRune2.reserved = True
        '                FReserved = True
        '                GoTo ReserveU
        '            End If
        '            If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
        '                BloodRune1.reserved = True
        '                FReserved = True
        '                GoTo ReserveU
        '            End If
        '            If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
        '                BloodRune2.reserved = True
        '                FReserved = True
        '                GoTo ReserveU
        '            End If
        '            If FReserved = False Then
        '                i = T
        '                Do Until FReserved = True
        '                    If FrostRune1.AvailableTime <= i Then
        '                        FrostRune1.reserved = True
        '                        GoTo ReserveU
        '                    End If
        '                    If FrostRune2.AvailableTime <= i Then
        '                        FrostRune2.reserved = True
        '                        GoTo ReserveU
        '                    End If
        '                    If BloodRune1.AvailableTime <= i And BloodRune1.death = True Then
        '                        BloodRune1.reserved = True
        '                        GoTo ReserveU
        '                    End If
        '                    If BloodRune2.AvailableTime <= i And BloodRune2.death = True Then
        '                        BloodRune2.reserved = True
        '                        GoTo ReserveU
        '                    End If
        '                    i = i + 10
        '                Loop
        '            End If

        'ReserveU:

        '            ' Reserve U

        '            If UnholyRune1.AvailableTime <= T Then
        '                UnholyRune1.reserved = True
        '                FReserved = True
        '                GoTo ToEnd
        '            End If
        '            If UnholyRune2.AvailableTime <= T Then
        '                UnholyRune2.reserved = True
        '                FReserved = True
        '                GoTo ToEnd
        '            End If
        '            If BloodRune1.AvailableTime <= T And BloodRune1.death = True Then
        '                BloodRune1.reserved = True
        '                FReserved = True
        '                GoTo ToEnd
        '            End If
        '            If BloodRune2.AvailableTime <= T And BloodRune2.death = True Then
        '                BloodRune2.reserved = True
        '                FReserved = True
        '                GoTo ToEnd
        '            End If
        '            If FReserved = False Then
        '                i = T
        '                Do Until FReserved = True
        '                    If UnholyRune1.AvailableTime <= i Then
        '                        UnholyRune1.reserved = True
        '                        GoTo ToEnd
        '                    End If
        '                    If UnholyRune2.AvailableTime <= i Then
        '                        UnholyRune2.reserved = True
        '                        GoTo ToEnd
        '                    End If
        '                    If BloodRune1.AvailableTime <= i And BloodRune1.death = True Then
        '                        BloodRune1.reserved = True
        '                        GoTo ToEnd
        '                    End If
        '                    If BloodRune2.AvailableTime <= i And BloodRune2.death = True Then
        '                        BloodRune2.reserved = True
        '                        GoTo ToEnd
        '                    End If
        '                    i = i + 100
        '                Loop
        '            End If
        'ToEnd:
        '            Return False
        '        End Function
        'Sub UnReserveFU(ByVal T As Long)
        '    BloodRune1.reserved = False
        '    BloodRune2.reserved = False
        '    FrostRune1.reserved = False
        '    FrostRune2.reserved = False
        '    UnholyRune1.reserved = False
        '    UnholyRune2.reserved = False
        'End Sub


        Function DRMFU(ByVal T As Long) As Boolean

            If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
            If (FrostRune1.AvailableTime <= T And FrostRune1.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
            If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune1.AvailableTime <= T And UnholyRune1.death = False) Then Return True
            If (FrostRune2.AvailableTime <= T And FrostRune2.death = False) And (UnholyRune2.AvailableTime <= T And UnholyRune2.death = False) Then Return True
            Return False
        End Function

        Function RuneRefreshTheNextGCD(ByVal T As Long) As Boolean
            Dim tmp As Long
            tmp = T + 150 + sim.latency / 10
            If BloodRune1.AvailableTime >= T And BloodRune1.AvailableTime < tmp Then Return False
            If BloodRune2.AvailableTime >= T And BloodRune2.AvailableTime < tmp Then Return False
            If FrostRune1.AvailableTime >= T And FrostRune1.AvailableTime < tmp Then Return False
            If FrostRune2.AvailableTime >= T And FrostRune2.AvailableTime < tmp Then Return False
            If UnholyRune1.AvailableTime >= T And UnholyRune1.AvailableTime < tmp Then Return False
            If UnholyRune2.AvailableTime >= T And UnholyRune2.AvailableTime < tmp Then Return False
            Return True
        End Function


    End Class
	
End Namespace

