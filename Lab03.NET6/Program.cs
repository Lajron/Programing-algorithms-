
using System.Diagnostics;
using System.Threading;

static List<Symbol> SymbolList(string text) {
    Dictionary<char, int> dict = text.GroupBy(c => c).ToDictionary(gr => gr.Key, gr => gr.Count());
    List<Symbol> symList = new List<Symbol>();
    foreach (var item in dict.Keys) {
        Symbol sym = new Symbol(item, dict[item], text.Length);
        symList.Add(sym);
    }
    symList = symList.OrderByDescending( x => x.freq).ToList();
    return symList;
}        
static void shannonFano(List<Symbol> chars, int start, int end) {
    if (start == end) {
        return;
    }

    int pLeft = start;
    int pRight = end;
    double sumLeft = 0.0;
    double sumRight = 0.0;

    while(pLeft <= pRight) {
        if(sumLeft <= sumRight) {
            sumLeft += chars[pLeft].freq;
            pLeft++;
        } else {
            sumRight += chars[pRight].freq;
            pRight--;
        }
    }           
    for(int i = start; i < pLeft; i++)
        chars[i].code += "0";
    for(int i = pLeft; i <= end; i++)
        chars[i].code += "1";            
    
    shannonFano(chars, start, pLeft-1);
    shannonFano(chars, pLeft, end);
}

string input = File.ReadAllText("100.txt").Trim();
Stopwatch swShannon = new Stopwatch();
Stopwatch swLZW = new Stopwatch();

#region ShannonFano
swShannon.Start();

Dictionary<char, string> shDict = new Dictionary<char, string>();
string result = "";

List<Symbol> symList = SymbolList(input);
shannonFano(symList, 0, symList.Count-1);

foreach(Symbol obj in symList) {
    shDict.Add(obj.sym, obj.code);
    //Console.WriteLine($"{obj.sym} - {obj.freq} - {obj.code}"); 
}

for (int i = 0; i < input.Length; i++) {
    result += shDict[input[i]];
}
Console.WriteLine(result);
swShannon.Stop();
Console.WriteLine(swShannon.Elapsed.ToString());
#endregion

Console.WriteLine("-----------");

#region LZW
swLZW.Start();
Dictionary<string, int> dict = new Dictionary<string, int>();
int top = 256;
for(int i =0; i < 256; i++) 
    dict.Add((char)i + "", i);
string w = "";
foreach(char c in input) {
    if (dict.ContainsKey(w + c)) {
        w += c;
    } else {
        dict.Add(w+c, top++);
        Console.Write(dict[w] + " ");
        w = c.ToString();
    }
}
Console.WriteLine(dict[w]);
swLZW.Stop();
Console.WriteLine(swLZW.Elapsed.ToString());
#endregion

Console.WriteLine("-----------");
Console.WriteLine("String length: " + input.Length);

public class Symbol {
    public char sym;
    public int count;
    public double freq;
    public string code;

    public Symbol(char sym, int count, int stringLength , string code = "") {
        this.sym = sym;
        this.count = count;
        this.freq = ((double) count) / stringLength;
    }
}
