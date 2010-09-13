Namespace Simulator.Character
    Public Class Stat
        Inherits SimObjet


        Friend AdditiveBuff As Integer
        Friend MultiplicativeBuff As Double = 1
        Friend CanHaveMultiplicativeBuff As Boolean
        Friend CanHaveProc As Boolean
        Private Stat As Sim.Stat
        Friend BaseValue As Integer





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
                    _Name = "Itelligence"
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
        End Sub
        Overridable Sub RemoveMulti(ByVal i As Double)
            If CanHaveMultiplicativeBuff Then
                MultiplicativeBuff /= i
            Else
                Diagnostics.Debug.WriteLine("It's not supposed to be")
            End If

        End Sub
        Overridable Sub Add(ByVal i As Integer)
            BaseValue += i
        End Sub
        Overridable Sub Remove(ByVal i As Integer)
            BaseValue -= i
        End Sub

        Overridable Function Value() As Integer
            TimeWasted.Start()
            Dim tmp As Integer
            If AdditiveBuff <> 0 Then
                BaseValue += AdditiveBuff
                AdditiveBuff = 0
            End If
            tmp = BaseValue * MultiplicativeBuff
            TimeWasted.Pause()
            Return tmp
        End Function
        Overridable Function MaxValue() As Integer
            TimeWasted.Start()
            Dim tmp As Integer
            If AdditiveBuff <> 0 Then
                BaseValue += AdditiveBuff
                AdditiveBuff = 0
            End If
            tmp = (BaseValue + sim.proc.GetMaxPossibleBonus(Stat)) * MultiplicativeBuff * sim.proc.GetMaxMultiPlier(Stat)
            TimeWasted.Pause()
            Return tmp
        End Function

    End Class

    Class ArmorStat
        Inherits Stat
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

