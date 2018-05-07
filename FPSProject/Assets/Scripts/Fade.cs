using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {


    public float fadeTime;

    //FadeIn
    public void CallFadeIn()
    {
        StartCoroutine(FadeIn(GetComponent<Image>()));
    }

   IEnumerator FadeIn(Image image)
    {

        Color tmpColor = image.color;

        while (tmpColor.a > 0f)
        {
            tmpColor.a -= Time.deltaTime / fadeTime;
            image.color = tmpColor;

            if(tmpColor.a <= 0f)
            {
                tmpColor.a = 0f;
            }
            yield return null;
        }

        image.color = tmpColor;
    }
    //Fade Out
    public void CallFadeOut()
    {
        StartCoroutine(FadeOut(GetComponent<Image>()));
    }

    IEnumerator FadeOut(Image image)
    {

        Color tmpColor = image.color;

        while (tmpColor.a < 1f)
        {
            tmpColor.a += Time.deltaTime / fadeTime;
            image.color = tmpColor;

            if (tmpColor.a <= 0f)
            {
                tmpColor.a = 0f;
            }
            yield return null;
        }

        image.color = tmpColor;
    }
}
