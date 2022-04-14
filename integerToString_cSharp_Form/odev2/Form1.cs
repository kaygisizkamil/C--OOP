/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
** NESNEYE DAYALI PROGRAMLAMA DERSİ
** 2021-2022 BAHAR DÖNEMİ
**
** ÖDEV NUMARASI..........:2
** ÖĞRENCİ ADI............:Kamil Kaygısız
** ÖĞRENCİ NUMARASI.......:B211210381
** DERSİN ALINDIĞI GRUP...:1/B
****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev2
{
    public partial class Form1 : Form
    {
        //gerekli label ve textboxlar declare edildi
        Button btnHesapla = new Button();
        TextBox txtSayi = new TextBox();
        Label labelx = new Label();
        Label labely = new Label();
        Label lblYazi = new Label();
      
        //TextBox txtSayi2 = new TextBox();
        public Form1()
        {
            InitializeComponent();
            NumericString num1 = new NumericString();
            //donusturulecek stringin nesnesi olusturuldu.

            //hizalamalar yapildi.
            labelx.Top = 100;
            labelx.Left = 270;
            labelx.Size=new Size(15, 15);
            labelx.BorderStyle = BorderStyle.FixedSingle;
            labelx.Text = "X";
            this.Controls.Add(labelx);

            txtSayi.Top = 100;
            txtSayi.Left = 300;
            txtSayi.KeyPress += txtSayi_KeyPress;
            this.Controls.Add(txtSayi);

            labely.Top = 150;
            labely.Left = 270;
            labely.Size = new Size(15, 15);
            labely.BorderStyle = BorderStyle.FixedSingle;
            labely.Text = "Y";
            this.Controls.Add(labely);

            lblYazi.Top = 150;
            lblYazi.Left = 300;
            lblYazi.Text = "                               ";
            lblYazi.BorderStyle = BorderStyle.FixedSingle;
            lblYazi.AutoSize = true;
            this.Controls.Add(lblYazi);

            btnHesapla.Text = "Hesapla";
            btnHesapla.Top = 200;
            btnHesapla.Left = 300;
            btnHesapla.Click += new EventHandler(hesapla);//tiklanmasi durumunda hesapla methodu çalışacak
            this.Controls.Add(btnHesapla);
    
           

        }
        void hesapla(object sender ,EventArgs e) 
        {
            NumericString sayi = new NumericString();
            bool a = false;
            a=sayi.HowMuchResult(txtSayi.Text);
            if (a)//Dogru deger girildiyse girilen dogru degeri yazdir
            {
                
                    lblYazi.Text = sayi.HowMuch;
             
            }
            else
            {//yanlis input girildiyse message boxa yazdir
                MessageBox.Show(sayi.HowMuch);
            }
        }
        void txtSayi_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)13)
            {
                btnHesapla.PerformClick(); 
            }
        }
    }
}
