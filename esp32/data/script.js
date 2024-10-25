document.getElementById('wifiForm').addEventListener('submit', function(event) {
    event.preventDefault();
  
    const ssid = document.getElementById('ssid').value;
    const password = document.getElementById('password').value;
  
    fetch('/configureWiFi', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ ssid: ssid, password: password })
    })
    .then(response => response.text())
    .then(data => {
      console.log("Respons fra ESP32:", data);
      document.getElementById('responseMessage').innerText = data;
    })
    .catch(error => {
      console.error('Error:', error);
      document.getElementById('responseMessage').innerText = "En feil oppstod: " + error;
    });
  });
  