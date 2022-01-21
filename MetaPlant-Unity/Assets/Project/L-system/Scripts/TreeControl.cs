using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
public class TreeControl : MonoBehaviour
{
    [SerializeField] private LSystemsGenerator TreeSpawner;
    [SerializeField] private M2MqttUnity.Examples.MQTTTest MQTT;
    
    
    // Start is called before the first frame update
    void Start()
    {
        TreeSpawner.title = 6;
        
    }

    // Update is called once per frame
    void Update()
    {   if((int)MQTT.minutes == 0)
        {
            TreeSpawner.iterations = 1;
        }
        else
        {
            TreeSpawner.iterations = (int)MQTT.minutes;
        }
        
        print((int)MQTT.minutes);
    }
}
