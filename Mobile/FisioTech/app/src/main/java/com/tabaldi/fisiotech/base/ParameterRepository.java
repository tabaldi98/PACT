package com.tabaldi.fisiotech.base;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.text.TextUtils;

public class ParameterRepository {
    private final SQLiteDatabase database;

    public ParameterRepository(SQLiteDatabase database) {
        this.database = database;
    }

    public String GetParameter(String parameterName) {
        String value = "";
        String query = "SELECT ParameterName, ParameterValue FROM CustomParameters WHERE ParameterName = ?";

        Cursor rawQuery = database.rawQuery(query, new String[]{parameterName});

        if (rawQuery.getCount() > 0) {
            rawQuery.moveToFirst();

            do {
                value = rawQuery.getString(rawQuery.getColumnIndexOrThrow("ParameterValue"));

            } while (rawQuery.moveToNext());
        }

        return value;
    }

    public void UpdateParameter(String parameterName, String parameterValue) {
        if (TextUtils.isEmpty(GetParameter(parameterName))) {
            ContentValues contentValues = new ContentValues();
            contentValues.put("ParameterName", parameterName);
            contentValues.put("ParameterValue", parameterValue);

            database.insertOrThrow("CustomParameters", null, contentValues);

            return;
        }

        ContentValues contentValues = new ContentValues();
        contentValues.put("ParameterValue", parameterValue);

        String[] params = new String[1];
        params[0] = String.valueOf(parameterName);

        database.update("CustomParameters", contentValues, "ParameterName = ?", params);
    }
}
