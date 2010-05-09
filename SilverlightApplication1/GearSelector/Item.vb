Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:45
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Item

    Friend Id As Integer
    Friend name As String
    Friend ilvl As Integer
    Friend slot As Integer
    Friend classs As Integer
    Friend subclass As Integer
    Friend heroic As Integer

    Friend Strength As Integer
    Friend Intel As Integer
    Friend Agility As Integer
    Friend BonusArmor As Integer
    Friend Armor As Integer
    Friend HasteRating As Integer
    Friend ExpertiseRating As Integer


    Friend HitRating As Integer
    Friend AttackPower As Integer
    Friend CritRating As Integer
    Friend ArmorPenetrationRating As Integer
    Friend Speed As String = "0"
    Friend DPS As String = "0"

    Friend setid As Integer
    Friend gembonus As Integer
    Friend keywords As String

    Friend gem1 As Gem
    Friend gem2 As Gem
    Friend gem3 As Gem
    Friend Enchant As Enchant




    Protected MainFrame As GearSelectorMainForm

    Protected ItemDB As XDocument

    Protected AdditionalGemNotSet As Boolean





    Sub New(ByVal MainFrm As GearSelectorMainForm, Optional ByVal ItemId As Integer = 0)
        MainFrame = MainFrm
        ItemDB = MainFrame.ItemDB

        gem1 = New Gem(Me.MainFrame, 0)
        gem2 = New Gem(Me.MainFrame, 0)
        gem3 = New Gem(Me.MainFrame, 0)
        Enchant = New Enchant(Me.MainFrame)
        If ItemId <> 0 Then LoadItem(ItemId)
    End Sub



    Sub LoadItem(ByVal ItemId As Integer)
        AdditionalGemNotSet = True
        Id = ItemId
        If Id = 0 Then
            Unload()
            Exit Sub
        End If
        name = ItemDB.Element("/items/item[id=" & ItemId & "]/name").Value
        ilvl = ItemDB.Element("/items/item[id=" & ItemId & "]/ilvl").Value
        slot = ItemDB.Element("/items/item[id=" & ItemId & "]/slot").Value
        classs = ItemDB.Element("/items/item[id=" & ItemId & "]/classs").Value
        subclass = ItemDB.Element("/items/item[id=" & ItemId & "]/subclass").Value
        heroic = ItemDB.Element("/items/item[id=" & ItemId & "]/heroic").Value



        Strength = ItemDB.Element("/items/item[id=" & ItemId & "]/Strength").Value
        Agility = ItemDB.Element("/items/item[id=" & ItemId & "]/Agility").Value
        BonusArmor = ItemDB.Element("/items/item[id=" & ItemId & "]/BonusArmor").Value
        Armor = ItemDB.Element("/items/item[id=" & ItemId & "]/Armor").Value
        HasteRating = ItemDB.Element("/items/item[id=" & ItemId & "]/HasteRating").Value
        ExpertiseRating = ItemDB.Element("/items/item[id=" & ItemId & "]/ExpertiseRating").Value
        HitRating = ItemDB.Element("/items/item[id=" & ItemId & "]/HitRating").Value
        AttackPower = ItemDB.Element("/items/item[id=" & ItemId & "]/AttackPower").Value
        CritRating = ItemDB.Element("/items/item[id=" & ItemId & "]/CritRating").Value
        ArmorPenetrationRating = ItemDB.Element("/items/item[id=" & ItemId & "]/ArmorPenetrationRating").Value


        Speed = ItemDB.Element("/items/item[id=" & ItemId & "]/speed").Value
        DPS = ItemDB.Element("/items/item[id=" & ItemId & "]/dps").Value

        setid = ItemDB.Element("/items/item[id=" & ItemId & "]/setid").Value

        Dim gem1Col As Integer = ItemDB.Element("/items/item[id=" & ItemId & "]/gem1").Value
        Dim gem2Col As Integer = ItemDB.Element("/items/item[id=" & ItemId & "]/gem2").Value
        Dim gem3Col As Integer = ItemDB.Element("/items/item[id=" & ItemId & "]/gem3").Value
        Dim i As Integer
        i = gem1.Id
        If gem1Col <> 0 Then
            i = gem1.Id
            gem1 = New Gem(Me.MainFrame, gem1Col)
            gem1.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem1 = New Gem(Me.MainFrame, 16)
                gem1.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem1 = New Gem(Me.MainFrame, 0)
            End If
        End If
        i = gem2.Id
        If gem2Col <> 0 Then
            gem2 = New Gem(Me.MainFrame, gem2Col)
            gem2.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem2 = New Gem(Me.MainFrame, 16)
                gem2.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem2 = New Gem(Me.MainFrame, 0)
            End If

        End If
        i = gem3.Id
        If gem3Col <> 0 Then
            gem3 = New Gem(Me.MainFrame, gem3Col)
            gem3.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem3 = New Gem(Me.MainFrame, 16)
                gem3.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem3 = New Gem(Me.MainFrame, 0)
            End If
        End If
        gembonus = ItemDB.Element("/items/item[id=" & ItemId & "]/gembonus").Value
        keywords = ItemDB.Element("/items/item[id=" & ItemId & "]/keywords").Value
    End Sub


    Sub Unload()
        Id = 0
        name = ""
        ilvl = 0
        slot = 0
        classs = 0
        subclass = 0
        heroic = 0
        Strength = 0
        Agility = 0
        BonusArmor = 0
        Armor = 0
        HasteRating = 0
        ExpertiseRating = 0
        HitRating = 0
        AttackPower = 0
        CritRating = 0
        ArmorPenetrationRating = 0
        Speed = 0
        DPS = 0
        setid = 0
        gem1.Detach()
        gem2.Detach()
        gem3.Detach()
        Enchant.Detach()

        gembonus = 0
        keywords = ""
    End Sub

    Function AdditionalGem() As Boolean
        Select Case slot
            Case 9, 10
                If GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) = 164 Or GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) = 164 Then
                    Return True
                Else
                    Return False
                End If
            Case 6
                Return True
            Case Else
                Return False
        End Select

    End Function

    Function IsGembonusActif() As Boolean

        If gem1.IsGemrightColor Or gem1.ColorId = 0 Or gem1.ColorId = 16 Then
            If gem2.IsGemrightColor Or gem2.ColorId = 0 Or gem2.ColorId = 16 Then
                If gem3.IsGemrightColor Or gem3.ColorId = 0 Or gem3.ColorId = 16 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function



End Class
