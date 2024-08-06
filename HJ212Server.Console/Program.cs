// See https://aka.ms/new-console-template for more information
using HJ212Server.Core;
using System.Data;

Console.WriteLine("Hello, World!");
Flag flag = new Flag();
string flagString = string.Join(string.Empty, flag.Version, flag.D ? 1 : 0, flag.A ? 1 : 0);
int flagInt = Convert.ToInt32(flagString, 2);
int v = Convert.ToInt32(flag.Version, 2);
int d = Convert.ToInt32(flag.D);
int a = Convert.ToInt32(flag.A);
v = v << 2;
d = d << 1;
int flagInt2 = v ^ d ^ a;
Console.ReadLine();