using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public Transform target;//获取旋转目标
    public float speed = 1;
    float fStartDis = 0;
    float fTouchStartTime = 0;
    int nMoveOrRotate = -1;//0=rotate  1=move
    TouchPhase mPhase;
    private Vector3 initPosition;//初始位置，用于恢复位置
    private Quaternion initRotation;

    void Start()
    {
        //initPosition = transform.position;
        //initRotation = transform.rotation;
        //if (target != null)
        //{
        //    Quaternion qua = Quaternion.LookRotation(target.position - transform.position, transform.up);
        //    transform.rotation = qua;
        //}
    }

    void Update()
    {
        if (target != null)
        {
            Camerarotate();
            Camerazoom();
        }
    }
    //重置位置-每次切换病例的时候重置
    public void rePosition()
    {
        transform.position = initPosition;
        transform.rotation = initRotation;

    }
    private void Camerarotate() //摄像机围绕目标旋转操作
    {
        //transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime); //摄像机围绕目标旋转
        var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
        var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetKey(KeyCode.Mouse0))  //左键
        {
            transform.RotateAround(target.transform.position, Vector3.up, mouse_x * 5);
            transform.RotateAround(target.transform.position, transform.right, mouse_y * 5);
        }
        //if (Input.GetKey(KeyCode.Mouse2))  //中键
        //{
        //    transform.Translate(Vector3.left * (mouse_x * 15f) * Time.deltaTime);
        //    transform.Translate(Vector3.up * (mouse_y * 15f) * Time.deltaTime);
        //}
#endif

#if UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount == 1)
        {
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
               mPhase=TouchPhase.Began;
            }
            else if (Input.GetTouch(0).phase==TouchPhase.Stationary||
                    Input.GetTouch(0).phase==TouchPhase.Canceled||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                mPhase = Input.GetTouch(0).phase;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if ( nMoveOrRotate == -1) //未移动前，判断第一次移动的机制
                {
                    if (Input.GetTouch(0).deltaPosition.magnitude>3) //小于2的过滤掉
                    {
                        mPhase = TouchPhase.Moved;
                    }
                }
                else
                {
                    mPhase = TouchPhase.Moved;
                }
            }
            //text.text = Input.GetTouch(0).phase.ToString();
            // text.text = text.text +" magnitude:"+Input.GetTouch(0).deltaPosition.magnitude;
            // mytext.text = " mPhase: " + mPhase.ToString();
            if (mPhase == TouchPhase.Began)
            {
                fTouchStartTime = Time.time;
                nMoveOrRotate = -1;
            }
            else if (mPhase == TouchPhase.Ended)
            {
                fTouchStartTime = 0;
                nMoveOrRotate = -1;
            }
            else if (mPhase == TouchPhase.Moved)
            {
                if (nMoveOrRotate==-1)
                {
                    if (Time.time - fTouchStartTime > 0.5f)
                    {
                        nMoveOrRotate = 1;
                    }
                    else
                    {
                        nMoveOrRotate = 0;
                    }
                }
                
                if (nMoveOrRotate==0)
                {
                    transform.RotateAround(target.transform.position, Vector3.up, mouse_x * 5);
                    transform.RotateAround(target.transform.position, transform.right, mouse_y * 5);
                }
                else if(nMoveOrRotate==1)
                {
                    Vector2 delpos = Input.GetTouch(0).deltaPosition;
                    transform.Translate(Vector3.left * (delpos.x ) * Time.deltaTime);
                    transform.Translate(-Vector3.up * (delpos.y ) * Time.deltaTime);
                }
            }
        }
#endif

    }

    private void Camerazoom() //摄像机滚轮缩放
    {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动       
        if (Input.GetKey(KeyCode.Mouse1))   //右键
        {
            print("独一无二" + "鼠标右键" + mouse_y + "==" + Vector3.forward);
            transform.Translate(Vector3.forward * mouse_y * 0.1f, Space.World);
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -9.5f, -0.2f));
        }
#endif

#if UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase==TouchPhase.Began||Input.GetTouch(1).phase==TouchPhase.Began)
            {
                fStartDis = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            if (Input.GetTouch(0).phase==TouchPhase.Moved|| Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 pos0 = Input.GetTouch(0).position;
                Vector2 pos1 = Input.GetTouch(1).position;
                float dis = Vector2.Distance(pos0, pos1);
                transform.Translate(Vector3.forward * (dis-fStartDis)*Time.deltaTime);
                fStartDis = dis;
            }
        }
#endif
    }



}
