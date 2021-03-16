/*
 * Fábián Gábor
 * CXNU8T
 */

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

		public void Atvalt(Irany irany, int osszeg, out double eredmeny) {
			eredmeny = 0;
			switch (irany) {
				case Irany.FtEur:
					eredmeny = osszeg / EurErteke; break;
				case Irany.EurFt:
					eredmeny = osszeg * EurErteke; break;
			}
		}
	}

	// 3. Készíts struktúrát, ami egy gyümölcs adatait tárolja: név, fajta (felsorolás, többféle is lehet egyszerre:
	// déligyümölcs, csonthéjas, magvas), egységár! Utóbbi csak tulajdonságként legyen elkészítve!
	// Készítsünk default konstruktort és valamennyi adatával meghatározottat! Készítsünk ToString metódust!
	internal class Gyumolcs {
		private string _nev;

		internal enum Fajta {
			Déligyümölcs, 
			Csonthéjas, 
			Magvas
		}

		private Fajta _fajta;
		private int _egysegar;

		public Gyumolcs() {
		}

		public Gyumolcs(string nev, Fajta fajta, int egysegar) {
			_nev = nev;
			_fajta = fajta;
			_egysegar = egysegar;
		}

		public string Nev {
			get => _nev;
			set => _nev = value;
		}

		public Fajta Faj {
			get => _fajta;
			set => _fajta = value;
		}

		public int Egysegar {
			get => _egysegar;
			set => _egysegar = value;
		}

		public override string ToString() {
			return _nev + " " + _fajta + " " + _egysegar;
		}
	}

	// 4. Készíts delegáltat, ami egy bemenő szövegtömbből egész számot ad vissza!
	// Készíts függvényt, ami visszaadja, hogy a paraméterben kapott szövegekben hány kezdődik nagybetűvel!
	// Készíts függvényt, ami visszaadja, hogy a paraméterben kapott szövegekben hány írásjel szerepel összesen! (.?!)
	// Használd a függvényeket delegáltként!
	internal class Delegalt {
		public delegate int SzovegSzam(string s);
		public static int NagybetuSzamol(string s) {
			int n = 0;
			foreach (char t in s) {
				if (char.IsUpper(t)) n++;
			}
			return n;
		}

		public static int IrasjelSzamol(string s) {
			int n = 0;
			foreach (char t in s) {
				if (char.IsPunctuation(t)) n++;
			}
			return n;
		}
	}
	
	internal class Program {
		public static void Main(string[] args) {
			Valuta valuta = new Valuta {EurErteke = 333};
			double eredmeny;
			valuta.Atvalt(Valuta.Irany.EurFt, 1000, out eredmeny);
			Console.WriteLine(eredmeny);
			valuta.Atvalt(Valuta.Irany.FtEur, 1000, out eredmeny);
			Console.WriteLine(eredmeny);

			// 2. Az előző metódust felhasználva készíts programot, ami úgy vált át, hogy az átváltás irányát menüből
			// lehet kiválasztani! Az összeg legyen bekérhető! Nem-pozitív esetben számoljon abszolút értéket!
			// Nem-szám bemenet esetében jelezze a hibát, majd ismételje meg a bekérést!
			Console.WriteLine("Valassz valutavaltast: ");
			foreach (Valuta.Irany i in (Valuta.Irany[]) Enum.GetValues(typeof(Valuta.Irany))) {
				Console.WriteLine(i.GetHashCode() + 1 + "." + i);
			}

			int valasz = 0;
			do {
				Console.Write("Valasz: ");
				try {
					valasz = Convert.ToInt32(Console.ReadLine());
					valasz--;
				}
				catch (Exception) {
					Console.WriteLine("Szamot (1 vagy 2)!");
				}
			} while (valasz < 0 || valasz > 1);

			int osszeg = 0;
			bool osszegOk = false;
			do {
				Console.Write("Osszeg: ");
				try {
					osszeg = Math.Abs(Convert.ToInt32(Console.ReadLine()));
					osszegOk = true;
				}
				catch (Exception) {
					Console.WriteLine("Szamot!");
				}
			} while (!osszegOk);

			if (Enum.IsDefined(typeof(Valuta.Irany), valasz)) {
				// kisse folosleges, mert do/while-ban ellenorizve van, hogy 0 vagy 1 legyen
				valuta.Atvalt((Valuta.Irany) valasz, osszeg, out eredmeny);
				Console.WriteLine("Eredmeny: " + eredmeny);
			}

			Gyumolcs gyumolcs = new Gyumolcs("alma", Gyumolcs.Fajta.Magvas, 100);
			Console.WriteLine(gyumolcs.ToString());

			Delegalt.SzovegSzam szovegSzam;

			szovegSzam = Delegalt.IrasjelSzamol;
			Console.WriteLine("Irasjel: " + szovegSzam("\"Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...\""));
			
			szovegSzam = Delegalt.NagybetuSzamol;
			Console.WriteLine("Nagybetu: " + szovegSzam("\"Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...\""));
		}
	}
}