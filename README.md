# Etkinlik Takvimi & Hatırlatıcı

**Öğrenci No & İsim**  
- 235260042 — Sudenur KEÇECİ  
- 220260044 — Halil İbrahim TURAN  

---

## Proje Özeti  
Bu Windows Forms uygulaması, kullanıcı tanımlı etkinlikleri zamanında hatırlatır, sesli uyarı verir ve uygulama kapanıp açılsa bile verileri `events.json` dosyasında saklar. Event-driven mimarisi, arayüz soyutlamaları (`IReminder`, `IZamanlayici`) ve dosya tabanlı kalıcılık ile hem basit hem de güvenilir bir hatırlatıcı sistemi sunar.

---

## Temel Özellikler  
- **Etkinlik Ekleme**: Başlık, Tarih/Saat (DateTimePicker), Açıklama  
- **Etkinlik Silme**: Seçili satırı veya “Tümünü Sil” ile toplu silme  
- **Dosya Kalıcılığı**: `events.json` ile eklenen tüm etkinlikler otomatik yüklenir ve kaydedilir  
- **Zamanlayıcı & Event**: `System.Timers.Timer` + `Hatirlatma` event’i — zamanı gelince bildirim  
- **Çoklu Hatırlatma Kuyruğu**: Birbirini beklemeden, sırada bekleyen tüm hatırlatmalar gösterilir  
- **Sesli Uyarı**: Gömülü WAV dosyası (`Properties.Resources.reminder`) ile özel ses oynatma  
- **JSON Serileştirme**: `System.Text.Json` ile okunaklı ve genişletilebilir veri formatı  
- **Modern UI**:  
  - Alt çizgili TextBox’lar, flat butonlar ve çöp-kutusu ikonu  
  - Özelleştirilmiş `DataGridView` (satır renkleri, full-row select)   

---
## Uygulama İçi Görseller
![Uygulama içi görsel11](https://github.com/user-attachments/assets/7f0c1d46-5c76-4a34-9496-ec57fa578b57)


![Uygulama içi görsel 2](https://github.com/user-attachments/assets/4e7cbdbc-d5f0-4fdb-b981-94d264bef452)

