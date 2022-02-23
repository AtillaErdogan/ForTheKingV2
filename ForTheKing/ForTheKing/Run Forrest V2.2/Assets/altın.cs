using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altın : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
    SpriteRenderer SpriteRenderer;
    float zaman = 0;
    int animasyonKareleriS = 0;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman>0.1f)
        {
            SpriteRenderer.sprite = animasyonKareleri[animasyonKareleriS++];
            if (animasyonKareleri.Length==animasyonKareleriS)
            {
                animasyonKareleriS = 0;
            }
            zaman = 0;
        }
    }
}
