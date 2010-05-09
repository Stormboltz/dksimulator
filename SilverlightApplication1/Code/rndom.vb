Imports System.Xml.Linq

'
' Created by SharpDevelop.
' User: Fabien
' Date: 03/04/2009
' Time: 23:40
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module rndom
    Friend RandomNumberGenerator As Random
    Friend SavedRNG As Random
    Friend RNGSeed As Integer




    Sub InitRNG()

        Dim doc As XDocument = New XDocument
        doc.Load("EPconfig.xml")
        If doc.Element("//config/Misc/chkEPSeed").Value = "True" Then
            If RNGSeed = 0 Then
                RandomNumberGenerator = New Random
                RNGSeed = RandomNumberGenerator.Next()
                WriteReport("RNG seed= " & RNGSeed)
            End If
            RandomNumberGenerator = New Random(RNGSeed)
        Else
            RandomNumberGenerator = New Random
        End If
    End Sub
End Module
