using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayfairCipher
{
    class MainClass
    {
        static List<char> textList = new List<char>();
        static void initPlainText(string text)
        {
            text = text.ToUpper();
            foreach (var t in text)
            {
                if (t == ' ') continue;
                else if (t == 'J') textList.Add('I');
                textList.Add(t);
            }
        }
        static List<char>  CreatePlainText()
        {
            List<char> tempList = new List<char>();
           
            for (int i = 0; i < textList.Count-1; i++) //compare two char and add X if same
            {
                if (textList[i] == textList[i + 1])
                {
                    tempList.Add(textList[i]);
                    tempList.Add('X');
                    continue;
                }
                else tempList.Add(textList[i]);
                    
            }
            tempList.Add(textList[textList.Count-1]);

            if (tempList.Count % 2 == 1) tempList.Add('X');

            return tempList;
        }

        static List<char> getAlphbt()
        {
            List<char> tempList = new List<char>();
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (i == 'J') continue;
                tempList.Add(i);
            }
            return tempList;
        }
        static List<char> CreateTable(string key)
        {
            key = key.ToUpper();
            List<char> tempList = new List<char>();
            List<char> alphbtList = getAlphbt();
            tempList = key.Distinct().ToList(); // remove duplicates char from key 


            for (int i = 0; i < tempList.Count; i++) //compare key char and alphbet char and remove duplicates char
            {
                for (int j = 0; j < alphbtList.Count; j++)
                {
                    if(tempList[i]==alphbtList[j])
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
        static void EncryptionTable(List<char> table,List<char> plainText)
        {
            int width = 5, hight = 5;
            char[,] tempList = new char[width,hight];

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
            Console.Write("After Encryption: ");

            //Encryption part
            for (int i = 0; i < plainText.Count; i++)
            {
                char  Ch1 = plainText[i]; //first char
                char Ch2 = plainText[i+1];//second char
                int row1=0, row2 = -1,col1 = 0, col2 = -1;

                for (int j = 0; j < width*hight; j++)// you can replace this with insted for loop
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

                //check if first char and scond char on same row
                if(row1==row2)
                {
                    Console.Write(tempList[row1,(col1+1)%hight]+""+tempList[row2, (col2+ 1)% hight]);
                }
                //check if first char and scond char on same colunm
                else if (col1 == col2)
                {
                    Console.Write(tempList[(row1+1)%hight, col1] + "" + tempList[(row2 + 1) % hight, col2]);
                }
                else{
                   Console.Write(tempList[row1, col2] + "" + tempList[row2, col1]);
                 //  Console.Write(tempList[row2, col1] + "" + tempList[row1, col2]); //another slove
                
                }
                i++;
            }
        }
        static void DecryptionTable(List<char> table, List<char> plainText)
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
                    Console.Write(tempList[row1, (col1 -1)%5] + "" + tempList[row2, (col2- 1%5) ]);
                }
                else if (col1 == col2)
                {
                    if (row1 == 0) row1 = 5;
                    if (row2 == 0) row2 = 5;

                    Console.Write(tempList[((row1-1)) %5, col1] + "" + tempList[(row2-1 ) %5, col2]);
                }
                else
                {
                    Console.Write(tempList[row1, col2] + "" + tempList[row2, col1]);
                }
                i++;
            }
        }
        public static void Main(string[] args)
        {
            Console.Write("Enter Plain Text: ");
            string plainText = Console.ReadLine();
            Console.Write("Enter Key: ");
            string key = Console.ReadLine();
            string plainX = "";

            initPlainText(plainText);
            // plainText after add X between same char
            foreach (var item in CreatePlainText())
            {
                plainX += item;
            }
            Console.WriteLine("1- Encryption \n2- Decryption");
            Console.Write("Enter Your Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Plain Text After Add X:"+plainX);
                    EncryptionTable(CreateTable(key), CreatePlainText());
                    break;
                case 2:
                    Console.WriteLine("Plain Text After Add X:" + plainX);
                    DecryptionTable(CreateTable(key), CreatePlainText());
                    break;
                default:
                    break;
            }

            Console.ReadKey();

        }
    }
}
