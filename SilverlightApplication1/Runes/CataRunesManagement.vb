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
            Dim bArray As New collection
            If BloodRunes.NextAvailableTime > T And BloodRunes.death = True Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.NextAvailableTime > T And FrostRunes.death = True Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.NextAvailableTime > T Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                'bArray.Sort()
                Return bArray.Item(0)
            End If
        End Function



        Function CataGetNextFrost(ByVal T As Long) As Long
            Dim bArray As New collection
            If BloodRunes.AvailableTime > T And BloodRunes.death = True Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.AvailableTime > T Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.AvailableTime > T And UnholyRunes.death = True Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                'bArray.Sort()
                Return bArray.Item(0)
            End If
        End Function




        Function CataGetNextBloodCD(ByVal T As Long) As Long
            Dim bArray As New collection

            If BloodRunes.AvailableTime > T Then bArray.Add(BloodRunes.AvailableTime)
            If FrostRunes.AvailableTime > T And FrostRunes.death = True Then bArray.Add(FrostRunes.AvailableTime)
            If UnholyRunes.AvailableTime > T And UnholyRune1.death = True Then bArray.Add(UnholyRunes.AvailableTime)
            If bArray.Count > 0 Then
                'bArray.Sort()
                Return bArray.Item(0)
            End If

        End Function
	
	
	
	Function CataAnyBlood(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.reserved=false Then return  True
		If FrostRunes.Available And FrostRunes.death = True and FrostRunes.reserved=false Then return  True
		If UnholyRunes.Available And UnholyRunes.death = True and UnholyRunes.reserved=false Then return  True
	End Function
	
	
	Function CataBlood(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = False and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available  And FrostRunes.death = True and FrostRunes.reserved=false Then return  True
		If UnholyRunes.Available  And UnholyRunes.death = True and UnholyRunes.reserved=false Then return  True
	End Function
	
	Function CataBloodOnly(T as long) As Boolean
		If BloodRunes.Available Then return True
	End Function
	
	
	Function CataFrostOnly(T As Long) As Boolean
		If FrostRunes.Available and FrostRunes.reserved=false Then return True
	End Function
	
	Function CataUnholyOnly(T as long) As Boolean
		If UnholyRunes.Available and UnholyRunes.reserved=false Then return True
	End Function
	
	
	Function CataFrost(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = True and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available and FrostRunes.reserved=false Then return True
		If UnholyRunes.Available And UnholyRunes.death = True and UnholyRunes.reserved=false Then return True
	End Function
	
	Function CataUnholy(T as long) As Boolean
		If BloodRunes.Available And BloodRunes.death = True and BloodRunes.reserved=false Then return  True
		If FrostRunes.Available And FrostRunes.death = True and FrostRunes.reserved=false Then return True
		If UnholyRunes.Available and UnholyRunes.reserved=false Then return True
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
		
	End Function
	
	Function CataUseDeathBlood(T As Long,Death As Boolean) As Boolean
		If BloodRunes.Available And BloodRune1.death Then
			BloodRunes.Use(T,Death)
		Else
                Diagnostics.Debug.WriteLine("Error in CataUseDeathBlood")
            End If
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
        End Function

	
	Function CataReserveFU(T As Long) As Boolean
		FrostRunes.reserved = True
		UnholyRunes.reserved = True
	End Function	
		
	Function CataUnReserveFU(T As long) as Boolean
		FrostRunes.reserved=False
		UnholyRunes.reserved=False
		BloodRunes.reserved=False
	End Function
	
	
	Function CataDRMFU( T As Long) As Boolean
		If UnholyRunes.Available And FrostRunes.Available and UnholyRunes.death = False and FrostRunes.death = False Then
			return true
		End If
	End Function
	
	Function CataRuneRefreshTheNextGCD(T as long) As Boolean
		Dim tmp As Long
		
		tmp = sim.NextFreeGCD
		If bloodrunes.Available = False And BloodRune1.AvailableTime < tmp Then Return False
		If frostrunes.Available = False And frostrunes.AvailableTime < tmp Then Return False
		If unholyrunes.Available = False And unholyrunes.AvailableTime < tmp Then Return False
		return true
	End Function
End Class
End Namespace