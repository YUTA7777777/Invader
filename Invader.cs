using System;
using System.Diagnostics;
using System.IO;
namespace Invader
{
	class Invader
	{
		static bool clear=false;
		static bool gameover=false;
		static Player p;
		static Player b;
		static Player[] invader;
		static Player[][] allinvader;
		public static string file="";
		public static uint interval=0;
		static uint cbuf=0;
		static void Main()
		{
			Console.Clear();
			Console.CursorVisible=false;
			Console.Title="Invader";
			Console.WindowWidth=20;
			Console.WindowHeight=20;
			Init();
			load();
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
			try
			{
				using(StreamReader r =new StreamReader("Data"))
				{
					file=r.ReadToEnd();
				}
			}
			catch
			{}
			p=new Player();
			b=new Player();
			b.name="|";
			p.count=0;
			p.x=0;
			p.y=Console.WindowHeight-1;
			p.name="A";
			p.count=0;
			string[] data=file.Split('\n');
			invader=new Player[0];
			allinvader=new Player[0][];
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
						if(int.Parse(node[4])==0)
						{
							players++;
							Array.Resize(ref invader,invader.Length+1);
							invader[invader.Length-1]=tmp;
							invader[invader.Length-1].count=players;
							invader[invader.Length-1].status=1;
							invader[invader.Length-1].wait=wait;
							invader[invader.Length-1].enter=1;
							Array.Sort(invader);
							Array.Reverse(invader);
						}else
						{
							players++;
							Array.Resize(ref invader,invader.Length+1);
							invader[invader.Length-1]=tmp;
							invader[invader.Length-1].count=players;
							invader[invader.Length-1].status=1;
							invader[invader.Length-1].wait=wait;
							invader[invader.Length-1].enter=1;
							Array.Sort(invader);
							Array.Reverse(invader);
							Array.Resize(ref allinvader,allinvader.Length+1);
							allinvader[allinvader.Length-1]=invader;
							invader=new Player[0];
							Array.Sort(allinvader);
							Array.Reverse(allinvader);
						}
					}catch{
					}
				}catch
				{
				}
			}
			load();
		}
		static void load()
		{
			if(cbuf<allinvader.Length)
			{
				Player[] tmpinvader=new Player[0];
				for(int i=0;i<invader.Length;i++)
				{
					if(invader[i].status==1)
					{
						Array.Resize(ref tmpinvader,tmpinvader.Length+1);
						tmpinvader[tmpinvader.Length-1]=invader[i];
					}
				}
				invader=allinvader[cbuf];
				for(int i=0;i<tmpinvader.Length;i++)
				{
					Array.Resize(ref invader,invader.Length+1);
					invader[invader.Length-1]=tmpinvader[i];
				}
				Array.Sort(invader);
				Array.Reverse(invader);
				cbuf++;
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
							for(int i=0;i<invader.Length;i++)
							{
								if(i==int.Parse(c.KeyChar.ToString())-1)
									invader[i].status=1;
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
				for(int i=0;i<invader.Length;i++)
				{
					if(p.y==invader[i].y && p.x==invader[i].x && invader[i].status==1)
						gameover=true;
				}
			}
		}
		static void Update()
		{
			interval++;
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].x>=0 & invader[i].x<=Console.WindowWidth-1 & invader[i].y>=0 & invader[i].y<=Console.WindowHeight-1)
					Console.SetCursorPosition(invader[i].x,invader[i].y);
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
				for(int i=0;i<invader.Length;i++)
				{
					if(invader[i].x==b.x & invader[i].y==b.y & invader[i].enter==1 & invader[i].status==1)
					{
						invader[i].status=0;
						invader[i].enter=0;
					}
				}
				Console.SetCursorPosition(b.x,b.y);
				Console.ForegroundColor=ConsoleColor.Red;
				Console.Write(b.name);
				Console.ForegroundColor=ConsoleColor.White;
			}
			if(interval>=invader[0].wait)
			{
				load();
			}
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].status==1&invader[i].enter==1)
				{
					switch(invader[i].c)
					{
						case "|":
							invader[i].y++;
							break;
						case "Z":
							invader[i].interval++;
							if(invader[i].interval%32>=16)
							{
								if(invader[i].x<Console.WindowWidth-1)
									invader[i].x++;
								else
									invader[i].interval=0;
							}else
							{
								if(invader[i].x>1)
									invader[i].x--;
								else
									invader[i].interval=16;
							}
							if(invader[i].interval%4==1)
								invader[i].y++;
							break;
						case ">":
							if(invader[i].x<Console.WindowWidth-1)
								invader[i].x++;
							else
								invader[i].status=0;
							break;
						case "<":
							if(invader[i].x>1)
								invader[i].x--;
							else
								invader[i].status=0;
							break;
					}
				}
			}
			int j=invader.Length;
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].status==1)
					clear=false;
				else
					j--;
			}
			if(j==0 & cbuf >=allinvader.Length)
				clear=true;
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].y>Console.WindowHeight-1 | invader[i].y<0 | invader[i].x>Console.WindowWidth-1 | invader[i].y<0 | (invader[i].x==b.x & invader[i].y==b.y & invader[i].status==1 & invader[i].enter==1))
				{
					invader[i].status=0;
					invader[i].y=0;
				}
				if(invader[i].status==1&invader[i].enter==1)
				{
					Console.SetCursorPosition(invader[i].x,invader[i].y);
					Console.Write(invader[i].name);
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
