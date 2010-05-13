Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 18:21
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Enchant
    Friend Id As Integer
    Friend name As String
    Friend slot As Integer


    Friend Strength As Integer
    Friend Intel As Integer
    Friend Agility As Integer
    Friend HasteRating As Integer
    Friend ExpertiseRating As Integer
    Friend HitRating As Integer
    Friend AttackPower As Integer
    Friend CritRating As Integer
    Friend ArmorPenetrationRating As Integer
    Friend Desc As String


    Protected EnchantDB As XDocument
    Protected MainFrame As GearSelectorMainForm

    Sub New(ByVal MainFrm As GearSelectorMainForm)
        MainFrame = MainFrm
        EnchantDB = MainFrame.EnchantDB
    End Sub
    Sub Attach(ByVal EnchantId As Integer)
        If EnchantId = 0 Then
            Detach()
            Exit Sub
        End If
        Id = EnchantId

        Dim el As XElement = (From x In MainFrame.EnchantDB.Element("enchant").Elements
                              Where x.Element("id").Value = EnchantId
                              ).First


        With Me
            .Id = el.Element("id").Value
            .name = el.Element("name").Value
            .Strength = el.Element("Strength").Value
            .Agility = el.Element("Agility").Value
            .HasteRating = el.Element("HasteRating").Value
            .ExpertiseRating = el.Element("ExpertiseRating").Value
            .HitRating = el.Element("HitRating").Value
            .AttackPower = el.Element("AttackPower").Value
            .CritRating = el.Element("CritRating").Value
            .ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
            .Desc = el.Element("Desc").Value
        End With
    End Sub

    Sub Detach()
        Id = 0
        slot = 0
        name = "Enchant"
        Strength = 0
        Agility = 0
        HasteRating = 0
        ExpertiseRating = 0
        HitRating = 0
        AttackPower = 0
        CritRating = 0
        ArmorPenetrationRating = 0
        Desc = ""

    End Sub
End Class
