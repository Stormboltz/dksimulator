Namespace Simulator.WowObjects.Runes
    Partial Friend Class runes
        Inherits SimObjet


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
            MyBase.New(S)
            _Name = "Runes"
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

        Overrides Sub SoftReset()
            MyBase.SoftReset()
            BloodRune1.Reset()
            BloodRune2.Reset()
            FrostRune1.Reset()
            FrostRune2.Reset()
            UnholyRune1.Reset()
            UnholyRune2.Reset()
            FillRunes()
        End Sub
        Friend PreviousRuneFill As Long
        Sub FillRunes()
            Dim l As Long = sim.TimeStamp
            If l = PreviousRuneFill Then Exit Sub
            TimeWasted.Start()
            BloodRunes.Refill((l - PreviousRuneFill) / 100)
            FrostRunes.Refill((l - PreviousRuneFill) / 100)
            UnholyRunes.Refill((l - PreviousRuneFill) / 100)
            If Not sim.isInGCD(l) Then
                sim.FutureEventManager.Add(sim.TimeStamp + 10, "RuneFill")
            End If
            PreviousRuneFill = sim.TimeStamp
            TimeWasted.Pause()
        End Sub
        Function RuneState() As String
            Dim tmp As String = ""
            tmp = tmp & "[" & CInt(BloodRune1.Value) & ":" & BloodRune1.Death_Report
            tmp = tmp & ":" & CInt(BloodRune2.Value) & ":" & BloodRune2.Death_Report
            tmp = tmp & ":" & CInt(FrostRune1.Value) & ":" & FrostRune1.Death_Report
            tmp = tmp & ":" & CInt(FrostRune2.Value) & ":" & FrostRune2.Death_Report
            tmp = tmp & ":" & CInt(UnholyRune1.Value) & ":" & UnholyRune1.Death_Report
            tmp = tmp & ":" & CInt(UnholyRune2.Value) & ":" & UnholyRune2.Death_Report
            tmp = tmp & ":" & sim.RunicPower.GetValue & ":" & sim.RunicPower.MaxValue
            tmp = tmp & "]"
            Return tmp
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


#Region "Get Next Runes"
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
#End Region

