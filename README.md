# KeyScope

Fırat Üniversitesi Yazılım Mühendisliği 2. sınıf **Nesne Tabanlı Programlama (YMH219)** dersi için hazırlanmış Windows Forms klavye olayları demonstrasyonu.

**Öğrenci adı soyadı:** …

**Öğrenci numarası:** …

## Teknolojiler

C#, .NET 10 ve Windows Forms. Arayüz kod üzerinden oluşturulur; harici tasarım kütüphanesi veya uygulama NuGet paketi kullanılmaz. Hedef sistem Windows 10/11'dir.

## Özellikler

- Başlat ve Durdur ile kayıt kontrolü; başlangıçta kayıt kapalıdır.
- Formun KeyDown olaylarında tuş adı ve yerel tarih/saat kaydı.
- Koyu temalı ListBox, renkli durum etiketi, toplam olay sayısı ve son tuş göstergesi.
- Temizle ile hem bellek hem ekran kayıtlarını ve göstergeleri sıfırlama.
- Kaydet ile kullanıcının seçtiği TXT dosyasına dışa aktarma; iptal ve hata yönetimi.

Temizle kayıt durumunu değiştirmez. Durdur eski kayıtları silmez. Tuşu basılı tutunca Windows'un ürettiği tekrarlanan KeyDown olayları ayrı ayrı sayılır. Kaydedilen bilgi tuş kodunun adıdır; yazılmış metin veya klavye düzenine göre üretilen karakter değildir. Windows Forms bazı gezinme/kısayol tuşlarını kontrol komutu olarak işleyebilir; yalnızca forma ulaşan KeyDown olayları kaydedilir.

## Güvenlik kapsamı

Yalnızca KeyScope penceresine gelen olaylar kullanılır; form aktif değilken olay kabul edilmez. Global keyboard hook, başka uygulamalardan kayıt, gizli kayıt, otomatik başlatma, ağ üzerinden gönderme veya otomatik dosya kaydı yoktur. Parola/hassas bilgi giriş alanı bulunmaz. Kaydetme penceresi ve mesaj kutuları ana formun dışında olduğundan bu sırada tuşlar kaydedilmez. Kayıtlar uygulama kapatılınca bellekten kaybolur; saklamak için Kaydet'e basılmalıdır.

## Windows üzerinde çalıştırma

1. Windows 10/11 bilgisayara .NET 10 SDK kurun.
2. Terminali proje klasöründe açın.
3. `dotnet restore KeyScope.sln` komutunu çalıştırın.
4. `dotnet build KeyScope.sln` komutunu çalıştırın.
5. `dotnet run --project KeyScope.csproj` komutunu çalıştırın.
6. Başlat'a tıklayıp pencere aktifken birkaç tuşa basın; Durdur ve Kaydet'i deneyin.

Alternatif olarak .NET 10 ve Windows masaüstü geliştirmeyi destekleyen Visual Studio ile `KeyScope.sln` dosyasını açın ve çalıştırın.

## macOS üzerinde derleme

Projede `TargetFramework=net10.0-windows`, `UseWindowsForms=true` ve `EnableWindowsTargeting=true` kullanılır. macOS üzerinde .NET 10 SDK ile restore/build yapılabilir; Windows Desktop referans paketi için ilk restore sırasında NuGet erişimi gerekebilir. Windows Forms uygulaması macOS üzerinde çalıştırılamaz. Derleme, Windows arayüz testi yerine geçmez.

## Dosyalar

- `Form1.cs`: Arayüz, form klavye olayları ve butonlar.
- `KeyRecord.cs`: Tek bir tuş olayının adı, zamanı ve metin biçimi.
- `EventLogger.cs`: Bellekteki liste, kayıt durumu ve TXT yazımı.
- `Program.cs`: Standart uygulama giriş noktası; ana formu açar.
- `KOD_ACIKLAMALARI.md`: Ders kavramlarının kodla açıklaması.
- `TEST_CHECKLIST.md`: Yapılan kontroller ve Windows'ta kalan testler.

## Gerçek ekran görüntüleri

Henüz eklenmedi. Aşağıdaki iki görüntü Windows üzerinde gerçek uygulamadan alınacaktır; temsili görsel kullanılmamıştır.

1. **Kayıt aktif:** Tuş listesi, toplam sayı ve son tuş görünürken alınacak ekran görüntüsü.
2. **Kayıt durduruldu:** Önceki kayıtlar korunurken alınacak ekran görüntüsü.

## GitHub

Yerel kaynak dosyaları GitHub'a yüklemeye hazırlanır. Uzak depo oluşturma ve push ayrı bir talimatla yapılacaktır.
