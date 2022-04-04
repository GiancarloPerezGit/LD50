using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesUI : MonoBehaviour
{
    public GameObject rulesMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSelect()
    {
        rulesMax.SetActive(!rulesMax.activeSelf);
    }
}
