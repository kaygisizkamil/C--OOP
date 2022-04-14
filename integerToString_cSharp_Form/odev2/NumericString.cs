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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev2
{



    
        public class NumericString
        {
           public static readonly string[] ones = { "", "BİR", "İKİ", "ÜÇ", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
           public static readonly string[] tens = { "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            const string hundred = "YÜZ";
            const string cent = "KURUŞ";
            const string lira = "TL";
            const string thousands = "BİN";
            const int MAX = 100000;

            private string howMuch;//kullanıcıdan alinan ilk string versiyonu
            private double moneyy;//double a cevirilip uzerinde islem yapilacak versiyon
            public string HowMuch//return edilecek ve label icine yazdirilacak string
            {
                set
                {
                    this.howMuch = value;
                }
                get
                {
                    return this.howMuch;
                }
            }
            public double Moneyy//functionlar arasi gecislerde miktar kiyaslamalarinda kullanilacak howMany'in double versiyonu
            {
                set
                {
                    this.moneyy = value;
                }
                get
                {
                    return this.moneyy;
                }
            }

            public bool isMoneyValid(string quantity, ref bool hasComma)
            {//kullanicidan string olarak alindi ref kullanmamin sebebi virgul var mi yok mu kontrol ve sonraki sefer icin bu degerin kaydını yapabilmek,alike pointer
                double moneyTemp;
                bool isValid = false;
                HowMuch = quantity;
                string tmp;//asil deger daha sonra kullanilma ihtimali uzerine tutuluyor islemler kopya uzerinde yapiliyor
                int numOfComma = 0;
                foreach (char c in quantity)
                {
                    if ('.'.Equals(c))
                    {
                        numOfComma++;//sayinin duzgun formatta girilen bir string oldugundan emin olmak icin kullanildi
                    }
                }
                tmp = quantity;
            
                if (double.TryParse(tmp, out moneyTemp))// kullanicidian degeri string olarak aldik daha sonra islemler icin doublea parse ettik.
                {
                    Moneyy = moneyTemp;
                    if (MAX > Moneyy)
                    {// toplam miktar 100000 den az olmali
                        if (Moneyy > 0)
                        {
                            if (numOfComma == 1)//dogru format icin bir adet , gerekir
                            {//decimal bir sayi ise virgulden sonraki basamak sayisini da kontrol etmek gereklidir
                                hasComma = true;//decimal sayi mi kontrol icin ref argumani gecirildi varsa true return edecek
                                if (HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length <= 2 && HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length > 0)
                                {
                                //virgulden sonra max 2 basamak varsa
                                     isValid = true;

                                }
                                else//gecersiz girilen degerlerde detaylı hata outputu verebilmek icin HowMuch properties'i tekrar baska bir degere atandi.
                                {

                                    HowMuch = "Noktadan(decimal point) sonra en az bir,en fazla iki basamak olabilir";
                                    isValid = false;
                                }
                            }
                            else
                            {// decimal degilse virgulden sonrası yok ama format true bool =true
                             //HowMuch için yeni bir deger atamasi yapmaya gerek yok cunku input dogru formatta
                                hasComma = false;
                                isValid = true;
                            }
                        }
                        else
                        {
                            HowMuch = "Sayı sıfırdan büyük olmalıdır.";
                            isValid = false;
                        }
                    }

                    else
                    {
                        HowMuch = "Sayı 100.000'den küçük olmalıdır.";
                        isValid = false;//sayi double a pars edilemezse diger sartlara bakmaya gerek yok
                    }
                }
                else
                {
                    isValid = false;
                    HowMuch = "Geçerli formatta giriş yapiniz(XXXXX.XX)";

                }
                if (quantity.Contains(','))
                {
                    HowMuch = "Geçerli formatta giriş yapiniz(XXXXX.XX)";
                    isValid = false;
                }
                return isValid;// tüm sartlar kontrol edildi artik gecerlilik degerini return edebiliriz
            }
    
            public string oneDigit(int beforeComma)
            {//sayi bir basamakli ise birler basamagini return eder
                string word;
            if (beforeComma != 0)
                word = String.Concat(ones[beforeComma], " ", lira, " ");
            else word = "";
                return word;
            }
            public string twoDigit(int beforeComma)
            {//sayi iki basamakliysa sayiyi komple stringe cevirip return eder
                string word;//return ifadesinin cok karisik olmamasi icin gecici degiskene atayip o sekilde return ediyoruz
                int tmp = beforeComma;
                // tmp %= 10;//sayiyi
                //burasi onlar bas birler basmagini ustteki fonksiyonda bulduk concat sonrasi return ediyoruz
                if (beforeComma >= 10)
                    word = String.Concat(tens[beforeComma / 10 - 1], " ", oneDigit(tmp % 10));

                else if (beforeComma > 0 && beforeComma < 10)
                    word = String.Concat(oneDigit(tmp));
                else
                    word = String.Concat(lira," ");


                return word;

            }
            public string threeDigit(int beforeComma)
            {
                string word;
                int tmp = beforeComma - (beforeComma / 100 * 100);//birler ve onlar basamagini buradan cekeriz (ilk iki digit)
                if (beforeComma != 0)
                {
                    if (beforeComma / 100 == 1)
                    {                     //two digit fonksiyonuna iki basamagi gonderdik iki basamagını orasi return edecek
                                          //bir basamakli oldugu icin bir yüz yazamayiz if kontrolünün sebebi budur
                        word = String.Concat(hundred, " ", twoDigit(tmp));
                    }
                    else
                    {
                        word = String.Concat(ones[beforeComma / 100], " ", hundred, " ", twoDigit(tmp));
                    }
                }
                else
                {
                    word = String.Concat(lira," ");
                }
                return word;
            }
            public string fourDigit(int beforeComma)
            {
                int tmp = beforeComma - (beforeComma / 1000 * 1000);//ilk 3 digiti cekmeyi saglar
                string word;
                if (beforeComma != 0)
                {
                    if (beforeComma / 1000 == 1)
                    {
                        word = String.Concat(thousands, " ", threeDigit(tmp));
                    }
                    else
                    {
                        word = String.Concat(ones[beforeComma / 1000], " ", thousands, " ", threeDigit(tmp));

                    }
                }
                else
                    word = String.Concat(thousands, " ", lira," ");
                return word;

            }
            public string fiveDigit(int beforeComma)
            {
                string word;
                int tmp = beforeComma - (beforeComma / 10000 * 10000);//ilk4 digiti cekmeyi saglar
                word = String.Concat(tens[beforeComma / 10000 - 1], " ", fourDigit(tmp));
                return word;
            }
            public string decimalDigit(int afterComma,int AcLength)//virgulden sonraki basamak ayisini da index olarak aliyoruz ki 0.1 ve 0.01 ayrimi saglansin
            {
            
                string word;
            if (AcLength == 2)
            {
                if (afterComma > 0 && afterComma < 10)
                {
                    word = String.Concat(ones[afterComma], " ", cent, " ");
                }
                else if (afterComma >= 10 && afterComma <= 99)
                {
                    word = String.Concat(tens[afterComma / 10 - 1], " ", ones[afterComma % 10], " ", cent, " ");
                }
                else
                {
                    word = "";
                }
            }
            else// sayi dogru formatta geldiginden emin oldugumuz icin direkt olarak else ile 2 degilse yani 1 se seklinde atama yapabiliriz
            {
                if (afterComma > 0 && afterComma < 10)
                {
                    word = String.Concat(tens[afterComma-1], " ", cent, " ");
                }
                else
                {
                    word = "";
                }
            }
           
                return word;
            }


            public bool HowMuchResult(string numberS)//kullanicidan para miktarini string olarak aldim ki daha rahat islem yapabileyim
            {
                bool mesageBoxReturn=false;
                int afterComma;
                int beforeComma;
                int digitNum;
                bool hasComma = false;
                string properMoney = "";
                int afterCommaLength;// bu decimal kisimdan sonra 1 01 ayrimini saglamak icin yapilir bir kurus on kurus

                mesageBoxReturn=isMoneyValid(numberS, ref hasComma);//girilen miktar geçerli mi
                if (mesageBoxReturn)//girilen fatura miktari gecerli bir string ise
                {
                    //
                    if (numberS.Contains('.'))//virgul varsa hasComma degeri  true return eder
                    {
                                  //detaylı hata outputu icin contexten bagımsız olarak bu kontrol gerekliydi.
                                    if (String.IsNullOrEmpty(HowMuch.Substring(0, HowMuch.IndexOf('.'))))
                                    {
                                        mesageBoxReturn = false;
                                        HowMuch = "Noktadan önce sayı girişi sağlamadınız tekrar deneyin";
                                        return mesageBoxReturn;
                                    }
                        beforeComma = int.Parse(HowMuch.Substring(0, HowMuch.IndexOf('.')));//burasi virgul oncesi yani ondalik olmayan kisim                        
                        digitNum = beforeComma.ToString().Length;//kaç basamakli oldugunu buluo ilgili functiona gonderiyoruz.
                        if (digitNum == 1)
                        {
                            if (hasComma)
                            {
                                afterCommaLength=HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length;
                                afterComma = int.Parse(HowMuch.Substring(HowMuch.IndexOf('.') + 1));//burası virgul sonrası yani ondalik kisim
                                properMoney = oneDigit(beforeComma) + decimalDigit(afterComma,afterCommaLength);
                            }
                            else
                            {
                                properMoney = oneDigit(beforeComma);
                            }
                        }
                        else if (digitNum == 2)
                        {
                            if (hasComma)
                            {
                                afterCommaLength = HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length;
                                afterComma = int.Parse(HowMuch.Substring(HowMuch.IndexOf('.') + 1));//burası virgul sonrası yani ondalik kisim
                                properMoney = twoDigit(beforeComma) + decimalDigit(afterComma,afterCommaLength);
                            }
                            else
                            {
                                properMoney = twoDigit(beforeComma);
                            }
                        }
                        else if (digitNum == 3)
                        {
                            if (hasComma)
                            {
                                afterCommaLength = HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length;
                                afterComma = int.Parse(HowMuch.Substring(HowMuch.IndexOf('.') + 1));//burası virgul sonrası yani ondalik kisim
                                properMoney = threeDigit(beforeComma) + decimalDigit(afterComma, afterCommaLength);
                            }
                            else
                            {
                                threeDigit(beforeComma);
                            }
                        }
                        else if (digitNum == 4)
                        {
                            if (hasComma)
                            {
                                afterCommaLength = HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length;
                                afterComma = int.Parse(HowMuch.Substring(HowMuch.IndexOf('.') + 1));//burası virgul sonrası yani ondalik kisim
                                properMoney = fourDigit(beforeComma) + decimalDigit(afterComma, afterCommaLength);
                            }
                            else
                            {
                                properMoney = fourDigit(beforeComma);
                            }
                        }
                        else if (digitNum == 5)
                        {
                            if (hasComma)
                            {
                                afterCommaLength = HowMuch.Substring(HowMuch.IndexOf('.') + 1).Length;
                                afterComma = int.Parse(HowMuch.Substring(HowMuch.IndexOf('.') + 1));//burası virgul sonrası yani ondalik kisim
                                properMoney = fiveDigit(beforeComma) + decimalDigit(afterComma, afterCommaLength);
                            }
                            else
                            {
                                properMoney = fiveDigit(beforeComma);
                            }
                        }
                    }
                    else if (!numberS.Contains('.'))//sayida virgul yoksa bu if bogu icine girer orengin 26532 virgul yok output yirmialtibinbesyuzotuziki olmali
                    {
                        beforeComma = int.Parse(HowMuch);//iki ayri formatta input girisi oldugu icin tekrar deger atamasi yapildi.
                        digitNum = beforeComma.ToString().Length;//kaç basamakli oldugunu buluo ilgili functiona gonderiyoruz.

                        //beforeComma
                        if (digitNum == 1)
                        {

                            properMoney = oneDigit(beforeComma);

                        }
                        else if (digitNum == 2)
                        {

                            properMoney = twoDigit(beforeComma);

                        }
                        else if (digitNum == 3)
                        {

                            properMoney = threeDigit(beforeComma);

                        }
                        else if (digitNum == 4)
                        {

                            properMoney = fourDigit(beforeComma);

                        }
                        else if (digitNum == 5)
                        {

                            properMoney = fiveDigit(beforeComma);

                        }
                    }
                    HowMuch = properMoney;
                }
            return mesageBoxReturn;
        }

            /*else
            {// diger secenekler icin HowMuch 'i yeni bir degere atamaya gerek yok ustte detayli kontrol yaparken atamalar yapildi gereklii hata mesaji ustte yazdirilacak
                HowMuch = "Lütfen x,y formatında y en fazla 2 ,sayi 100000 den kucuk olacak sekilde giris saglayin\n";
            }*/
            
        }
    

}
