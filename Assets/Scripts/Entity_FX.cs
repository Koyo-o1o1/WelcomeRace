using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class Entity_FX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float fllushDduration;
    private Material originalMat;



    private GameObject myHealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;

        myHealthBar = GetComponentInChildren<HealthBar_UI>().gameObject;
    }


    public IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(fllushDduration);

        sr.material=originalMat;
    }

    //reset
    public void ResetMaterial()
    {
        if (sr != null)
        {
            sr.material = originalMat;
            sr.color = Color.white; // ← 色もリセット
        }
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelRedBlink()
    {
        sr.color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear;
        }
        else
        {
            myHealthBar.SetActive(true);
            sr.color = Color.white;
        }
    }
}
