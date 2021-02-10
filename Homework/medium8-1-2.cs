using System;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    class Program
    {
        public static void Main()
        {
            
            Bank bank;
            bank = new Bank();

            while (true)
            {
                string inputLine = Console.ReadLine();
                var strings = inputLine.Split();

                var commandName = strings[0].ToLower();
                if (commandName == "newbill" || commandName == "closebill" || commandName == "transfer" )
                {
                    switch (commandName)
                    {
                        case "newbill" :
                            if (bank.Bills.FirstOrDefault(x => x.Name == strings[1]) == null)
                            {
                                bank.CreateBill(strings[1], strings[2]);
                            }
                            else
                            {
                                Console.WriteLine($"Bill name is taken");
                                continue;
                            }
                            break;
                        case "closebill" : bank.CloseBill(strings[1]);
                            break;
                        case "transfer" : bank.Transfer(strings[1], strings[2], strings[3]);
                            break;
                    }
                    bank.AddCommand(strings);
                    Console.WriteLine($"Total commands: {bank.Commands.Count}");
                }
                else if(commandName == "undo")
                {
                    bank.Undo();
                    Console.WriteLine($"Total commands: {bank.Commands.Count}");
                }
                else
                {
                    Console.WriteLine($"Wrong command");
                    continue;
                }

                bank.ShowInfo();
            }
        }
    }
    
    class Bank
    {
        public List<Command> Commands;
        public List<Bill> Bills;
        public List<Bill> TrashBills;

        public Bank()
        {
            Commands = new List<Command>();
            Bills = new List<Bill>();
            TrashBills = new List<Bill>();
        }

        public void AddCommand(string[] strings)
        {
            List<string> args = new List<string>();
            for (int i = 1; i < strings.Length; i++)
            {
                args.Add(strings[i]);
            }
            Commands.Add(new Command(strings[0],args.ToArray()));
        }

        public void CreateBill(string name, string stringAmount)
        {
            int.TryParse(stringAmount, out int amount);
            Bills.Add(new Bill(name, amount));
        }
        
        public void CloseBill(string name)
        {
            var bill = Bills.FirstOrDefault(x => x.Name == name);
            if (bill != null)
            {
                TrashBills.Add(bill);
                Bills.Remove(bill);
            }
        }

        public void Transfer(string nameFrom, string nameTo, string stringAmount)
        {
            var billFrom = Bills.FirstOrDefault(x => x.Name == nameFrom);
            var billTo = Bills.FirstOrDefault(x => x.Name == nameTo);
            int.TryParse(stringAmount, out int amount);
            if (billFrom != null && billTo != null)
            {
                if (billFrom.Amount >= amount)
                {
                    billFrom.Decrement(amount);
                    billTo.Increment(amount);
                }
            }
        }

        public void ShowInfo()
        {
            for (var i = 0; i < Bills.Count; i++)
            {
                var it = Bills[i];
                Console.WriteLine($"Bill {i} {it.Name} {it.Amount}");
            }
        }

        public void Undo()
        {
            if (Commands.Count <= 0)
            {
                return;
            }

            var commandName = Commands.Last().Name;
            var commandArgs = Commands.Last().Arguments;
            switch (commandName)
            {
                case "newbill" :
                    UndoCreate();
                    break;
                case "closebill" : 
                    UndoClose(commandArgs[0]);
                    break;
                case "transfer" : 
                    Transfer(commandArgs[1], commandArgs[0], commandArgs[2]);
                    break;
            }
            Commands.RemoveAt(Commands.Count - 1);
        }

        private void UndoCreate()
        {
            Bills.RemoveAt(Bills.Count - 1);
        }
        
        private void UndoClose(string name)
        {
            var bill = TrashBills.FirstOrDefault(x => x.Name == name);
            if (bill != null)
            {
                CreateBill(bill.Name, bill.Amount.ToString());
                TrashBills.Remove(bill);
            }
        }
    }
    
    class Command
    {
        public string Name { get;}
        public string[] Arguments { get;}

        public Command(string name, string[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }

    class Bill
    {
        public string Name { get; }
        public int Amount { get; private set; }

        public Bill(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public void Increment(int value)
        {
            Amount += value;
        }
        
        public void Decrement(int value)
        {
            Amount -= value;
        }
    }
}