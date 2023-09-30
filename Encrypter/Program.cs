
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ionic.Zip;

namespace Encrypter
{
    internal class Program
    {
       public static string Passcrypt(string password)
        {
            using(SHA256 sha = SHA256.Create())
            {

               
                return Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));



            }





        }
        public static void Encrypt(string filepath , string password, string outputpath)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password =Passcrypt(password);

                    zip.AddFile(filepath, "");


                    zip.Save(outputpath);
                    Console.WriteLine("Succesfully Encrypted!");
                    Console.WriteLine("Please Check Your Selected Folder...");
                    Console.ReadLine();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured when trying to encrypt selected file!");
                Console.ReadLine();

            }
          



        }

        public static void  Decrypt(string filepath , string password, string outputpath)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(filepath))
                {

                    zip.Password = Passcrypt(password);
                    zip.ExtractAll(outputpath,ExtractExistingFileAction.OverwriteSilently);
                    Console.WriteLine("Succesfully Decrypted!");
                    Console.WriteLine("Please Check Your Selected Folder...");
                    Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured when trying to decrypt selected file!");
                Console.ReadLine();
            }
          

        }
        [STAThread]
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.Title = "FileDataCrypter";
                Console.ForegroundColor = ConsoleColor.Yellow;


                Console.WriteLine("Welcome to the data encrypt-decrypt tool!");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1 : Encrypt");
                Console.WriteLine("2 : Decrypt");
                string command = Console.ReadLine();

                if (command == "1")
                {
                    using(OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.ShowDialog();
                        if (dialog.FileName != null)
                        {
                            Console.WriteLine("Please Enter File Password : ");
                            string pass = Console.ReadLine();

                            Encrypt(dialog.FileName,pass,Path.GetDirectoryName(dialog.FileName) + "\\(Enc)" + dialog.SafeFileName );
                           
                        }


                    }
                }
                if (command == "2")
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.ShowDialog();
                        if (dialog.FileName != null)
                        {
                            Console.WriteLine("Please Enter File Password : ");
                            string pass = Console.ReadLine();

                            Decrypt(dialog.FileName, pass, Path.GetDirectoryName(dialog.FileName));
                           
                           
                        }


                    }
                }
               
            }






        }
    }
}
