using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakesAndLadders
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void log(string msg)
        {
            lstLog.Items.Add(msg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable gameSquares = new Hashtable();

            using (StreamReader sr = new StreamReader("gameboard.txt"))
            {
                string contents = sr.ReadToEnd();

                string [] lines = contents.Split('\n');
                
                
                // loop through each line in the text file
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    string[] tokens = line.Split(' ');
                    GameSquare g; 

                    try
                    {
                        int number = int.Parse(tokens[0]);
                        g = (GameSquare)gameSquares[number];
                        if(g == null)
                        {
                            g = new GameSquare();
                            g.number = number;
                            gameSquares.Add(number, g);
                        }

                        
                    } catch
                    {
                        log("Could not understand line " + (i + 1));
                        continue;
                    }


                    /// TODO: crashes on 16
                    if (tokens.Length <= 1)
                    {
                        log("Square " + g.number + " does not connect to anything");
                        continue;
                    }
                    string connectionsToken = tokens[1];

                    // get all connections for this game square
                    string[] connections = connectionsToken.Split(',');
                    foreach(string connection in connections)
                    {
                        // last character is the type
                        Connection c = new Connection();
                        char connectionType = connection[connection.Length - 1];
                        switch (connectionType)
                        {
                            case 'n':
                                c.connectionType = Connection.ConnectionType.NEXT;
                                break;
                            case 'l':
                                c.connectionType = Connection.ConnectionType.LADDER;
                                break;
                            case 's':
                                c.connectionType = Connection.ConnectionType.SNAKE;
                                break;
                        }

                        // every other character is the connected game square
                        int connectedToInt = int.Parse(connection.Substring(0, connection.Length - 1));
                        c.from = g;

                        // see if gamesquare has been made yet
                        GameSquare connectedTo = (GameSquare)gameSquares[connectedToInt];
                        if(connectedTo == null)
                        {
                            connectedTo = new GameSquare();
                            connectedTo.number = connectedToInt;
                            gameSquares.Add(connectedToInt, connectedTo);

                            log(connectedToInt + " not found yet - creating: " + line);
                        }

                        c.to = connectedTo;
                        log(g.number + " is connected to " + connectedTo + ": " + c.connectionType);
                        g.connections.Add(c);
                    
                        
                    }
                    // log(line);
                }

                //lblStatus.Text = contents;
            }


            GameSquare g1 = (GameSquare)gameSquares[1];
            log(g1.ToString());
            

        }
    }
}
