using System;
using System.IO;

namespace C_
{
class Bank
    {
        static void Main(string[] args)
        {
            Console.Title = "***********************SKYBank***********************";
            Console.ForegroundColor = ConsoleColor.Green;
            Password(0);
            Task();
            Console.ReadLine();
        }
        public static class PASSWORD
        {
            public const string Passcode = "231233";
            public const string FileName = "SKYBank.txt";
            public const int amount = 63;
            public const int CorrctLngth = 6;
            public const int CorrctLngthFive = 5;
        }
        static void makeFile(){
 
            switch (File.Exists(PASSWORD.FileName))
            {
                case false:
                    File.Create(PASSWORD.FileName);
                    break;
                default:
                    break;
            }
            return;
        }
        static void Task(){
            information();
            tryAgain();
        }
        static void tryAgain(){
            Console.WriteLine("Another Task Y/N");
            string YrN = Console.ReadLine();
            switch (YrN)
            {
                case "y":
                    Task();
                    break;
                case "n":
                    Console.WriteLine("Bye");
                    break;
                case "Y":
                    Task();
                    break;
                case "N":
                    Console.WriteLine("Bye");
                    break;
                default:
                    Console.WriteLine("Did Not Understand");
                    tryAgain();
                    break;
            }
        }
        static void information(){
            string Read=Totals()[0];
            string dTotal = Totals()[1];
            string bTotal = Totals()[2];
            Console.WriteLine("FUNDS FOR DANNY: "+dTotal+" FUNDS FOR BENNY: "+bTotal);
            Console.WriteLine("ADD NEW FUNDS");
            string date = Date();
            string dadd = Add("danny");
            string dsub = Sub("danny");
            string badd = Add("benny");
            string bsub = Sub("benny");
            dTotal = Convert.ToString(Convert.ToInt32(dTotal) + Convert.ToInt32(dadd) - Convert.ToInt32(dsub));
            if (Convert.ToInt32(dTotal) < 0)
            {
                dTotal = Convert.ToString(Convert.ToInt32(dTotal) + Convert.ToInt32(dsub));
                Console.WriteLine("No Sufficent Funds Action not alowed for danny");
            }
            bTotal = Convert.ToString(Convert.ToInt32(bTotal) + Convert.ToInt32(badd) - Convert.ToInt32(bsub));
            if (Convert.ToInt32(bTotal) < 0)
            {
                bTotal = Convert.ToString(Convert.ToInt32(bTotal) + Convert.ToInt32(bsub));
                Console.WriteLine("No Sufficent Funds Action not alowed for benny");
            }
            dTotal = CorrectionSix(dTotal);
            bTotal = CorrectionSix(bTotal);
            string data=formating(date, dadd, dsub, dTotal, badd, bsub, bTotal);
            Read+=data;
            File.WriteAllText(PASSWORD.FileName, Read);
            File.Encrypt(PASSWORD.FileName);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(Read);
 
        }
        static string CorrectionSix(string ToCorrect){
            string Total = "";
            switch(ToCorrect.Length){
                case PASSWORD.CorrctLngth:
                    break;
                default:
                    int AmntFzero=PASSWORD.CorrctLngth-ToCorrect.Length;
                    
                    for (int i = 0; i < AmntFzero; i++)
                    {
                        Total+="0";
                    }
                    Total+=Convert.ToString(Convert.ToInt32(ToCorrect));
                    break;
            }
            return Total;
        }
        static string CorrectionFive(string ToCorrect){
            string Total = "";
            switch (ToCorrect.Length)
            {
                case PASSWORD.CorrctLngthFive:
                    break;
                default:
                    int AmntFzero = PASSWORD.CorrctLngthFive - ToCorrect.Length;
 
                    for (int i = 0; i < AmntFzero; i++)
                    {
                        Total += "0";
                    }
                    Total += ToCorrect;
                    break;
            }
            return Total;
        }
        static string Date(){
            Console.WriteLine("Enter 8 character Date:");
            string date = Console.ReadLine();
            switch (date.Length){
                case 8:
                    break;
                default:
                    Date();
                    break;
            }
            return date;
        }
        static string Add(string Name){
            Console.WriteLine("Enter Add for " + Name+ " :");
            string add = Console.ReadLine();
            switch (add.Length)
            {
                case 5:
                    break;
                default:
                if(add.Length>PASSWORD.CorrctLngthFive){
                    Add(Name);
                }
                    add = CorrectionFive(add);
                    break;
            }
            return add;
        }
        static string Sub(string Name){
            Console.WriteLine("Enter Sub " + Name + " :");
            string sub = Console.ReadLine();
            switch (sub.Length)
            {
                case 5:
                    break;
                default:
                    if (sub.Length > PASSWORD.CorrctLngthFive)
                    {
                        Sub(Name);
                    }
                    sub = CorrectionFive(sub);
                    break;
            }
            return sub;
        }
        static string formating(string Date, string dadd, string dsub, string dTotal, string badd, string bsub, string bTotal){
            string data = "\n" + "|" + Date + "|" + "+" + dadd + "|" + "-" + dsub + "|" + "D" + dTotal + "|" + Date + "|" + "+" + badd + "|" + "-" + bsub + "|" + "B" + bTotal + "|" + "\n";
            for (int i = 0; i < PASSWORD.amount; i++)
            {
                data+="-";
            }
            return data;
 
        }
        static string[] Totals(){
            FileStream fs =  File.Open(PASSWORD.FileName, FileMode.OpenOrCreate);
            fs.Close();
            string dTotal = "";
            string bTotal = "";
            File.Decrypt(PASSWORD.FileName);
            string Read = File.ReadAllText(PASSWORD.FileName);
            switch (Read.Length)
            {
                case 0:
                    dTotal = "0";
                    bTotal = "0";
                    break;
                default:
                    for (int i = Read.Length-1; i > Read.Length-(PASSWORD.amount * 2); i--)
                    {
                        switch (Read[i])
                        {
                            case 'D':
                                for (int j = 1; j < 7; j++)
                                {
                                    dTotal += Read[i + j];
                                    
                                }
                                break;
                            case 'B':
                                for (int j = 1; j < 7; j++)
                                {
                                    bTotal += Read[i + j];
                                }
                                break;
                        }
                    }
                    break;
            }
            string[] INFO = {Read, dTotal, bTotal};
            return INFO;
            
        }
        static void Password(int i){
            Console.WriteLine("ENTER PASSWORD");
            string PassCodeP=Console.ReadLine();
            switch (i)
            {
                case 5:
                    Console.WriteLine("WRONG     NOW CLOSING...");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
            }
            switch (PassCodeP)
            {
                case PASSWORD.Passcode:
                    Console.WriteLine("CORRECT");
                    break;
                
                default:
                    Console.WriteLine("WRONG");
                    Password(i+=1);
                    break;
            }
        }
    }
}