# Test kontrol listesi

## macOS üzerinde yapılan kontroller — 18 Kasım 2026

- [x] .NET SDK 10.0.401 mevcut.
- [x] `dotnet restore KeyScope.sln`: başarılı (çıkış kodu 0).
- [x] `dotnet build KeyScope.sln --no-restore`: başarılı, 0 hata, 1 uyarı.
- [x] Hedef `net10.0-windows`; Windows Forms ve Windows targeting etkin.

**Uyarı:** Restore ve build sırasında NU1900 görüldü: NuGet güvenlik açığı verileri için `https://api.nuget.org/v3/index.json` adresine erişilemedi. Derleme tamamlandı ancak bu veri kontrolü tamamlanmış sayılmaz. Ağ erişimi olan ortamda restore tekrar yapılmalıdır.

### Gerçek model sınıflarıyla çalıştırılan mantık kontrolleri

macOS'ta geçici bir `net10.0` konsol projesi, bu projenin gerçek `KeyRecord.cs` ve `EventLogger.cs` dosyalarını doğrudan derlemeye dahil etti. Ek test paketi kullanılmadı; uygulama projesine test sınıfı eklenmedi. Aşağıdaki **14 kontrolün tamamı geçti**:

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
- [x] Boş listeyle kaydetme, mevcut dosyanın içeriğini boşaltıyor.
- [x] Temizleme sonrasında yeni kayıt eklenebiliyor.
- [x] Kapalıyken Temizle kayıt durumunu kapalı tutuyor.
- [x] Olmayan klasöre yazma hatası çağırana iletiliyor.

Geçici dosya test sonunda silindi. Bu kontroller form, MessageBox veya SaveFileDialog çalıştırmaz. Kaydetme hatasının arayüzde gösterilmesi Windows'ta ayrıca denenmelidir.

## Windows 10/11 üzerinde yapılacaklar — henüz çalıştırılmadı

- [ ] .NET 10 SDK ile solution restore/build yap; NU1900 için ağ erişimini kontrol et.
- [ ] Uygulamayı terminalden veya Visual Studio'dan aç.
- [ ] Başlangıçta “Kayıt durduruldu”, toplam 0 ve “Son tuş: —” görünüyor.
- [ ] Başlat öncesinde tuşlara basınca kayıt oluşmuyor.
- [ ] Başlat sonrasında harf, sayı, Space, Shift ve Ctrl olayları listeleniyor.
- [ ] Her olayın zamanı ve tuş adı doğru; toplam ve son tuş güncelleniyor.
- [ ] Tuşu basılı tutunca tekrar olaylarının her biri sayılıyor.
- [ ] Odak ListBox'tayken ve butonlardayken forma gelen KeyDown olayları yakalanıyor.
- [ ] Tab, Enter, Escape ve ok tuşlarının gezinme/kısayol davranışını kontrol et; kontrollerin komut olarak işlediği tuşlar KeyDown üretmeyebilir.
- [ ] Durdur sonrasında yeni kayıt oluşmuyor; eski kayıtlar korunuyor.
- [ ] Yeniden Başlat eski kayıtların sonuna ekliyor.
- [ ] Aktifken ve durmuşken Temizle listeyi, toplamı ve son tuşu sıfırlıyor; durum değişmiyor.
- [ ] Başlat açıkken başka uygulamaya geçip yazınca KeyScope sayısı değişmiyor.
- [ ] KeyScope'a dönünce kayıt devam ediyor.
- [ ] Kaydet diyaloğu açıkken ve mesaj kutularında yazılanlar kaydedilmiyor.
- [ ] TXT kaydı seçilen konuma yazılıyor; içerik listedeki sırayla aynı.
- [ ] Kaydet diyaloğunda İptal dosya oluşturmuyor ve hata göstermiyor.
- [ ] Mevcut dosyanın üzerine yazma onayı ve iptali doğru çalışıyor.
- [ ] Yazma izni olmayan konumda anlaşılır hata mesajı gösteriliyor; uygulama açık kalıyor.
- [ ] Boş liste kaydı boş bir TXT oluşturuyor.
- [ ] Pencere yeniden boyutlandırıldığında kontroller üst üste binmiyor.
- [ ] %100, %125 ve %150 ekran ölçeklerinde etiketler ve butonlar okunuyor.
- [ ] Uzun listede yeni kayıt görünür kalıyor, liste kaydırılabiliyor.
- [ ] Uygulamayı kapatıp açınca kayıtlar ve durum sıfırlanıyor.
- [ ] README'deki öğrenci adı ve numarası dolduruluyor.
- [ ] Kayıt aktif ve durdurulmuş durumların iki gerçek ekran görüntüsü README'ye ekleniyor.

**Windows arayüzü macOS üzerinde çalıştırılmadı. Yukarıdaki Windows maddeleri başarılı olarak işaretlenmemiştir.**
