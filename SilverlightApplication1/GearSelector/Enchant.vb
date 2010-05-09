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

        Try
            name = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/name").Value
            slot = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/slot").Value
            Strength = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/Strength").Value
            Agility = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/Agility").Value
            HasteRating = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/HasteRating").Value
            ExpertiseRating = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/ExpertiseRating").Value
            HitRating = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/HitRating").Value
            AttackPower = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/AttackPower").Value
            CritRating = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/CritRating").Value
            ArmorPenetrationRating = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/ArmorPenetrationRating").Value
            Desc = EnchantDB.Element("/enchant/item[id=" & EnchantId & "]/Desc").Value
        Catch ex As System.Exception
            Diagnostics.Debug.WriteLine("error with enchant " & EnchantId)
            Detach()
        End Try
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
