using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    internal class BackupAgenda
    {
        public static String nomeArquivo = "dados.txt";
        public static void SalvarDados(ref String[] nome, ref String[] email, ref int tamanho, ref String[] telefone)
        {
            StreamWriter sr = new StreamWriter(nomeArquivo);
            for (int i = 0; i < tamanho; i++)
            {
                sr.WriteLine(nome[i] + "-" + email[i] + "-" + telefone[i]);
            }
            sr.Close();
        }
        public static void RestaurarDados(ref String[] nome, ref String[] email, ref int tamanho, ref String[] telefone)
        {
            tamanho = 0;
            int pos = 0;
            int pos2 = 0;

            StreamReader sr = new StreamReader(nomeArquivo);
            string line = sr.ReadLine();

            while(line != null)
            {
                //pos vai armazenar a posiçao em que achou o caracter
                pos = line.IndexOf("-");
                pos2 = line.IndexOf("-", pos+1);
                nome[tamanho] = line.Substring(0, pos);
                email[tamanho] = line.Substring(pos + 1, pos);
                telefone[tamanho] = line.Substring(pos2+1);
                tamanho++;
                line = sr.ReadLine();
            }
            sr.Close ();
        }
    }
}
