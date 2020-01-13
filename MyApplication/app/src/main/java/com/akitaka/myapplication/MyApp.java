package com.akitaka.myapplication;

import android.app.Application;

import me.jessyan.autosize.AutoSizeConfig;
import me.jessyan.autosize.unit.Subunits;

/**
 * Created by akitaka on 2019-11-28.
 *
 * @author akitaka
 * @filename MyApp
 * @describe
 * @email 960576866@qq.com
 */
public class MyApp extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        AutoSizeConfig.getInstance().setCustomFragment(true).getUnitsManager().setSupportDP(false).setSupportSP(false).setSupportSubunits(Subunits.MM);
    }
}
