using System;

namespace KeyScope;

public class KeyRecord
{
    public string KeyName { get; }
    public DateTime Timestamp { get; }

    public KeyRecord(string keyName, DateTime timestamp)
    {
        KeyName = keyName;
        Timestamp = timestamp;
    }

    public override string ToString()
    {
        return Timestamp.ToString("yyyy-MM-dd HH:mm:ss") + " | " + KeyName;
    }
}
