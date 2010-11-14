Imports System.Xml.Linq
Imports System.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.ComponentModel


Public Class Optimizer
    Inherits BackgroundWorker
    Enum SecondatyStat
        None
        CritRating
        MasteryRating
        HasteRating
        HitRating
        ExpertiseRating
    End Enum
    Dim SlotList As New List(Of Slot)
    Dim WiPEquipementSetList As New List(Of EquipementSet)
    Dim unFinishedSet As New List(Of EquipementSet)
    Dim FinishedSet As New List(Of EquipementSet)


    Event CalculationDone(ByVal EquipementSet)


    Sub New(ByVal ItemDB As XDocument, ByVal MainForm As MainForm, ByVal DW As Boolean)

        'Me.ReportProgress(0)
        Dim slt As Slot

        slt = New Slot("Head", MainForm.GearSelector.HeadSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.HeadSlot.Item.Id).First, SlotList)
        slt = New Slot("Neck", MainForm.GearSelector.NeckSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.NeckSlot.Item.Id).First, SlotList)
        slt = New Slot("Shoulder", MainForm.GearSelector.ShoulderSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.ShoulderSlot.Item.Id).First, SlotList)
        slt = New Slot("Back", MainForm.GearSelector.BackSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.BackSlot.Item.Id).First, SlotList)
        slt = New Slot("Chest", MainForm.GearSelector.ChestSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.ChestSlot.Item.Id).First, SlotList)
        slt = New Slot("Wrist", MainForm.GearSelector.WristSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.WristSlot.Item.Id).First, SlotList)
        slt = New Slot("Hand", MainForm.GearSelector.HandSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.HandSlot.Item.Id).First, SlotList)
        slt = New Slot("Waist", MainForm.GearSelector.BeltSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.BeltSlot.Item.Id).First, SlotList)
        slt = New Slot("Pant", MainForm.GearSelector.LegSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.LegSlot.Item.Id).First, SlotList)
        slt = New Slot("Feet", MainForm.GearSelector.FeetSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.FeetSlot.Item.Id).First, SlotList)
        slt = New Slot("Ring1", MainForm.GearSelector.ring1Slot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.ring1Slot.Item.Id).First, SlotList)
        slt = New Slot("Ring2", MainForm.GearSelector.ring2Slot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.ring2Slot.Item.Id).First, SlotList)
        slt = New Slot("Trinket1", MainForm.GearSelector.Trinket1Slot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.Trinket1Slot.Item.Id).First, SlotList)
        slt = New Slot("Trinket2", MainForm.GearSelector.Trinket2Slot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.Trinket2Slot.Item.Id).First, SlotList)
        slt = New Slot("Sigil", MainForm.GearSelector.Trinket2Slot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.SigilSlot.Item.Id).First, SlotList)
        If DW Then
            slt = New Slot("MainHand", MainForm.GearSelector.MHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.MHWeapSlot.Item.Id).First, SlotList)
            slt = New Slot("OffHand", MainForm.GearSelector.OHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.OHWeapSlot.Item.Id).First, SlotList)
        Else
            slt = New Slot("TwoHand", MainForm.GearSelector.TwoHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.TwoHWeapSlot.Item.Id).First, SlotList)
        End If
    End Sub
    Friend FinishedCount As Long

    Sub Populate()
        For Each s In SlotList
            s.GenerateVariation(True)
        Next

        Dim max As Long = 1
        Dim r As Integer
        For Each s In SlotList
            r = s.ItemList.Count
            max *= r
        Next



        Dim EPStatSet As New AllStat()

        Dim EQ As New EquipementSet(EPStatSet)
        'WiPEquipementSetList.Add(EQ)
        unFinishedSet.Add(EQ)
        Dim i As Integer = 1


        'For j = 0 To SlotList.Count - 1
        '    CloneAndAttach(SlotList.Item(j))
        '    i += 1
        'Next

        Me.ReportProgress(1)

        Dim k As Integer
        Dim l As Integer
ProcessList:
        Dim lst As List(Of EquipementSet)
        Do Until unFinishedSet.Count = 0 And WiPEquipementSetList.Count = 0
