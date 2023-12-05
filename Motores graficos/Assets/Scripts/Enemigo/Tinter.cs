using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Color tintColor;
    [SerializeField] private float timeToGoBack;

    private Color originalColor;

    private void Awake()
    {
        originalColor = spriteRenderer.color;
    }

    public void DoTint()
    {
        spriteRenderer.color = tintColor;
        Invoke("TintBackToOriginal", timeToGoBack);
    }

    public void TintBackToOriginal()
    {
        spriteRenderer.color = originalColor;
    }
}
