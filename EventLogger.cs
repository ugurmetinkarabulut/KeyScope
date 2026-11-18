using System;
using System.Collections.Generic;
using System.IO;

namespace KeyScope;

public class EventLogger
{
    private readonly List<KeyRecord> records = new List<KeyRecord>();

    public bool IsRecording { get; private set; }
    public int Count => records.Count;

    public void Start()
    {
        IsRecording = true;
    }

    public void Stop()
    {
        IsRecording = false;
    }

    public KeyRecord? AddRecord(string keyName)
    {
        // Kayıt kapalıysa listeye yeni bir olay eklemiyoruz.
        if (!IsRecording)
        {
            return null;
        }

        KeyRecord record = new KeyRecord(keyName, DateTime.Now);
        records.Add(record);
        return record;
    }

    public void Clear()
    {
        records.Clear();
    }

    public void SaveToFile(string filePath)
    {
        // Her kaydı TXT dosyasında ayrı bir satıra dönüştürüyoruz.
        List<string> lines = new List<string>();
        foreach (KeyRecord record in records)
        {
            lines.Add(record.ToString());
        }

        File.WriteAllLines(filePath, lines);
    }
}
