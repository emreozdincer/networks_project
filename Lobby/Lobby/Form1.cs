using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// Changes:
// moved Connection class to the bottom to fix a weird bug.

// TODO:
// Send scores with names when requested

namespace Lobby
{

    //  This class holds a player's information
    class Connection
    {
        public Socket socket;
        public string username;
        public bool in_game;
        public Connection invited_by;
        public Color color; //  Might use in future
        public int score;

        // for in-game values, -1 is used to imply i 
        public int in_game_score;
        public int current_guess;
        public int current_round;
        public int game_answer;

        public Connection( Socket socket, string username, Color color )
        {
            // about connection/player
            this.socket = socket;
            this.username = username;
            this.color = color;
            this.in_game = false;
            this.invited_by = null;
            this.score = 0;

            // about connection/player's in game scores (-1 is used to imply null)
            this.in_game_score = 0;
            this.current_guess = -1;
            this.current_round = -1;
            this.game_answer = -1;
        }
    }

    public partial class Form1 : Form
    {
        Socket server;
        List<Socket> handshake_list = new List<Socket>();
        List<Connection> connection_list = new List<Connection>();

        //  State
        private bool listening = false;

        // For in-game random numbers
        Random random = new Random();
        int answer;
        int score_diff;

        public Form1()
        {
            InitializeComponent();
        }

        //  Functions for console output
        private void AppendText(string message, Color text_color = default( Color ), Color bg_color = default( Color ))
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
        private void CLog(string message, Color text_color = default( Color ), Color bg_color = default( Color ))
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
        private void CLogError(string message, Exception e = default( Exception ))
        {
            CLog("Error! " + message + (e == default(Exception) ? "" : "\nError Message : " + e.Message), Color.WhiteSmoke, Color.DarkRed);
        }

        //  Tries to Initialize a server
        private bool InitServer(int port)
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(new IPEndPoint(IPAddress.Any, port));
                server.Listen(3);

                this.listening = true;

                Thread thr_listen = new Thread(new ThreadStart(Listen));
                thr_listen.Start();

                CLog("Started listening for incoming connections on port:" + port, Color.DarkGreen, Color.Yellow);

