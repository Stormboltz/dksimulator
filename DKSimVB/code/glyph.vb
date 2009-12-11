'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:27
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class glyph
	Friend xmlGlyph As New Xml.XmlDocument
	Friend  BloodStrike As Boolean
	Friend  DRW As Boolean
	Friend  DarkDeath As Boolean
	Friend  DeathandDecay As Boolean
	Friend  DeathStrike As Boolean
	Friend  Disease As Boolean
	Friend  FrostStrike As Boolean
	Friend  HowlingBlast As Boolean
	Friend  IcyTouch As Boolean
	Friend  Obliterate As Boolean
	Friend  PlagueStrike As Boolean
	Friend  RuneStrike As Boolean
	Friend  ScourgeStrike As Boolean
	Friend  Ghoul As Boolean
	Friend  UnholyBlight As Boolean
	Friend  BoneShield As Boolean
	
	
	Sub New(path As String)
		on error resume next
		xmlGlyph.Load(path)
		BloodStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/BloodStrike").InnerText =1)
		DRW = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DRW").InnerText =1)
		DarkDeath = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DarkDeath").InnerText =1)
		DeathandDecay = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DeathandDecay").InnerText =1)
		DeathStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DeathStrike").InnerText =1)
		Disease = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Disease").InnerText =1) 'NYI
		FrostStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/FrostStrike").InnerText =1)
		HowlingBlast = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/HowlingBlast").InnerText =1)
		IcyTouch = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/IcyTouch").InnerText =1)
		Obliterate = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Obliterate").InnerText =1)
		PlagueStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/PlagueStrike").InnerText =1)
		RuneStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/RuneStrike").InnerText =1) 'NYI
		ScourgeStrike = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/ScourgeStrike").InnerText =1)
		Ghoul = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Ghoul").InnerText =1)
		UnholyBlight = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/UnholyBlight").InnerText =1)
		BoneShield = (xmlGlyph.SelectSingleNode("//Talents/Glyphs/BoneShield").InnerText =1)
		
	End Sub
End Class
