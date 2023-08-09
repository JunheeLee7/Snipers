using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScenes : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    private float alpha;
    private void Start()
    {
        text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 0);
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);

        StartCoroutine(LastText(1.5f));
    }

    IEnumerator LastText(float t)
    {
        while (text1.color.a < 1)
        {
            float alpha = Time.deltaTime / t;
            text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, text1.color.a + alpha);
            yield return null;
        }
        text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 1);

        while(text1.color.a > 0)
        {
            float alpha = Time.deltaTime / t;
            text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, text1.color.a - alpha);
            yield return null;
        }
        text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 0);

        while (text2.color.a < 1)
        {
            float beta = Time.deltaTime / t;
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a + beta);
            yield return null;
        }
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 1);

        while(text2.color.a > 0)
        {
            float beta = Time.deltaTime / t;
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a - beta);
            yield return null;
        }
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);

        Application.Quit();
    }
}
