package com.tabaldi.fisiotech.ui.calendar;

import com.tabaldi.fisiotech.base.Utils;

public class AttendanceCalendarRetrieveByDateCommand {
    public String dateFilter;

    public void setDateFilter(int year, int month, int dayOfMonth) {
        this.dateFilter = year + "-" + Utils.formatValue(month + 1) + "-" + Utils.formatValue(dayOfMonth) + "T01:01:01.000Z";
    }
}
