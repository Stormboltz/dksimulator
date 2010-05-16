Imports System.Xml.Linq

'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class MainStat
    Friend MHWeaponDPS As Double
    Friend MHWeaponSpeed As Double
    Friend OHWeaponDPS As Double
    Friend OHWeaponSpeed As Double
    Friend BossArmor As Integer



    Private character As Character
    Private XmlCharacter As XDocument

    Friend T72PDPS As Integer
    Friend T74PDPS As Integer
    Friend T72PTNK As Integer
    Friend T74PTNK As Integer

    Friend T82PDPS As Integer
    Friend T84PDPS As Integer
    Friend T82PTNK As Integer
    Friend T84PTNK As Integer


    Friend T92PDPS As Integer
    Friend T94PDPS As Integer

    Friend T102PDPS As Integer
    Friend T104PDPS As Integer

    Friend T92PTNK As Integer
    Friend T102PTNK As Integer



    Private _Mitigation As Double
    Private _LastArP As Double


    Protected Sim As Sim
    Private _MaxAp As Integer




    Friend CSD As Integer


    Function DualW() As Boolean
        Return character.Dual
    End Function

    Sub New(ByVal S As Sim)
        Sim = S

        _Mitigation = 0
        _LastArP = 0


        'On Error Resume Next
        character = Sim.Character

        XmlCharacter = character.XmlDoc


        Try
            MHWeaponDPS = (XmlCharacter.Element("character").Element("weapon").Element("mainhand").Element("dps").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            If Sim.EPStat = "EP WeaponDPS" Then
                MHWeaponDPS = MHWeaponDPS + 10
            End If

            If InStr(Sim.EPStat, "ScaDPSA") Then
                MHWeaponDPS += Replace(Sim.EPStat, "ScaDPSA", "")
            End If


            MHWeaponSpeed = (XmlCharacter.Element("character").Element("weapon").Element("mainhand").Element("speed").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            If Sim.EPStat = "EP WeaponSpeed" Then
                MHWeaponSpeed = MHWeaponSpeed + 0.1
            End If
        Catch
            msgBox("Error reading MH Weapon characteristics")
        End Try

        Try
            OHWeaponDPS = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("dps").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            OHWeaponSpeed = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("speed").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        Catch
            Diagnostics.Debug.WriteLine("Error reading OH Weapon characteristics")
        End Try
        BossArmor = 10643

        If XmlCharacter.Element("character").Element("misc").Element("ChaoticSkyflareDiamond").Value = True Then
            CSD = 1
        Else
            CSD = 0
        End If

        'Trinkets
        Sim.Trinkets = New Trinkets(Sim)



        Try
            Select Case Sim._EPStat
                Case "EP NoTrinket"
                Case "EP AttackPowerNoTrinket"
                Case "EP MjolnirRunestone"
                    Sim.Trinkets.MjolRune.Equip()
                Case "EP GrimToll"
                    Sim.Trinkets.GrimToll.Equip()
                Case "EP BitterAnguish"
                    Sim.Trinkets.BitterAnguish.Equip()
                Case "EP Mirror"
                    Sim.Trinkets.Mirror.Equip()
                Case "EP Greatness"
                    Sim.Trinkets.Greatness.Equip()
                Case "EP DCDeath"
                    Sim.Trinkets.DCDeath.Equip()
                Case "EP Victory"
                    Sim.Trinkets.Victory.Equip()
                Case "EP Necromantic"
                    Sim.Trinkets.Necromantic.Equip()
                Case "EP Bandit"
                    Sim.Trinkets.Bandit.Equip()
                Case "EP Pyrite"
                    Sim.Trinkets.Pyrite.Equip()
                Case "EP DarkMatter"
                    Sim.Trinkets.DarkMatter.Equip()
                Case "EP OldGod"
                    Sim.Trinkets.OldGod.Equip()
                Case "EP Comet"
                    Sim.Trinkets.Comet.Equip()
                Case "EP DeathChoice"
                    Sim.Trinkets.DeathChoice.Equip()
                Case "EP DeathChoiceHeroic"
                    Sim.Trinkets.DeathChoiceHeroic.Equip()
                Case "EP DeathbringersWill"
                    Sim.Trinkets.DeathbringersWill.Equip()
                Case "EP TinyAbomination"
                    Sim.Trinkets.TinyAbomination.Equip()
                Case "EP DeathbringersWillHeroic"
                    Sim.Trinkets.DeathbringersWillHeroic.Equip()
                Case "EP WhisperingFangedSkull"
                    Sim.Trinkets.WhisperingFangedSkull.Equip()
                Case "EP WhisperingFangedSkullHeroic"
                    Sim.Trinkets.WhisperingFangedSkullHeroic.Equip()
                Case "EP NeedleEncrustedScorpion"
                    Sim.Trinkets.NeedleEncrustedScorpion.Equip()
                Case "EP HerkumlWarToken"
                    Sim.Trinkets.HerkumlWarToken.Equip()
                Case "EP MarkofSupremacy"
                    Sim.Trinkets.MarkofSupremacy.Equip()
                Case "EP VengeanceoftheForsaken"
                    Sim.Trinkets.VengeanceoftheForsaken.Equip()
                Case "EP VengeanceoftheForsakenHeroic"
                    Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip()



                Case Else
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("MjolnirRunestone").Value = 1 Then Sim.Trinkets.MjolRune.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("GrimToll").Value = 1 Then Sim.Trinkets.GrimToll.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("BitterAnguish").Value = 1 Then Sim.Trinkets.BitterAnguish.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Mirror").Value = 1 Then Sim.Trinkets.Mirror.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Greatness").Value = 1 Then Sim.Trinkets.Greatness.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DCDeath").Value = 1 Then Sim.Trinkets.DCDeath.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Victory").Value = 1 Then Sim.Trinkets.Victory.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Necromantic").Value = 1 Then Sim.Trinkets.Necromantic.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Bandit").Value = 1 Then Sim.Trinkets.Bandit.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Pyrite").Value = 1 Then Sim.Trinkets.Pyrite.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DarkMatter").Value = 1 Then Sim.Trinkets.DarkMatter.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("OldGod").Value = 1 Then Sim.Trinkets.OldGod.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("Comet").Value = 1 Then Sim.Trinkets.Comet.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DeathChoice").Value = 1 Then Sim.Trinkets.DeathChoice.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DeathChoiceHeroic").Value = 1 Then Sim.Trinkets.DeathChoiceHeroic.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DeathbringersWill").Value = 1 Then Sim.Trinkets.DeathbringersWill.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("DeathbringersWillHeroic").Value = 1 Then Sim.Trinkets.DeathbringersWillHeroic.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("WhisperingFangedSkull").Value = 1 Then Sim.Trinkets.WhisperingFangedSkull.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("WhisperingFangedSkullHeroic").Value = 1 Then Sim.Trinkets.WhisperingFangedSkullHeroic.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("NeedleEncrustedScorpion").Value = 1 Then Sim.Trinkets.NeedleEncrustedScorpion.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("TinyAbomination").Value = 1 Then Sim.Trinkets.TinyAbomination.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("HerkumlWarToken").Value = 1 Then Sim.Trinkets.HerkumlWarToken.Equip()
                    Catch
                    End Try
                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("MarkofSupremacy").Value = 1 Then Sim.Trinkets.MarkofSupremacy.Equip()
                    Catch
                    End Try

                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("VengeanceoftheForsaken").Value = 1 Then Sim.Trinkets.VengeanceoftheForsaken.Equip()
                    Catch
                    End Try

                    Try
                        If XmlCharacter.Element("character").Element("trinket").Element("VengeanceoftheForsakenHeroic").Value = 1 Then Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip()
                    Catch
                    End Try



            End Select
        Catch
            Diagnostics.Debug.WriteLine("ERROR init trinket")
        End Try

        T72PDPS = 0
        T74PDPS = 0
        T82PDPS = 0
        T84PDPS = 0
        T92PDPS = 0
        T94PDPS = 0
        T102PDPS = 0
        T104PDPS = 0


        Select Case Sim._EPStat
            Case "EP 0T7"
            Case "EP AttackPower0T7"
            Case "EP 2T7"
                T72PDPS = 1
            Case "EP 4T7"
                T74PDPS = 1
            Case "EP 2T8"
                T82PDPS = 1
            Case "EP 4T8"
                T84PDPS = 1
            Case "EP 2T9"
                T92PDPS = 1
            Case "EP 4T9"
                T94PDPS = 1
            Case "EP 2T10"
                T102PDPS = 1
            Case "EP 4T10"
                T104PDPS = 1
            Case Else
                Try
                    T72PDPS = XmlCharacter.Element("character").Element("Set").Element("T72PDPS").Value
                Catch
                End Try
                Try
                    T74PDPS = XmlCharacter.Element("character").Element("Set").Element("T74PDPS").Value
                Catch
                End Try
                Try
                    T82PDPS = XmlCharacter.Element("character").Element("Set").Element("T82PDPS").Value
                Catch
                End Try
                Try
                    T84PDPS = XmlCharacter.Element("character").Element("Set").Element("T84PDPS").Value
                Catch
                End Try
                Try

                    T92PDPS = XmlCharacter.Element("character").Element("Set").Element("T92PDPS").Value
                Catch
                End Try
                Try
                    T94PDPS = XmlCharacter.Element("character").Element("Set").Element("T94PDPS").Value
                Catch
                End Try
                Try

                    T72PTNK = XmlCharacter.Element("character").Element("Set").Element("T72PTNK").Value
                Catch
                End Try
                Try
                    T74PTNK = XmlCharacter.Element("character").Element("Set").Element("T74PTNK").Value
                Catch
                End Try
                Try
                    T82PTNK = XmlCharacter.Element("character").Element("Set").Element("T82PTNK").Value
                Catch
                End Try
                Try
                    T84PTNK = XmlCharacter.Element("character").Element("Set").Element("T84PTNK").Value
                Catch
                End Try
                Try
                    T102PDPS = XmlCharacter.Element("character").Element("Set").Element("T102PDPS").Value
                Catch
                End Try
                Try
                    T104PDPS = XmlCharacter.Element("character").Element("Set").Element("T104PDPS").Value
                Catch
                End Try
                Try

                    T92PTNK = XmlCharacter.Element("character").Element("Set").Element("T92PTNK").Value
                Catch
                End Try
                Try
                    T102PTNK = XmlCharacter.Element("character").Element("Set").Element("T102PTNK").Value
                Catch
                End Try

        End Select


    End Sub

    Function BaseAP() As Integer
        Dim tmp As Integer
        tmp += Sim.proc.GetActiveBonus("ap")
        tmp = tmp + character.AttackPower
        tmp = tmp + character.Strength * 2
        tmp = tmp + 550
        tmp = tmp * (1 + Sim.Character.Buff.AttackPowerPc / 10)
        Return tmp
    End Function

    Function GetMaxAP() As Integer
        Dim tmp As Integer
        If _MaxAp <> 0 Then Return _MaxAp
        tmp += Sim.proc.GetMaxPossibleBonus("ap")
        tmp += character.AttackPower
        tmp += character.MaxStrength * 2
        tmp += 550
        tmp = tmp * (1 + Sim.Character.Buff.AttackPowerPc / 10)
        _MaxAp = tmp
        Return tmp
    End Function



    Function AP() As Integer
        Return BaseAP()
    End Function

    Function crit(Optional ByVal target As Targets.Target = Nothing) As System.Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = tmp + character.CritRating / 45.91
        tmp = tmp + character.Agility * 0.016
        tmp = tmp + 5 * Sim.Character.Buff.MeleeCrit
        tmp = tmp + 3 * target.Debuff.CritChanceTaken
        tmp = tmp + Sim.Character.TalentBlood.Darkconv
        tmp = tmp + Sim.Character.TalentUnholy.EbonPlaguebringer
        tmp = tmp + Sim.Character.TalentFrost.Annihilation
        tmp = tmp - 4.8 'Crit malus vs bosses

        Return tmp / 100
    End Function
    Function critAutoattack(Optional ByVal target As Targets.Target = Nothing) As System.Double 'No Annihilation for autoattacks
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = tmp + character.CritRating / 45.91
        tmp = tmp + character.Agility / 62.5
        tmp = tmp + 5 * Sim.Character.Buff.MeleeCrit
        tmp = tmp + 3 * target.Debuff.CritChanceTaken
        tmp = tmp + Sim.Character.TalentBlood.Darkconv
        tmp = tmp + Sim.Character.TalentUnholy.EbonPlaguebringer
        tmp = tmp - 4.8 'Crit malus vs bosses

        Return tmp / 100
    End Function
    Function SpellCrit(Optional ByVal target As Targets.Target = Nothing) As Single
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = character.SpellCritRating / 45.91
        tmp = tmp + 3 * target.Debuff.CritChanceTaken
        tmp = tmp + 5 * Sim.Character.Buff.SpellCrit
        tmp = tmp + 5 * target.Debuff.SpellCritTaken
        tmp = tmp + Sim.Character.TalentBlood.Darkconv
        tmp = tmp + Sim.Character.TalentUnholy.EbonPlaguebringer
        tmp = tmp - 2.1 'Spell crit malus vs bosses

        Return tmp / 100
    End Function

    Function Haste() As Double
        Dim tmp As Double
        tmp = 1 + character.HasteRating / (25.22) / 100 '1.3 is the buff haste rating received
        tmp = tmp * (1 + Sim.UnholyPresence * 0.15)
        If Sim.Character.TalentFrost.ImprovedIcyTalons Then tmp = tmp * 1.05
        If Sim.proc.IcyTalons.IsActive Then tmp = tmp * (1 + 0.04 * Sim.proc.IcyTalons.ProcValue)
        If Sim.Character.Buff.MeleeHaste Then tmp = tmp * 1.2
        If Sim.Character.Buff.Haste Then tmp = tmp * 1.03
        If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
        If Sim.proc.TrollRacial.IsActive Then tmp = tmp * 1.2
        Return tmp
    End Function
    Function SpellHaste() As Double
        Dim tmp As Double
        tmp = 1 + character.SpellHasteRating / 25.22 / 100
        If Sim.Character.Buff.SpellHaste Then tmp = tmp * 1.05
        If Sim.Character.Buff.Haste Then tmp = tmp * 1.03
        If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
        Return tmp
    End Function
    Function EstimatedHasteBonus() As Double
        Dim tmp As Double
        tmp = 1 + (character.HasteRating + Sim.EPBase) / 25.22 / 100 'Haste change for 3.1 ?
        Return tmp / (1 + character.HasteRating / 25.22 / 100)
    End Function
    Function MHExpertise() As Double
        Dim tmp As Double
        tmp = Expertise()
        If Strings.InStr(Sim.EPStat, "EP Expertise") <> 0 Then
        Else
            tmp += (Sim.Character.MHExpertiseBonus * 0.25 / 100)
        End If
        Return tmp
    End Function

    Function OHExpertise() As Double
        Dim tmp As Double
        tmp = Expertise()
        If Strings.InStr(Sim.EPStat, "EP Expertise") <> 0 Then
        Else
            tmp += (Sim.Character.OHExpertiseBonus * 0.25 / 100)
        End If

        Return tmp
    End Function




    Function Expertise() As Double
        Dim tmp As Double

        tmp = character.ExpertiseRating / 32.79
        tmp += 0.25 * Sim.Character.TalentBlood.Vot3W * 2
        tmp += 0.25 * Sim.Character.TalentFrost.TundraStalker
        tmp += 0.25 * Sim.Character.TalentUnholy.RageofRivendare

        Select Case Sim.EPStat
            Case ""
            Case "EP ExpertiseRating"
                tmp = 6.5 - Sim.EPBase / 32.79
            Case "EP ExpertiseRatingCap"
                tmp = 6.5
            Case "EP ExpertiseRatingCapAP"
                tmp = 6.5
            Case "EP ExpertiseRatingAfterCap"
                tmp = 6.5 + Sim.EPBase / 32.79
            Case "EP RelativeExpertiseRating"
                tmp += Sim.EPBase / 32.79
            Case Else
                If InStr(Sim.EPStat, "ScaExp") Then
                    If InStr(Sim.EPStat, "ScaExpA") Then
                        tmp = tmp + Replace(Sim.EPStat, "ScaExpA", "") * Sim.EPBase / 32.79
                    Else
                        tmp = Replace(Sim.EPStat, "ScaExp", "") * Sim.EPBase / 32.79
                    End If
                End If
        End Select

        Return tmp / 100
    End Function
    Function Hit() As Double
        Dim tmp As Double
        tmp = (character.HitRating / 32.79)
        If DualW() Then tmp += Sim.Character.TalentFrost.NervesofColdSteel

        If InStr(Sim.EPStat, "EP ") <> 0 Then
            If InStr(Sim.EPStat, "Hit") = 0 Then
                tmp += Sim.Character.Buff.Draenei
            Else
                If Sim.EPStat = "EP RelativeHitRating" Then tmp += Sim.Character.Buff.Draenei
            End If
        Else
            tmp += Sim.Character.Buff.Draenei
        End If
        Hit = tmp / 100
    End Function

    Function SpellHitCapRating(Optional ByVal target As Targets.Target = Nothing) As Integer
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Integer
        tmp = 17
        tmp = tmp - Sim.Character.TalentUnholy.Virulence
        tmp = tmp - 3 * target.Debuff.SpellHitTaken
        tmp = tmp * 26.23
        Return tmp
    End Function



    Function SpellHit(Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = character.SpellHitRating / 26.23
        If InStr(Sim.EPStat, "EP ") <> 0 Then
            If InStr(Sim.EPStat, "Hit") = 0 Then
                tmp += Sim.Character.Buff.Draenei
            End If
        Else
            tmp += Sim.Character.Buff.Draenei
        End If
        tmp += 1 * Sim.Character.TalentUnholy.Virulence
        tmp += target.Debuff.SpellHitTaken * 3
        SpellHit = tmp / 100
    End Function

    Function NormalisedMHDamage() As Double
        Dim tmp As Double
        tmp = MHWeaponSpeed * MHWeaponDPS
        If DualW() Then
            tmp = tmp + 2.4 * (AP() / 14)
        Else
            tmp = tmp + 3.3 * (AP() / 14)
            tmp = tmp * (1 + Sim.Character.TalentBlood.Weapspec * 2 / 100)
        End If
        Return tmp
    End Function
    Function NormalisedOHDamage() As Double
        Dim tmp As Double
        tmp = OHWeaponSpeed * OHWeaponDPS
        tmp = tmp + 2.4 * (AP() / 14)
        Return tmp
    End Function
    Function MHBaseDamage() As Double
        Dim tmp As Double
        tmp = (MHWeaponDPS + (AP() / 14)) * MHWeaponSpeed
        If DualW() = False Then tmp = tmp * (1 + Sim.Character.TalentBlood.Weapspec * 2 / 100)
        Return tmp
    End Function
    Function OHBaseDamage() As Double
        OHBaseDamage = (OHWeaponDPS + (AP() / 14)) * OHWeaponSpeed
    End Function
    Function ArmorPen() As Double
        Dim tmp As Double
        tmp = character.ArmorPenetrationRating / 15.39
        tmp = tmp * 1.1 '1.1 with Patch 3.2.2, before 1.25
        tmp = tmp + Sim.Character.TalentBlood.BloodGorged * 2
        Return tmp / 100
    End Function

    Function getMitigation(Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim AttackerLevel As Integer = 80
        Dim tmpArmor As Integer
        Dim ArPDebuffs As Double
        Dim l_sunder As Double = 1.0
        Dim l_ff As Double = 1.0
        If target.Debuff.ArmorMajor > 0 Then l_sunder = 1 - 0.2
        If target.Debuff.ArmorMinor > 0 Then l_ff = 1 - 0.05
        ArPDebuffs = (l_sunder * l_ff)
        Dim ArmorConstant As Double = 400 + (85 * 80) + 4.5 * 85 * (80 - 59)
        tmpArmor = BossArmor * ArPDebuffs
        Dim ArPCap As Double = Math.Min((tmpArmor + ArmorConstant) / 3, tmpArmor)
        tmpArmor = tmpArmor - ArPCap * Math.Min(1, ArmorPen)
        _Mitigation = ArmorConstant / (ArmorConstant + tmpArmor)
        Return _Mitigation
    End Function



    Private Function _BaseDamageMultiplier(ByVal T As Long) As Double
        Dim tmp As Double
        tmp = 1 + Sim.BloodPresence * 0.15
        tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
        tmp = tmp * (1 + 0.02 * Sim.BoneShield.Value(T))
        tmp = tmp * (1 + 0.02 * Sim.Character.TalentBlood.BloodGorged)
        tmp = tmp * (1 + Sim.proc.GetActiveBonus("percent") / 100)
        If Sim.proc.Desolation.IsActiveAt(T) Then tmp = tmp * (1 + Sim.proc.Desolation.ProcValue * 0.01)
        If Sim.proc.T104PDPS.IsActiveAt(T) Then tmp = tmp * 1.03
        Return tmp
    End Function

    Function WhiteHitDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = _BaseDamageMultiplier(T) * getMitigation()
        tmp = tmp * (1 + 0.04 * target.Debuff.PhysicalVuln)
        tmp = tmp * (1 + 0.03 * Sim.Character.TalentBlood.BloodyVengeance)
        If Sim.Hysteria.IsActive(T) Then tmp = tmp * 1.2
        Return tmp
    End Function
    Function StandardPhysicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = WhiteHitDamageMultiplier(T)
        If Sim.Targets.MainTarget.FrostFever.isActive(T) Or target.Debuff.FrostFever = 1 Then tmp = tmp * (1 + 0.03 * Sim.Character.TalentFrost.TundraStalker)
        If Sim.Targets.MainTarget.BloodPlague.isActive(T) Or target.Debuff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * Sim.Character.TalentUnholy.RageofRivendare)
        Return tmp
    End Function
    Function StandardMagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = Sim.Targets.MainTarget
        Dim tmp As Double
        tmp = _BaseDamageMultiplier(T)
        If Sim.Targets.MainTarget.FrostFever.isActive(T) Or target.Debuff.FrostFever = 1 Then tmp = tmp * (1 + 0.03 * Sim.Character.TalentFrost.TundraStalker)
        If Sim.Targets.MainTarget.BloodPlague.isActive(T) Or target.Debuff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * Sim.Character.TalentUnholy.RageofRivendare)
        tmp = tmp * (1 + 0.13 * target.Debuff.SpellDamageTaken)
        tmp = tmp * (1 - 15 / (510 + 15)) 'Partial Resistance. It's about 0,029% less damage on average.

        Return tmp
    End Function
End Class
