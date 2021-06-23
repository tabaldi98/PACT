package com.tabaldi.fisiotech.ui.clients;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ClientRepository {
    public List<Client> RetrieveAll() {
        List<Client> clients = new ArrayList<Client>();

        Client client = new Client();
        client.name = "João da silva";
        client.chargingType = ChargingType.Day;
        client.clinicalDiagnosis = "ClinicalDiagnosis";
        client.dateOfBirth = Calendar.getInstance().getTime().toString();
        client.objectives = "Objectives";
        client.physiotherapeuticDiagnosis = "PhysiotherapeuticDiagnosis";
        client.treatmentConduct = "TreatmentConduct";
        client.value = 1.00;
        client.phone = "123456";

        Client client1 = new Client();
        client1.name = "João da silva 2";
        client1.chargingType = ChargingType.Month;
        client1.clinicalDiagnosis = "ClinicalDiagnosis";
        client1.dateOfBirth = Calendar.getInstance().getTime().toString();
        client1.objectives = "Objectives";
        client1.physiotherapeuticDiagnosis = "PhysiotherapeuticDiagnosis";
        client1.treatmentConduct = "TreatmentConduct";
        client1.value = 50.00;
        client.phone = "1234563424";

        clients.add(client);
        clients.add(client1);

        return clients;
    }
}
