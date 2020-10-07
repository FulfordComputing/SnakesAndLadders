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
                foreach (string line in lines)
                {
                    string[] tokens = line.Split(' ');
                    int number = int.Parse(tokens[0]);

                    /// TODO: crashes on 16
                    string connectionsToken = tokens[1];

                    // game square already loaded
                    if (gameSquares.ContainsKey(number))
                    {

                    } else
                    {
                        GameSquare g = new GameSquare();
                        g.number = number;
                        gameSquares.Add(number, g);

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
                            string connectedTo = connection.Substring(0, connection.Length - 1);
                            log(g.number + " is connected to " + connectedTo + ": " + c.connectionType);
                        }
                        
                    }
                    log(line);
                }

                //lblStatus.Text = contents;
            }


            /*GameSquare g1 = new GameSquare();
            g1.number = 1;

            GameSquare g2 = new GameSquare();
            g2.number = 2;

            Connection c = new Connection();
            c.from = g1;
            c.to = g2;
            c.connectionType = Connection.ConnectionType.NEXT;

            g1.connections.Add(c);*/

        }
    }
}
