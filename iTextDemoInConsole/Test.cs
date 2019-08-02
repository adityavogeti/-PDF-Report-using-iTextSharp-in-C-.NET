using System;
using iTextSharp.tutorial.Chapter1;

namespace iTextSharp.tutorial
{
	/// <summary>
	/// Main Class: Testing is going here...
	/// </summary>
	public class Test
	{
		static void Main() 		
		{
			new Example1();
			Console.WriteLine("Chapter1_Example1.pdf Created Successfully");
			new Example2();
			Console.WriteLine("Chapter1_Example2.pdf Created Successfully");
			new Example3();
			Console.WriteLine("Chapter1_Example3.pdf Created Successfully");
			new Example4();
			Console.WriteLine("Chapter1_Example4.pdf Created Successfully");
			new Example5();
			Console.WriteLine("Chapter1_Example5.pdf Created Successfully");
			new Example6();
			Console.WriteLine("Chapter1_Example6.pdf Created Successfully");
			new Example7();
			Console.WriteLine("Chapter1_Example7.pdf Created Successfully");
			new Example8();
			Console.WriteLine("Chapter1_Example8.pdf Created Successfully");

			Console.WriteLine("Press any key to exit...");
			Console.Read();
		}
	}
}
