using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeRules : MonoBehaviour
{
    public GameObject[] red;
    public GameObject[] blue;
    public GameObject[] green;
    // Start is called before the first frame update
    void Start()
    {
        red = GameObject.FindGameObjectsWithTag("Red");
        blue = GameObject.FindGameObjectsWithTag("Blue");
        green = GameObject.FindGameObjectsWithTag("Green");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void redSelected()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().interactable = false;
        }
    }
    public void blueSelected()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().interactable = true;
        }
    }
    public void greenSelected()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().interactable = false;
        }
    }

    public void graySelected()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().interactable = true;
        }
    }    
}
