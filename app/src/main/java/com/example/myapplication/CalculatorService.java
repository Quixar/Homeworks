package com.example.myapplication;

import android.app.ActivityManager;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.util.Log;

import java.util.List;

public class CalculatorService extends Service {
    private static final String TAG = "CalculatorService";
    private final Handler handler = new Handler(Looper.getMainLooper());
    private String calculatorPackageName;
    private boolean isRunning = false;

    private final Runnable runnable = new Runnable() {
        @Override
        public void run() {
            if (isRunning) {
                if (!isCalculatorRunning()) {
                    Log.d(TAG, "Calculator is not running, launching...");
                    launchCalculator();
                }
                handler.postDelayed(this, 5000);
            }
        }
    };

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        if (!isRunning) {
            isRunning = true;
            calculatorPackageName = getCalculatorPackageName();
            handler.post(runnable);
        }
        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        isRunning = false;
        handler.removeCallbacks(runnable);
        super.onDestroy();
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    private String getCalculatorPackageName() {
        Intent intent = new Intent(Intent.ACTION_MAIN);
        intent.addCategory(Intent.CATEGORY_APP_CALCULATOR);
        ResolveInfo resolveInfo = getPackageManager().resolveActivity(intent, PackageManager.MATCH_DEFAULT_ONLY);
        if (resolveInfo != null) {
            return resolveInfo.activityInfo.packageName;
        }
        return "com.google.android.calculator";
    }

    private boolean isCalculatorRunning() {
        ActivityManager activityManager = (ActivityManager) getSystemService(Context.ACTIVITY_SERVICE);
        List<ActivityManager.RunningAppProcessInfo> processes = activityManager.getRunningAppProcesses();
        if (processes != null) {
            for (ActivityManager.RunningAppProcessInfo processInfo : processes) {
                if (processInfo.processName.equals(calculatorPackageName)) {
                    return true;
                }
            }
        }
        return false;
    }

    private void launchCalculator() {
        try {
            Intent intent = getPackageManager().getLaunchIntentForPackage(calculatorPackageName);
            if (intent != null) {
                intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(intent);
            } else {
                Intent calcIntent = new Intent(Intent.ACTION_MAIN);
                calcIntent.addCategory(Intent.CATEGORY_APP_CALCULATOR);
                calcIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(calcIntent);
            }
        } catch (Exception e) {
            Log.e(TAG, "Could not launch calculator: " + e.getMessage());
            isRunning = false;
            handler.removeCallbacks(runnable);
        }
    }
}
