using System;

class ControlFlow
{
    public static void Main()
    {
        Console.WriteLine("Enter a number:");
        int num = Convert.ToInt32(Console.ReadLine());

        // if-else statement
        if (num > 0)
        {
            Console.WriteLine("The number is positive.");
        }
        else if (num < 0)
        {
            Console.WriteLine("The number is negative.");
        }
        else
        {
            Console.WriteLine("The number is zero.");
        }

        // switch statement
        Console.WriteLine("Enter a number (1-5): ");
        int switchNum = Convert.ToInt32(Console.ReadLine());
        switch (switchNum)
        {
            case 1:
                Console.WriteLine("One");
                break;
            case 2:
                Console.WriteLine("Two");
                break;
            case 3:
                Console.WriteLine("Three");
                break;
            case 4:
                Console.WriteLine("Four");
                break;
            case 5:
                Console.WriteLine("Five");
                break;
            default:
                Console.WriteLine("Number is not between 1 and 5.");
                break;
        }

        // for loop
        Console.WriteLine("Numbers from 1 to 5:");
        for (int i = 1; i <= 5; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        // while loop
        int count = 1;
        Console.WriteLine("Numbers from 1 to 5 (using while loop):");
        while (count <= 5)
        {
            Console.Write(count + " ");
            count++;
        }
        Console.WriteLine();

        // do-while loop
        int doCount = 1;
        Console.WriteLine("Numbers from 1 to 5 (using do-while loop):");
        do
        {
            Console.Write(doCount + " ");
            doCount++;
        }
        while (doCount <= 5);
        Console.WriteLine();

        // foreach loop
        string[] names = { "Alucard", "Pawan", "Diksha" };
        Console.WriteLine("Names in the array:");
        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        // break and continue
        Console.WriteLine("Numbers from 1 to 10 (skipping 5):");
        for (int i = 1; i <= 10; i++)
        {
            if (i == 5)
            {
                continue;
            }
            if (i == 8)
            {
                break;
            }
            Console.Write(i + " ");
        }
        Console.WriteLine();
    }
}
