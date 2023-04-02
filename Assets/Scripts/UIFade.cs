using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance { get; set; }
    private Image image;
    [SerializeField]
    private bool isFading;
    [SerializeField]
    private bool isFadingIn;
    [SerializeField]
    private float fadeDuration;

    public void FadeOut(float duration)
    {
        fadeDuration = duration;
        isFading = true;
        isFadingIn = false;
    }
    public void FadeIn(float duration)
    {
        fadeDuration = duration;
        isFading = true;
        isFadingIn = true;
    }

    private void Update()
    {
        if (isFading)
        {
            if (isFadingIn)
            {
                SetOpacity(0);
            }
            else
            {
                SetOpacity(1);
            }
        }
    }

    private void SetOpacity(float opacity)
    {
        Color newColor = image.color;
        newColor.a = opacity;
        image.color = Color.Lerp(image.color, newColor, Time.deltaTime/fadeDuration);
    }

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
    }
}
