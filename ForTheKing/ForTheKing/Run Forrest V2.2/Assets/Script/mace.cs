using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif
using UnityEditor;
public class mace : MonoBehaviour
{
    public int resim;
    bool aradakimesafeyibirkereal = true;
    GameObject[] hedef;
    GameObject karakter;
    Vector3 aradakimesafe;
    public GameObject kursun;
    RaycastHit2D ray;
    int aradakimesafesayaç = 0;
    bool ilermigerimi = true;
    public LayerMask layermask;
    int hız = 5;
    float ateşzaman=0;
    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player");
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
        BeniGördümü();
        hedefeGit();

        if (ray.collider.tag=="Player")
        {
            hız = 3;
            ateşet();
        }
        else
        {
            hız = 2;
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
        if (mesafe < 0.5f)
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
    public Vector2 getyon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }
    void BeniGördümü()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 10,layermask);
        
    }
    void ateşet()
    {
        ateşzaman += Time.deltaTime;
        if (ateşzaman > Random.Range(0.2f, 1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            ateşzaman = 0;
        }
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
[CustomEditor(typeof(mace))]
[System.Serializable]
class maceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        mace script = (mace)target;
        if (GUILayout.Button("Oluştur"))
        {
            GameObject objec = new GameObject();
            objec.transform.parent = script.transform;
            objec.transform.position = script.transform.position;
            objec.name = script.transform.childCount.ToString();
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }
}
#endif



