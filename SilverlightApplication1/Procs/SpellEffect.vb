Namespace Simulator.WowObjects.Procs


    Public Class SpellEffect
        Inherits Effect

        Dim Effect As SpellEffectManager.SpeelEffectEnum

      
        Sub New(ByVal s As Sim, ByVal name As String, ByVal Effect As SpellEffectManager.SpeelEffectEnum, ByVal value As Double, ByVal Length As Long)
            MyBase.New(s)
            s.SpellEffectManager.SpellEffects.Add(Me)
            _Name = name
            Me.Effect = Effect
            Me.Value = value
            Me.Lenght = Length
        End Sub

        Overrides Sub Apply()
            MyBase.Apply()


            Dim T As Long = sim.TimeStamp
            If Currentstack < MaxStack Then
                Currentstack += 1

                Select Case Effect
                    Case SpellEffectManager.SpeelEffectEnum.StrengthPercentage
                        sim.Character.Strength.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackSpeed
                        sim.Character.PhysicalHaste.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseCastingSpeed
                        sim.Character.SpellHaste.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackAndCastingSpeed
                        sim.Character.SpellHaste.AddMulti(Value * Currentstack)
                        sim.Character.PhysicalHaste.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseRuneRegeneration
                        sim.Character.RuneRegeneration.AddMulti(Value * Currentstack)
                        For i As Integer = 1 To Lenght
                            sim.FutureEventManager.Add(T + (i * 100), "RuneFill", Me)
                        Next
                    Case SpellEffectManager.SpeelEffectEnum.RunicEmpowerement

                        Currentstack = 0
                        Dim d As Double '= RngCrit
                        Dim DepletedRunes As New List(Of Runes.CataRune)
                        If sim.Runes.BloodRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.BloodRune1)
                        If sim.Runes.BloodRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.BloodRune2)

                        If sim.Runes.UnholyRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.UnholyRune1)
                        If sim.Runes.UnholyRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.UnholyRune2)

                        If sim.Runes.FrostRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.FrostRune1)
                        If sim.Runes.FrostRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.FrostRune2)

                        If DepletedRunes.Count = 0 Then
                            sim.CombatLog.write(sim.TimeStamp & vbTab & Me.Name & " proc with no depleted rune")
                            Return
                        End If

                        d = (DepletedRunes.Count - 1) * RngHit()
                        Dim dec As Decimal = Convert.ToDecimal(d)
                        Dim i As Integer
                        i = Decimal.Round(dec, 0)

                        Try
                            DepletedRunes.Item(i).Value = 100
                            sim.CombatLog.write(sim.TimeStamp & vbTab & Me.Name & " on " & DepletedRunes.Item(i).Name)
                        Catch ex As Exception

                        End Try
                End Select
            End If
            

            If Not IsNothing(FutureEvent) Then
                If FutureEvent.T > T Then
                    sim.FutureEventManager.Remove(FutureEvent)
                End If
            End If
            If Lenght <> 0 Then
                FutureEvent = sim.FutureEventManager.Add(T + (Lenght * 100), "BuffFade", Me)
            End If
            AddUptime(T)
        End Sub
        Overrides Sub Fade()
            MyBase.Fade()
            If Multiplicator <> 0 And Currentstack <> 0 Then
                Select Case Effect
                    Case SpellEffectManager.SpeelEffectEnum.StrengthPercentage
                        sim.Character.Strength.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackSpeed
                        sim.Character.PhysicalHaste.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseCastingSpeed
                        sim.Character.SpellHaste.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackAndCastingSpeed
                        sim.Character.PhysicalHaste.RemoveMulti(Value * Currentstack)
                        sim.Character.SpellHaste.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseRuneRegeneration
                        sim.Character.RuneRegeneration.RemoveMulti(Value * Currentstack)
                    Case Else

                End Select

            End If
            Currentstack = 0
            RemoveUptime(sim.TimeStamp)
        End Sub

    End Class

    Public Class SpellEffectManager
        Enum SpeelEffectEnum
            IncreaseAttackSpeed
            IncreaseCastingSpeed
            IncreaseAttackAndCastingSpeed
            IncreaseRuneRegeneration
            StrengthPercentage
            RunicEmpowerement
            Debuff
            KillingMachine
        End Enum

        Friend SpellEffects As New List(Of SpellEffect)

        Sub SoftReset()
            For Each SE In SpellEffects
                SE.Fade()
            Next
        End Sub




    End Class

End Namespace

