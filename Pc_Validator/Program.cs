using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace Pc_Validator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("C:\\Users\\bexy\\source\\repos\\Pc_Validator\\Pc_Validator\\pc-store-inventory.json");
            var data = JsonConvert.DeserializeObject<Root>(jsonString);

            var cpuPar = data.CPUs;
            var memPar = data.Memory;
            var boardPar = data.Motherboards;

            //Storing products from input
            List<string> finalProducts = new List<string>();


            //Checking the inventory
            if (data == null)
            {
                Console.WriteLine("No items in inventory!");
            }

            string[] componentCategory = { "CPUs", "Motherboards", "Memory" };

            Console.WriteLine("Welcome to PC configurator!");
            Console.WriteLine();

            //Listing items in inventory to client
            for (int i = 0; i < componentCategory.Length; i++)
            {
                Console.WriteLine($"\n{componentCategory[i]}");
                if (componentCategory[i] == "CPUs")
                {
                    foreach (var cpu in data.CPUs)
                    {
                        Console.WriteLine($" Name:{cpu.Name} \n Part number: {cpu.PartNumber} - Socket: {cpu.Socket}, Supported memory: {cpu.SupportedMemory} \n Price: {cpu.Price}$");

                    };
                }

                else if (componentCategory[i] == "Memory")
                {
                    foreach (var memory in data.Memory)
                    {
                        Console.WriteLine($" Name:{memory.Name} \n Part number:{memory.PartNumber} - Memory type: {memory.Type} \n Price: {memory.Price}$");

                    };
                }

                else if (componentCategory[i] == "Motherboards")
                {
                    foreach (var board in data.Motherboards)
                    {
                        Console.WriteLine($" Name:{board.Name} \n Part number: {board.PartNumber} - Socket: {board.Socket} \n Price: {board.Price}$");

                    };
                }


            }

            //Getting separate inputs to store them in array, to manipulate it on further.
            string[] input = new string[3];
            Console.WriteLine("Enter the following information for the part you want to combine:");
            Console.Write("CPU`s part number: ");
            input[0] = Console.ReadLine();
            Console.Write("Memory part number: ");
            input[1] = Console.ReadLine();
            Console.Write("Motherboard part number: ");
            input[2] = Console.ReadLine();



            //Checking if part numbers user provides its correct/in order
            if (!cpuPar.Any(c => c.PartNumber == input[0]))
            {
                Console.WriteLine("Wrong CPU part number! \nPlease use correct part numbers of the items listed above!");
                Console.WriteLine();
            }
            if (!memPar.Any(m => m.PartNumber == input[1]))
            {
                Console.WriteLine("Wrong Memory part number \nPlease use correct part numbers of the items listed above!");
                Console.WriteLine();
            }
            if (!boardPar.Any(b => b.PartNumber == input[2]))
            {
                Console.WriteLine("Wrong Memory part number \nPlease use correct part numbers of the items listed above!");
                Console.WriteLine();
            }


            //----------------///

            bool isValid = false;
            //Validation for the parts if they are compatible
            foreach (var x in cpuPar)
            {
                if (x.PartNumber == input[0])
                {
                    foreach (var y in memPar)
                    {
                        if (y.PartNumber == input[1])
                        {
                            foreach (var z in boardPar)
                            {
                                if (z.PartNumber == input[2])
                                {
                                    if (x.SupportedMemory == y.Type && x.Socket == z.Socket)
                                    {
                                        isValid = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!isValid)
            {
                for (var i = 0; i < input.Length; i++)
                {
                    foreach (var cpu in cpuPar)
                    {
                        if (input[i] == cpu.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible memories for this CPU");
                            foreach (var p in memPar)
                            {
                                if (cpu.SupportedMemory == p.Type)
                                {
                                    Console.WriteLine($"Name: {p.Name}, part number: {p.PartNumber}");
                                }

                            }

                            Console.WriteLine("Here is the compatible motherboards for this CPU");
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
                        if (input[i] == memory.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible CPU`s for this type of memory");
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
                        if (input[i] == board.PartNumber)
                        {
                            Console.WriteLine("Here is the compatible CPU`s for this motherboard");
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

            else
            {
                double totalPrice = 0;

                for (var i = 0; i < input.Length; i++)
                {
                    foreach (var cpu in cpuPar)
                    {
                        if (input[i] == cpu.PartNumber)
                        {
                            finalProducts.Add(input[i]);
                            totalPrice += cpu.Price;
                            Console.WriteLine($"{cpu.Name}, {cpu.Price} $");
                        }
                    }

                    foreach (var memory in memPar)
                    {
                        if (input[i] == memory.PartNumber)
                        {
                            finalProducts.Add(input[i]);
                            totalPrice += memory.Price;
                            Console.WriteLine($"{memory.Name}, {memory.Price} $");
                        }
                    }

                    foreach (var board in boardPar)
                    {
                        if (input[i] == board.PartNumber)
                        {
                            finalProducts.Add(input[i]);
                            totalPrice += board.Price;
                            Console.WriteLine($"{board.Name}, {board.Price} $");
                        }
                    }
                }

                Console.WriteLine($"Total cost of parts: {totalPrice} $");
            }
        }
    }


    public abstract class Components
    {
        public string? ComponentType { get; set; }
        public string? PartNumber { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

    }

    public class CPU : Components
    {
        public string? SupportedMemory { get; set; }
        public string? Socket { get; set; }

    }

    public class Memory : Components
    {
        public string? Type { get; set; }
    }

    public class Motherboard : Components
    {
        public string? Socket { get; set; }
    }

    public class Root
    {
        public List<CPU>? CPUs { get; set; }
        public List<Memory>? Memory { get; set; }
        public List<Motherboard>? Motherboards { get; set; }

    }



}