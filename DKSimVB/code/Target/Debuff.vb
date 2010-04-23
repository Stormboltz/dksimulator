'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 9:56 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Targets
	Public Class Debuff
		
		Friend ArmorMajor As Integer
		Friend ArmorMinor As Integer
		Friend CritChanceTaken As Integer
		Friend PhysicalVuln As Integer
		Friend SpellCritTaken As Integer
		Friend SpellDamageTaken As Integer
		Friend SpellHitTaken As Integer
		Friend CrypticFever As Integer
		Friend BloodPlague As Integer
		Friend FrostFever As Integer
		
		Protected sim As Sim
		Sub New(S As Sim)
			Sim = S
			
			Dim doc As xml.XmlDocument = New xml.XmlDocument
			Dim liveXml As new xml.XmlDocument
			dim tmp as String
			doc.Load("Buffconfig.xml")
			tmp  = doc.OuterXml
			
			tmp = tmp.Replace("True","1")
			tmp = tmp.Replace("False","0")
			liveXml.LoadXml(tmp)
			
			ArmorMajor = liveXml.SelectSingleNode("/config/chkBArmorMaj").InnerText
			ArmorMinor = liveXml.SelectSingleNode("/config/chkBArmorMinor").InnerText
			CritChanceTaken = liveXml.SelectSingleNode("/config/chkBCritchanceTaken").InnerText
			PhysicalVuln = liveXml.SelectSingleNode("/config/chkBPhyVuln").InnerText
			SpellCritTaken = liveXml.SelectSingleNode("/config/chkBSpCrTaken").InnerText
			SpellDamageTaken = liveXml.SelectSingleNode("/config/chkBSpDamTaken").InnerText
			SpellHitTaken = liveXml.SelectSingleNode("/config/chkBSpHitTaken").InnerText
			CrypticFever = liveXml.SelectSingleNode("/config/chkCrypticFever").InnerText
			BloodPlague = liveXml.SelectSingleNode("/config/chkBloodPlague").InnerText
			FrostFever = liveXml.SelectSingleNode("/config/chkFrostFever").InnerText
		End Sub
		Sub Unbuff()
			ArmorMajor = 0
			ArmorMinor = 0
			CritChanceTaken =0
			PhysicalVuln =0
			SpellCritTaken = 0
			SpellDamageTaken = 0
			SpellHitTaken = 0
			CrypticFever = 0
			BloodPlague = 0
			FrostFever =  0
		End Sub
		
		
'		Function RazorIceMultiplier(T As Long) as Double
'			If RIProc Is Nothing Then Return 1.0
'			Return 1.0 + 0.02 * RIProc.Stack
'		End Function
'		
		
		
		
		
	End Class
End Namespace