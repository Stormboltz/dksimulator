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
    Protected MainFrame As GearSelectorMainForm

    Sub New(ByVal MainFrm As GearSelectorMainForm)
        MainFrame = MainFrm
        foodDB = MainFrame.FoodDB
    End Sub
    Sub Attach(ByVal FoodId As Integer)
        If FoodId = 0 Then
            Detach()
            Exit Sub
        End If
        Id = FoodId
        name = foodDB.Element("/food/item[id=" & FoodId & "]/name").Value
        Strength = foodDB.Element("/food/item[id=" & FoodId & "]/Strength").Value
        Agility = foodDB.Element("/food/item[id=" & FoodId & "]/Agility").Value
        HasteRating = foodDB.Element("/food/item[id=" & FoodId & "]/HasteRating").Value
        ExpertiseRating = foodDB.Element("/food/item[id=" & FoodId & "]/ExpertiseRating").Value
        HitRating = foodDB.Element("/food/item[id=" & FoodId & "]/HitRating").Value
        AttackPower = foodDB.Element("/food/item[id=" & FoodId & "]/AttackPower").Value
        CritRating = foodDB.Element("/food/item[id=" & FoodId & "]/CritRating").Value
        ArmorPenetrationRating = foodDB.Element("/food/item[id=" & FoodId & "]/ArmorPenetrationRating").Value
        Desc = foodDB.Element("/food/item[id=" & FoodId & "]/Desc").Value
    End Sub

    Sub Attach(ByVal FoodName As String)
        If FoodName = "" Then
            Detach()
            Exit Sub
        End If
        Dim XPathQ As String
        XPathQ = "/food/item[name=" & Convert.ToChar(34) & FoodName & Convert.ToChar(34) & "]"

        Id = foodDB.Element(XPathQ & "/id").Value
        name = foodDB.Element(XPathQ & "/name").Value
        Strength = foodDB.Element(XPathQ & "/Strength").Value
        Agility = foodDB.Element(XPathQ & "/Agility").Value
        HasteRating = foodDB.Element(XPathQ & "/HasteRating").Value
        ExpertiseRating = foodDB.Element(XPathQ & "/ExpertiseRating").Value
        HitRating = foodDB.Element(XPathQ & "/HitRating").Value
        AttackPower = foodDB.Element(XPathQ & "/AttackPower").Value
        CritRating = foodDB.Element(XPathQ & "/CritRating").Value
        ArmorPenetrationRating = foodDB.Element(XPathQ & "/ArmorPenetrationRating").Value
        Desc = foodDB.Element(XPathQ & "/Desc").Value
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
