using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoSoft
{
    class Xor
    {
        public string cle { get; set; }
        public Xor(string cle)
        {
            this.cle = cle;
        }

        public void faire_XOR(string mot)
        {
            int keyposition = 0;
            byte[] entre = File.ReadAllBytes(mot);
            byte[] sortie = new byte[entre.Length];
          
            for (int i = 0; i< entre.Length; i++)
            {
                string  pos = cle.Substring(keyposition, 1);
                
                sortie[i] = (byte)(int)(entre[i] ^ Convert.ToInt32(pos));
             
                if (keyposition == cle.Length-1)
                {
                    keyposition = 0;
                }
                else
                {
                    keyposition+=1;
                   
                }
            }
            
            File.WriteAllBytes(mot, sortie);

        }

     
    }
}


