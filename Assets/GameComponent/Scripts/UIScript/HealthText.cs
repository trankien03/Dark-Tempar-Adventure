using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    //pixel per second
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    RectTransform textTransform;
    public float timeToFade = 1f;

    private Color startColor;
    TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private float timeElapse = 0f;

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapse += Time.deltaTime;
        if (timeElapse < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapse / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
