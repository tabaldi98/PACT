package com.tabaldi.fisiotech.ui.calendar;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CalendarView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.tabaldi.fisiotech.R;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CalendarFragment extends Fragment {
    private CalendarView _calendar;
    private CalendarAdapter _adapter;
    private List<CalendarAttendances> _attendances;
    private RecyclerView _listAttendances;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_calendar, container, false);

        _calendar = root.findViewById(R.id.calendar_attendances);
        _listAttendances = root.findViewById(R.id.list_calendar_attendances);
        _adapter = new CalendarAdapter(new ArrayList<>());
        _listAttendances.setLayoutManager(new LinearLayoutManager(getContext()));
        _listAttendances.setAdapter(_adapter);

        _calendar.setOnDateChangeListener(new CalendarView.OnDateChangeListener() {
            @Override
            public void onSelectedDayChange(@NonNull CalendarView view, int year, int month, int dayOfMonth) {
                AttendanceCalendarRetrieveByDateCommand command = new AttendanceCalendarRetrieveByDateCommand();
                command.setDateFilter(year, month, dayOfMonth);
                SetAttendances(command);
            }
        });

        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(new Date().getTime());
        int currentYear = calendar.get(Calendar.YEAR);
        int currentMonth = calendar.get(Calendar.MONTH);
        int currentDay = calendar.get(Calendar.DAY_OF_MONTH);

        AttendanceCalendarRetrieveByDateCommand command1 = new AttendanceCalendarRetrieveByDateCommand();
        command1.setDateFilter(currentYear, currentMonth, currentDay);
        SetAttendances(command1);

        return root;
    }

    private void SetAttendances(AttendanceCalendarRetrieveByDateCommand command) {
        IAttendanceCalendarHttpRepository.createInstance(getContext()).RetrieveAllByDate(command).enqueue(new Callback<List<CalendarAttendances>>() {
            @Override
            public void onResponse(Call<List<CalendarAttendances>> call, Response<List<CalendarAttendances>> response) {
                if (response.isSuccessful()) {
                    _attendances = response.body();
                    _adapter.calendarAttendances = _attendances;
                    _adapter.notifyDataSetChanged();
                }
            }

            @Override
            public void onFailure(Call<List<CalendarAttendances>> call, Throwable t) {
            }
        });
    }
}