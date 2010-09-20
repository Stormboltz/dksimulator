'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/13/2010
' Heure: 3:52 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Runes
    Partial Friend Class runes

        Function cataBFU(ByVal T As Long) As Boolean
            If BloodRunes.Value >= 100 And FrostRunes.Value >= 100 And UnholyRunes.Value >= 100 Then
                Return True
            Else
                Return False
            End If
        End Function
        Function CataGetNextUnholy(ByVal T As Long) As Long
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



        Function CataAnyBlood(ByVal T As Long) As Boolean
            If BloodRunes.Available And BloodRunes.reserved = False Then Return True
            If FrostRunes.Available And FrostRunes.death = True And FrostRunes.reserved = False Then Return True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
        End Function


        Function CataBlood(ByVal T As Long) As Boolean
            If BloodRunes.Available And BloodRunes.death = False And BloodRunes.reserved = False Then Return True
            If FrostRunes.Available And FrostRunes.death = True And FrostRunes.reserved = False Then Return True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
        End Function

        Function CataBloodOnly(ByVal T As Long) As Boolean
            If BloodRunes.Available Then Return True
            Return False
        End Function


        Function CataFrostOnly(ByVal T As Long) As Boolean
            If FrostRunes.Available And FrostRunes.reserved = False Then Return True
            Return False
        End Function

        Function CataUnholyOnly(ByVal T As Long) As Boolean
            If UnholyRunes.Available And UnholyRunes.reserved = False Then Return True
            Return False
        End Function


        Function CataFrost(ByVal T As Long) As Boolean
            If BloodRunes.Available And BloodRunes.death = True And BloodRunes.reserved = False Then Return True
            If FrostRunes.Available And FrostRunes.reserved = False Then Return True
            If UnholyRunes.Available And UnholyRunes.death = True And UnholyRunes.reserved = False Then Return True
            Return False
        End Function

        Function CataUnholy(ByVal T As Long) As Boolean
            If BloodRunes.Available And BloodRunes.death = True And BloodRunes.reserved = False Then Return True
            If FrostRunes.Available And FrostRunes.death = True And FrostRunes.reserved = False Then Return True
            If UnholyRunes.Available And UnholyRunes.reserved = False Then Return True
            Return False
        End Function

        Function CataFU(ByVal T As Long) As Boolean
            If UnholyRunes.Available And FrostRunes.Available Then
                Return True
            End If
            If UnholyRunes.Available And BloodRunes.Available And BloodRunes.death Then
                Return True
            End If
            If BloodRunes.AvailableTwice And BloodRunes.death Then
                Return True
            End If
            Return False
        End Function

        Function CataUseDeathBlood(ByVal T As Long, ByVal Death As Boolean) As Boolean
            If BloodRunes.Available And BloodRune1.death Then
                BloodRunes.Use(T, Death)
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


        Function CataReserveFU(ByVal T As Long) As Boolean
            FrostRunes.reserved = True
            UnholyRunes.reserved = True
            Return False
        End Function

        Function CataUnReserveFU(ByVal T As Long) As Boolean
            FrostRunes.reserved = False
            UnholyRunes.reserved = False
            BloodRunes.reserved = False
            Return False
        End Function


        Function CataDRMFU(ByVal T As Long) As Boolean
            If UnholyRunes.Available And FrostRunes.Available And UnholyRunes.death = False And FrostRunes.death = False Then
                Return True
            End If
            Return False
        End Function

        Function CataRuneRefreshTheNextGCD(ByVal T As Long) As Boolean
            Dim tmp As Long
            tmp = sim.NextFreeGCD
            If BloodRunes.Available = False And BloodRunes.AvailableTime < tmp Then Return False
            If FrostRunes.Available = False And FrostRunes.AvailableTime < tmp Then Return False
            If UnholyRunes.Available = False And UnholyRunes.AvailableTime < tmp Then Return False
            Return True
        End Function
    End Class
End Namespace