using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // 인스펙터에서 수정가능하도록
public struct ZoomData
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float cutDist;
    public float desireDist;
    public ZoomData(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1, 7);
        ZoomLerpSpeed = 3.0f;
        desireDist = cutDist = 0.0f;
    }

    public void Update(float dist)
    {
        desireDist += dist;
        desireDist = Mathf.Clamp(desireDist, ZoomRange.x, ZoomRange.y);
        cutDist = Mathf.Lerp(cutDist, desireDist, Time.deltaTime * ZoomLerpSpeed);
    }
}

public class Practice : MonoBehaviour
{
    // 카메라의 움직임
    public float RotSpped = 180.0f;
    public ZoomData myZoom = new ZoomData(3.0f);
    public Transform target;
    float lenghth;
    Vector3 offS;

    Camera myCam = null;

    Vector3 curRot = Vector3.zero;

    // 위아래 각도 막기
    public Vector2 lookUpRange = new Vector2(-60, 80);

    public LayerMask crashMask;

    // 자연스럽게 허리를 접을수 있도록하는 변수
    public Transform mySpin = null;

    private void Start()
    {
        transform.LookAt(target);
        myCam = GetComponentInChildren<Camera>();
        //myZoom.desireDist = myZoom.cutDist = Mathf.Abs(myCam.transform.localPosition.z);
        //curRot = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {

        }
        else
        {

        }

        myZoom.Update(Input.GetAxis("Mouse ScrollWheel"));

        // 카메라가 바닥에 뭊히는 것을 방지

        float offSet = 0.5f;
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, myZoom.cutDist + offSet, crashMask))
        {
            myZoom.cutDist = hit.distance - offSet;
        }

        myCam.transform.localPosition = Vector3.back * myZoom.cutDist;
    }

    private void LateUpdate()
    {
        mySpin.localRotation = Quaternion.Euler(curRot.x, 0, 0);
    }
}
