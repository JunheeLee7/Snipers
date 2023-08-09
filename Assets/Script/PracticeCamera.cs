using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[System.Serializable]
public struct ZoomDatas
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float cutDist;
    public float desireDist;
    public ZoomDatas(float speed)
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

public class PracticeCamera : MonoBehaviour
{
    public ZoomDatas datas = new ZoomDatas(3.0f);

    Player player;
    public Transform target;

    // 카메라의 움직임
    public float rotSpped = 180.0f;

    Camera myCam = null;

    Vector3 curRot = Vector3.zero;

    // 위아래 각도 막기
    public Vector2 lookUpRange = new Vector2(-60, 80);

    public LayerMask crashMask;

    // 카메라 alt회전
    float viewRotY = 0.0f;

    // 자연스럽게 허리를 접을수 있도록하는 변수
    public Transform mySpin = null;

    private Vector3 nowVec;
    public Transform rect;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        myCam = GetComponentInChildren<Camera>();
        datas.desireDist = datas.cutDist = Mathf.Abs(myCam.transform.localPosition.z);
        curRot = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        curRot.x -= Input.GetAxis("Mouse Y") * rotSpped * Time.deltaTime * 0.5f;   // 마우스의 위아래 움직임
        curRot.x = Mathf.Clamp(curRot.x, lookUpRange.x, lookUpRange.y);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            viewRotY = Input.GetAxis("Mouse X");

            Vector3 rotation = new Vector3(0, viewRotY * rotSpped, 0);
            transform.RotateAround(player.transform.position, Vector3.up, rotation.y);

            Vector3 lookDir = target.transform.position - transform.position;
            Quaternion newRot = Quaternion.LookRotation(lookDir);
            transform.rotation = newRot;
        }
        else
        {
            transform.position = rect.position;
            viewRotY = 0.0f;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            curRot.y += Input.GetAxis("Mouse X") * rotSpped * Time.deltaTime * 0.5f;   // 마우스의 좌우 움직임
            player.transform.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }

        float offSet = 0.5f;
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, datas.cutDist + offSet,  crashMask))
        {
            datas.cutDist = hit.distance - offSet;
        }
        myCam.transform.localPosition = Vector3.back * datas.cutDist;
    }

    private void LateUpdate()
    {

    }
}
