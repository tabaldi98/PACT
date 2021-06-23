package com.tabaldi.fisiotech.ui.login;

import android.content.Context;

import com.tabaldi.fisiotech.base.HttpRepositoryBase;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface IHttpLoginRepository {
    static IHttpLoginRepository createInstance(Context context) {
        return HttpRepositoryBase.buildRetrofit(context).create(IHttpLoginRepository.class);
    }

    @GET("api/public/is-alive")
    Call<Boolean> IsAlive();

    @POST("token")
    Call<TokenModel> Authenticate(@Body TokenCommand command);
}

