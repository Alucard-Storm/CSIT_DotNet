using System;

class ExceptionHandling
{
    public static void Main()
    {
        try
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.WriteLine(numbers[10]);
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("Error: Index out of range.");
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Finally executed.");
        }
    }
}
