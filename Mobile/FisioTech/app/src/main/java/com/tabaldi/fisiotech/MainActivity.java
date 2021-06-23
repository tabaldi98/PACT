package com.tabaldi.fisiotech;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.SearchView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.Fragment;
import androidx.navigation.NavController;
import androidx.navigation.NavDestination;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.navigation.NavigationView;
import com.tabaldi.fisiotech.profile.IProfileHttpRepository;
import com.tabaldi.fisiotech.profile.ProfileActivity;
import com.tabaldi.fisiotech.profile.ProfileModel;
import com.tabaldi.fisiotech.ui.clients.Client;
import com.tabaldi.fisiotech.ui.clients.ClientAdapter;

import org.jetbrains.annotations.NotNull;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private AppBarConfiguration mAppBarConfiguration;
    private RecyclerView _listClients;
    private TextView _txtProfileIcon;
    private TextView _txtFullName;
    private TextView _txtMail;
    private List<Client> _clients;
    private DrawerLayout _drawer;
    private NavigationView _navigationView;
    private NavController _navController;
    private SearchView _searchView;
    private ProfileModel _profile;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

       // Toolbar toolbar = findViewById(R.id.toolbar);
       //setSupportActionBar(toolbar);
        _drawer = findViewById(R.id.drawer_layout);
        _navigationView = findViewById(R.id.nav_view);
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_clients, R.id.nav_calendar)
                .setDrawerLayout(_drawer)
                .build();
        _navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, _navController, mAppBarConfiguration);
        NavigationUI.setupWithNavController(_navigationView, _navController);

        _listClients = findViewById(R.id.list_clients);

        View menu = ((NavigationView) findViewById(R.id.nav_view)).getHeaderView(0);
        _txtProfileIcon = menu.findViewById(R.id.txt_profile_icon);
        _txtFullName = menu.findViewById(R.id.txt_profile_name);
        _txtMail = menu.findViewById(R.id.txt_profile_mail);

        GetProfile();

        SubscribeToDrawer();
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        return super.onPrepareOptionsMenu(menu);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.main, menu);

        MenuItem menuItem = menu.findItem(R.id.search_clients);
        _searchView = (SearchView) menuItem.getActionView();

        _searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                ClientAdapter clientAdapter = (ClientAdapter) _listClients.getAdapter();
                _clients = clientAdapter.clients;
                clientAdapter.clients = filterClientsByName(query);
                clientAdapter.notifyDataSetChanged();
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                if (TextUtils.isEmpty(newText.trim())) {
                    ClientAdapter clientAdapter = (ClientAdapter) _listClients.getAdapter();
                    clientAdapter.clients = _clients;
                    clientAdapter.notifyDataSetChanged();
                }
                return false;
            }
        });
        return true;
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }

    private void GetProfile() {
        IProfileHttpRepository.createInstance(this).RetrieveModel().enqueue(new Callback<ProfileModel>() {
            @Override
            public void onResponse(Call<ProfileModel> call, Response<ProfileModel> response) {
                if (response.isSuccessful()) {
                    _profile = response.body();
                    String firstChar = _profile.fullName.substring(0, 1);
                    String secundChar = _profile.fullName.split(" ")[1].substring(0, 1);
                    _txtProfileIcon.setText(firstChar + secundChar);
                    _txtFullName.setText(_profile.fullName);
                    _txtMail.setText(_profile.mail);
                }
            }

            @Override
            public void onFailure(Call<ProfileModel> call, Throwable t) {
            }
        });

        _txtProfileIcon.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               navigateToProfile();
            }
        });
        _txtFullName.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                navigateToProfile();
            }
        });
        _txtMail.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                navigateToProfile();
            }
        });
    }

    private void navigateToProfile(){
        Intent it = new Intent(getBaseContext(), ProfileActivity.class);
        it.putExtra("Profile", _profile);
        startActivity(it);
    }

    private List<Client> filterClientsByName(String filter) {
        List<Client> clients = new ArrayList<>();
        for (Client client : _clients) {
            if (client.name.toLowerCase().contains(filter.toLowerCase())) {
                clients.add(client);
            }
        }

        return clients;
    }

    private void SubscribeToDrawer() {
        _navController.addOnDestinationChangedListener(new NavController.OnDestinationChangedListener() {
            @Override
            public void onDestinationChanged(@NonNull @NotNull NavController controller, @NonNull @NotNull NavDestination destination, @Nullable @org.jetbrains.annotations.Nullable Bundle arguments) {
                switch (destination.getId()) {
                    case R.id.nav_clients:
                        if (_searchView != null) {
                            _searchView.setVisibility(View.VISIBLE);
                        }
                        break;
                    case R.id.nav_calendar:
                        _searchView.setVisibility(View.GONE);
                        break;
                }
            }
        });
    }
}