# Etkinlik Takvimi & Hatırlatıcı

- 235260042  Sudenur KEÇECİ  
- 220260044  Halil İbrahim TURAN  

---

## Proje Hakkında  
Bu Windows Forms uygulaması, kullanıcının belirlediği tarih ve saatlerde gerçekleşecek etkinlikleri listeleyen ve zamanı geldiğinde otomatik olarak hatırlatma yapan basit bir etkinlik takvimi sistemidir.

### Temel Özellikler  
- Etkinlik ekleme (Başlık, Tarih, Açıklama)  
- Etkinlik silme  
- Zamanlayıcı tabanlı otomatik hatırlatma (popup mesaj)  
- Bellek içi çalışma (şimdilik dosya kaydı yok)  
- Modern görünümlü arayüz (alt çizgili TextBox’lar, flat butonlar, özelleştirilmiş DataGridView)

---
### Geliştirme Notları
-KullaniciTakvimi sınıfı içinde System.Windows.Forms.Timer ile her 10 saniyede bir Etkinlikler listesi taranır.

-Zamanı gelen ilk etkinlik Hatirlatma event’i ile Form1’e bildirilir ve listeden kaldırılır.

#### İlerleyen sürümlerde:

-Dosya veya veritabanı tabanlı kalıcılık

-Tray icon veya sistem izinli bildirimler (toast)

-Etkinlik düzenleme ve filtreleme

-Unit test’ler eklenmesi planlanmaktadır.

### Teknolojiler
-C# /.NET Framework

-Windows Forms

-System.Windows.Forms.Timer

-Visual Studio 2022 veya üzeri


