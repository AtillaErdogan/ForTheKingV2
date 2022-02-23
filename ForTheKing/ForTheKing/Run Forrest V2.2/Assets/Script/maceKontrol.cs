using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif
using UnityEditor;
public class maceKontrol : MonoBehaviour
{

    public int resim;
    bool aradakimesafeyibirkereal = true;
    GameObject[] hedef;
    Vector3 aradakimesafe;
    int aradakimesafesayaç = 0;
    bool ilermigerimi = true;
    GameObject Karakter;
    public GameObject kursun;
    public LayerMask layermask;
    RaycastHit2D ray;
    int hız = 5;
    float zaman = 0;

    public Sprite on;
    public Sprite arka;
    void Start()
    {
        Karakter = GameObject.FindGameObjectWithTag("Player");
        hedef = new GameObject[transform.childCount];
        for (int i = 0; i < hedef.Length; i++)
        {
            hedef[i] = transform.GetChild(0).gameObject;
            hedef[i].transform.SetParent(transform.parent);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dusmankontrol();
        if (ray.collider.tag == "Player")
        {
            hız = 5;
            ates();
        }
        else
        {
            hız = 3;
            Debug.Log("görmüyor");
        }
        hedefeGit();
    }

    void ates()
    {
        zaman += Time.deltaTime;
        if (zaman > Random.Range(0.2f, 7))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);

            zaman = 0;
        }
    }
    void hedefeGit()
    {
        if (aradakimesafeyibirkereal)
        {
            aradakimesafe = (hedef[aradakimesafesayaç].transform.position - transform.position).normalized;
            aradakimesafeyibirkereal = false;

        }
        float mesafe = Vector3.Distance(transform.position, hedef[aradakimesafesayaç].transform.position);
        transform.position += aradakimesafe * Time.deltaTime * hız;
        if (mesafe < 10f)
        {
            aradakimesafeyibirkereal = true;
            if (aradakimesafesayaç == hedef.Length - 1)
            {
                ilermigerimi = false;
            }
            else if (aradakimesafesayaç == 0)
            {
                ilermigerimi = true;
            }

            if (ilermigerimi)
            {
                aradakimesafesayaç++;
            }
            else
            {
                aradakimesafesayaç--;
            }

        }
    }
    void Dusmankontrol()
    {
        Vector3 rayYonum = Karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 100, layermask);
        
    }
    void Hedef()//testerenin'hedefleri
    {

    }

    public Vector3 getyon()
    {
        return (Karakter.transform.position - transform.position);
    }



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 0.5f);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif

}
#if UNITY_EDITOR
[CustomEditor(typeof(maceKontrol))]
[System.Serializable]
class maceKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        maceKontrol script = (maceKontrol)target;
        if (GUILayout.Button("Oluştur"))
        {
            GameObject objec = new GameObject();
            objec.transform.parent = script.transform;
            objec.transform.position = script.transform.position;
            objec.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("on"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arka"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }
}
#endif



