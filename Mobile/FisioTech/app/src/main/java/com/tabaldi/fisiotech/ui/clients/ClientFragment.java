package com.tabaldi.fisiotech.ui.clients;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.activity.result.ActivityResult;
import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.tabaldi.fisiotech.ClientAddActivity;
import com.tabaldi.fisiotech.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ClientFragment extends Fragment {

    private ClientAdapter _clientAdapter;
    private List<Client> _clients;
    private RecyclerView _listClients;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_client, container, false);

        _listClients = root.findViewById(R.id.list_clients);


        ActivityResultLauncher<Intent> mStartForResult = registerForActivityResult(new ActivityResultContracts.StartActivityForResult(),
                new ActivityResultCallback<ActivityResult>() {
                    @Override
                    public void onActivityResult(ActivityResult result) {
                        GetClients();
                    }
                });

        FloatingActionButton fab = root.findViewById(R.id.btn_add_client);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent it = new Intent(getActivity(), ClientAddActivity.class);
                mStartForResult.launch(it);
            }
        });

        GetClients();

        return root;
    }

    private void GetClients() {
        IClientHttpRepository.createInstance(getContext()).RetrieveAll().enqueue(new Callback<List<Client>>() {
            @Override
            public void onResponse(Call<List<Client>> call, Response<List<Client>> response) {
                if (response.isSuccessful()) {
                    _clients = response.body();
                    _clientAdapter = new ClientAdapter(_clients);
                    _listClients.setLayoutManager(new LinearLayoutManager(getContext()));
                    _listClients.setAdapter(_clientAdapter);
                }
            }

            @Override
            public void onFailure(Call<List<Client>> call, Throwable t) {
            }
        });
    }
}