takeNext:
            If unFinishedSet.Count = 0 Then
                unFinishedSet = WiPEquipementSetList.ToList
                WiPEquipementSetList.Clear()

            End If
            If unFinishedSet.Count = 0 Then Exit Do
            Dim myEq As EquipementSet = unFinishedSet.Item(0)

            If myEq.ItemList.Count = SlotList.Count Then
                lst = (From e In unFinishedSet
                         Where e.ItemList.Count = SlotList.Count).ToList
                FinishedCount += lst.Count
                FinishedSet.AddRange(lst)
                For Each eqp As EquipementSet In lst
                    unFinishedSet.Remove(eqp)
                Next
                lst.Clear()
                GoTo takeNext
            End If
            i += 1

            WiPEquipementSetList.AddRange(myEq.CloneAddingThisSlot(SlotList.Item(myEq.ItemList.Count)))
            unFinishedSet.Remove(myEq)
            ' ProcessEquipementSetList()

         
processNext:
            If WiPEquipementSetList.Count > 0 Then
                Dim myLastEq As EquipementSet = WiPEquipementSetList.Last
                If myLastEq.ItemList.Count = SlotList.Count Then
                    FinishedSet.Add(myLastEq)
                    FinishedCount += 1
                Else
                    WiPEquipementSetList.AddRange(myLastEq.CloneAddingThisSlot(SlotList.Item(myLastEq.ItemList.Count)))
                End If
                WiPEquipementSetList.Remove(myLastEq)
                CleanupFinishedSet()

                l += 1
                If l = 1000 Then
                    Me.ReportProgress(1 + 100 * FinishedCount / max)
                    l = 0
                End If
                GoTo processNext
            End If


            CleanupFinishedSet()
            k += 1
            If k = 10000 Then
                CleanUpList()
                Me.ReportProgress(FinishedSet(0).EPValue)
                k = 0
            End If


        Loop
        CleanUpList()
        If unFinishedSet.Count > 0 Or WiPEquipementSetList.Count > 0 Then GoTo ProcessList
        FinishedSet = (From e In FinishedSet
                       Order By e.EPValue Descending).ToList


        Dim ret As New RunWorkerCompletedEventArgs(FinishedSet.First, Nothing, False)
        Me.OnRunWorkerCompleted(ret)

    End Sub



    Sub CleanupFinishedSet()

        If FinishedSet.Count > 100000 Then
            FinishedSet = (From e In FinishedSet
                            Order By e.EPValue Descending
                            Take 10
                            ).ToList
        End If

    End Sub

    Sub PutFinishedInTheRightList()
        Dim lst = (From e In WiPEquipementSetList Where e.ItemList.Count = SlotList.Count).ToList()

        If lst.Count > 0 Then
            FinishedCount += lst.Count
            FinishedSet.AddRange(lst)
            For Each eqp As EquipementSet In lst
                WiPEquipementSetList.Remove(eqp)
            Next

        End If


    End Sub

    Sub CleanUpList()
        PutFinishedInTheRightList()

        Dim buffer As Integer = 500

        CleanupFinishedSet()

        If WiPEquipementSetList.Count > buffer Then

            WiPEquipementSetList = (From e In WiPEquipementSetList
                                 Where e.ItemList.Count < SlotList.Count
                                              ).ToList

            unFinishedSet.AddRange((From e In WiPEquipementSetList
                                    Skip (buffer + 1)
                                    ).ToList)

            unFinishedSet = (From e In unFinishedSet
                             Order By e.ItemList.Count Descending).ToList

            WiPEquipementSetList = (From e In WiPEquipementSetList
                                 Take buffer
                                 ).ToList

            WiPEquipementSetList = (From e In WiPEquipementSetList
                                 Order By e.ItemList.Count Ascending
                                 ).ToList
        End If

    End Sub

    Sub ProcessEquipementSetList(Optional ByVal Stack = 0)
        If Stack > 5000 Then
            ' CleanUpList()
            Exit Sub
        End If

        If WiPEquipementSetList.Count > 0 Then
            Dim myEq As EquipementSet = WiPEquipementSetList.Last
            If myEq.ItemList.Count = SlotList.Count Then
                FinishedSet.Add(myEq)
                FinishedCount += 1
            Else
                WiPEquipementSetList.AddRange(myEq.CloneAddingThisSlot(SlotList.Item(myEq.ItemList.Count)))
            End If
            WiPEquipementSetList.Remove(myEq)
            ProcessEquipementSetList(Stack + 1)
        End If
    End Sub
    Sub CloneAndAttach(ByVal sl As Slot)
        Dim localEquipementSetList As New List(Of EquipementSet)
        Dim EquipementSetListToRemove As New List(Of EquipementSet)

        For Each EQSet In WiPEquipementSetList
            localEquipementSetList.AddRange(EQSet.CloneAddingThisSlot(sl))
            EQSet = Nothing
        Next
        WiPEquipementSetList.Clear()
        WiPEquipementSetList = localEquipementSetList
        Diagnostics.Debug.WriteLine("EquipementSet Count=" & WiPEquipementSetList.Count)
        CleanUpList()
    End Sub
    Class AllStat
        Friend EPStr As Stat
        Friend EPHast As Stat
        Friend EPCrit As Stat
        Friend EPHit As Stat
        Friend EPExp As Stat
        Friend EPMast As Stat

        Sub New()
            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Optimiser.xml", FileMode.Open, isoStore)
                    Dim doc As XDocument = XDocument.Load(isoStream)
                    EPStr = New Stat(doc.<EPValues>.<str>.<cap>.Value, doc.<EPValues>.<str>.<beforecap>.Value, doc.<EPValues>.<str>.<aftercap>.Value)
                    EPHast = New Stat(doc.<EPValues>.<haste>.<cap>.Value, doc.<EPValues>.<haste>.<beforecap>.Value, doc.<EPValues>.<haste>.<aftercap>.Value)
                    EPCrit = New Stat(doc.<EPValues>.<crit>.<cap>.Value, doc.<EPValues>.<crit>.<beforecap>.Value, doc.<EPValues>.<crit>.<aftercap>.Value)
                    EPHit = New Stat(doc.<EPValues>.<hit>.<cap>.Value, doc.<EPValues>.<hit>.<beforecap>.Value, doc.<EPValues>.<hit>.<aftercap>.Value)
                    EPExp = New Stat(doc.<EPValues>.<exp>.<cap>.Value, doc.<EPValues>.<exp>.<beforecap>.Value, doc.<EPValues>.<exp>.<aftercap>.Value)
                    EPMast = New Stat(doc.<EPValues>.<mast>.<cap>.Value, doc.<EPValues>.<mast>.<beforecap>.Value, doc.<EPValues>.<mast>.<aftercap>.Value)
                End Using
            End Using
        End Sub



        Class Stat
            Friend Cap As Integer
            Friend ValBeforeCap As Double
            Friend ValAfterCap As Double

            Sub New(ByVal Cap As Integer, ByVal BefCap As Double, ByVal AftCat As Double)
                Me.Cap = Cap
                ValBeforeCap = BefCap
                ValAfterCap = AftCat
            End Sub
            Sub New(ByVal value As Double)
                Me.Cap = 0
                ValBeforeCap = 0
                ValAfterCap = value
            End Sub

            Function GetValueFor(ByVal StatValue As Integer)
                Dim a As Integer
                Dim B As Integer
                a = Math.Min(Cap, StatValue)
                B = Math.Max(0, StatValue - Cap)
                Return (a * ValBeforeCap + B * ValAfterCap)
            End Function



        End Class

    End Class

    Class EquipementSet
        Inherits WowItem
        Dim EPStats As AllStat
        Friend ItemList As New List(Of WowItem)

        ReadOnly Property EPValue As Double
            Get
                Me.Strength = 0
                Me.HasteRating = 0
                Me.CritRating = 0
                Me.HitRating = 0
                Me.ExpertiseRating = 0
                Me.MasteryRating = 0
                For Each w In ItemList
                    Me.Strength += w.Strength
                    Me.HasteRating += w.HasteRating
                    Me.CritRating += w.CritRating
                    Me.HitRating += w.HitRating
                    Me.ExpertiseRating += w.ExpertiseRating
                    Me.MasteryRating += w.MasteryRating
                Next
                Dim i As Double
                i = EPStats.EPStr.GetValueFor(Strength) + EPStats.EPHast.GetValueFor(HasteRating) + EPStats.EPCrit.GetValueFor(CritRating) + EPStats.EPHit.GetValueFor(HitRating) + EPStats.EPExp.GetValueFor(ExpertiseRating) + EPStats.EPMast.GetValueFor(MasteryRating)
                Return i
            End Get
        End Property

        Sub Report()
            Dim t As String
            t = t & "	Me.Strength	" & Me.Strength & vbCrLf
            t = t & "	Me.HasteRating	" & Me.HasteRating & vbCrLf
            t = t & "	Me.CritRating	" & Me.CritRating & vbCrLf
            t = t & "	Me.HitRating	" & Me.HitRating & vbCrLf
            t = t & "	Me.ExpertiseRating	" & Me.ExpertiseRating & vbCrLf
            t = t & "	Me.MasteryRating	" & Me.MasteryRating & vbCrLf
            For Each itm In Me.ItemList
                t = t & itm.name & " " & itm.HitRating & vbCrLf
            Next


            Diagnostics.Debug.WriteLine(t)
        End Sub

        Sub New(ByVal EPStatSet As AllStat)
            EPStats = EPStatSet
        End Sub

        Shadows Function Clone() As EquipementSet
            Dim NewSet As New EquipementSet(EPStats)
            NewSet.ItemList.AddRange(ItemList)
            'For Each itm In ItemList
            '    NewSet.ItemList.Add(itm)
            'Next
            Return NewSet
        End Function

        Function CloneAddingThisSlot(ByVal sl As Slot) As List(Of EquipementSet)
            Dim localEquipementSetList As New List(Of EquipementSet)
            For Each Item In sl.ItemList
                Dim NewEQSet As EquipementSet
                NewEQSet = Me.Clone()
                NewEQSet.ItemList.Add(Item)
                localEquipementSetList.Add(NewEQSet)
            Next
            Return localEquipementSetList
        End Function

    End Class

    Class OptimizerWowItem
        Inherits WowItem

        Friend ReforgeFrom As SecondatyStat
        Friend Reforgeto As SecondatyStat
        Friend ReforgeValue As Integer

        Property ValueBeforeCap As Double
        Property ValueAfterCap As Double


        Function GetValueAfterCap(ByVal EPStats As AllStat) As Double

            Dim d As Double = 0
            d += Me.Strength * EPStats.EPStr.ValAfterCap
            d += Me.HitRating * EPStats.EPHit.ValAfterCap
            d += Me.CritRating * EPStats.EPCrit.ValAfterCap
            d += Me.ExpertiseRating * EPStats.EPExp.ValAfterCap
            d += Me.HasteRating * EPStats.EPHast.ValAfterCap
            d += Me.MasteryRating * EPStats.EPMast.ValAfterCap
            ValueAfterCap = d
            Return d

        End Function
        Function GetValueBeforeCap(ByVal EPStats As AllStat, Optional ByVal WithoutHit As Boolean = False, Optional ByVal WithoutExp As Boolean = False) As Double
            Dim d As Double = 0
            d += Me.Strength * EPStats.EPStr.ValBeforeCap
            If WithoutHit = False Then d += Me.HitRating * EPStats.EPHit.ValBeforeCap
            If WithoutExp = False Then d += Me.ExpertiseRating * EPStats.EPExp.ValBeforeCap
            d += Me.CritRating * EPStats.EPCrit.ValBeforeCap
            d += Me.HasteRating * EPStats.EPHast.ValBeforeCap
            d += Me.MasteryRating * EPStats.EPMast.ValBeforeCap
            ValueBeforeCap = d
            Return d
        End Function

        Shadows Function Clone() As OptimizerWowItem

            Dim w As New OptimizerWowItem
            w.Id = Id
            w.name = name
            w.ilvl = ilvl
            w.slot = slot
            w.classs = classs
            w.subclass = subclass
            w.heroic = heroic
            w.Strength = Strength
            w.Agility = Agility
            w.BonusArmor = BonusArmor
            w.Armor = Armor
            w.HasteRating = HasteRating
            w.ExpertiseRating = ExpertiseRating
            w.HitRating = HitRating
            w.AttackPower = AttackPower
            w.CritRating = CritRating
            w.ArmorPenetrationRating = ArmorPenetrationRating
            w.Speed = Speed
            w.DPS = DPS
            w.ReforgeFrom = ReforgeFrom
            w.Reforgeto = Reforgeto
            w.ReforgeValue = ReforgeValue

            Return w
        End Function


    End Class

    Class Slot
        Friend ItemList As New List(Of OptimizerWowItem)
        Friend name As String
        Dim id As String
        Dim OriginalItem As OptimizerWowItem
        Dim myItemXML As XElement
        Dim EchantAnGem As New WowItem


        Sub New(ByVal slotName As String, ByVal Vslot As VisualEquipSlot, ByVal xeL As XElement, ByVal List As List(Of Slot))
            name = slotName
            id = Vslot.Item.Id

            OriginalItem = New OptimizerWowItem
            myItemXML = xeL
            OriginalItem.Load(myItemXML)



            Dim w As WowItem = Vslot.Item.Enchant

            EchantAnGem.Strength += w.Strength
            EchantAnGem.HasteRating += w.HasteRating
            EchantAnGem.CritRating += w.CritRating
            EchantAnGem.HitRating += w.HitRating
            EchantAnGem.ExpertiseRating += w.ExpertiseRating
            EchantAnGem.MasteryRating += w.MasteryRating

            w = Vslot.Item.gem1

            EchantAnGem.Strength += w.Strength
            EchantAnGem.HasteRating += w.HasteRating
            EchantAnGem.CritRating += w.CritRating
            EchantAnGem.HitRating += w.HitRating
            EchantAnGem.ExpertiseRating += w.ExpertiseRating
            EchantAnGem.MasteryRating += w.MasteryRating

            w = Vslot.Item.gem2
            EchantAnGem.Strength += w.Strength
            EchantAnGem.HasteRating += w.HasteRating
            EchantAnGem.CritRating += w.CritRating
            EchantAnGem.HitRating += w.HitRating
            EchantAnGem.ExpertiseRating += w.ExpertiseRating
            EchantAnGem.MasteryRating += w.MasteryRating

            w = Vslot.Item.gem3
            EchantAnGem.Strength += w.Strength
            EchantAnGem.HasteRating += w.HasteRating
            EchantAnGem.CritRating += w.CritRating
            EchantAnGem.HitRating += w.HitRating
            EchantAnGem.ExpertiseRating += w.ExpertiseRating
            EchantAnGem.MasteryRating += w.MasteryRating


            If Vslot.Item.IsGembonusActif() Then

            End If



            List.Add(Me)
        End Sub

        Function GetTheHighestStattoReforge(ByVal EPs As AllStat) As SecondatyStat
            Dim tmp As SecondatyStat = Nothing
            Dim prevResult As Double = 0
            If OriginalItem.CritRating = 0 Then
                If prevResult < SecondatyStat.CritRating * EPs.EPCrit.ValBeforeCap Then
                    prevResult = SecondatyStat.CritRating * EPs.EPCrit.ValBeforeCap
                    tmp = SecondatyStat.CritRating
                End If
            End If

            If OriginalItem.HasteRating = 0 Then
                If prevResult < SecondatyStat.HasteRating * EPs.EPHast.ValBeforeCap Then
                    prevResult = SecondatyStat.HasteRating * EPs.EPHast.ValBeforeCap
                    tmp = SecondatyStat.HasteRating
                End If

            End If

            If OriginalItem.MasteryRating = 0 Then
                If prevResult < SecondatyStat.MasteryRating * EPs.EPMast.ValBeforeCap Then
                    prevResult = SecondatyStat.MasteryRating * EPs.EPMast.ValBeforeCap
                    tmp = SecondatyStat.MasteryRating
                End If
            End If
            If prevResult <> 0 Then
                Return tmp
            Else
                Return Nothing
            End If
        End Function

        Function GetTheLowestStattoReforge(ByVal EPs As AllStat) As SecondatyStat
            Dim tmp As SecondatyStat = Nothing
            Dim prevResult As Double = Double.MaxValue
            If OriginalItem.CritRating <> 0 Then
                If prevResult > SecondatyStat.CritRating * EPs.EPCrit.ValBeforeCap Then
                    prevResult = SecondatyStat.CritRating * EPs.EPCrit.ValBeforeCap
                    tmp = SecondatyStat.CritRating
                End If
            End If

            If OriginalItem.HasteRating <> 0 Then
                If prevResult > SecondatyStat.HasteRating * EPs.EPHast.ValBeforeCap Then
                    prevResult = SecondatyStat.HasteRating * EPs.EPHast.ValBeforeCap
                    tmp = SecondatyStat.HasteRating
                End If

            End If

            If OriginalItem.MasteryRating <> 0 Then
                If prevResult > SecondatyStat.MasteryRating * EPs.EPMast.ValBeforeCap Then
                    prevResult = SecondatyStat.MasteryRating * EPs.EPMast.ValBeforeCap
                    tmp = SecondatyStat.MasteryRating
                End If
            End If
            If prevResult <> Double.MaxValue Then
                Return tmp
            Else
                Return Nothing
            End If

        End Function


        Sub GenerateVariation(Optional ByVal ForHiTExpOnly As Boolean = False)
            ItemList.Add(OriginalItem)


            If OriginalItem.CritRating <> 0 Then
                Dim Itm As OptimizerWowItem = OriginalItem.Clone
                Dim Ref As Integer = GetReforgeValue(SecondatyStat.CritRating, Itm)
                Itm.ReforgeFrom = SecondatyStat.CritRating
                Itm.ReforgeValue = Ref
                Itm.CritRating -= Ref
                ItemList.AddRange(GenerateVariationForThisStat(Itm, Ref))
            End If

            If OriginalItem.MasteryRating <> 0 Then
                Dim Itm As OptimizerWowItem = OriginalItem.Clone
                Dim Ref As Integer = GetReforgeValue(SecondatyStat.MasteryRating, Itm)
                Itm.ReforgeFrom = SecondatyStat.MasteryRating
                Itm.ReforgeValue = Ref
                Itm.MasteryRating -= Ref
                ItemList.AddRange(GenerateVariationForThisStat(Itm, Ref))
            End If

            If OriginalItem.HasteRating <> 0 Then
                Dim Itm As OptimizerWowItem = OriginalItem.Clone
                Dim Ref As Integer = GetReforgeValue(SecondatyStat.HasteRating, Itm)
                Itm.ReforgeFrom = SecondatyStat.HasteRating
                Itm.ReforgeValue = Ref
                Itm.HasteRating -= Ref
                ItemList.AddRange(GenerateVariationForThisStat(Itm, Ref))
            End If

            If OriginalItem.HitRating <> 0 Then
                Dim Itm As OptimizerWowItem = OriginalItem.Clone
                Dim Ref As Integer = GetReforgeValue(SecondatyStat.HitRating, Itm)
                Itm.ReforgeFrom = SecondatyStat.HitRating
                Itm.ReforgeValue = Ref
                Itm.HitRating -= Ref
                ItemList.AddRange(GenerateVariationForThisStat(Itm, Ref))
            End If
            If OriginalItem.ExpertiseRating <> 0 Then
                Dim Itm As OptimizerWowItem = OriginalItem.Clone
                Dim Ref As Integer = GetReforgeValue(SecondatyStat.ExpertiseRating, Itm)
                Itm.ReforgeFrom = SecondatyStat.ExpertiseRating
                Itm.ReforgeValue = Ref
                Itm.ExpertiseRating -= Ref
                ItemList.AddRange(GenerateVariationForThisStat(Itm, Ref))
            End If
            Dim EPs As New AllStat
            Dim d1 = OriginalItem.GetValueAfterCap(EPs)
            Dim d2 = OriginalItem.GetValueBeforeCap(EPs)

            ' get WeakestStatValue
            Dim CalcWithoutExp As Boolean = True
            Dim WStat As Double = Math.Min(EPs.EPCrit.ValBeforeCap, EPs.EPMast.ValBeforeCap)
            WStat = Math.Min(EPs.EPHast.ValBeforeCap, WStat)
            If EPs.EPExp.ValBeforeCap < WStat Then
                'get rid of exp
                ItemList = (From e In ItemList Where e.Reforgeto <> SecondatyStat.ExpertiseRating).ToList
            Else
                CalcWithoutExp = False
            End If
            ItemList = (From e In ItemList
            Order By e.GetValueBeforeCap(EPs) Descending
                ).ToList

            Dim f = (From e In ItemList
                Where e.GetValueBeforeCap(EPs) >= d2
                ).ToList



            If EPs.EPHast.ValBeforeCap > Math.Min(EPs.EPCrit.ValBeforeCap, EPs.EPMast.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.ReforgeFrom <> SecondatyStat.HasteRating).ToList
            End If

            If EPs.EPCrit.ValBeforeCap > Math.Min(EPs.EPHast.ValBeforeCap, EPs.EPMast.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.ReforgeFrom <> SecondatyStat.CritRating).ToList
            End If

            If EPs.EPMast.ValBeforeCap > Math.Min(EPs.EPHast.ValBeforeCap, EPs.EPCrit.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.ReforgeFrom <> SecondatyStat.MasteryRating).ToList
            End If

            If EPs.EPHast.ValBeforeCap < Math.Max(EPs.EPCrit.ValBeforeCap, EPs.EPMast.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.Reforgeto <> SecondatyStat.HasteRating).ToList
            End If

            If EPs.EPCrit.ValBeforeCap < Math.Max(EPs.EPHast.ValBeforeCap, EPs.EPMast.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.Reforgeto <> SecondatyStat.CritRating).ToList
            End If

            If EPs.EPMast.ValBeforeCap < Math.Max(EPs.EPHast.ValBeforeCap, EPs.EPCrit.ValBeforeCap) Then
                ItemList = (From e In ItemList Where e.Reforgeto <> SecondatyStat.MasteryRating).ToList
            End If

            d2 = OriginalItem.GetValueBeforeCap(EPs, True, CalcWithoutExp)
            Dim List2 As List(Of OptimizerWowItem)
            'take the best one only
            If CalcWithoutExp Then
                List2 = (From e In ItemList Where
                         e.ReforgeFrom <> SecondatyStat.HitRating AndAlso e.Reforgeto <> SecondatyStat.HitRating
                        Order By e.GetValueBeforeCap(EPs) Descending
                        ).ToList

            Else
                List2 = (From e In ItemList Where
                         e.ReforgeFrom <> SecondatyStat.ExpertiseRating AndAlso e.Reforgeto <> SecondatyStat.ExpertiseRating _
                         AndAlso e.ReforgeFrom <> SecondatyStat.HitRating AndAlso e.Reforgeto <> SecondatyStat.HitRating
                        Order By e.GetValueBeforeCap(EPs) Descending
                        ).ToList
            End If

            If List2.Count > 1 Then
                For i As Integer = 1 To List2.Count - 1
                    ItemList.Remove(List2(i))
                Next
            End If


            List2 = (From e In ItemList Where
                         e.ReforgeFrom = SecondatyStat.HitRating
                        Order By e.GetValueBeforeCap(EPs) Descending
                        ).ToList

            If List2.Count > 1 Then
                For i As Integer = 1 To List2.Count - 1
                    ItemList.Remove(List2(i))
                Next
            End If
            If Not CalcWithoutExp Then
                List2 = (From e In ItemList Where
                        e.ReforgeFrom = SecondatyStat.ExpertiseRating
                       Order By e.GetValueBeforeCap(EPs) Descending
                       ).ToList

                If List2.Count > 1 Then
                    For i As Integer = 1 To List2.Count - 1
                        ItemList.Remove(List2(i))
                    Next
                End If
            End If


            'For Each e In List2

            '    If e.Reforgeto <> SecondatyStat.HitRating AndAlso e.ReforgeFrom <> SecondatyStat.HitRating Then
            '        If CalcWithoutExp Then
            '            ItemList.Remove(e)
            '        Else
            '            If e.Reforgeto <> SecondatyStat.ExpertiseRating AndAlso e.ReforgeFrom <> SecondatyStat.ExpertiseRating Then
            '                ItemList.Remove(e)
            '            End If
            '        End If
            '    End If
            'Next


            'If ForHiTExpOnly Then
            '    If EPs.EPExp.ValBeforeCap < (Math.Min(EPs.EPCrit.ValBeforeCap, EPs.EPMast.ValBeforeCap)) Then
            '        ItemList = (From e In ItemList
            '                    Where (e.Reforgeto <> SecondatyStat.ExpertiseRating AndAlso (e.HitRating <> 0)) OrElse e.ReforgeFrom = e.Reforgeto
            '                    ).ToList
            '    Else
            '        ItemList = (From e In ItemList
            '                    Where (e.ExpertiseRating <> 0 OrElse e.HitRating <> 0) OrElse e.ReforgeFrom = e.Reforgeto
            '                    ).ToList
            '    End If
            'Else

            '    Dim ValAfterCapList = (From i In ItemList
            '                       Where i.GetValueAfterCap(EPs) >= d1 OrElse i.GetValueBeforeCap(EPs) >= d2
            '                       Order By i.GetValueAfterCap(EPs) Descending).ToList
            '    ItemList = ValAfterCapList.ToList
            'End If

            For Each itm In Me.ItemList
                itm.Strength += EchantAnGem.Strength
                itm.HasteRating += EchantAnGem.HasteRating
                itm.CritRating += EchantAnGem.CritRating
                itm.HitRating += EchantAnGem.HitRating
                itm.ExpertiseRating += EchantAnGem.ExpertiseRating
                itm.MasteryRating += EchantAnGem.MasteryRating
            Next
            EchantAnGem = Nothing





        End Sub

        Function GenerateVariationForThisStat(ByVal item As OptimizerWowItem, ByVal Value As Integer) As List(Of OptimizerWowItem)
            Dim lst As New List(Of OptimizerWowItem)
            Dim NewItem As OptimizerWowItem
            If item.HitRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.HitRating
                NewItem.HitRating += Value
                lst.Add(NewItem)
            End If
            If item.ExpertiseRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.ExpertiseRating
                NewItem.ExpertiseRating += Value
                lst.Add(NewItem)
            End If

            If item.HasteRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.HasteRating
                NewItem.HasteRating += Value
                lst.Add(NewItem)
            End If


            If item.CritRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.CritRating
                NewItem.CritRating += Value
                lst.Add(NewItem)
            End If



            If item.MasteryRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.MasteryRating
                NewItem.MasteryRating += Value
                lst.Add(NewItem)
            End If

            Return lst


        End Function

        Function GetReforgeValue(ByVal stat As SecondatyStat, ByVal item As WowItem) As Integer
            Select Case stat
                Case SecondatyStat.CritRating
                    Return Decimal.Truncate(item.CritRating * 0.4)
                Case SecondatyStat.ExpertiseRating
                    Return Decimal.Truncate(item.ExpertiseRating * 0.4)
                Case SecondatyStat.HasteRating
                    Return Decimal.Truncate(item.HasteRating * 0.4)
                Case SecondatyStat.HitRating
                    Return Decimal.Truncate(item.HitRating * 0.4)
                Case SecondatyStat.MasteryRating
                    Return Decimal.Truncate(item.MasteryRating * 0.4)
                Case Else
                    Return 0
            End Select
        End Function


    End Class

End Class
