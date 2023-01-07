using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Menu_cadastro_clientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string idade;
            public string email;
            public string cpf;
        }
        static List<Cliente> clientes = new List<Cliente>();

        enum Menu {Adicionar = 1, Cadastrados, Remover, Sair}
        static void Main(string[] args)
        {
            carregar();
            bool fecharMenu = false;
            while (!fecharMenu)
            {
                Console.WriteLine("Seja bem vindo ao banco de dados: \n");
                Console.WriteLine("1-Adicionar \n2-Cadastrados \n3-Remover \n4-Sair");
                int selectNumb = int.Parse(Console.ReadLine());
                Menu opcoes = (Menu)selectNumb;
                Console.WriteLine($"{opcoes} \n");


                switch (opcoes)
                {

                    case Menu.Adicionar:
                        adicionar();
                        break;

                    case Menu.Cadastrados:
                        cadastro();
                        break;

                    case Menu.Remover:
                        remover();
                        break;

                    case Menu.Sair:
                        fecharMenu = true;
                        break;
                }
                Console.Clear();
            }

            void adicionar()
            {
                Cliente cliente = new Cliente();
                Console.WriteLine("Cadastro de clientes \n");
                Console.WriteLine("Nome do cliente: ");
                cliente.nome = Console.ReadLine();
                Console.WriteLine("Idade do cliente");
                cliente.idade = Console.ReadLine();
                Console.WriteLine("Email do cliente");
                cliente.email = Console.ReadLine();
                Console.WriteLine("Cpf do cliente");
                cliente.cpf = Console.ReadLine();

                clientes.Add(cliente);
                salvar();

                Console.WriteLine("Cadastro concluido! Aperte Enter para sair: ");
                Console.ReadLine();
            }

            void cadastro()
            {
                if(clientes.Count > 0)
                {
                    Console.WriteLine("Lista de cadastro: \n");
                    int i = 0;
                    foreach (Cliente cliente in clientes)
                    {
                        Console.WriteLine($"ID: {i}");
                        Console.WriteLine($"Nome: {cliente.nome}");
                        Console.WriteLine($"Idade: {cliente.idade}");
                        Console.WriteLine($"Email: {cliente.email}");
                        Console.WriteLine($"Cpf: {cliente.cpf}");
                        Console.WriteLine("================================ \n");
                        i++;
                    }              
                }
                else
                {
                    Console.WriteLine("Nenhum cadastro encontrado");
                }
                Console.WriteLine("Aperte ENTER para sair");
                Console.ReadLine();
            }

            void salvar()
            {
                FileStream stream = new FileStream("Clientes.dat", FileMode.OpenOrCreate);
                BinaryFormatter encoder = new BinaryFormatter();

                encoder.Serialize(stream, clientes);
                stream.Close();
            }

            void carregar()
            {
                FileStream stream = new FileStream("Clientes.dat", FileMode.OpenOrCreate);
                try
                {
                    BinaryFormatter encoder = new BinaryFormatter();

                    clientes = (List<Cliente>)encoder.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception)
                {

                }
                stream.Close();
            }

            void remover()
            {
                cadastro();
                Console.WriteLine("Digite o ID do cliente que queira deletar:");
                int id = int.Parse(Console.ReadLine());

                if(id >= 0 && id < clientes.Count)
                {
                    clientes.RemoveAt(id);
                    salvar();
                }
                else
                {
                    Console.WriteLine("ID não encontrado, Tente novamente!");
                }
            }
        }
    }
}
