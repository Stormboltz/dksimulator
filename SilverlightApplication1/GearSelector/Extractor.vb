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
	Friend col As New collection
	Friend BonusScanned as New collection
	
	
	Sub Start
		dim i as Integer
		init
		GenerateWowheadFilter
		'GenerateWowheadFilterMAil
		'GetListofGems
		Dim str As String
		col.Sort
		
		For Each str In col
			ExtractThis(GetXmlFromID(str))
			Diagnostics.Debug.WriteLine (i & "/"  & col.Count)
			i +=1
		Next
	End Sub
	Sub GemExtrator
		Dim i As Integer
		Dim str As String
		col.Clear
		GetListofGems
		For Each str In col
			ExtractThisGem(GetXmlFromID(str))
			'Diagnostics.Debug.WriteLine (i & "/"  & col.Count)
			i+=1
		Next
		col.Clear
	End Sub
	
	Sub init()
		client = New WebClient()
		client.Proxy = WebRequest.GetSystemWebProxy
		client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials
	End Sub
	
	
	Sub GetSigils
		GetListofSigils
		Dim i As Integer
		Dim str As String
		
		For Each str In col
			ExtractThis(GetXmlFromID(str),false)
			Diagnostics.Debug.WriteLine (i & "/"  & col.Count)
			i +=1
		Next
		
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
	
	Sub GenerateWowheadFilterMAil()
		GetListofMail
	End Sub
	
	Sub GetListofSigils
		dim url as String
		url  = "http://www.wowhead.com/?items=4.10&filter=qu=4"
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
		'Diagnostics.Debug.WriteLine(str)
		Dim i As Integer
		If instr(str,"?item=") Then
			i= instr(str,"?item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,convert.tochar(34) & ">") Then
				i= instr(tmp,convert.tochar(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'Diagnostics.Debug.WriteLine(tmp)
				num  += 1
				if num > 180 then Diagnostics.Debug.WriteLine("trop de résultat " )
			End If
		Next
	End Sub
	Sub GetListofMail ()
		dim url as String
		url  = "http://www.wowhead.com/?items=4.3&filter=qu=4;minle=251;maxle=290;ub=6;cr=123:61:79;crs=3:3:3;crv=0:0:0"
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
		'Diagnostics.Debug.WriteLine(str)
		Dim i As Integer
		If instr(str,"/item=") Then
			i= instr(str,"/item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,convert.tochar(34) & ">") Then
				i= instr(tmp,convert.tochar(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'Diagnostics.Debug.WriteLine(tmp)
				num  += 1
				if num > 180 then Diagnostics.Debug.WriteLine("trop de résultat")
			End If
		Next
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
			'Diagnostics.Debug.WriteLine(tmp)
			If instr(tmp,"/item=") Then	str += tmp
		Loop
		'Diagnostics.Debug.WriteLine(str)
		Dim i As Integer
		If instr(str,"/item=") Then
			i= instr(str,"/item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,convert.tochar(34) & ">") Then
				i= instr(tmp,convert.tochar(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'Diagnostics.Debug.WriteLine(tmp)
				num  += 1
				if num > 180 then Diagnostics.Debug.WriteLine("trop de résultat pour" & MinLvl & "/" & MaxLvl )
			End If
		Next
	End Sub
	
	
	Sub GetListofGems ()
		dim url as String
		url  = "http://www.wowhead.com/?items=3&filter=qu=3:4;minle=75;cr=23:61:79:24:86;crs=3:3:3:3:7;crv=0:0:0:0:0#0+2+1"
			   'http://www.wowhead.com/?items=3&filter=qu=4;minle=75;cr=23:61:79:24:86;crs=3:3:3:3:7;crv=0:0:0:0:0#0+1
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
		'Diagnostics.Debug.WriteLine(str)
		Dim i As Integer
		If instr(str,"?item=") Then
			i= instr(str,"?item=")
			str = right(str,str.Length-i)
		End If
		iList = str.Split("=")
		num = 0
		For Each tmp In iList
			If instr(tmp,convert.tochar(34) & ">") Then
				i= instr(tmp,convert.tochar(34))
				tmp = left(tmp,i-1)
				col.Add (tmp)
				'Diagnostics.Debug.WriteLine(tmp)
				num  += 1
				if num > 190 then Diagnostics.Debug.WriteLine("trop de résultat ")
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
			'Diagnostics.Debug.WriteLine(str)
		Loop
		return str
		'
	End Function
	Sub ExtractThisGem(myXMLstr As String)
		Dim myXML As New xdocument
		dim itemDB as New xdocument

		myXML.parse(myXMLstr)
		
		
		
		
		
        Dim id As String = myXML.Element("/wowhead/item").Attribute("id").Value
        Dim name As String = myXML.Element("/wowhead/item/name").Value
        Dim ilvl As String = myXML.Element("/wowhead/item/level").Value
        Dim quality As String = myXML.Element("/wowhead/item/quality").Attribute("id").Value
        Dim slot As String = myXML.Element("/wowhead/item/inventorySlot").Attribute("id").Value
        Dim heroic As String = 0
        If myXML.Element("/wowhead/item/json").Value.Contains("heroic:1") Then heroic = 1
        Dim aStr As String()
        aStr = myXML.Element("/wowhead/item/jsonEquip").Value.Split(",")
        Dim Strength As String = GetValue(aStr, "str")
        Dim Agility As String = GetValue(aStr, "agi")
        Dim BonusArmor As String = GetValue(aStr, "armorbonus")
        Dim Armor As String = GetValue(aStr, "armor")
        Armor = Armor - BonusArmor
        Dim HasteRating As String = GetValue(aStr, "hastertng")
        Dim dps As String = GetValue(aStr, "dps")
        Dim speed As String = GetValue(aStr, "speed")
        Dim ExpertiseRating As String = GetValue(aStr, "exprtng")
        Dim HitRating As String = GetValue(aStr, "hitrtng")
        Dim AttackPower As String = GetValue(aStr, "atkpwr")

        Dim CritRating As String = GetValue(aStr, "critstrkrtng")
        Dim ArmorPenetrationRating As String = GetValue(aStr, "armorpenrtng")
        Dim setid As String = GetValue(aStr, "itemset")
        Dim gem1 As String = GetValue(aStr, "socket1")
        Dim gem2 As String = GetValue(aStr, "socket2")
        Dim gem3 As String = GetValue(aStr, "socket3")
        Dim gembonus As String = GetValue(aStr, "socketbonus")
        Dim classs As String = myXML.Element("/wowhead/item/class").Attribute("id").Value
        Dim subclass As String = myXML.Element("/wowhead/item/subclass ").Attribute("id").Value
        Dim reqskill As String = GetValue(aStr, "reqskill")
        Dim keywords As String = ""
        Dim doc As xdocument = New xdocument


        doc.Load( "\GearSelector\" & "gems.xml")

        Dim xNode As xElement
        Try
            xNode = doc.Element("/gems/item[id=" & id & "]")
            xNode.ParentNode.RemoveChild(xNode)
        Catch e As Exception
            Diagnostics.Debug.WriteLine("Didn't found " & id)
            'Diagnostics.Debug.WriteLine (e.ToString)
        End Try


        Dim root As xml.XmlElement = doc.DocumentElement
        Dim newElem As xElement
        Dim newItem As xElement
        Dim newAttrib As Xml.XmlAttribute

        newItem = doc.CreateNode(xElementType.Element, "item", "")

        'newItem.Value = id
        root.AppendChild(newItem)
        newAttrib = doc.CreateAttribute(xElementType.Element, "id", "")
        newAttrib.Value = id
        newItem.Attributes.Append(newAttrib)

        newElem = doc.CreateNode(xElementType.Element, "id", "")
        newElem.Value = id
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "name", "")
        newElem.Value = name
        newItem.AppendChild(newElem)


        newElem = doc.CreateNode(xElementType.Element, "quality", "")
        newElem.Value = quality
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ilvl", "")
        newElem.Value = ilvl
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "class", "")
        newElem.Value = classs
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "subclass", "")
        newElem.Value = subclass
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "Strength", "")
        newElem.Value = Strength
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "Agility", "")
        newElem.Value = Agility
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "HasteRating", "")
        newElem.Value = HasteRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ExpertiseRating", "")
        newElem.Value = ExpertiseRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "HitRating", "")
        newElem.Value = HitRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "AttackPower", "")
        newElem.Value = AttackPower
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "CritRating", "")
        newElem.Value = CritRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ArmorPenetrationRating", "")
        newElem.Value = ArmorPenetrationRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "reqskill", "")
        newElem.Value = reqskill
        newItem.AppendChild(newElem)




        If ArmorPenetrationRating <> "0" Then keywords += "ArmorPenetrationRatingArp"
        If CritRating <> "0" Then keywords += "CritRating"
        If AttackPower <> "0" Then keywords += "AttackPower"
        If HitRating <> "0" Then keywords += "HitRating"
        If ExpertiseRating <> "0" Then keywords += "ExpertiseRating"
        If HasteRating <> "0" Then keywords += "HasteRating"
        If Agility <> "0" Then keywords += "Agility"
        If Strength <> "0" Then keywords += "Strength"
        If keywords = "" Then
            Diagnostics.Debug.WriteLine("skipping " & name)
            Exit Sub
        End If

        newElem = doc.CreateNode(xElementType.Element, "keywords", "")
        newElem.Value = keywords
        newItem.AppendChild(newElem)

        doc.Save( "\GearSelector\" & "gems.xml")
    End Sub




    Sub ExtractThis(ByVal myXMLstr As String, Optional ByVal SkipIfEmpty As Boolean = True)
        Dim myXML As New xdocument
        Dim itemDB As New xdocument

        myXML.parse(myXMLstr)
        Dim id As String = myXML.Element("/wowhead/item").Attribute("id").Value
        Dim name As String = myXML.Element("/wowhead/item/name").Value
        Dim ilvl As String = myXML.Element("/wowhead/item/level").Value
        Dim slot As String = myXML.Element("/wowhead/item/inventorySlot").Attribute("id").Value
        Dim heroic As String = 0
        If myXML.Element("/wowhead/item/json").Value.Contains("heroic:1") Then heroic = 1
        Dim aStr As String()
        aStr = myXML.Element("/wowhead/item/jsonEquip").Value.Replace(convert.tochar(34), "").Split(",")
        Dim Strength As String = GetValue(aStr, "str")
        Dim Agility As String = GetValue(aStr, "agi")
        Dim BonusArmor As String = GetValue(aStr, "armorbonus")
        Dim Armor As String = GetValue(aStr, "armor")
        Armor = Armor - BonusArmor
        Dim HasteRating As String = GetValue(aStr, "hastertng")
        Dim dps As String = GetValue(aStr, "dps")
        Dim speed As String = GetValue(aStr, "speed")
        Dim ExpertiseRating As String = GetValue(aStr, "exprtng")
        Dim HitRating As String = GetValue(aStr, "hitrtng")
        Dim AttackPower As String = GetValue(aStr, "atkpwr")

        Dim CritRating As String = GetValue(aStr, "critstrkrtng")
        Dim ArmorPenetrationRating As String = GetValue(aStr, "armorpenrtng")
        Dim setid As String = GetValue(aStr, "itemset")
        Dim gem1 As String = GetValue(aStr, "socket1")
        Dim gem2 As String = GetValue(aStr, "socket2")
        Dim gem3 As String = GetValue(aStr, "socket3")
        Dim gembonus As String = GetValue(aStr, "socketbonus")
        Dim classs As String = myXML.Element("/wowhead/item/class").Attribute("id").Value
        Dim subclass As String = myXML.Element("/wowhead/item/subclass ").Attribute("id").Value
        Dim keywords As String = ""

        Dim doc As xdocument = New xdocument

        doc.Load( "\GearSelector\" & "itemDB.xml")

        Dim xNode As xElement

        Try
            xNode = doc.Element("/items/item[id=" & id & "]")
            xNode.ParentNode.RemoveChild(xNode)
        Catch

        End Try






        Dim root As xml.XmlElement = doc.DocumentElement
        Dim newElem As xElement
        Dim newItem As xElement
        Dim newAttrib As Xml.XmlAttribute

        newItem = doc.CreateNode(xElementType.Element, "item", "")
        'newItem.Value = id
        root.AppendChild(newItem)

        newAttrib = doc.CreateAttribute(xElementType.Element, "id", "")
        newAttrib.Value = id
        newItem.Attributes.Append(newAttrib)

        newElem = doc.CreateNode(xElementType.Element, "id", "")
        newElem.Value = id
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "name", "")
        newElem.Value = name
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ilvl", "")
        newElem.Value = ilvl
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "slot", "")
        newElem.Value = slot
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "classs", "")
        newElem.Value = classs
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "subclass", "")
        newElem.Value = subclass
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "heroic", "")
        newElem.Value = heroic
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "Strength", "")
        newElem.Value = Strength
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "Agility", "")
        newElem.Value = Agility
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "Armor", "")
        newElem.Value = Armor
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "BonusArmor", "")
        newElem.Value = BonusArmor
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "HasteRating", "")
        newElem.Value = HasteRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ExpertiseRating", "")
        newElem.Value = ExpertiseRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "dps", "")
        newElem.Value = dps
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "speed", "")
        newElem.Value = speed
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "HitRating", "")
        newElem.Value = HitRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "AttackPower", "")
        newElem.Value = AttackPower
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "CritRating", "")
        newElem.Value = CritRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "ArmorPenetrationRating", "")
        newElem.Value = ArmorPenetrationRating
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "setid", "")
        newElem.Value = setid
        newItem.AppendChild(newElem)

        newElem = doc.CreateNode(xElementType.Element, "gem1", "")
        newElem.Value = gem1
        newItem.AppendChild(newElem)
        newElem = doc.CreateNode(xElementType.Element, "gem2", "")
        newElem.Value = gem2
        newItem.AppendChild(newElem)
        newElem = doc.CreateNode(xElementType.Element, "gem3", "")
        newElem.Value = gem3
        newItem.AppendChild(newElem)
        newElem = doc.CreateNode(xElementType.Element, "gembonus", "")
        newElem.Value = gembonus
        newItem.AppendChild(newElem)

        If ArmorPenetrationRating <> "0" Then keywords += "ArmorPenetrationRatingArP"
        If CritRating <> "0" Then keywords += "CritRating"
        If AttackPower <> "0" Then keywords += "AttackPower"
        If HitRating <> "0" Then keywords += "HitRating"
        If ExpertiseRating <> "0" Then keywords += "ExpertiseRating"
        If HasteRating <> "0" Then keywords += "HasteRating"
        If Agility <> "0" Then keywords += "Agility"
        If Strength <> "0" Then keywords += "Strength"
        If keywords = "" Then
            If SkipIfEmpty Then Exit Sub
        End If
        newElem = doc.CreateNode(xElementType.Element, "keywords", "")
        newElem.Value = keywords
        newItem.AppendChild(newElem)
        doc.Save( "\GearSelector\" & "itemDB.xml")
    End Sub

    Function GetValue(ByVal jString As String(), ByVal attrib As String) As String
        Dim str As String
        For Each str In jString
            If str.StartsWith(attrib & ":") Then
                Return str.Replace(attrib & ":", "")
            End If
        Next
        Return 0
    End Function

    Sub ListBonus()
        Dim xDoc As New xdocument
        Dim txDoc As New xdocument
        Dim xList As xElementList
        Dim xnode As xElement
        Dim WowheadXml As String
        Dim bns As String

        Dim xGemBonus As New xdocument

        xGemBonus.parse("<bonus></bonus>")

        xdoc.Load("\GearSelector\" & "itemDB.xml")
        xList = xdoc.SelectNodes("/items/item[gembonus!=0]")
        For Each xnode In xList
            txDoc.parse(xNode.OuterXml)
            If BonusScanned.Contains(txDoc.Element("/item/gembonus").Value) Then
                GoTo NextOne
            End If



            BonusScanned.Add(txDoc.Element("/item/gembonus").Value)
            WowheadXml = GetXmlFromID(txDoc.Element("/item/id").Value)
            bns = getSocketbonus(WowheadXml)
            'Diagnostics.Debug.WriteLine(txDoc.Element("/item/gembonus").Value & "= " & bns )


            Dim root As xml.XmlElement = xGemBonus.DocumentElement
            Dim newElem As xElement
            Dim newItem As xElement


            Dim Strength As String = 0
            Dim Agility As String = 0
            Dim HasteRating As String = 0
            Dim ExpertiseRating As String = 0
            Dim HitRating As String = 0
            Dim CritRating As String = 0
            Dim AttackPower As String = 0
            Dim ArmorPenetrationRating As String = 0
            Dim Desc As String = bns
            Dim keywords As String = ""

            Dim i As Integer

            i = instr(bns, convert.tochar(32))
            Select Case bns.Substring(i)
                Case "Strength"
                    Strength = left(bns, i).Trim
                Case "Agility"
                    Agility = left(bns, i).Trim
                Case "Expertise Rating"
                    ExpertiseRating = left(bns, i).Trim
                Case "Hit Rating"
                    HitRating = left(bns, i)
                Case "Critical Strike Rating"
                    CritRating = left(bns, i).Trim
                Case "Attack Power"
                    AttackPower = left(bns, i).Trim
                Case "Haste Rating"
                    HasteRating = left(bns, i).Trim
                Case "Armor Penetration Rating"
                    ArmorPenetrationRating = left(bns, i).Trim
                Case Else
                    'Diagnostics.Debug.WriteLine(bns.Substring(i) & " N/A")
            End Select

            If ArmorPenetrationRating <> "0" Then keywords += "ArmorPenetrationRatingArP"
            If CritRating <> "0" Then keywords += "CritRating"
            If AttackPower <> "0" Then keywords += "AttackPower"
            If HitRating <> "0" Then keywords += "HitRating"
            If ExpertiseRating <> "0" Then keywords += "ExpertiseRating"
            If HasteRating <> "0" Then keywords += "HasteRating"
            If Agility <> "0" Then keywords += "Agility"
            If Strength <> "0" Then keywords += "Strength"
            If keywords = "" Then
                GoTo NextOne
            End If





            newItem = xGemBonus.CreateNode(xElementType.Element, "item", "")
            root.AppendChild(newItem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "id", "")
            newElem.Value = txDoc.Element("/item/gembonus").Value
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "Strength", "")
            newElem.Value = Strength
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "Agility", "")
            newElem.Value = Agility
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "HasteRating", "")
            newElem.Value = HasteRating
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "ExpertiseRating", "")
            newElem.Value = ExpertiseRating
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "HitRating", "")
            newElem.Value = HitRating
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "AttackPower", "")
            newElem.Value = AttackPower
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "CritRating", "")
            newElem.Value = CritRating
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "ArmorPenetrationRating", "")
            newElem.Value = ArmorPenetrationRating
            newItem.AppendChild(newElem)

            newElem = xGemBonus.CreateNode(xElementType.Element, "Desc", "")
            newElem.Value = Desc
            newItem.AppendChild(newElem)




NextOne:

        Next
        xGemBonus.Save("GemBonus.xml")
    End Sub
	
	Function getSocketbonus(str As String) as String
		Dim i As Integer
		dim j as Integer
		i = InStr(str,"Socket Bonus:")
		i=i+14
		j = InStr(i,str,"<")
		return str.Substring(i,j-i-1)
	End Function
	
	
	
End Class
