using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder2.Common;

public class ColorPicker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject picker;
    public float x;
    public float y;
    public bool TriggerBool;
    public LightControl control;
    Color col = new Color(0f, .7f, 1f, 1f);
    void Start()
    {
        TriggerBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(TriggerBool == true)
        {
            picker.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            col = other.GetComponent<Renderer>().material.color;
            pb_XYZ_Color xyz = pb_XYZ_Color.FromRGB(col);
            pb_CIE_Lab_Color lab = pb_CIE_Lab_Color.FromXYZ(xyz);
            //Calculate the xy values from the XYZ values

            //float x = X / (X + Y + Z); float y = Y / (X + Y + Z);
            //https://github.com/johnciech/PhilipsHueSDK/blob/master/ApplicationDesignNotes/RGB%20to%20xy%20Color%20conversion.md
            x = xyz.x / (xyz.x + xyz.y + xyz.z);
            y = xyz.y / (xyz.x + xyz.y + xyz.z);
            //b =  true;
            StartCoroutine(control.HttpPutLight(x, y));
            //control.controlLight(x, y);
            //control.controlLight(x, y);
            //print(col);
            print(x + " " + y);
            //print(y);

            TriggerBool = false;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        TriggerBool = true;
    }


}
