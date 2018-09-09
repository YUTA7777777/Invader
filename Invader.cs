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
			Console.WindowWidth=32;
			Console.WindowHeight=15;
			p=new Player();
			b=new Player();
			p.x=0;
			p.y=Console.WindowHeight-1;
			p.name="A";
			tb=new Player[8];
			for(int i=0;i<tb.Length;i++)
			{
				tb[i]=new Player();
			}
			t=new Player[8];
			for(int i=0;i<t.Length;i++)
			{
				t[i]=new Player();
				t[i].x=i+1;
				t[i].y=0;
				t[i].name="V";
			}
			Run();
			Console.Clear();
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
		{}
		static void Draw()
		{
			for(int i=0;i<t.Length;i++)
			{
				t[i].x=8;
				Console.SetCursorPosition(t[i].x,t[i].y);
				Console.Write("V");
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write("A");
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
						Console.Write("A");
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
						Console.Write("A");
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
						case ConsoleKey.Spacebar:
							if(b.status!=1)
							{
								b.status=1;
								b.x=p.x;
								b.y=p.y-1;
							}
							Console.SetCursorPosition(b.x,b.y);
							Console.Write("|");
							break;
						case ConsoleKey.LeftArrow:
							Move(1);
							break;
						case ConsoleKey.RightArrow:
							Move(2);
							break;
						case ConsoleKey.Enter:
							Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
							Console.Write("PAUSE");
							Console.ReadKey();
							break;
					}
				}
				TimeSpan ts = sw.Elapsed;
				if(ts.TotalSeconds>=0.2)
				{
					Console.Clear();
					if(t[t.Length-1].x>=Console.WindowWidth-1)
					{
						interval=-1;
					}else if(t[0].x<=0)
					{
						interval=1;
					}
					if(b.status==1)
					{
						b.y--;
						Console.SetCursorPosition(b.x,b.y);
						Console.Write("|");
					}
					int seed = Environment.TickCount;
					for(int i=0;i<t.Length;i++)
					{
						t[i].x+=interval;
						Console.SetCursorPosition(t[i].x,t[i].y);
						if(b.y==t[i].y && b.x==t[i].x)
							t[i].status=1;
						if(t[i].status==0)
						{
							Console.Write("V");
							if(tb[i].status==0)
							{
								Random r = new Random(seed++);
								if(r.Next(4)==1)
								{
									tb[i].y=t[i].y;
									tb[i].x=t[i].x;
									tb[i].status=1;
								}
							}else
							{
								tb[i].y++;
							}
							if(tb[i].y==Console.WindowHeight)
							{
								tb[i].status=0;
								tb[i].y=0;
							}
							if(tb[i].status==1)
							{
								Console.SetCursorPosition(tb[i].x,tb[i].y);
								Console.Write("|");
							}
						}
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
					if(b.y==0 & b.status==1)
					{
						b.status=0;
						b.y=p.y;
					}
					for(int i=0;i<tb.Length;i++)
					{
						if(p.y==tb[i].y && p.x==tb[i].x)
							gameover=true;
					}
					Console.SetCursorPosition(p.x,p.y);
					Console.Write("A");
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
	}
	class Player
	{
		public int status,x,y;
		public string name;
	}
}
