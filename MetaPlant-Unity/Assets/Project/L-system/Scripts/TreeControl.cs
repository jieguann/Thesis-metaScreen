using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
public class TreeControl : MonoBehaviour
{
    [SerializeField] private LSystemsGenerator TreeSpawner;
    [SerializeField] private M2MqttUnity.Examples.MQTTTest MQTT;
    public int treeTitle;

    // Start is called before the first frame update
    private void Awake()
    {
        TreeSpawner.title = 4;
    }
    void Start()
    {
        StartCoroutine(setTreeTitle());

    }
    // Update is called once per frame
    void Update()
    {
        
        if ((int)MQTT.minutes == 0)
        {
            TreeSpawner.iterations = 1;
        }
        else
        {
            TreeSpawner.iterations = (int)MQTT.minutes;
        }
        
        print((int)MQTT.minutes);
    }


    IEnumerator setTreeTitle()
    {
        

       
        yield return new WaitForSeconds(1);

        TreeSpawner.title = treeTitle;
    }
}

