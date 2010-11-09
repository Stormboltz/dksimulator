Namespace Simulator.WowObjects.Spells
    Public Class Outbreak
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            Me.Resource = New Resource(S, Resource.ResourcesEnum.None, 0)
            logLevel = LogLevelEnum.Basic
            DamageSchool = DamageSchoolEnum.OtherMagical
        End Sub

        Public Overrides Function IsAvailable() As Boolean
            If sim.level85 Then
                Return (CD < sim.TimeStamp)
            Else
                Return False
            End If

        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            UseGCD(T)
            Use()
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            If ret = True Then
                sim.Targets.MainTarget.FrostFever.Apply(T)
                sim.Targets.MainTarget.BloodPlague.Apply(T)
            End If
            CD = T + 6000
            Return ret
        End Function
    End Class
End Namespace

