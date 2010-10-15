Public Class ReportLine
    Property Ability As String
    Property Damage_done_Total As Long
    Property Damage_done_Pc As Long
    Property Damage_done_Count As Long
    Property Damage_done_Avg As Long


    Property hit_count As Long
    Property hit_Pc As Long
    Property hit_Avg As Long

    Property Crit_count As Long
    Property Crit_Pc As Long
    Property Crit_Avg As Long

    Property Miss_Count As Long
    Property Miss_Pc As Long




    Property Glance_Count As Long
    Property Glance_Pc As Long
    Property Glance_Avg As Long

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
        tmp += "<Damage_Pc>" & Damage_done_Pc & "</Damage_Pc>"
        tmp += "<Damage_Count>" & Damage_done_Count & "</Damage_Count>"
        tmp += "<Damage_Avg>" & Damage_done_Avg & "</Damage_Avg>"
        tmp += "<hit_Count>" & hit_count & "</hit_Count>"
        tmp += "<hit_Avg>" & hit_Avg & "</hit_Avg>"
        tmp += "<hit_Pc>" & hit_Pc & "</hit_Pc>"
        tmp += "<Crit_Count>" & Crit_count & "</Crit_Count>"
        tmp += "<Crit_Avg>" & Crit_Avg & "</Crit_Avg>"
        tmp += "<Crit_Pc>" & Crit_Pc & "</Crit_Pc>"
        tmp += "<Miss_Count>" & Miss_Count & "</Miss_Count>"
        tmp += "<Miss_Pc>" & Miss_Pc & "</Miss_Pc>"
        tmp += "<Glance_Count>" & Glance_Count & "</Glance_Count>"
        tmp += "<Glance_Avg>" & Glance_Avg & "</Glance_Avg>"
        tmp += "<Glance_Pc>" & Glance_Pc & "</Glance_Pc>"
        tmp += "<TPS>" & TPS & "</TPS>"
        tmp += "<Uptime>" & Uptime & "</Uptime>"
        tmp += "</row>"
        Return tmp
    End Function
End Class
