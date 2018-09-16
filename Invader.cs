using System;
using System.Diagnostics;
namespace Invader
{
	class Invader
	{
		static bool clear=false;
		static bool gameover=false;
		static Player p;
		static Player b;
		static Player[] t;
		static Player[] tb;
		static int interval=1;
		static void Main()
		{
			Console.Clear();
			Console.CursorVisible=false;
			Console.Title="Invader";
			Console.WindowWidth=32;
			Console.WindowHeight=15;
			Init();
			Run();
			Console.Clear();
			Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
			if(clear)
			{
				Console.Write("Clear!!");
			}else
			{
				Console.Write("GameOver!!");
			}
			Console.ReadKey(true);
			Console.CursorVisible=true;
		}
		static void Init()
		{
			p=new Player();
			b=new Player();
			b.name="|";
			p.x=0;
			p.y=Console.WindowHeight-1;
			p.name="A";
			t=new Player[9];
			for(int i=0;i<t.Length;i++)
			{
				t[i]=new Player();
				t[i].x=i+1;
				t[i].y=0;
				t[i].name="V";
			}
			tb=new Player[t.Length];
			for(int i=0;i<tb.Length;i++)
			{
				tb[i]=new Player();
				tb[i].name="|";
			}
		}
		static void Move(int h)
		{
			switch(h)
			{
				case 1:
					if(p.x>0)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.x--;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
						for(int i=0;i<tb.Length;i++)
						{
							if(p.y==tb[i].y && p.x==tb[i].x)
								gameover=true;
						}
					}
					break;
				case 2:
					if(p.x<Console.WindowWidth-1)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.x++;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
						for(int i=0;i<tb.Length;i++)
						{
							if(p.y==tb[i].y && p.x==tb[i].x)
								gameover=true;
						}
					}
					break;
				case 3:
					if(p.y>0)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.y--;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
						for(int i=0;i<tb.Length;i++)
						{
							if(p.y==tb[i].y && p.x==tb[i].x)
								gameover=true;
						}
					}
					break;
				case 4:
					if(p.y<Console.WindowHeight-1)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.y++;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
						for(int i=0;i<tb.Length;i++)
						{
							if(p.y==tb[i].y && p.x==tb[i].x)
								gameover=true;
						}
					}
					break;
			}
		}
		static void Run()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			while(!clear&!gameover)
			{
				if(Console.KeyAvailable)
				{
					ConsoleKeyInfo c = Console.ReadKey(true);
					switch(c.Key)
					{
						case ConsoleKey.Escape:
							Environment.Exit(0);
							break;
						case ConsoleKey.D1:
						case ConsoleKey.D2:
						case ConsoleKey.D3:
						case ConsoleKey.D4:
						case ConsoleKey.D5:
						case ConsoleKey.D6:
						case ConsoleKey.D7:
						case ConsoleKey.D8:
						case ConsoleKey.D9:
							for(int i=0;i<t.Length;i++)
							{
								if(i==int.Parse(c.KeyChar.ToString())-1)
									t[i].status=1;
							}
							break;
						case ConsoleKey.Spacebar:
							if(b.status!=1 && p.y>0)
							{
								b.status=1;
								b.x=p.x;
								b.y=p.y-1;
								Console.ForegroundColor=ConsoleColor.Red;
								Console.SetCursorPosition(b.x,b.y);
								Console.Write(b.name);
								Console.ForegroundColor=ConsoleColor.White;
							}
							break;
						case ConsoleKey.LeftArrow:
							Move(1);
							break;
						case ConsoleKey.RightArrow:
							Move(2);
							break;
						case ConsoleKey.UpArrow:
							Move(3);
							break;
						case ConsoleKey.DownArrow:
							Move(4);
							break;
						case ConsoleKey.Enter:
							sw.Stop();
							Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
							Console.Write("PAUSE");
							Console.ReadKey();
							Console.Clear();
							sw.Start();
							break;
					}
				}
				TimeSpan ts = sw.Elapsed;
				if(ts.TotalSeconds>=0.1)
				{
					Update();
					sw.Reset();
					sw.Start();
				} 
				for(int i=0;i<tb.Length;i++)
				{
					if(p.y==tb[i].y && p.x==tb[i].x)
						gameover=true;
				}
			}
		}
		static void Update()
		{
			for(int i=0;i<t.Length;i++)
			{
				Console.SetCursorPosition(t[i].x,t[i].y);
				Console.Write(" ");
			}
			for(int i=0;i<tb.Length;i++)
			{
				Console.SetCursorPosition(tb[i].x,tb[i].y);
				Console.Write(" ");
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write(" ");
			Console.SetCursorPosition(b.x,b.y);
			Console.Write(" ");
			if(b.y==0 & b.status==1)
			{
				b.status=0;
				b.y=p.y;
			}
			if(b.status==1)
			{
				b.y--;
				Console.SetCursorPosition(b.x,b.y);
				Console.ForegroundColor=ConsoleColor.Red;
				Console.Write(b.name);
				Console.ForegroundColor=ConsoleColor.White;
			}
			int j=t.Length;
			for(int i=0;i<t.Length;i++)
			{
				if(t[i].status==0)
					clear=false;
				else
					j--;
			}
			if(j==0)
				clear=true;
			for(int i=0;i<tb.Length;i++)
			{
				if(tb[i].y==Console.WindowHeight)
				{
					tb[i].status=0;
					tb[i].y=0;
				}
				if(tb[i].status==1)
				{
					Console.SetCursorPosition(tb[i].x,tb[i].y);
					Console.ForegroundColor=ConsoleColor.Green;
					Console.Write(tb[i].name);
					Console.ForegroundColor=ConsoleColor.White;
				}
			}
			for(int i=0;i<tb.Length;i++)
			{
				if(p.y==tb[i].y && p.x==tb[i].x)
					gameover=true;
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write(p.name);
		}
	}
	class Player
	{
		public int status,x,y;
		public string name,c;
	}
}
