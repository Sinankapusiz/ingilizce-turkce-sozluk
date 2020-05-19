using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; //vt baglanabilmek için eklenmesi gerekli
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ders8_VTUygulama
{
    public partial class Form1 : Form
    {
        //baglantı cumlesi global deg. olarak tanımlandı. Bu cumle mdb vt için geçerli.
        OleDbConnection baglantim = new OleDbConnection("Provider= Microsoft.JET.OleDb.4.0;Data Source=" + Application.StartupPath + "\\sozluk.mdb");   
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ingilizce,turkce;
            turkce=textBox2.Text;
            ingilizce=textBox1.Text;
            try
            {
                baglantim.Open(); // baglantı acıldı
                // ekleme komutu, baglantı nesnesini kullanarak oluşuturuldu.
                OleDbCommand eklekomutu = new OleDbCommand("insert into IngTurk (ingilizce,turkce) values('" + ingilizce + "','" + turkce + "')", baglantim);
                eklekomutu.ExecuteNonQuery();  // ekleme komutu çalıştırıldı.
                baglantim.Close(); // işi biten bağlantı kapatılmalı.
                MessageBox.Show("Başarı ile eklendi");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception aciklama)
            {
                // bir hata varsa yakalanması için try-catch
                MessageBox.Show(aciklama.Message, "Bir hata oluştu!");
                baglantim.Close();
            }
        }

        private void guncelle(object sender, EventArgs e)
        {
            string ingilizce, turkce;
            turkce = textBox2.Text;
            ingilizce = textBox1.Text;
            try
            {
                baglantim.Open();// baglantı acıldı
                // guncelleme komutu , baglantı nesnesi kullanarak oluşturuldu.
                OleDbCommand guncellekomutu = new OleDbCommand("update IngTurk set turkce= '"+turkce+"' where ingilizce='"+ingilizce+"' ", baglantim);
                guncellekomutu.ExecuteNonQuery(); // komut çalıştırıldı.
                baglantim.Close(); // bağlantı kapatıldı
                MessageBox.Show("Sözcük başarı ile güncellendi");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception aciklama)
            {

                MessageBox.Show(aciklama.Message, "Bir hata oluştu!");
                baglantim.Close();
            }
        }

        private void sil(object sender, EventArgs e)
        {
            string ingilizce = textBox1.Text;
            try
            {
                baglantim.Open();// baglantı acıldı
                // silme komutu baglantı nesnesi ile oluşturuldu.
                OleDbCommand silkomutu = new OleDbCommand("delete from IngTurk where ingilizce= '" + ingilizce + "' ", baglantim);
                silkomutu.ExecuteNonQuery(); // sil komutu çalıştırıldı.
                baglantim.Close();
                MessageBox.Show("Sözcük başarı ile silindi");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception aciklama)
            {

                MessageBox.Show(aciklama.Message, "Bir hata oluştu!");
                baglantim.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
            //textboxa bir kelime yazılmaya başlandığı anda çalışacak ve o harfle başlayan kelimeleri anlık olarak tarayacak
            try
            {
                listBox1.Items.Clear();
                baglantim.Open();
                // aramakomutu vtde textbox içersine yazılan harf/kelime ye benzer bir kelimeleri çeker
                OleDbCommand aramakomutu=new OleDbCommand("select ingilizce,turkce from IngTurk where ingilizce like '" + textBox1.Text + "%'",baglantim);
                OleDbDataReader oku = aramakomutu.ExecuteReader();
                // burada çekilen sonuçlar vtdeki tablonun bir görüntüsü gibidir.
                //dolayısı ile tek bir değişken gibi tutulamaz. o yüzden datareader nesnesi tanımlanır.vtden çekilen alanlar datareader içerisinde saklanır.
                while (oku.Read()) // datareader tablosu dolu olduğu sürece dön
                {
                    // oku datareaderın içerisindeki alanlardan "ingilizce" ve "türkce" olan alanları al,listbox içersine yaz.
                    listBox1.Items.Add(oku["ingilizce"].ToString() +"=" +oku ["turkce"].ToString());
                }
                baglantim.Close();
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
