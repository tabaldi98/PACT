package com.tabaldi.fisiotech.ui.clients;

import com.tabaldi.fisiotech.base.Utils;

import java.util.ArrayList;
import java.util.List;

public class ClientAddCommand {
    public String name;
    public String phone;
    public String dateOfBirth;
    public String value;
    public ChargingType chargingType;
    public String clinicalDiagnosis;
    public String physiotherapeuticDiagnosis;
    public String objectives;
    public String treatmentConduct;
    public List<AttendanceAddRecurrenceCommand> recurrences;

    public void AddRecurrence(WeekDay weekDay, String startTime, String endTime) {
        AttendanceAddRecurrenceCommand recurrence = new AttendanceAddRecurrenceCommand();
        recurrence.weekDay = weekDay;
        recurrence.startTime = Utils.formatApiDate(Utils.stringToDate(startTime, Utils.VIEW_TIME_FORMAT));
        recurrence.endTime = Utils.formatApiDate(Utils.stringToDate(endTime, Utils.VIEW_TIME_FORMAT));

        if (this.recurrences == null) this.recurrences = new ArrayList<>();

        this.recurrences.add(recurrence);
    }
}
