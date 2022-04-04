using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeRules : MonoBehaviour
{
    public GameObject[] red;
    public GameObject[] blue;
    public GameObject[] green;

    public Image redIcon;
    public Image blueIcon;
    public Image greenIcon;
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

    public void off()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().gameObject.SetActive(false);
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().gameObject.SetActive(false);
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().gameObject.SetActive(false);
        }
    }

    public void on()
    {
        foreach (GameObject action in red)
        {
            action.GetComponent<Button>().gameObject.SetActive(true);
        }
        foreach (GameObject action in blue)
        {
            action.GetComponent<Button>().gameObject.SetActive(true);
        }
        foreach (GameObject action in green)
        {
            action.GetComponent<Button>().gameObject.SetActive(true);
        }
        
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
        redIcon.color = Color.black;
        blueIcon.color = Color.white;
        greenIcon.color = Color.black;
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
        redIcon.color = Color.white;
        blueIcon.color = Color.black;
        greenIcon.color = Color.white;
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
        redIcon.color = Color.black;
        blueIcon.color = Color.white;
        greenIcon.color = Color.black;
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
