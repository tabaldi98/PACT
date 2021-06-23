package com.tabaldi.fisiotech.profile;

import android.content.Intent;
import android.database.SQLException;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import com.tabaldi.fisiotech.R;
import com.tabaldi.fisiotech.base.FisioTechDatabaseOpenHelper;
import com.tabaldi.fisiotech.base.ParameterRepository;
import com.tabaldi.fisiotech.base.Parameters;
import com.tabaldi.fisiotech.base.Utils;
import com.tabaldi.fisiotech.ui.login.LoginActivity;

public class ProfileActivity extends AppCompatActivity {
    private Button _btnExit;
    private EditText _txtLogin;
    private EditText _txtPassword;
    private EditText _txtFullName;
    private EditText _txtMail;
    private CheckBox _checkSendAlerts;
    private TextView _txtDate;

    private ParameterRepository _parameterRepository;
    private ProfileModel _profile;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        _btnExit = findViewById(R.id.btn_exit);
        _txtLogin = findViewById(R.id.txt_login);
        _txtPassword = findViewById(R.id.txt_password);
        _txtFullName = findViewById(R.id.txt_fullname);
        _txtMail = findViewById(R.id.txt_mail);
        _checkSendAlerts = findViewById(R.id.check_enable_alerts);
        _txtDate = findViewById(R.id.txt_registration_date);

        _btnExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                _parameterRepository.UpdateParameter(Parameters.TOKEN, "");
                Intent it = new Intent(ProfileActivity.this, LoginActivity.class);
                startActivity(it);
            }
        });

        CreateConnection();

        Bundle extras = getIntent().getExtras();
        _profile = (ProfileModel) extras.getSerializable("Profile");
        _txtLogin.setText(_profile.userName);
        _txtPassword.setText(_profile.password);
        _txtFullName.setText(_profile.fullName);
        _txtMail.setText(_profile.mail);
        _checkSendAlerts.setChecked(_profile.sendAlerts);
        _txtDate.setText("Registrado em " +
                android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(_profile.registrationDate)));
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        return super.onPrepareOptionsMenu(menu);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.profile_options, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home: // Botão de voltar
                finish();
                break;
            case R.id.profile_act_save:
                finish();
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    private void CreateConnection() {
        try {
            _parameterRepository = new ParameterRepository(new FisioTechDatabaseOpenHelper(this).getWritableDatabase());
        } catch (SQLException ex) {
            //
        }
    }
}