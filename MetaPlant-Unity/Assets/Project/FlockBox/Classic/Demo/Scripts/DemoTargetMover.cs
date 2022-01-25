using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTargetMover : MonoBehaviour
{
    public Transform headPosition;
    public Transform treePosition;
    //public float moveTime = 5f;

    //private float time;
    //private int positionIndex;
    [SerializeField] private M2MqttUnity.Examples.MQTTTest MQTT;
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MQTT.cellPhoneSeconds > 2f)
        {
            transform.position = treePosition.position;
        }
        else
        {
            
            transform.position = headPosition.position;
        }
        //print(MQTT.cellPhoneSeconds);
    }
    /*
    void MoveToPosition(int posIndex)
    {
        if (posIndex >= positions.Length) posIndex = 0;
        if (posIndex < 0) posIndex = positions.Length - 1;
        positionIndex = posIndex;
        if (positions[posIndex] != null)
        {
            transform.position = positions[posIndex].position;
        }
    }
    */
}
