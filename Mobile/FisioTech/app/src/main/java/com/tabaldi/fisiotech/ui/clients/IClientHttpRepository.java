package com.tabaldi.fisiotech.ui.clients;

import android.content.Context;

import com.tabaldi.fisiotech.base.HttpRepositoryBase;
import com.tabaldi.fisiotech.ui.attendances.AttendanceAddCommand;
import com.tabaldi.fisiotech.ui.attendances.AttendanceModel;
import com.tabaldi.fisiotech.ui.attendances.AttendanceRemoveCommand;
import com.tabaldi.fisiotech.ui.attendances.AttendanceUpdateCommand;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface IClientHttpRepository {
    static IClientHttpRepository createInstance(Context context) {
        return HttpRepositoryBase.buildRetrofit(context).create(IClientHttpRepository.class);
    }

    @GET("api/client")
    Call<List<Client>> RetrieveAll();

    @POST("api/client")
    Call<Integer> Add(@Body ClientAddCommand command);

    @POST("api/client/edit")
    Call<Boolean> Update(@Body ClientUpdateCommand command);

    @POST("api/client/remove")
    Call<Boolean> RemoveClient(@Body ClientRemoveCommand command);

    @GET("api/attendance")
    Call<List<AttendanceModel>> RetrieveAttendances(@Query("clientId") int clientId);

    @POST("api/attendance")
    Call<Integer> AddAttendance(@Body AttendanceAddCommand attendanceAddCommand);

    @POST("api/attendance/edit")
    Call<Boolean> UpdateAttendance(@Body AttendanceUpdateCommand attendanceUpdateCommand);

    @POST("api/attendance/remove")
    Call<Boolean> RemoveAttendance(@Body AttendanceRemoveCommand attendanceRemoveCommand);
}

