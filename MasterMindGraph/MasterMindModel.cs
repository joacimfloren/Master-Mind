using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class MasterMindModel
    {
        public string _secretKey;

        /**********************************************************/
        //  ANROP:      MasterMindModel model;
        //  UPPGIFT:    Konstruktor, lägger in en hemlig nyckel.
        /**********************************************************/
        public MasterMindModel() {
            _secretKey = createSecretKey();
        }

        /**********************************************************/
        //  ANROP:      string key = createSecretKey();
        //  UPPGIFT:    Skapar en hemlig nyckel.
        /**********************************************************/
        private static string createSecretKey()
        {
            Random random = new Random();
            string allowedSymbols = "1234567";
            string secretKey = "";

            for (int i = 0; i < 4; i++)
                secretKey += allowedSymbols[random.Next(allowedSymbols.Length)];

            return secretKey;
        }

        /**********************************************************/
        //  ANROP:      bool isValid = IsValidKey(userKey);
        //  UPPGIFT:    Kontrollerar om strängen är ok.
        /**********************************************************/
        public static bool IsValidKey(string key)
        {
            if (key.Length != 4)
                return false;

            if (!IsCorrectInts(key)) 
                        return false;

            return true;
        }

        /**********************************************************/
        //  ANROP:      bool ok = IsIntsOnly(key);
        //  UPPGIFT:    Kontrollerar om strängen bara innehåller
        //              integers. 
        /**********************************************************/
        public static bool IsCorrectInts(string str)
        {
            foreach (char c in str)
                if (c < '1' || c > '7')
                    return false;

            return true;
        }

        /**********************************************************/
        //  ANROP:      MatchResult mr = MatchKeys(secretKey, testKey);
        //  UPPGIFT:    Kontrollera strängarna mot varandra.
        /**********************************************************/
        public static MatchResult MatchKeys(string secretKey, string testKey)
        {
            int countCorrect = 0;
            int countSemiCorrect= 0;

            char[] secret = secretKey.ToCharArray();
            char[] test = testKey.ToCharArray();

            for (int i = 0; i < 4; i++)
                if (secret[i] == test[i])
                {
                    countCorrect++;
                    secret[i] = '0';
                    test[i] = '8';
                }

            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (secret[i] != '0')
                        if (secret[i] == test[x])
                        {
                            countSemiCorrect++;
                            secret[i] = '9';
                            test[i] = 'a';
                        }
                }
            }

            return new MatchResult(countCorrect, countSemiCorrect);
        }




        /**********************************************************/
        //  ANROP:      bool ok = SelfTest();
        //  UPPGIFT:    Används vid debugging. Returnerar true om 
        //              ingen bug hittas i alla de andra metoderna.
        /**********************************************************/
        public static bool SelfTest()
        {
            bool ok = IsValidKey("2361") && !IsValidKey("2368")
                    && !IsValidKey("ABCD") && !IsValidKey("+-*=")
                    && !IsValidKey("2301") && !IsValidKey("23611")
                    && !IsValidKey("231");

            for (int i = 0; i < 1000 && ok; i++)
                ok = IsValidKey(createSecretKey());

            Console.WriteLine("IsValidKey: " + ok);
            Console.WriteLine("createSecretKey: " + ok);
            
            MasterMindModel model = new MasterMindModel();
            ok = IsValidKey(model._secretKey);
            System.Diagnostics.Debug.WriteLine("MasterMindModel konstruktor: " + ok);

            ok = ok && (MatchKeys("1234", "1234") == new MatchResult(4, 0));
            ok = ok && (MatchKeys("1234", "4321") == new MatchResult(0, 4));
            ok = ok && (MatchKeys("1234", "1243") == new MatchResult(2, 2));
            ok = ok && (MatchKeys("1234", "1212") == new MatchResult(2, 0));
            ok = ok && (MatchKeys("1234", "5612") == new MatchResult(0, 2));
            ok = ok && (MatchKeys("1444", "1144") == new MatchResult(3, 0));
            ok = ok && (MatchKeys("5224", "4334") == new MatchResult(1, 0));
            ok = ok && (MatchKeys("1223", "2245") == new MatchResult(1, 1));
            ok = ok && (MatchKeys("1222", "3111") == new MatchResult(0, 1));

            System.Diagnostics.Debug.WriteLine("MatchKeys: " + ok);

            return ok;
        }
    }
}
