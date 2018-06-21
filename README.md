# transliterated2arabic
C# code that takes a transliterated data string and converts it to its Arabic equivalent.

The code will take transliterated data like: 
Jamāl ʻAbd al-Nāṣir wa-al-ḥarakāt al-siyāsīyah fī ʻUmān

And output:
[ز]مال ع[ز]بد ال[ز]اصر والحركات السياسيه في ع[ز]مان

The code is based off of a PERL script created by Mark Muehlhaeusler, who graciously allowed the code to be used and ported into C#.

Usage in C#:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arabic_translit
{
    class Program
    {
        static void Main(string[] args)
        {
            string test_string = "Jamāl ʻAbd al-Nāṣir wa-al-ḥarakāt al-siyāsīyah fī ʻUmān";
            clsArabicTransliteration objArabic = new clsArabicTransliteration();
            System.IO.File.WriteAllText(@"c:\users\reese\desktop\arabic_output.txt", objArabic.Transliterated2Arabic(test_string), new System.Text.UTF8Encoding(false));
            Console.WriteLine("finished");
            Console.Read();
        }
    }
}
```

