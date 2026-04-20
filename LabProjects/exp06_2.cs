using System;

delegate void MyDelegate(int a, int b);

class Del
{
    public static void Main()
    {
        static void Add(int x, int y)
        {
            Console.WriteLine("Add: " + (x + y));
        }
        static void Subtract(int x, int y)
        {
            Console.WriteLine("Subtract: " + (x - y));
        }

        MyDelegate del = Add;
        del += Subtract;
        del(10, 20);
    }
}
