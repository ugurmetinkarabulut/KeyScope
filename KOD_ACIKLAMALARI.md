# Kod açıklamaları

## 1. KeyDown olayı ne işe yarar?

Bir tuşa basıldığında çalışır. `Form1.cs` içinde `KeyDown += Form1_KeyDown;` satırı olayı metodumuza bağlar. `e.KeyCode.ToString()` basılan tuşun adını verir. Tuş basılı tutulursa birden fazla olay oluşabilir.

## 2. KeyPreview nedir?

`KeyPreview = true;` sayesinde odak ListBox veya butonda olsa da form, KeyDown olayını önce alabilir. Kontrollerin gezinme/kısayol olarak işlediği her tuş mutlaka KeyDown üretmez. Bu özellik sistem genelindeki tuşları dinlemez.

## 3. KeyRecord sınıfını neden oluşturduk?

Tuş adı ve zamanı aynı nesnede tutmak için. `public string KeyName { get; }` ve `public DateTime Timestamp { get; }` özellikleri tek olayı temsil eder. `ToString()` liste ve dosyada aynı biçimin kullanılmasını sağlar.

## 4. EventLogger sınıfının görevi nedir?

Arayüzden bağımsız kayıt yönetimidir. Formdaki `private readonly EventLogger logger = new EventLogger();` satırı bu sınıftan bir nesne oluşturur. `Start()`, `Stop()`, `AddRecord()`, `Clear()` ve `SaveToFile()` işlemlerini form bu nesne üzerinden çağırır.

## 5. List<KeyRecord> ne işe yarar?

`private readonly List<KeyRecord> records = new List<KeyRecord>();` birden çok kayıt tutan field'dır (alandır). `records.Add(record)` ekler, `records.Clear()` siler. `Count` property'si `records.Count` değerini dışarı verir. `readonly`, liste alanının başka bir listeyle değiştirilmesini engeller; listeye ekleme ve silme yapılabilir.

## 6. DateTime ile zamanı nasıl alıyoruz?

`new KeyRecord(keyName, DateTime.Now)` bilgisayarın yerel zamanını alır. `Timestamp.ToString("yyyy-MM-dd HH:mm:ss")` zamanı yıl-ay-gün saat:dakika:saniye biçiminde gösterir. `ToString()` sonucu örneğin `2026-11-18 21:04:12 | Space` olur.

## 7. bool ile kayıt durumunu nasıl kontrol ediyoruz?

`public bool IsRecording { get; private set; }` başlangıçta false'tur. `Start()` true, `Stop()` false yapar. `AddRecord()` içindeki `if (!IsRecording)` kayıt kapalıysa `null` döndürür. Form da bu durumda ekranı güncellemeden çıkar. `KeyRecord?`, sonuçta kayıt nesnesi veya null olabileceğini belirtir.

## 8. Kayıtları TXT dosyasına nasıl yazıyoruz?

`SaveToFile()` önce `records.Count == 0` kontrolüyle boş listeyi reddeder. `foreach (KeyRecord record in records)` döngüsü her nesneyi `lines.Add(record.ToString())` ile metne dönüştürür. `File.WriteAllLines(filePath, lines)` bunları ayrı satırlara yazar. İşlem sadece Kaydet üzerinden çağrılır. Seçilen mevcut dosya, diyalogdaki üzerine yazma onayı sonrası değiştirilir.

## 9. SaveFileDialog ne işe yarar?

Kullanıcının konum ve dosya adı seçmesini sağlar. `dialog.Filter = "Metin dosyası (*.txt)|*.txt";` dosya türünü gösterir. `dialog.ShowDialog(this) != DialogResult.OK` ise metottan çıkılır; iptalde dosya yazılmaz. `using`, diyalog işi bittiğinde kaynaklarının bırakılmasını sağlar.

## 10. try-catch neden kullanılır?

Dosya yazma izni olmayabilir veya seçilen konum kullanılamayabilir. Formdaki `try` içinde `logger.SaveToFile(dialog.FileName)` çağrılır. `catch (Exception ex)` hatayı yakalar ve MessageBox ile açıklama gösterir; uygulama beklenmedik biçimde kapanmaz.

