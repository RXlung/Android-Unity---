using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 旋转缩放
/// </summary>
public class RotateAround : MonoBehaviour
{
    public float mouseSensitivity = 2;
    public Transform targetTrans;
    Transform thirdPCamAxle;
    Vector3 thirdPCamEuler;
    Camera thirdPCam;
    float camForwardMaxDistance = 9;
    float camForwardDistance;
    float camBackDistance;

    void Start()
    {
        thirdPCamAxle = GameObject.FindGameObjectWithTag("3rdCameraAxle").transform;
        thirdPCamEuler = thirdPCamAxle.localEulerAngles;

        thirdPCam = Camera.main;
    }

    void Update()
    {
        thirdPCamAxle.position = targetTrans.position;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        thirdPCamEuler.y += mouseX;
        if (thirdPCamEuler.z < 90 && mouseY > 0) { thirdPCamEuler.z += mouseY; }
        if (thirdPCamEuler.z > -90 && mouseY < 0) { thirdPCamEuler.z += mouseY; }
        thirdPCamAxle.localEulerAngles = thirdPCamEuler;

        Ray camForwardRay = new Ray(thirdPCam.transform.position, thirdPCam.transform.forward);
        RaycastHit forwardRayHit;
        if (Physics.Raycast(camForwardRay, out forwardRayHit))
        {
            camForwardDistance = Vector3.Distance(thirdPCam.transform.position, forwardRayHit.point);
        }

        Vector3 dir = thirdPCam.transform.position - targetTrans.position; ;
        dir = dir.normalized;
        Ray ray = new Ray(targetTrans.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            thirdPCam.transform.position = hit.point;
        }
        else
        {
            if (camForwardDistance < camForwardMaxDistance)
            {
                thirdPCam.transform.Translate(Vector3.back * Time.deltaTime * 5);
            }
        }
    }
}
