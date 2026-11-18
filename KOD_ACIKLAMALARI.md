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

`SaveToFile()` içindeki `foreach (KeyRecord record in records)` döngüsü her nesneyi `lines.Add(record.ToString())` ile metne dönüştürür. `File.WriteAllLines(filePath, lines)` bunları ayrı satırlara yazar. İşlem sadece Kaydet üzerinden çağrılır. Seçilen mevcut dosya, diyalogdaki üzerine yazma onayı sonrası değiştirilir.

## 9. SaveFileDialog ne işe yarar?

Kullanıcının konum ve dosya adı seçmesini sağlar. `dialog.Filter = "Metin dosyası (*.txt)|*.txt";` dosya türünü gösterir. `dialog.ShowDialog(this) != DialogResult.OK` ise metottan çıkılır; iptalde dosya yazılmaz. `using`, diyalog işi bittiğinde kaynaklarının bırakılmasını sağlar.

## 10. try-catch neden kullanılır?

Dosya yazma izni olmayabilir veya seçilen konum kullanılamayabilir. Formdaki `try` içinde `logger.SaveToFile(dialog.FileName)` çağrılır. `catch (Exception ex)` hatayı yakalar ve MessageBox ile açıklama gösterir; uygulama beklenmedik biçimde kapanmaz.

## 11. Constructor nedir?

Nesne oluşturulurken çalışan, sınıfla aynı adlı metottur. `public KeyRecord(string keyName, DateTime timestamp)` aldığı değerleri `KeyName = keyName;` ve `Timestamp = timestamp;` ile özelliklere atar. `Form1()` constructor'ı ise arayüzü oluşturur. `Program.Main()` standart giriş noktasıdır; ders modeline ek bir kayıt sınıfı değildir.

## 12. Uygulama neden başka pencerelerdeki tuşları kaydetmez?

Yalnızca formun `KeyDown` olayına aboneyiz. Global klavye dinleyicisi yoktur. Ayrıca `if (ActiveForm != this || !ContainsFocus)` koşulu ana form aktif değilken metottan çıkar. `KeyPreview` sadece formun kendi alt kontrollerini kapsar. Başlat açık kalsa bile başka uygulamadaki tuşlar kaydedilmez.
