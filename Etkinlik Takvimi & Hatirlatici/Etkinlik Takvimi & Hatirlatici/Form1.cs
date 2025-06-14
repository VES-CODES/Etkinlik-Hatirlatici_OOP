﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public partial class Form1 : Form
    {
        // Basitçe bir örnek takvim nesnesi:
        // KullaniciTakvimi takvim = new KullaniciTakvimi();
        private const string DosyaAdi = "Takvim.json";
        private readonly KullaniciTakvimi takvim;
        public Form1()
        {
            InitializeComponent();
            takvim = new KullaniciTakvimi(this);     // Form1 kendisini syncObj olarak ver
            takvim.Hatirlatma += Hatirlat;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            takvim.Baslat();
            Goster();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEventsFromFile();
            dgvEtkinlikler.AutoGenerateColumns = true;
            takvim.Baslat();
            Goster();
        }

        private void Goster()
        {
            // DataGridView’i sıfırla, sonra yeniden ata
            dgvEtkinlikler.DataSource = null;
            dgvEtkinlikler.DataSource = takvim.Listele().ToList();
            // Id gösterilmesin
            dgvEtkinlikler.Columns["Id"].Visible = false;
        }

        private void Hatirlat(Etkinlik e)
        {
            // takvim.Durdur();
             using (var player = new System.Media.SoundPlayer(Properties.Resources.reminder))
             {
                 player.Play();  
             }/*
               * Bu kısımda hata oluşuyor zamanı gelen etkinliğin messageboxı kapanmadan diğeri kapanmıyor.
             var result = MessageBox.Show(
               e.Baslik,
              "! HATIRLATMA !",
             MessageBoxButtons.OK,
             MessageBoxIcon.Information

     );


             if (result == DialogResult.OK)
             {
                 takvim.Sil(e.Id);       // takvimden sil
                 Goster();               // grid'i yenile
                 SaveEventsToFile();     // dosyaya kaydet
             }
             takvim.Baslat();
             Goster();*/
           
            takvim.Sil(e.Id);
            Goster();
            SaveEventsToFile();

            // Her uyarıyı kendi STA thread'inde gösteriyorum
            var t = new System.Threading.Thread(() =>
            {
                
                System.Media.SystemSounds.Exclamation.Play();

                // Modal MessageBox ama yalnızca bu thread'i bloke ediyo
                System.Windows.Forms.MessageBox.Show(
                    $"{e.Baslik}\n{e.Tarih:yyyy-MM-dd HH:mm}\n{e.Aciklama}",
                    "! HATIRLATMA !",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information
                );
            });

            t.IsBackground = true;
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveEventsToFile();
            takvim.Durdur();
        }
        private void LoadEventsFromFile()
        {
            // events.json dosyasının yolu
            string path = System.IO.Path.Combine(
                System.Windows.Forms.Application.StartupPath,
                DosyaAdi
            );

            if (!System.IO.File.Exists(path))
                return;

            try
            {
                // Dosyayı string olarak oku (otomatik kapanıyor)
                string json = System.IO.File.ReadAllText(path);

                // JSON’dan List<Etkinlik> oluştur
                var liste = System.Text.Json.JsonSerializer
                             .Deserialize<System.Collections.Generic.List<Etkinlik>>(json);

                if (liste != null)
                {
                    foreach (var ev in liste)
                        takvim.Ekle(ev);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Etkinlikler yüklenirken hata:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SaveEventsToFile()
        {
            string path = System.IO.Path.Combine(
                System.Windows.Forms.Application.StartupPath,
                DosyaAdi
            );

            try
            {
                // JSON seçenekleri
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Listeyi JSON’a çevir
                string json = System.Text.Json.JsonSerializer
                              .Serialize(takvim.Listele(), options);

                // Dosyaya yaz (otomatik kapanır)
                System.IO.File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Etkinlikler kaydedilirken hata:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            Etkinlik yeni = new Etkinlik();
            yeni.Baslik = txtBaslik.Text;

            DateTime tarih;
            if (!DateTime.TryParse(txtTarih.Text, out tarih))
            {
                MessageBox.Show("Tarih formatı yanlış!");
                return;
            }
            yeni.Tarih = tarih;
            yeni.Aciklama = txtAciklama.Text;

            takvim.Ekle(yeni);
            Goster();
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            // Geçerli bir satır seçili değilse çık
            if (dgvEtkinlikler.CurrentRow == null) return;

            var secilen = dgvEtkinlikler.CurrentRow.DataBoundItem as Etkinlik;
            if (secilen == null) return;

            // Takvim listesinden çıkar ve grid’i yenile
            takvim.Sil(secilen.Id);
            Goster();

        }

        private void dgvEtkinlikler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEtkinlikler.SelectedRows.Count == 0) return;

            // İlk seçili satır:
            var row = dgvEtkinlikler.SelectedRows[0];

            // DataBoundItem doğrudan Etkinlik nesnesini tutar:
            if (row.DataBoundItem is Etkinlik secilen)
            {
                txtBaslik.Text = secilen.Baslik;
                txtTarih.Text = secilen.Tarih.ToString("yyyy-MM-dd HH:mm");
                txtAciklama.Text = secilen.Aciklama;
            }
        }

        private void dgvEtkinlikler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Başlık satırı veya geçersiz tıklama için çık
            if (e.RowIndex < 0) return;

            // Seçili satırın bağlı nesnesini al
            var secilen = dgvEtkinlikler.Rows[e.RowIndex].DataBoundItem as Etkinlik;
            if (secilen == null) return;

            // TextBox’ları doldur
            txtBaslik.Text = secilen.Baslik;
            txtTarih.Text = secilen.Tarih.ToString("yyyy-MM-dd HH:mm");
            txtAciklama.Text = secilen.Aciklama;
        }

        private void btnTumunuSil_Click(object sender, EventArgs e)
        {
            // İsteğe bağlı onay penceresi:
            var cevap = MessageBox.Show(
                "Tüm etkinlikleri silmek istediğinize emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (cevap != DialogResult.Yes) return;

            //  uyarı gelmesin diye
            takvim.Durdur();

            
            takvim.Listele().Clear();

           
            Goster();

            // 4) Dosyaya kaydet
            SaveEventsToFile();

            // Timerı yeniden başlat
            takvim.Baslat();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
