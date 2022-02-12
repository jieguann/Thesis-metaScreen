let timeSecents = 0;


AFRAME.registerComponent('bulb-light', {
    schema: {
      color: {default: 'yellow'},
      intensity: {default: 0.5}
    },

    init: function () {
      var data = this.data;
      var el = this.el;  // <a-box>
      //var defaultColor = el.getAttribute('material').color;
      //var defaultColor = 'green';
      var defaultLight = el.getAttribute('light').intensity;
      //var defaultColor = white
      setInterval(function(){
        console.log(timeSecents);
        if(timeSecents>0){
        //defaultColor = 'green';
        defaultLight = 1;
        
        //el.setAttribute('color', 'green');
      }
      else{
        defaultLight = 0.5;
        //defaultColor = 'red';
        //el.setAttribute('color', 'red');
      }
      el.setAttribute('light', {intensity: defaultLight});
    } , 1000)
      
     
      
      el.addEventListener('mouseenter', function () {
        el.setAttribute('light', {color:'yellow'});
        //el.setAttribute('light', {intensity: 1});
        lightControl(240,0.39, 0.47);
        client.publish('jieThesis/MetaScreen/PhilipHue', JSON.stringify({"bri":240, 'x':0.39, 'y':0.47}), { qos: 0, retain: false });
        //client.publish('jieThesis/Meta/seconds', seconds.toString(), { qos: 0, retain: false });
        
 });
      
      
      el.addEventListener('mouseleave', function () {
        //el.setAttribute('color', defaultColor);
        //el.setAttribute('light', {intensity: defaultLight});
        el.setAttribute('light', {color:'white'});
        lightControl(200, 0.31, 0.32);
        client.publish('jieThesis/MetaScreen/PhilipHue',JSON.stringify({"bri":200, 'x':0.31, 'y':0.32}), { qos: 0, retain: false });
        
      });

      // el.addEventListener('mousedown', function () {
      //   //el.setAttribute('color', defaultColor);
      //   el.setAttribute('light', {intensity: 1});
      // });
      
    }
  })


  const lightControl = (bri,x,y) =>{
    const requestOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      //body: JSON.stringify({"bri": bri, "xy": [0.31, 0.32]})
      body: JSON.stringify({"bri": bri, "xy": [x, y]})
  };
  fetch('http://192.168.2.49/api/zx9NNIegikmyEgZZOQmR-FTTzTomumRr4nzjyoWc/lights/4/state', requestOptions)
      .then(async response => {
          const isJson = response.headers.get('content-type')?.includes('application/json');
          const data = isJson && await response.json();
  
          // check for error response
          if (!response.ok) {
              // get error message from body or default to response status
              const error = (data && data.message) || response.status;
              return Promise.reject(error);
          }
  
          //element.innerHTML = data.updatedAt;
      })
      .catch(error => {
          //element.parentElement.innerHTML = `Error: ${error}`;
          console.error('There was an error!', error);
      });
  }


 //Code for MQTT
const clientId = 'mqttjs_' + Math.random().toString(16).substr(2, 8)
const host = 'wss://test.mosquitto.org:8081/mqtt'
//const host = 'ws://134.122.33.147:9001/mqtt'

console.log('Connecting mqtt client')
const client = mqtt.connect(host)

client.on('connect', function () {
    console.log('Connected')
    client.subscribe('jieThesis/MetaScreen/BulbTouched', function (err) {
      if (err) {
        //client.publish('jieThesis/MetaPlant/seconds', 'Hello mqtt')
      }
    })
  })



client.on('reconnect', () => {
console.log('Reconnecting...')
})


client.on('message', (topic, message, packet) => {
   //console.log('Received Message: ' + message.toString() + '\nOn topic: ' + topic)
   timeSecents = parseInt(message.toString());
   
})
