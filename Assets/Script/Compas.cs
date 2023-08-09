using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compas : MonoBehaviour
{
    public GameObject cams;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Image>().material;
    }

    private void Update()
    {
        float vecY = cams.transform.rotation.eulerAngles.y;
        if (vecY <= 360.0f)
        {
            float z = vecY / 360;
            mat.mainTextureOffset = new Vector2(z, 0);
        }
    }
}
