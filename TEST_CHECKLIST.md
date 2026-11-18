# Test kontrol listesi

## macOS üzerinde yapılan kontroller — 18 Kasım 2026

- [x] .NET SDK 10.0.401 mevcut.
- [x] `dotnet restore KeyScope.sln`: başarılı (çıkış kodu 0).
- [x] `dotnet build KeyScope.sln --no-restore`: son çalıştırma başarılı, 0 hata, 0 uyarı.
- [x] Hedef `net10.0-windows`; Windows Forms ve Windows targeting etkin.

**NU1900 incelemesi:** İlk restore/build sırasında NuGet güvenlik açığı verileri için `https://api.nuget.org/v3/index.json` adresine erişilemedi. Ağ erişimi verilerek `dotnet restore KeyScope.sln --force` tekrarlandı ve uyarısız tamamlandı. Ardından build 0 uyarı, 0 hata verdi. Güvenlik denetimi kapatılmadı.

### Gerçek model sınıflarıyla çalıştırılan mantık kontrolleri

macOS'ta geçici bir `net10.0` konsol projesi, bu projenin gerçek `KeyRecord.cs` ve `EventLogger.cs` dosyalarını doğrudan derlemeye dahil etti. Ek test paketi kullanılmadı; uygulama projesine test sınıfı eklenmedi. Önceki kontroller, yeni boş liste korumasına göre güncellendi. Aşağıdaki **16 kontrolün tamamı geçti**:

- [x] Başlangıçta kayıt kapalı, liste boş.
- [x] Kapalıyken ekleme reddediliyor, sayı değişmiyor.
- [x] Başlat ve tekrar Başlat kayıt durumunu açıyor.
- [x] Tuş adı ve `DateTime.Now` ile yerel zaman korunuyor.
- [x] İki olay sayıyı ikiye çıkarıyor.
- [x] Bilinen zamanla tam çıktı biçimi: `2026-11-18 21:04:12 | Space`.
- [x] Durdur ve tekrar Durdur kayıtları koruyor; yeni olay reddediliyor.
- [x] TXT satır sırası, içerik ve Türkçe `Ö` karakteri geri okunarak doğrulanıyor.
- [x] Yeniden başlatma eski kayıtları koruyor.
- [x] Aktifken Temizle listeyi sıfırlıyor, kayıt durumu aktif kalıyor.
- [x] Boş listeyle kaydetme reddediliyor; mevcut dosyanın içeriği korunuyor.
- [x] Boş listeyle kaydetme yeni dosya oluşturmuyor.
- [x] Reddedilen kaydetme kayıt durumunu ve boş listeyi değiştirmiyor.
- [x] Temizleme sonrasında yeni kayıt eklenebiliyor.
- [x] Kapalıyken Temizle kayıt durumunu kapalı tutuyor.
- [x] Liste doluyken olmayan klasöre yazma hatası çağırana iletiliyor.

Geçici dosya test sonunda silindi. Bu kontroller form, MessageBox veya SaveFileDialog çalıştırmaz. Kaydetme hatasının arayüzde gösterilmesi Windows'ta ayrıca denenmelidir.

### Kod ve depo denetimi

- [x] Kaydet başlangıçta, her yeni kayıtta ve Temizle sonrasında `logger.Count > 0` ile güncelleniyor (kod incelemesi).
- [x] Kaydet olayında dosya diyaloğundan önce boş liste kontrolü var.
- [x] ActiveForm ve ContainsFocus korundu; devre dışı ana form için Enabled kontrolü eklendi.
- [x] Durdur listeyi silmiyor; Temizle kayıt durumunu değiştirmiyor.
- [x] Alt açıklama kısaltıldı, son tuş etiketi genişliği pencereye bağlandı; görsel sonuç Windows'ta doğrulanacak.
- [x] `git check-ignore` ile `bin/`, `obj/`, `.vs/`, `.DS_Store` ve iki farklı TXT dosya adı yok sayılıyor.
- [x] Takip edilen dosyalarda gerçek tuş kaydı veya kullanıcı TXT verisi yok.

## Windows 10/11 üzerinde yapılacaklar — henüz çalıştırılmadı

- [ ] .NET 10 SDK ile solution restore/build yap; NU1900 için ağ erişimini kontrol et.
- [ ] Uygulamayı terminalden veya Visual Studio'dan aç.
- [ ] Başlangıçta “Kayıt durduruldu”, toplam 0 ve “Son tuş: —” görünüyor; Kaydet ve Durdur pasif, Başlat etkin.
- [ ] İlk tuş kaydıyla Kaydet etkinleşiyor; Durdur sonrasında kayıtlar varsa etkin kalıyor.
- [ ] Başlat öncesinde tuşlara basınca kayıt oluşmuyor.
- [ ] Başlat sonrasında harf, sayı, Space, Shift ve Ctrl olayları listeleniyor.
- [ ] Her olayın zamanı ve tuş adı doğru; toplam ve son tuş güncelleniyor.
- [ ] Tuşu basılı tutunca tekrar olaylarının her biri sayılıyor.
- [ ] Odak ListBox'tayken ve butonlardayken forma gelen KeyDown olayları yakalanıyor.
- [ ] Tab, Enter, Escape ve ok tuşlarının gezinme/kısayol davranışını kontrol et; kontrollerin komut olarak işlediği tuşlar KeyDown üretmeyebilir.
- [ ] Durdur sonrasında yeni kayıt oluşmuyor; eski kayıtlar korunuyor.
- [ ] Yeniden Başlat eski kayıtların sonuna ekliyor.
- [ ] Aktifken ve durmuşken Temizle listeyi, toplamı ve son tuşu sıfırlıyor; durum değişmiyor.
- [ ] Temizle sonrasında Kaydet pasif oluyor; yeniden kayıt gelince etkinleşiyor.
- [ ] Başlat açıkken başka uygulamaya geçip yazınca KeyScope sayısı değişmiyor.
- [ ] KeyScope'a dönünce kayıt devam ediyor.
- [ ] Kaydet diyaloğu açıkken ve mesaj kutularında yazılanlar kaydedilmiyor.
- [ ] TXT kaydı seçilen konuma yazılıyor; içerik listedeki sırayla aynı.
- [ ] Kaydet diyaloğunda İptal dosya oluşturmuyor ve hata göstermiyor.
- [ ] Mevcut dosyanın üzerine yazma onayı ve iptali doğru çalışıyor.
- [ ] Yazma izni olmayan konumda anlaşılır hata mesajı gösteriliyor; uygulama açık kalıyor.
- [ ] Boş listede Kaydet kullanılamıyor; dosya diyaloğu açılmıyor, dosya oluşmuyor/değişmiyor.
- [ ] Pencere yeniden boyutlandırıldığında kontroller üst üste binmiyor.
- [ ] Minimum pencere boyutunda son tuş ve alt açıklama kesilmiyor; pasif butonlar anlaşılır görünüyor.
- [ ] %100, %125 ve %150 ekran ölçeklerinde etiketler ve butonlar okunuyor.
- [ ] Uzun listede yeni kayıt görünür kalıyor, liste kaydırılabiliyor.
- [ ] Uygulamayı kapatıp açınca kayıtlar ve durum sıfırlanıyor.
- [ ] README'deki öğrenci adı ve numarası dolduruluyor.
- [ ] Kayıt aktif ve durdurulmuş durumların iki gerçek ekran görüntüsü README'ye ekleniyor.

**Windows arayüzü macOS üzerinde çalıştırılmadı. Yukarıdaki Windows maddeleri başarılı olarak işaretlenmemiştir.**
