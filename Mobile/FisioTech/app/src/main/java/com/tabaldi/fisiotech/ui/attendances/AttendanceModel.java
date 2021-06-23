package com.tabaldi.fisiotech.ui.attendances;

import java.io.Serializable;

public class AttendanceModel implements Serializable {
    public int id;
    public String date;
    public String hourInitial;
    public String hourFinish;
    public String description;
}
