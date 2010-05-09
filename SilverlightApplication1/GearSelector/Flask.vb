Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/25/2010
' Heure: 4:04 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Flask
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
    Friend Armor As Integer
    Protected FlaskDB As XDocument
    Protected MainFrame As GearSelectorMainForm

    Sub New(ByVal MainFrm As GearSelectorMainForm)
        MainFrame = MainFrm
        FlaskDB = MainFrame.FlaskDB
    End Sub
    Sub Attach(ByVal FlaskId As Integer)
        If FlaskId = 0 Then
            Detach()
            Exit Sub
        End If
        Id = FlaskId
        name = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/name").Value
        Strength = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/Strength").Value
        Agility = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/Agility").Value
        HasteRating = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/HasteRating").Value
        ExpertiseRating = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/ExpertiseRating").Value
        HitRating = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/HitRating").Value
        AttackPower = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/AttackPower").Value
        CritRating = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/CritRating").Value
        ArmorPenetrationRating = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/ArmorPenetrationRating").Value
        Armor = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/Armor").Value
        Desc = FlaskDB.Element("/flask/item[id=" & FlaskId & "]/Desc").Value
    End Sub

    Sub Attach(ByVal FlaskName As String)
        If FlaskName = "" Then
            Detach()
            Exit Sub
        End If
        Try
            Dim XPathQ As String

            XPathQ = "/flask/item[name=" & Convert.ToChar(34) & FlaskName & Convert.ToChar(34) & "]"

            Id = FlaskDB.Element(XPathQ & "/id").Value
            name = FlaskDB.Element(XPathQ & "/name").Value
            Strength = FlaskDB.Element(XPathQ & "/Strength").Value
            Agility = FlaskDB.Element(XPathQ & "/Agility").Value
            HasteRating = FlaskDB.Element(XPathQ & "/HasteRating").Value
            ExpertiseRating = FlaskDB.Element(XPathQ & "/ExpertiseRating").Value
            HitRating = FlaskDB.Element(XPathQ & "/HitRating").Value
            AttackPower = FlaskDB.Element(XPathQ & "/AttackPower").Value
            CritRating = FlaskDB.Element(XPathQ & "/CritRating").Value
            ArmorPenetrationRating = FlaskDB.Element(XPathQ & "/ArmorPenetrationRating").Value
            Armor = FlaskDB.Element(XPathQ & "/Armor").Value
            Desc = FlaskDB.Element(XPathQ & "/Desc").Value
        Catch Err As Exception
            Diagnostics.Debug.WriteLine(Err.ToString)

        End Try

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
        Armor = 0
        Desc = ""
    End Sub
End Class
