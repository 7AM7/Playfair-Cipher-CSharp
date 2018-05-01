using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayfairCipherWin
{
    public partial class Form1 : Form
    {
        int counter,i,z=0;
        string key;
        string text = "";
        int width = 5, hight = 5;
        char[,] tempList;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        public Form1()
        {
            tempList = new char[width, hight];
            counter =i=0;
            key = "";
            InitializeComponent();
        }

        async void Wait(int n)
        {
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(n));
        }
        static List<char> textList = new List<char>();
        void initPlainText(string text)
        {
            text = text.ToUpper();
            foreach (var t in text)
            {
                if (t == ' ') continue;
                else if (t == 'J') textList.Add('I');
                textList.Add(t);
            }
        }
        List<char> CreatePlainText()
        {
            List<char> tempList = new List<char>();

            for (int i = 0; i < textList.Count - 1; i++) //compare two char and add X if same
            {
                if (textList[i] == textList[i + 1])
                {
                    tempList.Add(textList[i]);
                    tempList.Add('X');
                    continue;
                }
                else tempList.Add(textList[i]);

            }
            tempList.Add(textList[textList.Count - 1]);

            if (tempList.Count % 2 == 1) tempList.Add('X');

            return tempList;
        }

        List<char> getAlphbt()
        {
            List<char> tempList = new List<char>();
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (i == 'J') continue;
                tempList.Add(i);
            }
            return tempList;
        }
        List<char> CreateTable(string key)
        {
            key = key.ToUpper();
            List<char> tempList = new List<char>();
            List<char> alphbtList = getAlphbt();
            tempList = key.Distinct().ToList(); // remove duplicates char from key 


            for (int i = 0; i < tempList.Count; i++) //compare key char and alphbet char and remove duplicates char
            {
                for (int j = 0; j < alphbtList.Count; j++)
                {
                    if (tempList[i] == alphbtList[j])
                    {
                        alphbtList.Remove(alphbtList[j]);
                    }
                }
            }
            foreach (var item in alphbtList)
            {
                tempList.Add(item);
            }
            return tempList;
        }
        string EncryptionTable(List<char> table, List<char> plainText)
        {
            return "";
        }
        void DecryptionTable(List<char> table, List<char> plainText)
        {
            int width = 5, hight = 5;
            char[,] tempList = new char[width, hight];

            int z = 0;
            //convert one dimensional array to two dimensional array
            for (int i = 0; i < width * hight; i++) // you can replace this with insted for loop
            {

                tempList[i / width, i % hight] = table[z];
                z++;
            }

            //print Table in two dimensional array
            for (int i = 0; i < width * hight; i++) // you can replace this with insted for loop
            {
                if (i % 5 == 0) Console.WriteLine();
                Console.Write(tempList[i / width, i % hight] + "  ");
            }

            Console.WriteLine();
            Console.Write("After Decryption: ");
            for (int i = 0; i < plainText.Count; i++)
            {
                char Ch1 = plainText[i]; //first char
                char Ch2 = plainText[i + 1];//second char
                int row1 = 0, row2 = -1, col1 = 0, col2 = -1;

                for (int j = 0; j < width * hight; j++)// you can replace this with insted for loop
                {

                    if (tempList[j / width, j % hight] == Ch1) // get first char row index and colunm index
                    {
                        row1 = j / width;
                        col1 = j % hight;
                    }
                    if (tempList[j / width, j % hight] == Ch2) // get second char row index and colunm index
                    {
                        row2 = j / width;
                        col2 = j % hight;
                    }
                }

                if (row1 == row2)
                {
                    if (col1 == 0) col1 = 5;
                    if (col2 == 0) col2 = 5;
                    Console.Write(tempList[row1, (col1 - 1) % 5] + "" + tempList[row2, (col2 - 1 % 5)]);
                }
                else if (col1 == col2)
                {
                    if (row1 == 0) row1 = 5;
                    if (row2 == 0) row2 = 5;

                    Console.Write(tempList[((row1 - 1)) % 5, col1] + "" + tempList[(row2 - 1) % 5, col2]);
                }
                else
                {
                    Console.Write(tempList[row1, col2] + "" + tempList[row2, col1]);
                }
                i++;
            }
        }

        void ResetData()
        {
            timer = new System.Windows.Forms.Timer();
            timer2 = new System.Windows.Forms.Timer();
            tempList = new char[width, hight];
            counter = i =  0;
            key = "";
            plainxLable.Text = "PlainX : ";
            tempList = new char[width, hight];
        }
        private void encBtn_Click(object sender, EventArgs e)
        {
            ResetData();
            string plainText = plainTextbox.Text;
            key = keyTextbox.Text;
            string plainX = "";
            if (string.IsNullOrEmpty(plainText) || (string.IsNullOrEmpty(key)))
                return;


            initPlainText(plainText);
            foreach (var item in CreatePlainText())
            {
                plainX += item;
            }
            //plainxLable.Text += plainX;

            timer.Interval = (1 * 300);
            timer.Tick += new EventHandler(Timer_Tick1);
            timer.Start();

        }

        private void Timer_Tick1(object sender, EventArgs e)
        {
            timer.Stop();
            label4.Text = "Create Table Process...";
            textBox1.Text = "";

            Button[] btn = {button1,button2,button3,button4,button5,
            button6,button7,button8,button9,button10,
            button11,button12,button13,button14,button15,
            button16,button17,button18,button19,button20,
            button21,button22,button23,button24,button25,};
            if (counter >= 25)
            { 
                timer.Stop();
                timer.Enabled = false;
                for (int i = 0; i < width * hight; i++) // you can replace this with insted for loop
                {
                    tempList[i / width, i % hight] = CreateTable(key)[z];
                    z++;
                }
                timer2.Interval = (4 * 1000);
                timer2.Tick += new EventHandler(Timer2_Tick);
                timer2.Start();

                //textBox1.Text = EncryptionTable(CreateTable(key), CreatePlainText());
                return;
            }
            btn[counter].Text = Convert.ToString(CreateTable(key)[counter]);
            counter++;
            timer.Enabled = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        void ResetButtonColor()
        {
            Button[] btn = {button1,button2,button3,button4,button5,
            button6,button7,button8,button9,button10,
            button11,button12,button13,button14,button15,
            button16,button17,button18,button19,button20,
            button21,button22,button23,button24,button25,};
            for (int q = 0; q < 25; q++)
            {
                btn[q].BackColor = Color.Gainsboro;

            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            label4.Text = "Encryption Text Process...";
            if (i >= CreatePlainText().Count)
            {
                timer2.Stop();
                timer2.Enabled = false;
                ResetButtonColor();
                MessageBox.Show("Encryption Finished!","Playfair");
                label4.Text = "Encryption Finished!";
                return;
            }


            Button[] btn = {button1,button2,button3,button4,button5,
            button6,button7,button8,button9,button10,
            button11,button12,button13,button14,button15,
            button16,button17,button18,button19,button20,
            button21,button22,button23,button24,button25,};
            ResetButtonColor();
            char Ch1 = CreatePlainText()[i]; //first char
            char Ch2 = CreatePlainText()[i + 1];//second char
            int [] btnIndex = new int[2];
            for (int i = 0; i < btn.Length; i++)
            {
                if(btn[i].Text==Convert.ToString(Ch1))
                {
                    btnIndex[0] = i;
                }
                if (btn[i].Text == Convert.ToString(Ch2))
                {
                    btnIndex[1] = i;
                }
            }
                btn[btnIndex[0]].BackColor = Color.Red;
                btn[btnIndex[1]].BackColor = Color.Red;
                int row1 = 0, row2 = -1, col1 = 0, col2 = -1;

                for (int j = 0; j < width * hight; j++)// you can replace this with insted for loop
                {

                    if (tempList[j / width, j % hight] == Ch1) // get first char row index and colunm index
                    {
                        row1 = j / width;
                        col1 = j % hight;
                    }
                    if (tempList[j / width, j % hight] == Ch2) // get second char row index and colunm index
                    {
                        row2 = j / width;
                        col2 = j % hight;
                    }
                }
                


                int n = width;
            //check if first char and scond char on same row
            if (row1 == row2)
                {
                if (btn[btnIndex[0]].Text == btn[(row1 * n) + (col1 + 1) % hight].Text||
                    btn[btnIndex[1]].Text == btn[(row1 * n) + (col1 + 1) % hight].Text
                    )
                {
                    btn[(row1 * n) + (col1 + 1) % hight].BackColor = Color.Yellow;
                }
                else
                    btn[(row1 * n) + (col1 + 1) % hight].BackColor = Color.Blue;

                if (btn[btnIndex[0]].Text == btn[(row2 * n) + (col2 + 1) % hight].Text ||
    btn[btnIndex[1]].Text == btn[(row2 * n) + (col2 + 1) % hight].Text
    )
                {
                    btn[(row1 * n) + (col1 + 1) % hight].BackColor = Color.Yellow;
                }
                else
                    btn[(row2 *n)+ (col2 + 1) % hight].BackColor = Color.Blue;
                    text += (tempList[row1, (col1 + 1) % hight] + "" + tempList[row2, (col2 + 1) % hight]);
                }
                //check if first char and scond char on same colunm
                else if (col1 == col2)
                {
                if (btn[btnIndex[0]].Text == btn[(((row2 + 1) % hight) * n) + col2].Text ||
    btn[btnIndex[1]].Text == btn[(((row2 + 1) % hight) * n) + col2].Text
    )
                {
                    btn[(((row2 + 1) % hight) * n) + col2].BackColor = Color.Yellow;
                }
                else
                    btn[(((row2 + 1) % hight) * n) + col2].BackColor = Color.Blue;

                if (btn[btnIndex[0]].Text == btn[(((row2 + 1) % hight) * n) + col1].Text ||
    btn[btnIndex[1]].Text == btn[(((row2 + 1) % hight) * n) + col1].Text
    )
                {
                    btn[(((row2 + 1) % hight) * n) + col1].BackColor = Color.Yellow;
                }
                else
                    btn[(((row2 + 1) % hight) * n) + col1].BackColor = Color.Blue;


                
                    text += (tempList[(row1 + 1) % hight, col1] + "" + tempList[ (row2 + 1) % hight, col2]);
                }
                else
                {
                    btn[(row1*n) + col2].BackColor = Color.Blue;
                    btn[(row2*n) + col1].BackColor = Color.Blue;
                    text += (tempList[row1, col2] + "" + tempList[row2, col1]);
                    //  Console.Write(tempList[row2, col1] + "" + tempList[row1, col2]); //another slove

                }
            
            textBox1.Text = text;
            plainxLable.Text += Ch1+""+Ch2;
            i +=2;
            timer2.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {



            Button[] btn = {button1,button2,button3,button4,button5,
button6,button7,button8,button9,button10,
button11,button12,button13,button14,button15,
button16,button17,button18,button19,button20,
button21,button22,button23,button24,button25,};
            for (int i = 0; i < 25; i++)
            {
                btn[i].Text = "";
                btn[i].BackColor = Color.Gainsboro;

            }
        }
    }
}
