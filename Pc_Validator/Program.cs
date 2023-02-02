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

            //var compatibleCpuOne = cpuPar.Where(p => p.SupportedMemory == "DDR4" && p.Socket == "AM4");
            //var compatibleCpuTwo = cpuPar.Where(p => p.SupportedMemory == "DDR5" && p.Socket == "AM5");
            //var compatibleCpuThird = cpuPar.Where(p => p.SupportedMemory == "DDR5" && p.Socket == "LGA1700");
            //var compatibleCpuFourth = cpuPar.Where(p => p.SupportedMemory == "DDR4" && p.Socket == "LGA1200");

            //var compatibleMemOne = memPar.Where(p => p.Type == "DDR5");
            //var compatibleMemTwo = memPar.Where(p => p.Type == "DDR4");

            //var compatibleBoardOne = boardPar.Where(p => p.Socket == "AM4");
            //var compatibleBoardTwo = boardPar.Where(p => p.Socket == "AM5");
            //var compatibleBoardThird = boardPar.Where(p => p.Socket == "LGA1700");
            //var compatibleBoardFourth = boardPar.Where(p => p.Socket == "LGA1200");

            


            //foreach (var foo in compatibleMemOne)
            //{
            //    Console.WriteLine($"{foo.Name}, {foo.PartNumber}, {foo.Price}");
            //}

            //foreach (var p in compatibleCpuOne)
            //{
            //    Console.WriteLine(p.PartNumber);
            //}

            //foreach (var p in compatibleCpuTwo)
            //{
            //    Console.WriteLine(p.PartNumber);
            //}


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

            string input;
            Console.WriteLine("Please enter part number(s): ");
            input = Console.ReadLine();
            var array = input.Split(' ');

            if (array.Length <= 2)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    foreach (var cpu in cpuPar)
                    {
                        if (array[i] == cpu.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible memories");
                            foreach (var p in memPar)
                            {
                                if (cpu.SupportedMemory == p.Type)
                                {
                                    Console.WriteLine($"Name: {p.Name}, part number: {p.PartNumber}");
                                }
                            }
                            Console.WriteLine("Here is the compatible motherboards");
                            foreach (var b in boardPar)
                            {
                                if (cpu.Socket == b.Socket)
                                {
                                    Console.WriteLine($"Name: {b.Name}, part number: {b.PartNumber}");
                                }
                            }
                        }
                    }

                    foreach (var memory in memPar)
                    {
                        if (array[i] == memory.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible CPU`s");
                            foreach (var cpu in cpuPar)
                            {
                                if (cpu.SupportedMemory == memory.Type)
                                {
                                    Console.WriteLine($"Name: {cpu.Name}, part number: {cpu.PartNumber}");

                                }
                            }

                        }
                    }

                    foreach (var board in boardPar)
                    {
                        if (input == board.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible CPU`s");
                            foreach (var cpu in cpuPar)
                            {
                                if (board.Socket == cpu.Socket)
                                {
                                    Console.WriteLine($"Name: {cpu.Name}, part number: {cpu.PartNumber}");
                                }
                            }

                        }
                    }
                }
            }
            else if (array.Length == 3)
            {
                double totalPrice = 0;
                for (var i = 0; i < array.Length; i++)
                {
                    foreach (var cpu in cpuPar)
                    {
                        if (array[i] == cpu.PartNumber)
                        {
                            totalPrice += cpu.Price;
                            Console.WriteLine($"{cpu.Price}, {cpu.Name}");
                        }
                    }

                    foreach (var memory in memPar)
                    {
                        if (array[i] == memory.PartNumber)
                        {
                            totalPrice += memory.Price;
                            Console.WriteLine($"{memory.Price}, {memory.Name}");
                        }
                    }

                    foreach (var board in boardPar)
                    {
                        if (array[i] == board.PartNumber)
                        {
                            totalPrice += board.Price;
                            Console.WriteLine($"{board.Price}, {board.Name}");
                        }
                    }
                }

                Console.WriteLine(totalPrice);
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