Public Class ReportLine
    Property Ability As String
    Property Damage_done_Total As Long
    Property Damage_done_Pc As Long
    Property Damage_done_Count As Long
    Property Damage_done_Avg As Long


    Property hit_count As Long
    Property hit_count_Pc As Long
    Property hit_count_Avg As Long

    Property Crit_count As Long
    Property Crit_count_Pc As Long
    Property Crit_count_Avg As Long

    Property Miss_Count As Long
    Property Miss_Count_Pc As Long




    Property Glance_Count As Long
    Property Glance_Count_Pc As Long
    Property Glance_Count_Avg As Long

    Property Uptime As Long

    Property TPS As Long

    Property TotalHit As Long
    Property TotalCrit As Long
    Property TotalGlance As Long
    Sub New()

    End Sub
    Overridable Function InnerText() As String
        Dim tmp As String = "<row>"

        tmp += "<Ability>" & Ability & "</Ability>"
        tmp += "<Damage_done_Total>" & Damage_done_Total & "</Damage_done_Total>"
        tmp += "<Damage_done_Pc>" & Damage_done_Pc & "</Damage_done_Pc>"
        tmp += "<Damage_done_Count>" & Damage_done_Count & "</Damage_done_Count>"
        tmp += "<Damage_done_Avg>" & Damage_done_Avg & "</Damage_done_Avg>"
        tmp += "<hit_count>" & hit_count & "</hit_count>"
        tmp += "<hit_count_Avg>" & hit_count_Avg & "</hit_count_Avg>"
        tmp += "<hit_count_Pc>" & hit_count_Pc & "</hit_count_Pc>"
        tmp += "<Crit_count>" & Crit_count & "</Crit_count>"
        tmp += "<Crit_count_Avg>" & Crit_count_Avg & "</Crit_count_Avg>"
        tmp += "<Crit_count_Pc>" & Crit_count_Pc & "</Crit_count_Pc>"
        tmp += "<Miss_Count>" & Miss_Count & "</Miss_Count>"
        tmp += "<Miss_Count_Pc>" & Miss_Count_Pc & "</Miss_Count_Pc>"
        tmp += "<Glance_Count>" & Glance_Count & "</Glance_Count>"
        tmp += "<Glance_Count_Avg>" & Glance_Count_Avg & "</Glance_Count_Avg>"
        tmp += "<Glance_Count_Pc>" & Glance_Count_Pc & "</Glance_Count_Pc>"
        tmp += "<TPS>" & TPS & "</TPS>"
        tmp += "<Uptime>" & Uptime & "</Uptime>"
        tmp += "</row>"
        Return tmp
    End Function
End Class
