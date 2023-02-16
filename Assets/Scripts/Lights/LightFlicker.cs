using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private new Light light;
    public float minIntensity = 0f;
    public float maxIntensity = 10f;
    public float flickerSpeed = 10f;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        if (light != null)
        {
            UpdateLightIntensity();
        }

    }

    private void UpdateLightIntensity()
    {
        float newVal = Random.Range(minIntensity, maxIntensity);
        light.intensity = Mathf.Lerp(light.intensity, newVal, flickerSpeed*Time.deltaTime);
    }
}
