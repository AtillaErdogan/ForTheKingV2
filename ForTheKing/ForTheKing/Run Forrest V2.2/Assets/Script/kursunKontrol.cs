using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol : MonoBehaviour
{
    mace dusman;
    Rigidbody2D fizik;
    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<mace>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getyon()*500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