## 11. Constructor nedir?

Nesne oluşturulurken çalışan, sınıfla aynı adlı metottur. `public KeyRecord(string keyName, DateTime timestamp)` aldığı değerleri `KeyName = keyName;` ve `Timestamp = timestamp;` ile özelliklere atar. `Form1()` constructor'ı ise arayüzü oluşturur. `Program.Main()` standart giriş noktasıdır; ders modeline ek bir kayıt sınıfı değildir.

## 12. Uygulama neden başka pencerelerdeki tuşları kaydetmez?

Yalnızca formun `KeyDown` olayına aboneyiz. Global klavye dinleyicisi yoktur. `if (ActiveForm != this || !ContainsFocus || !Enabled)` koşulu ana form aktif değilse, odak kendisinde/alt kontrollerinde değilse veya form devre dışıysa metottan çıkar. Modal diyaloglar açıkken ana form devre dışıdır. `KeyPreview` sadece formun kendi alt kontrollerini kapsar. Başlat açık kalsa bile başka uygulamadaki tuşlar kaydedilmez.

## 13. Class, Object, public ve private

`EventLogger` bir class (sınıf), `new EventLogger()` ile oluşturduğumuz `logger` bir object'tir (nesne). `public void Start()` formdan çağrılabilir. `private readonly List<KeyRecord> records` yalnızca EventLogger içinde kullanılabilir. Böylece form listeyi doğrudan değiştiremez.

## 14. get ve private set

`IsRecording { get; private set; }` dışarıdan okunabilir ama yalnızca kendi sınıfından değiştirilebilir. Form `logger.IsRecording` değerini okuyabilir, ona doğrudan değer atayamaz. `KeyName { get; }` constructor'da atanır ve sonradan değiştirilemez.

## 15. ToString ve override

Her nesnenin metin karşılığını veren bir `ToString()` metodu vardır. `public override string ToString()` ile KeyRecord için bu davranışı değiştiririz. Böylece ListBox'a nesnenin kendisini eklediğimizde sınıf adı yerine tarih ve tuş adı görünür.

## 16. Boş kayıtları nasıl engelliyoruz?

`saveButton` form alanıdır; farklı olay metotları aynı butona erişebilir. `UpdateRecordingStatus()` içindeki `saveButton.Enabled = logger.Count > 0;` ilk kayıt ve Temizle sonrasında da çalışır. Kaydet olayında ikinci bir boşluk kontrolü vardır. Modelde `throw new InvalidOperationException(...)` ise boş listeyle dosya yazılmasını engeller; hata oluşursa formdaki catch bunu gösterebilir.

## Öğretmenin isteyebileceği beş küçük değişiklik

Bu örnekler alıştırmadır; uygulamaya eklenmemiştir.

1. **Tarihte saniyeden sonra milisaniye göster.** KeyRecord.ToString içindeki biçimi `"yyyy-MM-dd HH:mm:ss.fff"` yap.
2. **Kayıtları dosyada en yeniden eskiye yaz.** SaveToFile içindeki foreach yerine `for (int i = records.Count - 1; i >= 0; i--) { lines.Add(records[i].ToString()); }` kullan. Ekrandaki sıra değişmez.
3. **Son tuş göstergesinin adını değiştir.** Form1'deki üç `"Son tuş: ..."` metnini `"Son basılan: ..."` olarak değiştir; başlangıç, ekleme ve Temizle birbiriyle tutarlı kalır.
4. **Başlangıç pencere boyutunu değiştir.** Form1 constructor'ındaki `ClientSize = new Size(880, 600);` satırını `new Size(960, 640)` olarak değiştir. Anchor ayarları listeyi büyütür.
5. **Shift olaylarını listeye ekleme.** Form1_KeyDown'da AddRecord çağrısından önce `if (e.KeyCode == Keys.ShiftKey) { return; }` ekle. Böylece Shift kendi başına sayılmaz; diğer tuşlar normal kaydedilir.
