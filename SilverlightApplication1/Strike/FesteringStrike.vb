Public Class FesteringStrike
    Inherits Strikes.Strike
    Sub New(ByVal S As Sim)
        MyBase.New(S)
        BaseDamage = 840
        Coeficient = 1.5
        Multiplicator = 1
    End Sub

    'Festering Strike *New* - An instant attack that deals 150% weapon damage 
    'plus 840.54 and increases the duration of your Blood Plague, Frost Fever, 
    'and Chains of Ice effects on the target by up to 6 sec. / 5 yd range, Blood+Frost

    public Overrides Function ApplyDamage(T As long) As boolean
        UseGCD(T)
        Dim ret As Boolean = MyBase.ApplyDamage(T)
        If ret Then
            sim.RunicPower.add(10)
            sim.Targets.MainTarget.BloodPlague.FadeAt = sim.Targets.MainTarget.BloodPlague.FadeAt + 6 * 100
            sim.Targets.MainTarget.FrostFever.FadeAt = sim.Targets.MainTarget.FrostFever.FadeAt + 6 * 100
            sim.Runes.UseBF(T, True)
            Return True
        Else
            Return False
        End If
    End Function
    

   
    
    
End Class
