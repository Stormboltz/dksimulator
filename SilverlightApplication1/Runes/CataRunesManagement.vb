'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/13/2010
' Heure: 3:52 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Runes
Friend Partial Class runes
        Friend BloodRunes As CataRune
        Friend FrostRunes As CataRune
        Friend UnholyRunes As CataRune
	
	
	Sub FillRunes
		BloodRunes.Refill(0.1)
		FrostRunes.Refill(0.1)
		UnholyRunes.Refill(0.1)
		sim.FutureEventManager.Add(sim.TimeStamp+10,"RuneFill")
	End sub
	
	
	
	
	Function CataRuneState As String
		Dim T As Long
		T = sim.TimeStamp
		Dim tmp As String
		tmp = "["
		tmp = tmp & BloodRunes.Value
		tmp = tmp & ":" & FrostRunes.Value
		tmp = tmp & ":" & UnholyRunes.Value
		tmp = tmp & "]"
		return tmp
	End Function
	
	Function cataBFU (T As Long) As Boolean
		If BloodRunes.Value >= 100 And FrostRunes.Value >= 100 And UnholyRunes.Value >= 100 Then
			Return True
		Else
			return false
		End If
	End Function
	Function CataGetNextUnholy(T As Long) As Long
            Dim bArray As New Collections.Generic.List(Of Long)
            If BloodRunes.NextAvailableTime > T And BloodRunes.death = True Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.NextAvailableTime > T And FrostRunes.death = True Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.NextAvailableTime > T Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            End If
            Return T
        End Function



        Function CataGetNextFrost(ByVal T As Long) As Long
            Dim bArray As New Collections.Generic.List(Of Long)
            If BloodRunes.AvailableTime > T And BloodRunes.death = True Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.AvailableTime > T Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.AvailableTime > T And UnholyRunes.death = True Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            End If
            Return T
        End Function




        Function CataGetNextBloodCD(ByVal T As Long) As Long
            Dim bArray As New Collections.Generic.List(Of Long)

            If BloodRunes.AvailableTime > T Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.AvailableTime > T And FrostRunes.death = True Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.AvailableTime > T And UnholyRunes.death = True Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                bArray.Sort()
                Return bArray.Item(0)
            End If
            Return T
        End Function
	
	
	
	Function CataAnyBlood(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.reserved=false Then return  True
		If FrostRunes.Available And FrostRunes.death = True and FrostRunes.reserved=false Then return  True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
	End Function
	
	
	Function CataBlood(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = False and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available  And FrostRunes.death = True and FrostRunes.reserved=false Then return  True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
	End Function
	
	Function CataBloodOnly(T as long) As Boolean
            If BloodRunes.Available Then Return True
            Return False
	End Function
	
	
	Function CataFrostOnly(T As Long) As Boolean
            If FrostRunes.Available And FrostRunes.reserved = False Then Return True
            Return False
	End Function
	
	Function CataUnholyOnly(T as long) As Boolean
            If UnholyRunes.Available And UnholyRunes.reserved = False Then Return True
            Return False
	End Function
	
	
	Function CataFrost(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = True and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available and FrostRunes.reserved=false Then return True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
	End Function
	
	Function CataUnholy(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = True and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available And FrostRunes.death = True and FrostRunes.reserved=false Then return True
            If UnholyRunes.Available And UnholyRunes.reserved = False Then Return True
            Return False
	End Function
	
	Function CataFU(T As long) As Boolean
		If UnholyRunes.Available And FrostRunes.Available Then
			return true
		End If
		If UnholyRunes.Available And BloodRunes.Available and BloodRunes.death Then
			return true
		End If
		If  BloodRunes.AvailableTwice and BloodRunes.death Then
			return true
		End If
            Return False
	End Function
	
	Function CataUseDeathBlood(T As Long,Death As Boolean) As Boolean
		If BloodRunes.Available And BloodRune1.death Then
			BloodRunes.Use(T,Death)
		Else
                Diagnostics.Debug.WriteLine("Error in CataUseDeathBlood")
            End If
            Return False
        End Function




        Function CataUseBlood(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If BloodRunes.Available Then
                BloodRunes.Use(T, Death)
                Return True
            End If
            If FrostRunes.Available And FrostRunes.death Then
                FrostRunes.Use(T, Death)
                Return True
            End If
            If UnholyRunes.Available And UnholyRunes.death Then
                UnholyRunes.Use(T, Death)
                Return True
            End If
            Diagnostics.Debug.WriteLine("ERROR Blood rune")
            Return False
        End Function

        Function CataUseFrost(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If FrostRunes.Available Then
                FrostRunes.Use(T, Death)
                Return True
            End If
            If BloodRunes.Available And BloodRunes.death Then
                BloodRunes.Use(T, Death)
                Return True
            End If
            If UnholyRunes.Available And UnholyRunes.death Then
                UnholyRunes.Use(T, Death)
                Return True
            End If
            Diagnostics.Debug.WriteLine("ERROR FROST RUNE")
            Return False
        End Function

        Function CataUseUnholy(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If UnholyRunes.Available Then
                UnholyRunes.Use(T, Death)
                Return True
            End If
            If BloodRunes.Available And BloodRunes.death Then
                BloodRunes.Use(T, Death)
                Return True
            End If
            If FrostRunes.Available And FrostRunes.death Then
                FrostRunes.Use(T, Death)
                Return True
            End If
            Diagnostics.Debug.WriteLine("ERROR Unholy RUNE")
            Return False
        End Function
        Function CataUseFU(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If UnholyRunes.Available And FrostRunes.Available Then
                UnholyRunes.Use(T, Death)
                FrostRunes.Use(T, Death)
                Return True
            End If
            If UnholyRunes.Available And BloodRunes.Available And BloodRunes.death Then
                UnholyRunes.Use(T, Death)
                BloodRunes.Use(T, Death)
                Return True
            End If
            If BloodRunes.AvailableTwice And BloodRunes.death Then
                BloodRunes.Use(T, Death)
                BloodRunes.Use(T, Death)
                Return True
            End If
            Diagnostics.Debug.WriteLine("ERRRRRROOOOORRRR FU @ :" & T)
            Return False
        End Function

	
	Function CataReserveFU(T As Long) As Boolean
		FrostRunes.reserved = True
            UnholyRunes.reserved = True
            Return False
	End Function	
		
	Function CataUnReserveFU(T As long) as Boolean
		FrostRunes.reserved=False
		UnholyRunes.reserved=False
            BloodRunes.reserved = False
            Return False
	End Function
	
	
	Function CataDRMFU( T As Long) As Boolean
            If UnholyRunes.Available And FrostRunes.Available And UnholyRunes.death = False And FrostRunes.death = False Then
                Return True
            End If
            Return False
	End Function
	
	Function CataRuneRefreshTheNextGCD(T as long) As Boolean
		Dim tmp As Long
		
		tmp = sim.NextFreeGCD
            If BloodRunes.Available = False And BloodRunes.AvailableTime < tmp Then Return False
            If FrostRunes.Available = False And FrostRunes.AvailableTime < tmp Then Return False
            If UnholyRunes.Available = False And UnholyRunes.AvailableTime < tmp Then Return False
		return true
	End Function
End Class
End Namespace