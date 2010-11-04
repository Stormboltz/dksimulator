Imports System.Xml.Linq
Imports System.Linq

Public Class Optimizer

    Enum SecondatyStat
        None
        CritRating
        MasteryRating
        HasteRating
        HitRating
        ExpertiseRating
    End Enum
    Dim SlotList As New List(Of Slot)
    Dim EquipementSetList As New List(Of EquipementSet)
    Dim ParentForm As MainForm


    Sub New(ByVal ItemDB As XDocument, ByVal MainForm As MainForm)
        Dim slt As Slot
        ParentForm = MainForm
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
        If MainForm.GearSelector.rdDW.IsChecked Then
            slt = New Slot("MainHand", MainForm.GearSelector.MHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.MHWeapSlot.Item.Id).First, SlotList)
            slt = New Slot("OffHand", MainForm.GearSelector.OHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.OHWeapSlot.Item.Id).First, SlotList)
        Else
            slt = New Slot("TwoHand", MainForm.GearSelector.TwoHWeapSlot, (From el In ItemDB.<items>.Elements Where el.<id>.Value = MainForm.GearSelector.TwoHWeapSlot.Item.Id).First, SlotList)
        End If
    End Sub

    Function Populate() As EquipementSet
        For Each s In SlotList
            s.GenerateVariation()
        Next
        Dim EPStatSet As New AllStat(ParentForm)

        Dim EQ As New EquipementSet(EPStatSet)
        EquipementSetList.Add(EQ)
        Dim i As Integer = 1
        For Each s In SlotList
            CloneAndAttach(s, i)
            i += 1
        Next
        Return EquipementSetList.Item(0)
    End Function

    Sub CloneAndAttach(ByVal sl As Slot, ByVal expected As Integer)
        Dim localEquipementSetList As New List(Of EquipementSet)
        Dim EquipementSetListToRemove As New List(Of EquipementSet)
        For Each itm In sl.ItemList
            For Each EQSet In EquipementSetList
                Dim NewEQSet As EquipementSet
                NewEQSet = EQSet.Clone()
                NewEQSet.ItemList.Add(itm)
                localEquipementSetList.Add(NewEQSet)
                'EquipementSetListToRemove.Add(EQSet)
            Next
        Next
        For Each e In EquipementSetList
            For Each i In e.ItemList
                i = Nothing
            Next
            e = Nothing
        Next
        For Each w In EquipementSetList
            w.ItemList.Clear()
            w = Nothing
        Next
        EquipementSetList.Clear()


        EquipementSetList = localEquipementSetList

        'EquipementSetList.AddRange(localEquipementSetList)
        'EquipementSetList = (From e In EquipementSetList
        '              Where e.ItemList.Count = expected).ToList

        'For Each e In EquipementSetListToRemove
        '    e.ItemList.Clear()
        '    e = Nothing
        'Next
        Diagnostics.Debug.WriteLine("EquipementSet Count=" & EquipementSetList.Count)

        If EquipementSetList.Count > 100000 Then
            ' clean the lowest EP to release memory
            EquipementSetList = (From e In EquipementSetList
                                 Order By e.EPValue Descending
                                              ).ToList
            'Dim ItemTocleanList = (From e In EquipementSetList
            '                      Skip (1 + EquipementSetList.Count / 2)
            '                      ).ToList
            'For Each w In ItemTocleanList
            '    w = Nothing
            'Next
            'ItemTocleanList.Clear()
            'ItemTocleanList = Nothing
            EquipementSetList = (From e In EquipementSetList
                                 Take 100000
                                  ).ToList
        End If


    End Sub
    Class AllStat
        Friend EPStr As Stat
        Friend EPHast As Stat
        Friend EPCrit As Stat
        Friend EPHit As Stat
        Friend EPExp As Stat
        Friend EPMast As Stat

        Sub New(ByVal MF As MainForm)
            EPStr = New Stat(MF.txtStrC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                             MF.txtStrBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                                         MF.txtStrAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            EPHast = New Stat(MF.txtHasteC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                              MF.txtHasteBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                              MF.txtHasteAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            EPCrit = New Stat(MF.txtCritC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                              MF.txtCritBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                              MF.txtCritAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))


            EPHit = New Stat(MF.txtHitC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                             MF.txtHitBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                             MF.txtHitAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            EPExp = New Stat(MF.txtExpC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                             MF.txtExpBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                             MF.txtExpAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            EPMast = New Stat(MF.txtMastC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                            MF.txtMastBC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), _
                            MF.txtMastAC.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))


        End Sub



        Class Stat
            Dim Cap As Integer
            Dim ValBeforeCap As Double
            Dim ValAfterCap As Double

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



    End Class


    Class OptimizerWowItem
        Inherits WowItem

        Friend ReforgeFrom As SecondatyStat
        Friend Reforgeto As SecondatyStat
        Friend ReforgeValue As Integer

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

        Sub GenerateVariation()
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

            If item.CritRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.CritRating
                NewItem.CritRating += Value
                lst.Add(NewItem)
            End If
            If item.HasteRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.HasteRating
                NewItem.HasteRating += Value
                lst.Add(NewItem)
            End If
            If item.MasteryRating = 0 Then
                NewItem = item.Clone
                NewItem.Reforgeto = SecondatyStat.MasteryRating
                NewItem.MasteryRating += Value
                lst.Add(NewItem)
            End If
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
