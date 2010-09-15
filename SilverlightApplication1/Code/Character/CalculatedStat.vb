Namespace Simulator.Character.CalculatedStats

    Public Enum StatEnum
        PhysicalCrit
        SpellCrit
        ArmorPenetration
        Mastery
        PhysicalHaste
        SpellHaste
        Haste
        SpellHit
        MeleeHit
    End Enum

    Public Class CalculatedStat
        Inherits SimObjet

        Friend CurrentValue As Double
        Friend AdditiveBuff As Double
        Friend MultiplicativeBuff As Double = 1
        Friend WithEvents PrimaryStat As PrimaryStat
        Friend RatingRatio As Double = 1
        Friend Stat As StatEnum
        Event CaluclatedStat_Update()

        Overridable Sub On_PrimaryStat_Update() Handles PrimaryStat.ValueModifed
            CalculateCurrentValue()

        End Sub
        Overridable Sub CalculateCurrentValue()
            TimeWasted.Start()
            Dim tmp As Double = PrimaryStat.Value / (RatingRatio * 100)
            CurrentValue = (tmp + AdditiveBuff) * MultiplicativeBuff
            RaiseEvent CaluclatedStat_Update()
            TimeWasted.Pause()
        End Sub

        Protected Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s)
            Me.PrimaryStat = PrimaryStat
            CalculateCurrentValue()
        End Sub
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat, ByVal ratio As Double)
            MyBase.New(s)
            Me.PrimaryStat = PrimaryStat
            RatingRatio = ratio
            CalculateCurrentValue()
        End Sub
        Overridable Function Value() As Double
            Return CurrentValue
        End Function

        Sub Add(ByVal i As Integer)
            AdditiveBuff += i
            CalculateCurrentValue()
        End Sub
        Sub AddMulti(ByVal i As Double)
            MultiplicativeBuff *= i
            CalculateCurrentValue()
        End Sub
        Sub Remove(ByVal i As Integer)
            AdditiveBuff -= i
            CalculateCurrentValue()
        End Sub
        Sub RemoveMulti(ByVal i As Double)
            MultiplicativeBuff /= i
            CalculateCurrentValue()
        End Sub
    End Class
    Class PhysicalHaste
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 25.22
            AdditiveBuff = 1 '<= Very important
            CalculateCurrentValue()
        End Sub
    End Class
    Class SpellHaste
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 25.22
            AdditiveBuff = 1 '<= Very important
            CalculateCurrentValue()
        End Sub
    End Class
    Class Haste
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 25.22
            AdditiveBuff = 1 '<= Very important
            CalculateCurrentValue()
        End Sub
    End Class
    Class Expertise
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 32.79
            AdditiveBuff = 0.25 * sim.Character.Talents.Talent("Vot3W").Value * 2
            CalculateCurrentValue()
        End Sub
    End Class
    Class MHExpertise
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 32.79
            AdditiveBuff = 0.25 * sim.Character.Talents.Talent("Vot3W").Value * 2
            AdditiveBuff += sim.Character.MHExpertiseBonus * 0.25 / 100
            CalculateCurrentValue()
        End Sub
    End Class
    Class OHExpertise
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 32.79
            AdditiveBuff = 0.25 * sim.Character.Talents.Talent("Vot3W").Value * 2
            AdditiveBuff += sim.Character.OHExpertiseBonus * 0.25 / 100
            CalculateCurrentValue()
        End Sub
    End Class
    Class SpellCrit
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 45.91
            AdditiveBuff += 5 * sim.Character.Buff.Crit / 100
            AdditiveBuff += 5 * sim.Targets.MainTarget.Debuff.SpellCritTaken / 100
            AdditiveBuff -= 0.021 'Spell crit malus vs bosses
            CalculateCurrentValue()
        End Sub
    End Class
    Class Crit
        Inherits CalculatedStat
        Sub New(ByVal s As Sim, ByVal PrimaryStat As PrimaryStat)
            MyBase.New(s, PrimaryStat)
            RatingRatio = 45.91
            AdditiveBuff += 5 * sim.Character.Buff.Crit / 100
            AdditiveBuff += 5 * sim.Targets.MainTarget.Debuff.SpellCritTaken / 100
            AdditiveBuff -= 0.048 'Crit malus vs bosses
            CalculateCurrentValue()
        End Sub
        Overrides Sub CalculateCurrentValue()
            MyBase.CalculateCurrentValue()
            CurrentValue = MyBase.Value() + ((sim.Character.Agility.Value / 62.5) / 100)
        End Sub
    End Class
    Class ModifiedWeaponSpeed
        Inherits CalculatedStat
        Dim WithEvents CalculatedStat As CalculatedStat
        Dim WeapSpeed As Double
        Sub New(ByVal s As Sim, ByVal WeaponSpeed As Double, ByVal CalcStat As CalculatedStat)
            MyBase.New(s, CalcStat.PrimaryStat)
            CalculatedStat = CalcStat
            WeapSpeed = WeaponSpeed
            CalculateCurrentValue()
            _Name = "ModifiedWeaponSpeed"
        End Sub
        Overrides Sub CalculateCurrentValue() Handles CalculatedStat.CaluclatedStat_Update
            If WeapSpeed = 0 Then Return
            If CalculatedStat Is Nothing Then Return
            If CalculatedStat.Value = 0 Then Return
            CurrentValue = WeapSpeed / CalculatedStat.Value
            ' Diagnostics.Debug.WriteLine(sim.TimeStamp & "WeaponSpeed=" & CurrentValue)
        End Sub
    End Class
End Namespace

