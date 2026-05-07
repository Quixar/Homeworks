package com.example.myapplication;

import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Locale;
import java.util.Map;

public class MainActivity extends AppCompatActivity {

    private TextView usdRateText, eurRateText, gbpRateText;
    private RequestQueue requestQueue;

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

        usdRateText = findViewById(R.id.usdRateText);
        eurRateText = findViewById(R.id.eurRateText);
        gbpRateText = findViewById(R.id.gbpRateText);
        Button refreshButton = findViewById(R.id.refreshButton);

        requestQueue = Volley.newRequestQueue(this);

        refreshButton.setOnClickListener(v -> fetchExchangeRates());

        fetchExchangeRates();
    }

    private void fetchExchangeRates() {
        String url = "https://open.er-api.com/v6/latest/UAH";

        JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.GET, url, null,
                response -> {
                    try {
                        JSONObject rates = response.getJSONObject("rates");
                        double uahToUsd = rates.getDouble("USD");
                        double uahToEur = rates.getDouble("EUR");
                        double uahToGbp = rates.getDouble("GBP");

                        usdRateText.setText(String.format(Locale.getDefault(), "USD: %.2f UAH", 1.0 / uahToUsd));
                        eurRateText.setText(String.format(Locale.getDefault(), "EUR: %.2f UAH", 1.0 / uahToEur));
                        gbpRateText.setText(String.format(Locale.getDefault(), "GBP: %.2f UAH", 1.0 / uahToGbp));

                    } catch (JSONException e) {
                        Toast.makeText(MainActivity.this, R.string.error_processing, Toast.LENGTH_SHORT).show();
                    }
                },
                error -> {
                    Toast.makeText(MainActivity.this, R.string.error_network, Toast.LENGTH_SHORT).show();
                }) {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> headers = new HashMap<>();
                headers.put("User-Agent", "MyCurrencyApp/1.0");
                return headers;
            }
        };

        requestQueue.add(jsonObjectRequest);
    }
}
