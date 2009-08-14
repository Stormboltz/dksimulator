'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:27
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module glyph
	Friend xmlGlyph As New Xml.XmlDocument
	
	Sub init(path as String)
		xmlGlyph.Load(path)
	End Sub
	Function BloodStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/BloodStrike").InnerText =1)
	End Function
	Function DRW As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DRW").InnerText =1)
	End Function
	Function DarkDeath As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DarkDeath").InnerText =1) 
	End Function
	Function DeathandDecay As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DeathandDecay").InnerText =1)
	End Function
	Function DeathStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/DeathStrike").InnerText =1)
	End Function
	Function Disease As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Disease").InnerText =1) 'NYI
	End Function
	
	Function FrostStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/FrostStrike").InnerText =1)
	End Function
	Function HowlingBlast As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/HowlingBlast").InnerText =1) 
	End Function
	Function IcyTouch As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/IcyTouch").InnerText =1)
	End Function
	Function Obliterate As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Obliterate").InnerText =1)
	End Function
	Function PlagueStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/PlagueStrike").InnerText =1)
	End Function
	Function RuneStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/RuneStrike").InnerText =1) 'NYI
	End Function
	Function ScourgeStrike As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/ScourgeStrike").InnerText =1)
	End Function
	Function Ghoul As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/Ghoul").InnerText =1)
	End Function
	Function UnholyBlight As Boolean
		return (xmlGlyph.SelectSingleNode("//Talents/Glyphs/UnholyBlight").InnerText =1)
	End Function
End Module
