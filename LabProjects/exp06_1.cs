using System;

delegate int MyDelegate(int a, int b);

class Del
{
    public static void Main()
    {
        static int Add(int x, int y)
        {
            return x + y;
        }
        static int Subtract(int x, int y)
        {
            return x - y;
        }

        MyDelegate del = new MyDelegate(Add);
        int result = del(10, 20);
        Console.WriteLine("Result: " + result);
        del = new MyDelegate(Subtract);
        result = del(10, 20);
        Console.WriteLine("Result: " + result);
    }
}
