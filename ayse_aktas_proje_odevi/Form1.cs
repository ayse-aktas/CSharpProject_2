using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ayse_aktas_proje_odevi
{
    public partial class Form1 : Form
    {
        int maxX = 600;
        int maxY = 500;
        int centerX = 300;
        int centerY = 250;

        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        //------------------------------------------------Çizim Methodları-------------------------------------------------------------------------
        private void KoordinatCiz(PaintEventArgs e)
        {
            Point center = new Point(centerX, centerY);
            Point y = new Point(centerX, 0);
            Point x = new Point(maxX, centerY);
            Point Z = new Point(centerX + 150, centerY + 200);
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.DarkGreen);

            g.DrawLine(p, center, y);
            g.DrawLine(p, center, x);
            g.DrawLine(p, center, Z);
        }
        private void DikdortgenCiz(PaintEventArgs e, int x, int y, int en, int yukseklik, Brush b)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rc = new Rectangle(x + centerX, centerY - y - yukseklik, en, yukseklik);
            gp.AddRectangle(rc);
            System.Drawing.Region r = new System.Drawing.Region(gp);
            Graphics grv = e.Graphics;
            grv.FillRegion(b, r);
        }

        private void CemberCiz(PaintEventArgs e, int r, int x, int y, Color color)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(color);
            Rectangle rectangle = new Rectangle(x + centerX, centerY - y - (r * 2), r * 2, r * 2);
            g.DrawEllipse(p, rectangle);
        }
        private void KureCiz(PaintEventArgs e, Kure k, Brush brush)
        {
            int aX = centerX + k.X - k.R + 3 * k.Z / 5;
            int aY = centerY - k.Y - k.R + 4 * k.Z / 5;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(aX, aY, k.R * 2, k.R * 2);
            System.Drawing.Region rg = new System.Drawing.Region(gp);
            Graphics gr = e.Graphics;
            gr.FillRegion(brush, rg);
        }
        private void NoktaCiz(PaintEventArgs e, int x, int y, int z = 0)
        {

            if (x > 810 || y > 600)
            {
                MessageBox.Show("Nokta PaintBox'un sınırları dışında! Lütfen koordinatları kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Graphics graph = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            graph.DrawRectangle(pen, x + centerX + (z * 3 / 5), centerY - y + (z * 4 / 5), 1, 1);
        }
        // en : x, yükseklik : y, derinlik: z
        private void DikdortgenPrizmaCiz(PaintEventArgs e, int x, int y, int z, int en, int yukseklik, int derinlik)
        {

            Graphics Graph = e.Graphics;
            int aZ = z - derinlik / 2;
            int aX = centerX + x - (en / 2) + 3 * aZ / 5;
            int aY = centerY - y - (yukseklik / 2) + 4 * aZ / 5;

            int bZ = z + derinlik / 2;
            int bX = centerX + x - (en / 2) + 3 * bZ / 5;
            int bY = centerY - y - (yukseklik / 2) + 4 * bZ / 5;

            Rectangle A = new Rectangle(aX, aY, en, yukseklik);
            Rectangle B = new Rectangle(bX, bY, en, yukseklik);

            //Rectangle A = new Rectangle((x - en / 2) + centerX + derinlik / 2, centerY - (y + yukseklik / 2) + derinlik / 2, en, yukseklik);
            //Rectangle B = new Rectangle((x - en / 2) + centerX - derinlik / 2, centerY - (y + yukseklik / 2) - derinlik / 2, en, yukseklik);

            Graph.DrawRectangle(Pens.Red, A);
            Graph.DrawRectangle(Pens.Blue, B);

            Point[] points = new Point[8];
            points[0] = new Point(A.Left, A.Top);
            points[1] = new Point(B.Left, B.Top);

            points[2] = new Point(A.Right, A.Top);
            points[3] = new Point(B.Right, B.Top);

            points[4] = new Point(A.Left, A.Bottom);
            points[5] = new Point(B.Left, B.Bottom);

            points[6] = new Point(A.Right, A.Bottom);
            points[7] = new Point(B.Right, B.Bottom);

            Graph.DrawRectangle(Pens.Red, A);
            Graph.DrawRectangle(Pens.Blue, B);
            Graph.DrawLine(Pens.Green, points[0], points[1]);
            Graph.DrawLine(Pens.Green, points[2], points[3]);
            Graph.DrawLine(Pens.Green, points[4], points[5]);
            Graph.DrawLine(Pens.Green, points[6], points[7]);
        }

        // kesenEksen:     Yüzeyi 90 derece kesen eksen
        // kesisimNoktasi: Yüzey ile kesenEksenin kesişim noktası
        // x, y, z:        Yüzeyin merkezi bu noktaya en yakın olacak şekilde çizdirilir
        // kesenEksen:     Yüzeyi 90 derece kesen eksen
        // kesisimNoktasi: Yüzey ile kesenEksenin kesişim noktası
        // boyut:          Çizilecek yüzeyin eni ve boyu
        // x, y, z:        Yüzeyin merkezi bu noktaya en yakın olacak şekilde çizdirilir
        private void YuzeyCiz(PaintEventArgs e, Yuzey yuzey, int boyut, int x, int y, int z)
        {
            Graphics Graph = e.Graphics;

            // A -------- B
            // |          |
            // |          |
            // C -------- D
            if (yuzey.NoktaEksen == "X") // Eksenin merkezi : (kesisimNoktasi, y, z)
            {
                int aZ = z - (boyut / 2);
                int aX = centerX + yuzey.Sayi + 3 * aZ / 5;
                int aY = centerY - y - (boyut / 2) + 4 * aZ / 5;
                Point A = new Point(aX, aY);

                int bZ = z + (boyut / 2);
                int bX = centerX + yuzey.Sayi + 3 * bZ / 5;
                int bY = centerY - y - (boyut / 2) + 4 * bZ / 5;
                Point B = new Point(bX, bY);

                int cZ = z - (boyut / 2);
                int cX = centerX + yuzey.Sayi + 3 * cZ / 5;
                int cY = centerY - y + (boyut / 2) + 4 * cZ / 5;
                Point C = new Point(cX, cY);

                int dZ = z + (boyut / 2);
                int dX = centerX + yuzey.Sayi + 3 * dZ / 5;
                int dY = centerY - y + (boyut / 2) + 4 * dZ / 5;
                Point D = new Point(dX, dY);

                Graph.DrawLine(Pens.Green, A, B);
                Graph.DrawLine(Pens.Green, B, D);
                Graph.DrawLine(Pens.Green, D, C);
                Graph.DrawLine(Pens.Green, C, A);
            }
            else if (yuzey.NoktaEksen == "Y") // Eksenin merkezi : (x, kesisimNoktasi, z)
            {
                int aZ = z - (boyut / 2);
                int aX = centerX + x - (boyut / 2) + 3 * aZ / 5;
                int aY = centerY - yuzey.Sayi + 4 * aZ / 5;
                Point A = new Point(aX, aY);

                int bZ = z - (boyut / 2);
                int bX = centerX + x + (boyut / 2) + 3 * bZ / 5;
                int bY = centerY - yuzey.Sayi + 4 * bZ / 5;
                Point B = new Point(bX, bY);

                int cZ = z + (boyut / 2);
                int cX = centerX + x - (boyut / 2) + 3 * cZ / 5;
                int cY = centerY - yuzey.Sayi + 4 * cZ / 5;
                Point C = new Point(cX, cY);

                int dZ = z + (boyut / 2);
                int dX = centerX + x + (boyut / 2) + 3 * dZ / 5;
                int dY = centerY - yuzey.Sayi + 4 * dZ / 5;
                Point D = new Point(dX, dY);

                Graph.DrawLine(Pens.Green, A, B);
                Graph.DrawLine(Pens.Green, B, D);
                Graph.DrawLine(Pens.Green, D, C);
                Graph.DrawLine(Pens.Green, C, A);
            }
            else if (yuzey.NoktaEksen == "Z") // Eksenin merkezi : (x, y, kesisimNoktasi)
            {
                int eX = centerX + x - (boyut / 2) + 3 * yuzey.Sayi / 5;
                int eY = centerY - y - (boyut / 2) + 4 * yuzey.Sayi / 5;

                Rectangle duzlem = new Rectangle(eX, eY, boyut, boyut);

                Graph.DrawRectangle(Pens.Green, duzlem);
            }
        }

        private void SilindirCiz(PaintEventArgs e, Silindir s)
        {

            int aZ = s.MerkezZ - s.Yukseklik / 2;
            int aX = centerX + s.MerkezX - s.Yaricap + 3 * aZ / 5;
            int aY = centerY - s.MerkezY - s.Yaricap + 4 * aZ / 5;

            int bZ = s.MerkezZ + s.Yukseklik / 2;
            int bX = centerX + s.MerkezX - s.Yaricap + 3 * bZ / 5;
            int bY = centerY - s.MerkezY - s.Yaricap + 4 * bZ / 5;


            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Chocolate, 1);

            // Silindirin üst kısmını çiz
            g.DrawEllipse(pen, aX, aY, s.Yaricap * 2, s.Yaricap * 2);

            // Silindirin alt kısmını çiz
            g.DrawEllipse(pen, bX, bY, s.Yaricap * 2, s.Yaricap * 2);

            // Silindirin yan yüzünü çiz
            g.DrawLine(pen, aX, aY + s.Yaricap, bX, bY + s.Yaricap);
            g.DrawLine(pen, aX + 2 * s.Yaricap, aY + s.Yaricap, bX + 2 * s.Yaricap, bY + s.Yaricap);
        }
        //------------------------------------------------Form İşlemleri-------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            //numerizUpDown tools'unun max min değerlerini belirledim.
            numericUpDown1.Maximum = 700; numericUpDown1.Minimum = -300;
            numericUpDown2.Maximum = 700; numericUpDown2.Minimum = -300;
            numericUpDown3.Maximum = 700; numericUpDown3.Minimum = -300;
            numericUpDown4.Maximum = 700; numericUpDown4.Minimum = -300;
            numericUpDown5.Maximum = 700; numericUpDown5.Minimum = -300;
            numericUpDown6.Maximum = 700; numericUpDown6.Minimum = -300;
            numericUpDown7.Maximum = 700; numericUpDown7.Minimum = -300;
            numericUpDown8.Maximum = 700; numericUpDown8.Minimum = -300;
            numericUpDown9.Maximum = 700; numericUpDown9.Minimum = -300;
            numericUpDown10.Maximum = 700; numericUpDown10.Minimum = -300;
            numericUpDown11.Maximum = 700; numericUpDown11.Minimum = -300;
            numericUpDown12.Maximum = 700; numericUpDown12.Minimum = -300;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //DikdortgenPrizmaCiz(e);
            KoordinatCiz(e);
            if (comboBox1.SelectedIndex == 0)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                //Dikdörtgen sol üst köşe x ve y hesabı:
                int x = ((int)numericUpDown1.Value) - (((int)numericUpDown3.Value) / 2);
                int y = ((int)numericUpDown2.Value) - (((int)numericUpDown8.Value) / 2);

                DikdortgenCiz(e, x, y, ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), Brushes.Red);
                NoktaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value));

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                //Çemberi kapsayan dörtgenin sol üst köşe koordinat hesabı:
                int x = ((int)numericUpDown1.Value) - ((int)numericUpDown3.Value);
                int y = ((int)numericUpDown2.Value) - ((int)numericUpDown3.Value);

                CemberCiz(e, ((int)numericUpDown3.Value), x, y, Color.Blue);
                NoktaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value));
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                //Dikdörtgenlerin sol üst köşe x ve y hesabı:
                int x1 = ((int)numericUpDown1.Value) - (((int)numericUpDown3.Value) / 2);
                int y1 = ((int)numericUpDown2.Value) - (((int)numericUpDown8.Value) / 2);
                int x2 = ((int)numericUpDown4.Value) - (((int)numericUpDown6.Value) / 2);
                int y2 = ((int)numericUpDown5.Value) - (((int)numericUpDown7.Value) / 2);

                DikdortgenCiz(e, x1, y1, ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), Brushes.Red);
                DikdortgenCiz(e, x2, y2, ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), Brushes.Green);
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                //Dikdörtgen sol üst köşe x ve y hesabı:
                int x = ((int)numericUpDown1.Value) - (((int)numericUpDown3.Value) / 2);
                int y = ((int)numericUpDown2.Value) - (((int)numericUpDown8.Value) / 2);

                //Çemberi kapsayan dörtgenin sol üst köşe koordinat hesabı:
                int cX = ((int)numericUpDown4.Value) - ((int)numericUpDown6.Value);
                int cY = ((int)numericUpDown5.Value) - ((int)numericUpDown6.Value);


                DikdortgenCiz(e, x, y, ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), Brushes.Red);
                CemberCiz(e, ((int)numericUpDown6.Value), cX, cY, Color.Blue);
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                DikdortgenPrizmaCiz(e, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                NoktaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));

            }
            else if (comboBox1.SelectedIndex == 5)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                Silindir s = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));

                SilindirCiz(e, s);
                NoktaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                //Çemberi kapsayan dörtgenin sol üst köşe koordinat hesabı:
                int cX1 = ((int)numericUpDown4.Value) - ((int)numericUpDown6.Value);
                int cY1 = ((int)numericUpDown5.Value) - ((int)numericUpDown6.Value);
                int cX2 = ((int)numericUpDown1.Value) - ((int)numericUpDown3.Value);
                int cY2 = ((int)numericUpDown2.Value) - ((int)numericUpDown3.Value);

                CemberCiz(e, ((int)numericUpDown6.Value), cX1, cY1, Color.Red);
                CemberCiz(e, ((int)numericUpDown3.Value), cX2, cY2, Color.Blue);
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));

                KureCiz(e, k, Brushes.DarkGreen);
                NoktaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                Silindir s1 = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));
                Silindir s2 = new Silindir(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value));
                SilindirCiz(e, s1);
                SilindirCiz(e, s2);
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                Kure k1 = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Kure k2 = new Kure(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value));

                KureCiz(e, k1, Brushes.DarkGreen);
                KureCiz(e, k2, Brushes.Orange);

            }
            else if (comboBox1.SelectedIndex == 10)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Silindir s = new Silindir(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value));

                KureCiz(e, k, Brushes.DeepPink);
                SilindirCiz(e, s);
            }
            else if (comboBox1.SelectedIndex == 11)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);

                KureCiz(e, k, Brushes.DeepPink);
                YuzeyCiz(e, y, k.R * 2, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value));
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);
                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));

                YuzeyCiz(e, y, (dp.Yukseklik + dp.Derinlik + dp.En) / 2, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value));
                DikdortgenPrizmaCiz(e, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));

            }
            else if (comboBox1.SelectedIndex == 13)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);
                Silindir s = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));

                YuzeyCiz(e, y, (s.Yukseklik + s.Yaricap), ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value));
                SilindirCiz(e, s);
            }
            else if (comboBox1.SelectedIndex == 14)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                //Küreleri kapsayan küplerin sol ön üst köşe koordinat hesabı:
                int kX = ((int)numericUpDown4.Value) - ((int)numericUpDown7.Value);
                int kY = ((int)numericUpDown5.Value) - ((int)numericUpDown7.Value);

                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                Kure k = new Kure(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value));

                KureCiz(e, k, Brushes.Orange);
                DikdortgenPrizmaCiz(e, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
            }
            else if (comboBox1.SelectedIndex == 15)
            {
                DikdortgenCiz(e, -centerX, 900 - centerY, 800, 900, Brushes.White);
                KoordinatCiz(e);

                DikdortgenPrizmaCiz(e, ((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                DikdortgenPrizmaCiz(e, ((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value), ((int)numericUpDown12.Value));
            }
            else if (comboBox1.SelectedIndex == 16)
            {
                Sekiller Nokta = new Nokta(5, 4);
                Sekiller Dikdortgen = new Dikdortgen(10, 50, 45, 10);
                Sekiller Cember = new Cember(20, 40, 10);
                Kure Kure = new Kure(200, 200, 20, 20);
                Sekiller DikdortgenP = new DikdortgenPrizma(0, 0, 0, 40, 50, 60);
                Silindir Silindir = new Silindir(100, 20, 40, 20, 30);
                Yuzey Yuzey = new Yuzey(120, "X");

                List<Sekiller> list = new List<Sekiller>() { Nokta, Dikdortgen, DikdortgenP, Cember, Kure, Silindir, Yuzey };

                NoktaCiz(e, 5, 4);
                DikdortgenCiz(e, 10, 50, 45, 10, Brushes.DarkCyan);
                CemberCiz(e, 20, 40, 10, Color.Orange);
                KureCiz(e, Kure, Brushes.Red);
                DikdortgenPrizmaCiz(e, 0, 0, 0, 40, 50, 60);
                SilindirCiz(e, Silindir);
                YuzeyCiz(e, Yuzey, 200, 8, 6, 9);
                GorunmezYap();
                VerileriSifirla();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            //Çarpışma durumu - Çarpışma türü 
            if (comboBox1.SelectedIndex == 0)
            {
                label17.Text = "Nokta Dörtgen Denetimi";
                Nokta n = new Nokta(((int)numericUpDown4.Value), ((int)numericUpDown5.Value));
                Dikdortgen d = new Dikdortgen(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                if (CarpismaDenetimi.NoktaDortgen(n, d))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label17.Text = "Nokta Çember Denetimi";
                Nokta n = new Nokta(((int)numericUpDown4.Value), ((int)numericUpDown5.Value));
                Cember c = new Cember(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value));
                if (CarpismaDenetimi.NoktaCember(n, c))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                label17.Text = "Dikdörtgen Dikdörtgen Denetimi";
                Dikdortgen d1 = new Dikdortgen(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Dikdortgen d2 = new Dikdortgen(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value));
                if (CarpismaDenetimi.DikdortgenDikdortgen(d1, d2))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                label17.Text = "Dikdörtgen Çember Denetimi";
                Dikdortgen d = new Dikdortgen(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Cember c = new Cember(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
                if (CarpismaDenetimi.DikdortgenCember(d, c))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                label17.Text = "Nokta DikdörtgenPrizma Denetimi";
                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                Nokta n = new Nokta(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
                if (CarpismaDenetimi.NoktaDikdortgenPrizma(dp, n))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                label17.Text = "Nokta Silindir Denetimi";
                Silindir s = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));
                Nokta n = new Nokta(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
                if (CarpismaDenetimi.NoktaSilindir(s, n))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                label17.Text = "Çember Çember Denetimi";
                Cember c1 = new Cember(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value));
                Cember c2 = new Cember(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
                if (CarpismaDenetimi.CemberCember(c1, c2))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }

            }
            else if (comboBox1.SelectedIndex == 7)
            {
                label17.Text = "Nokta Küre Denetimi";
                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Nokta n = new Nokta(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value));
                if (CarpismaDenetimi.NoktaKure(k, n))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                label17.Text = "Silindir Silindir Denetimi";
                Silindir s1 = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));
                Silindir s2 = new Silindir(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value));
                if (CarpismaDenetimi.SilindirSilindir(s1, s2))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                label17.Text = "Küre Küre Denetimi";
                Kure k1 = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Kure k2 = new Kure(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value));
                if (CarpismaDenetimi.KureKure(k1, k2))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 10)
            {
                label17.Text = "Küre Silindir Denetimi";
                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Silindir s = new Silindir(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value));
                if (CarpismaDenetimi.KureSilindir(k, s))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 11)
            {
                label17.Text = "Yüzey Küre Denetimi";
                Kure k = new Kure(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value));
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);
                if (CarpismaDenetimi.YuzeyKure(y, k))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                label17.Text = "DikdörtgenPrizma Yüzey Denetimi";
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);
                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                if (CarpismaDenetimi.YuzeyDikdortgenP(y, dp))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 13)
            {
                label17.Text = "Yüzey Silindir Denetimi";
                Yuzey y = new Yuzey(((int)numericUpDown4.Value), textBox6.Text);
                Silindir s = new Silindir(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value));
                if (CarpismaDenetimi.YuzeySilindir(y, s))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }

            }
            else if (comboBox1.SelectedIndex == 14)
            {
                label17.Text = "Küre  DikdörtgenPrizma Denetimi";
                DikdortgenPrizma dp = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                Kure k = new Kure(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value));
                if (CarpismaDenetimi.KureDikdortgenP(k, dp))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }
            else if (comboBox1.SelectedIndex == 15)
            {
                label17.Text = "DikdörtgenPrizma DikdörtgenPrizma Denetimi";
                DikdortgenPrizma dp1 = new DikdortgenPrizma(((int)numericUpDown1.Value), ((int)numericUpDown2.Value), ((int)numericUpDown3.Value), ((int)numericUpDown8.Value), ((int)numericUpDown10.Value), ((int)numericUpDown11.Value));
                DikdortgenPrizma dp2 = new DikdortgenPrizma(((int)numericUpDown4.Value), ((int)numericUpDown5.Value), ((int)numericUpDown6.Value), ((int)numericUpDown7.Value), ((int)numericUpDown9.Value), ((int)numericUpDown12.Value));
                if (CarpismaDenetimi.DikdortgenPDikdortgenP(dp1, dp2))
                {
                    durum.Text = "Çarpışma gerçekleşti.";
                }
                else
                {
                    durum.Text = "Çarpışma gerçekleşmedi.";
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)//Nokta-Dörtgen
            {
                GorunmezYap();
                sekil1.Text = "Dörtgen";
                sekil2.Text = "Nokta";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "En: ";
                label4.Text = "Yükseklik: ";

                label7.Text = "X: ";
                label8.Text = "Y: ";

                label9.Visible = false;
                label10.Visible = false;
                numericUpDown6.Visible = false;
                numericUpDown7.Visible = false;
                label4.Visible = true;
                numericUpDown8.Visible = true;
                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 1)             //Nokta-Çember
            {
                GorunmezYap();

                sekil1.Text = "Çember";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Yarıçap: ";

                sekil2.Text = "Nokta";
                label7.Text = "X: ";
                label8.Text = "Y: ";

                numericUpDown8.Visible = false;
                label4.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                numericUpDown6.Visible = false;
                numericUpDown7.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 2)             //Dikdörtgen-Dikdörtgen
            {
                GorunmezYap();

                sekil1.Text = "Dikdörtgen";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "En: ";
                label4.Text = "Yükseklik: ";

                sekil2.Text = "Dikdörtgen";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "En: ";
                label10.Text = "Yükseklik: ";

                numericUpDown8.Visible = true;
                label4.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                numericUpDown6.Visible = true;
                numericUpDown7.Visible = true;
                VerileriSifirla();
                pictureBox1.Refresh();

            }
            else if (comboBox1.SelectedIndex == 3)            //Dikdörtgen-Çember
            {
                GorunmezYap();

                sekil1.Text = "Dikdörtgen";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "En: ";
                label4.Text = "Yükseklik: ";

                sekil2.Text = "Çember";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Yarıçap: ";

                label10.Visible = false;
                numericUpDown7.Visible = false;

                label4.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                numericUpDown8.Visible = true;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 4)            //Nokta-DikdörtgenPrizma
            {
                GorunmezYap();
                sekil1.Text = "Dikdörtgen Prizma";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "En: ";
                label5.Text = "Yükseklik: ";
                label6.Text = "Derinlik: ";

                sekil2.Text = "Nokta";
                label7.Text = "X: ";
                label8.Text = "Y: ";
                label9.Text = "Z: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label5.Visible = true;
                numericUpDown10.Visible = true;
                label6.Visible = true;
                numericUpDown11.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = false;
                numericUpDown7.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();

            }
            else if (comboBox1.SelectedIndex == 5)             //Nokta-Silindir
            {
                GorunmezYap();
                sekil1.Text = "Silindir";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";
                label5.Text = "Yükseklik: ";

                sekil2.Text = "Nokta";
                label7.Text = "X: ";
                label8.Text = "Y: ";
                label9.Text = "Z: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = false;
                numericUpDown7.Visible = false;
                label5.Visible = true;
                numericUpDown10.Visible = true;
                label6.Visible = false;
                numericUpDown11.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 6)            //Çember-Çember
            {
                GorunmezYap();

                sekil1.Text = "Çember";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Yarıçap: ";

                sekil2.Text = "Çember";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Yarıçap: ";

                label10.Visible = false;
                numericUpDown7.Visible = false;
                label4.Visible = false;
                numericUpDown8.Visible = false;

                label9.Visible = true;
                numericUpDown6.Visible = true;
                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 7)            //Nokta-Küre
            {
                GorunmezYap();

                sekil1.Text = "Küre";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";

                sekil2.Text = "Nokta";
                label7.Text = "X: ";
                label8.Text = "Y: ";
                label9.Text = "Z: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = false;
                numericUpDown7.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();

            }
            else if (comboBox1.SelectedIndex == 8)           //Silindir-Silindir
            {
                GorunmezYap();
                sekil1.Text = "Silindir";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";
                label5.Text = "Yükseklik: ";


                sekil2.Text = "Silindir";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Merkez Z: ";
                label10.Text = "Yarıçap: ";
                label11.Text = "Yükseklik: ";

                label5.Visible = true;
                numericUpDown10.Visible = true;
                label4.Visible = true;
                numericUpDown8.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = true;
                numericUpDown7.Visible = true;
                label11.Visible = true;
                numericUpDown9.Visible = true;

                VerileriSifirla();
                pictureBox1.Refresh();

            }
            else if (comboBox1.SelectedIndex == 9)            //Küre-Küre
            {
                GorunmezYap();
                sekil1.Text = "Küre";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";

                sekil2.Text = "Küre";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Merkez Z: ";
                label10.Text = "Yarıçap: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = true;
                numericUpDown7.Visible = true;
                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 10)         //Küre-Silindir
            {
                GorunmezYap();
                sekil1.Text = "Küre";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";

                sekil2.Text = "Silindir";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Merkez Z: ";
                label10.Text = "Yarıçap: ";
                label11.Text = "Yükseklik: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = true;
                numericUpDown7.Visible = true;
                label11.Visible = true;
                numericUpDown9.Visible = true;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 11)         //Yüzey-Küre
            {
                GorunmezYap();

                sekil1.Text = "Küre";
                label1.Text = "Sol üst köşe X: ";
                label2.Text = "Sol üst köşe Y: ";
                label3.Text = "Sol üst köşe Z: ";
                label4.Text = "Yarıçap: ";

                sekil2.Text = "Yüzey";
                label7.Text = "Kesişim Noktası: ";
                label12.Text = "Yüzeye Dik Eksen: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label12.Visible = true;
                textBox6.Visible = true;
                label8.Visible = false;
                numericUpDown5.Visible = false;
                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 12)            //Yüzey-DikdörtgenPrizma
            {
                GorunmezYap();
                sekil1.Text = "Dikdörtgen Prizma";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "En: ";
                label5.Text = "Yükseklik: ";
                label6.Text = "Derinlik: ";

                sekil2.Text = "Yüzey";
                label7.Text = "Kesişim Noktası: ";
                label12.Text = "Yüzeye Dik Eksen: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label5.Visible = true;
                numericUpDown10.Visible = true;
                label6.Visible = true;
                numericUpDown11.Visible = true;
                label12.Visible = true;
                textBox6.Visible = true;
                label8.Visible = false;
                numericUpDown5.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 13)            //Yüzey-Silindir
            {
                GorunmezYap();
                sekil1.Text = "Silindir";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "Yarıçap: ";
                label5.Text = "Yükseklik: ";

                sekil2.Text = "Yüzey";
                label7.Text = "Kesişim Noktası: ";
                label12.Text = "Yüzeye Dik Eksen: ";

                label5.Visible = true;
                numericUpDown10.Visible = true;
                label4.Visible = true;
                numericUpDown8.Visible = true;
                label12.Visible = true;
                textBox6.Visible = true;
                label8.Visible = false;
                numericUpDown5.Visible = false;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 14)            //Küre - DikdörtgenPrizma
            {
                GorunmezYap();
                sekil1.Text = "Dikdörtgen Prizma";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "En: ";
                label5.Text = "Yükseklik: ";
                label6.Text = "Derinlik: ";

                sekil2.Text = "Küre";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Merkez Z: ";
                label10.Text = "Yarıçap: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label5.Visible = true;
                numericUpDown10.Visible = true;
                label6.Visible = true;
                numericUpDown11.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = true;
                numericUpDown7.Visible = true;

                VerileriSifirla();
                pictureBox1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 15)            //DikdörtgenPrizma - DikdörtgenPrizma
            {
                GorunmezYap();
                sekil1.Text = "Dikdörtgen Prizma";
                label1.Text = "Merkez X: ";
                label2.Text = "Merkez Y: ";
                label3.Text = "Merkez Z: ";
                label4.Text = "En: ";
                label5.Text = "Yükseklik: ";
                label6.Text = "Derinlik: ";

                sekil2.Text = "Dikdörtgen Prizma";
                label7.Text = "Merkez X: ";
                label8.Text = "Merkez Y: ";
                label9.Text = "Merkez Z: ";
                label10.Text = "En: ";
                label11.Text = "Yükseklik: ";
                label21.Text = "Derinlik: ";

                label4.Visible = true;
                numericUpDown8.Visible = true;
                label5.Visible = true;
                numericUpDown10.Visible = true;
                label6.Visible = true;
                numericUpDown11.Visible = true;
                label9.Visible = true;
                numericUpDown6.Visible = true;
                label10.Visible = true;
                numericUpDown7.Visible = true;
                label11.Visible = true;
                numericUpDown9.Visible = true;
                label21.Visible = true;
                numericUpDown12.Visible = true;


                VerileriSifirla();
                pictureBox1.Refresh();
            }

        }
        //------------------------------------------------Diğer Methodlar-------------------------------------------------------------------------
        public void VerileriSifirla()
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 0;
            numericUpDown8.Value = 0;
            numericUpDown9.Value = 0;
            numericUpDown10.Value = 0;
            numericUpDown11.Value = 0;
            textBox6.Text = string.Empty;
        }
        public void GorunmezYap()
        {
            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown3.Visible = true;
            numericUpDown4.Visible = true;
            numericUpDown5.Visible = true;
            numericUpDown6.Visible = false;
            numericUpDown7.Visible = false;
            numericUpDown8.Visible = false;
            numericUpDown9.Visible = false;
            numericUpDown10.Visible = false;
            numericUpDown11.Visible = false;
            numericUpDown12.Visible = false;
            textBox6.Visible = false;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label21.Visible = false;
        }
    }
}
