package com.tabaldi.fisiotech.ui.clients;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class ClientRemoveCommand implements Serializable {
    public List<Integer> ids;

    public void AddId(int id) {
        if (this.ids == null) {
            this.ids = new ArrayList<>();
        }

        this.ids.add(id);
    }
}
