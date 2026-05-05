package com.example.myapplication;

import android.os.Bundle;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        RecyclerView recyclerView = findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        List<Contact> contacts = new ArrayList<>();
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));
        contacts.add(new Contact("Test", "Test", "12:00", R.drawable.ic_launcher_background));

        ContactAdapter adapter = new ContactAdapter(contacts);
        recyclerView.setAdapter(adapter);
    }
}
