Imports System.Xml.Linq

'

'
Public Class Flask
    Inherits WowItem

    Protected FlaskDB As XDocument
    Protected MainFrame As FrmGearSelector

    Sub New(ByVal MainFrm As FrmGearSelector)
        MainFrame = MainFrm
        FlaskDB = MainFrame.FlaskDB
    End Sub
    Sub Attach(ByVal FlaskId As Integer)
        If FlaskId = 0 Then
            Unload()
            Exit Sub
        End If


        Dim el As XElement = (From x In MainFrame.FlaskDB.Element("flask").Elements
                              Where x.Element("id").Value = FlaskId
                              ).First
        Load(el)

    End Sub

    Sub Attach(ByVal FlaskName As String)
        If FlaskName = "" Or IsNothing(FlaskName) Then
            Unload()
            Exit Sub
        End If
        Try
            Dim el As XElement = (From x In MainFrame.FlaskDB.Element("flask").Elements
                               Where x.Element("name").Value = FlaskName
                               ).First

            Load(el)
        Catch Err As Exception

            Log.Log(Err.StackTrace, logging.Level.ERR)
            Diagnostics.Debug.WriteLine(Err.ToString)

        End Try

    End Sub



   
End Class
