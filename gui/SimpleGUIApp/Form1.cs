using System;
using System.Windows.Forms;

namespace SimpleGUIApp
{
    public partial class Form1 : Form
    {
        private UIComponents uiComponents;
        private ApiHandler apiHandler;

        public Form1()
        {
            // Initialize the form properties
            this.Text = "API Interaction Example";
            this.Size = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(500, 500);

            // Instantiate helper classes
            uiComponents = new UIComponents(this);
            apiHandler = new ApiHandler(this);

            // Setup UI and attach events
            uiComponents.SetupUI();
            this.Resize += new EventHandler(Form1_Resize);
        }

        // Event handler for resizing the form
        private void Form1_Resize(object sender, EventArgs e)
        {
            uiComponents.AdjustForResize(this.ClientSize.Width);
        }
    }
}
