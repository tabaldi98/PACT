package com.tabaldi.fisiotech.profile;

import android.content.Context;

import com.tabaldi.fisiotech.base.HttpRepositoryBase;

import retrofit2.Call;
import retrofit2.http.GET;

public interface IProfileHttpRepository {
    public static IProfileHttpRepository createInstance(Context context){
        return HttpRepositoryBase.buildRetrofit(context).create(IProfileHttpRepository.class);
    }

    @GET("api/user/profile")
    Call<ProfileModel> RetrieveModel();
}
