Imports System.Xml.Linq
Imports System.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:54
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Gem
    Inherits WowItem

    Friend Color As Color
    Friend ColorId As Integer
    Friend keywords As String

    Protected GemDB As XDocument
    Protected MainFrame As FrmGearSelector
    Sub New()

    End Sub
    Sub New(ByVal MainFrm As FrmGearSelector, ByVal Color As Integer)
        MainFrame = MainFrm
        GemDB = MainFrame.GemDB
        ColorId = Color
    End Sub
    Sub Attach(ByVal GemId As Integer)
        If GemId = 0 Or ColorId = 0 Then
            Unload()
            Exit Sub
        End If
        Try


            Dim el As XElement = (From x In GemDB.<gems>.Elements
                                  Where x.<id>.Value = GemId
                                  ).First
            Load(el)
        Catch ex As Exception
            Unload()
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try


    End Sub
    Public Overrides Sub Unload()
        MyBase.Unload()
        Color = Nothing
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
                Return New SolidColorBrush(Colors.White)
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
