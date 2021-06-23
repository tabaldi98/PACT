package com.tabaldi.fisiotech.ui.calendar;

import android.content.Context;

import com.tabaldi.fisiotech.base.HttpRepositoryBase;
import com.tabaldi.fisiotech.ui.attendances.AttendanceAddCommand;
import com.tabaldi.fisiotech.ui.attendances.AttendanceModel;
import com.tabaldi.fisiotech.ui.attendances.AttendanceRemoveCommand;
import com.tabaldi.fisiotech.ui.attendances.AttendanceUpdateCommand;
import com.tabaldi.fisiotech.ui.calendar.CalendarAttendances;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface IAttendanceCalendarHttpRepository {
    static IAttendanceCalendarHttpRepository createInstance(Context context) {
        return HttpRepositoryBase.buildRetrofit(context).create(IAttendanceCalendarHttpRepository.class);
    }

    @POST("api/attendance-recurrence/by-date")
    Call<List<CalendarAttendances>> RetrieveAllByDate(@Body AttendanceCalendarRetrieveByDateCommand command);
}

