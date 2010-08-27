Imports System.Xml.Linq
Imports System.Linq


Public Class WowItem

    Property Id As Integer
    Property name As String
    Property ilvl As Integer
    Property slot As Integer
    Property classs As Integer
    Property subclass As Integer
    Property heroic As Integer

    Property Strength As Integer
    Property Intel As Integer
    Property Agility As Integer
    Property BonusArmor As Integer
    Property Armor As Integer
    Property HasteRating As Integer
    Property ExpertiseRating As Integer

    Property Stamina As Integer

    Property HitRating As Integer
    Property AttackPower As Integer
    Property CritRating As Integer
    Property ArmorPenetrationRating As Integer
    Property MasteryRating As Integer
    Property DodgeRating As Integer
    Property ParryRating As Integer

    Property Speed As String = "0"
    Property DPS As String = "0"

    Property icon As String

    Friend Desc As String
    Overridable Sub Load(ByVal el As XElement)
        Id = el.<id>.Value
        name = el.<name>.Value
        ilvl = el.<ilvl>.Value
        slot = el.<slot>.Value
        classs = el.<classs>.Value
        subclass = el.<subclass>.Value
        heroic = el.<heroic>.Value
        icon = el.<icon>.Value
        Strength = el.<Strength>.Value
        Agility = el.<Agility>.Value
        BonusArmor = el.<BonusArmor>.Value
        Armor = el.<Armor>.Value
        HasteRating = el.<HasteRating>.Value
        ExpertiseRating = el.<ExpertiseRating>.Value
        HitRating = el.<HitRating>.Value
        AttackPower = el.<AttackPower>.Value
        CritRating = el.<CritRating>.Value
        ArmorPenetrationRating = el.<ArmorPenetrationRating>.Value
        MasteryRating = el.<Mastery>.Value

        Speed = el.<speed>.Value
        DPS = el.<dps>.Value

        Armor = el.<Armor>.Value
        BonusArmor = el.<BonusArmor>.Value
        DodgeRating = el.<Dodge>.Value
        ParryRating = el.<Parry>.Value
        Stamina = el.<Stamina>.Value
    End Sub
    Overridable Sub Unload()
        Id = 0
        name = ""
        ilvl = 0
        slot = 0
        classs = 0
        subclass = 0
        heroic = 0
        Strength = 0
        Agility = 0
        BonusArmor = 0
        Armor = 0
        HasteRating = 0
        ExpertiseRating = 0
        HitRating = 0
        AttackPower = 0
        CritRating = 0
        ArmorPenetrationRating = 0
        Speed = 0
        DPS = 0
        icon = ""
    End Sub


End Class
