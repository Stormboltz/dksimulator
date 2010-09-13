Namespace Simulator.WowObjects.Spells
    Public Class DarkTransformation
        Inherits Spell

        Friend DarkTransformationBuff As Procs.Proc

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            DarkTransformationBuff = New Procs.Proc(sim)
            DarkTransformationBuff.ProcLenght = 30
            DarkTransformationBuff.ProcChance = 1
            DarkTransformationBuff.ProcOn = Procs.ProcsManager.ProcOnType.OnMisc
            DarkTransformationBuff._Name = "Dark Transformation"

            If sim.Character.Talents.Talent("DarkTransformation").Value > 0 Then
                DarkTransformationBuff.Equip()
            End If

        End Sub

        Function IsAvailable() As Boolean
            If DarkTransformationBuff.Equiped = 0 Then Return False
            If Not sim.Runes.Unholy Then Return False
            If Not sim.Ghoul.ShadowInfusion.Stack >= 5 Then Return False
            Return True
        End Function

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            sim.Runes.UseUnholy(T, False)
            HitCount = HitCount + 1
            sim.Ghoul.ShadowInfusion.Stack -= 5
            sim.Ghoul.ShadowInfusion.CD = T + 3000
            sim.Ghoul.ShadowInfusion.Cancel()
            DarkTransformationBuff.TryMe(T)
            Return (True)
        End Function
    End Class
End Namespace
