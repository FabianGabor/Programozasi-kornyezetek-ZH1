using System;

namespace ZH1 {
	// 1. Készíts metódust, ami euró és ft között vált oda-vissza! Az első paraméterben legyen az átváltás iránya,
	// a másodikban az összeg, a harmadik kimenő paraméterben az eredmény!
	class Valuta {
		internal enum Irany {
			FtEur,
			EurFt
		}

		public double EurErteke { get; set; } = 300;

		public void atvalt(Irany irany, int osszeg, out double eredmeny) {
			eredmeny = 0;
			switch (irany) {
				case Irany.FtEur:
					eredmeny = osszeg / EurErteke; break;
				case Irany.EurFt:
					eredmeny = osszeg * EurErteke; break;
			}
		}
	}
	internal class Program {
		public static void Main(string[] args) {
			Valuta valuta = new Valuta();
			valuta.EurErteke = 333;
			double eredmeny;
			valuta.atvalt(Valuta.Irany.EurFt, 1000, out eredmeny);
			Console.WriteLine(eredmeny);
			valuta.atvalt(Valuta.Irany.FtEur, 1000, out eredmeny);
			Console.WriteLine(eredmeny);
		}
	}
}