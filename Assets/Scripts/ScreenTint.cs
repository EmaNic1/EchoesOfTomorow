using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScreenTint : MonoBehaviour
{
    [SerializeField]
    Color unTintColor;

    [SerializeField]
    Color tintColor;
    
    public float speed = 0.5f;
    
    float f;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while(f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(unTintColor, tintColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator UnTintScreen()
    {
        while(f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(tintColor, unTintColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }
}
