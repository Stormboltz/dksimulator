'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Sigils
	Friend WildBuck As Boolean 'DC + 80
	Friend FrozenConscience As Boolean ' IT + 111
	Friend DarkRider As Boolean ' BS and HS + 90
	Friend ArthriticBinding As Boolean ' SS +203
	Friend Awareness As Boolean ' Scourge Strike + 189, Obliterate + 336, Deaths Strike by +
	Friend Strife As Boolean ' PS -> +120 AP for 10s
	Friend HauntedDreams As Boolean '15% chance on BS/HS to add 173 Crit Rating
	Friend VengefulHeart As Boolean
	Friend Virulence As Boolean
	
	Sub TryHauntedDreams()
		dim RNG as Double
		if HauntedDreams then
			RNG = sim.RandomNumberGenerator.RNGProc
			if RNG <= 0.15 then
				sim.Proc.HauntedDreamsFade = sim.TimeStamp + 10 * 100
			end if
		End If
	End Sub
end Class
