package com.tabaldi.fisiotech;

import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import com.tabaldi.fisiotech.base.Utils;
import com.tabaldi.fisiotech.ui.attendances.AttendanceAddCommand;
import com.tabaldi.fisiotech.ui.attendances.AttendanceModel;
import com.tabaldi.fisiotech.ui.attendances.AttendanceUpdateCommand;
import com.tabaldi.fisiotech.ui.clients.Client;
import com.tabaldi.fisiotech.ui.clients.IClientHttpRepository;

import java.util.Date;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AttendanceAddActivity extends AppCompatActivity {
    /**
     * ELEMENTOS
     */
    private EditText _txtDate;
    private EditText _txtStartTime;
    private EditText _txtEndTime;
    private EditText _txtEvolution;
    /***/


    private AttendanceModel _attendance;
    private Client _client;
    private boolean _isEditable;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_attendance_add);

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        _txtDate = findViewById(R.id.txt_attendance_day);
        _txtStartTime = findViewById(R.id.txt_attendance_initial_hour);
        _txtEndTime = findViewById(R.id.txt_attendance_end_hour);
        _txtEvolution = findViewById(R.id.txt_attendance_evolution);

        SetTextsIfIsEditable();

        Bundle extras = getIntent().getExtras();
        if (extras != null || extras.containsKey("Client")) {
            _client = (Client) extras.getSerializable("Client");
        }
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        return super.onPrepareOptionsMenu(menu);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.attendance_options, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home: // Botão de voltar
                finish();
                break;
            case R.id.attendance_act_save: // Botão de salvar
                if (ValidateFields()) {
                    if (_isEditable) {
                        updateAttendance();
                    } else {
                        addAttendance();
                    }
                }
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    private boolean ValidateFields() {
        if (TextUtils.isEmpty(_txtDate.getText().toString().trim())) {
            ShowEmptyFieldAlert("data");
            _txtDate.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtStartTime.getText().toString().trim())) {
            ShowEmptyFieldAlert("hora de inicio");
            _txtStartTime.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtEndTime.getText().toString().trim())) {
            ShowEmptyFieldAlert("hora do fim ");
            _txtEndTime.requestFocus();
            return false;
        }

        return true;
    }

    private void ShowEmptyFieldAlert(String fieldName) {
        AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle(R.string.txt_attention);
        dialog.setMessage("O campo " + fieldName + " não está preenchido.");
        dialog.setNeutralButton(R.string.txt_ok, null);
        dialog.show();
    }

    private void SetTextsIfIsEditable() {
        _isEditable = false;
        Bundle extras = getIntent().getExtras();
        if (extras == null || !extras.containsKey("Attendance")) {
            _txtDate.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, new Date()));

            _txtStartTime.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, new Date()));

            Date endDate = new Date();
            endDate.setTime(endDate.getTime() + 3_600_000); // Adiciona uma hora
            _txtEndTime.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, endDate));

            _txtEvolution.requestFocus();

            return;
        }

        _attendance = (AttendanceModel) extras.getSerializable("Attendance");
        _isEditable = true;

        _txtDate.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(_attendance.date)));
        _txtStartTime.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(_attendance.hourInitial)));
        _txtEndTime.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(_attendance.hourFinish)));
        _txtEvolution.setText(_attendance.description);
    }

    private void addAttendance() {
        AttendanceAddCommand command = new AttendanceAddCommand(
                _client.id,
                _txtDate.getText().toString(),
                _txtStartTime.getText().toString(),
                _txtEndTime.getText().toString(),
                _txtEvolution.getText().toString());

        IClientHttpRepository.createInstance(this).AddAttendance(command).enqueue(new Callback<Integer>() {
            @Override
            public void onResponse(Call<Integer> call, Response<Integer> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(AttendanceAddActivity.this, "Atendimento cadastrado com sucesso", Toast.LENGTH_SHORT).show();
                }
                finish();
            }

            @Override
            public void onFailure(Call<Integer> call, Throwable t) {
            }
        });
    }

    private void updateAttendance() {
        AttendanceUpdateCommand command = new AttendanceUpdateCommand(
                _attendance.id,
                _txtDate.getText().toString(),
                _txtStartTime.getText().toString(),
                _txtEndTime.getText().toString(),
                _txtEvolution.getText().toString());

        IClientHttpRepository.createInstance(this).UpdateAttendance(command).enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(AttendanceAddActivity.this, "Atendimento atualizado com sucesso", Toast.LENGTH_SHORT).show();
                }
                finish();
            }

            @Override
            public void onFailure(Call<Boolean> call, Throwable t) {
                /***/
                Log.i("", "");
            }
        });
    }
}