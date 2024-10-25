using System;
using System.Windows.Forms;

//DYNAMISK RESIZING FUNKER IKKE HELT SOM DEN SKAL LMAO

namespace SimpleGUIApp
{
    public class UIComponents
    {
        private Form form;
        public TextBox screenTextBox;
        public ComboBox groupComboBox;
        public TrackBar myTrackBar;
        private Label sliderLabel;
        private GroupBox buttonGroup;
        private GroupBox sliderGroup;

        public UIComponents(Form form)
        {
            this.form = form;
        }

        public void SetupUI()
        {
            SetupTextBox();
            SetupComboBox();
            SetupButtonGroup();
            SetupSliderGroup();
            SetupApiButtons();
        }

        // Setup TextBox for displaying information
        private void SetupTextBox()
        {
            screenTextBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Text = "Screen Output",
                Font = new System.Drawing.Font("Consolas", 12),
                Location = new System.Drawing.Point(50, 300),
                Size = new System.Drawing.Size(500, 100)
            };
            form.Controls.Add(screenTextBox);
        }

        // Method to update the screen output
        public void UpdateScreen(string message)
        {
            screenTextBox.AppendText(Environment.NewLine + message);
        }

        // Setup ComboBox for displaying groups
        private void SetupComboBox()
        {
            groupComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new System.Drawing.Size(200, 30),
                Location = new System.Drawing.Point(form.ClientSize.Width - 220, 10)
            };

            // Dynamisk plassering ved resizing
            form.Resize += (sender, e) =>
            {
                groupComboBox.Location = new System.Drawing.Point(form.ClientSize.Width - 220, 10);
            };

            groupComboBox.Items.Add("Group 1");
            groupComboBox.Items.Add("Group 2");
            groupComboBox.Items.Add("Group 3");

            form.Controls.Add(groupComboBox);
        }

        // Setup Button Group (Action buttons)
        private void SetupButtonGroup()
        {
            buttonGroup = new GroupBox
            {
                Text = "Actions",
                Size = new System.Drawing.Size(350, 100),
                Location = new System.Drawing.Point(50, 50)
            };

            Button myButton = new Button
            {
                Text = "First Button",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(10, 30)
            };
            myButton.Click += (sender, e) => UpdateScreen("First Button has been pressed");
            buttonGroup.Controls.Add(myButton);

            Button secondButton = new Button
            {
                Text = "Second Button",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(150, 30)
            };
            secondButton.Click += (sender, e) => UpdateScreen("Second Button has been pressed");
            buttonGroup.Controls.Add(secondButton);

            form.Controls.Add(buttonGroup);
        }

        // Setup Slider Group
        private void SetupSliderGroup()
        {
            sliderGroup = new GroupBox
            {
                Text = "Slider Control",
                Size = new System.Drawing.Size(350, 120),
                Location = new System.Drawing.Point(50, 160)
            };

            sliderLabel = new Label
            {
                Text = "Slider value: 1",
                Font = new System.Drawing.Font("Arial", 10),
                Location = new System.Drawing.Point(10, 20),
                AutoSize = true
            };
            sliderGroup.Controls.Add(sliderLabel);

            myTrackBar = new TrackBar
            {
                Minimum = 1,
                Maximum = 12,
                Value = 1,
                TickFrequency = 1,
                Size = new System.Drawing.Size(300, 45),
                Location = new System.Drawing.Point(10, 50)
            };
            myTrackBar.Scroll += (sender, e) => 
            {
                sliderLabel.Text = $"Slider value: {myTrackBar.Value}";
               // UpdateScreen($"Slider value: {myTrackBar.Value}"); (Trenger ikke se slider-value på "Screen" for GUIen)
            };
            sliderGroup.Controls.Add(myTrackBar);

            form.Controls.Add(sliderGroup);
        }

        // Setup API buttons (GET and POST)
        private void SetupApiButtons()
        {
            Button getRequestButton = new Button
            {
                Text = "GET Request",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(50, 450)
            };
            getRequestButton.Click += async (sender, e) => 
            {
                string response = await new ApiHandler(form).SendGetRequest("https://jsonplaceholder.typicode.com/posts/1");
                UpdateScreen("GET Response: " + response);
            };
            form.Controls.Add(getRequestButton);

            Button postRequestButton = new Button
            {
                Text = "POST Request",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(200, 450)
            };
            postRequestButton.Click += async (sender, e) => 
            {
                string jsonData = "{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1 }";
                string response = await new ApiHandler(form).SendPostRequest("https://jsonplaceholder.typicode.com/posts", jsonData);
                UpdateScreen("POST Response: " + response);
            };
            form.Controls.Add(postRequestButton);
        }

        public void AdjustForResize(int formWidth)
        {
            screenTextBox.Width = formWidth - 100;
        }
    }
}
