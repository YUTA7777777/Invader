using System;
using System.Diagnostics;
namespace Invader
{
	class Invader
	{
		static bool clear=false;
		static void Main()
		{
			Run();
		}
		static void Init()
		{}
		static void Draw()
		{}
		static void Move(int h)
		{}
		static void Run()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			while(!clear)
			{
				if(Console.KeyAvailable)
				{
					ConsoleKeyInfo c = Console.ReadKey(true);
					switch(c.Key)
					{
						case ConsoleKey.Escape:
							Environment.Exit(0);
							break;
						case ConsoleKey.UpArrow:
							Move(1);
							break;
						case ConsoleKey.DownArrow:
							Move(2);
							break;
						case ConsoleKey.LeftArrow:
							Move(3);
							break;
						case ConsoleKey.RightArrow:
							Move(4);
							break;
					}
				}
				TimeSpan ts = sw.Elapsed;
				if(ts.TotalSeconds>=0.2)
				{
					Draw();
					sw.Reset();
					sw.Start();
				} 
			}
		}
	}
}
