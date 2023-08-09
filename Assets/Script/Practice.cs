using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // �ν����Ϳ��� ���������ϵ���
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
    // ī�޶��� ������
    public float RotSpped = 180.0f;
    public ZoomData myZoom = new ZoomData(3.0f);
    public Transform target;
    float lenghth;
    Vector3 offS;

    Camera myCam = null;

    Vector3 curRot = Vector3.zero;

    // ���Ʒ� ���� ����
    public Vector2 lookUpRange = new Vector2(-60, 80);

    public LayerMask crashMask;

    // �ڿ������� �㸮�� ������ �ֵ����ϴ� ����
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

        // ī�޶� �ٴڿ� �U���� ���� ����

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
