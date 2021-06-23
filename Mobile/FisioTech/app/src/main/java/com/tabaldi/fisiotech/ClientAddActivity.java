package com.tabaldi.fisiotech;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import com.tabaldi.fisiotech.base.Utils;
import com.tabaldi.fisiotech.ui.clients.AttendanceRecurrence;
import com.tabaldi.fisiotech.ui.clients.ChargingType;
import com.tabaldi.fisiotech.ui.clients.Client;
import com.tabaldi.fisiotech.ui.clients.ClientAddCommand;
import com.tabaldi.fisiotech.ui.clients.ClientUpdateCommand;
import com.tabaldi.fisiotech.ui.clients.IClientHttpRepository;
import com.tabaldi.fisiotech.ui.clients.WeekDay;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ClientAddActivity extends AppCompatActivity {

    /**
     * COMPONENTES
     */
    private EditText _txtName;
    private EditText _txtPhone;
    private EditText _txtBirthDate;
    private EditText _txtAmountCharged;
    private RadioButton _radioTypeMonth;
    private RadioButton _radioTypeDaily;
    private EditText _txtClinicalDiagnosis;
    private EditText _txtPhysiotherapeuticDiagnosis;
    private EditText _txtObjetives;
    private EditText _txtTreatmentConduct;
    // Segunda-feira
    private CheckBox _checkMonday;
    private EditText _startTimeMonday;
    private EditText _endTimeMonday;
    // Terça-feira
    private CheckBox _checkTuesday;
    private EditText _startTimeTuesday;
    private EditText _endTimeTuesday;
    // Quarta-feira
    private CheckBox _checkWednesday;
    private EditText _startTimeWednesday;
    private EditText _endTimeWednesday;
    // Quinta-feira
    private CheckBox _checkThursday;
    private EditText _startTimeThursday;
    private EditText _endTimeThursday;
    // Sexta-feira
    private CheckBox _checkFriday;
    private EditText _startTimeFriday;
    private EditText _endTimeFriday;
    // Sábado
    private CheckBox _checkSaturday;
    private EditText _startTimeSaturday;
    private EditText _endTimeSaturday;
    // Domingo
    private CheckBox _checkSunday;
    private EditText _startTimeSunday;
    private EditText _endTimeSunday;
    /***/

    private boolean _isEditable;
    private Client _client;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client_add);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        /** INICIALIZAÇÃO DOS COMPONENTES */
        _txtName = findViewById(R.id.txt_profile_name);
        _txtPhone = findViewById(R.id.txt_phone);
        _txtBirthDate = findViewById(R.id.txt_birth_date);
        _txtAmountCharged = findViewById(R.id.txt_amount_charged);
        _radioTypeMonth = findViewById(R.id.radio_type_month);
        _radioTypeDaily = findViewById(R.id.radio_type_daily);
        _txtClinicalDiagnosis = findViewById(R.id.txt_clinical_diagnosis);
        _txtPhysiotherapeuticDiagnosis = findViewById(R.id.txt_physiotherapeutic_diagnosis);
        _txtObjetives = findViewById(R.id.txt_objetives);
        _txtTreatmentConduct = findViewById(R.id.txt_treatment_conduct);

        _checkMonday = findViewById(R.id.check_monday);
        _startTimeMonday = findViewById(R.id.txt_start_service_time_monday);
        _endTimeMonday = findViewById(R.id.txt_end_service_time_monday);

        _startTimeTuesday = findViewById(R.id.txt_start_service_time_tuesday);
        _endTimeTuesday = findViewById(R.id.txt_end_service_time_tuesday);
        _checkTuesday = findViewById(R.id.check_tuesday);

        _checkWednesday = findViewById(R.id.check_wednesday);
        _startTimeWednesday = findViewById(R.id.txt_start_service_time_wednesday);
        _endTimeWednesday = findViewById(R.id.txt_end_service_time_wednesday);

        _checkThursday = findViewById(R.id.check_thursday);
        _startTimeThursday = findViewById(R.id.txt_start_service_time_thursday);
        _endTimeThursday = findViewById(R.id.txt_end_service_time_thursday);

        _checkFriday = findViewById(R.id.check_friday);
        _startTimeFriday = findViewById(R.id.txt_start_service_time_friday);
        _endTimeFriday = findViewById(R.id.txt_end_service_time_friday);

        _checkSaturday = findViewById(R.id.check_saturday);
        _startTimeSaturday = findViewById(R.id.txt_start_service_time_saturday);
        _endTimeSaturday = findViewById(R.id.txt_end_service_time_saturday);

        _checkSunday = findViewById(R.id.check_sunday);
        _startTimeSunday = findViewById(R.id.txt_start_service_time_sunday);
        _endTimeSunday = findViewById(R.id.txt_end_service_time_sunday);

        SetTextsIfIsEditable();
        /***/

        /** EVENTOS */
        _checkMonday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeMonday.requestFocus();
                }
            }
        });
        _checkTuesday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeTuesday.requestFocus();
                }
            }
        });
        _checkWednesday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeWednesday.requestFocus();
                }
            }
        });
        _checkThursday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeThursday.requestFocus();
                }
            }
        });
        _checkFriday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeFriday.requestFocus();
                }
            }
        });
        _checkSaturday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeSaturday.requestFocus();
                }
            }
        });
        _checkSunday.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                HandleEnabledViews();
                if (isChecked) {
                    _startTimeSunday.requestFocus();
                }
            }
        });
        /***/
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        MenuItem actAttendances = menu.findItem(R.id.client_act_attendances);
        actAttendances.setVisible(_isEditable);

        return super.onPrepareOptionsMenu(menu);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.client_options, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home: // Botão de voltar
                finish();
                break;
            case R.id.client_act_ok: // Botão de salvar
                if (ValidateFields()) {
                    if (_isEditable) {
                        IClientHttpRepository.createInstance(this).Update(getUpdateCommand()).enqueue(new Callback<Boolean>() {
                            @Override
                            public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                                if (response.isSuccessful()) {
                                    Toast.makeText(ClientAddActivity.this, R.string.txt_client_update_success, Toast.LENGTH_SHORT).show();
                                }
                                finish();
                            }

                            @Override
                            public void onFailure(Call<Boolean> call, Throwable t) {
                                Log.i("", "");
                            }
                        });
                    } else {
                        IClientHttpRepository.createInstance(this).Add(getAddCommand()).enqueue(new Callback<Integer>() {
                            @Override
                            public void onResponse(Call<Integer> call, Response<Integer> response) {
                                if (response.isSuccessful()) {
                                    Toast.makeText(ClientAddActivity.this, R.string.txt_client_cad_success, Toast.LENGTH_SHORT).show();
                                }
                                finish();
                            }

                            @Override
                            public void onFailure(Call<Integer> call, Throwable t) {
                            }
                        });
                    }
                }
                break;
            case R.id.client_act_attendances:
                Intent it = new Intent(this, AttendanceListActivity.class);
                it.putExtra("Client", _client);
                startActivity(it);
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    private ClientAddCommand getAddCommand() {
        ClientAddCommand command = new ClientAddCommand();
        command.name = _txtName.getText().toString();
        command.phone = _txtPhone.getText().toString();
        command.dateOfBirth = Utils.formatApiDate(Utils.stringToDate(_txtBirthDate.getText().toString(), Utils.VIEW_DATE_FORMAT));
        command.value = _txtAmountCharged.getText().toString();
        command.chargingType = _radioTypeDaily.isChecked() ? ChargingType.Day : ChargingType.Month;
        command.clinicalDiagnosis = _txtClinicalDiagnosis.getText().toString();
        command.physiotherapeuticDiagnosis = _txtPhysiotherapeuticDiagnosis.getText().toString();
        command.objectives = _txtObjetives.getText().toString();
        command.treatmentConduct = _txtTreatmentConduct.getText().toString();

        if (_checkMonday.isChecked()) {
            command.AddRecurrence(WeekDay.Monday, _startTimeMonday.getText().toString(), _endTimeMonday.getText().toString());
        }

        if (_checkTuesday.isChecked()) {
            command.AddRecurrence(WeekDay.Tuesday, _startTimeTuesday.getText().toString(), _endTimeTuesday.getText().toString());
        }

        if (_checkWednesday.isChecked()) {
            command.AddRecurrence(WeekDay.Wednesday, _startTimeWednesday.getText().toString(), _endTimeWednesday.getText().toString());
        }

        if (_checkThursday.isChecked()) {
            command.AddRecurrence(WeekDay.Thursday, _startTimeThursday.getText().toString(), _endTimeThursday.getText().toString());
        }

        if (_checkFriday.isChecked()) {
            command.AddRecurrence(WeekDay.Friday, _startTimeFriday.getText().toString(), _endTimeFriday.getText().toString());
        }

        if (_checkSaturday.isChecked()) {
            command.AddRecurrence(WeekDay.Saturday, _startTimeSaturday.getText().toString(), _endTimeSaturday.getText().toString());
        }

        if (_checkSunday.isChecked()) {
            command.AddRecurrence(WeekDay.Sunday, _startTimeSunday.getText().toString(), _endTimeSunday.getText().toString());
        }

        return command;
    }

    private ClientUpdateCommand getUpdateCommand() {
        ClientUpdateCommand command = new ClientUpdateCommand();
        command.id = _client.id;
        command.name = _txtName.getText().toString();
        command.phone = _txtPhone.getText().toString();
        command.dateOfBirth = Utils.formatApiDate(Utils.stringToDate(_txtBirthDate.getText().toString(), Utils.VIEW_DATE_FORMAT));
        command.value = _txtAmountCharged.getText().toString();
        command.chargingType = _radioTypeDaily.isChecked() ? ChargingType.Day : ChargingType.Month;
        command.clinicalDiagnosis = _txtClinicalDiagnosis.getText().toString();
        command.physiotherapeuticDiagnosis = _txtPhysiotherapeuticDiagnosis.getText().toString();
        command.objectives = _txtObjetives.getText().toString();
        command.treatmentConduct = _txtTreatmentConduct.getText().toString();

        if (_checkMonday.isChecked()) {
            command.AddRecurrence(WeekDay.Monday, _startTimeMonday.getText().toString(), _endTimeMonday.getText().toString());
        }

        if (_checkTuesday.isChecked()) {
            command.AddRecurrence(WeekDay.Tuesday, _startTimeTuesday.getText().toString(), _endTimeTuesday.getText().toString());
        }

        if (_checkWednesday.isChecked()) {
            command.AddRecurrence(WeekDay.Wednesday, _startTimeWednesday.getText().toString(), _endTimeWednesday.getText().toString());
        }

        if (_checkThursday.isChecked()) {
            command.AddRecurrence(WeekDay.Thursday, _startTimeThursday.getText().toString(), _endTimeThursday.getText().toString());
        }

        if (_checkFriday.isChecked()) {
            command.AddRecurrence(WeekDay.Friday, _startTimeFriday.getText().toString(), _endTimeFriday.getText().toString());
        }

        if (_checkSaturday.isChecked()) {
            command.AddRecurrence(WeekDay.Saturday, _startTimeSaturday.getText().toString(), _endTimeSaturday.getText().toString());
        }

        if (_checkSunday.isChecked()) {
            command.AddRecurrence(WeekDay.Sunday, _startTimeSunday.getText().toString(), _endTimeSunday.getText().toString());
        }

        return command;
    }

    private void HandleEnabledViews() {
        _startTimeMonday.setEnabled(_checkMonday.isChecked());
        _endTimeMonday.setEnabled(_checkMonday.isChecked());

        _startTimeTuesday.setEnabled(_checkTuesday.isChecked());
        _endTimeTuesday.setEnabled(_checkTuesday.isChecked());

        _startTimeWednesday.setEnabled(_checkWednesday.isChecked());
        _endTimeWednesday.setEnabled(_checkWednesday.isChecked());

        _startTimeThursday.setEnabled(_checkThursday.isChecked());
        _endTimeThursday.setEnabled(_checkThursday.isChecked());

        _startTimeFriday.setEnabled(_checkFriday.isChecked());
        _endTimeFriday.setEnabled(_checkFriday.isChecked());

        _startTimeSaturday.setEnabled(_checkSaturday.isChecked());
        _endTimeSaturday.setEnabled(_checkSaturday.isChecked());

        _startTimeSunday.setEnabled(_checkSunday.isChecked());
        _endTimeSunday.setEnabled(_checkSunday.isChecked());
    }

    private boolean ValidateFields() {
        if (TextUtils.isEmpty(_txtName.getText().toString().trim())) {
            ShowEmptyFieldAlert("nome");
            _txtName.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtPhone.getText().toString().trim())) {
            ShowEmptyFieldAlert("telefone");
            _txtPhone.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtBirthDate.getText().toString().trim())) {
            ShowEmptyFieldAlert("data de nascimento");
            _txtBirthDate.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtAmountCharged.getText().toString().trim())) {
            ShowEmptyFieldAlert("valor cobrado");
            _txtAmountCharged.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtClinicalDiagnosis.getText().toString().trim())) {
            ShowEmptyFieldAlert("diagnóstico clínico");
            _txtClinicalDiagnosis.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtPhysiotherapeuticDiagnosis.getText().toString().trim())) {
            ShowEmptyFieldAlert("diagnóstico fisioterapêutico");
            _txtPhysiotherapeuticDiagnosis.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtObjetives.getText().toString().trim())) {
            ShowEmptyFieldAlert("objetivos");
            _txtObjetives.requestFocus();
            return false;
        }

        if (TextUtils.isEmpty(_txtTreatmentConduct.getText().toString().trim())) {
            ShowEmptyFieldAlert("conduta de tratamento");
            _txtTreatmentConduct.requestFocus();
            return false;
        }

        if (_checkMonday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeMonday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de segunda feira");
                _startTimeMonday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeMonday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de segunda feira");
                _endTimeMonday.requestFocus();
                return false;
            }
        }

        if (_checkTuesday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeTuesday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de terça feira");
                _startTimeTuesday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeTuesday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de terça feira");
                _endTimeTuesday.requestFocus();
                return false;
            }
        }

        if (_checkWednesday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeWednesday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de quarta feira");
                _startTimeWednesday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeWednesday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de quarta feira");
                _endTimeWednesday.requestFocus();
                return false;
            }
        }

        if (_checkThursday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeThursday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de quinta feira");
                _startTimeThursday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeThursday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de quinta feira");
                _endTimeThursday.requestFocus();
                return false;
            }
        }

        if (_checkFriday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeFriday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de sexta feira");
                _startTimeFriday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeFriday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de sexta feira");
                _endTimeFriday.requestFocus();
                return false;
            }
        }

        if (_checkSaturday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeSaturday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de sábado");
                _startTimeSaturday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeSaturday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de sábado");
                _endTimeSaturday.requestFocus();
                return false;
            }
        }

        if (_checkSunday.isChecked()) {
            if (TextUtils.isEmpty(_startTimeSunday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora inicial do atendimento de domingo");
                _startTimeSunday.requestFocus();
                return false;
            }

            if (TextUtils.isEmpty(_endTimeSunday.getText().toString().trim())) {
                ShowEmptyFieldAlert("hora final do atendimento de domingo");
                _endTimeSunday.requestFocus();
                return false;
            }
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
        if (extras == null || !extras.containsKey("Client")) {
            return;
        }

        _client = (Client) extras.getSerializable("Client");
        _isEditable = true;

        _txtName.setText(_client.name);
        _txtPhone.setText(_client.phone);
        _txtBirthDate.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(_client.dateOfBirth)));
        _txtAmountCharged.setText(_client.value.toString());
        _radioTypeMonth.setChecked(_client.chargingType == ChargingType.Month);
        _radioTypeDaily.setChecked(_client.chargingType == ChargingType.Day);
        _txtClinicalDiagnosis.setText(_client.clinicalDiagnosis);
        _txtPhysiotherapeuticDiagnosis.setText(_client.physiotherapeuticDiagnosis);
        _txtObjetives.setText(_client.objectives);
        _txtTreatmentConduct.setText(_client.treatmentConduct);

        AttendanceRecurrence monday = getWeekDay(WeekDay.Monday);
        if (monday != null) {
            _checkMonday.setChecked(true);
            _startTimeMonday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(monday.startTime)));
            _endTimeMonday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(monday.endTime)));
        }

        AttendanceRecurrence tuesday = getWeekDay(WeekDay.Tuesday);
        if (tuesday != null) {
            _checkTuesday.setChecked(true);
            _startTimeTuesday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(tuesday.startTime)));
            _endTimeTuesday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(tuesday.endTime)));
        }

        AttendanceRecurrence wednesday = getWeekDay(WeekDay.Wednesday);
        if (wednesday != null) {
            _checkWednesday.setChecked(true);
            _startTimeWednesday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(wednesday.startTime)));
            _endTimeWednesday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(wednesday.endTime)));
        }

        AttendanceRecurrence thursday = getWeekDay(WeekDay.Thursday);
        if (thursday != null) {
            _checkThursday.setChecked(true);
            _startTimeThursday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(thursday.startTime)));
            _endTimeThursday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(thursday.endTime)));
        }

        AttendanceRecurrence friday = getWeekDay(WeekDay.Friday);
        if (friday != null) {
            _checkFriday.setChecked(true);
            _startTimeFriday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(friday.startTime)));
            _endTimeFriday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(friday.endTime)));
        }

        AttendanceRecurrence saturday = getWeekDay(WeekDay.Saturday);
        if (saturday != null) {
            _checkSaturday.setChecked(true);
            _startTimeSaturday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(saturday.startTime)));
            _endTimeSaturday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(saturday.endTime)));
        }

        AttendanceRecurrence sunday = getWeekDay(WeekDay.Sunday);
        if (sunday != null) {
            _checkSunday.setChecked(true);
            _startTimeSunday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(sunday.startTime)));
            _endTimeSunday.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(sunday.endTime)));
        }

        HandleEnabledViews();
    }

    private AttendanceRecurrence getWeekDay(WeekDay weekDay) {
        for (AttendanceRecurrence recurrence : _client.recurrences) {
            if (recurrence.weekDay == weekDay) {
                return recurrence;
            }
        }

        return null;
    }
}