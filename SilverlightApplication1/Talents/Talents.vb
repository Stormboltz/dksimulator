Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Public Class Talents
    Private Talents As New Collections.Generic.Dictionary(Of String, Talent)
    'Private Talents As New List(Of Talent)
    Private IncludedTalents As New List(Of Talent)
    Protected Sim As Sim
    Enum Schools As Integer
        Blood = 0
        Frost = 1
        Unholy = 2
    End Enum

    Sub New(ByVal s As Sim)
        Sim = s
    End Sub


    Function Talent(ByVal Name As String) As Talent
        Try
            Name = Name.ToLower
            Return Talents(Name)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("WTF IS THIS TALENT " & Name)
            Return New Talent(Name, Schools.Blood)
        End Try
    End Function

    Sub Clear()
        For Each T In Talents
            T = Nothing
        Next
        Talents.Clear()
    End Sub
    Function GetSchool(ByVal Sc As String) As Schools
        If Sc = "blood" Then
            Return Schools.Blood
        ElseIf Sc = "frost" Then
            Return Schools.Frost
        ElseIf Sc = "unholy" Then
            Return Schools.Unholy
        Else
            Err.Raise(0, "School Indifined")
            Return 4
        End If
    End Function
    Sub LoadEmptyTemplate()
        Dim XmlDoc As XDocument = XDocument.Load("config/template.xml")
        Dim xNode As XElement
        Me.Clear()
        Dim T As Talent

        For Each xNode In XmlDoc.Element("Talents").Elements("blood").Elements
            If xNode.Name.ToString <> "include" Then
                T = New Talent(xNode.Name.ToString, Schools.Blood)
                Talents.Add(T.Name.ToLower, T)

            Else
                For Each xN As XElement In xNode.Elements
                    T = New Talent(xN.Name.ToString, Schools.Blood)
                    IncludedTalents.Add(T)
                    Talents.Add(T.Name.ToLower, T)
                Next

            End If
        Next

        For Each xNode In XmlDoc.Element("Talents").Elements("frost").Elements
            If xNode.Name.ToString <> "include" Then
                T = New Talent(xNode.Name.ToString, Schools.Frost)
                Talents.Add(T.Name.ToLower, T)
            Else
                For Each xN As XElement In xNode.Elements
                    T = New Talent(xN.Name.ToString, Schools.Frost)
                    IncludedTalents.Add(T)
                    Talents.Add(T.Name.ToLower, T)
                Next
            End If
        Next

        For Each xNode In XmlDoc.Element("Talents").Element("unholy").Elements
            If xNode.Name.ToString <> "include" Then
                T = New Talent(xNode.Name.ToString, Schools.Unholy)
                Talents.Add(T.Name.ToLower, T)
            Else
                For Each xN As XElement In xNode.Elements
                    T = New Talent(xN.Name.ToString, Schools.Unholy)
                    IncludedTalents.Add(T)
                    Talents.Add(T.Name.ToLower, T)
                Next

            End If
        Next
    End Sub
    Sub AddIncluded()
        If GetNumOfThisSchool(Schools.Blood) > 10 Then
            For Each T As Talent In IncludedTalents
                If T.School = Schools.Blood Then
                    Talent(T.Name).Value = 1
                End If
            Next
        End If
        If GetNumOfThisSchool(Schools.Frost) > 10 Then
            For Each T As Talent In IncludedTalents
                If T.School = Schools.Frost Then
                    Talent(T.Name).Value = 1
                End If
            Next
        End If
        If GetNumOfThisSchool(Schools.Unholy) > 10 Then
            For Each T As Talent In IncludedTalents
                If T.School = Schools.Unholy Then
                    Talent(T.Name).Value = 1
                End If
            Next
        End If
    End Sub


    Function GetNumOfThisSchool(ByVal School As Schools) As Integer
        Dim i As Integer
        For Each T As Talent In Talents.Values
            If T.School = School Then i = i + 1
        Next
        Return i
    End Function

    Sub ReadTemplate(ByVal File As String)
        Dim XmlDoc As XDocument
        If Talents.Count = 0 Then
            LoadEmptyTemplate()
        End If

        Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & File, FileMode.Open, FileAccess.Read, Sim.isoStore)
            XmlDoc = XDocument.Load(isoStream)
        End Using

        For Each el As XElement In XmlDoc.Element("Talents").Elements
            If el.Name.ToString <> "Glyphs" Then
                Talent(el.Name.ToString).Value = el.Value
            End If
        Next
        AddIncluded()

    End Sub





End Class
