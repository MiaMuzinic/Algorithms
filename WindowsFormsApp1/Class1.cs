using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Class1
    {
        // 1. BRUTE FORCE   https://www.geeksforgeeks.org/naive-algorithm-for-pattern-searching/
        public bool BruteForce(string uzorak, string tekst)
        {
            uzorak = uzorak.ToLower();
            tekst = tekst.ToLower();
            int velicina_uzorka = uzorak.Length;
            int velicina_teksta = tekst.Length;

            for (int i = 0; i <= velicina_teksta - velicina_uzorka; i++)
            {
                int j;
                for (j = 0; j < velicina_uzorka; j++)
                {
                    if (tekst[i + j] != uzorak[j])
                    {
                        break;
                    }
                }
                if (j == velicina_uzorka)
                {
                    return true; 
                }
            }
            return false;
        }


        // 2. Boyer-Moore-Simple-Search  https://www.baeldung.com/java-full-text-search-algorithms
        public bool BoyerMooreSimpleSearch(string uzorak, string tekst)
        {
            uzorak = uzorak.ToLower();
            tekst = tekst.ToLower();
            int velicina_uzorka = uzorak.Length;
            int velicina_teksta = tekst.Length;

            int i = 0, j = 0;

            while ((i + velicina_uzorka) <= velicina_teksta)
            {
                j = velicina_uzorka - 1;

                while (tekst[i + j] == uzorak[j])
                {
                    j--;
                    if (j < 0)
                        return true;
                }
                i++;
            }
            return false;
        }


        // 3. RABIN-KARP https://www.geeksforgeeks.org/rabin-karp-algorithm-for-pattern-searching/
        public bool RabinKarp(string uzorak, string tekst)
        {
            tekst = tekst.ToLower();
            uzorak = uzorak.ToLower();
            ulong p = 0;
            ulong t = 0;
            ulong q = 100007;
            ulong ASCII = 256;
            for (int i = 0; i < uzorak.Length; i++)
            {
                try
                {
                    p = (p * ASCII + (ulong)tekst[i]) % q;
                    t = (t * ASCII + (ulong)uzorak[i]) % q;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }
            if (p == t)
            {
                return true;
            }
            ulong h = 1;
            for (int i = 1; i <= uzorak.Length - 1; i++)
                h = (h * ASCII) % q;

            for (int j = 1; j <= tekst.Length - uzorak.Length; j++)
            {
                p = (p + q - h * (ulong)tekst[j - 1] % q) % q;
                p = (p * ASCII + (ulong)tekst[j + uzorak.Length - 1]) % q;
                if (p == t)
                {
                    if (tekst.Substring(j, uzorak.Length) == uzorak)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        // 4. KNUTH–MORRIS–PRATT   https://www.geeksforgeeks.org/kmp-algorithm-for-pattern-searching/
        public bool KnutMorrisPratt(string uzorak, string tekst)
        {
            uzorak = uzorak.ToLower();
            tekst = tekst.ToLower();
            int velicina_uzorka = uzorak.Length;
            int velicina_teksta = tekst.Length;

            int[] longest_prefix_suffix = new int[velicina_uzorka];

            int j = 0; 
            computeLPSArray(uzorak, velicina_uzorka, ref longest_prefix_suffix);

            int i = 0;
            while (i < velicina_teksta)
            {
                if (uzorak[j] == tekst[i])
                {
                    j++;
                    i++;
                }
                if (j == velicina_uzorka)
                {
                    return true;
                }
                else if (i < velicina_teksta && uzorak[j] != tekst[i])
                {
                    if (j != 0)
                        j = longest_prefix_suffix[j - 1];
                    else
                        i = i + 1;
                }
            }
            return false;
        }
        public void computeLPSArray(string pat, int M, ref int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0; 

            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }
    }
}
