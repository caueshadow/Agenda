using Agenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole
{
    internal class Program
    {
        static int ExibirMenu()
        {
            int op = 0;
            Console.Clear();
            Console.WriteLine("Agenda");
            Console.WriteLine("\nExibir Contatos - 1");
            Console.WriteLine("Inserir Contato - 2");
            Console.WriteLine("Alterar Contato - 3");
            Console.WriteLine("Excluir Contato - 4");
            Console.WriteLine("Localizar Contato - 5");
            Console.WriteLine("Sair - 6\n");
            Console.Write("Selecione uma Opção: ");
            op = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            return op;
        }

    static void ExibirContatos(String[] nome, String[] email, int tamanho, String[] telefone)
        {
            Console.WriteLine("Exibir Contatos");
            for (int i = 0; i < tamanho; i++)
            {
                Console.WriteLine("Nome: {0}\nEmail: {1}\nTelefone: {2}\n", nome[i], email[i], telefone[i]);
            }
            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
            Console.ReadKey();
        }

        static void InserirContato(ref String[] nome, ref String[] email, ref int tamanho, ref String[] telefone)
        {
            try
            {
                if (tamanho >= 200)
                {
                    Console.WriteLine("Excesso de contato detectado!!!!");
                }
                else
                {
                    Console.WriteLine("Inserir Contato");
                    Console.Write("Nome: ");
                    nome[tamanho] = Console.ReadLine();
                    Console.Write("Email: ");
                    email[tamanho] = Console.ReadLine();
                    Console.Write("Telefone: ");
                    telefone[tamanho] = Console.ReadLine();
                    int pos = LocalizarContato(nome, tamanho, email[tamanho]);
                    if (pos == -1)
                    {
                        tamanho++;
                        Console.WriteLine("Contato inserido.");
                        Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                    }
                    else
                    {
                        Console.WriteLine("Contato já cadastrado.");
                        Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro:" + e.Message);
                Console.ReadKey();
            }

        }

        static void AlterarContato(ref String[] nome, ref String[] email, ref int tamanho, ref String[] telefone)
        {
            try
            {
                Console.WriteLine("Alterar Contato");
                Console.Write("Nome: ");
                string nomeContato = Console.ReadLine();
                int pos = LocalizarContato(nome, tamanho, nomeContato);
                if (pos != -1)
                {
                    Console.WriteLine("Novos dados do Contato");
                    Console.Write("Nome: ");
                    string novoNome = Console.ReadLine();
                    Console.Write("Email: ");
                    string novoEmail = Console.ReadLine();
                    Console.Write("Email: ");
                    string novoTelefone = Console.ReadLine();
                    int posV = LocalizarContato(nome, tamanho, novoEmail);
                    if (posV == -1 || posV == pos)
                    {
                        nome[pos] = novoNome;
                        email[pos] = novoEmail;
                        telefone[pos] = novoTelefone;
                        Console.WriteLine("Contato alterado.");
                        Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                    }
                    else
                    {
                        Console.WriteLine("Já existe um contato com este nome");
                        Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Contato não encontrado");
                    Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro:" + e.Message);
                Console.ReadKey();
            }
        }

        static Boolean ExcluirContato(ref String[] nome, ref String[] email, ref int tamanho, ref String[] telefone, String nomeContato)
        {
            Boolean excluiu = false;
            int pos = -1;
            pos = LocalizarContato(nome, tamanho, nomeContato);
            //[1,5,7,9,9]
            //pos = 1
            if (pos != -1)
            {
                for (int i = pos; i < tamanho - 1; i++)
                {
                    nome[i] = nome[i + 1];
                    email[i] = email[i + 1];
                    telefone[i] = telefone[i + 1];
                }
                excluiu = true;
                tamanho--;
            }
            return excluiu;
        }
        static int LocalizarContato(String[] nome, int tamanho, String nomeContato)
        {
            int pos = -1;
            int i = 0;
            while (i < tamanho && nome[i] != nomeContato)
            {
                i++;
            }
            if (i < tamanho) pos = i;
            return pos;
        }
        static void Main(string[] args)
        {
            //armazana os dados da agenda
            String[] nome = new string[200];
            String[] email = new string[200];
            String[] telefone = new string[200];
            //tamanha lógico
            int tamanho = 0;
            int op = 0;
            int pos = 0;
            String nomeContato = "";

            //carregar dados do arquivo texto
            BackupAgenda.nomeArquivo = "dados.txt";
            BackupAgenda.RestaurarDados(ref nome, ref email, ref tamanho, ref telefone);

            while (op != 6)
            {
                op = ExibirMenu();
                switch (op)
                {
                    case 1:
                        //exibir os contatos
                        ExibirContatos(nome, email, tamanho, telefone);
                        break;
                    case 2:
                        //inserir um contato
                        InserirContato(ref nome, ref email, ref tamanho, ref telefone);
                        break;
                    case 3:
                        //alterar um contato
                        AlterarContato(ref nome, ref email, ref tamanho, ref telefone);
                        break;
                    case 4:
                        //excluir um contato
                        Console.WriteLine("Excluir um contato");
                        Console.Write("Nome: \n");
                        nomeContato = Console.ReadLine();
                        if (ExcluirContato(ref nome, ref email, ref tamanho, ref telefone, nomeContato))
                        {
                            Console.WriteLine("Contado excluido");
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        else
                        {
                            Console.WriteLine("Contado Não encontrado");
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        //localizar um contato
                        Console.WriteLine("Localizar um contato");
                        Console.Write("Nome: ");
                        nomeContato = Console.ReadLine();
                        pos = LocalizarContato(nome, tamanho, nomeContato);
                        if (pos != -1)
                        {
                            Console.WriteLine("\nNome: {0}\nEmail: {1}\nTelefone: {2}\n", nome[pos], email[pos], telefone[pos]);
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado");
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        Console.ReadKey();
                        break;
                }
            }
            //salvar dados no arquivo texto
            BackupAgenda.SalvarDados(ref nome, ref email, ref tamanho, ref telefone);
        }
    }
}