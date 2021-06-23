package com.tabaldi.fisiotech;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;

import androidx.activity.result.ActivityResult;
import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.tabaldi.fisiotech.ui.attendances.AttendanceAdapter;
import com.tabaldi.fisiotech.ui.attendances.AttendanceModel;
import com.tabaldi.fisiotech.ui.clients.Client;
import com.tabaldi.fisiotech.ui.clients.ClientRepository;
import com.tabaldi.fisiotech.ui.clients.IClientHttpRepository;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AttendanceListActivity extends AppCompatActivity {

    private RecyclerView _listClientAttendances;
    private ClientRepository _clientRepository;
    private Client _client;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_attendance_list);

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        _listClientAttendances = findViewById(R.id.list_client_attendances);

        Bundle extras = getIntent().getExtras();
        if (extras != null || extras.containsKey("Client")) {
            _client = (Client) extras.getSerializable("Client");
        }

        ActivityResultLauncher<Intent> mStartForResult = registerForActivityResult(new ActivityResultContracts.StartActivityForResult(),
                new ActivityResultCallback<ActivityResult>() {
                    @Override
                    public void onActivityResult(ActivityResult result) {
                        GetAttendances();
                    }
                });

        FloatingActionButton fab = findViewById(R.id.btn_add_client_attendance);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent it = new Intent(AttendanceListActivity.this, AttendanceAddActivity.class);
                it.putExtra("Client", _client);
                mStartForResult.launch(it);
            }
        });

        GetAttendances();
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable @org.jetbrains.annotations.Nullable Intent data) {
        GetAttendances();

        super.onActivityResult(requestCode, resultCode, data);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home: // Botão de voltar
                finish();
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    private void GetAttendances() {
        IClientHttpRepository.createInstance(this).RetrieveAttendances(_client.id).enqueue(new Callback<List<AttendanceModel>>() {
            @Override
            public void onResponse(Call<List<AttendanceModel>> call, Response<List<AttendanceModel>> response) {
                if (response.isSuccessful()) {
                    _listClientAttendances.setLayoutManager(new LinearLayoutManager(AttendanceListActivity.this));
                    _listClientAttendances.setAdapter(new AttendanceAdapter(response.body()));
                }
            }

            @Override
            public void onFailure(Call<List<AttendanceModel>> call, Throwable t) {
                /***/
                Log.i("", "");
            }
        });
    }
}