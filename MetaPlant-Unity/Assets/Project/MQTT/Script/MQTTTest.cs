using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LitJson;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class MQTTTest : M2MqttUnityClient
    {
        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;
       

        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;
        private JsonData Data;

         
        public float seconds = 0f;
        public float minutes = 0f;
        public float totalSeconds = 0f;
        public float cellPhoneSeconds = 0f;


        public void TestPublish()
        {
            client.Publish("M2MQTT_Unity/test", System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            print("Test message published");
            //AddUiMessage("Test message published.");
        }

        public void SetBrokerAddress(string brokerAddress)
        {/*
            if (addressInputField && !updateUI)
            {
                this.brokerAddress = brokerAddress;
            }
            */
        }

        public void SetBrokerPort(string brokerPort)
        {/*
            if (portInputField && !updateUI)
            {
                int.TryParse(brokerPort, out this.brokerPort);
            }
            */
        }

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }

        /*
        public void SetUiMessage(string msg)
        {
            if (consoleInputField != null)
            {
                consoleInputField.text = msg;
                updateUI = true;
            }
        }

        public void AddUiMessage(string msg)
        {
            if (consoleInputField != null)
            {
                consoleInputField.text += msg + "\n";
                updateUI = true;
            }
        }
        */
        protected override void OnConnecting()
        {
            base.OnConnecting();
            //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            //SetUiMessage("Connected to broker on " + brokerAddress + "\n");

            if (autoTest)
            {
                TestPublish();
            }
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { "jieThesis/MetaPlant/seconds" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "jieThesis/MetaPlant/minutes" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "jieThesis/MetaPlant/totalSecond" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "jieThesis/MetaPlant/CellPhoneSeconds" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        }

        protected override void UnsubscribeTopics()
        {
            //client.Unsubscribe(new string[] { "M2MQTT_Unity/test" });
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            //AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            //AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            //AddUiMessage("CONNECTION LOST!");
        }
        /*
        private void UpdateUI()
        {
            if (client == null)
            {
                if (connectButton != null)
                {
                    connectButton.interactable = true;
                    disconnectButton.interactable = false;
                    testPublishButton.interactable = false;
                }
            }
            else
            {
                if (testPublishButton != null)
                {
                    testPublishButton.interactable = client.IsConnected;
                }
                if (disconnectButton != null)
                {
                    disconnectButton.interactable = client.IsConnected;
                }
                if (connectButton != null)
                {
                    connectButton.interactable = !client.IsConnected;
                }
            }
            if (addressInputField != null && connectButton != null)
            {
                addressInputField.interactable = connectButton.interactable;
                addressInputField.text = brokerAddress;
            }
            if (portInputField != null && connectButton != null)
            {
                portInputField.interactable = connectButton.interactable;
                portInputField.text = brokerPort.ToString();
            }
            if (encryptedToggle != null && connectButton != null)
            {
                encryptedToggle.interactable = connectButton.interactable;
                encryptedToggle.isOn = isEncrypted;
            }
            if (clearButton != null && connectButton != null)
            {
                clearButton.interactable = connectButton.interactable;
            }
            updateUI = false;
        }
        */
        protected override void Start()
        {
            Connect();
            //SetUiMessage("Ready.");
            //updateUI = true;
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            
            StoreMessage(msg);
            //Data = JsonMapper.ToObject(msg);

            if (topic == "jieThesis/MetaPlant/seconds")
            {
                //print("1: " + Single.Parse(msg));
                //print(msg.GetType());
                seconds = Single.Parse(msg);
                print("seconds: "+ seconds);
            }

            if (topic == "jieThesis/MetaPlant/minutes")
            {
                //print("1: " + Single.Parse(msg));
                //print(msg.GetType());
                minutes = Single.Parse(msg);
                print("minutes: " + minutes);
            }

            if (topic == "jieThesis/MetaPlant/totalSecond")
            {
                //print("1: " + Single.Parse(msg));
                //print(msg.GetType());
                totalSeconds = Single.Parse(msg);
                print("totalSecond: " + totalSeconds);
            }
            
            if (topic == "jieThesis/MetaPlant/CellPhoneSeconds")
            {
                //print("1: " + Single.Parse(msg));
                //print(msg.GetType());
                cellPhoneSeconds = Single.Parse(msg);
                print("CellPhoneSeconds: " + cellPhoneSeconds);
            }

        }

        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            //AddUiMessage("Received: " + msg);
            print(msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()
            //ProcessMessage(msg);
            /*
            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
            */
            /*
            if (updateUI)
            {
                UpdateUI();
            }
            */
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            if (autoTest)
            {
                autoConnect = true;
            }
        }
    }
}