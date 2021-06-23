package com.tabaldi.fisiotech.base;

import android.content.Context;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class HttpRepositoryBase {
    private static final String BASE_URL = "http://192.168.0.107:9001/";

    public static Retrofit buildRetrofit(Context context) {
        OkHttpClient client = new OkHttpClient.Builder().addInterceptor(new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                String token = getRepository(context).GetParameter(Parameters.TOKEN);
                Request newRequest = chain.request().newBuilder()
                        .addHeader("Authorization", "Bearer " + token)
                        .build();
                return chain.proceed(newRequest);
            }
        }).build();

        return new Retrofit.Builder()
                .client(client)
                .baseUrl(HttpRepositoryBase.BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }

    private static ParameterRepository getRepository(Context context) {
        try {
            FisioTechDatabaseOpenHelper FisioTechDatabaseOpenHelper = new FisioTechDatabaseOpenHelper(context);
            SQLiteDatabase database = FisioTechDatabaseOpenHelper.getWritableDatabase();
            return new ParameterRepository(database);
        } catch (SQLException ex) {
            return null;
        }
    }
}
