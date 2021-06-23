package com.tabaldi.fisiotech.ui.clients;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Typeface;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.recyclerview.widget.RecyclerView;

import com.tabaldi.fisiotech.ClientAddActivity;
import com.tabaldi.fisiotech.R;
import com.tabaldi.fisiotech.base.Utils;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ClientAdapter extends RecyclerView.Adapter<ClientAdapter.ViewHolderClient> {
    public List<Client> clients;

    public ClientAdapter(List<Client> clients) {
        this.clients = clients;
    }

    @NonNull
    @Override
    public ClientAdapter.ViewHolderClient onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.client_list_line, parent, false);

        return new ViewHolderClient(view, parent.getContext());
    }

    @Override
    public void onBindViewHolder(@NonNull ClientAdapter.ViewHolderClient holder, int position) {
        if (clients == null || clients.size() < 1) {
            return;
        }

        Client client = clients.get(position);

        holder.txtName.setText(client.name);
        holder.txtName.setTypeface(Typeface.DEFAULT, client.enabled ? Typeface.BOLD : Typeface.NORMAL);

        holder.txtIcon.setText(client.name.substring(0, 1));

        holder.txtPhone.setText(client.phone);
        holder.txtPhone.setTypeface(Typeface.DEFAULT, client.enabled ? Typeface.BOLD : Typeface.NORMAL);

        holder.txtDate.setText(android.text.format.DateFormat.format(Utils.VIEW_DATE_FORMAT, Utils.stringToDate(client.dateOfBirth)));
        holder.txtDate.setTypeface(Typeface.DEFAULT, client.enabled ? Typeface.BOLD : Typeface.NORMAL);
    }

    @Override
    public int getItemCount() {
        return clients.size();
    }


    public class ViewHolderClient extends RecyclerView.ViewHolder {

        public TextView txtName;
        public TextView txtIcon;
        public TextView txtPhone;
        public TextView txtDate;

        public ViewHolderClient(@NonNull View itemView, final Context context) {
            super(itemView);
            txtName = itemView.findViewById(R.id.txt_client_list_name);
            txtIcon = itemView.findViewById(R.id.txt_client_icon);
            txtPhone = itemView.findViewById(R.id.txt_client_list_phone);
            txtDate = itemView.findViewById(R.id.txt_client_list_date);

            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if (clients.size() > 0) {
                        Intent it = new Intent(context, ClientAddActivity.class);
                        it.putExtra("Client", clients.get(getLayoutPosition()));
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
                                    ClientRemoveCommand command = new ClientRemoveCommand();
                                    command.AddId(clients.get(getLayoutPosition()).id);

                                    IClientHttpRepository.createInstance(context).RemoveClient(command).enqueue(new Callback<Boolean>() {
                                        @Override
                                        public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                                            if (response.isSuccessful()) {
                                                clients.remove(getLayoutPosition());
                                                notifyItemRemoved(getLayoutPosition());
                                                Toast.makeText(context, R.string.txt_client_remove_success, Toast.LENGTH_SHORT).show();
                                            }
                                        }

                                        @Override
                                        public void onFailure(Call<Boolean> call, Throwable t) {
                                            /***/
                                            Log.i("", "");
                                        }
                                    });

                                    break;
                            }
                        }
                    };

                    AlertDialog.Builder builder = new AlertDialog.Builder(context);
                    builder
                            .setMessage("Deseja deletar o(a) paciente " + clients.get(getLayoutPosition()).name + "?")
                            .setPositiveButton("Sim", dialogClickListener)
                            .setNegativeButton("Não", dialogClickListener)
                            .show();

                    return false;
                }
            });
        }
    }
}
