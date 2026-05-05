package com.example.myapplication;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.imageview.ShapeableImageView;

import java.util.List;

public class ContactAdapter extends RecyclerView.Adapter<ContactAdapter.ContactViewHolder> {

    private final List<Contact> contactList;

    public ContactAdapter(List<Contact> contactList) {
        this.contactList = contactList;
    }

    @NonNull
    @Override
    public ContactViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_contact, parent, false);
        return new ContactViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ContactViewHolder holder, int position) {
        Contact contact = contactList.get(position);
        holder.nameTextView.setText(contact.getName());
        holder.lastMessageTextView.setText(contact.getLastMessage());
        holder.timeTextView.setText(contact.getTime());
        holder.avatarImageView.setImageResource(contact.getAvatarResId());
    }

    @Override
    public int getItemCount() {
        return contactList.size();
    }

    public static class ContactViewHolder extends RecyclerView.ViewHolder {
        ShapeableImageView avatarImageView;
        TextView nameTextView;
        TextView lastMessageTextView;
        TextView timeTextView;

        public ContactViewHolder(@NonNull View itemView) {
            super(itemView);
            avatarImageView = itemView.findViewById(R.id.avatar);
            nameTextView = itemView.findViewById(R.id.name);
            lastMessageTextView = itemView.findViewById(R.id.lastMessage);
            timeTextView = itemView.findViewById(R.id.time);
        }
    }
}
