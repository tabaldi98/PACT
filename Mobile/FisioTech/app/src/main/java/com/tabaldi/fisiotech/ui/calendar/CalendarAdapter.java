package com.tabaldi.fisiotech.ui.calendar;


import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.tabaldi.fisiotech.R;
import com.tabaldi.fisiotech.base.Utils;

import java.util.List;

public class CalendarAdapter extends RecyclerView.Adapter<CalendarAdapter.ViewHolderCalendar> {
    public List<CalendarAttendances> calendarAttendances;

    public CalendarAdapter(List<CalendarAttendances> clients) {
        this.calendarAttendances = clients;
    }

    @NonNull
    @Override
    public CalendarAdapter.ViewHolderCalendar onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.calendar_line, parent, false);

        return new ViewHolderCalendar(view, parent.getContext());
    }

    @Override
    public void onBindViewHolder(@NonNull CalendarAdapter.ViewHolderCalendar holder, int position) {
        if (calendarAttendances == null || calendarAttendances.size() < 1) {
            return;
        }

        CalendarAttendances attendance = calendarAttendances.get(position);

        holder.txtName.setText(attendance.clientName);

        holder.txtIcon.setText(attendance.clientName.substring(0, 1));

        holder.txtDate.setText(
                "Das " +
                        android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(attendance.startAttendance)) +
                        " até as " +
                        android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(attendance.endAttendance)));
    }

    @Override
    public int getItemCount() {
        return calendarAttendances.size();
    }


    public class ViewHolderCalendar extends RecyclerView.ViewHolder {

        public TextView txtIcon;
        public TextView txtName;
        public TextView txtDate;

        public ViewHolderCalendar(@NonNull View itemView, final Context context) {
            super(itemView);
            txtIcon = itemView.findViewById(R.id.txt_caledar_icon);
            txtName = itemView.findViewById(R.id.txt_caledar_list_name);
            txtDate = itemView.findViewById(R.id.txt_caledar_list_hours);

            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if (calendarAttendances.size() > 0) {

                    }
                }
            });
        }
    }
}
