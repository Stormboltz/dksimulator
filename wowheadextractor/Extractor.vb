'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:42 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System
Imports System.IO
Imports System.Net

Public Class Extractor
	friend client As WebClient
	Friend col as new ArrayList
	Sub Start
		dim i as Integer
		init
		GenerateWowheadFilter
		'GetListofGems
		Dim str As String
		col.Sort
		
		For Each str In col
			ExtractThis(GetXmlFromID(str))
			debug.Print (i & "/"  & col.Count)
			i +=1
		Next
	End Sub
	Sub GemExtrator
		
	End Sub
	
	Sub init()
		client = New WebClient()
		'Dim prox = client.Proxy
		client.Proxy = WebRequest.GetSystemWebProxy
		client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials
	End Sub
	
	
	
	Sub GenerateWowheadFilter()
		GetListofID(200,212)
		GetListofID(213,218)
		GetListofID(219,225)
		GetListofID(226,231)
		GetListofID(232,244)
		GetListofID(245,250)
		GetListofID(251,257)
		GetListofID(258,263)
		GetListofID(264,270)
		GetListofID(271,300)
	End Sub
	
	Sub GetListofID (MinLvl As String, MaxLvl as String)
		dim url as String
		url  = "http://www.wowhead.com/?items&filter=qu=4;minle=" & MinLvl & ";maxle=" & MaxLvl & ";ub=6;cr=23:123:61:79;crs=3:3:3:3;crv=0:0:0:0"
		Dim data As Stream = client.OpenRead(URL)
		Dim reader As StreamReader = New StreamReader(data)
		Dim str As String = ""
		Dim tmp As String = ""
		dim num as Integer
		dim iList as String()
		Do Until reader.EndOfStream
			tmp = reader.ReadLine
			If instr(tmp,"?item=") Then	str += tmp
		Loop
		'debug.Print(str)
		Dim i As Integer
		If instr(str,"?item=") Then
			i= instr(str,"?item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,Chr(34) & ">") Then
				i= instr(tmp,Chr(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'debug.Print(tmp)
				num  += 1
				if num > 200 then debug.Print("trop de résultat pour" & MinLvl & "/" & MaxLvl )
			End If
		Next
	End Sub
	
	
	Sub GetListofGems ()
		dim url as String
		url  = "http://www.wowhead.com/?items=3&filter=qu=3:4;minle=75;cr=23:61:79:24:86;crs=3:3:3:3:7;crv=0:0:0:0:0#0+2+1"
		Dim data As Stream = client.OpenRead(URL)
		Dim reader As StreamReader = New StreamReader(data)
		Dim str As String = ""
		Dim tmp As String = ""
		dim num as Integer
		dim iList as String()
		Do Until reader.EndOfStream
			tmp = reader.ReadLine
			If instr(tmp,"?item=") Then	str += tmp
		Loop
		'debug.Print(str)
		Dim i As Integer
		If instr(str,"?item=") Then
			i= instr(str,"?item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,Chr(34) & ">") Then
				i= instr(tmp,Chr(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'debug.Print(tmp)
				num  += 1
				if num > 190 then debug.Print("trop de résultat ")
			End If
		Next
	End Sub
	
	
	
	Function GetXmlFromID(Id As String) As String
		
		Dim URL As String = "http://www.wowhead.com/?item=" & Id & "&xml"
		Dim data As Stream = client.OpenRead(URL)
		Dim reader As StreamReader = New StreamReader(data)
		Dim str As String = ""
		Do Until reader.EndOfStream
			str += reader.ReadLine
			'debug.Print(str)
		Loop
		return str
		'
	End Function
	Sub ExtractThisGem(myXMLstr As String)
		Dim myXML As New Xml.XmlDocument
		dim itemDB as New Xml.XmlDocument

		myXML.LoadXml(myXMLstr)
		
		
		
		
		
		dim id as String = myXML.SelectSingleNode("/wowhead/item").Attributes.GetNamedItem("id").Value
		Dim name As String = myXML.SelectSingleNode("/wowhead/item/name").InnerText
		Dim ilvl As String =  myXML.SelectSingleNode("/wowhead/item/level").InnerText
		Dim slot as String = myXML.SelectSingleNode("/wowhead/item/inventorySlot").Attributes.GetNamedItem("id").Value
		Dim heroic As String = 0
		if myXML.SelectSingleNode("/wowhead/item/json").InnerText.Contains("heroic:1") then heroic = 1
		Dim aStr As string()
		aStr = myXML.SelectSingleNode("/wowhead/item/jsonEquip").InnerText.Split(",")
		Dim Strength as String = GetValue(aStr,"str")
		Dim Agility as String= GetValue(aStr,"agi")
		Dim BonusArmor as String= GetValue(aStr,"armorbonus")
		Dim Armor As String= GetValue(aStr,"armor")
		Armor = Armor - BonusArmor
		Dim HasteRating As String= GetValue(aStr,"hastertng")
		Dim dps As String= GetValue(aStr,"dps")
		Dim speed As String= GetValue(aStr,"speed")
		Dim ExpertiseRating As String= GetValue(aStr,"exprtng")
		Dim HitRating as String= GetValue(aStr,"hitrtng")
		Dim AttackPower As String= GetValue(aStr,"atkpwr")
				
		Dim CritRating as String= GetValue(aStr,"critstrkrtng")
		Dim ArmorPenetrationRating as String= GetValue(aStr,"armorpenrtng")
		Dim setid as String= GetValue(aStr,"itemset")
		Dim gem1 as String= GetValue(aStr,"socket1")
		Dim gem2 as String= GetValue(aStr,"socket2")
		Dim gem3 as String= GetValue(aStr,"socket3")
		Dim gembonus as String= GetValue(aStr,"socketbonus")
		Dim classs As String = myXML.SelectSingleNode("/wowhead/item/class").Attributes.GetNamedItem("id").Value
		Dim subclass as String = myXML.SelectSingleNode("/wowhead/item/subclass ").Attributes.GetNamedItem("id").Value
		Dim keywords as String = ""
		Dim doc As xml.XmlDocument = New xml.XmlDocument

		'doc.Load("itemDB.xml")
		doc.Load("gems.xml")
		
		
		
		Dim root as xml.XmlElement = doc.DocumentElement
		Dim newElem As xml.XmlNode
		Dim newItem As xml.XmlNode
		dim newAttrib as Xml.XmlAttribute
		
		newItem = doc.CreateNode(xml.XmlNodeType.Element, "item", "")
		
		'newItem.InnerText = id
		root.AppendChild(newItem)
		newAttrib = doc.CreateAttribute(xml.XmlNodeType.Element, "id", "")
		newAttrib.Value=id
		newItem.Attributes.Append(newAttrib)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "id", "")
		newElem.InnerText = id
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "name", "")
		newElem.InnerText = name
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ilvl", "")
		newElem.InnerText = ilvl
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "class", "")
		newElem.InnerText = classs
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "subclass", "")
		newElem.InnerText = subclass
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Strength", "")
		newElem.InnerText = Strength
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Agility", "")
		newElem.InnerText = Agility
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HasteRating", "")
		newElem.InnerText = HasteRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ExpertiseRating", "")
		newElem.InnerText = ExpertiseRating
		newItem.AppendChild(newElem)

		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HitRating", "")
		newElem.InnerText = HitRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "AttackPower", "")
		newElem.InnerText = AttackPower
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CritRating", "")
		newElem.InnerText = CritRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ArmorPenetrationRating", "")
		newElem.InnerText = ArmorPenetrationRating
		newItem.AppendChild(newElem)
		
		If ArmorPenetrationRating <> "0" Then keywords += "ArmorPenetrationRating"
		If CritRating <> "0" Then keywords += "CritRating"
		If AttackPower <> "0" Then keywords += "AttackPower"
		If HitRating <> "0" Then keywords += "HitRating"
		If ExpertiseRating <> "0" Then keywords += "ExpertiseRating"
		If HasteRating <> "0" Then keywords += "HasteRating"
		If Agility <> "0" Then keywords += "Agility"
		If Strength <> "0" Then keywords += "Strength"
		If keywords = "" Then
			exit sub
		End If
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "keywords", "")
		newElem.InnerText = keywords
		newItem.AppendChild(newElem)

		doc.Save("gems.xml")
	End Sub
	
	
	
	
	Sub ExtractThis(myXMLstr As String)
		Dim myXML As New Xml.XmlDocument
		dim itemDB as New Xml.XmlDocument

		myXML.LoadXml(myXMLstr)
		dim id as String = myXML.SelectSingleNode("/wowhead/item").Attributes.GetNamedItem("id").Value
		Dim name As String = myXML.SelectSingleNode("/wowhead/item/name").InnerText
		Dim ilvl As String =  myXML.SelectSingleNode("/wowhead/item/level").InnerText
		Dim slot as String = myXML.SelectSingleNode("/wowhead/item/inventorySlot").Attributes.GetNamedItem("id").Value
		Dim heroic As String = 0
		if myXML.SelectSingleNode("/wowhead/item/json").InnerText.Contains("heroic:1") then heroic = 1
		Dim aStr As string()
		aStr = myXML.SelectSingleNode("/wowhead/item/jsonEquip").InnerText.Split(",")
		Dim Strength as String = GetValue(aStr,"str")
		Dim Agility as String= GetValue(aStr,"agi")
		Dim BonusArmor as String= GetValue(aStr,"armorbonus")
		Dim Armor As String= GetValue(aStr,"armor")
		Armor = Armor - BonusArmor
		Dim HasteRating As String= GetValue(aStr,"hastertng")
		Dim dps As String= GetValue(aStr,"dps")
		Dim speed As String= GetValue(aStr,"speed")
		Dim ExpertiseRating As String= GetValue(aStr,"exprtng")
		Dim HitRating as String= GetValue(aStr,"hitrtng")
		Dim AttackPower As String= GetValue(aStr,"atkpwr")
		
		Dim CritRating as String= GetValue(aStr,"critstrkrtng")
		Dim ArmorPenetrationRating as String= GetValue(aStr,"armorpenrtng")
		Dim setid as String= GetValue(aStr,"itemset")
		Dim gem1 as String= GetValue(aStr,"socket1")
		Dim gem2 as String= GetValue(aStr,"socket2")
		Dim gem3 as String= GetValue(aStr,"socket3")
		Dim gembonus as String= GetValue(aStr,"socketbonus")
		Dim classs As String = myXML.SelectSingleNode("/wowhead/item/class").Attributes.GetNamedItem("id").Value
		Dim subclass as String = myXML.SelectSingleNode("/wowhead/item/subclass ").Attributes.GetNamedItem("id").Value
		Dim keywords As String = ""
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument

		doc.Load("itemDB.xml")
		
		dim xNode as Xml.XmlNode
		
		Try
			xNode = doc.SelectSingleNode("/items/item[id="& id &"]")
			doc.RemoveChild (xNode)
		Catch
			
		End Try
		
		
		
		
		
		
		Dim root as xml.XmlElement = doc.DocumentElement
		Dim newElem As xml.XmlNode
		Dim newItem As xml.XmlNode
		dim newAttrib as Xml.XmlAttribute
		
		newItem = doc.CreateNode(xml.XmlNodeType.Element, "item", "")
		'newItem.InnerText = id
		root.AppendChild(newItem)
		
		newAttrib = doc.CreateAttribute(xml.XmlNodeType.Element, "id", "")
		newAttrib.Value=id
		newItem.Attributes.Append(newAttrib)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "id", "")
		newElem.InnerText = id
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "name", "")
		newElem.InnerText = name
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ilvl", "")
		newElem.InnerText = ilvl
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "slot", "")
		newElem.InnerText = slot
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "classs", "")
		newElem.InnerText = classs
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "subclass", "")
		newElem.InnerText = subclass
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "heroic", "")
		newElem.InnerText = heroic
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Strength", "")
		newElem.InnerText = Strength
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Agility", "")
		newElem.InnerText = Agility
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Armor", "")
		newElem.InnerText = Armor
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BonusArmor", "")
		newElem.InnerText = BonusArmor
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HasteRating", "")
		newElem.InnerText = HasteRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ExpertiseRating", "")
		newElem.InnerText = ExpertiseRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "dps", "")
		newElem.InnerText = dps
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "speed", "")
		newElem.InnerText = speed
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HitRating", "")
		newElem.InnerText = HitRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "AttackPower", "")
		newElem.InnerText = AttackPower
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CritRating", "")
		newElem.InnerText = CritRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ArmorPenetrationRating", "")
		newElem.InnerText = ArmorPenetrationRating
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "setid", "")
		newElem.InnerText = setid
		newItem.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "gem1", "")
		newElem.InnerText = gem1
		newItem.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "gem2", "")
		newElem.InnerText = gem2
		newItem.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "gem3", "")
		newElem.InnerText = gem3
		newItem.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "gembonus", "")
		newElem.InnerText = gembonus
		newItem.AppendChild(newElem)
		
		If ArmorPenetrationRating <> "0" Then keywords += "ArmorPenetrationRating"
		If CritRating <> "0" Then keywords += "CritRating"
		If AttackPower <> "0" Then keywords += "AttackPower"
		If HitRating <> "0" Then keywords += "HitRating"
		If ExpertiseRating <> "0" Then keywords += "ExpertiseRating"
		If HasteRating <> "0" Then keywords += "HasteRating"
		If Agility <> "0" Then keywords += "Agility"
		If Strength <> "0" Then keywords += "Strength"
		If keywords = "" Then
			exit sub
		End If
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "keywords", "")
		newElem.InnerText = keywords
		newItem.AppendChild(newElem)
		
		
		
		
		doc.Save("itemDB.xml")
		
		
		
	End Sub
	
	Function GetValue(jString As String(),attrib As String) As String
		Dim str As String
		For Each str In jString
			If str.StartsWith(attrib & ":") Then
				return str.Replace(attrib & ":","")
			End If
		Next
		return 0
	End Function
	
	
	
End Class
