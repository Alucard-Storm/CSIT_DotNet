using System;
using System.IO;

class FileIO
{
    public static void Main()
    {
        // Write to file
        File.WriteAllText("hello.txt", "Hello World");

        // Read from file
        string content = File.ReadAllText("hello.txt");
        Console.WriteLine(content);

        // Append to file
        File.AppendAllText("hello.txt", " Hello World Again!");

        // Read from file
        content = File.ReadAllText("hello.txt");
        Console.WriteLine(content);

        // Delete file
        File.Delete("hello.txt");
    }
}
