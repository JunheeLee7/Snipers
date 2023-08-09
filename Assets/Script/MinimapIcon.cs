using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public void SetIcon(Transform root, Color color)
    {
        GetComponent<Image>().color = color;
        StartCoroutine(Following(root));
    }

    IEnumerator Following(Transform target)
    {
        while (target != null)
        {
            Vector3 pos = Camera.allCameras[1].WorldToViewportPoint(target.position);
            pos.x = pos.x * 200.0f;
            pos.y = pos.y * 200.0f;
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
    }
}