#Region "Rune Available"
        Function BF() As Boolean
            Return (BloodOnlyNoDeath() And FrostOnlyNoDeath())
        End Function
        Function BFU() As Boolean
            If BloodRune1.Available Or BloodRune2.Available Then
                If FrostRune1.Available Or FrostRune2.Available Then
                    If UnholyRune1.Available Or UnholyRune2.Available Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Function
        Function Death() As Boolean
            If BloodRune1.Available And BloodRune1.death = True Then Return True
            If BloodRune2.Available And BloodRune2.death = True Then Return True
            If FrostRune1.Available And FrostRune1.death = True Then Return True
            If FrostRune2.Available And FrostRune2.death = True Then Return True
            If UnholyRune1.Available And UnholyRune1.death = True Then Return True
            If UnholyRune2.Available And UnholyRune2.death = True Then Return True
            Return False
        End Function
        Function AnyBlood() As Boolean
            If BloodRune1.Available And BloodRune1.reserved = False Then Return True
            If BloodRune2.Available And BloodRune2.reserved = False Then Return True
            If FrostRune1.Available And FrostRune1.death = True And FrostRune1.reserved = False Then Return True
            If FrostRune2.Available And FrostRune2.death = True And FrostRune2.reserved = False Then Return True
            If UnholyRune1.Available And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.Available And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
            Return False
        End Function
        Function Blood() As Boolean
            If BloodRune1.Available And BloodRune1.death = False Then Return True
            If BloodRune2.Available And BloodRune2.death = False Then Return True
            If FrostRune1.Available And FrostRune1.death = True Then Return True
            If FrostRune2.Available And FrostRune2.death = True Then Return True
            If UnholyRune1.Available And UnholyRune1.death = True Then Return True
            If UnholyRune2.Available And UnholyRune2.death = True Then Return True
            Return False
        End Function
        Function BloodOnly() As Boolean
            If BloodRune1.Available Then Return True
            If BloodRune2.Available Then Return True

            Return False
        End Function

        Function BloodOnlyNoDeath() As Boolean
            If BloodRune1.Available And Not BloodRune1.death Then Return True
            If BloodRune2.Available And Not BloodRune2.death Then Return True
            Return False
        End Function
        Function FrostOnlyNoDeath() As Boolean
            If FrostRune1.Available And FrostRune1.death = False Then Return True
            If FrostRune2.Available And FrostRune2.death = False Then Return True
            Return False
        End Function
        Function UnholyOnlyNoDeath() As Boolean

            If UnholyRune1.Available And UnholyRune1.death = False Then Return True
            If UnholyRune2.Available And UnholyRune2.death = False Then Return True
            Return False
        End Function
        Function Frost() As Boolean
            If BloodRune1.Available And BloodRune1.death = True And BloodRune1.reserved = False Then Return True
            If BloodRune2.Available And BloodRune2.death = True And BloodRune2.reserved = False Then Return True
            If FrostRune1.Available And FrostRune1.reserved = False Then Return True
            If FrostRune2.Available And FrostRune2.reserved = False Then Return True
            If UnholyRune1.Available And UnholyRune1.death = True And UnholyRune1.reserved = False Then Return True
            If UnholyRune2.Available And UnholyRune2.death = True And UnholyRune2.reserved = False Then Return True
            Return False
        End Function
        Function FrostOnly() As Boolean
            If FrostRune1.Available Then Return True
            If FrostRune2.Available Then Return True
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

        Function FU() As Boolean
            Dim UH As Boolean
            Dim Rune1reserved As Boolean
            Dim Rune2reserved As Boolean
            If FrostRune1.Available Then
                UH = True
            Else
                If FrostRune2.Available Then
                    UH = True
                Else
                    If BloodRune1.Available And BloodRune1.death = True Then
                        Rune1reserved = True
                        UH = True
                    Else
                        If BloodRune2.Available And BloodRune2.death = True Then
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

            If UnholyRune1.Available Then
                FR = True
            Else
                If UnholyRune2.Available Then
                    FR = True
                Else
                    If BloodRune1.Available And BloodRune1.death = True And Rune1reserved = False Then
                        FR = True
                    Else
                        If BloodRune2.Available And BloodRune2.death = True And Rune2reserved = False Then
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
        Function DRMFU() As Boolean

            If (FrostRune1.Available And FrostRune1.death = False) And (UnholyRune1.Available And UnholyRune1.death = False) Then Return True
            If (FrostRune1.Available And FrostRune1.death = False) And (UnholyRune2.Available And UnholyRune2.death = False) Then Return True
            If (FrostRune2.Available And FrostRune2.death = False) And (UnholyRune1.Available And UnholyRune1.death = False) Then Return True
            If (FrostRune2.Available And FrostRune2.death = False) And (UnholyRune2.Available And UnholyRune2.death = False) Then Return True
            Return False
        End Function
#End Region

