using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arabic_translit
{
    class clsArabicTransliteration
    {
        /*
         * Transliterated2Arabic is based on PERL code originally written by
         * Mark Muehlhaeusler <mpm97@georgetown.edu> 
         */
        public string Transliterated2Arabic(string record)
        {
            string[] lines = record.Split("\n".ToCharArray());
            string tmp_line = "";
            string tmp_record = "";

            foreach (string line in lines)
            {
                tmp_line = line;
                tmp_line = tmp_line.TrimStart(new char[] { '\uFEFF', '\u200B' });
                //#STEP 1 disinguish 'ibn and reduce all plain capitals to lower case
                /*
                 * $line = ~s /\b\x{ 0069}\x{ 0062}\x{ 006E} / bn / g; #escape lower-case 'ibn'
                   $line = ~tr / A - Z / a - z /;
                 */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0069\u0062\u006E", "bn");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"[A-Z]", m => m.ToString().ToLower());


                /*
                 *
                 *#STEP 1A CONVERTING AYN and HAMZAH - NECESSARY TO AVOID CONFUSON WITH WORD-INITALS

                    $line=~s/\x{02BB}/\x{0063}/g; #ayn to c
                    $line=~s/\x{2019}/\x{004F}/g; #change hamzah to capital o ...02BC
                    $line=~s/\x{2032}/\x{004F}/g;
                    $line=~s/\x{0027}/\x{004F}/g;

                    $line=~s/\x{02BC}/\x{004F}/g; #alternate representations of Hamzah as apostrophe
                    $line=~s/\bO//g; #... and delete word-initial ones
                 */
                tmp_line = tmp_line.Replace("\u02BB", "\u0063");
                tmp_line = tmp_line.Replace("\u2019", "\u004F");
                tmp_line = tmp_line.Replace("\u2032", "\u004F");
                tmp_line = tmp_line.Replace("\u0027", "\u004F");
                tmp_line = tmp_line.Replace("\u02BC", "\u004F");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bO", "");

                /*
                 * 
                 * #STEP 2 CONVERT INTIAL LONG VOWELS AND DIPHTONGS
                #A - long vowels

                $line=~s/\b\x{012B}/\x{0045}\x{0079}/g;	#lower case i macrons to Ey alif hamzah plus ya
                $line=~s/\b\x{012A}/\x{0045}\x{0079}/g;	#upper case i macrons to Ey alif hamzah plus ya  
                $line=~s/\b\x{016B}/\x{004C}\x{0077}/g; 	#lower case u macrons to Lw alif hamzah plus waw
                $line=~s/\b\x{016A}/\x{004C}\x{0077}/g; 	#upper case u macrons to Lw alif hamzah plus waw
                $line=~s/\b\x{0101}/\x{004D}/g; 	#lower case a macrons to M maddah 
                $line=~s/\b\x{0100}/\x{004D}/g; 	#upper case a macrons to M maddah
                 * 
                 */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u012B", "\u0045\u0079");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u012A", "\u0045\u0079");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u016B", "\u004C\u0077");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u016A", "\u004C\u0077");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0101", "\u004D");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0100", "\u004D");

                /*
                 * #C - diphtongs word-/ sentence-initial, after dash (diphtongs already lower case at this point)

                $line=~s/\b\x{0061}\x{0079}/\x{004C}\x{0079}/g;	#ay to Ly alif sup hamzah plus ya 
                $line=~s/\b\x{0061}\x{0077}/\x{004C}\x{0077}/g; 	#aw to Lw alif sup hamzah plus waw

                */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0061\u0079", "\u004C\u0079");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0061\u0077", "\u004C\u0077");

                /*
                 * #STEP 2A CONVERT INITIAL SHORT VOWELS TO ALIF

                $line=~s/\b\x{0061}/\x{004C}/g;	#initial a to L alif sup hamzah
                $line=~s/\b\x{0075}/\x{004C}/g;	#initial u to L alif sup hamzah
                $line=~s/\b\x{0069}/\x{0045}/g;	#initial i to A alif sub hamzah

                #STEP 3 reduce all remaining long vowels to placeholders

                $line=~s/\x{0100}/\x{0041}/g; 
                $line=~s/\x{0101}/\x{0041}/g;
                #all a macrons to capital a

                $line=~s/\x{012A}/\x{0079}/g;
                $line=~s/\x{012B}/\x{0079}/g;
                #all i macrons to y

                $line=~s/\x{016A}/\x{0077}/g;
                $line=~s/\x{016B}/\x{0077}/g;
                #all u macrons to w 
                */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0061", "\u004C");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0075", "\u004C");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\b\u0069", "\u0045");

                tmp_line = tmp_line.Replace("\u0100", "\u0041");
                tmp_line = tmp_line.Replace("\u0101", "\u0041");

                tmp_line = tmp_line.Replace("\u012A", "\u0079");
                tmp_line = tmp_line.Replace("\u012B", "\u0079");

                tmp_line = tmp_line.Replace("\u016A", "\u0077");
                tmp_line = tmp_line.Replace("\u016B", "\u0077");

                /*
                 #STEP X REMOVE INITIAL HAMZAHS

                $line=~s/\bOE/E/g;
                $line=~s/\bOL/L/g;
                $line=~s/\bOM/M/g;
                $line=~s/\bOA/A/g;

                #STEP   CONVERT HAMZAH

                $line=~s/Oi/Y/g;
                $line=~s/iO/Y/g;
                $line=~s/Oy/Yy/g;
                $line=~s/yO/yY/g;
                $line=~s/uO/W/g;
                $line=~s/Ow/Ww/g;
                $line=~s/aO/L/g;
                $line=~s/AOa/AC/g;
                $line=~s/AOA/ACA/g;
                $line=~s/OA/M/g;
                $line=~s/AO/AC/g;
                $line=~s/Oa/L/g;
                $line=~s/O/C/g;
                */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bOE", "E");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bOL", "L");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bOM", "M");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bOA", "A");

                tmp_line = tmp_line.Replace("Oi", "Y");
                tmp_line = tmp_line.Replace("iO", "Y");
                tmp_line = tmp_line.Replace("Oy", "Yy");
                tmp_line = tmp_line.Replace("yO", "yY");
                tmp_line = tmp_line.Replace("uO", "W");
                tmp_line = tmp_line.Replace("Ow", "Ww");
                tmp_line = tmp_line.Replace("aO", "L");
                tmp_line = tmp_line.Replace("AOa", "AC");
                tmp_line = tmp_line.Replace("AOA", "ACA");
                tmp_line = tmp_line.Replace("OA", "M");
                tmp_line = tmp_line.Replace("AO", "AC");
                tmp_line = tmp_line.Replace("Oa", "L");
                tmp_line = tmp_line.Replace("O", "C");

                /*
                 * 
                 #STEP 4 reduce all diacritics to placeholders
                $line=~s/\x{1E24}/\x{0048}/g; 
                $line=~s/\x{1E25}/\x{0048}/g;
                #all h dots to capital h

                $line=~s/\x{1E0C}/\x{0044}/g;
                $line=~s/\x{1E0D}/\x{0044}/g;
                #all d dots to capital d

                $line=~s/\x{1E62}/\x{0053}/g;
                $line=~s/\x{1E63}/\x{0053}/g;
                #all s dots to capital s

                $line=~s/\x{1E6C}/\x{0054}/g;
                $line=~s/\x{1E6D}/\x{0054}/g;
                #all t dots to capital t

                $line=~s/\x{1E92}/\x{005A}/g;
                $line=~s/\x{1E93}/\x{005A}/g;
                #all z dots to capital z

                $line=~s/\x{00E1}/\x{0065}/g;
                #all alif maqsurah to e

                $line=~s/\x{02BB}/\x{0063}/g;
                #converting ayn to c
                */
                tmp_line = tmp_line.Replace("\u1E24", "\u0048");
                tmp_line = tmp_line.Replace("\u1E25", "\u0048");

                tmp_line = tmp_line.Replace("\u1E0C", "\u0044");
                tmp_line = tmp_line.Replace("\u1E0D", "\u0044");

                tmp_line = tmp_line.Replace("\u1E62", "\u0053");
                tmp_line = tmp_line.Replace("\u1E63", "\u0053");

                tmp_line = tmp_line.Replace("\u1E6C", "\u0054");
                tmp_line = tmp_line.Replace("\u1E6D", "\u0054");

                tmp_line = tmp_line.Replace("\u1E92", "\u005A");
                tmp_line = tmp_line.Replace("\u1E93", "\u005A");

                tmp_line = tmp_line.Replace("\u00E1", "\u0065");

                tmp_line = tmp_line.Replace("\u02BB", "\u0063");

                /*
                 * #STEP    CONVERT DIGRAPHS
                $line=~s/gh/g/g;
                $line=~s/sh/K/g;
                $line=~s/kh/x/g;
                $line=~s/th/V/g;
                $line=~s/dh/X/g;
                $line=~s/ch/C/g;

                #STEP   CONVERT TA MARBUTAH

                $line=~s/ah\b/aQ/g;
                $line=~s/Ah\b/AQ/g;
                */

                tmp_line = tmp_line.Replace("gh", "g");
                tmp_line = tmp_line.Replace("sh", "K");
                tmp_line = tmp_line.Replace("kh", "x");
                tmp_line = tmp_line.Replace("th", "V");
                tmp_line = tmp_line.Replace("dh", "X");
                tmp_line = tmp_line.Replace("ch", "C");

                //Console.WriteLine(tmp_line);
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"ah\b", "aQ");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"Ah\b", "AQ");
                //Console.WriteLine(tmp_line);
                /*
                 * #STEP 3 A COLLAPSE ALL DOUBLE LETTERS
                $line=~s/bb/b/g;
                $line=~s/cc/c/g;
                $line=~s/dd/d/g;
                $line=~s/DD/D/g;
                $line=~s/ff/f/g;
                $line=~s/gg/g/g;
                $line=~s/GG/G/g;
                $line=~s/hh/h/g;
                $line=~s/HH/H/g;
                $line=~s/jj/j/g;
                $line=~s/kk/k/g;
                $line=~s/KK/K/g;
                $line=~s/ll/l/g;
                $line=~s/mm/m/g;
                $line=~s/nn/n/g;
                $line=~s/qq/q/g;
                $line=~s/rr/r/g;
                $line=~s/ss/s/g;
                $line=~s/tt/t/g;
                $line=~s/TT/T/g;
                $line=~s/VV/V/g;
                $line=~s/ww/w/g;
                $line=~s/xx/x/g;
                $line=~s/yy/y/g;
                $line=~s/zz/z/g;
                */
                tmp_line = tmp_line.Replace("bb", "b");
                tmp_line = tmp_line.Replace("cc", "c");
                tmp_line = tmp_line.Replace("dd", "d");
                tmp_line = tmp_line.Replace("DD", "D");
                tmp_line = tmp_line.Replace("ff", "f");
                tmp_line = tmp_line.Replace("gg", "g");
                tmp_line = tmp_line.Replace("GG", "G");
                tmp_line = tmp_line.Replace("hh", "h");
                tmp_line = tmp_line.Replace("HH", "H");
                tmp_line = tmp_line.Replace("jj", "j");
                tmp_line = tmp_line.Replace("kk", "k");
                tmp_line = tmp_line.Replace("KK", "K");
                tmp_line = tmp_line.Replace("ll", "l");
                tmp_line = tmp_line.Replace("mm", "m");
                tmp_line = tmp_line.Replace("nn", "n");
                tmp_line = tmp_line.Replace("qq", "q");
                tmp_line = tmp_line.Replace("rr", "r");
                tmp_line = tmp_line.Replace("ss", "s");
                tmp_line = tmp_line.Replace("tt", "t");
                tmp_line = tmp_line.Replace("TT", "T");
                tmp_line = tmp_line.Replace("VV", "V");
                tmp_line = tmp_line.Replace("ww", "w");
                tmp_line = tmp_line.Replace("xx", "x");
                tmp_line = tmp_line.Replace("yy", "y");
                tmp_line = tmp_line.Replace("zz", "z");

                /*
                 #STEP W restore conventional orthography

                $line=~s/\bLlAQ\b/Lllh/g;
                $line=~s/\bElAQ\b/Elh/g;
                $line=~s/\blAQ\b/llh/g;

                $line=~s/\braHmAn\b/rHmn/g;
                $line=~s/\bcamr\b/cmrw/g;
                $line=~s/\bhAXA\b/hXA/g;
                $line=~s/\bhAXihi\b/hXh/g;

                #STEP W2 restore ta marbutah in idafah constructions

                $line=~s/at al-\b/Q al-/g;


                #STEP X CONVERT DEFINTE ARTICLE

                $line=~s/Ll-/Al/g;
                $line=~s/al-/Al/g;
                */
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bLlAQ\b", "Lllh");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bElAQ\b", "Elh");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\blAQ\b", "llh");

                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\braHmAn\b", "rHmn");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bcamr\b", "cmrw");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bhAXA\b", "hXA");
                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"\bhAXihi\b", "hXh");

                tmp_line = System.Text.RegularExpressions.Regex.Replace(tmp_line, @"at al-\b", "Q al-");

                tmp_line = tmp_line.Replace("Ll-", "Al");
                tmp_line = tmp_line.Replace("al-", "Al");

                /*
                 * #STEP Y REPLACE HYPHENS IN DATES WITH PLACEHOLDERS

                $line=~s/\n-\n/ o /g;

                #STEP Z REMOVE REMAINING SHORT VOWELS AND HYPHENS

                $line=~s/\x{00AD}//g; 
                $line=~s/\x{002D}//g;
                $line=~s/a//g;
                $line=~s/u//g;
                $line=~s/i//g;
                */

                //this doesn't look right - need to see what the 
                //perl value is doing here.
                tmp_line = tmp_line.Replace("\n-\n", " o ");

                tmp_line = tmp_line.Replace("\u00AD", "");
                tmp_line = tmp_line.Replace("\u002D", "");
                tmp_line = tmp_line.Replace("a", "");
                tmp_line = tmp_line.Replace("u", "");
                tmp_line = tmp_line.Replace("i", "");

                /*
                 * 
                 $line=~s/\x{0041}/\x{0627}/g; #alif
                $line=~s/\x{004D}/\x{0622}/g; #alif maddah 
                $line=~s/\x{0062}/\x{0628}/g; #ba
                $line=~s/\x{0074}/\x{062A}/g; #ta
                $line=~s/\x{0051}/\x{0629}/g; #ta marbutah
                $line=~s/\x{0056}/\x{062B}/g; #tha
                $line=~s/\x{006A}/\x{062C}/g; #jim  
                $line=~s/\x{0048}/\x{062D}/g; #guttural ha
                $line=~s/\x{0078}/\x{062E}/g; #kha
                $line=~s/\x{0064}/\x{062F}/g; #dal 
                $line=~s/\x{0058}/\x{0630}/g; #dhal
                $line=~s/\x{0072}/\x{0631}/g; #ra
                $line=~s/\x{007A}/\x{0632}/g; #zay
                $line=~s/\x{0073}/\x{0633}/g; #sin
                $line=~s/\x{004B}/\x{0634}/g; #shin
                $line=~s/\x{0053}/\x{0635}/g; #sad
                $line=~s/\x{0044}/\x{0636}/g; #dad
                $line=~s/\x{0054}/\x{0637}/g; #ta
                $line=~s/\x{005A}/\x{0638}/g; #za
                $line=~s/\x{0063}/\x{0639}/g; #ayn
                $line=~s/\x{0067}/\x{063A}/g; #ghayn
                $line=~s/\x{0066}/\x{0641}/g; #fa
                $line=~s/\x{0071}/\x{0642}/g; #qaf
                $line=~s/\x{006B}/\x{0643}/g; #kaf
                $line=~s/\x{006C}/\x{0644}/g; #lam
                $line=~s/\x{006D}/\x{0645}/g; #mim
                $line=~s/\x{006E}/\x{0646}/g; #nun
                $line=~s/\x{0068}/\x{0647}/g; #ha
                $line=~s/\x{0077}/\x{0648}/g; #waw
                $line=~s/\x{0079}/\x{064A}/g; #ya
                */
                
                tmp_line = tmp_line.Replace("\u0041", "\u0627");
                tmp_line = tmp_line.Replace("\u004D", "\u0622");
                tmp_line = tmp_line.Replace("\u0062", "\u0628");
                tmp_line = tmp_line.Replace("\u0074", "\u062A");
                tmp_line = tmp_line.Replace("\u0051", "\u0629");
                tmp_line = tmp_line.Replace("\u0056", "\u062B");
                tmp_line = tmp_line.Replace("\u006A", "\u062C");
                tmp_line = tmp_line.Replace("\u0048", "\u062D");
                tmp_line = tmp_line.Replace("\u0078", "\u062E");
                tmp_line = tmp_line.Replace("\u0064", "\u062F");
                tmp_line = tmp_line.Replace("\u0058", "\u0630");
                tmp_line = tmp_line.Replace("\u0072", "\u0631");
                tmp_line = tmp_line.Replace("\u007A", "\u0632");
                tmp_line = tmp_line.Replace("\u0073", "\u0633");
                tmp_line = tmp_line.Replace("\u004B", "\u0634");
                tmp_line = tmp_line.Replace("\u0053", "\u0635");
                tmp_line = tmp_line.Replace("\u0044", "\u0636");
                tmp_line = tmp_line.Replace("\u0054", "\u0637");
                tmp_line = tmp_line.Replace("\u005A", "\u0638");
                tmp_line = tmp_line.Replace("\u0063", "\u0639");
                tmp_line = tmp_line.Replace("\u0067", "\u063A");
                tmp_line = tmp_line.Replace("\u0066", "\u0641");
                tmp_line = tmp_line.Replace("\u0071", "\u0642");
                tmp_line = tmp_line.Replace("\u006B", "\u0643");
                tmp_line = tmp_line.Replace("\u006C", "\u0644");
                tmp_line = tmp_line.Replace("\u006D", "\u0645");
                tmp_line = tmp_line.Replace("\u006E", "\u0646");
                tmp_line = tmp_line.Replace("\u0068", "\u0647");
                tmp_line = tmp_line.Replace("\u0077", "\u0648");
                tmp_line = tmp_line.Replace("\u0079", "\u064A");

                /*
                 *#additional characters

                $line=~s/\x{0043}/\x{0621}/g; #hamzah on line
                $line=~s/\x{004C}/\x{0623}/g; #alif hamzah 
                $line=~s/\x{0057}/\x{0624}/g; #waw hamzah
                $line=~s/\x{0059}/\x{0626}/g; # ya hamzah
                $line=~s/\x{0045}/\x{0625}/g; #initial alif hamzah below
                $line=~s/\x{0065}/\x{0649}/g; #alif maqsurah

                #special characters

                $line=~s/\x{0070}/\x{067E}/g; #persian p
                $line=~s/\x{0047}/\x{0686}/g; #persian cha
                */
                tmp_line = tmp_line.Replace("\u0043", "\u0621");
                tmp_line = tmp_line.Replace("\u004C", "\u0623");
                tmp_line = tmp_line.Replace("\u0057", "\u0624");
                tmp_line = tmp_line.Replace("\u0059", "\u0626");
                tmp_line = tmp_line.Replace("\u0045", "\u0625");
                tmp_line = tmp_line.Replace("\u0065", "\u0649");

                tmp_line = tmp_line.Replace("\u0070", "\u067E");
                tmp_line = tmp_line.Replace("\u0047", "\u0686");

                tmp_record += tmp_line + System.Environment.NewLine;
            }
            return tmp_record;
        }
    }
}
