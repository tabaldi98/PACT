package com.tabaldi.fisiotech.profile;

import java.io.Serializable;

public class ProfileModel implements Serializable {
    public int id;
    public String userName;
    public String password;
    public String fullName;
    public String mail;
    public boolean sendAlerts;
    public String registrationDate;
}
