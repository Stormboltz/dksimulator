Imports System.Xml.Linq

Public Class Food
    Friend Id As Integer
    Friend name As String
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
    Protected foodDB As XDocument
    Protected MainFrame As FrmGearSelector

    Sub New(ByVal MainFrm As FrmGearSelector)
        MainFrame = MainFrm
        foodDB = MainFrame.FoodDB
    End Sub
    Sub Attach(ByVal FoodId As Integer)
        If FoodId = 0 Then
            Detach()
            Exit Sub
        End If
        Dim el As XElement = (From x In MainFrame.FoodDB.Element("food").Elements
                              Where x.Element("id").Value = FoodId
                              ).First
        Id = el.Element("id").Value
        name = el.Element("name").Value
        Strength = el.Element("Strength").Value
        Agility = el.Element("Agility").Value
        HasteRating = el.Element("HasteRating").Value
        ExpertiseRating = el.Element("ExpertiseRating").Value
        HitRating = el.Element("HitRating").Value
        AttackPower = el.Element("AttackPower").Value
        CritRating = el.Element("CritRating").Value
        ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
        Desc = el.Element("Desc").Value

    End Sub

    Sub Attach(ByVal FoodName As String)
        If FoodName = "" Then
            Detach()
            Exit Sub
        End If
        Dim el As XElement = (From x In MainFrame.FoodDB.Element("food").Elements
                              Where x.Element("name").Value = FoodName
                              ).First
        Id = el.Element("id").Value
        name = el.Element("name").Value
        Strength = el.Element("Strength").Value
        Agility = el.Element("Agility").Value
        HasteRating = el.Element("HasteRating").Value
        ExpertiseRating = el.Element("ExpertiseRating").Value
        HitRating = el.Element("HitRating").Value
        AttackPower = el.Element("AttackPower").Value
        CritRating = el.Element("CritRating").Value
        ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
        Desc = el.Element("Desc").Value
    End Sub



    Sub Detach()
        Id = 0
        name = ""
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
