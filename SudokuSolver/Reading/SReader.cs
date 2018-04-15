using SudokuSolver.Threads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolver.Reading {

    public static class SReader {
        
        public static void ReadCSV(string path, char delimiter, Action<WorkerStack> finish, int threads = 1) {

            string[] lines = File.ReadAllLines(path);

            if(lines.Length < threads) {
                threads = 1;
            }

            int chunkSize = (int)Math.Ceiling((double)lines.Length / threads);
            WorkerStack stack = new WorkerStack();

            stack.OnFinish += (s) => finish(s);

            for(int i = threads - 1; i >= 0; i--) {

                Worker worker = new Worker() {
                    Name = "ThreadWorker #" + (i + 1),
                    WS = stack,
                    Delimiter = delimiter
                };

                worker.Work = new string[i * chunkSize + chunkSize > lines.Length ? lines.Length - i * chunkSize : chunkSize];
                Array.Copy(lines, i * chunkSize, worker.Work, 0, worker.Work.Length);

                worker.Thread = new Thread(worker._Run);
                worker.Thread.Start();

                stack.Workers.Add(worker);

            }

        }

    }

}
