package com.tabaldi.fisiotech.ui.login;

import android.app.ProgressDialog;
import android.content.Intent;
import android.database.SQLException;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
import android.widget.EditText;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import com.google.android.material.floatingactionbutton.ExtendedFloatingActionButton;
import com.tabaldi.fisiotech.MainActivity;
import com.tabaldi.fisiotech.R;
import com.tabaldi.fisiotech.base.FisioTechDatabaseOpenHelper;
import com.tabaldi.fisiotech.base.ParameterRepository;
import com.tabaldi.fisiotech.base.Parameters;
import com.tabaldi.fisiotech.base.Utils;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {
    private ExtendedFloatingActionButton _buttonLogin;
    private EditText _txtLogin;
    private EditText _txtPassword;
    private CheckBox _checkSaveCredentials;

    private ParameterRepository _parameterRepository;
    private String _token;

    private ProgressDialog _progress;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        _checkSaveCredentials = findViewById(R.id.checkSaveCredentials);
        _txtLogin = findViewById(R.id.txt_user_name);
        _txtPassword = findViewById(R.id.txt_password);
        _buttonLogin = findViewById(R.id.btn_login);
        _progress = Utils.buildProgress(this);

        enableComponents(false);

        CreateConnection();
        checkIsAlive();
        startFields();

        _buttonLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                login();
            }
        });
    }

    private void login() {
        _progress.show();
        TokenCommand command = new TokenCommand();
        command.userName = _txtLogin.getText().toString();
        command.password = _txtPassword.getText().toString();

        IHttpLoginRepository.createInstance(this).Authenticate(command).enqueue(new Callback<TokenModel>() {
            @Override
            public void onResponse(Call<TokenModel> call, Response<TokenModel> response) {
                _progress.dismiss();
                if (response.isSuccessful()) {
                    TokenModel tokenModel = response.body();
                    _token = tokenModel.accessToken;
                    saveCredentials();
                    doStart();
                } else {
                    showLoginError();
                }
            }

            @Override
            public void onFailure(Call<TokenModel> call, Throwable t) {
                showLoginError();
            }
        });
    }

    private void showLoginError() {
        AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle(R.string.txt_attention);
        dialog.setMessage("Usuário e/ou senha inválidos");
        dialog.setNeutralButton(R.string.txt_ok, null);
        dialog.show();
    }

    private void saveCredentials() {
        _parameterRepository.UpdateParameter(Parameters.SAVE_CREDENTIALS, _checkSaveCredentials.isChecked() ? "True" : "False");

        if (_checkSaveCredentials.isChecked()) {
            _parameterRepository.UpdateParameter(Parameters.USER_NAME, _txtLogin.getText().toString());
            _parameterRepository.UpdateParameter(Parameters.PASSWORD, _txtPassword.getText().toString());
        }

        _parameterRepository.UpdateParameter(Parameters.TOKEN, _token);
    }

    private void checkIsAlive() {
        _progress.show();
        IHttpLoginRepository.createInstance(this).IsAlive().enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                _progress.dismiss();
                if (response.isSuccessful()) {
                    doStart();
                } else {
                    _parameterRepository.UpdateParameter(Parameters.TOKEN, "");
                    enableComponents(true);
                }
            }

            @Override
            public void onFailure(Call<Boolean> call, Throwable t) {
                _parameterRepository.UpdateParameter(Parameters.TOKEN, "");
                enableComponents(true);
            }
        });
    }

    private void enableComponents(boolean enabled) {
        _buttonLogin.setEnabled(enabled);
        _txtLogin.setEnabled(enabled);
        _txtPassword.setEnabled(enabled);
    }

    private void doStart() {
        Intent it = new Intent(LoginActivity.this, MainActivity.class);
        startActivity(it);
    }

    private void CreateConnection() {
        try {
            _parameterRepository = new ParameterRepository(new FisioTechDatabaseOpenHelper(this).getWritableDatabase());
        } catch (SQLException ex) {
            //
        }
    }

    private void startFields() {
        _checkSaveCredentials.setChecked(_parameterRepository.GetParameter(Parameters.SAVE_CREDENTIALS).equals("True"));
        if (_checkSaveCredentials.isChecked()) {
            _txtLogin.setText(_parameterRepository.GetParameter(Parameters.USER_NAME));
            _txtPassword.setText(_parameterRepository.GetParameter(Parameters.PASSWORD));
        }
    }
}