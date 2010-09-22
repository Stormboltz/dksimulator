Namespace Simulator
    Enum ResourcesEnum
        RunicPower
        BloodRune
        FrostRune
        UnholyRune
        BloodFrostRune
        FrostUnholy
        Death
        BloodTap
        AllRunicPower
        None
    End Enum






    Class Resource
        Dim sim As Sim
        Dim Ress As ResourcesEnum
        Dim Cost As Integer
        Dim DeathRune As Boolean

        Sub New(ByVal s As Sim, ByVal Ressource As ResourcesEnum)
            sim = s
            Ress = Ressource
            Me.Cost = 0
        End Sub


        Sub New(ByVal s As Sim, ByVal Ressource As ResourcesEnum, ByVal RPCost As Integer)
            sim = s
            Ress = Ressource
            Me.Cost = Cost
        End Sub
        Sub New(ByVal s As Sim, ByVal Ressource As ResourcesEnum, ByVal ToDeathRune As Boolean, ByVal RPGain As Integer)
            sim = s
            Ress = Ressource
            DeathRune = ToDeathRune
            Cost = RPGain
        End Sub
        Function IsAvailable() As Boolean
            Select Case Ress
                Case ResourcesEnum.RunicPower
                    Return sim.RunicPower.Check(Cost)
                Case ResourcesEnum.BloodFrostRune
                    Return sim.Runes.BF()
                Case ResourcesEnum.BloodRune
                    If DeathRune Then
                        Return sim.Runes.BloodOnly(sim.TimeStamp)
                    Else
                        Return sim.Runes.AnyBlood(sim.TimeStamp)
                    End If
                Case ResourcesEnum.Death
                    Return sim.Runes.Death()
                Case ResourcesEnum.FrostRune
                    Return sim.Runes.Frost(sim.TimeStamp)
                Case ResourcesEnum.FrostUnholy
                    Return sim.Runes.FU(sim.TimeStamp)
                Case ResourcesEnum.UnholyRune
                    Return sim.Runes.Unholy()
                Case ResourcesEnum.BloodTap
                    Return sim.BloodTap.IsAvailable()
                Case ResourcesEnum.AllRunicPower
                    Return True
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
                    Return False
            End Select
        End Function

        Sub Use()
            Select Case Ress
                Case ResourcesEnum.RunicPower
                    sim.RunicPower.Use(Cost)
                Case ResourcesEnum.AllRunicPower
                    sim.RunicPower.UseAll()
                Case ResourcesEnum.BloodFrostRune
                    sim.Runes.UseBF(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.BloodRune
                    sim.Runes.UseBlood(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.Death
                    sim.Runes.UseDeathBlood(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.FrostRune
                    sim.Runes.UseFrost(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.FrostUnholy
                    sim.Runes.UseFU(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.UnholyRune
                    sim.Runes.UseUnholy(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case ResourcesEnum.BloodRune
                    sim.BloodTap.Use()
                    sim.Runes.UseDeathBlood(sim.TimeStamp, True)
                Case ResourcesEnum.None
                    sim.RunicPower.add(Cost)
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
            End Select

        End Sub
        Sub UseAlf()
            Dim A As Boolean = True
            Select Case Ress
                Case ResourcesEnum.RunicPower
                    sim.RunicPower.Use(Cost / 2)
                Case ResourcesEnum.BloodFrostRune
                    sim.Runes.UseBF(sim.TimeStamp, DeathRune, A)
                Case ResourcesEnum.BloodRune
                    sim.Runes.UseBlood(sim.TimeStamp, DeathRune, A)
                Case ResourcesEnum.Death
                    sim.Runes.UseDeathBlood(sim.TimeStamp, DeathRune, A)
                Case ResourcesEnum.FrostRune
                    sim.Runes.UseFrost(sim.TimeStamp, DeathRune, A)
                Case ResourcesEnum.FrostUnholy
                    sim.Runes.UseFU(sim.TimeStamp, DeathRune, A)
                Case ResourcesEnum.UnholyRune
                    sim.Runes.UseUnholy(sim.TimeStamp, DeathRune, A)
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
            End Select
        End Sub
    End Class

    Public Class Resources


    End Class
End Namespace
