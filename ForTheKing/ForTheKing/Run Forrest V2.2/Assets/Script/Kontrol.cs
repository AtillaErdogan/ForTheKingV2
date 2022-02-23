using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kontrol : MonoBehaviour
{
    public Sprite[] beklemeAnimasyon;
    public Sprite[] yurumeAnimasyon;
    public Image Menu;
    public Text altınT;
    public int altınS=0;
    
    public Sprite[] ziplamaAnimasyon;
    public Text cantext;
    int can = 100;


    SpriteRenderer spriteRenderer;
    Rigidbody2D yerCekimi;
    Vector3 vector3;
    Vector3 kameraSonPozisyon;
    Vector3 kameraIlkPozisyon;
    float siyah=0;
    float horizontal = 0;
    float beklemeAnimasyonSure = 0;
    float yurumuAnimasyonSure = 0;

    bool ziplamakontrol = true;
    int beklemeAnimasyonSayac=0;
    int yurumeAnimasyonSayac=0;
    float menuyedönzaman=0;
    GameObject kamera;
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("kaçıncılevel", SceneManager.GetActiveScene().buildIndex);
        spriteRenderer = GetComponent<SpriteRenderer>();
        yerCekimi = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        kameraIlkPozisyon = kamera.transform.position - transform.position;
        cantext.text = "Can 100 ";
        altınT.text = "Altın ";

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ziplamakontrol)
            {
                yerCekimi.AddForce(new Vector2(0, 400));
                ziplamakontrol = false;
            }

        }
        
    }
    void FixedUpdate()
    {
        Hareket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.6f;
            cantext.enabled = false;
            siyah += 0.03f;
            Menu.color = new Color(0,0,0,siyah);
            menuyedönzaman += Time.deltaTime;
            if (menuyedönzaman>1)
            {
                SceneManager.LoadScene("Demo");
            }
        }

    }
    void LateUpdate()
    {
        KameraKontrol();

    }

    void Animasyon()
    {
        if (ziplamakontrol)
        {
            if (horizontal == 0)
            {
                beklemeAnimasyonSure += Time.deltaTime;
                if (beklemeAnimasyonSure > 0.10f)
                {
                    spriteRenderer.sprite = beklemeAnimasyon[beklemeAnimasyonSayac++];
                    if (beklemeAnimasyonSayac == beklemeAnimasyon.Length)
                    {
                        beklemeAnimasyonSayac = 0;
                    }
                    beklemeAnimasyonSure = 0;
                }

            }
            else if (horizontal > 0)
            {
                yurumuAnimasyonSure += Time.deltaTime;
                if (yurumuAnimasyonSure > 0.09f)
                {
                    spriteRenderer.sprite = yurumeAnimasyon[yurumeAnimasyonSayac++];
                    if (yurumeAnimasyonSayac == yurumeAnimasyon.Length)
                    {
                        yurumeAnimasyonSayac = 0;
                    }
                    yurumuAnimasyonSure = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {

                yurumuAnimasyonSure += Time.deltaTime;
                if (yurumuAnimasyonSure > 0.09f)
                {
                    spriteRenderer.sprite = yurumeAnimasyon[yurumeAnimasyonSayac++];
                    if (yurumeAnimasyonSayac == yurumeAnimasyon.Length)
                    {
                        yurumeAnimasyonSayac = 0;
                    }
                    yurumuAnimasyonSure = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (yerCekimi.velocity.y>0)
            {
                spriteRenderer.sprite = ziplamaAnimasyon[0];
            }
            else
            {
                spriteRenderer.sprite = ziplamaAnimasyon[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }
    void Hareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //Hareket hızı
        vector3 = new Vector3(horizontal * 4, yerCekimi.velocity.y, 0);
        yerCekimi.velocity = vector3;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        ziplamakontrol = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="kursun")
        {
            can= can - 1;
            cantext.text = "Can   " + can;
        }
        if (col.gameObject.tag == "dusman")
        {
            can = can - 10;
            cantext.text = "Can   " + can;
        }
        if (col.gameObject.tag == "saw")
        {
            can = can - 10;
            cantext.text = "Can   " + can;
        }
        if (col.gameObject.tag == "sandık")
        {
            can = can + 10;
            cantext.text = "Can   " + can;
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<sandık>().enabled = true;
            Destroy(col.gameObject,2);
            

            
        }
        if (col.gameObject.tag == "altın")
        {
            altınS = altınS+1;
            altınT.text = "Altın " + altınS;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "bitis")
        {
            if (altınS>=5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
    }
    


    void KameraKontrol()
    {
        kameraSonPozisyon = kameraIlkPozisyon + transform.position;
        kamera.transform.position = kameraSonPozisyon;

    }
    
}
