package com.example.myapplication;

import android.os.Bundle;
import android.widget.Button;
import android.widget.GridLayout;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class MainActivity extends AppCompatActivity {

    private static final int SIZE = 100;
    private static final int WIN_COUNT = 5;
    private final String[][] board = new String[SIZE][SIZE];
    private boolean isXTurn = true;
    private boolean isGameOver = false;

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

        GridLayout gridLayout = findViewById(R.id.gridLayout);
        gridLayout.setRowCount(SIZE);
        gridLayout.setColumnCount(SIZE);

        int buttonSize = (int) (50 * getResources().getDisplayMetrics().density);

        for (int i = 0; i < SIZE; i++) {
            for (int j = 0; j < SIZE; j++) {
                final int row = i;
                final int col = j;
                Button button = new Button(this);
                
                GridLayout.LayoutParams params = new GridLayout.LayoutParams();
                params.width = buttonSize;
                params.height = buttonSize;
                params.rowSpec = GridLayout.spec(i);
                params.columnSpec = GridLayout.spec(j);
                button.setLayoutParams(params);
                
                button.setPadding(0, 0, 0, 0);
                button.setTextSize(14);
                
                button.setOnClickListener(v -> {
                    if (isGameOver || board[row][col] != null) {
                        return;
                    }

                    String symbol = isXTurn ? "X" : "O";
                    button.setText(symbol);
                    board[row][col] = symbol;

                    if (checkWin(row, col, symbol)) {
                        isGameOver = true;
                        Toast.makeText(MainActivity.this, "Winner: " + symbol, Toast.LENGTH_LONG).show();
                    } else {
                        isXTurn = !isXTurn;
                    }
                });
                gridLayout.addView(button);
            }
        }
    }

    private boolean checkWin(int row, int col, String symbol) {
        return checkDirection(row, col, 1, 0, symbol) ||
               checkDirection(row, col, 0, 1, symbol) ||
               checkDirection(row, col, 1, 1, symbol) ||
               checkDirection(row, col, 1, -1, symbol);
    }

    private boolean checkDirection(int row, int col, int dRow, int dCol, String symbol) {
        int count = 1;

        int r = row + dRow;
        int c = col + dCol;
        while (r >= 0 && r < SIZE && c >= 0 && c < SIZE && symbol.equals(board[r][c])) {
            count++;
            r += dRow;
            c += dCol;
        }

        r = row - dRow;
        c = col - dCol;
        while (r >= 0 && r < SIZE && c >= 0 && c < SIZE && symbol.equals(board[r][c])) {
            count++;
            r -= dRow;
            c -= dCol;
        }

        return count >= WIN_COUNT;
    }
}