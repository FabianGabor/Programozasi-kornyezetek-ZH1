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
			Valuta valuta = new Valuta {EurErteke = 333};
			double eredmeny;
			valuta.atvalt(Valuta.Irany.EurFt, 1000, out eredmeny);
			Console.WriteLine(eredmeny);
			valuta.atvalt(Valuta.Irany.FtEur, 1000, out eredmeny);
			Console.WriteLine(eredmeny);
			
			// 2. Az előző metódust felhasználva készíts programot, ami úgy vált át, hogy az átváltás irányát menüből
			// lehet kiválasztani! Az összeg legyen bekérhető! Nem-pozitív esetben számoljon abszolút értéket!
			// Nem-szám bemenet esetében jelezze a hibát, majd ismételje meg a bekérést!
			Console.WriteLine("Valassz valutavaltast: ");
			foreach (Valuta.Irany i in (Valuta.Irany[]) Enum.GetValues(typeof(Valuta.Irany)))
			{
				Console.WriteLine(i.GetHashCode()+1 + "." + i);
			}
			
			int valasz = 0;
			do {
				Console.Write("Valasz: ");
				try {
					valasz = Convert.ToInt32(Console.ReadLine());
					valasz--;
				}
				catch (Exception e) {
					Console.WriteLine("Szamot (1 vagy 2)!");
				}
			} while (valasz < 0 || valasz > 1);

			int osszeg = 0;
			bool osszegOK = false;
			do {
				Console.Write("Osszeg: ");
				try {
					osszeg = Math.Abs(Convert.ToInt32(Console.ReadLine()));
					osszegOK = true;
				}
				catch (Exception e) {
					Console.WriteLine("Szamot!");
				}
			} while (!osszegOK);
			
			if (Enum.IsDefined(typeof(Valuta.Irany), valasz)) {				// kisse folosleges, mert do/while-ban ellenorizve van, hogy 0 vagy 1 legyen
				valuta.atvalt((Valuta.Irany) valasz, osszeg, out eredmeny);
				Console.WriteLine("Eredmeny: " + eredmeny);
			}
		}
	}
}