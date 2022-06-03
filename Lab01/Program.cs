using System.Diagnostics;
using System.IO;

string filename = "txt/100000.txt";
string textFile = File.ReadAllText(filename);

RabinKarp(filename, textFile, "Sed eu dui pharetra metus pellentesque pulvinar eu quis massa. Curabitur ac libero justo.", 256);
KMP(filename, textFile, "Sed eu dui pharetra metus pellentesque pulvinar eu quis massa. Curabitur ac libero justo.", 256);


static int hex(char i) {
    return (i >= '0' && i <= '9') ? i - '0' : (i - 'A') + 10;
}

static void RabinKarp(string filename, string textFile, string text, int a) {
    Stopwatch sw = new Stopwatch();
    sw.Start();
    List<int> patterns = new List<int>();
    int prime = 271; // neki prime broj, moze bilo koji cisto za heshiranje (strukture podataka)
    int m = text.Length;
    int n = textFile.Length;
    int i, j;
    int h = 1;  
    int p = 0;
    int t = 0;

    for (i = 0; i < m - 1; i++)
      h = (h * a) % prime;

    if ( a == 256 ) {
        // Izracunaj hash
        for (i = 0; i < m; i++) {
        p = (a * p + text[i]) % prime;
        t = (a * t + textFile[i]) % prime;
        }

        for (i = 0; i <= n - m; i++) {
            if (p == t) {
                for (j = 0; j < m; j++) {
                if (textFile[i+j] != text[j])
                    break;
                }

                if (j == m) {
                patterns.Add(i + 1);
                }
            }

            if (i < n - m) {
                t = (a * (t - textFile[i] * h) + textFile[i + m]) % prime;
                if (t < 0)
                t = (t + prime);
            }
        }
    }

    if ( a == 16) {
        // Izracunaj hash
        for (i = 0; i < m; i++) {
        p = (a * p + hex(text[i])) % prime;
        t = (a * t + hex(textFile[i])) % prime;
        }

        for (i = 0; i <= n - m; i++) {
            if (p == t) {
                for (j = 0; j < m; j++) {
                if (hex(textFile[i+j]) != hex(text[j]))
                    break;
                }

                if (j == m) {
                patterns.Add(i + 1);
                }
            }

            if (i < n - m) {
                t = (a * (t - hex(textFile[i]) * h) + hex(textFile[i + m])) % prime;
                if (t < 0)
                t = (t + prime);
            }
        }
    }

    sw.Stop();
    TimeSpan ts = sw.Elapsed;

    using (StreamWriter writer = new StreamWriter($"RKresult.txt", true)) {
        writer.WriteLine("-----------------");
        writer.WriteLine($"Text lenght: {textFile.Length}");
        writer.WriteLine($"Pattern lenght: {text.Length}");
        writer.WriteLine("Time: " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        writer.Write("Position: ");
        foreach(int inte in patterns)
            writer.Write(inte + " ");
        writer.WriteLine("");
    }
}
static void KMP(string filename, string textFile, string text, int a) {
    Stopwatch sw = new Stopwatch();
    sw.Start();
    List<int> patterns = new List<int>();

    if (a == 256) {
        int m = text.Length;
        int n = textFile.Length;
        int i = 0;
        int j = 0;
        int[] lps = new int[m];

        ComputeLPSArray(text, m, lps, a);

        while (i < n) {
            if (text[j] == textFile[i]) {
                j++;
                i++;
            }

            if (j == m) {
                patterns.Add(i - j + 1);
                j = lps[j - 1];
            }

            else if (i < n && text[j] != textFile[i]) {
                if (j != 0)
                    j = lps[j - 1];
                else
                    i = i + 1;
            }
        }
    }

    if (a == 16) {
        int m = text.Length;
        int n = textFile.Length;
        int i = 0;
        int j = 0;
        int[] lps = new int[m];

        ComputeLPSArray(text, m, lps, a);

        while (i < n) {
            if (hex(text[j]) == hex(textFile[i])) {
                j++;
                i++;
            }

            if (j == m) {
                patterns.Add(i - j + 1);
                j = lps[j - 1];
            }

            else if (i < n && hex(text[j]) != hex(textFile[i])) {
                if (j != 0)
                    j = lps[j - 1];
                else
                    i = i + 1;
            }
        }
    }
    sw.Stop();
    TimeSpan ts = sw.Elapsed;

	using (StreamWriter writer = new StreamWriter("KMPresult.txt", true)) {
        writer.WriteLine("-----------------");
        writer.WriteLine($"Text lenght: {textFile.Length}");
        writer.WriteLine($"Pattern lenght: {text.Length}");
        writer.WriteLine("Time: " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        writer.Write("Position: ");
        foreach(int inte in patterns)
            writer.Write(inte + " ");
        writer.WriteLine("");
    }
}

static void ComputeLPSArray(string pat, int m, int[] lps, int a) {
        int len = 0;
        int i = 1;

        lps[0] = 0;
        if (a == 256) {
            while (i < m) {
                if (pat[i] == pat[len]) {
                    len++;
                    lps[i] = len;
                    i++;
                } else {
                    if (len != 0) {
                        len = lps[len - 1];
                    } else {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }

        if (a == 16) {
            while (i < m) {
                if (hex(pat[i]) == hex(pat[len])) {
                    len++;
                    lps[i] = len;
                    i++;
                } else {
                    if (len != 0) {
                        len = lps[len - 1];
                    } else {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }
    }