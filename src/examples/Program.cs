namespace Conpanna.Examples
{
    using System;

    public class Program {
        static void Main(string[] args) {
            using (var app = new Conpanna()) {
                app.Get("/hello", (req, res) => res.Send("Hello World!"));
                app.Listen("localhost", 8080, () => Console.WriteLine("Listening on http://localhost:8080"));
                Console.ReadLine();
            }
        }
    }
}
