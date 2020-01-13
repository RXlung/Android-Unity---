package com.JuchengCompany.CPRHeart;

import android.content.Context;

import com.unity3d.player.UnityPlayer;

/**
 * Created by akitaka on 2019-10-31.
 *
 * @author akitaka
 * @filename MUnityPlayer
 * @describe
 * @email 960576866@qq.com
 */
public class MUnityPlayer extends UnityPlayer {
    public MUnityPlayer(Context context) {
        super(context);
    }

    @Override
    protected void kill() {
        //unity默认一些返回操作等会直接kill掉进程，覆写kill方法，不让他kill
    }
}
