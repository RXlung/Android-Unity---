using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private AndroidJavaObject m_androidObj = null;
    public GameObject diqiu;

    private void Awake()
    {
    }
    void Start()
    {
        //注意-情况不同 com.unity3d.player.UnityPlayer  可能不同，可参考其他博主
        AndroidJavaClass androidClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        m_androidObj = androidClass.GetStatic<AndroidJavaObject>("currentActivity");
        diqiu.SetActive(false);
    }
    //Unity中的某个物体出发此事件
    public void UnityCallAndroid()
    {
        Debug.Log("调用方法");
        if (m_androidObj != null)
        {
            Debug.Log("调用方法进来");
            // 第一个参数是android里面java代码的方法名，第二个是携带的字符串参数
            m_androidObj.Call("CallAndroid", "我是Unity，我给你发消息了");
        }
    }
    //Android调用Unity-方法名一定要注意
    public void AndroidCallUnity(string json)
    {
        if (diqiu.activeInHierarchy)
        {
            diqiu.SetActive(false);
        }
        else
        {
            diqiu.SetActive(true);
        }
    }
}
