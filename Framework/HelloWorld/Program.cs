using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace GenericCalculator
{
    /// <summary>
    /// The main class/entry point of the application.
    /// </summary>
    /// <remarks>
    /// This is the (in)famous 'Hello, world!' application Managed Extensibility Framework
    /// style!
    /// 
    /// Rather than simply printing the proverbial message to the console, it requests
    /// (from the Managed Extensibility Framework runtime) a dependency, an implementation
    /// of the <see cref="IDependency" /> interface (see below), and calls its
    /// <see cref="IDependency.SaySomething" /> method. This method, in turn, shares the
    /// aforementioned communique with the audience.
    /// 
    /// There is exactly one "official", production implementation of the interface,
    /// represented by the <see cref="Dependency" /> class.
    /// </remarks>
    public static class Program
    {
        /// <summary>
        /// The application's main entry point.
        /// </summary>
        /// <remarks>
        /// The method creates an instance of the <see cref="CompositionContainer" />
        /// class (the MEF class that can be used to directly request individual exports),
        /// passing it an instance of the <see cref="AssemblyCatalog"/> class, which
        /// ensures that all exports defined in the current assembly get loaded.
        /// 
        /// Subsequently, it requests the implementation of the <see cref="IDependency" />
        /// interface, upon which it calls its <see cref="IDependency.SaySomething" />
        /// method, which does the actual heavy lifting - it prints 'Hello, world!' to the
        /// console.
        /// 
        /// Please note that the approach taken here assumes that exactly one "official",
        /// production implementation of the <see cref="IDependency" /> interface exists.
        /// The <see cref="ExportProvider.GetExportedValue{T}" /> method would throw an
        /// exception if that wasn't the case. If you want to allow multiple exports
        /// (i.e., implementations) of a single interface, you should use the
        /// <see cref="ExportProvider.GetExportedValues{T}" /> (plural) method instead.
        /// </remarks>
        public static void Main()
        {
            using(var container = new CompositionContainer(
                new AssemblyCatalog(typeof(Program).Assembly)))
            {
                container.ComposeParts();
                IDependency dependency = container.GetExportedValue<IDependency>();
                dependency.SaySomething();
            }
            Console.ReadLine();
        }
    }

    /// <summary>
    /// The interface to be implemented by the application's "dependency".
    /// </summary>
    public interface IDependency
    {
        /// <summary>
        /// Says something by printing it to the console.
        /// </summary>
        void SaySomething();
    }

    /// <summary>
    /// The application's "dependency", implementing and exporting the
    /// <see cref="IDependency" /> interface.
    /// </summary>
    [Export(typeof(IDependency))]
    public class Dependency : IDependency
    {
        /// <summary>
        /// Says something by printing it to the console.
        /// </summary>
        public void SaySomething()
        {
            Console.WriteLine("Hello, world!");
        }
    }
}
