package com.tabaldi.fisiotech.ui.clients;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class Client implements Serializable {
    public int id;
    public String name;
    public String phone;
    public String dateOfBirth;
    public Double value;
    public ChargingType chargingType;
    public String clinicalDiagnosis;
    public String physiotherapeuticDiagnosis;
    public String objectives;
    public String treatmentConduct;
    public List<AttendanceRecurrence> recurrences;
    public boolean enabled;
}
