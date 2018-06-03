using System;
using System.Composition;
using System.Composition.Hosting;

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
        /// This method creates an instance of the <see cref="ContainerConfiguration" />
        /// class, setting it up to include all "exports" defined in the current assembly,
        /// and uses it to obtain an instance of the <see cref="CompositionHost" /> class,
        /// the Managed Extensibility Framework "container", which can be used to directly
        /// request individual exports.
        /// 
        /// Subsequently, it requests the implementation of the <see cref="IDependency" />
        /// interface, upon which it calls its <see cref="IDependency.SaySomething" />
        /// method, which does the actual heavy lifting - it prints 'Hello, world!' to the
        /// console.
        /// 
        /// Please note that the approach taken here assumes that exactly one "official",
        /// production implementation of the <see cref="IDependency" /> interface exists.
        /// The <see cref="CompositionContext.GetExport{TExport}" /> method would throw an
        /// exception if that wasn't the case. If you want to allow multiple exports
        /// (i.e., implementations) of a single interface, you should use the
        /// <see cref="CompositionContext.GetExports{TExport}" /> (plural) method instead.
        /// </remarks>
        public static void Main()
        {
            var configuration = new ContainerConfiguration();
            configuration.WithAssembly(typeof(Program).Assembly);
            using(CompositionHost container = configuration.CreateContainer())
            {
                IDependency dependency = container.GetExport<IDependency>();
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
