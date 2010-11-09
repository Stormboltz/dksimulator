Namespace Simulator







    Class Resource
        Enum ResourcesEnum
            RunicPower
            BloodRune
            BloodOrDeathRune
            FrostRune
            UnholyRune
            BloodFrostRune
            FrostUnholy
            Death
            BloodTap
            AllRunicPower
            None
        End Enum


        Dim sim As Sim
        Dim Ress As ResourcesEnum
        Dim Cost As Integer
        Dim DeathRune As Boolean

        Sub New(ByVal s As Sim, ByVal Ressource As ResourcesEnum)
            sim = s
            Ress = Ressource
            Me.Cost = 0
        End Sub


        Sub New(ByVal s As Sim, ByVal Ressource As Resource.ResourcesEnum, ByVal RPCost As Integer)
            sim = s
            Ress = Ressource
            Me.Cost = RPCost
        End Sub
        Sub New(ByVal s As Sim, ByVal Ressource As Resource.ResourcesEnum, ByVal ToDeathRune As Boolean, ByVal RPGain As Integer)
            sim = s
            Ress = Ressource
            DeathRune = ToDeathRune
            Cost = RPGain
        End Sub
        Function IsAvailable() As Boolean
            Select Case Ress
                Case Resource.ResourcesEnum.RunicPower
                    Return sim.RunicPower.Check(Cost)
                Case Resource.ResourcesEnum.BloodFrostRune
                    Return sim.Runes.BF()
                Case Resource.ResourcesEnum.BloodOrDeathRune
                    Return sim.Runes.AnyBlood()
                Case Resource.ResourcesEnum.BloodRune
                    If DeathRune Then
                        Return sim.Runes.BloodOnlyNoDeath()
                    Else
                        Return sim.Runes.AnyBlood()
                    End If
                Case Resource.ResourcesEnum.Death
                    Return sim.Runes.Death()
                Case Resource.ResourcesEnum.FrostRune
                    Return sim.Runes.Frost()
                Case Resource.ResourcesEnum.FrostUnholy
                    Return sim.Runes.FU()
                Case Resource.ResourcesEnum.UnholyRune
                    Return sim.Runes.Unholy()
                Case Resource.ResourcesEnum.BloodTap
                    Return sim.BloodTap.IsAvailable()
                Case Resource.ResourcesEnum.AllRunicPower
                    Return True
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
                    Return False
            End Select
        End Function

        Sub Use()
            Select Case Ress
                Case Resource.ResourcesEnum.RunicPower
                    sim.RunicPower.Use(Cost)
                Case Resource.ResourcesEnum.AllRunicPower
                    sim.RunicPower.UseAll()
                Case Resource.ResourcesEnum.BloodFrostRune
                    sim.Runes.UseBF(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.BloodRune, Resource.ResourcesEnum.BloodOrDeathRune
                    sim.Runes.UseBlood(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.Death
                    sim.Runes.UseDeathBlood(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.FrostRune
                    sim.Runes.UseFrost(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.FrostUnholy
                    sim.Runes.UseFU(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.UnholyRune
                    sim.Runes.UseUnholy(sim.TimeStamp, DeathRune)
                    sim.RunicPower.add(Cost)
                Case Resource.ResourcesEnum.BloodTap
                    sim.BloodTap.Use()
                    sim.Runes.UseDeathBlood(sim.TimeStamp, True)
                Case Resource.ResourcesEnum.None
                    sim.RunicPower.add(Cost)
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
            End Select

        End Sub
        Sub UseAlf()

            Dim A As Boolean = True
            Select Case Ress
                Case Resource.ResourcesEnum.RunicPower
                    sim.RunicPower.Use(Cost / 2)
                Case Resource.ResourcesEnum.BloodFrostRune
                    sim.Runes.UseBF(sim.TimeStamp, DeathRune, A)
                Case Resource.ResourcesEnum.BloodRune
                    sim.Runes.UseBlood(sim.TimeStamp, DeathRune, A)
                Case Resource.ResourcesEnum.Death
                    sim.Runes.UseDeathBlood(sim.TimeStamp, DeathRune, A)
                Case Resource.ResourcesEnum.FrostRune
                    sim.Runes.UseFrost(sim.TimeStamp, DeathRune, A)
                Case Resource.ResourcesEnum.FrostUnholy
                    sim.Runes.UseFU(sim.TimeStamp, DeathRune, A)
                Case Resource.ResourcesEnum.UnholyRune
                    sim.Runes.UseUnholy(sim.TimeStamp, DeathRune, A)
                Case Else
                    Diagnostics.Debug.WriteLine("WTF IS THIS RESOURCE")
            End Select
        End Sub
    End Class

   
End Namespace