                return true;
            }
            catch (SocketException)
            {
                CLogError("Please try a different port");
                return false;
            }
            catch (Exception e)
            {
                CLogError("Error while server initialization", e);
                return false;
            }
        }

        private void Listen()
        {
            while (this.listening)
            {
                try
                {
                    Socket new_socket = server.Accept();
                    handshake_list.Add(new_socket); //  Add any connection request to the handshake_list
                    CLog("[NewC][" + new_socket.RemoteEndPoint + "] just connected", Color.DarkGreen, Color.DarkGray);

                    Thread thr_receive = new Thread(() => HandShake(new_socket));
                    thr_receive.Start();
                }
                catch (Exception e)
                {
                    if (this.listening)
                    {
                        CLogError("Connection fault while listening", e);
                    }
                }
            }
        }

        //  Closes server and closes sockets on lists
        private void CloseServer()
        {
            this.listening = false;
            server.Close();

            //  Clear players and close sockets
            while (connection_list.Count != 0)
            {
                RemovePlayer(connection_list.Last());
            }

            //  Close sockets of handshakers
            foreach (Socket socket in handshake_list)
            {
                string address = socket.RemoteEndPoint.ToString();
                socket.Close();
                CLog("[DisC][" + address + "] Disconnected", Color.Brown, Color.LightGoldenrodYellow);
            }
            handshake_list.Clear();

            CLog("Stopped listening", Color.DarkRed, Color.Yellow);
        }

        private void SendMessage(Socket socket, string message)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(message);

                socket.Send(buffer);

                CLog("[To  ][" + socket.RemoteEndPoint + "] : " + message, Color.LightBlue);
            }
            catch (Exception e)
            {
                CLogError("Error in SendMessage", e);
            }

        }
        private string GetMessage(Socket socket, int byteSize = 64)
        {
            Byte[] buffer = new byte[64];
            int rec = socket.Receive(buffer);

            if (rec <= 0)
            {
                throw new SocketException();
            }

            string message = Encoding.Default.GetString(buffer);
            message = message.Substring(0, message.IndexOf("\0"));

            CLog("[From][" + socket.RemoteEndPoint + "] : " + message, Color.LightYellow);

            return message;
        }

        private void HandShake(Socket socket)
        {
            bool validated = false;
            string newUsername = "";

            try
            {
                SendMessage(socket, "Choose a username to login");
            }
            catch (Exception e)
            {
                throw e;
            }

            //  Take usernames until a valid one given or the player disconnets
            while (!validated)
            {
                try
                {
                    newUsername = GetMessage(socket);

                    if (ValidUsername(newUsername))
                    {
                        validated = true;

                        SendMessage(socket, "Successful login");
                    }
                    else
                    {
                        SendMessage(socket, "This username is already in use. Try another one");
                    }
                }
                catch (Exception e)
                {
                    if (this.listening)
                    {
                        CLog("[FtHS][" + socket.RemoteEndPoint + "] Failed to handshake", Color.Brown, Color.DarkGray);
                        handshake_list.Remove(socket);
                    }

                    socket.Close();
                    return;
                }
            }

            //  If validated without an error
            Connection connection = new Connection(socket, newUsername, Color.Red);

            new Thread(() => Receive(connection)).Start();
        }

        private void Receive(Connection connection)
        {
            AddNewPlayer( connection );

            bool connected = true;

            while (connected)
            {
                try
                {
                    string message = GetMessage( connection.socket );

                    if (message == "Send me the Player List")
                    {
                        SendMessage(connection.socket, PlayerList2String());
                    }
                    else if (message.StartsWith("Invite "))
                    {
                        string invited_player = message.Substring(7);

                        Connection invited_connection = connection_list.Find( ( elm ) => elm.username == invited_player );

                        if (invited_connection == null)
                        {
                            SendMessage(connection.socket, "There is no player with this name in the lobby");
                        }
                        else if (invited_connection.username == connection.username)
                        {
                            SendMessage(connection.socket, "You can't start a game with yourself. Choose another player");
                        }
                        else if (invited_connection.invited_by != null)
                        {
                            SendMessage(connection.socket, invited_connection.username + " is already invited by another player");
                        }
                        else if (invited_connection.in_game)
                        {
                            SendMessage(connection.socket, invited_connection.username + " is already in a game");
                        }
                        else
                        {
                            SendMessage(invited_connection.socket, "Invitation from " + connection.username);
                            invited_connection.invited_by = connection;

                            SendMessage(connection.socket, "Invitation sent");
                            connection.invited_by = invited_connection;
                        }
                    }
                    else if (message == "Accept")
                    {
                        UpdateBeforeGameStart(connection, connection.invited_by);
                    }
                    else if (message == "Decline")
                    {
                        SendMessage(connection.invited_by.socket, connection.username + " declined your invitation");
                        connection.invited_by.invited_by = null;
                        connection.invited_by = null;
                    }
                    else if (message == "Surrender")
                    {
                        UpdateAfterGameEnd(connection.invited_by, connection);
                    }
                    else if (message.StartsWith("Guess: "))
                    {
                        // Read connection's guess.
                        int index = message.IndexOf(" ") + 1;
                        connection.current_guess = Int32.Parse(message.Substring(index));
                        CLog(connection.username + "'s guess is: " + connection.current_guess);

                        // Check if the opponent also has a current guess, if so, continue; otherwise, don't do anything
                        if (connection.invited_by.current_guess != -1)
                        {
                            // Round is being determined. There are two players who gave an answer: 
                            // connection and connection.invited_by (referred as opponent in comments)

                            // Send players' guesses, and the actual answer to both
                            SendMessage(connection.invited_by.socket, "Opponent guessed: " + connection.current_guess.ToString()
                               + "\nThe answer was: " + connection.game_answer);
                            SendMessage(connection.socket, "Opponent guessed: " + connection.invited_by.current_guess.ToString()
                               + "\nThe answer was: " + connection.invited_by.game_answer);

                            // If guesses are same, round is tied.
                            if (connection.current_guess == connection.invited_by.current_guess)
                            {
                                CLog("Same answers, round tied.");
                                SendMessage(connection.invited_by.socket, "Round is tied.");
                                SendMessage(connection.socket, "Round is tied.");
                                UpdateBeforeNextRound(connection, connection.invited_by);
                            }
                            else // If different guesses, find guess confidences
                            {
                                // Confidence: How close to the actual answer
                                int connection_confidence = Math.Abs(connection.game_answer - connection.current_guess);
                                int opponent_confidence = Math.Abs(connection.game_answer - connection.invited_by.current_guess);

                                // if same confidence, round is tied, just update round
                                if (connection_confidence == opponent_confidence)
                                {
                                    CLog("Same accurucies, round tied.");
                                    SendMessage(connection.invited_by.socket, "Round is tied.");
                                    SendMessage(connection.socket, "Round is tied.");
                                    UpdateBeforeNextRound(connection, connection.invited_by);
                                }
                                else // A player won this round
                                {
                                    // Connection won
                                    if (connection_confidence < opponent_confidence)
                                    {
                                        UpdateInGameScore(connection, connection.invited_by);
                                    }
                                    else  // Opponent won
                                    {
                                        UpdateInGameScore(connection.invited_by, connection);
                                    }

                                    // If the connection player won the game
                                    if (connection.in_game_score == 2)
                                    {
                                            UpdateAfterGameEnd(connection, connection.invited_by);
                                    }   
                                    else if (connection.invited_by.in_game_score == 2) // If the opponent player won
                                    {
                                            UpdateAfterGameEnd(connection.invited_by, connection);
                                    }
                                    // If game hasn't ended
                                    else 
                                    {
                                        UpdateBeforeNextRound(connection, connection.invited_by);
                                    }
                                }
                            }
                        }


                    }
                }
                catch   //  Client is closed or some other error happend
                {
                    // if server is still listening
                    if (this.listening)
                    {
                        RemovePlayer( connection );
                    }

                    //  If the server is not listening anymore, deleting is done on CloseServer already

                    connected = false;  //  Stop this thead no matter what
                }
            }
        }



        // This function is called when a player accepts the invitation and sends "Accept" message,
        // to initiate the game.
        private void UpdateBeforeGameStart(Connection p1, Connection p2)
        {
            answer = random.Next(1, 100);
            CLog( "A game started between " + p1.username + " vs " + p2.username, Color.OrangeRed );
            CLog( "The hidden answer is : " + answer.ToString(), Color.Orange);

            p1.in_game = true;
            p1.current_round = 1;
            p1.game_answer = answer;
            SendMessage(p1.socket, "Game Started!\nRound 1: Enter a number between 1 and 100: ");

            p2.in_game = true;
            p2.current_round = 1;
            p2.game_answer = answer;
            SendMessage(p2.socket, "Game Started!\nRound 1: Enter a number between 1 and 100: ");
        }

        // This function is called when a round winner is determined 
        // to update in game scores and broadcast the round winner.
        private void UpdateInGameScore(Connection winner, Connection loser)
        {
            SendMessage(winner.socket, "You won the round.");
            winner.in_game_score += 1;

            SendMessage(loser.socket, "You lost the round.");

            CLog( winner.username + " won the round.", Color.OrangeRed );
            CLog( "Scores:", Color.Orange );
            CLog( winner.username + " : " + winner.in_game_score, Color.Orange );
            CLog( loser.username + " : " + loser.in_game_score, Color.Orange );
        }

        // This function is called when a new round is to start
        private void UpdateBeforeNextRound(Connection p1, Connection p2)
        {
            answer = random.Next(1, 100);
            CLog("New round starting. The hidden answer is: " + answer.ToString(), Color.Orange );

            p1.game_answer = answer;
            p1.current_guess = -1;
            p1.current_round += 1;
            SendMessage(p1.socket, "Round " + p1.current_round.ToString() + " started. Enter guess: ");

            p2.game_answer = answer;
            p2.current_guess = -1;
            p2.current_round += 1;
            SendMessage(p2.socket, "Round " + p2.current_round.ToString() + " started. Enter guess: ");

        }

        // This function is called when a game between two players ends (including disconnections).
        private void UpdateAfterGameEnd( Connection winner, Connection loser )
        {
            CLog( winner.username + " won the game againist " + loser.username, Color.OrangeRed );

            winner.in_game = false;
            winner.invited_by = null;
            winner.score += 1;
            winner.in_game_score = 0;
            winner.current_guess = -1;
            winner.current_round = -1;
            SendMessage(winner.socket, "You won the game and earned 1 point.");

            loser.in_game = false;
            loser.invited_by = null;
            loser.in_game_score = 0;
            loser.current_guess = -1;
            winner.current_round = -1;
            SendMessage(loser.socket, "You lost the game.");

            RefreshPlayerList();
        }

        //  Called when a player disconnects from a game
        private void UpdateAfterGameEndByDisconnection( Connection winner, Connection loser )
        {
            CLog( winner.username + " won the game againist " + loser.username, Color.OrangeRed );

            winner.in_game = false;
            winner.invited_by = null;
            winner.score += 1;
            winner.in_game_score = 0;
            winner.current_guess = -1;
            winner.current_round = -1;
            SendMessage( winner.socket, "You won the game and earned 1 point." );

            RefreshPlayerList();
        }

        private void RefreshPlayerList()
        {
            Invoke( new MethodInvoker( () =>
            {
                playerList.Items.Clear();

                foreach( Connection pleyer in connection_list )
                {
                    playerList.Items.Add( pleyer.username + " [" + pleyer.score + "]" );
                }
            } )
            );
        }
        private string PlayerList2String()
        {
            if (playerList.InvokeRequired)
            {
                return (string)this.Invoke(
                    new Func<string>(() => PlayerList2String())
                );
            }
            else
            {
                var players = connection_list.Select( con => (con.username + '\t' + con.score) );

                return String.Join( "\n",  players );
            }
        }

        //  Checks if the username is currently in the list or not
        private bool ValidUsername(string newUsername)
        {
            if (playerList.InvokeRequired)
            {
                return (bool)this.Invoke(
                    new Func<bool>(() => ValidUsername(newUsername))
                );
            }
            else
            {
                return playerList.FindString( newUsername ) == ListBox.NoMatches;
            }
        }

        private void AddNewPlayer(Connection connection)
        {
            CLog( "[NewP][" + connection.socket.RemoteEndPoint + "][" + connection.username + "] joined to lobby. Welcome!", Color.DarkGreen, Color.LightGoldenrodYellow );

            handshake_list.Remove(connection.socket);
            connection_list.Add(connection);

            RefreshPlayerList();
        }

        private void RemovePlayer(Connection connection)
        {
            CLog("[RmwP][" + connection.socket.RemoteEndPoint + "][" + connection.username + "] removed from lobby", Color.Brown, Color.LightGoldenrodYellow);

            if (connection.invited_by != null)
            {
                if( connection.in_game )
                {
                    SendMessage( connection.invited_by.socket, "Game canceled because of disconnected player" );
                    UpdateAfterGameEndByDisconnection( connection.invited_by, connection );
                }
                else
                {
                    SendMessage( connection.invited_by.socket, "Invitation canceled because of disconnected player" );
                    connection.invited_by.invited_by = null;
                }

            }

            connection_list.Remove( connection );
            connection.socket.Close();
                
            RefreshPlayerList();
        }

        //  Opens and closes the server according to the curent state.
        //  Also does form state control.
        private void ListenButton_Click(object sender, EventArgs e)
        {
            if (this.listening)  //  Already listening on a port
            {
                CloseServer();

                portInput.Enabled = true;
                listenButton.Text = "Listen";
            }
            else    //  Init a new server
            {
                //  Form validation
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

                if (InitServer(port_number))
                {
                    portInput.Enabled = false;
                    listenButton.Text = "Stop";
                }
            }
        }

        //  Closes the server and other sockets when the window is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.listening)
            {
                CloseServer();
            }

            CLog("Good bye!");
        }
    }
}
