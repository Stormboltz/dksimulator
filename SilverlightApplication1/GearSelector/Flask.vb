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


        Dim el As XElement = (From x In MainFrame.FlaskDB.Element("flask").Elements
                              Where x.Element("id").Value = FlaskId
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

    Sub Attach(ByVal FlaskName As String)
        If FlaskName = "" Or IsNothing(FlaskName) Then
            Detach()
            Exit Sub
        End If
        Try
            Dim el As XElement = (From x In MainFrame.FlaskDB.Element("flask").Elements
                               Where x.Element("name").Value = FlaskName
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
        Catch Err As Exception

            Log.Log(Err.StackTrace, logging.Level.ERR)
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
