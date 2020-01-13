package com.akitaka.myapplication;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Environment;
import android.support.v4.app.FragmentActivity;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.JuchengCompany.CPRHeart.MUnityPlayer;
import com.unity3d.player.UnityPlayer;

import java.io.File;


public class MainActivity extends FragmentActivity implements View.OnClickListener {

    private LinearLayout mLinear;
    /**
     * 哈哈哈按键
     */
    private Button mBtn;
    //    private MUnityPlayer mUnityPlayer;
    private MUnityPlayer mUnityPlayer;
    private TextView mTv;
    private Button mBtnAdd;
    private Button mBtnDel;
    private Button mBtnJia;
    private Button mBtnXiao;
    private Button mBtnTinh;
    private Button mBtnResume;
    private Button mBtnPause;
    private Button mBtn1;
    /**
     * 动态2
     */
    private Button mBtn2;
    private EditText mEt;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //去掉标题栏
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 全屏
        //去掉状态栏
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_main);
        initView();
        mUnityPlayer = new MUnityPlayer(this);
//        mEt.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_FLAG_CAP_CHARACTERS | InputType.TYPE_TEXT_FLAG_NO_SUGGESTIONS);
        // 添加Unity视图
//        mEt.removeTextChangedListener();
    }
    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        mUnityPlayer.windowFocusChanged(hasFocus);
    }

    //这是unity调用了android的方法
    public void CallAndroid(final String ss) {
        Toast.makeText(this, "我被调用了", Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            default:
                break;
            case R.id.btn:
                UnityPlayer.UnitySendMessage("Main Camera", "AndroidCallUnity", "");
                Toast.makeText(this, "去调用Unity", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_add:
                mLinear.removeAllViews();
                mLinear.addView(mUnityPlayer.getView());
                mUnityPlayer.requestFocus();
                Toast.makeText(this, "显示", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_del:
                mLinear.removeAllViews();
                mUnityPlayer.clearFocus();
                Toast.makeText(this, "隐藏", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_jia:
                mUnityPlayer.start();
                Toast.makeText(this, "加载", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_xiao:
                mUnityPlayer.destroy();
                mUnityPlayer.clearFocus();
                mLinear.removeAllViews();
                Toast.makeText(this, "销毁", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_tinh:
                mUnityPlayer.stop();
                Toast.makeText(this, "停止", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_resume:
                mUnityPlayer.resume();
                Toast.makeText(this, "前台", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_pause:
                mUnityPlayer.pause();
                Toast.makeText(this, "暂停", Toast.LENGTH_SHORT).show();
                break;
            case R.id.btn_1:
                UnityPlayer.UnitySendMessage("AndriodMethodMgr", "CallUnitySetPath", Environment.getExternalStorageDirectory() + "");
                if (new File((Environment.getExternalStorageDirectory() + "/broadleaf")).exists()) {
                    Toast.makeText(this, "动态1=" + Environment.getExternalStorageDirectory() + "/broadleaf", Toast.LENGTH_SHORT).show();
                }
                break;
            case R.id.btn_2:
                UnityPlayer.UnitySendMessage("AndriodMethodMgr", "CallUnitySetBundleAndPrefab", "conifer#Conifer_Desktop_Prefab");
                Toast.makeText(this, "动态2=" + Environment.getExternalStorageDirectory() + "/broadleaf", Toast.LENGTH_SHORT).show();
                break;
        }
    }

    private void initView() {
        mLinear = findViewById(R.id.linear);
        mBtn = findViewById(R.id.btn);
        mBtn.setOnClickListener(this);
        mTv = findViewById(R.id.tv);
        mBtnAdd = findViewById(R.id.btn_add);
        mBtnAdd.setOnClickListener(this);
        mBtnDel = findViewById(R.id.btn_del);
        mBtnDel.setOnClickListener(this);
        mBtnJia = findViewById(R.id.btn_jia);
        mBtnJia.setOnClickListener(this);
        mBtnXiao = findViewById(R.id.btn_xiao);
        mBtnXiao.setOnClickListener(this);
        mBtnTinh = findViewById(R.id.btn_tinh);
        mBtnTinh.setOnClickListener(this);
        mBtnResume = findViewById(R.id.btn_resume);
        mBtnResume.setOnClickListener(this);
        mBtnPause = findViewById(R.id.btn_pause);
        mBtnPause.setOnClickListener(this);
        mBtn1 = findViewById(R.id.btn_1);
        mBtn1.setOnClickListener(this);
        mBtn2 = findViewById(R.id.btn_2);
        mBtn2.setOnClickListener(this);
        mEt = findViewById(R.id.et);
    }

    @Override
    protected void onNewIntent(Intent intent) {
        setIntent(intent);
    }

    // Quit Unity
    @Override
    protected void onDestroy() {
        mUnityPlayer.destroy();
        super.onDestroy();
    }

    // Pause Unity
    @Override
    protected void onPause() {
        super.onPause();
        mUnityPlayer.pause();
    }

    // Resume Unity
    @Override
    protected void onResume() {
        super.onResume();
        mUnityPlayer.resume();
    }

    @Override
    protected void onStart() {
        super.onStart();
        mUnityPlayer.start();
    }

    @Override
    protected void onStop() {
        super.onStop();
        mUnityPlayer.stop();
    }

    // Low Memory Unity
    @Override
    public void onLowMemory() {
        super.onLowMemory();
        mUnityPlayer.lowMemory();
    }

    // Trim Memory Unity
    @Override
    public void onTrimMemory(int level) {
        super.onTrimMemory(level);
        if (level == TRIM_MEMORY_RUNNING_CRITICAL) {
            mUnityPlayer.lowMemory();
        }
    }

    // This ensures the layout will be correct.
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        mUnityPlayer.configurationChanged(newConfig);
    }

    // For some reason the multiple keyevent type is not supported by the ndk.
    // Force event injection by overriding dispatchKeyEvent().
    @SuppressLint("RestrictedApi")
    @Override
    public boolean dispatchKeyEvent(KeyEvent event) {
        if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
            return mUnityPlayer.injectEvent(event);
        return super.dispatchKeyEvent(event);
    }

    // Pass any events not handled by (unfocused) views straight to UnityPlayer
    @Override
    public boolean onKeyUp(int keyCode, KeyEvent event) {
        return mUnityPlayer.injectEvent(event);
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        return mUnityPlayer.injectEvent(event);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        return mUnityPlayer.injectEvent(event);
    }

    /*API12*/
    public boolean onGenericMotionEvent(MotionEvent event) {
        return mUnityPlayer.injectEvent(event);
    }
}
