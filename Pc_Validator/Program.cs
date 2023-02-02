using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;

namespace Pc_Validator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("C:\\Users\\bexy\\source\\repos\\Pc_Validator\\Pc_Validator\\pc-store-inventory.json");
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonString);

            var cpuPar = myDeserializedClass.CPUs;
            var memPar = myDeserializedClass.Memory;
            var boardPar = myDeserializedClass.Motherboards;

            var compatibleCpuOne = cpuPar.Where(p => p.SupportedMemory == "DDR4" || p.Socket == "AM4");
            var compatibleCpuTwo = cpuPar.Where(p => p.SupportedMemory == "DDR5" || p.Socket == "AM5");


            foreach (var p in compatibleCpuOne)
            {
                Console.WriteLine(p.PartNumber);
            }
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
                        Console.WriteLine($" Name:{cpu.Name} \n Part number: {cpu.PartNumber} - {cpu.Socket}, {cpu.SupportedMemory} \n Price: {cpu.Price}$");

                    };
                }

                else if (componentCategory[i] == "Motherboards")
                {
                    foreach (var board in myDeserializedClass.Motherboards)
                    {
                        Console.WriteLine($" Name:{board.Name} \n Part number: {board.PartNumber} - {board.Socket} \n Price: {board.Price}$");

                    };
                }

                else if (componentCategory[i] == "Memory")
                {
                    foreach (var memory in myDeserializedClass.Memory)
                    {
                        Console.WriteLine($" Name:{memory.Name} \n Part number:{memory.PartNumber} - {memory.Type} \n Price: {memory.Price}$");

                    };
                }

            }

            Console.WriteLine("Please enter part number(s): ");
            for (int i = 0; i < componentCategory.Length; i++)
            {
                string input = Console.ReadLine();
                

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