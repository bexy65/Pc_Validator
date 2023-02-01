using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace Pc_Validator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("C:\\Users\\bexy\\source\\repos\\Pc_Validator\\Pc_Validator\\pc-store-inventory.json");
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonString);

            //Checking the inventory
            if (myDeserializedClass == null)
            {
                Console.WriteLine("No items in inventory!");
            };

            string[] componentCategory = { "CPUs", "Motherboards", "Memory" };

            Console.WriteLine("Welcome to PC configurator!");
            Console.WriteLine();

            //Listing items in inventory to client
            for(int i = 0;i < componentCategory.Length; i++)
            {
                Console.WriteLine($"\n{componentCategory[i]}");
                if (componentCategory[i] == "CPUs") 
                { 
                    foreach (var cpu in myDeserializedClass.CPUs)
                    {
                        Console.WriteLine($"Name:{cpu.Name} - {cpu.Socket}, {cpu.SupportedMemory} \n Price:{cpu.Price}$");

                    };
                }

                else if (componentCategory[i] == "Motherboards")
                {
                    foreach (var board in myDeserializedClass.Motherboards)
                    {
                        Console.WriteLine($"Name:{board.Name} - {board.Socket} \n Price:{board.Price}$");

                    };
                }

                else if (componentCategory[i] == "Memory")
                {
                    foreach (var memory in myDeserializedClass.Memory)
                    {
                        Console.WriteLine($"Name:{memory.Name} - {memory.Type} \n Price:{memory.Price}$");

                    };
                }

            }


        }

    }
    public abstract class Components
    {
        public string ComponentType { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }

    public class CPU : Components
    {
        public string SupportedMemory { get; set; }
        public string Socket { get; set; }
       
    }

    public class Memory : Components
    {
        public string Type { get; set; }
    }

    public class Motherboard : Components
    {
        public string Socket { get; set; }
    }

    public class Root
    {
        public List<CPU> CPUs { get; set; }
        public List<Memory> Memory { get; set; }
        public List<Motherboard> Motherboards { get; set; }
    }


}