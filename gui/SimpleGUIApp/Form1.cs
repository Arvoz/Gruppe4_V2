using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleGUIApp
{
    public partial class Form1 : Form
    {
        private Button myButton;
        private Button secondButton;
        private TextBox screenTextBox;
        private TrackBar myTrackBar;
        private Label sliderLabel;
        private Button getRequestButton;
        private Button postRequestButton;
        private GroupBox buttonGroup;
        private GroupBox sliderGroup;

        public Form1()
        {
            // Initialize the form and controls
            this.Text = "API Interaction Example";
            this.Size = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(500, 500);

            // Setup UI
            SetupButtonGroup();
            SetupSliderGroup();
            SetupScreen();
            SetupApiButtons();

            // Handle window resizing
            this.Resize += new EventHandler(Form1_Resize);
        }

        // Setup the API buttons (GET and POST)
        private void SetupApiButtons()
        {
            getRequestButton = new Button();
            getRequestButton.Text = "GET Request";
            getRequestButton.Size = new System.Drawing.Size(100, 50);
            getRequestButton.Location = new System.Drawing.Point(50, 450);
            getRequestButton.Click += async (sender, e) => await HandleGetRequest();  // Correct async handling
            Controls.Add(getRequestButton);

            postRequestButton = new Button();
            postRequestButton.Text = "POST Request";
            postRequestButton.Size = new System.Drawing.Size(100, 50);
            postRequestButton.Location = new System.Drawing.Point(200, 450);
            postRequestButton.Click += async (sender, e) => await HandlePostRequest();  // Correct async handling
            Controls.Add(postRequestButton);
        }

        // Event handler for GET request
        private async Task HandleGetRequest()
        {
            string url = "https://jsonplaceholder.typicode.com/posts/1";  // Example API URL
            string response = await SendGetRequest(url);
            UpdateScreen("GET Response: " + response);
        }

        // Event handler for POST request
        private async Task HandlePostRequest()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";  // Example API URL
            string jsonData = "{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1 }";  // Example data
            string response = await SendPostRequest(url, jsonData);
            UpdateScreen("POST Response: " + response);
        }

        // GET request method
        private async Task<string> SendGetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();  // Throw if not success
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    return $"Request error: {e.Message}";
                }
            }
        }

        // POST request method
        private async Task<string> SendPostRequest(string url, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();  // Throw if not success
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    return $"Request error: {e.Message}";
                }
            }
        }

        // Setup the button group (Actions)
        private void SetupButtonGroup()
        {
            buttonGroup = new GroupBox();
            buttonGroup.Text = "Actions";
            buttonGroup.Size = new System.Drawing.Size(350, 100);
            buttonGroup.Location = new System.Drawing.Point(50, 50);

            myButton = new Button();
            myButton.Text = "First Button";
            myButton.Size = new System.Drawing.Size(100, 50);
            myButton.Location = new System.Drawing.Point(10, 30);
            myButton.Click += new EventHandler(MyButton_Click);
            buttonGroup.Controls.Add(myButton);

            secondButton = new Button();
            secondButton.Text = "Second Button";
            secondButton.Size = new System.Drawing.Size(100, 50);
            secondButton.Location = new System.Drawing.Point(150, 30);
            secondButton.Click += new EventHandler(SecondButton_Click);
            buttonGroup.Controls.Add(secondButton);

            Controls.Add(buttonGroup);
        }

        // Setup the slider group (TrackBar)
        private void SetupSliderGroup()
        {
            sliderGroup = new GroupBox();
            sliderGroup.Text = "Slider Control";
            sliderGroup.Size = new System.Drawing.Size(350, 120);
            sliderGroup.Location = new System.Drawing.Point(50, 160);

            sliderLabel = new Label();
            sliderLabel.Text = "Slider value: 1";
            sliderLabel.Font = new System.Drawing.Font("Arial", 10);
            sliderLabel.Location = new System.Drawing.Point(10, 20);
            sliderLabel.AutoSize = true;
            sliderGroup.Controls.Add(sliderLabel);

            myTrackBar = new TrackBar();
            myTrackBar.Minimum = 1;
            myTrackBar.Maximum = 12;
            myTrackBar.Value = 1;
            myTrackBar.TickFrequency = 1;
            myTrackBar.Size = new System.Drawing.Size(300, 45);
            myTrackBar.Location = new System.Drawing.Point(10, 50);
            myTrackBar.Scroll += new EventHandler(TrackBar_Scroll);
            sliderGroup.Controls.Add(myTrackBar);

            Controls.Add(sliderGroup);
        }

        // Setup the TextBox (screen)
        private void SetupScreen()
        {
            screenTextBox = new TextBox();
            screenTextBox.Multiline = true;
            screenTextBox.ReadOnly = true;
            screenTextBox.Text = "Screen Output";
            screenTextBox.Font = new System.Drawing.Font("Consolas", 12);
            screenTextBox.Location = new System.Drawing.Point(50, 300);
            screenTextBox.Size = new System.Drawing.Size(500, 100);
            Controls.Add(screenTextBox);
        }

        // TrackBar Scroll Event Handler
        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            sliderLabel.Text = "Slider value: " + myTrackBar.Value.ToString();
            UpdateSliderValueOnScreen();
        }

        // Update slider value on the screen (TextBox)
        private void UpdateSliderValueOnScreen()
        {
            string[] lines = screenTextBox.Lines;
            if (lines.Length > 0 && lines[lines.Length - 1].StartsWith("Slider value:"))
            {
                screenTextBox.Lines = lines[0..^1];  // Remove last line
            }
            UpdateScreen("Slider value: " + myTrackBar.Value.ToString());
        }

        // Helper method to append text to the screen
        private void UpdateScreen(string message)
        {
            screenTextBox.AppendText(Environment.NewLine + message);
        }

        // Event handler for dynamically resizing the controls
        private void Form1_Resize(object sender, EventArgs e)
        {
            screenTextBox.Width = this.ClientSize.Width - 100;
        }

        // Button Click Event Handlers
        private void MyButton_Click(object sender, EventArgs e)
        {
            UpdateScreen("Button has been pressed");
        }

        private void SecondButton_Click(object sender, EventArgs e)
        {
            UpdateScreen("Second button has been pressed");
        }
    }
}
