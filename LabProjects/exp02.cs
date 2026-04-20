using System;   
    class DataTypesDemo
    {
        public static void Main()
        {
            int age = 32;
            string name = "Alucard";
            double height = 178.6;
            bool isStudent = false;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Is Student: " + isStudent);

            Console.WriteLine("");
            
            Console.WriteLine("Enter your name ");
            name = Console.ReadLine();
            Console.WriteLine("Enter your age ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your height ");
            height = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Are you a student? (Enter True or False): ");
            isStudent = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("");
            
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Is Student: " + isStudent);
        }
}