using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Anamenükontrol : MonoBehaviour
{
    GameObject basla;
    GameObject bolumsec;
    GameObject Bölümler;
    GameObject level1,level2,level3;
    void Start()
    {
        level1 = GameObject.Find("Level 1");
        level2 = GameObject.Find("Level 2");
        level3 = GameObject.Find("Level 3");

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        Bölümler = GameObject.Find("Bölümler");
        

        for (int i = 0; i < PlayerPrefs.GetInt("kaçıncılevel"); i++)
        {
            Bölümler.transform.GetChild(i).GetComponent<Button>().interactable = true;

         }
    }
    public void buton(int gelenbutun)
    {
        if (gelenbutun==1)
        {
            SceneManager.LoadScene(1);
        }
        else if (gelenbutun==2)
        {
            level1.SetActive(true);
            level2.SetActive(true);
            level3.SetActive(true);
        }
        else if (gelenbutun == 3)
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
