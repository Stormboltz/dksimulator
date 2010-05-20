Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:27
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class glyph
    Friend xmlGlyph As New XDocument
    Friend BloodStrike As Boolean
    Friend DRW As Boolean
    Friend DarkDeath As Boolean
    Friend DeathandDecay As Boolean
    Friend DeathStrike As Boolean
    Friend Disease As Boolean
    Friend FrostStrike As Boolean
    Friend HowlingBlast As Boolean
    Friend IcyTouch As Boolean
    Friend Obliterate As Boolean
    Friend PlagueStrike As Boolean
    Friend RuneStrike As Boolean
    Friend ScourgeStrike As Boolean
    Friend Ghoul As Boolean
    Friend UnholyBlight As Boolean
    Friend BoneShield As Boolean


    Sub New(ByVal path As String)
        On Error Resume Next
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & path, FileMode.Open, FileAccess.Read, isoStore)
                xmlGlyph = XDocument.Load(isoStream)
            End Using
        End Using


        BloodStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("BloodStrike").Value = 1)
        DRW = (xmlGlyph.Element("Talents").Element("Glyphs").Element("DRW").Value = 1)
        DarkDeath = (xmlGlyph.Element("Talents").Element("Glyphs").Element("DarkDeath").Value = 1)
        DeathandDecay = (xmlGlyph.Element("Talents").Element("Glyphs").Element("DeathandDecay").Value = 1)
        DeathStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("DeathStrike").Value = 1)
        Disease = (xmlGlyph.Element("Talents").Element("Glyphs").Element("Disease").Value = 1) 'NYI
        FrostStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("FrostStrike").Value = 1)
        HowlingBlast = (xmlGlyph.Element("Talents").Element("Glyphs").Element("HowlingBlast").Value = 1)
        IcyTouch = (xmlGlyph.Element("Talents").Element("Glyphs").Element("IcyTouch").Value = 1)
        Obliterate = (xmlGlyph.Element("Talents").Element("Glyphs").Element("Obliterate").Value = 1)
        PlagueStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("PlagueStrike").Value = 1)
        RuneStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("RuneStrike").Value = 1) 'NYI
        ScourgeStrike = (xmlGlyph.Element("Talents").Element("Glyphs").Element("ScourgeStrike").Value = 1)
        Ghoul = (xmlGlyph.Element("Talents").Element("Glyphs").Element("Ghoul").Value = 1)
        UnholyBlight = (xmlGlyph.Element("Talents").Element("Glyphs").Element("UnholyBlight").Value = 1)
        BoneShield = (xmlGlyph.Element("Talents").Element("Glyphs").Element("BoneShield").Value = 1)

    End Sub
End Class
