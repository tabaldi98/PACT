package com.tabaldi.fisiotech.ui.attendances;

import com.tabaldi.fisiotech.base.Utils;

import java.util.Date;

public class AttendanceAddCommand {
    public int clientId;
    public String date;
    public String hourInitial;
    public String hourFinish;
    public String description;

    public AttendanceAddCommand() {
    }

    public AttendanceAddCommand(int clientId, String date, String hourInitial, String hourFinish, String description) {
        this.clientId = clientId;
        this.date = Utils.formatApiDate(Utils.stringToDate(date, Utils.VIEW_DATE_FORMAT));
        this.hourInitial = Utils.formatApiDate(Utils.stringToDate(hourInitial, Utils.VIEW_TIME_FORMAT));
        this.hourFinish = Utils.formatApiDate(Utils.stringToDate(hourFinish, Utils.VIEW_TIME_FORMAT));
        this.description = description;
    }
}
