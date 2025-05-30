using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public partial class Form1 : Form
    {
        // Basitçe bir örnek takvim nesnesi:
        KullaniciTakvimi takvim = new KullaniciTakvimi();

        public Form1()
        {
            InitializeComponent();
            // Hatırlatma olduğunda basit mesaj:
            takvim.Hatirlatma += Hatirlat;
            takvim.Baslat();
            Goster();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            // Çok basit: sadece başlığı göster
            MessageBox.Show(e.Baslik,"! HATIRLATMA !");
            Goster();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            takvim.Durdur();
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
    }
}
