using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Reflection;
using Mydyk.Properties;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Mydyk.Form1;

namespace Mydyk
{

    public partial class Form1 : Form
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private int horizontalParameter = 0;
        private int verticalParameter = 0;
        private string gameProcess = "";
        private const int MIN_VALUE = -10;
        private const int MAX_VALUE = 10;

        private const int WM_HOTKEY = 0x0312;
        private const int MOD_NONE = 0x0000; // no modifier
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;
        //... other modifiers
        private bool recoilEnabled = false;
        private bool isRecoilActivated = false;
        private Timer recoilTimer;
        private Timer timer1;
        private bool isUDPFloodActivated = false;
        private bool udpFloodEnabled = false;
        private int RecoilHotkeyId = 1;
        private int UDPFloodHotkeyId = 2;
        UdpClient udpClient = new UdpClient(11000);
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private const string ProfilesDirectory = @"Profiles\";

        private Overlay overlay;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public class LagStatusChangedEventArgs : EventArgs
        {
            public bool IsLagging { get; set; }

            public LagStatusChangedEventArgs(bool isLagging)
            {
                IsLagging = isLagging;
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeOverlay();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            MakeLabelsTransparent();
            this.Invalidate();
            textBoxHorizontalParameter.TextChanged += textBoxHorizontalParameter_TextChanged;
            textBoxVerticalParameter.TextChanged += textBoxVerticalParameter_TextChanged;
            textBoxGameProcess.TextChanged += TextBoxGameProcess_TextChanged;
            //textBoxOnOffKey.KeyDown += textBoxOnOffKey_KeyDown;
            //textBoxUDPKey.KeyDown += textBoxUDPKey_KeyDown;
            button3.Click += button3_Click;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            overlay = new Overlay(this);

            LoadProfiles(); // Load profiles into comboBox2

            timer1 = new Timer(); // Initialize timer1
            timer1.Tick += new EventHandler(timer1_Tick); // Subscribe to the Tick event
            timer1.Interval = 1000; // Set the interval (in milliseconds), adjust as needed

            trackBar1.Scroll += trackBar1_Scroll;
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;

            recoilTimer = new Timer();
            recoilTimer.Interval = 25; // in milliseconds
            recoilTimer.Tick += (sender, args) => ApplyRecoil();
            recoilTimer.Start();
            // Register hotkeys for recoil and UDP flood
            RegisterHotKey(this.Handle, RecoilHotkeyId, MOD_NONE, (int)Keys.NumPad0);
            RegisterHotKey(this.Handle, UDPFloodHotkeyId, MOD_NONE, (int)Keys.NumPad3);

            // Set the text of the text boxes for hotkeys
            textBoxOnOffKey.Text = "Num0";
            textBoxUDPKey.Text = "Num3";
        }

        //RECOIL
        private void textBoxSetProfileName_TextChanged(object sender, EventArgs e)
        {
            // Get the text entered by the user in the textBoxSetProfileName textbox
            string profileName = textBoxSetProfileName.Text;

            // Add null check and trim to prevent a crash or unexpected behavior due to leading/trailing whitespaces
            if (!string.IsNullOrWhiteSpace(profileName))
            {
                // Add your logic here for handling the Set Profile Name event
                // For example, you can validate the input or save the profile name to a variable or database
                // ...
            }
        }
        private void textBoxHorizontalParameter_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxHorizontalParameter.Text, out int horizontalParameterValue))
            {
                horizontalParameter = horizontalParameterValue;
            }
            else
            {
                // Optional: Handle invalid input here, such as clearing the text box or displaying an error message
                // textBoxHorizontalParameter.Text = "0"; // Example of resetting to a default value
            }
        }

        private void textBoxVerticalParameter_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxVerticalParameter.Text, out int verticalParameterValue))
            {
                verticalParameter = verticalParameterValue;
            }
            else
            {
                // Optional: Handle invalid input here
                // textBoxVerticalParameter.Text = "0"; // Example of resetting to a default value
            }
        }

        private void TextBoxGameProcess_TextChanged(object sender, EventArgs e)
        {
            gameProcess = textBoxGameProcess.Text.Trim();
            Process game = IsGameActive(gameProcess);

            if (game != null && game.Responding && IsGameWindowForeground(game))
            // Capture the process name entered in the TextBox
            {
                MessageBox.Show("Recoil conditions are met!");
            }
        }

        private Process IsGameActive(string processName)
        {
            // Trim ".exe" from the end if present
            if (processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                processName = processName.Substring(0, processName.Length - 4);
            }

            Process[] processes = Process.GetProcessesByName(processName);
            return processes.FirstOrDefault();
        }

        private bool IsGameWindowForeground(Process process)
        {
            if (process == null) return false;

            IntPtr foregroundWindowHandle = GetForegroundWindow();
            return process.MainWindowHandle == foregroundWindowHandle;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                int hotkeyId = m.WParam.ToInt32();

                if (hotkeyId == RecoilHotkeyId)
                {
                    // Toggle the recoil functionality
                    ToggleRecoil();
                }
                else if (hotkeyId == UDPFloodHotkeyId) // UDP Flood toggle hotkey
                {
                    // Toggle the UDP flood activation status
                    isUDPFloodActivated = !isUDPFloodActivated;
                    udpFloodEnabled = !udpFloodEnabled;

                    // Update UI and apply UDP flood as needed
                    ChangeLabelText(udpFloodEnabled);
                    overlay.UpdateLagStatus(udpFloodEnabled);

                    if (udpFloodEnabled)
                    {
                        ApplyUDPFlood();
                    }
                    else
                    {
                        StopUDPFlood();
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        // This method toggles the recoil activation status and updates the UI.
        private void ToggleRecoil()
        {
            // Toggle the recoil activation status
            isRecoilActivated = !isRecoilActivated;
            recoilEnabled = !recoilEnabled;

            // Update the label's text and color based on the new status
            labelRecoilStatus.Text = recoilEnabled ? "Enabled" : "Disabled";
            labelRecoilStatus.ForeColor = recoilEnabled ? Color.Green : Color.Red; // Use ForeColor in WinForms

            // If necessary, here you can also call the method that starts or stops applying recoil
        }

        private void ApplyRecoil()
        {
            Process gameProcessInstance = IsGameActive(gameProcess);
            if (recoilEnabled && gameProcessInstance != null && IsGameWindowForeground(gameProcessInstance))
            {
                // Check if the left mouse button is pressed
                if (GetAsyncKeyState(Keys.LButton) != 0)
                {
                    // Apply recoil by calling mouse_event
                    if (isRecoilActivated)
                    {
                        mouse_event(MOUSEEVENTF_MOVE, horizontalParameter, verticalParameter, 0, 0);
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                                                                      Color.FromArgb(255, 139, 150, 206), // Start color
                                                                      Color.FromArgb(255, 136, 53, 185), // End color
                                                                      LinearGradientMode.Vertical)) // Gradient direction
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // Forces the form to redraw
        }

        private void MakeLabelsTransparent()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.BackColor = Color.Transparent;
                }
            }
        }

        public Bitmap ChangeImageOpacity(Image img, float opacityValue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Creating a blank bitmap with the correct dimensions

            // Create graphics from the bitmap
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                // Create a color matrix and set its opacity
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacityValue; // Opacity

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Draw the image onto the bitmap with the new opacity
                gfx.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attributes);
            }

            return bmp; // Return the bitmap with adjusted opacity
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Load the image, change its opacity, and set it to the PictureBox
            string imagePath = Path.Combine(Application.StartupPath, "manhunt.png");
            if (File.Exists(imagePath))
            {
                using (Image originalImage = Image.FromFile(imagePath))
                {
                    Bitmap imageWithNewOpacity = ChangeImageOpacity(originalImage, 0.013f); // 50% opacity
                    pictureBox2.Image = imageWithNewOpacity;
                }
            }
            else
            {
                MessageBox.Show("Image file not found: " + imagePath);
            }

            // Register the hotkey here to ensure the form handle is created
            RegisterHotKey(this.Handle, RecoilHotkeyId, MOD_CONTROL, Keys.Y.GetHashCode()); // for Ctrl+Y, as an example

            // Setting the icon
            try
            {
                string iconPath = Path.Combine(Application.StartupPath, "Mydyk.ico");
                if (File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
                else
                {
                    MessageBox.Show("Icon file not found: " + iconPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set icon: " + ex.Message);
            }
        }

        //UDP Flood
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString(); // Assuming 'label4' is the label you want to use for displaying the value
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            Process.Start("https://www.paypal.me/holyr00d");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (udpFloodEnabled)
            {
                UdpClient udpClient = new UdpClient(0); string s = "MYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYKMYDYK";
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                for (int index = 0; index < this.trackBar1.Value * 180; ++index)
                {
                    try
                    {
                        udpClient.Connect("27.27.27.27", 0);
                        udpClient.Send(bytes, s.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void ChangeLabelText(bool isLagging)
        {
            if (isLagging)
            {
                this.label1.Text = "Lagging";
                this.label1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.label1.Text = "Not Lagging";
                this.label1.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void ApplyUDPFlood()
        {
            // Start the UDP flood by enabling the timer
            udpFloodEnabled = true;
            timer1.Enabled = true;
            udpClient = new UdpClient(); // Initialize the UdpClient instance

            ChangeLabelText(true);
        }

        private void StopUDPFlood()
        {
            // Stop the UDP flood by disabling the timer
            udpFloodEnabled = false;
            timer1.Enabled = false;

            // Close the UdpClient instance to stop the UDP flood
            if (udpClient != null)
                udpClient.Close();

            ChangeLabelText(false);
        }

        //PROFILES
        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure the directory exists or create it if it doesn't
            if (!Directory.Exists(ProfilesDirectory))
            {
                Directory.CreateDirectory(ProfilesDirectory);
            }

            // Gather settings and functions into a data structure
            ProfileData profileData = new ProfileData
            {
                ProfileName = textBoxSetProfileName.Text.Trim(),
                HorizontalParameter = horizontalParameter,
                VerticalParameter = verticalParameter,
                GameProcess = gameProcess,
                UDPFloodKey = textBoxUDPKey.Text.Trim(),
                RecoilKey = textBoxOnOffKey.Text.Trim(),
                TrackBarValue = trackBar1.Value,
            };

            // Serialize profileData to JSON
            string json = JsonConvert.SerializeObject(profileData);

            // Get the profile name from the textBoxSetProfileName
            string profileName = textBoxSetProfileName.Text.Trim();
            if (string.IsNullOrEmpty(profileName))
            {
                MessageBox.Show("Please enter a profile name.");
                return;
            }

            // Save JSON string to a file in the profiles directory
            string filePath = Path.Combine(Application.StartupPath, ProfilesDirectory, profileName + ".json");
            File.WriteAllText(filePath, json);

            // Update ComboBox2 with the new profile name
            comboBox2.Items.Add(profileName);

            // After saving the new profile, update the overlay's ComboBox
            overlay.LoadProfiles(); // Call the LoadProfiles method of the overlay
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve the selected profile name from comboBox2
            string selectedProfile = comboBox2.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedProfile))
            {
                // Construct the file path for the selected profile
                string profileFilePath = Path.Combine(ProfilesDirectory, selectedProfile + ".json");

                try
                {
                    // Check if the profile file exists
                    if (File.Exists(profileFilePath))
                    {
                        // Delete the profile file
                        File.Delete(profileFilePath);

                        // Remove the profile from comboBox2
                        comboBox2.Items.Remove(selectedProfile);

                        // Optionally, clear the selection in comboBox2
                        comboBox2.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Selected profile does not exist.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the profile: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a profile to delete.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProfile = comboBox2.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedProfile))
            {
                string profileFilePath = Path.Combine(ProfilesDirectory, selectedProfile + ".json");

                try
                {
                    string profileJson = File.ReadAllText(profileFilePath);
                    ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(profileJson);

                    // Apply profile settings to UI
                    ApplyProfileSettings(profileData);

                    // Reinitialize event handlers, including hotkeys
                    ReinitializeEventHandlers(profileData);

                    // Set focus to another control to prevent cursor staying in comboBox2
                    linkLabel1.Focus(); // Set focus to button1
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the profile: {ex.Message}");
                }
            }
        }

        private void ApplyProfileSettings(ProfileData profileData)
        {
            textBoxSetProfileName.Text = profileData.ProfileName;
            textBoxHorizontalParameter.Text = profileData.HorizontalParameter.ToString();
            textBoxVerticalParameter.Text = profileData.VerticalParameter.ToString();
            textBoxGameProcess.Text = profileData.GameProcess;
            textBoxUDPKey.Text = profileData.UDPFloodKey;
            textBoxOnOffKey.Text = profileData.RecoilKey;
            trackBar1.Value = profileData.TrackBarValue;
        }

        private int ConvertToKeyCode(string keyString)
        {
            // Check if the key string starts with "Num"
            if (keyString.StartsWith("Num", StringComparison.OrdinalIgnoreCase))
            {
                // Map the "Num" keys to their corresponding Keys enum values
                switch (keyString)
                {
                    case "Num0":
                        return (int)Keys.NumPad0;
                    case "Num1":
                        return (int)Keys.NumPad1;
                    case "Num2":
                        return (int)Keys.NumPad2;
                    case "Num3":
                        return (int)Keys.NumPad3;
                    case "Num4":
                        return (int)Keys.NumPad4;
                    case "Num5":
                        return (int)Keys.NumPad5;
                    case "Num6":
                        return (int)Keys.NumPad6;
                    case "Num7":
                        return (int)Keys.NumPad7;
                    case "Num8":
                        return (int)Keys.NumPad8;
                    case "Num9":
                        return (int)Keys.NumPad9;
                    default:
                        // Log error or handle unrecognized key string
                        MessageBox.Show($"Unrecognized key string: {keyString}");
                        return -1; // Return an invalid code or handle this case as needed
                }
            }
            else
            {
                // Attempt to parse the key string to a Keys enum value
                if (Enum.TryParse(keyString, out Keys keyCode))
                {
                    return (int)keyCode;
                }
                else
                {
                    // Log error or handle unrecognized key string
                    MessageBox.Show($"Unrecognized key string: {keyString}");
                    return -1; // Return an invalid code or handle this case as needed
                }
            }
        }

        // Add this method to load profile names into comboBox2 during application startup
        private void LoadProfiles()
        {
            if (Directory.Exists(ProfilesDirectory))
            {
                // Clear existing items
                comboBox2.Items.Clear();

                // Get all JSON files in the profiles directory
                string[] profileFiles = Directory.GetFiles(ProfilesDirectory, "*.json");

                // Create a dictionary to store the profile data
                Dictionary<string, ProfileData> profiles = new Dictionary<string, ProfileData>();

                // Extract profile names and data from file names
                foreach (string file in profileFiles)
                {
                    // Extract the profile name from the file name
                    string profileName = Path.GetFileNameWithoutExtension(file);

                    // Deserialize the JSON string into a ProfileData object
                    ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(File.ReadAllText(file));

                    // Add the profile name and ProfileData object to the dictionary
                    profiles[profileName] = profileData;

                    // Add the profile name to the ComboBox
                    comboBox2.Items.Add(profileName);
                }

                // Set the selected profile based on the user's previous selection
                if (comboBox2.SelectedIndex >= 0)
                {
                    HandleProfileSelectionChange(comboBox2.SelectedItem.ToString());
                }
            }
        }

        // Define a class to hold profile data
        public class ProfileData
        {
            public string ProfileName { get; set; }
            public int HorizontalParameter { get; set; }
            public int VerticalParameter { get; set; }
            public string GameProcess { get; set; }

            // UDP Flood Hotkey
            public string UDPFloodKey { get; set; } // Key as string (e.g., "F3")
            public int UDPFloodModifiers { get; set; } // Modifiers for the UDP Flood key
            public int UDPFloodHotkeyId { get; set; } // Unique ID for the UDP Flood hotkey

            // Recoil Control Hotkey
            public string RecoilKey { get; set; } // Key as string (e.g., "D7")
            public int RecoilModifiers { get; set; } // Modifiers for the Recoil key
            public int RecoilHotkeyId { get; set; } // Unique ID for the Recoil hotkey

            public int TrackBarValue { get; set; }
            // Add other properties as needed
        }

        private void ReinitializeEventHandlers(ProfileData profileData)
        {
            // Unregister previous hotkeys if they were registered
            // You might need to keep track of what was previously registered
            UnregisterHotKey(this.Handle, profileData.UDPFloodHotkeyId);
            UnregisterHotKey(this.Handle, profileData.RecoilHotkeyId);

            // Register new hotkeys based on the loaded profile
            int udpFloodKeyCode = ConvertToKeyCode(profileData.UDPFloodKey);

            RegisterHotKey(this.Handle, profileData.UDPFloodHotkeyId, profileData.UDPFloodModifiers, udpFloodKeyCode);
        }

        //OVERLAY
        private void InitializeOverlay()
        {
            // Instantiate the overlay with the current instance of Form1
            overlay = new Overlay(this);

            // Hook up event handler for the checkbox
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Ensure overlay object is not null
            if (overlay != null)
            {
                // Show/hide the overlay when the checkbox is checked/unchecked
                overlay.Visible = checkBox1.Checked;
            }
        }

        // Add this method to load profile names into comboBox2 during application startup
        public void LoadProfilesIntoComboBox(ComboBox comboBox)
        {
            if (Directory.Exists(ProfilesDirectory))
            {
                // Clear existing items
                comboBox.Items.Clear();

                // Get all JSON files in the profiles directory
                string[] profileFiles = Directory.GetFiles(ProfilesDirectory, "*.json");

                // Extract profile names from file names
                foreach (string file in profileFiles)
                {
                    string profileName = Path.GetFileNameWithoutExtension(file);
                    comboBox.Items.Add(profileName);
                }
            }
        }

        // Add this method to handle profile selection change
        public void HandleProfileSelectionChange(string selectedProfile)
        {
            if (!string.IsNullOrEmpty(selectedProfile))
            {
                // Construct the file path for the selected profile
                string profileFilePath = Path.Combine(ProfilesDirectory, selectedProfile + ".json");

                try
                {
                    // Read the contents of the selected profile file
                    string profileJson = File.ReadAllText(profileFilePath);

                    // Deserialize the JSON into appropriate objects
                    ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(profileJson);

                    // Apply the settings from the selected profile directly to your application's UI or logic
                    textBoxSetProfileName.Text = profileData.ProfileName;
                    textBoxHorizontalParameter.Text = profileData.HorizontalParameter.ToString();
                    textBoxVerticalParameter.Text = profileData.VerticalParameter.ToString();
                    textBoxGameProcess.Text = profileData.GameProcess;
                    textBoxUDPKey.Text = profileData.UDPFloodKey;
                    textBoxOnOffKey.Text = profileData.RecoilKey;

                    // Update the track bar value
                    trackBar1.Value = profileData.TrackBarValue;

                    // Register hotkey based on the loaded profile
                    RegisterHotKey(this.Handle, profileData.UDPFloodHotkeyId, (int)profileData.UDPFloodModifiers, ConvertToKeyCode(profileData.UDPFloodKey));

                    // Optionally, perform any other actions or updates based on the selected profile
                    MessageBox.Show($"Profile '{selectedProfile}' loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the profile: {ex.Message}");
                }
            }
        }
    }
}
