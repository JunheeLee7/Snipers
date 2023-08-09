using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingValue : MonoBehaviour
{
    StartSceneManager manage;
    Slider slider;

    private float nowTime;
    private void Start()
    {
        manage = FindObjectOfType<StartSceneManager>();
        slider = GetComponent<Slider>();
        StartCoroutine(SliderValued());
    }

    IEnumerator SliderValued()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(1);
        oper.allowSceneActivation = false;

        while (slider.value <= 1.0f)
        {
            yield return null;

            nowTime += Time.deltaTime;

            if(nowTime < 9.0f)
            {
                slider.value = nowTime / 9.0f;
            }
            else
            {
                slider.value = 1.0f;
                oper.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
