Namespace Simulator
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
	Friend HangedMan as Boolean
	
	Private Sim as Sim
	Sub New(S As Sim )
		Sim = S
	End Sub
	
end Class
End Namespace