Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:54
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Gem
    Friend ColorId As Integer
    Friend Id As Integer
    Friend name As String
    Friend ilvl As Integer
    Friend classs As Integer
    Friend subclass As Integer = -1

    Friend Strength As Integer
    Friend Intel As Integer
    Friend Agility As Integer
    Friend HasteRating As Integer
    Friend ExpertiseRating As Integer
    Friend HitRating As Integer
    Friend AttackPower As Integer
    Friend CritRating As Integer
    Friend ArmorPenetrationRating As Integer
    Friend keywords As String


    Protected GemDB As XDocument
    Protected MainFrame As GearSelectorMainForm

    Sub New(ByVal MainFrm As GearSelectorMainForm, ByVal Color As Integer)
        MainFrame = MainFrm
        GemDB = MainFrame.GemDB
        ColorId = Color
    End Sub
    Sub Attach(ByVal GemId As Integer)
        If GemId = 0 Or ColorId = 0 Then
            Detach()
            Exit Sub
        End If
        Id = GemId
        name = GemDB.Element("/gems/item[id=" & GemId & "]/name").Value
        ilvl = GemDB.Element("/gems/item[id=" & GemId & "]/ilvl").Value
        classs = GemDB.Element("/gems/item[id=" & GemId & "]/class").Value
        subclass = GemDB.Element("/gems/item[id=" & GemId & "]/subclass").Value
        Strength = GemDB.Element("/gems/item[id=" & GemId & "]/Strength").Value
        Agility = GemDB.Element("/gems/item[id=" & GemId & "]/Agility").Value
        HasteRating = GemDB.Element("/gems/item[id=" & GemId & "]/HasteRating").Value
        ExpertiseRating = GemDB.Element("/gems/item[id=" & GemId & "]/ExpertiseRating").Value
        HitRating = GemDB.Element("/gems/item[id=" & GemId & "]/HitRating").Value
        AttackPower = GemDB.Element("/gems/item[id=" & GemId & "]/AttackPower").Value
        CritRating = GemDB.Element("/gems/item[id=" & GemId & "]/CritRating").Value
        ArmorPenetrationRating = GemDB.Element("/gems/item[id=" & GemId & "]/ArmorPenetrationRating").Value
        keywords = GemDB.Element("/gems/item[id=" & GemId & "]/keywords").Value
    End Sub

    Sub Detach()
        Id = 0
        'ColorId = 0
        name = ""
        ilvl = 0
        classs = 0
        subclass = -1
        Strength = 0
        Agility = 0
        HasteRating = 0
        ExpertiseRating = 0
        HitRating = 0
        AttackPower = 0
        CritRating = 0
        ArmorPenetrationRating = 0
        keywords = ""
    End Sub

    Function GemSlotColorName() As String
        Select Case ColorId
            Case 0
                Return ""
            Case 1
                Return "Meta"
            Case 2
                Return "Red"
            Case 4
                Return "Yellow"
            Case 8
                Return "Blue"
            Case 16
                Return "Prism"
            Case Else
                Diagnostics.Debug.WriteLine("GemSlotColorName: " & Id & " ??")
                Return ""
        End Select
    End Function

    Function GemSlotColor() As Brush
        Select Case ColorId
            Case 0
                Return New SolidColorBrush(Colors.White)
            Case 1
                Return New SolidColorBrush(Colors.Blue)

            Case 2
                Return New SolidColorBrush(Colors.Red)
            Case 4
                Return New SolidColorBrush(Colors.Yellow)
            Case 8
                Return New SolidColorBrush(Colors.Blue)
            Case 16
                Return New SolidColorBrush(Colors.Gray)

            Case Else
                Diagnostics.Debug.WriteLine("GemSlotColorName: " & Id & " ??")
        End Select
    End Function

    Function IsGemrightColor() As Boolean

        If subclass = 8 Then Return True
        Select Case ColorId
            Case 0 '??
                Return False
            Case 1 ' Meta
                If subclass = 6 Then Return True
            Case 2 ' Red
                If subclass = 0 Or subclass = 3 Or subclass = 5 Then Return True

            Case 4 'Yellow
                If subclass = 2 Or subclass = 4 Or subclass = 5 Then Return True
            Case 8 ' Blue
                If subclass = 1 Or subclass = 3 Or subclass = 4 Then Return True
            Case 16 'Prim
                Return True
            Case Else
                Diagnostics.Debug.WriteLine("GemSlotColorName: " & ColorId & " ??")
                Return False
        End Select
        Return False

    End Function


End Class
