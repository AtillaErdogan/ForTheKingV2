using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandık : MonoBehaviour
{
    public Sprite[] animasyonkareleri;
    SpriteRenderer spriterenderer;
    float zaman = 0;
    int animasyonkareleriS = 0;
    void Start()
    {
        spriterenderer = GetComponent < SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman = Time.deltaTime;
        if (zaman>5f)
        {
            spriterenderer.sprite = animasyonkareleri[animasyonkareleriS++];
            if (animasyonkareleri.Length==animasyonkareleriS)
            {
                animasyonkareleriS = animasyonkareleri.Length - 1;
            }
;        }
    }
}
