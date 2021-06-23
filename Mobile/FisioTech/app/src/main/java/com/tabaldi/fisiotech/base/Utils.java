package com.tabaldi.fisiotech.base;

import android.app.ProgressDialog;
import android.content.Context;

import com.tabaldi.fisiotech.R;

import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class Utils {
    public static final String VIEW_DATE_FORMAT = "dd/MM/yyyy";
    public static final String VIEW_TIME_FORMAT = "HH:mm";

    public static ProgressDialog buildProgress(Context context) {
        ProgressDialog progress = new ProgressDialog(context);
        progress.setTitle(context.getString(R.string.txt_loading));
        progress.setMessage(context.getString(R.string.txt_loading_message));
        progress.setCancelable(false);

        return progress;
    }

    public static Date stringToDate(String aDate) {
        if (aDate == null) {
            return null;
        }

        aDate = aDate.replace("T", " ");
        ParsePosition pos = new ParsePosition(0);
        SimpleDateFormat simpledateformat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        return simpledateformat.parse(aDate, pos);
    }

    public static Date stringToDate(String aDate, String format) {
        if (aDate == null) {
            return null;
        }

        aDate = aDate.replace("T", " ");
        ParsePosition pos = new ParsePosition(0);
        SimpleDateFormat simpledateformat = new SimpleDateFormat(format);
        return simpledateformat.parse(aDate, pos);
    }

    public static String formatApiDate(Date date) {
        long milliseconds = date.getTime();
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(milliseconds);

        int year = calendar.get(Calendar.YEAR);
        if (year == 1970) year = 2021;
        int month = calendar.get(Calendar.MONTH) + 1;
        if (month == 0) month = 01;
        int day = calendar.get(Calendar.DAY_OF_MONTH);
        if (day == 0) day = 01;
        int hour = calendar.get(Calendar.HOUR_OF_DAY);
        int minute = calendar.get(Calendar.MINUTE);
        int second = calendar.get(Calendar.SECOND);

        return year + "-" + formatValue(month) + "-" + formatValue(day) + "T" + formatValue(hour) + ":" + formatValue(minute) + ":" + formatValue(second) + ".000Z";
    }

    public static String formatValue(int value) {
        String valueString = "" + value + "";

        if (valueString.length() == 1) {
            valueString = "0" + valueString;
        }

        return valueString;
    }
}