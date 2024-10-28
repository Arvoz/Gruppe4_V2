using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

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
        private Button getRequestButton;
        private Button postRequestButton;

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

            // Koble AdjustForResize til formens Resize-event
            form.Resize += (sender, e) => AdjustForResize();
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
                Size = new System.Drawing.Size(form.ClientSize.Width - 100, 100)
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

            LoadGroupsFromJson(); // Laster inn grupper fra standard JSON-fil

            form.Controls.Add(groupComboBox);
        }

        // Load groups from a JSON file into the ComboBox
        // Parameter "filePath" allows for a custom file path, defaulting to "groups.json"
        public void LoadGroupsFromJson(string filePath = "groups.json")
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Filen ble ikke funnet: {filePath}");
                    return;
                }

                string jsonData = File.ReadAllText(filePath);
                List<Group> groups = JsonSerializer.Deserialize<List<Group>>(jsonData);

                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        groupComboBox.Items.Add(group.GroupName); // Legger til GroupName i ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Feil ved lasting av grupper fra JSON: {ex.Message}");
            }
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
            };
            sliderGroup.Controls.Add(myTrackBar);

            form.Controls.Add(sliderGroup);
        }

        // Setup API buttons (GET and POST)
        private void SetupApiButtons()
        {
            getRequestButton = new Button
            {
                Text = "GET Request",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(50, 450)
            };
            getRequestButton.Click += async (sender, e) => 
            {
                string response = await new ApiHandler(form).SendGetRequest("http://localhost:5013/Group/GetData");
                UpdateScreen("GET Response: " + response);
                string jsonData = response;
                File.WriteAllText("./groups.json", jsonData);
            };
            form.Controls.Add(getRequestButton);

            postRequestButton = new Button
            {
                Text = "POST Request",
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(200, 450)
            };
            postRequestButton.Click += async (sender, e) => 
            {
                // string jsonData = "{ \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1 }";
                string response = await new ApiHandler(form).SendPostRequest("http://localhost:5013/group/PostFromGui", "Hello from GUI");
                UpdateScreen("POST Response: " + response);
            };
            form.Controls.Add(postRequestButton);
        }

        // Dynamisk Resizing
        private void AdjustForResize()
        {
            int formWidth = form.ClientSize.Width;
            int formHeight = form.ClientSize.Height;

            screenTextBox.Width = formWidth - 100;
            groupComboBox.Location = new System.Drawing.Point(formWidth - groupComboBox.Width - 20, 10);

            buttonGroup.Width = formWidth - 100;
            sliderGroup.Width = formWidth - 100;

            getRequestButton.Location = new System.Drawing.Point(50, formHeight - 120);
            postRequestButton.Location = new System.Drawing.Point(200, formHeight - 120);
        }
    }

    // Modellklasser for JSON-dataene
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Device> Devices { get; set; }
    }

    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}
