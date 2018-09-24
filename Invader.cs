using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
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
		static Data[] data;
		public static string file="";
		public static uint interval=0,cinterval=0,Level=1,Score=0;
		static uint cbuf=0;
		static void Main()
		{
			data=new Data[0];
			try{
				var dexmlSerializer = new XmlSerializer(typeof(Data[]));
				var xmlSettings = new System.Xml.XmlReaderSettings()
				{
					CheckCharacters = false,
				};
				using (var streamReader = new StreamReader("Score", Encoding.UTF8))
					using (var xmlReader
							= System.Xml.XmlReader.Create(streamReader, xmlSettings))
					{
						data = (Data[])dexmlSerializer.Deserialize(xmlReader);
					}
			}catch{
				data=new Data[0];
			}
			Console.Clear();
			Console.CursorVisible=false;
			Console.Title="Invader";
			Console.WindowWidth=20;
			Console.WindowHeight=20;
			Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
			Console.Write("Invader");
			Console.ReadKey();
			Console.Clear();
			while(!clear)
			{
				Init();
				load();
				Run();
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
				if(clear)
				{
					Console.Write("Clear!!");
					Console.SetCursorPosition(Console.WindowWidth/2-String.Format("Score {0}",Score).Length/2,Console.WindowHeight/2+1);
					Score+=500;
					Console.Write("Score {0}",Score);
				}else
				{
					Console.Write("GameOver!!");
					Console.SetCursorPosition(Console.WindowWidth/2-String.Format("Score {0}",Score).Length/2,Console.WindowHeight/2+1);
					Console.Write("Score {0}",Score);
					gameover=false;
					interval=0;
					cinterval=0;
					Level=1;
					cbuf=0;
				}
				Array.Resize(ref data,data.Length+1);
				data[data.Length-1]=new Data();
				data[data.Length-1].score=Score;
				Array.Sort(data);
				Array.Reverse(data);
				Data[] tmpdata=new Data[0];
				for(int i=0;i<10;i++)
				{
					if(i<=data.Length-1)
					{
						Array.Resize(ref tmpdata,tmpdata.Length+1);
						tmpdata[tmpdata.Length-1]=new Data();
						tmpdata[tmpdata.Length-1]=data[i];
					}
				}
				data=tmpdata;
				using (var streamWriter = new StreamWriter("Score", false, Encoding.UTF8))
				{
					var xmlSerializer1 = new XmlSerializer(typeof(Data[]));
					xmlSerializer1.Serialize(streamWriter, data);
				}
				Console.ReadKey();
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth/2-3,0);
				Console.Write("Score");
				for(int i=0;i<data.Length;i++)
				{
					if(i<10)
					{
						Console.SetCursorPosition((Console.WindowWidth-String.Format("{0}:{1}",i+1,data[i].score).Length)/2,Console.WindowHeight/2+i-10/2);
						Console.Write("{0}:{1}",i+1,data[i].score);
					}
				}
				Score=0;
				Console.ReadKey();
				Console.Clear();
			}
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
			p.y=Console.WindowHeight-2;
			p.name="A";
			p.count=0;
			string[] data=file.Split('\n');
			invader=new Player[0];
			allinvader=new Player[0][];
			int players=0;
			uint wait=0;
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
						tmp.iswait=int.Parse(node[5]);
					}catch{
						tmp.iswait=0;
					}
					try{
						wait+=uint.Parse(node[4]);
						if(int.Parse(node[4])==0)
						{
							players++;
							Array.Resize(ref invader,invader.Length+1);
							invader[invader.Length-1]=tmp;
							invader[invader.Length-1].count=players;
							invader[invader.Length-1].status=1;
							invader[invader.Length-1].wait=wait;
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
				if(cbuf<allinvader.Length)
					cinterval=allinvader[cbuf][0].wait;
			}
		}
		static void Move(int h)
		{
			switch(h)
			{
				case 1:// <<
					if(p.x>0)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.x--;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
					}
					break;
				case 2:// >>
					if(p.x<Console.WindowWidth-1)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.x++;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
					}
					break;
				case 3://A
					if(p.y>0)
					{
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(" ");
						p.y--;
						Console.SetCursorPosition(p.x,p.y);
						Console.Write(p.name);
					}
					break;
				case 4://V
					if(p.y<Console.WindowHeight-2)
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
								for(int i=0;i<invader.Length;i++)
								{
									if(invader[i].x==b.x & invader[i].y==b.y & invader[i].status==1)
									{
										if(invader[i].life<=1)
										{
											invader[i].status=0;
											invader[i].y=0;
											Score+=invader[i].score;
										}else
										{
											invader[i].life--;
										}
									}
								}
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
		static void Clear()
		{
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].x>=0 & invader[i].x<=Console.WindowWidth-1 & invader[i].y>=0 & invader[i].y<=Console.WindowHeight-2)
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
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].x>=0 & invader[i].x<=Console.WindowWidth-1 & invader[i].y>=0 & invader[i].y<=Console.WindowHeight-2)
					Console.SetCursorPosition(invader[i].x,invader[i].y);
				Console.Write(" ");
			}
		}
		static void Update()
		{
			Clear();
			interval++;
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
					if(invader[i].x==b.x & invader[i].y==b.y & invader[i].status==1)
					{
						if(invader[i].life<=1)
						{
							invader[i].status=0;
							invader[i].y=0;
							Score+=invader[i].score;
						}else
						{
							invader[i].life--;
						}
					}
				}
				Console.SetCursorPosition(b.x,b.y);
				Console.ForegroundColor=ConsoleColor.Red;
				Console.Write(b.name);
				Console.ForegroundColor=ConsoleColor.White;
			}
			if(interval>cinterval)
			{
				load();
			}
			UpdateInvader();
			Array.Sort(invader);
			int j=invader.Length;
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].status==1 & !invader[i].isbomb)
					clear=false;
				else
					j--;
			}
			if(j==0 & cbuf >=allinvader.Length)
				clear=true;
			Player[] tmpinvader=new Player[0];
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].x==b.x & invader[i].y==b.y & invader[i].status==1 & b.status==1)
				{
					if(invader[i].life<=1)
					{
						invader[i].status=0;
						invader[i].y=0;
						Score+=invader[i].score;
					}else
					{
						invader[i].life--;
						Console.ForegroundColor=ConsoleColor.Red;
					}
				}
				if(invader[i].y>Console.WindowHeight-2 | invader[i].y<0 | invader[i].x>Console.WindowWidth | invader[i].y<0)
				{
					invader[i].status=0;
				}
				if(invader[i].status==1)
				{
					Console.SetCursorPosition(invader[i].x,invader[i].y);
					if(invader[i].isbomb & (invader[i].c=="|" | invader[i].c=="Y"))
					{
						Console.ForegroundColor=ConsoleColor.Green;
						if(invader[i].life>1)
							Console.ForegroundColor=ConsoleColor.Magenta;
					}
					Console.Write(invader[i].name);
					Console.ForegroundColor=ConsoleColor.White;
					Array.Resize(ref tmpinvader,tmpinvader.Length+1);
					tmpinvader[tmpinvader.Length-1]=invader[i];
				}
			}
			invader=new Player[0];
			for(int i=0;i<tmpinvader.Length;i++)
			{
				Array.Resize(ref invader,invader.Length+1);
				invader[invader.Length-1]=tmpinvader[i];
			}
			Console.SetCursorPosition(p.x,p.y);
			Console.Write(p.name);
		}
		static void Sort()
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
			for(int i=0;i<tmpinvader.Length;i++)
			{
				Array.Resize(ref invader,invader.Length+1);
				invader[invader.Length-1]=tmpinvader[i];
			}
			Array.Sort(invader);
			Array.Reverse(invader);
		}
		static void UpdateInvader()
		{
			for(int i=0;i<invader.Length;i++)
			{
				if(invader[i].status==1)
				{
					if(!invader[i].isbomb)
					{
						MoveInvader(i);
					}
					else
					{
						MoveInvaderBombs(i);
					}
					if(invader[i].iswait!=0)
					{
						interval--;
					}
				}
			}
		}
		static void MoveInvaderBombs(int i)
		{
			int seed = Environment.TickCount;
			invader[i].score=0;
			switch(invader[i].c)
			{
				case "Y":
					if(invader[i].variant==null)
					{
						invader[i].variant=new int[1];
						invader[i].variant[0]=invader[i].x>p.x?-3:3;
					}
					invader[i].y++;
					if(invader[i].x>p.x)
					{
						invader[i].variant[0]--;
					}
					if(invader[i].x<p.x)
					{
						invader[i].variant[0]++;
					}
					invader[i].x+=invader[i].variant[0];
					if(invader[i].x<0)
					{
						invader[i].x=0;
					}
					if(invader[i].x>Console.WindowWidth-1)
					{
						invader[i].x=Console.WindowWidth-1;
					}
					break;
				case "|":
					invader[i].y++;
					break;
				case "*":
					invader[i].interval++;
					if(invader[i].interval==1)
					{
						if(invader[i].x>p.x)
						{
							invader[i].x--;
						} if(invader[i].x<p.x)
						{
							invader[i].x++;
						} if(invader[i].y>p.y)
						{
							invader[i].y--;
						} if(invader[i].y<p.y)
						{
							invader[i].y++;
						}
					}
					if(invader[i].interval>=8)
					{
						invader[i].status=0;
					}	
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
				case "p":
					invader[i].score=10;
					invader[i].interval++;
					if(invader[i].interval%2==1)
					{
						if(invader[i].x>p.x)
						{
							invader[i].x--;
						} if(invader[i].x<p.x)
						{
							invader[i].x++;
						} if(invader[i].y>p.y)
						{
							invader[i].y--;
						} if(invader[i].y<p.y)
						{
							invader[i].y++;
						}
					}
					break;
			}
		}
		static void MoveInvader(int i)
		{
			int seed = Environment.TickCount;
			switch(invader[i].c)
			{
				case "Level":
					invader[i].score=0;
					invader[i].status=0;
					Console.Clear();
					Console.SetCursorPosition(Console.WindowWidth/2-String.Format("LEVEL {0}",Level).Length/2,Console.WindowHeight/2);
					Console.Write(String.Format("Level {0}",Level));
					Console.ReadKey();
					Console.Clear();
					Level++;
					break;
				case "X":
					invader[i].score=600;
					invader[i].interval++;
					if(invader[i].interval==2)
					{
						invader[i].life=16;
					}
					if(invader[i].interval%32>=20 && invader[i].interval%2==1)
					{
						if(invader[i].x>p.x)
						{
							invader[i].x--;
						}
						if(invader[i].x<p.x)
						{
							invader[i].x++;
						}
						Array.Resize(ref invader,invader.Length+1);
						invader[invader.Length-1]=new Player();
						invader[invader.Length-1].x=invader[i].x;
						invader[invader.Length-1].y=invader[i].y;
						invader[invader.Length-1].wait=interval;
						invader[invader.Length-1].count=invader[i].count;
						invader[invader.Length-1].status=1;
						invader[invader.Length-1].interval=0;
						invader[invader.Length-1].isbomb=true;
						invader[invader.Length-1].c="|";
						invader[invader.Length-1].name="|";
						invader[invader.Length-1].life=2;
						if(invader[i].x>=1)
						{
							Array.Resize(ref invader,invader.Length+1);
							invader[invader.Length-1]=new Player();
							invader[invader.Length-1].x=invader[i].x-1;
							invader[invader.Length-1].y=invader[i].y;
							invader[invader.Length-1].wait=interval;
							invader[invader.Length-1].count=invader[i].count;
							invader[invader.Length-1].status=1;
							invader[invader.Length-1].interval=0;
							invader[invader.Length-1].isbomb=true;
							invader[invader.Length-1].c="|";
							invader[invader.Length-1].name="|";
							invader[invader.Length-1].life=2;
						}
						if(invader[i].x<=Console.WindowWidth-2)
						{
							Array.Resize(ref invader,invader.Length+1);
							invader[invader.Length-1]=new Player();
							invader[invader.Length-1].x=invader[i].x+1;
							invader[invader.Length-1].y=invader[i].y;
							invader[invader.Length-1].wait=interval;
							invader[invader.Length-1].count=invader[i].count;
							invader[invader.Length-1].status=1;
							invader[invader.Length-1].interval=0;
							invader[invader.Length-1].isbomb=true;
							invader[invader.Length-1].c="|";
							invader[invader.Length-1].name="|";
							invader[invader.Length-1].life=2;
						}
					}
					break;
				case ";":
					invader[i].score=50;
					invader[i].interval++;
					if(invader[i].variant==null)
						invader[i].variant=new int[1];
					if(invader[i].x==Console.WindowWidth-1)
						invader[i].variant[0]=0;
					else if(invader[i].x==0)
						invader[i].variant[0]=1;
					if(invader[i].variant[0]==1)
					{
						invader[i].x++;
					}else
					{
						invader[i].x--;
					}
					if(p.x==invader[i].x | (invader[i].interval%10==4))
					{
						Array.Resize(ref invader,invader.Length+1);
						invader[invader.Length-1]=new Player();
						invader[invader.Length-1].x=invader[i].x;
						invader[invader.Length-1].y=invader[i].y+1;
						invader[invader.Length-1].wait=interval;
						invader[invader.Length-1].count=invader[i].count;
						invader[invader.Length-1].status=1;
						invader[invader.Length-1].interval=0;
						invader[invader.Length-1].isbomb=true;
						invader[invader.Length-1].variant=new int[1];
						invader[invader.Length-1].variant[0]=0;
						invader[invader.Length-1].c="Y";
						invader[invader.Length-1].name="*";
					}
					break;
				case ":":
				       invader[i].score=50;
				       invader[i].interval++;
				       if(invader[i].variant==null)
					       invader[i].variant=new int[1];
				       if(invader[i].x==Console.WindowWidth-1)
					       invader[i].variant[0]=0;
				       else if(invader[i].x==0)
					       invader[i].variant[0]=1;
				       if(invader[i].variant[0]==1)
				       {
					       invader[i].x++;
				       }else
				       {
					       invader[i].x--;
				       }
				       if(p.x==invader[i].x | (invader[i].interval%10>=4))
				       {
					       Array.Resize(ref invader,invader.Length+1);
					       invader[invader.Length-1]=new Player();
					       invader[invader.Length-1].x=invader[i].x;
					       invader[invader.Length-1].y=invader[i].y+1;
					       invader[invader.Length-1].wait=interval;
					       invader[invader.Length-1].count=invader[i].count;
					       invader[invader.Length-1].status=1;
					       invader[invader.Length-1].interval=0;
					       invader[invader.Length-1].isbomb=true;
					       invader[invader.Length-1].c="|";
					       invader[invader.Length-1].name="|";
				       }
				       break;
				case "|":
				       invader[i].y++;
				       break;
				case "V":
				       invader[i].score=30;
				       invader[i].y++;
				       if(p.y==invader[i].y|p.x==invader[i].x & new Random(seed++).Next(2)==1)
				       {
					       Array.Resize(ref invader,invader.Length+1);
					       invader[invader.Length-1]=new Player();
					       invader[invader.Length-1].x=invader[i].x;
					       invader[invader.Length-1].y=invader[i].y;
					       invader[invader.Length-1].wait=interval;
					       invader[invader.Length-1].count=invader[i].count;
					       invader[invader.Length-1].status=1;
					       invader[invader.Length-1].interval=0;
					       invader[invader.Length-1].isbomb=true;
					       invader[invader.Length-1].c="*";
					       invader[invader.Length-1].name="*";
				       }
				       break;
				case "Z":
				       invader[i].score=30;
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
				       invader[i].score=20;
				       if(invader[i].x<Console.WindowWidth-1)
					       invader[i].x++;
				       else
					       invader[i].status=0;
				       if(p.x==invader[i].x & new Random(seed++).Next(15)==1)
				       {
					       Array.Resize(ref invader,invader.Length+1);
					       invader[invader.Length-1]=new Player();
					       invader[invader.Length-1].x=invader[i].x;
					       invader[invader.Length-1].y=invader[i].y+1;
					       invader[invader.Length-1].wait=interval;
					       invader[invader.Length-1].count=invader[i].count;
					       invader[invader.Length-1].status=1;
					       invader[invader.Length-1].interval=0;
					       invader[invader.Length-1].isbomb=true;
					       invader[invader.Length-1].c="|";
					       invader[invader.Length-1].name="|";
				       }
				       break;
				case "<":
				       invader[i].score=20;
				       if(invader[i].x>0)
					       invader[i].x--;
				       else
					       invader[i].status=0;
				       if(p.x==invader[i].x & new Random(seed++).Next(15)==1)
				       {
					       Array.Resize(ref invader,invader.Length+1);
					       invader[invader.Length-1]=new Player();
					       invader[invader.Length-1].x=invader[i].x;
					       invader[invader.Length-1].y=invader[i].y+1;
					       invader[invader.Length-1].wait=interval;
					       invader[invader.Length-1].count=invader[i].count;
					       invader[invader.Length-1].status=1;
					       invader[invader.Length-1].interval=0;
					       invader[invader.Length-1].isbomb=true;
					       invader[invader.Length-1].c="|";
					       invader[invader.Length-1].name="|";
				       }
				       break;
				case "B":
				       invader[i].score=40;
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
				       if(p.y==invader[i].y|p.x==invader[i].x & new Random(seed++).Next(2)==1)
				       {
					       Array.Resize(ref invader,invader.Length+1);
					       invader[invader.Length-1]=new Player();
					       invader[invader.Length-1].x=invader[i].x;
					       invader[invader.Length-1].y=invader[i].y;
					       invader[invader.Length-1].wait=interval;
					       invader[invader.Length-1].count=invader[i].count;
					       invader[invader.Length-1].status=1;
					       invader[invader.Length-1].interval=0;
					       invader[invader.Length-1].isbomb=true;
					       invader[invader.Length-1].c="*";
					       invader[invader.Length-1].name="*";
				       }
				       break;
				case "p":
				       invader[i].score=50;
				       invader[i].interval++;
				       if(invader[i].interval%2==1)
				       {
					       if(invader[i].x>p.x)
					       {
						       invader[i].x--;
					       } if(invader[i].x<p.x)
					       {
						       invader[i].x++;
					       } if(invader[i].y>p.y)
					       {
						       invader[i].y--;
					       } if(invader[i].y<p.y)
					       {
						       invader[i].y++;
					       }
				       }
				       break;
				case "Y":
				       invader[i].score=30;
				       if(invader[i].variant==null)
				       {
					       invader[i].variant=new int[1];
					       invader[i].variant[0]=invader[i].x>p.x?-2:2;
				       }
				       invader[i].y++;
				       if(invader[i].x>p.x)
				       {
					       invader[i].variant[0]--;
				       }
				       if(invader[i].x<p.x)
				       {
					       invader[i].variant[0]++;
				       }
				       invader[i].x+=invader[i].variant[0];
				       if(invader[i].x<0)
				       {
					       invader[i].x=0;
				       }
				       if(invader[i].x>Console.WindowWidth-1)
				       {
					       invader[i].x=Console.WindowWidth-1;
				       }
				       break;
			}
		}
	}
	public class Data : System.IComparable
	{
		public uint score;
		public int CompareTo(object obj)
		{
			return this.score.CompareTo(((Data)obj).score);
		}
	}
	class Player : System.IComparable
	{
		public int status=1,x,y,count,life=1,iswait=0;
		public uint interval,wait,score=10;
		public int[] variant=null;
		public string name,c;
		public bool isbomb=false;
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
