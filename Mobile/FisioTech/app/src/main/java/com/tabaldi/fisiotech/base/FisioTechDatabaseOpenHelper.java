package com.tabaldi.fisiotech.base;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

public class FisioTechDatabaseOpenHelper extends SQLiteOpenHelper {
    public FisioTechDatabaseOpenHelper(@Nullable Context context) {
        super(context, "FISIOTECHDatabase", null, 1);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String scriptParmsTable = "CREATE TABLE IF NOT EXISTS CustomParameters (\n" +
                "    ID    INTEGER       PRIMARY KEY AUTOINCREMENT\n" +
                "                        NOT NULL,\n" +
                "    ParameterName  VARCHAR (255) NOT NULL,\n" +
                "    ParameterValue VARCHAR (5000) NOT NULL\n" +
                ");\n";

        db.execSQL(scriptParmsTable);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
    }
}
