Namespace Simulator.Character
    Public Class PrimaryStat
        Inherits SimObjet

        Friend CanHaveMultiplicativeBuff As Boolean
        Friend CanHaveProc As Boolean
        Private Stat As Sim.Stat
        Private BaseValue As Integer
        Private CurrentValue As Integer
        Private MultiplicativeBuff As Double = 1
        Event ValueModifed()

        Sub New(ByVal Type As Sim.Stat, ByVal S As Sim, Optional ByVal CanHaveProc As Boolean = False, Optional ByVal CanHaveMultiplicativeBuff As Boolean = False)
            MyBase.New(S)
            Me.CanHaveProc = CanHaveProc
            Me.CanHaveMultiplicativeBuff = CanHaveMultiplicativeBuff
            sim = S
            Stat = Type
            Select Case Type
                Case sim.Stat.AP
                    _Name = "Attack Power"
                Case sim.Stat.Armor
                    _Name = "Armor"
                Case sim.Stat.ArP
                    _Name = "Armor Penetration Rating"
                Case sim.Stat.Crit
                    _Name = "Critical Stike Rating"
                Case sim.Stat.Expertise
                    _Name = "Expertise Rating"
                Case sim.Stat.Haste
                    _Name = "Haste Rating"
                Case sim.Stat.Hit
                    _Name = "Hit Rating"
                Case sim.Stat.Mastery
                    _Name = "Mastery Rating"
                Case sim.Stat.Strength
                    _Name = "Strength"
                Case sim.Stat.Agility
                    _Name = "Agility"
                Case sim.Stat.Intel
                    _Name = "Intelligence"
                Case sim.Stat.Stamina
                    _Name = "Stamina"
                Case Else
                    Diagnostics.Debug.WriteLine("WTF is this stat")
            End Select
        End Sub
        Overridable Sub AddMulti(ByVal i As Double)
            If i = 1 Then Exit Sub
            If CanHaveMultiplicativeBuff Then
                MultiplicativeBuff *= i
            Else
                Diagnostics.Debug.WriteLine("It's not supposed to be")
            End If
            CalculateCurrentValue()
        End Sub
        Overridable Sub RemoveMulti(ByVal i As Double)
            If CanHaveMultiplicativeBuff Then
                MultiplicativeBuff /= i
            Else
                Diagnostics.Debug.WriteLine("It's not supposed to be")
            End If
            CalculateCurrentValue()
        End Sub
        Overridable Sub Add(ByVal i As Integer)
            BaseValue += i
            CalculateCurrentValue()
        End Sub
        Overridable Sub Replace(ByVal i As Integer)
            BaseValue = i
            CalculateCurrentValue()
        End Sub

        Overridable Sub Remove(ByVal i As Integer)
            BaseValue -= i
            CalculateCurrentValue()
        End Sub
        Sub CalculateCurrentValue()
            TimeWasted.Start()


            CurrentValue = BaseValue * MultiplicativeBuff
            RaiseEvent ValueModifed()
            TimeWasted.Pause()
        End Sub
        Overridable Function Value() As Integer
            Return CurrentValue
        End Function
        Overridable Function MaxValue() As Integer
            TimeWasted.Start()
            Dim tmp As Integer
            tmp = (BaseValue + sim.proc.GetMaxPossibleBonus(Stat)) * MultiplicativeBuff * sim.proc.GetMaxMultiPlier(Stat)
            TimeWasted.Pause()
            Return tmp
        End Function
    End Class
    Class ArmorStat
        Inherits PrimaryStat
        Friend SpecialArmor As Integer

        Sub New(ByVal Type As Sim.Stat, ByVal S As Sim, Optional ByVal CanHaveProc As Boolean = False, Optional ByVal CanHaveMultiplicativeBuff As Boolean = False)
            MyBase.New(Type, S)
            _Name = "Armor"
        End Sub

        Public Overrides Function Value() As Integer
            Dim i As Integer = MyBase.Value()
            i = i + SpecialArmor
            Return i
        End Function

    End Class
End Namespace

