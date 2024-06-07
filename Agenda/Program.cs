using System;
using System.Collections.Generic;
using System.IO;

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
            try
            {
                op = Convert.ToByte(Console.ReadLine());
                if (op == 6)
                {
                    Console.WriteLine("\nVocê tem certeza que quer sair?!");
                    Console.WriteLine("Pressione 1 para sair e 2 para ficar");
                    int num = Convert.ToByte(Console.ReadLine());
                    if (num == 1)
                    {
                        return op;
                    }
                    else if (num == 2)
                    {
                        op = 0;
                        num = 0;
                        ExibirMenu();
                    }
                    else
                    {
                        Console.WriteLine("Opção Invalida, verifique as opções do menu!");
                        Console.ReadKey();
                    }
                }
                if (op > 6)
                {
                    Console.WriteLine("Opção Invalida, verifique as opções do menu!");
                    Console.ReadKey();
                }
            }
            catch
            {
                Console.WriteLine("Opção Invalida, verifique as opções do menu!");
                Console.ReadKey();
            }
            Console.Clear();
            return op;
        }

        static void ExibirContatos(string[] nome, string[] email, int tamanho, List<string>[] telefones)
        {
            Console.WriteLine("Exibir Contatos\n");
            for (int i = 0; i < tamanho; i++)
            {
                Console.WriteLine("Nome: {0}", nome[i]);
                Console.WriteLine("Email: {0}", string.IsNullOrEmpty(email[i]) ? "Não informado" : email[i]);
                Console.WriteLine("Telefones: {0}", string.Join(", ", telefones[i]));
                Console.WriteLine();
            }
            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
            Console.ReadKey();
        }

        static void InserirContato(ref string[] nome, ref string[] email, ref int tamanho, ref List<string>[] telefones)
        {
            try
            {
                if (tamanho >= 200)
                {
                    Console.WriteLine("Excesso de contato detectado!!!!");
                    return;
                }

                Console.WriteLine("Inserir Contato\n");
                Console.WriteLine("Nome: ");
                nome[tamanho] = Convert.ToString(Console.ReadLine());
                while (string.IsNullOrEmpty(nome[tamanho]))
                {
                    Console.WriteLine("Nome vazio, digite um nome\n");
                    Console.WriteLine("Nome: ");
                    nome[tamanho] = Convert.ToString(Console.ReadLine());
                }

                Console.WriteLine("Email (opcional): ");
                email[tamanho] = Console.ReadLine();
                if (!string.IsNullOrEmpty(email[tamanho]))
                {
                    int arroba = email[tamanho].IndexOf("@");
                    string dominio = email[tamanho].Substring(arroba + 1);

                    while (arroba == -1 || arroba == 0 || !EmailValido(dominio))
                    {
                        Console.WriteLine("Email invalido, digite um email existente com domínio gmail.com, outlook.com ou hotmail.com\n");
                        Console.WriteLine("Email (opcional): ");
                        email[tamanho] = Console.ReadLine();
                        if (string.IsNullOrEmpty(email[tamanho]))
                            break;
                        arroba = email[tamanho].IndexOf("@");
                        dominio = email[tamanho].Substring(arroba + 1);
                    }
                }

                telefones[tamanho] = new List<string>();
                bool adicionarOutroTelefone;
                do
                {
                    Console.WriteLine("Telefone: ");
                    string telefone = Console.ReadLine();

                    while (!TelefoneValido(telefone) || TelefoneDuplicado(telefones, tamanho, telefone))
                    {
                        if (!TelefoneValido(telefone))
                        {
                            Console.WriteLine("Telefone inválido. Deve conter apenas números e ter entre 8 e 11 dígitos.");
                        }
                        else
                        {
                            Console.WriteLine("Telefone já cadastrado. Digite outro telefone.");
                        }
                        telefone = Console.ReadLine();
                    }

                    telefones[tamanho].Add(telefone);

                    Console.WriteLine("Deseja adicionar outro telefone? (S/N)");
                    adicionarOutroTelefone = Console.ReadLine().Trim().ToUpper() == "S";

                } while (adicionarOutroTelefone);

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
            catch (Exception e)
            {
                Console.WriteLine("Erro:" + e.Message);
                Console.ReadKey();
            }
        }

        static bool TelefoneValido(string telefone)
        {
            if (telefone.Length < 8 || telefone.Length > 11)
                return false;

            foreach (char c in telefone)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        static bool EmailValido(string dominio)
        {
            return dominio.Equals("gmail.com", StringComparison.OrdinalIgnoreCase) ||
                   dominio.Equals("outlook.com", StringComparison.OrdinalIgnoreCase) ||
                   dominio.Equals("hotmail.com", StringComparison.OrdinalIgnoreCase);
        }

        static bool TelefoneDuplicado(List<string>[] telefones, int tamanho, string telefoneInput)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (telefones[i].Contains(telefoneInput))
                {
                    return true;
                }
            }
            return false;
        }

        static void AlterarContato(ref string[] nome, ref string[] email, ref int tamanho, ref List<string>[] telefones)
        {
            try
            {
                Console.WriteLine("Alterar Contato\n");
                Console.Write("Nome: ");
                string nomeContato = Console.ReadLine();
                int pos = LocalizarContato(nome, tamanho, nomeContato);
                if (pos != -1)
                {
                    Console.WriteLine("Novos dados do Contato");
                    Console.Write("Nome: ");
                    string novoNome = Console.ReadLine();
                    Console.Write("Email (opcional): ");
                    string novoEmail = Console.ReadLine();

                    if (!string.IsNullOrEmpty(novoEmail))
                    {
                        int arroba = novoEmail.IndexOf("@");
                        string dominio = novoEmail.Substring(arroba + 1);

                        while (arroba == -1 || arroba == 0 || !EmailValido(dominio))
                        {
                            Console.WriteLine("Email invalido, digite um email existente com domínio gmail.com, outlook.com ou hotmail.com\n");
                            Console.WriteLine("Email (opcional): ");
                            novoEmail = Console.ReadLine();
                            if (string.IsNullOrEmpty(novoEmail))
                                break;
                            arroba = novoEmail.IndexOf("@");
                            dominio = novoEmail.Substring(arroba + 1);
                        }
                    }

                    nome[pos] = novoNome;
                    email[pos] = novoEmail;

                    Console.WriteLine("Telefones atuais: {0}", string.Join(", ", telefones[pos]));
                    Console.WriteLine("Deseja alterar os telefones? (S/N)");
                    if (Console.ReadLine().Trim().ToUpper() == "S" || Console.ReadLine().Trim().ToUpper() == "s")
                    {
                        telefones[pos].Clear();
                        bool adicionarOutroTelefone;
                        do
                        {
                            Console.WriteLine("Telefone: ");
                            string telefone = Console.ReadLine();

                            while (!TelefoneValido(telefone) || TelefoneDuplicado(telefones, tamanho, telefone))
                            {
                                if (!TelefoneValido(telefone))
                                {
                                    Console.WriteLine("Telefone inválido. Deve conter apenas números e ter entre 8 e 11 dígitos.");
                                }
                                else
                                {
                                    Console.WriteLine("Telefone já cadastrado. Digite outro telefone.");
                                }
                                telefone = Console.ReadLine();
                            }

                            telefones[pos].Add(telefone);

                            Console.WriteLine("Deseja adicionar outro telefone? (S/N)");
                            adicionarOutroTelefone = Console.ReadLine().Trim().ToUpper() == "S";

                        } while (adicionarOutroTelefone);
                    }

                    Console.WriteLine("Contato alterado.");
                    Console.WriteLine("\n\nPressione qualquer tecla para retornar");
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

        static bool ExcluirContato(ref string[] nome, ref string[] email, ref int tamanho, ref List<string>[] telefones, string nomeContato)
        {
            bool excluiu = false;
            int pos = LocalizarContato(nome, tamanho, nomeContato);
            if (pos != -1)
            {
                for (int i = pos; i < tamanho - 1; i++)
                {
                    nome[i] = nome[i + 1];
                    email[i] = email[i + 1];
                    telefones[i] = telefones[i + 1];
                }
                nome[tamanho - 1] = null;
                email[tamanho - 1] = null;
                telefones[tamanho - 1] = null;
                tamanho--;
                excluiu = true;
            }
            return excluiu;
        }

        static int LocalizarContato(string[] nome, int tamanho, string nomeContato)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (string.Equals(nome[i], nomeContato, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        static void Main(string[] args)
        {
            string[] nome = new string[200];
            string[] email = new string[200];
            List<string>[] telefones = new List<string>[200];
            int tamanho = 0;
            int op = 0;

            // Verifica se o arquivo existe, se não existir, cria
            if (!File.Exists("dados.txt"))
            {
                using (FileStream fs = File.Create("dados.txt")) ;
            }
            //carregar dados do arquivo texto
            BackupAgenda.nomeArquivo = "dados.txt";
            BackupAgenda.RestaurarDados(ref nome, ref email, ref tamanho, ref telefones);

            while (op != 6)
            {
                op = ExibirMenu();
                switch (op)
                {
                    case 1:
                        //exibir os contatos
                        ExibirContatos(nome, email, tamanho, telefones);
                        break;
                    case 2:
                        //inserir um contato
                        InserirContato(ref nome, ref email, ref tamanho, ref telefones);
                        break;
                    case 3:
                        //alterar um contato
                        AlterarContato(ref nome, ref email, ref tamanho, ref telefones);
                        break;
                    case 4:
                        //excluir um contato
                        Console.WriteLine("Excluir um contato\n");
                        Console.Write("Nome: \n");
                        string nomeContato = Console.ReadLine();
                        if (ExcluirContato(ref nome, ref email, ref tamanho, ref telefones, nomeContato))
                        {
                            Console.WriteLine("Contato excluido");
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        else
                        {
                            Console.WriteLine("Contato Não encontrado");
                            Console.WriteLine("\n\nPressione qualquer tecla para retornar");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        //localizar um contato
                        Console.WriteLine("Localizar um contato\n");
                        Console.Write("Nome: ");
                        nomeContato = Console.ReadLine();
                        int pos = LocalizarContato(nome, tamanho, nomeContato);
                        if (pos != -1)
                        {
                            Console.WriteLine("\nNome: {0}\nEmail: {1}\nTelefones: {2}\n", nome[pos], string.IsNullOrEmpty(email[pos]) ? "Não informado" : email[pos], string.Join(", ", telefones[pos]));
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
            BackupAgenda.SalvarDados(ref nome, ref email, ref tamanho, ref telefones);
        }
    }
}
