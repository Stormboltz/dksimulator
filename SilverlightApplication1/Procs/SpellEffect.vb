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


            If Currentstack < MaxStack Then
                Currentstack += 1

                Select Case Effect
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackSpeed
                        sim.Character.PhysicalHaste.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseCastingSpeed
                        sim.Character.SpellHaste.AddMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackAndCastingSpeed
                        sim.Character.SpellHaste.AddMulti(Value * Currentstack)
                        sim.Character.PhysicalHaste.AddMulti(Value * Currentstack)
                End Select
            End If
            
            Dim T As Long = sim.TimeStamp
            If Not IsNothing(FutureEvent) Then
                If FutureEvent.T > T Then
                    sim.FutureEventManager.Remove(FutureEvent)
                End If
            End If
            FutureEvent = sim.FutureEventManager.Add(T + (Lenght * 100), "BuffFade", Me)
            AddUptime(T)

        End Sub
        Overrides Sub Fade()
            If Multiplicator <> 0 And Currentstack <> 0 Then
                Select Case Effect
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackSpeed
                        sim.Character.PhysicalHaste.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseCastingSpeed
                        sim.Character.SpellHaste.RemoveMulti(Value * Currentstack)
                    Case SpellEffectManager.SpeelEffectEnum.IncreaseAttackAndCastingSpeed
                        sim.Character.PhysicalHaste.RemoveMulti(Value * Currentstack)
                        sim.Character.SpellHaste.RemoveMulti(Value * Currentstack)
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
        End Enum

        Friend SpellEffects As New List(Of SpellEffect)

        Sub SoftReset()
            For Each SE In SpellEffects
                SE.Fade()
            Next
        End Sub




    End Class

End Namespace

