using System;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

// TODO
// Get and put the scores to GUI, alongside the player list

namespace Player
{
    public partial class Form1 : Form
    {
        Socket client;

        //  States
        private bool connected = false;
        private bool listens = true;
        private bool joined = false;
        private bool deciding_invitation = false;
        private bool in_game = false;
        private bool waiting_for_player_list = false;

        public Form1()
        {
            InitializeComponent();

            inviteInput.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            Random rnd = new Random();
            usernameInput.Text += rnd.Next(1, 500).ToString();
        }

        //  Functions for console output
        private void AppendText(string message, Color text_color = default(Color), Color bg_color = default(Color))
        {
            if (text_color == default(Color))
            {
                console.AppendText(message);
            }
            else
            {
                console.SelectionStart = console.TextLength;
                console.SelectionLength = 0;

                console.SelectionColor = (Color)text_color;

                if (bg_color != default(Color))
                {
                    console.SelectionBackColor = (Color)bg_color;
                }

                console.AppendText(message);
                console.SelectionColor = console.ForeColor;

                if (bg_color != default(Color))
                {
                    console.SelectionBackColor = console.BackColor;
                }
            }
        }
        private void CLog(string message, Color text_color = default(Color), Color bg_color = default(Color))
        {
            if (console.InvokeRequired)     //  If reached from another thread
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    AppendText(message + '\n', text_color, bg_color);
                }));
            }
            else
            {
                AppendText(message + '\n', text_color, bg_color);
            }
        }
        private void CLogEndLine()
        {
            CLog("\n");
        }
        private void CLogError(string message, Exception e = default(Exception))
        {
            CLog("Error! " + message + (e == default(Exception) ? "" : "\nError Message : " + e.Message), Color.WhiteSmoke, Color.DarkRed);
        }

        //  Tries to connect to the server with given values
        private void ConnectToServer(string ip, int port)
        {
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ip, port);

                this.connected = true;

                CLog("Connected to the server on " + ip + ":" + port, Color.DarkGreen, Color.Yellow);

                CLog("Your address is [" + client.LocalEndPoint + "]", Color.GhostWhite, Color.DodgerBlue);

                //  Change form state to connected
                Invoke(new MethodInvoker(() =>
                {
                    portInput.Enabled = false;
                    ipInput.Enabled = false;
                    usernameInput.Enabled = true;

                    loginButton.Enabled = true;
                    connectButton.Text = "Leave";
                }));

                //  Lisen if there is a greeting message from server
                GetMessage();
            }
            catch (Exception e)
            {
                CLogError("Connection fault. Check the IP and Port", e);
            }
        }

        //  Requests to join the lobby with the given username
        private void JoinLobby()
        {
            try
            {
                //  Send a username suggestion to server
                SendMessage(usernameInput.Text);

                //  Listen for an answer
                string response = GetMessage();

                if (response == "Successful login")
                {
                    //  Change form state to joined
                    this.joined = true;
                    this.listens = true;

                    Invoke(new MethodInvoker(() =>
                    {
                        usernameInput.Enabled = false;
                        connectButton.Enabled = false;

                        loginButton.Enabled = false;
                        loginButton.Text = "Logged In!";

                        connectButton.Enabled = true;   //  To exit lobby

                        refreshButton.Enabled = true;   //  To refresh player list

                        inviteButton.Enabled = true;    //  To invite a player to a game
                        inviteInput.Enabled = true;
                    }));

                    //  Refresh once to fill the player list
                    RefreshPlayerList();

                    //  Then start listening for any incoming messages
                    new Thread(new ThreadStart(Receive)).Start();
                }
            }
            catch (SocketException)
            {
                CLogError("Connection with the server is down. Please connect again");
                CloseConnection();
            }
            catch (Exception e)
            {
                CLogError("Connection fault while joining!", e);
                CloseConnection();
            }
        }

        //  Requests the player list
        private void RefreshPlayerList()
        {
            try
            {
                this.waiting_for_player_list = true;

                SendMessage("Send me the Player List");
            }
            catch (SocketException)
            {
                CLogError("Connection with the server is down. Please connect again");
                CloseConnection();
            }
            catch (Exception e)
            {
                CLogError("Connection fault while refreshing the player list!", e);
                CloseConnection();
            }
        }

        private void ProcessPlayerList(string message)
        {
            string[] players =  message.Split( '\n' );

            Invoke(new MethodInvoker(() =>
            {
                //  Clear List
                playerList.Items.Clear();

                //  Add all players
                foreach (string player in players)
                {
                    string[] player_info = player.Split( '\t' );
                    playerList.Items.Add( player_info[ 0 ] + " [" + player_info[ 1 ] + "]" );
                }

                //  Refresh autocomplete list
                inviteInput.AutoCompleteCustomSource.Clear();
                inviteInput.AutoCompleteCustomSource.AddRange(players);
            })
            );
        }

        //  Looks for any input
        private void Receive()
        {
            while (this.connected)
            {
                try
                {
                    if (this.listens)
                    {
                        string message = GetMessage();

                        if (this.waiting_for_player_list)
                        {
                            ProcessPlayerList(message);
                            this.waiting_for_player_list = false;
                        }
                        else if (message.StartsWith("Invitation from"))
                        {
                            string inviter = message.Split(' ').Last();

                            Invoke(new MethodInvoker(() =>
                            {
                                inviteButton.Text = "Waiting Response";
                                inviteButton.Enabled = false;
                            }));

                            this.deciding_invitation = true;

                            new Thread(() => ApproveInvitation(inviter)).Start();
                        }
                        else if (message.Contains("Game Started!"))
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                inviteButton.Text = "Surrender!";
                                inviteButton.Enabled = true;

                                guessPanel.Visible = true;
                                guessButton.Enabled = true;
                                guessInput.Enabled = true;
                            }));

                            this.in_game = true;
                            // CLog("Game started", Color.DodgerBlue, Color.White); // better to be informed by the server.
                        }
                        else if (message.Contains("declined") || message.Contains("disconnected player"))
                        {
                            this.deciding_invitation = false;

                            Invoke(new MethodInvoker(() =>
                            {
                                inviteButton.Text = "Invite to Game";
                                inviteButton.Enabled = true;
                            }));
                        }
                        else if (message.Contains("You won the game") || message.Contains("You lost the game"))
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                inviteButton.Text = "Invite to Game";
                                inviteButton.Enabled = true;

                                guessPanel.Visible = false;
                                guessButton.Enabled = false;
                                guessInput.Enabled = false;
                                guessInput.ResetText();
                            }));

                            this.in_game = false;
                        }
                        else if (message == "Invitation sent")
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                inviteButton.Text = "Waiting Response";
                                inviteButton.Enabled = false;
                            }));
                        }
                    }
                }
                catch (Exception e)
                {
                    if (this.connected)
                    {
                        CLogError("Connection fault on Receiving!", e);
                    }
                }
            }
        }

        //  Ask if user wants to accept or decline
        private void ApproveInvitation(string inviter)
        {
            DialogResult dialogResult = MessageBox.Show("Would you like to join?", inviter + " invited you to a game", MessageBoxButtons.YesNo);

            if (!this.connected || !this.deciding_invitation)
            {
                return;
            }

            if (dialogResult == DialogResult.Yes)
            {
                SendMessage("Accept");
            }
            else
            {
                SendMessage("Decline");

                Invoke(new MethodInvoker(() =>
                {
                    inviteButton.Text = "Invite to Game";
                    inviteButton.Enabled = true;
                }));
            }
        }

        //  Closes the socket
        private void CloseConnection()
        {
            if (!this.connected)
            {
                return;
            }

            this.connected = false;
            this.joined = false;
            this.in_game = false;
            client.Close();

            //  Stop any waitings
            this.waiting_for_player_list = false;
            this.deciding_invitation = false;

            Invoke(new MethodInvoker(() =>
            {
                portInput.Enabled = true;
                ipInput.Enabled = true;
                usernameInput.Enabled = false;

                connectButton.Text = "Connect";

                loginButton.Enabled = false;
                loginButton.Text = "Log In";

                refreshButton.Enabled = false;

                inviteButton.Text = "Invite to Game";
                inviteButton.Enabled = false;
                inviteInput.Enabled = false;

                guessPanel.Visible = false;
                guessButton.Enabled = false;
                guessInput.ResetText();
                guessInput.Enabled = false;
            })
            );

            CLog("Connection closed!", Color.DarkRed, Color.Yellow);
        }

        //  Sends message to server
        private void SendMessage(string message)
        {
            byte[] buffer = Encoding.Default.GetBytes(message);

            client.Send(buffer);

            CLog("To Server: " + message, Color.LightBlue);
        }

        //  Waits for a message and returns
        private string GetMessage(int byteSize = 64)
        {
            Byte[] buffer = new byte[64];
            int rec;
            try
            {
                rec = client.Receive(buffer);
            }
            catch (Exception)
            {
                CloseConnection();
                return "";
            }

            if (rec <= 0)
            {
                throw new SocketException();
            }

            string message = Encoding.Default.GetString(buffer);
            message = message.Substring(0, message.IndexOf("\0"));

            CLog("Server: " + message, Color.LightYellow);

            return message;
        }

        //  Click functions do what the buttons say and a lot of form state control.
        //  It works correctly and explanation will be unnecessary, just try the program.
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (this.connected)
            {
                CloseConnection();
            }
            else
            {
                //  Form validation
                string ip = ipInput.Text;
                if (ip == "")
                {
                    CLogError("IP can't be empty");
                    return;
                }

                string port = portInput.Text;
                if (port == "")
                {
                    CLogError("Port number can't be empty");
                    return;
                }

                int port_number;
                try
                {
                    port_number = Convert.ToInt16(port);
                }
                catch
                {
                    CLogError("Port number must be a number between 0 and 65535");
                    return;
                }

                //  Try to connect with given values
                new Thread(() => ConnectToServer(ip, port_number)).Start();
            }
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (this.joined)
            {
                CLog("New Achievement: Easter Egg Hunter!!", Color.White, Color.Purple);
                CLog("This part supposed to be unreachable ^^", Color.White, Color.Purple);
                CLog("I suggest you to restart the program to prevent any error.", Color.White, Color.Purple);
            }
            else
            {
                new Thread(new ThreadStart(JoinLobby)).Start();
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RefreshPlayerList)).Start();
        }
        private void inviteButton_Click(object sender, EventArgs e)
        {
            if (this.in_game)
            {
                SendMessage("Surrender");

                this.in_game = false;
                this.listens = true;

                inviteButton.Text = "Invite to Game";

                guessPanel.Visible = false;
                guessButton.Enabled = false;
                guessInput.Enabled = false;
                guessInput.ResetText();

                return;
            }

            string player_invited = inviteInput.Text;

            if (player_invited == "")
            {
                CLogError("Please choose a player");
                return;
            }
            else if (player_invited == usernameInput.Text)
            {
                CLogError("You can't invite yourself. Please choose another player");
                return;
            }

            //  Send invitation request to server
            SendMessage("Invite " + player_invited);
        }
        private void guessButton_Click( object sender, EventArgs e )
        {
            int input;
            if( Int32.TryParse( guessInput.Text, out input ) && input >= 1 && input <= 100 )
            {
                SendMessage( "Guess: " + guessInput.Text );
                CLog( "You guessed: " + guessInput.Text );
            }
            else
            {
                CLogError( "Please input an integer between 1 and 100" );
            }
        }
        //  Puts player name from list to textbox
        private void playerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // inviteInput.Text = playerList.SelectedItem?.ToString(); //  WOW
            if (playerList.SelectedItem != null)
            {
                string player = playerList.SelectedItem.ToString();
                inviteInput.Text = player.Substring( 0, player.LastIndexOf( ' ' ) );
            }
        }

        //  Closes the server and other sockets when the window is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.connected)
            {
                CloseConnection();
            }

            CLog("Good bye!");
        }
    }
}
