using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mydyk
{
    internal class Overlay : Form
    {
        private Label label1; // New label for displaying lag status
        private Point offset; // Offset between mouse position and form position when dragging
        private bool isDragging = false; // Flag to indicate if the form is being dragged
        private ComboBox profileComboBox;
        private Form1 mainForm;

        public Overlay(Form1 form)
        {
            mainForm = form;
            InitializeOverlay();
        }

        private void InitializeOverlay()
        {
            // Set overlay properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.TopMost = true;
            this.Enabled = true; // Ensure the form is enabled
            // Subscribe to mouse events for dragging
            this.MouseDown += Overlay_MouseDown;
            this.MouseMove += Overlay_MouseMove;
            this.MouseUp += Overlay_MouseUp;

            // Set overlay size and position
            int overlayWidth = 200; // Adjust size as needed
            int overlayHeight = 80; // Adjust size as needed
            this.Size = new Size(overlayWidth, overlayHeight);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the overlay

            // Add ComboBox for selecting profiles
            profileComboBox = new ComboBox();
            profileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profileComboBox.Location = new System.Drawing.Point(10, 10); // Adjust position as needed
            profileComboBox.Size = new System.Drawing.Size(180, 21); // Adjust size as needed
            profileComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            this.Controls.Add(profileComboBox);

            // Load profiles into the ComboBox
            LoadProfiles();

            // Initialize the label for displaying lag status
            label1 = new Label();
            label1.AutoSize = false; // Disable AutoSize to manually set the size
            label1.Size = new Size(160, 60); // Manually set the label's size
            label1.Font = new Font("Arial", 16); // Set font size as needed
            label1.ForeColor = Color.White; // Set text color as needed
            label1.Text = "Not Lagging"; // Set the initial text to "Not Lagging"
            label1.TextAlign = ContentAlignment.MiddleCenter; // Ensure the text is centered within the label
            label1.SendToBack();

            // Calculate the location to center the label within the overlay
            int labelX = (overlayWidth - label1.Width) / 2; // Center horizontally
            int labelY = (overlayHeight - label1.Height) / 1; // Center vertically

            label1.Location = new Point(labelX, labelY); // Update the label's location

            // Add the label to the form or overlay
            this.Controls.Add(label1);

            // Add mouse event handlers to the label
            label1.MouseDown += Overlay_MouseDown;
            label1.MouseMove += Overlay_MouseMove;
            label1.MouseUp += Overlay_MouseUp;

            // Hide the overlay initially
            this.Visible = false;
        }

        private void Overlay_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging when the left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                offset = new Point(e.X, e.Y);
                isDragging = true;
            }
        }

        private void Overlay_MouseMove(object sender, MouseEventArgs e)
        {
            // If dragging, update form position based on mouse movement
            if (isDragging)
            {
                Point newPosition = this.PointToScreen(new Point(e.X, e.Y));
                newPosition.Offset(-offset.X, -offset.Y);
                this.Location = newPosition;
            }
        }

        private void Overlay_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging when the left mouse button is released
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        public void LoadProfiles()
        {
            mainForm.LoadProfilesIntoComboBox(profileComboBox);
        }

        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProfile = profileComboBox.SelectedItem?.ToString();
            mainForm.HandleProfileSelectionChange(selectedProfile);
        }

        public void UpdateLagStatus(bool isLagging)
        {
            if (isLagging)
            {
                label1.Text = "Lagging";
                label1.ForeColor = Color.Red;
            }
            else
            {
                label1.Text = "Not Lagging";
                label1.ForeColor = Color.Green;
            }
        }
    }
}