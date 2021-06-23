package com.tabaldi.fisiotech.ui.attendances;

import java.util.ArrayList;
import java.util.List;

public class AttendanceRemoveCommand {
    public List<Integer> ids;

    public void AddId(int id) {
        if (this.ids == null) {
            this.ids = new ArrayList<>();
        }

        this.ids.add(id);
    }
}