#Region "UseRunes"
        Sub UseDeathBlood(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)
            If BloodRune2.Available(T) And BloodRune2.death = True Then
                BloodRune2.Use(T, False, Alf)
            Else
                If BloodRune1.Available(T) And BloodRune1.death = True Then
                    BloodRune1.Use(T, False, Alf)
                End If
            End If
        End Sub
        Sub UseBlood(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)
            If BloodRune2.Available And BloodRune2.death = False Then
                BloodRune2.Use(T, Death, Alf)
            Else
                If BloodRune1.Available And BloodRune1.death = False Then
                    BloodRune1.Use(T, Death, Alf)
                Else
                    If BloodRune2.Available Then
                        BloodRune2.Use(T, Death, Alf)
                    Else
                        If BloodRune1.Available Then
                            BloodRune1.Use(T, Death, Alf)
                        Else
                            If FrostRune2.Available And FrostRune2.death = True Then
                                FrostRune2.Use(T, Death, Alf)
                            Else
                                If FrostRune1.Available And FrostRune1.death = True Then
                                    FrostRune1.Use(T, Death, Alf)
                                Else
                                    If UnholyRune2.Available And UnholyRune2.death = True Then
                                        UnholyRune2.Use(T, Death, Alf)
                                    Else
                                        If UnholyRune1.Available And UnholyRune1.death = True Then
                                            UnholyRune1.Use(T, Death, Alf)
                                        Else
                                            Diagnostics.Debug.WriteLine("ERROR Blood RUNE")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If



        End Sub
        Sub UseFrost(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)


            If FrostRune2.Available Then
                FrostRune2.Use(T, Death, Alf)
            Else
                If FrostRune1.Available Then
                    FrostRune1.Use(T, Death, Alf)
                Else
                    If BloodRune2.Available And BloodRune2.death = True Then
                        BloodRune2.Use(T, Death, Alf)
                    Else
                        If BloodRune1.Available And BloodRune1.death = True Then
                            BloodRune1.Use(T, Death, Alf)
                        Else
                            If UnholyRune2.Available And UnholyRune2.death = True Then
                                UnholyRune2.Use(T, Death, Alf)
                            Else
                                If UnholyRune1.Available And UnholyRune1.death = True Then
                                    UnholyRune1.Use(T, Death, Alf)
                                Else
                                    Diagnostics.Debug.WriteLine("ERROR FROST RUNE")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Sub UseUnholy(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)
            If UnholyRune2.Available Then
                UnholyRune2.Use(T, Death, Alf)
            Else
                If UnholyRune1.Available Then
                    UnholyRune1.Use(T, Death, Alf)
                Else
                    If BloodRune2.Available And BloodRune2.death = True Then
                        BloodRune2.Use(T, Death, Alf)
                    Else
                        If BloodRune1.Available And BloodRune1.death = True Then
                            BloodRune1.Use(T, Death, Alf)
                        Else
                            If FrostRune2.Available And FrostRune2.death = True Then
                                FrostRune2.Use(T, Death, Alf)
                            Else
                                If FrostRune1.Available And FrostRune1.death = True Then
                                    FrostRune1.Use(T, Death, Alf)
                                Else
                                    Diagnostics.Debug.WriteLine("Unholy rune ERROR")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Sub UseFU(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)
            If FrostRune2.Available Then
                FrostRune2.Use(T, Death, Alf)
            Else
                If FrostRune1.Available Then
                    FrostRune1.Use(T, Death, Alf)
                Else
                    If BloodRune2.Available And BloodRune2.death = True Then
                        BloodRune2.Use(T, Death, Alf)
                    Else
                        If BloodRune1.Available And BloodRune1.death = True Then
                            BloodRune1.Use(T, Death, Alf)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If UnholyRune2.Available Then
                UnholyRune2.Use(T, Death, Alf)
            Else
                If UnholyRune1.Available Then
                    UnholyRune1.Use(T, Death, Alf)
                Else
                    If BloodRune2.Available And BloodRune2.death = True Then
                        BloodRune2.Use(T, Death, Alf)
                    Else
                        If BloodRune1.Available And BloodRune1.death = True Then
                            BloodRune1.Use(T, Death, Alf)
                        Else
                            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End Sub
        Sub UseBF(ByVal T As Long, ByVal Death As Boolean, Optional ByVal Alf As Boolean = False)
            'use Blood

            If BloodRune2.Available And BloodRune2.death = False Then
                BloodRune2.Use(T, Death, Alf)
            ElseIf BloodRune1.Available And BloodRune1.death = False Then
                BloodRune1.Use(T, Death, Alf)
            ElseIf BloodRune2.Available Then
                BloodRune2.Use(T, Death, Alf)
            ElseIf BloodRune1.Available Then
                BloodRune1.Use(T, Death, Alf)
            Else
                Diagnostics.Debug.WriteLine("BF ERROR")
            End If

Frost:
            'Use Frost
            If FrostRune2.Available And FrostRune2.death = False Then
                FrostRune2.Use(T, Death, Alf)
            ElseIf FrostRune1.Available And FrostRune1.death = False Then
                FrostRune1.Use(T, Death, Alf)
            ElseIf FrostRune2.Available Then
                FrostRune2.Use(T, Death, Alf)
            ElseIf FrostRune1.Available Then
                FrostRune1.Use(T, Death, Alf)
            Else
                Diagnostics.Debug.WriteLine("BF ERROR")
            End If
        End Sub
#End Region




        Function RuneRefreshTheNextGCD(ByVal T As Long) As Boolean
            Dim tmp As Long
            tmp = T + 100 + sim.latency / 10
            If BloodRune1.AvailableTime > T And BloodRune1.AvailableTime < tmp Then Return False
            If BloodRune2.AvailableTime > T And BloodRune2.AvailableTime < tmp Then Return False
            If FrostRune1.AvailableTime > T And FrostRune1.AvailableTime < tmp Then Return False
            If FrostRune2.AvailableTime > T And FrostRune2.AvailableTime < tmp Then Return False
            If UnholyRune1.AvailableTime > T And UnholyRune1.AvailableTime < tmp Then Return False
            If UnholyRune2.AvailableTime > T And UnholyRune2.AvailableTime < tmp Then Return False
            Return True
        End Function


    End Class

End Namespace

