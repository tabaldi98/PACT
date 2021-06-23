package com.tabaldi.fisiotech.ui.clients;

import java.io.Serializable;

public class AttendanceRecurrence implements Serializable {
    public int id;
    public WeekDay weekDay;
    public String startTime;
    public String endTime;
}
