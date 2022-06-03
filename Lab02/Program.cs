/*
using System.Diagnostics;

int min = 0;
int max = 10000;
Random RandNum = new Random();

int[] LVL0 = Enumerable.Repeat(0, 10).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL1 = Enumerable.Repeat(0, 100).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL2 = Enumerable.Repeat(0, 1000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL3 = Enumerable.Repeat(0, 10000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL4 = Enumerable.Repeat(0, 100000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL5 = Enumerable.Repeat(0, 1000000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL6 = Enumerable.Repeat(0, 10000000).Select(i => RandNum.Next(min, max)).ToArray();

float[] LVL0F = LVL0.Select( num => ((float)num)/10000).ToArray();
float[] LVL1F = LVL1.Select( num => ((float)num)/10000).ToArray();
float[] LVL2F = LVL2.Select( num => ((float)num)/10000).ToArray();
float[] LVL3F = LVL3.Select( num => ((float)num)/10000).ToArray();
float[] LVL4F = LVL4.Select( num => ((float)num)/10000).ToArray();
float[] LVL5F = LVL5.Select( num => ((float)num)/10000).ToArray();
float[] LVL6F = LVL6.Select( num => ((float)num)/10000).ToArray();

////////////// - Helper  -//////////////
void Printvector<T>(T[] vector) {
    foreach(T x in vector) 
        Console.Write(x + " ");
}

void XCHG(ref int a, ref int b) {
    int temp = a;
	a = b;
	b = temp;
}

////////////// - Insertion -//////////////
void InsertionSort(int[] vector) {
    for (int i=0; i < vector.Length; i++) {
        int currentElement = vector[i];
        int beforeIndex = i - 1;
        while (beforeIndex >= 0 && vector[beforeIndex] > currentElement) {
            vector[beforeIndex + 1] = vector[beforeIndex];
            beforeIndex--;
        }
        vector[beforeIndex + 1] = currentElement;
    }
}

////////////// - Quick -//////////////
int Partition(int[] vector, int start, int end) {
    int pivot = vector[end];
    int startIndex = (start - 1);

    for (int i = start; i < end; i++) {
        if (vector[i] <= pivot) {
            startIndex++;
            XCHG(ref vector[startIndex], ref vector[i]);
        } 
    }
    XCHG(ref vector[startIndex + 1], ref vector[end]);
    return startIndex + 1;
}

void QuickSort(int[] vector, int end, int start = 0) {
    if (start < end) {
        int partitionIndex = Partition(vector, start, end);

        QuickSort(vector, start, partitionIndex - 1);
        QuickSort(vector, partitionIndex + 1, end);
    }
}

////////////// - Bucket -//////////////
void BucketInsertionSort(List<float> vector) {
    for (int i=0; i < vector.Count; i++) {
        float currentElement = vector[i];
        int beforeIndex = i - 1;
        while (beforeIndex >= 0 && vector[beforeIndex] > currentElement) {
            vector[beforeIndex + 1] = vector[beforeIndex];
            beforeIndex--;
        }
        vector[beforeIndex + 1] = currentElement;
    }
}

void BucketSort(float[] vector, int numBuckets) {
    List<float>[] buckets = new List<float>[numBuckets];

    for (int i=0; i < numBuckets; i++)
        buckets[i] = new List<float>();

    for (int i=0; i < vector.Length; i++) {
        int bucketIndex = (int) (vector[i] * numBuckets);
        buckets[bucketIndex].Add(vector[i]);
    }
    int vectorIndex = 0;
    for (int i = 0; i < numBuckets; i++) {
        BucketInsertionSort(buckets[i]); // Moglo i samo buckets[i].sort
        foreach(float x in buckets[i]) {
            vector[vectorIndex] = x;
            vectorIndex++;
        }
    }
}

////////////// - Calcuations -//////////////
Stopwatch sw = new Stopwatch();
sw.Start();

//InsertionSort(LVL1);
//QuickSort(LVL3, LVL3.Length - 1);
//BucketSort();

sw.Stop();
TimeSpan ts = sw.Elapsed;
Console.Write("Time: ");
Console.Write(String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
Console.Write("\n");
*/





