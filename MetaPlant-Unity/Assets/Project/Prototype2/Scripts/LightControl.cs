using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using CI.HttpClient;
public class LightControl : MonoBehaviour
{
    [Serializable]
    public class LightJson
    {
        public int bri;
        public float[] xy;
        
    }
    LightJson lightControl = new LightJson();
    public ColorPicker picker;
    //public Transform objectValue;
    // Start is called before the first frame update
    void Start()
    {
        lightControl.xy = new float[2];
        //StartCoroutine(HttpPutLight());
        //updateLight();
        //lightControl.bri = (int)objectValue.position.y;
        //lightControl.bri = (int)Mathf.Lerp(-1f,1f,objectValue.position.y)*255;
        
    }

    // Update is called once per frame
    void Update()
    {

        //print(lightControl.bri);
        //updateLight();
        

    }

    /*
    public void controlLight(float x, float y)
    {
        StartCoroutine(HttpPutLight(x,y));
    }
    */
    public IEnumerator HttpPutLight(float x, float y)
    {

        /*
        while (true)
        {
            
            yield return new WaitForSeconds(0.1f);
            lightControl.bri = (int)map(objectValue.position.y, -1f,1f, 0f,255f);
            lightControl.xy[0] = map(objectValue.localScale.y, 0f, 1f, 0f, 1f);
            lightControl.xy[1] = map(objectValue.localRotation.eulerAngles.y, 0f, 360f, 0f, 1f);
            //print(objectValue.localRotation.eulerAngles.y);
            updateLight();
            
    }
        */

            //yield return new WaitUntil(() => b == true);
            lightControl.xy[0] = x;
            lightControl.xy[1] = y;
            lightControl.bri = 100;
            updateLight();
            //picker.b = false;
        
            yield return null;
    }

    void updateLight()
    {
        httpPostLight("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/4/state");
        httpPostLight("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/3/state");
        httpPostLight("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/2/state");
        httpPostLight("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/1/state");
        /*
        string json = JsonUtility.ToJson(lightControl);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //StartCoroutine(LightHttpPost());
        
        var client = new HttpClient();
        //light 4
        client.Put(new Uri("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/4/state"), content, HttpCompletionOption.AllResponseContent, r =>
        {   // This callback is raised when the request completes
            if (r.IsSuccessStatusCode)
            {    // Read the response content as a string if the server returned a success status code
                string responseData = r.ReadAsString();
                //print(responseData);
            }
        });
        //Light 3
        client.Put(new Uri("http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/3/state"), content, HttpCompletionOption.AllResponseContent, r =>
        {   // This callback is raised when the request completes
            if (r.IsSuccessStatusCode)
            {    // Read the response content as a string if the server returned a success status code
                string responseData = r.ReadAsString();
                //print(responseData);
            }
        });
        */
    }

    public void httpPostLight(string url)
    {
        var client = new HttpClient();
        string json = JsonUtility.ToJson(lightControl);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        client.Put(new Uri(url), content, HttpCompletionOption.AllResponseContent, r =>
        {   // This callback is raised when the request completes
            if (r.IsSuccessStatusCode)
            {    // Read the response content as a string if the server returned a success status code
                string responseData = r.ReadAsString();
                //print(responseData);
            }
        });
    }
    /*
    public static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }
    */

}

    

   




/*
public WWW POST()
    {
        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

        www = new WWW(POSTAddUserURL, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;
    }
    IEnumerator WaitForRequest(WWW data)
    {
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            MainUI.ShowDebug("There was an error sending request: " + data.error);
        }
        else
        {
            MainUI.ShowDebug("WWW Request: " + data.text);
        }
    }  
*/