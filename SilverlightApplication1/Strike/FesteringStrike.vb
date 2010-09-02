Public Class FesteringStrike
    Inherits Strikes.Strike
    Sub New(ByVal S As Sim)
        MyBase.New(S)
        BaseDamage = 840
        Coeficient = 1.5
        Multiplicator = 1
        Multiplicator = Multiplicator * (1 + 10 * sim.Character.Talents.Talent("CorruptingStrikes").Value / 100)
        logLevel = LogLevelEnum.Basic
    End Sub

    'Festering Strike *New* - An instant attack that deals 150% weapon damage 
    'plus 840.54 and increases the duration of your Blood Plague, Frost Fever, 
    'and Chains of Ice effects on the target by up to 6 sec. / 5 yd range, Blood+Frost

    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        If MyBase.ApplyDamage(T) = False Then
            sim.Runes.UseBF(T, False, True)
            UseGCD(T)
            Return False
        End If

        If OffHand = False Then

            sim.RunicPower.add(25)
            sim.Targets.MainTarget.BloodPlague.IncreaseDuration(600)
            sim.Targets.MainTarget.FrostFever.IncreaseDuration(600)
            sim.Runes.UseBF(T, True)
        End If
        Return True
    End Function
    

   
    
    
End Class
