# Conpanna

Simple HTTP server framework, written in C#. Inspired by [Express](https://github.com/strongloop/express).

*Espresso con panna* is also Italian for coffee with cream.

**NOTE**: it is a project built for learning and fun, not with intention to use in production (yet).
## Example

To you can create a simple server with Conpanna in just a few lines of code:

```csharp
public class Program {
    static void Main(string[] args) {
        using (var app = new Conpanna()) {
            app.Get("/hello", (req, res) => res.Send("Hello World!"));
            app.Listen("localhost", 8080, () => Console.WriteLine("Listening on http://localhost:8080"));
            System.Console.ReadLine();
        }
    }
}
```

This server would respond to GET request on http://localhost:8000/ with 'Hello World!' string.

## Dependencies

Project depends on [Moq](https://github.com/Moq/moq) for mocking, [SimpleInjector](https://simpleinjector.org/index.html) for dependency injection and [Xunit](http://xunit.github.io/) for unit testing.

## TODO

There are a few more things I would like to add:
* Middleware support, e.g. ```app.use(...)```
* A few built in middlewares
* JSON support
* Implement missing feature from Request and Response classes, e.g. JSON support
* Test on Linux and MacOs with CoreCLR

## Contributing
Issues, PRs, suggestions always welcome.
