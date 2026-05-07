package com.example.hwroom;

import android.os.Bundle;
import android.view.*;
import android.widget.*;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.*;
import androidx.annotation.NonNull;
import androidx.lifecycle.*;
import androidx.room.*;
import java.util.*;

// ======================== ROOM КЛАСИ ========================
@Entity(tableName = "students")
class Student {
    @PrimaryKey(autoGenerate = true)
    public long _id;
    public String firstName;
    public String lastName;
    public int age;

    public Student() {}
    public Student(String firstName, String lastName, int age) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }

    @NonNull
    @Override
    public String toString() {
        return firstName + " " + lastName + ", вік: " + age;
    }
}

@Dao
interface StudentDao {
    @Insert void insert(Student student);
    @Update void update(Student student);
    @Query("DELETE FROM students WHERE _id = :id") void deleteById(long id);
    @Query("SELECT * FROM students ORDER BY lastName") LiveData<List<Student>> getAllStudents();
    @Query("DELETE FROM students") void deleteAll();
}

@Database(entities = {Student.class}, version = 1, exportSchema = false)
abstract class AppDatabase extends RoomDatabase {
    abstract StudentDao studentDao();

    private static volatile AppDatabase INSTANCE;
    static AppDatabase getDatabase(final android.content.Context context) {
        if (INSTANCE == null) {
            synchronized (AppDatabase.class) {
                if (INSTANCE == null) {
                    INSTANCE = Room.databaseBuilder(context.getApplicationContext(),
                            AppDatabase.class, "student_database").build();
                    // Room працює саме з SQLite - це не окрема БД, а зручна надбудова (обгортка) над класичною Android-базою SQLite
                }
            }
        }
        return INSTANCE;
    }
}

class StudentRepository {
    private final StudentDao dao;
    private final LiveData<List<Student>> allStudents;

    StudentRepository(android.app.Application app) {
        AppDatabase db = AppDatabase.getDatabase(app);
        dao = db.studentDao();
        allStudents = dao.getAllStudents();
    }

    LiveData<List<Student>> getAllStudents() { return allStudents; }
    void insert(Student s) { new Thread(() -> dao.insert(s)).start(); }
    void update(Student s) { new Thread(() -> dao.update(s)).start(); }
    void deleteById(long id) { new Thread(() -> dao.deleteById(id)).start(); }
    void deleteAll() { new Thread(dao::deleteAll).start(); }
}

class StudentAdapter extends RecyclerView.Adapter<StudentAdapter.VH> {
    private List<Student> data = new ArrayList<>();

    interface OnItemClickListener {
        void onItemClick(Student student);
    }

    private OnItemClickListener listener;

    void setOnItemClickListener(OnItemClickListener l) { listener = l; }

    void setStudents(List<Student> list) {
        data = list;
        notifyDataSetChanged();
    }

    Student getStudentAt(int position) { return data.get(position); }

    @NonNull
    @Override
    public VH onCreateViewHolder(@NonNull android.view.ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext())
                .inflate(android.R.layout.simple_list_item_1, parent, false);
        return new VH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull VH holder, int pos) {
        Student s = data.get(pos);
        holder.tv.setText(s.toString());
        holder.itemView.setOnClickListener(v -> {
            if (listener != null) listener.onItemClick(s);
        });
    }

    @Override
    public int getItemCount() { return data.size(); }

    static class VH extends RecyclerView.ViewHolder {
        TextView tv;
        VH(View v) { super(v); tv = v.findViewById(android.R.id.text1); }
    }
}

public class MainActivity extends AppCompatActivity {

    // в ідеалі, звісно, всі ці класи треба рознести по окремим файлам
    public static class StudentViewModel extends AndroidViewModel {
        private final StudentRepository repo;
        private final LiveData<List<Student>> allStudents;

        public StudentViewModel(android.app.Application app) {
            super(app);
            repo = new StudentRepository(app);
            allStudents = repo.getAllStudents();
        }

        public LiveData<List<Student>> getAllStudents() { return allStudents; }
        public void insert(Student s) { repo.insert(s); }
        public void update(Student s) { repo.update(s); }
        public void deleteById(long id) { repo.deleteById(id); }
        public void deleteAll() { repo.deleteAll(); }
    }

    private StudentViewModel viewModel;
    private final StudentAdapter adapter = new StudentAdapter();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        EditText etFirstName = findViewById(R.id.etFirstName);
        EditText etLastName  = findViewById(R.id.etLastName);
        EditText etAge       = findViewById(R.id.etAge);
        Button btnAdd        = findViewById(R.id.btnAdd);
        Button btnClear      = findViewById(R.id.btnClear);
        RecyclerView rv      = findViewById(R.id.recyclerView);

        rv.setLayoutManager(new LinearLayoutManager(this));
        rv.setAdapter(adapter);

        // отримуємо ViewModel, який має доступ до Application (потрібно для Room)
        // без другого параметра (фабрики) крошиться, бо AndroidViewModel вимагає Application у конструкторі
        viewModel = new ViewModelProvider(
                this,
                ViewModelProvider.AndroidViewModelFactory.getInstance(getApplication())
        ).get(StudentViewModel.class);

        viewModel.getAllStudents().observe(this, adapter::setStudents);

        adapter.setOnItemClickListener(student -> showEditDialog(student));

        // свайп вліво — видалення
        new ItemTouchHelper(new ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.LEFT) {
            @Override
            public boolean onMove(@NonNull RecyclerView rv, @NonNull RecyclerView.ViewHolder vh,
                                  @NonNull RecyclerView.ViewHolder target) {
                return false;
            }

            @Override
            public void onSwiped(@NonNull RecyclerView.ViewHolder viewHolder, int direction) {
                Student s = adapter.getStudentAt(viewHolder.getAdapterPosition());
                viewModel.deleteById(s._id);
            }
        }).attachToRecyclerView(rv);

        btnAdd.setOnClickListener(v -> {
            String fn = etFirstName.getText().toString().trim();
            String ln = etLastName.getText().toString().trim();
            String ageStr = etAge.getText().toString().trim();
            if (!fn.isEmpty() && !ln.isEmpty() && !ageStr.isEmpty()) {
                int age = Integer.parseInt(ageStr);
                viewModel.insert(new Student(fn, ln, age));
                etFirstName.setText("");
                etLastName.setText("");
                etAge.setText("");
            }
        });

        btnClear.setOnClickListener(v -> viewModel.deleteAll());
    }

    private void showEditDialog(Student student) {
        View dialogView = LayoutInflater.from(this).inflate(R.layout.dialog_edit_student, null);
        EditText etFn  = dialogView.findViewById(R.id.dialogEtFirstName);
        EditText etLn  = dialogView.findViewById(R.id.dialogEtLastName);
        EditText etAge = dialogView.findViewById(R.id.dialogEtAge);

        etFn.setText(student.firstName);
        etLn.setText(student.lastName);
        etAge.setText(String.valueOf(student.age));

        new AlertDialog.Builder(this)
                .setTitle("Редагування")
                .setView(dialogView)
                .setPositiveButton("Зберегти", (dialog, which) -> {
                    String fn = etFn.getText().toString().trim();
                    String ln = etLn.getText().toString().trim();
                    String ageStr = etAge.getText().toString().trim();
                    if (!fn.isEmpty() && !ln.isEmpty() && !ageStr.isEmpty()) {
                        student.firstName = fn;
                        student.lastName = ln;
                        student.age = Integer.parseInt(ageStr);
                        viewModel.update(student);
                    }
                })
                .setNegativeButton("Скасувати", null)
                .show();
    }
}