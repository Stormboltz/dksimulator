Namespace Simulator.WowObjects.Spells
    Public Class DarkTransformation
        Inherits Spell

        Friend DarkTransformationBuff As Procs.Proc

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            Resource = New Resource(s, ResourcesEnum.UnholyRune, False, 15)
            DarkTransformationBuff = New Procs.Proc(sim)
            DarkTransformationBuff.ProcLenght = 30
            DarkTransformationBuff.ProcChance = 1
            DarkTransformationBuff.ProcOn = Procs.ProcsManager.ProcOnType.OnMisc
            DarkTransformationBuff._Name = "Dark Transformation"

            If sim.Character.Talents.Talent("DarkTransformation").Value > 0 Then
                Talented = True
                DarkTransformationBuff.Equip()
            End If
            Resource = New Resource(s, ResourcesEnum.UnholyRune, False, 15)
        End Sub

        Overrides Function IsAvailable() As Boolean
            If Not Talented Then Return False
            If Not sim.Ghoul.ShadowInfusion.Stack >= 5 Then Return False
            Return MyBase.IsAvailable
        End Function

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Use()
            UseGCD(T)
            sim.CombatLog.write(T & vbTab & "Dark Transformation")
            HitCount = HitCount + 1
            sim.Ghoul.ShadowInfusion.Stack = 0
            sim.Ghoul.ShadowInfusion.CD = T + 3000
            sim.Ghoul.ShadowInfusion.Cancel()
            DarkTransformationBuff.TryMe(T)
            Return (True)
        End Function
    End Class
End Namespace
