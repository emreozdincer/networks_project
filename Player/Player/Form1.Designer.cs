namespace Player
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.module_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.console_label = new System.Windows.Forms.Label();
            this.console = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.loginButton = new System.Windows.Forms.Button();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.username_label = new System.Windows.Forms.Label();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.ip_label = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.portInput = new System.Windows.Forms.TextBox();
            this.port_label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.playerList = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.inviteInput = new System.Windows.Forms.TextBox();
            this.inviteButton = new System.Windows.Forms.Button();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.guessPanel = new System.Windows.Forms.Panel();
            this.guessLabel = new System.Windows.Forms.Label();
            this.guessButton = new System.Windows.Forms.Button();
            this.guessInput = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.guessPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.module_name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(982, 550);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // module_name
            // 
            this.module_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.module_name.AutoSize = true;
            this.module_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.module_name.Location = new System.Drawing.Point(359, 0);
            this.module_name.Name = "module_name";
            this.module_name.Size = new System.Drawing.Size(264, 45);
            this.module_name.TabIndex = 0;
            this.module_name.Text = "Client Module";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 48);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(976, 499);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.console_label);
            this.panel1.Controls.Add(this.console);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(491, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 493);
            this.panel1.TabIndex = 1;
            // 
            // console_label
            // 
            this.console_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.console_label.AutoSize = true;
            this.console_label.BackColor = System.Drawing.Color.White;
            this.console_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.console_label.Location = new System.Drawing.Point(395, 5);
            this.console_label.Margin = new System.Windows.Forms.Padding(5);
            this.console_label.Name = "console_label";
            this.console_label.Padding = new System.Windows.Forms.Padding(6);
            this.console_label.Size = new System.Drawing.Size(82, 32);
            this.console_label.TabIndex = 0;
            this.console_label.Text = "Console";
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.Black;
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.console.ForeColor = System.Drawing.Color.Lime;
            this.console.Location = new System.Drawing.Point(0, 0);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(482, 493);
            this.console.TabIndex = 1;
            this.console.Text = "\n\nWelcome!\nPlease enter an IP and a port number to connect.\n\n";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.guessPanel, 0, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(482, 493);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.loginButton);
            this.panel2.Controls.Add(this.usernameInput);
            this.panel2.Controls.Add(this.username_label);
            this.panel2.Controls.Add(this.ipInput);
            this.panel2.Controls.Add(this.ip_label);
            this.panel2.Controls.Add(this.connectButton);
            this.panel2.Controls.Add(this.portInput);
            this.panel2.Controls.Add(this.port_label);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(476, 91);
            this.panel2.TabIndex = 0;
            // 
            // loginButton
            // 
            this.loginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginButton.Enabled = false;
            this.loginButton.Location = new System.Drawing.Point(387, 63);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(86, 25);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // usernameInput
            // 
            this.usernameInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameInput.Enabled = false;
            this.usernameInput.Location = new System.Drawing.Point(88, 64);
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Size = new System.Drawing.Size(293, 24);
            this.usernameInput.TabIndex = 3;
            this.usernameInput.Text = "qweqwe";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(4, 67);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(77, 18);
            this.username_label.TabIndex = 5;
            this.username_label.Text = "Username";
            // 
            // ipInput
            // 
            this.ipInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipInput.Location = new System.Drawing.Point(97, 3);
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(284, 24);
            this.ipInput.TabIndex = 1;
            this.ipInput.Text = "localhost";
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Location = new System.Drawing.Point(4, 4);
            this.ip_label.Margin = new System.Windows.Forms.Padding(3);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(85, 18);
            this.ip_label.TabIndex = 3;
            this.ip_label.Text = "IP of Server";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Location = new System.Drawing.Point(387, 2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(86, 55);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portInput
            // 
            this.portInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portInput.Location = new System.Drawing.Point(106, 33);
            this.portInput.Name = "portInput";
            this.portInput.Size = new System.Drawing.Size(275, 24);
            this.portInput.TabIndex = 2;
            this.portInput.Text = "666";
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(3, 35);
            this.port_label.Margin = new System.Windows.Forms.Padding(3);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(96, 18);
            this.port_label.TabIndex = 0;
            this.port_label.Text = "Port to Listen";
            this.port_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.refreshButton);
            this.groupBox1.Controls.Add(this.playerList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(16, 10, 16, 16);
            this.groupBox1.Size = new System.Drawing.Size(476, 317);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connected Players";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Enabled = false;
            this.refreshButton.Location = new System.Drawing.Point(382, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(86, 25);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // playerList
            // 
            this.playerList.BackColor = System.Drawing.Color.GhostWhite;
            this.playerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerList.FormattingEnabled = true;
            this.playerList.ItemHeight = 18;
            this.playerList.Location = new System.Drawing.Point(16, 27);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(444, 274);
            this.playerList.TabIndex = 3;
            this.playerList.SelectedIndexChanged += new System.EventHandler(this.playerList_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.inviteInput);
            this.panel3.Controls.Add(this.inviteButton);
            this.panel3.Controls.Add(this.playerNameLabel);
            this.panel3.Location = new System.Drawing.Point(3, 423);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(476, 31);
            this.panel3.TabIndex = 2;
            // 
            // inviteInput
            // 
            this.inviteInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inviteInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.inviteInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.inviteInput.Enabled = false;
            this.inviteInput.Location = new System.Drawing.Point(102, 4);
            this.inviteInput.Name = "inviteInput";
            this.inviteInput.ReadOnly = true;
            this.inviteInput.Size = new System.Drawing.Size(253, 24);
            this.inviteInput.TabIndex = 2;
            // 
            // inviteButton
            // 
            this.inviteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inviteButton.Enabled = false;
            this.inviteButton.Location = new System.Drawing.Point(361, 3);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(112, 25);
            this.inviteButton.TabIndex = 1;
            this.inviteButton.Text = "Invite to Game";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new System.Drawing.Point(3, 7);
            this.playerNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(93, 18);
            this.playerNameLabel.TabIndex = 0;
            this.playerNameLabel.Text = "Player Name";
            this.playerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guessPanel
            // 
            this.guessPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guessPanel.Controls.Add(this.guessInput);
            this.guessPanel.Controls.Add(this.guessButton);
            this.guessPanel.Controls.Add(this.guessLabel);
            this.guessPanel.Location = new System.Drawing.Point(3, 460);
            this.guessPanel.Name = "guessPanel";
            this.guessPanel.Size = new System.Drawing.Size(476, 30);
            this.guessPanel.TabIndex = 3;
            this.guessPanel.Visible = false;
            // 
            // guessLabel
            // 
            this.guessLabel.AutoSize = true;
            this.guessLabel.Location = new System.Drawing.Point(4, 6);
            this.guessLabel.Margin = new System.Windows.Forms.Padding(3);
            this.guessLabel.Name = "guessLabel";
            this.guessLabel.Size = new System.Drawing.Size(143, 18);
            this.guessLabel.TabIndex = 0;
            this.guessLabel.Text = "What is your guess?";
            // 
            // guessButton
            // 
            this.guessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guessButton.Enabled = false;
            this.guessButton.Location = new System.Drawing.Point(398, 3);
            this.guessButton.Name = "guessButton";
            this.guessButton.Size = new System.Drawing.Size(75, 25);
            this.guessButton.TabIndex = 1;
            this.guessButton.Text = "Guess";
            this.guessButton.UseVisualStyleBackColor = true;
            this.guessButton.Click += new System.EventHandler(this.guessButton_Click);
            // 
            // guessInput
            // 
            this.guessInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guessInput.Enabled = false;
            this.guessInput.Location = new System.Drawing.Point(152, 3);
            this.guessInput.Name = "guessInput";
            this.guessInput.Size = new System.Drawing.Size(240, 24);
            this.guessInput.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1009, 577);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "Form1";
            this.Text = "Player Module";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.guessPanel.ResumeLayout(false);
            this.guessPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label module_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label console_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox portInput;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.TextBox ipInput;
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.TextBox inviteInput;
        private System.Windows.Forms.Panel guessPanel;
        private System.Windows.Forms.Button guessButton;
        private System.Windows.Forms.Label guessLabel;
        private System.Windows.Forms.TextBox guessInput;
    }
}