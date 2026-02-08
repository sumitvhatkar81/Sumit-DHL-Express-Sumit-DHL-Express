using System;
using System.Threading.Tasks;
using DHL_Document_App.Tests;

namespace DHL_Document_App.Tests
{
    /// <summary>
    /// Simple test runner to execute our basic tests
    /// </summary>
    public class TestRunner
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("DHL Document App - Unit Test Runner");
            Console.WriteLine("====================================");
            Console.WriteLine();

            var testClass = new DocumentsControllerBasicTests();
            var results = await testClass.RunAllTests();

            Console.WriteLine();
            Console.WriteLine("SUMMARY:");
            Console.WriteLine($"Total Tests: {results.TotalCount}");
            Console.WriteLine($"Passed: {results.PassedCount}");
            Console.WriteLine($"Failed: {results.FailedCount}");
            
            if (results.FailedCount > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Failed Tests:");
                foreach (var failedTest in results.FailedTests)
                {
                    Console.WriteLine($"  - {failedTest}");
                }
            }

            Console.WriteLine();
            Console.WriteLine(results.FailedCount == 0 ? "All tests PASSED! ✓" : "Some tests FAILED! ✗");
            
            // Exit code: 0 for success, 1 for failure
            Environment.Exit(results.FailedCount == 0 ? 0 : 1);
        }
    }
}