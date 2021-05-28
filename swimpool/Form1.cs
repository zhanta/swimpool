using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Swimpool
{

    public partial class Form1 : Form
    {

        int beton = 14000; // цена 14000 1куб
        int zakazBetona = 30000; // цена заказа бетона 30 000 - 40 000
        
        double wPool; // ширина бассейна
        double lPool; // длина бассейна
        double hPool;
        double cenaDoski = 150 * 1000; // кол Досок примерный +-
        int sim = 2000*12; // сым акшасы

        Boolean podogrevPool; // Будет еще насос, фонарики скиммер лестница или ступеньки.
        Boolean fonari;
        Boolean filter;
        double ElectricProvoda = 15000; // цена для одной пачки(ну хз пока)
        int workers = 8000000; // Цена для услуги


        public static double numArmatura(double kg) // штук арматуры
        {
            return (81.6 * kg) / 1000; // 81,6шт - 1 тонна арматуры это 14мм 
        }

        public static double cenaArmatura(int kg)
        {
            return (237820*kg) / 1000; // 237820тг - 1т цена в интернете
        }

        public static double vBetonPool(double w, double l, double h)
        { // объем бетона для бассейна
            return (w * l * h) / 4;
        }
        public static double vPool(double w, double l, double h)
        {
            return w * l * h;
        }
        public static double sumKgArma(double v) // 15 для ленточного бетона
        {
            return 20 * v; // возвращает кг Арматур для бетона
        }
        public static double numOfPlitki(double w, double l, double h) // 16000 за кв.м
        {
            double Ss, Sp;
            Ss = (2 * (w + l)) * h; // площадь весь бассейна
            Sp = 0.3 * 0.2; // площадь 1й плитки
            double numPlitki = Ss / Sp;
            int zapasPlitki = (int)numPlitki / 10;
            return numPlitki + zapasPlitki; // кол плиток с запасом
        }

        public static double cenaPlitki(double w, double l, double h)
        {
            double Ss = (2 * (w + l)) * h;
            return 9000 * Ss;
        }

        public static double IzoMaterials(double Ss)
        {
            double S_izo_Mat = 1.2 * 1;
            double Ss_total = Ss + (Ss / 4);
            double numIzoMat = Ss_total / S_izo_Mat;
            return numIzoMat;
        }
        public static double setkiMaterials(double Ss)
        {
            double S_setka_Mat = 0.7 * 0.8;
            double Ss_total = Ss + (Ss / 4);
            double numSetkiMat = Ss_total / S_setka_Mat;
            return numSetkiMat;
        }
        public static double cenaIzoSetkiMaterials(double w, double l, double h) //2500тг 1кв м
        {
            double S1 = l*w;
            double S2 = h * l;
            double S3 = h * w;
            return 2500 * (S1 + S2 + S3+(S1/10+S2/10+S3/10));
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)//Расчитать
        {
            hPool = Convert.ToDouble(textBox1.Text);
            wPool = Convert.ToDouble(textBox3.Text);
            lPool = Convert.ToDouble(textBox2.Text);

            if (checkBox1.Checked)
                podogrevPool = true;
            else
                podogrevPool = false;
            if (checkBox2.Checked)
                fonari = true;
            else
                fonari = false;
            if (checkBox3.Checked)
                filter = true;
            else
                filter = false;

            double obBetonPool = vBetonPool(wPool, lPool, hPool);
            double obPool = vPool(wPool, lPool, hPool);
            double cenaBeton = 14000 * obBetonPool; //Бетон цена
            Int32 numArma = Convert.ToInt32(numArmatura(sumKgArma(obBetonPool)));
            double cenaArma = cenaArmatura((int)sumKgArma(obBetonPool)); //Арматура цена
            double kgArma = sumKgArma(obBetonPool);
            Int32 numIzoMat = Convert.ToInt32(IzoMaterials(obPool));
            Int32 numSetkiMat = Convert.ToInt32(setkiMaterials(obPool));
            double cenaIzoSetki = cenaIzoSetkiMaterials(wPool, lPool, hPool);
            double cenaFilter = 0;
            double cenaFonari = 0;
            double cenaPodogrevatel = 0;
            double cenaPlitkov = cenaPlitki(wPool, lPool, hPool);
            double cenaTruby = 2000 *(wPool + lPool + hPool); //Примерный затрат поскольку не знаешь сколько потребуется
            listBox1.Items.Add("Бетон: " + obBetonPool +
                "- объем, " + cenaBeton + "тг за бетон, за доставку " + zakazBetona + "тг");
            listBox1.Items.Add("");
            listBox1.Items.Add("Арматура: " + numArma + "шт, " + kgArma + "кг, " + cenaArma + "тг;");
            listBox1.Items.Add("");
            listBox1.Items.Add("Изо. Материалы: " + numIzoMat + "шт; Сетки: " + numSetkiMat + "шт; Общ цена: "
                + cenaIzoSetki + "тг");
            listBox1.Items.Add("");
            listBox1.Items.Add("Трубы: " + cenaTruby + "тг;");
            listBox1.Items.Add("");
            listBox1.Items.Add("Плитки: " + numOfPlitki(wPool,lPool,hPool)+"шт; " + cenaPlitkov  +" тг");
            listBox1.Items.Add("Пиломатериалы: "+ cenaDoski+"тг;");

            if (lPool >= 8 && wPool >= 4)
                workers = 8000000;
            else if (lPool < 8 && lPool >= 6 && wPool < 4 && wPool >= 3)
                workers = 6500000;
            else
                workers = 5000000;

            if (filter)
            {
                if (lPool >= 8)
                {
                    cenaFilter = 120000;
                    listBox1.Items.Add("Фильтр: " + cenaFilter + " тг;");
                }
                else
                {
                    cenaFilter = 65000;
                    listBox1.Items.Add("Фильтр: " + cenaFilter + " тг;");
                }
            }
            else
                listBox1.Items.Add("Фильтр: нету");
            if (fonari)
            {
                if (lPool >= 8)
                {
                    cenaFonari = 6 * 25000;
                    listBox1.Items.Add("Фонари: 6шт, " + cenaFonari +"тг;");
                }
                else
                {
                    cenaFonari = 4 * 15000;
                    listBox1.Items.Add("Фонари: 4шт, " + cenaFonari + "тг;");
                }
            }
            else
            {
                listBox1.Items.Add("Фонари: нету");
            }
            if(podogrevPool)
            {
                cenaPodogrevatel = 95000;
                listBox1.Items.Add("Подогреватель: " + cenaPodogrevatel + "тг;");
            }
            else
                listBox1.Items.Add("Подогреватель: нету");
            listBox1.Items.Add("");
            listBox1.Items.Add("Плата за услугу:" + workers + "тг");
            listBox1.Items.Add("---------------------------------------------------------");
            double obshCena = cenaBeton + zakazBetona + cenaArma + cenaIzoSetki + cenaTruby + cenaPlitkov + workers+sim + cenaFilter+cenaFonari+cenaPodogrevatel + cenaDoski;
            listBox1.Items.Add("Общ. цена: " + obshCena +"тг.");
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Height
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Length
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Width
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//Подогрев
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)// Фонари
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //listBox
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
