using System;
using System.IO;
using System.Threading;

namespace MultithreadedArraySum
{
    class Program
    {
        int[] A;
        int N;
        int k;
        long[] results;

        public Program(int arraySize, int numberOfThreads)
        {
            N = arraySize;
            A = new int[N];
            k = numberOfThreads;
            results = new long[k];
        }

        void RandomArray(int maxRandomValue)
        {
            var rand = new Random();
            for (int i = 0; i < N; i++)
            {
                A[i] = rand.Next(0, maxRandomValue);
                Console.Write(A[i] + " ");
            }
            

                //A[i] = (i == N - 1) ? 1000 : rand.Next(0, maxRandomValue);
            //A[N - 1] = 1000;

            Console.WriteLine($"\nĐã khởi tạo mảng với kích thước gồm: {N} phần tử");
        }

        void ProcessSegment(int i)
        {
            int size = N / k;
            int start = i * size;
            int end = (i == k - 1) ? N - 1 : start + size - 1;

            long sum = 0;
            //string elements = "";

            for (int j = start; j <= end; j++)
            {
                sum += A[j];
                //elements += A[j] + (j < end ? ", " : "");
                Thread.Sleep(10);
            }

            results[i] = sum;

            //Console.WriteLine($"\nT{i + 1} xử lý các phần tử: [{elements}]");
            Console.WriteLine($"T{i + 1}: <{sum}> : Thời điểm hoàn thành: {DateTime.Now:HH:mm:ss.fff}");
        }

        public void Run(int maxRandomValue)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Chương trình tính tổng mảng sử dụng đa luồng");

            RandomArray(maxRandomValue);

            Thread[] threads = new Thread[k];
            for (int i = 0; i < k; i++)
            {
                int idx = i;
                threads[i] = new Thread(() => ProcessSegment(idx));
                threads[i].Start();
            }

            foreach (var t in threads)
                t.Join();

            long total = 0;
            for (int i = 0; i < k; i++)
                total += results[i];

            Console.WriteLine($"\nTổng của {N} phần tử trong mảng: {total}");

            long seqTotal = 0;
            foreach (var num in A)
                seqTotal += num;
            Console.WriteLine($"Kiểm tra với phương pháp tuần tự: {seqTotal}");
        }

        static void Main(string[] args)
        {
            int size = 11;
            //Console.WriteLine("Nhập kích thước mảng: ");
            //size = int.Parse(Console.ReadLine());
            int threads = 3;

            int maxValue = 10;

            var program = new Program(size, threads);
            program.Run(maxValue);
        }
    }
}
