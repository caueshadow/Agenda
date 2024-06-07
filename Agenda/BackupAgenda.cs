using System;
using System.Collections.Generic;
using System.IO;

namespace AgendaConsole
{
    internal class BackupAgenda
    {
        public static string nomeArquivo = "dados.txt";

        public static void SalvarDados(ref string[] nome, ref string[] email, ref int tamanho, ref List<string>[] telefones)
        {
            using (StreamWriter sr = new StreamWriter(nomeArquivo))
            {
                for (int i = 0; i < tamanho; i++)
                {
                    sr.WriteLine(nome[i] + "§" + email[i] + "§" + string.Join("|", telefones[i]));
                }
            }
        }

        public static void RestaurarDados(ref string[] nome, ref string[] email, ref int tamanho, ref List<string>[] telefones)
        {
            tamanho = 0;
            try
            {
                using (StreamReader sr = new StreamReader(nomeArquivo))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        int pos = line.IndexOf('§');
                        int pos2 = line.IndexOf('§', pos + 1);

                        // Verifica se ambos os delimitadores são encontrados
                        if (pos != -1 && pos2 != -1)
                        {
                            nome[tamanho] = line.Substring(0, pos);
                            email[tamanho] = line.Substring(pos + 1, pos2 - pos - 1);
                            string telefonesStr = line.Substring(pos2 + 1);
                            telefones[tamanho] = new List<string>(telefonesStr.Split('|'));

                            tamanho++;
                        }

                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao restaurar dados: " + e.Message);
            }
        }
    }
}
