using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ayse_aktas_proje_odevi
{

    static class CarpismaDenetimi
    {
        /// <summary>
        /// Nokta, dikdörtgen çarpışma denetimi
        /// </summary>
        /// <param name="n">Nokta</param>
        /// <param name="d">Dikdörtgen</param>
        /// <returns></returns>
        public static bool NoktaDortgen(Nokta n, Dikdortgen d)
        {
            if ((n.X <= (d.X + (d.En / 2))) && ((d.X - (d.En / 2)) <= n.X) && (n.Y <= (d.Y + (d.Yukseklik / 2))) && ((d.Y - (d.Yukseklik / 2)) <= n.Y))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Dikdörtgen({d.X},{d.Y},{d.Yukseklik},{d.En})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Dikdörtgen({d.X},{d.Y},{d.Yukseklik},{d.En})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Nokta, çember çarpışma denetimi
        /// </summary>
        /// <param name="n">Nokta</param>
        /// <param name="c">Çember</param>
        /// <returns>bool</returns>
        public static bool NoktaCember(Nokta n, Cember c)
        {
            double mesafe = Math.Sqrt(Math.Pow(n.X - c.X, 2) + Math.Pow(n.Y - c.Y, 2));
            if (mesafe <= c.R)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Çember({c.X},{c.Y},{c.R})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Çember({c.X},{c.Y},{c.R})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Dikdörtgen, dikdörtgen çarpışma denetimi
        /// </summary>
        /// <param name="d1">Dikdörtgen1</param>
        /// <param name="d2">Dikdörtgen2</param>
        /// <returns>bool</returns>
        public static bool DikdortgenDikdortgen(Dikdortgen d1, Dikdortgen d2)
        {
            if ((Math.Abs(d1.X - d2.X) < (d1.En / 2 + d2.En / 2)) && (Math.Abs(d1.Y - d2.Y) < (d1.Yukseklik / 2 + d2.Yukseklik / 2)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdörtgen({d1.X},{d1.Y},{d1.Yukseklik},{d1.En})", $"Dikdörtgen({d2.X},{d2.Y},{d2.Yukseklik},{d2.En})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdörtgen({d1.X},{d1.Y},{d1.Yukseklik},{d1.En})", $"Dikdörtgen({d2.X},{d2.Y},{d2.Yukseklik},{d2.En})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Dikdörtgen, çember çarpışma denetimi
        /// </summary>
        /// <param name="d">Dikdörtgen</param>
        /// <param name="c">Çember</param>
        /// <returns>bool</returns>
        public static bool DikdortgenCember(Dikdortgen d, Cember c)
        {
            // Dikdörtgenin kenar uzunluklarının yarısını hesaplayın
            int dikdortgenYarisiEn = d.En / 2;
            int dikdortgenYarisiYukseklik = d.Yukseklik / 2;

            // Dikdörtgenin sol, sağ, üst ve alt kenarlarının koordinatlarını hesaplayın
            int solKenarX = d.X - dikdortgenYarisiEn;
            int sagKenarX = d.X + dikdortgenYarisiEn;
            int ustKenarY = d.Y - dikdortgenYarisiYukseklik;
            int altKenarY = d.Y + dikdortgenYarisiYukseklik;

            // Çemberin merkezine en yakın dikdörtgen kenarının uzaklığını hesaplayın
            int xUzaklik = Math.Max(solKenarX - c.X, 0) + Math.Max(c.X - sagKenarX, 0);
            int yUzaklik = Math.Max(ustKenarY - c.Y, 0) + Math.Max(c.Y - altKenarY, 0);
            double kenarUzaklik = Math.Sqrt(xUzaklik * xUzaklik + yUzaklik * yUzaklik);

            // Eğer çemberin merkezi, dikdörtgenin içinde değilse, çarpışma yoktur
            if (kenarUzaklik > c.R)
            {
                return false;
            }

            // Eğer çemberin merkezi, dikdörtgenin içindeyse, çarpışma vardır
            return true;
        }

        /// <summary>
        /// Çember, çember çarpışma denetimi
        /// </summary>
        /// <param name="c1">Çember1</param>
        /// <param name="c2">Çember2</param>
        /// <returns>bool</returns>
        public static bool CemberCember(Cember c1, Cember c2)
        {
            // İki çemberin merkezleri arasındaki mesafe
            double cemberlerArasıMesafe = Math.Sqrt(Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2));

            // Eğer iki çemberin merkezleri arasındaki mesafe, çember yarıçaplarının toplamından küçükse, çarpışma var.
            if (cemberlerArasıMesafe <= c1.R + c2.R)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Çember({c1.X},{c1.Y},{c1.R})", $"Çember({c2.X},{c2.Y},{c2.R}) ", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Çember({c1.X},{c1.Y},{c1.R})", $"Çember({c2.X},{c2.Y},{c2.R}) ", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Nokta, Küre çarpışma denetimi
        /// </summary>
        /// <param name="k">Küre</param>
        /// <param name="n">Nokta</param>
        /// <returns>bool</returns>
        public static bool NoktaKure(Kure k, Nokta n)
        {
            // Nokta ile küre merkezi arasındaki mesafe hesaplaması
            double mesafe = Math.Sqrt(Math.Pow(n.X - k.X, 2) + Math.Pow(n.Y - k.Y, 2) + Math.Pow(n.Z - k.Z, 2));

            // Eğer mesafe, küre yarıçapından küçük veya eşitse, çarpışma var.
            if (mesafe <= k.R)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Küre({k.X},{k.Y},{k.Z},{k.R})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Küre({k.X},{k.Y},{k.Z},{k.R})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Nokta, dikdörtgen prizma çarpışma denetimi
        /// </summary>
        /// <param name="dp">Dikdörtgen</param>
        /// <param name="n">Nokta</param>
        /// <returns>bool</returns>
        public static bool NoktaDikdortgenPrizma(DikdortgenPrizma dp, Nokta n)
        {
            if ((n.X <= (dp.X + dp.En / 2)) && (n.X >= (dp.X - dp.En / 2)) &&
                (n.Y <= (dp.Y + dp.Yukseklik / 2)) && (n.Y >= (dp.Y - dp.Yukseklik / 2)) &&
                (n.Z <= (dp.Z + dp.Derinlik / 2)) && (n.Z >= (dp.Z - dp.Derinlik / 2)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik},{dp.Derinlik})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik},{dp.Derinlik})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Nokta, Silindir çarpışma denetimi
        /// </summary>
        /// <param name="s">Silindir</param>
        /// <param name="n">Nokta</param>
        /// <returns>bool</returns>
        public static bool NoktaSilindir(Silindir s, Nokta n)
        {
            // Silindir üzerindeki noktanın yüksekliği hesaplanır
            int mesafeyx = (int)Math.Sqrt(Math.Pow((n.X - s.MerkezX), 2) + Math.Pow((n.Y - s.MerkezY), 2));
            int mesafez = Math.Abs(n.Z - s.MerkezZ);

            // Noktanın silindir içinde olup olmadığı kontrol edilir
            if (mesafeyx <= s.Yaricap && mesafez <= s.Yukseklik / 2)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Nokta({n.X},{n.Y},{n.Z})", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// İki silindirin çakışma durumunu gösterir
        /// </summary>
        /// <param name="s1">Silinidir1</param>
        /// <param name="s2">Silindir2</param>
        /// <returns>bool</returns>
        public static bool SilindirSilindir(Silindir s1, Silindir s2)
        {
            if (((Math.Abs(s1.MerkezX - s2.MerkezX) <= (s1.Yaricap + s2.Yaricap)) &&
                (Math.Abs(s1.MerkezY - s2.MerkezY) <= (s1.Yaricap + s2.Yaricap))) &&
                (Math.Abs(s1.MerkezZ - s2.MerkezZ) <= ((s1.Yukseklik + s2.Yukseklik) / 2)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s1.MerkezX},{s1.MerkezY},{s1.MerkezZ},{s1.Yaricap},{s1.Yukseklik})", $"Silindir({s2.MerkezX},{s2.MerkezY},{s2.MerkezZ},{s2.Yaricap},{s2.Yukseklik})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s1.MerkezX},{s1.MerkezY},{s1.MerkezZ},{s1.Yaricap},{s1.Yukseklik})", $"Silindir({s2.MerkezX},{s2.MerkezY},{s2.MerkezZ},{s2.Yaricap},{s2.Yukseklik})", "Çarpışmaz.");
                return false;
            }
        }


        /// <summary>
        /// Küre, küre çarpışma denetimi
        /// </summary>
        /// <param name="k1">Küre1</param>
        /// <param name="k2">Küre2</param>
        /// <returns>bool</returns>
        public static bool KureKure(Kure k1, Kure k2)
        {
            if ((Math.Abs(k1.X - k2.X) <= (k1.R + k2.R)) &&
                (Math.Abs(k1.Y - k2.Y) <= (k1.R + k2.R)) &&
                (Math.Abs(k1.Z - k2.Z) <= (k1.R + k2.R)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k1.X},{k1.Y},{k1.Z},{k1.R})", $"Küre({k2.X},{k2.Y},{k2.Z},{k2.R})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k1.X},{k1.Y},{k1.Z},{k1.R})", $"Küre({k2.X},{k2.Y},{k2.Z},{k2.R})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Küre silindir çarpışma denetimi
        /// </summary>
        /// <param name="k">Küre</param>
        /// <param name="s">Silindir</param>
        /// <returns>bool</returns>
        public static bool KureSilindir(Kure k, Silindir s)
        {
            //Fark hesaplamaları
            int FarkX = Math.Abs(k.X - s.MerkezX) - k.R - s.Yaricap;
            int FarkZ = Math.Abs(k.Y - s.MerkezY) - k.R - s.Yaricap;
            int FarkY = Math.Abs(k.Z - s.MerkezZ) - k.R - s.Yukseklik / 2;

            if (FarkX <= 0 && FarkY <= 0 && FarkZ <= 0)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Yüzey, küre çarpışma denetimi
        /// </summary>
        /// <param name="y">Yüzey</param>
        /// <param name="k">Küre</param>
        /// <returns>bool</returns>
        public static bool YuzeyKure(Yuzey y, Kure k)
        {
            if ((y.NoktaEksen == "X") && ((y.nokta.X <= (k.X + k.R)) && ((k.X - k.R) <= y.nokta.X)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Y") && (((k.Y - k.R) <= y.nokta.Y) && ((k.Y + k.R) >= y.nokta.Y)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Z") && ((y.nokta.Z <= (k.Z + k.R)) && ((k.Z - k.R) <= y.nokta.Z)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Yüzey, dikdörtgen prizma çarpışma denetimi
        /// </summary>
        /// <param name="y">Yüzey</param>
        /// <param name="dp">Dikdörtgen prizma</param>
        /// <returns>bool</returns>
        public static bool YuzeyDikdortgenP(Yuzey y, DikdortgenPrizma dp)
        {
            if ((y.NoktaEksen == "X") && ((dp.X - dp.En / 2) <= y.nokta.X && (dp.X + dp.En / 2) >= y.nokta.X))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Z") && ((dp.Z - dp.Derinlik / 2) <= y.nokta.Z && (dp.Z + dp.Derinlik / 2) >= y.nokta.Z))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Y") && ((dp.Y - dp.Yukseklik / 2) <= y.nokta.Y && (dp.Y + dp.Yukseklik / 2) >= y.nokta.Y))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışmaz.");
                return false;
            }
        }

        /// <summary>
        /// Yüzey silindir çarpışma denetimi
        /// </summary>
        /// <param name="y">Yüzey</param>
        /// <param name="s">Silindir</param>
        /// <returns>bool</returns>
        public static bool YuzeySilindir(Yuzey y, Silindir s)
        {
            if ((y.NoktaEksen == "X") && ((s.MerkezX + s.Yaricap >= y.nokta.X) && (s.MerkezX - s.Yaricap <= y.nokta.X)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Z") && (((s.MerkezZ - (s.Yukseklik / 2)) <= y.nokta.Z) && ((s.MerkezZ + (s.Yukseklik / 2)) >= y.nokta.Z)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else if ((y.NoktaEksen == "Y") && ((s.MerkezY + s.Yaricap >= y.nokta.Y) && (s.MerkezY - s.Yaricap <= y.nokta.Y)))
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Silindir({s.MerkezX},{s.MerkezY},{s.MerkezZ},{s.Yaricap},{s.Yukseklik})", $"Yüzey({y.Sayi},{y.NoktaEksen})", "Çarpışmaz.");
                return false;
            }
        }
        /// <summary>
        /// Küre,dikdörtgen prizma çarpışma denetimi
        /// </summary>
        /// <param name="k">Küre</param>
        /// <param name="dp">Dikdörtgen Prizma</param>
        /// <returns>bool</returns>
        public static bool KureDikdortgenP(Kure k, DikdortgenPrizma dp)
        {
            int mesafeX = Math.Abs(k.X - dp.X);
            int mesafeY = Math.Abs(k.Y - dp.Y);
            int mesafeZ = Math.Abs(k.Z - dp.Z);

            int yariEn = dp.En / 2;
            int yariYukseklik = dp.Yukseklik / 2;
            int yariDerinlik = dp.Derinlik / 2;

            // Kürenin merkezi dikdörtgen prizmanın sınırlarının dışında mı?
            if (mesafeX > yariEn + k.R || mesafeY > yariYukseklik + k.R || mesafeZ > yariDerinlik + k.R)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", "Çarpışmaz.");
                return false;
            }

            // Kürenin merkezi dikdörtgen prizmanın içinde mi?
            if (mesafeX <= yariEn || mesafeY <= yariYukseklik || mesafeZ <= yariDerinlik)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", "Çarpışır.");
                return true;
            }

            // Kürenin merkezi dikdörtgen prizmanın yüzeyinde mi?
            int dx = mesafeX - Math.Sign(k.X - dp.X) * yariEn;
            int dy = mesafeY - Math.Sign(k.Y - dp.Y) * yariYukseklik;
            int dz = mesafeZ - Math.Sign(k.Z - dp.Z) * yariDerinlik;

            int mesafeKare = dx * dx + dy * dy + dz * dz;

            if (mesafeKare <= k.R * k.R)
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", "Çarpışır.");
                return true;
            }
            else
            {
                Console.WriteLine("{0,-35}{1,-35}{2,-10}", $"Küre({k.X},{k.Y},{k.Z},{k.R})", $"Dikdortgen Prizma({dp.X},{dp.Y},{dp.Z},{dp.En},{dp.Yukseklik})", "Çarpışmaz.");
                return false;
            }
        }



        /// <summary>
        /// Dikdörtgen prizma, dikdörtgen prizma çarpışma denetimi
        /// </summary>
        /// <param name="dp1">DikdortgenPrizma1</param>
        /// <param name="dp2">DikdortgenPrizma2</param>
        /// <returns>bool</returns>
        public static bool DikdortgenPDikdortgenP(DikdortgenPrizma dp1, DikdortgenPrizma dp2)
        {
            int DikdortgenP1MaxX = dp1.X + dp1.En / 2;
            int DikdortgenP1MaxY = dp1.Y + dp1.Yukseklik / 2;
            int DikdortgenP1MaxZ = dp1.Z + dp1.Derinlik / 2;
            int DikdortgenP1MinX = dp1.X - dp1.En / 2;
            int DikdortgenP1MinY = dp1.Y - dp1.Yukseklik / 2;
            int DikdortgenP1MinZ = dp1.Z - dp1.Derinlik / 2;

            int DikdortgenP2MaxX = dp2.X + dp2.En / 2;
            int DikdortgenP2MaxY = dp2.Y + dp2.Yukseklik / 2;
            int DikdortgenP2MaxZ = dp2.Z + dp2.Derinlik / 2;
            int DikdortgenP2MinX = dp2.X - dp2.En / 2;
            int DikdortgenP2MinY = dp2.Y - dp2.Yukseklik / 2;
            int DikdortgenP2MinZ = dp2.Z - dp2.Derinlik / 2;



            // Check if any of the planes of prism 1 are outside of prism 2
            if (DikdortgenP1MaxX < DikdortgenP2MinX || DikdortgenP1MinX > DikdortgenP2MaxX ||
                DikdortgenP1MaxY < DikdortgenP2MinY || DikdortgenP1MinY > DikdortgenP2MaxY ||
                DikdortgenP1MaxZ < DikdortgenP2MinZ || DikdortgenP1MinZ > DikdortgenP2MaxZ)
            {
                return false;
            }

            return true;
        }
        //
    }
    #region Şekiller


    class Sekiller
    {
    }

    class Nokta : Sekiller
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Nokta(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
    class Cember : Sekiller
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        public Cember(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }
    }
    class Dikdortgen : Sekiller
    {
        //Merkez nokta koordinatları
        public int X { get; set; }
        public int Y { get; set; }
        //Yükseklik-Genişlik
        public int Yukseklik { get; set; }
        public int En { get; set; }
        public Dikdortgen(int x, int y, int en, int yukseklik)
        {
            X = x;
            Y = y;
            Yukseklik = yukseklik;
            En = en;
        }
    }
    class Kure : Sekiller
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int R { get; set; }

        public Kure(int x, int y, int z, int r)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
        }
    }
    class DikdortgenPrizma : Sekiller
    {
        public int En { get; set; }
        public int Yukseklik { get; set; }
        public int Derinlik { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public DikdortgenPrizma(int x, int y, int z, int en, int yukseklik, int derinlik)
        {
            En = en;
            Yukseklik = yukseklik;
            Derinlik = derinlik;
            X = x;
            Y = y;
            Z = z;
        }
    }
    class Silindir : Sekiller
    {
        public Silindir(int merkezX, int merkezY, int merkezZ, int yaricap, int yukseklik)
        {
            MerkezX = merkezX;
            MerkezY = merkezY;
            MerkezZ = merkezZ;
            Yaricap = yaricap;
            Yukseklik = yukseklik;
        }

        public int MerkezX { get; set; }
        public int MerkezY { get; set; }
        public int MerkezZ { get; set; }
        public int Yaricap { get; set; }
        public int Yukseklik { get; set; }
    }
    class Yuzey : Sekiller
    {
        public int Sayi { get; set; }
        public string YEksen { get; set; }
        public string NoktaEksen { get; set; }
        public Nokta nokta = new Nokta(0, 0, 0);

        public Yuzey(int sayi, string noktaEksen)
        {
            Sayi = sayi;
            NoktaEksen = noktaEksen;

            if (noktaEksen == "X")
            {
                nokta.X = sayi;
                nokta.Y = 0;
                nokta.Z = 0;
            }
            else if (noktaEksen == "Y")
            {
                nokta.X = 0;
                nokta.Y = sayi;
                nokta.Z = 0;
            }
            else if (noktaEksen == "Z")
            {
                nokta.X = 0;
                nokta.Y = 0;
                nokta.Z = sayi;
            }
            else
            {
                nokta.X = 0;
                nokta.Y = 0;
                nokta.Z = 0;
            }
            //Yayıldığı ekseni belirleme
            if (NoktaEksen == "X" || NoktaEksen == "Z")
            {
                YEksen = "Y";
            }
            else
            {
                YEksen = "X";
            }
            if (!(YEksen == "X" || YEksen == "Y"))
            {
                YEksen = "X";
            }
        }
    }
    #endregion
}