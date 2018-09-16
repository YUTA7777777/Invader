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
		public static string file="";
		public static uint interval=0;
		static void Main()
		{
			Console.Clear();
			Console.CursorVisible=false;
			Console.Title="Invader";
			Console.WindowWidth=20;
			Console.WindowHeight=20;
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
			file+="V,19,0,|,5\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";
			file+="V,20,0,|,5\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";
			file+="V,19,0,|,5\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";
			file+="V,20,0,|,5\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";

			file+="V,1,0,Z,40\n";
			file+="V,1,0,Z,10\n";
			file+="V,1,0,Z,10\n";
			file+="V,1,0,Z,10\n";

			file+="V,1,1,Z,10\n";
			file+="V,1,1,Z,10\n";
			file+="V,1,1,Z,10\n";
			file+="V,1,1,Z,10\n";

			file+="V,19,0,|,20\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";

			file+="V,20,0,|,20\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";

			file+="V,1,2,Z,10\n";
			file+="V,1,2,Z,10\n";
			file+="V,1,2,Z,10\n";
			file+="V,1,2,Z,10\n";

			file+="V,19,0,|,20\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";

			file+="V,20,0,|,20\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";

			file+="V,1,3,Z,10\n";
			file+="V,1,3,Z,10\n";
			file+="V,1,3,Z,10\n";
			file+="V,1,3,Z,10\n";

			file+="V,19,0,|,20\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";

			file+="V,20,0,|,20\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";

			file+="V,1,4,Z,10\n";
			file+="V,1,4,Z,10\n";
			file+="V,1,4,Z,10\n";

			file+="V,19,0,|,20\n";
			file+="V,17,0,|,0\n";
			file+="V,15,0,|,0\n";
			file+="V,13,0,|,0\n";
			file+="V,11,0,|,0\n";
			file+="V,9,0,|,0\n";
			file+="V,7,0,|,0\n";
			file+="V,5,0,|,0\n";
			file+="V,3,0,|,0\n";
			file+="V,1,0,|,0\n";

			file+="V,20,0,|,20\n";
			file+="V,18,0,|,0\n";
			file+="V,16,0,|,0\n";
			file+="V,14,0,|,0\n";
			file+="V,12,0,|,0\n";
			file+="V,10,0,|,0\n";
			file+="V,8,0,|,0\n";
			file+="V,6,0,|,0\n";
			file+="V,4,0,|,0\n";
			file+="V,2,0,|,0\n";
			file+="V,0,0,|,0\n";
			file+="V,1,4,Z,10\n";

			p=new Player();
			b=new Player();
			b.name="|";
			p.count=0;
			p.x=0;
			p.y=Console.WindowHeight-1;
			p.name="A";
			p.count=0;
			string[] data=file.Split('\n');
			t=new Player[0];
			int players=0;
			int wait=0;
			for(int i=0;i<data.Length;i++)
			{
				string[] node=data[i].Split(',');
				try
				{
					Player tmp=new Player();
					tmp.name=node[0];
					tmp.x=int.Parse(node[1]);
					tmp.y=int.Parse(node[2]);
					try{
						tmp.c=node[3];
					}catch{
						tmp.c="";
					}
					try{
						wait+=int.Parse(node[4]);
					}catch{
					}
					players++;
					Array.Resize(ref t,t.Length+1);
					t[t.Length-1]=tmp;
					t[t.Length-1].count=players;
					t[t.Length-1].status=1;
					t[t.Length-1].wait=wait;
					Array.Sort(t);
					Array.Reverse(t);
				}catch
				{
				}
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
				for(int i=0;i<t.Length;i++)
				{
					if(p.y==t[i].y && p.x==t[i].x && t[i].status==1)
						gameover=true;
				}
			}
		}
		static void Update()
		{
			interval++;
			for(int i=0;i<t.Length;i++)
			{
				if(t[i].x>=0 & t[i].x<=Console.WindowWidth-1 & t[i].y>=0 & t[i].y<=Console.WindowHeight-1)
					Console.SetCursorPosition(t[i].x,t[i].y);
				Console.Write(" ");
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write(" ");
			if(b.status==1)
			{
				Console.SetCursorPosition(b.x,b.y);
				Console.Write(" ");
			}
			if(b.y==0 & b.status==1)
			{
				b.status=0;
				b.y=Console.WindowHeight+1;
			}
			if(b.status==1)
			{
				b.y--;
				for(int i=0;i<t.Length;i++)
				{
					if(t[i].x==b.x & t[i].y==b.y & t[i].enter==1 & t[i].status==1)
					{
						t[i].status=0;
						t[i].enter=0;
					}
				}
				Console.SetCursorPosition(b.x,b.y);
				Console.ForegroundColor=ConsoleColor.Red;
				Console.Write(b.name);
				Console.ForegroundColor=ConsoleColor.White;
			}
			for(int i=0;i<t.Length;i++)
			{
				if(interval>=t[i].wait)
				{
					t[i].enter=1;
				}
				if(t[i].status==1&t[i].enter==1)
				{
					switch(t[i].c)
					{
						case "|":
							t[i].y++;
							break;
						case "Z":
							t[i].interval++;
							if(t[i].interval%32>=16)
							{
								if(t[i].x<Console.WindowWidth-1)
									t[i].x++;
							}else
							{
								if(t[i].x>1)
									t[i].x--;
							}
							if(t[i].interval%4==1)
								t[i].y++;
							break;
					}
				}
			}
			int j=t.Length;
			for(int i=0;i<t.Length;i++)
			{
				if(t[i].status==1)
					clear=false;
				else
					j--;
			}
			if(j==0)
				clear=true;
			for(int i=0;i<t.Length;i++)
			{
				if(t[i].y>Console.WindowHeight-1 | t[i].y<0 | t[i].x>Console.WindowWidth-1 | t[i].y<0 | (t[i].x==b.x & t[i].y==b.y & t[i].status==1 & t[i].enter==1))
				{
					t[i].status=0;
					t[i].y=0;
				}
				if(t[i].status==1&t[i].enter==1)
				{
					Console.SetCursorPosition(t[i].x,t[i].y);
					Console.Write(t[i].name);
				}
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write(p.name);
		}
	}
	class Player : System.IComparable
	{
		public int status,x,y,count,wait,enter,interval;
		public string name,c;
		public static int Count=1;
		public Player()
		{
			Count++;
		}
		public void Dispose()
		{
			Count--;
		}
		public int CompareTo(object obj)
		{
			return this.count.CompareTo(((Player)obj).count);
		}
	}
}
