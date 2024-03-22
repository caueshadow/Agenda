namespace Agenda
{
    internal class Program
    {
        static void ler(string arq)
        {
            using var ler = new StreamReader(arq);
            string line;
            while ((line = ler.ReadLine()) != "-")
            {
                Console.WriteLine(line);
            }
        }

        static void escrever(string arq, string line)
        {
            StreamWriter escrever = new StreamWriter(arq);
            string nome;
            Console.WriteLine("Escreva o nome");
            nome = Console.ReadLine();
            escrever.WriteLine(nome);
            escrever.WriteLine("-");
            escrever.Close();
        }
        static void Main(string[] args)
        {
            //Y:\aa.txt
            String arq;
            Console.WriteLine("Informe o caminho do arquivo");
            arq = Console.ReadLine();
            //escrever(arq);
            ler(arq);
        }
    }
}
