using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif
using UnityEditor;
public class TestereKontrol : MonoBehaviour
{
    public int resim;
    bool aradakimesafeyibirkereal = true;
    GameObject []hedef;
    Vector3 aradakimesafe;
    int aradakimesafesayaç=0;
    bool ilermigerimi = true;
    void Start()
    {
        hedef = new GameObject[transform.childCount];
        for (int i = 0; i <hedef.Length ; i++)
        {
            hedef[i] = transform.GetChild(0).gameObject;
            hedef[i].transform.SetParent(transform.parent);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 2);
        hedefeGit();
    }
    void hedefeGit()
    {
        if (aradakimesafeyibirkereal)
        {
            aradakimesafe = (hedef[aradakimesafesayaç].transform.position - transform.position).normalized;
            aradakimesafeyibirkereal = false;
            
        }
        float mesafe = Vector3.Distance(transform.position, hedef[aradakimesafesayaç].transform.position);
        transform.position += aradakimesafe * Time.deltaTime * 20;
        if (mesafe<0.5f)
        {
            aradakimesafeyibirkereal = true;
            if (aradakimesafesayaç==hedef.Length-1)
            {
                ilermigerimi = false;
            }
            else if (aradakimesafesayaç==0)
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
    void Hedef()//testerenin'hedefleri
    {
        
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 0.5f);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif

}
#if UNITY_EDITOR
[CustomEditor(typeof(TestereKontrol))]
[System.Serializable]
class SawEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TestereKontrol script = (TestereKontrol)target;
        if (GUILayout.Button("Oluştur"))
        {
            GameObject objec = new GameObject();
            objec.transform.parent = script.transform;
            objec.transform.position = script.transform.position;
            objec.name = script.transform.childCount.ToString();
        }
    }
}
#endif



