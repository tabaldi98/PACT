package com.tabaldi.fisiotech.ui.attendances;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.tabaldi.fisiotech.AttendanceAddActivity;
import com.tabaldi.fisiotech.AttendanceListActivity;
import com.tabaldi.fisiotech.R;
import com.tabaldi.fisiotech.base.Utils;
import com.tabaldi.fisiotech.ui.clients.Client;
import com.tabaldi.fisiotech.ui.clients.ClientAdapter;
import com.tabaldi.fisiotech.ui.clients.ClientRemoveCommand;
import com.tabaldi.fisiotech.ui.clients.IClientHttpRepository;

import org.jetbrains.annotations.NotNull;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AttendanceAdapter extends RecyclerView.Adapter<AttendanceAdapter.ViewHolderAttendance> {
    private final List<AttendanceModel> _attendances;

    public AttendanceAdapter(List<AttendanceModel> attendances) {
        this._attendances = attendances;
    }

    @NonNull
    @Override
    public ViewHolderAttendance onCreateViewHolder(@NonNull @NotNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.attendance_list_line, parent, false);

        return new AttendanceAdapter.ViewHolderAttendance(view, parent.getContext());
    }

    @Override
    public void onBindViewHolder(@NonNull @NotNull ViewHolderAttendance holder, int position) {
        if (_attendances == null || _attendances.size() < 1) {
            return;
        }

        AttendanceModel attendanceModel = _attendances.get(position);
        holder.txtIcon.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(attendanceModel.date)).toString().substring(0,2));

        holder.txtDate.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(attendanceModel.date)));

        holder.txtHour.setText(android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(attendanceModel.hourInitial))
                + " - "
                + android.text.format.DateFormat.format(Utils.VIEW_TIME_FORMAT, Utils.stringToDate(attendanceModel.hourFinish)));
    }

    @Override
    public int getItemCount() {
        return _attendances.size();
    }

    public class ViewHolderAttendance extends RecyclerView.ViewHolder {

        public TextView txtIcon;
        public TextView txtDate;
        public TextView txtHour;

        public ViewHolderAttendance(@NonNull @NotNull View itemView, final Context context) {
            super(itemView);

            txtIcon = itemView.findViewById(R.id.txt_attendance_client_icon);
            txtDate = itemView.findViewById(R.id.txt_attendance_client_date);
            txtHour = itemView.findViewById(R.id.txt_attendance_client_list_hour);

            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if (_attendances.size() > 0) {
                        Intent it = new Intent(context, AttendanceAddActivity.class);
                        it.putExtra("Attendance", _attendances.get(getLayoutPosition()));
                        context.startActivity(it);
                    }
                }
            });

            itemView.setOnLongClickListener(new View.OnLongClickListener() {
                @Override
                public boolean onLongClick(View v) {
                    DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            switch (which) {
                                case DialogInterface.BUTTON_POSITIVE:
                                    AttendanceRemoveCommand command = new AttendanceRemoveCommand();
                                    command.AddId(_attendances.get(getLayoutPosition()).id);

                                    IClientHttpRepository.createInstance(context).RemoveAttendance(command).enqueue(new Callback<Boolean>() {
                                        @Override
                                        public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                                            if (response.isSuccessful()) {
                                                _attendances.remove(getLayoutPosition());
                                                notifyItemRemoved(getLayoutPosition());
                                                Toast.makeText(context, R.string.txt_attendance_remove_success, Toast.LENGTH_SHORT).show();
                                            }
                                        }

                                        @Override
                                        public void onFailure(Call<Boolean> call, Throwable t) {
                                        }
                                    });

                                    break;
                            }
                        }
                    };

                    AlertDialog.Builder builder = new AlertDialog.Builder(context);
                    builder
                            .setMessage("Deseja deletar o atendimento de " + txtDate.getText().toString() + "?")
                            .setPositiveButton("Sim", dialogClickListener)
                            .setNegativeButton("Não", dialogClickListener)
                            .show();

                    return false;
                }
            });
        }
    }
}
