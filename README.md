# LinqTask

## إزاي تشغله

```powershell
Update-Database
```

## املا الداتا

حط السطرين دول في أول `Main` في `Program.cs`:

```csharp
db.Database.Migrate();
SeedData.Seed(db);
```

## الأسئلة

كل سؤال جوه `#region` في `Program.cs` وعليه comment. حضرتك تشيل الـ comment من السؤال اللي عايز تجربه، ويفضل تجرب سؤال واحد كل مرة عشان الـ output ما يتلخبطش.
