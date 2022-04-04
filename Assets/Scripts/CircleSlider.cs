using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour {

    [SerializeField] Transform handle;

    //tbh you don't have to use a visible fill if the knob itself is clear enough
    [SerializeField] Image fill;

    [SerializeField] Text text; //for debugging, not needed 

    //[SerializeField] Text valTxt;

    public float value; //Use this as your output
    Vector3 mousePos;

    public void Start()
    {
        //Ideally the position of the handle would correspond to the value
        //but idk how to do math and I won't blame you if you don't either
        value = PlayerPrefs.GetFloat("SongVolume");

        //circle math here to make the handle move in its place idk
    }

    public void OnHandleDrag() {
       
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle = (angle <= 0) ? (360 + angle) : angle;

        if (angle <= 225 || angle >= 315){
            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            handle.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f);

            value = Mathf.Round((fill.fillAmount * 100) / 0.75f);
            //PlayerPrefs.SetFloat("SongVolume", value);
            text.text = value.ToString();
            //valTxt.text = value.ToString();
        }

    }


    private void Update()
    {
        
    }
}
