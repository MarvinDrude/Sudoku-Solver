using SudokuSolver.Common;
using SudokuSolver.Reading;
using SudokuSolver.Threads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver {

    class Program {

        static void Main(string[] args) {

#if DEBUG
            args = new string[] {
                "-s", "sudoku.csv", "7"
            };
#endif

            if(args.Length > 0) {

                string command = args[0].ToLower().Trim();

                if(command == "-tc") {

                    if(args.Length > 1) {

                        //TestCase(args[1]);

                    }

                } else if(command == "-s") {

                    if(args.Length > 1) {

                        if(File.Exists(args[1])) {

                            int threads = -1;

                            if(!int.TryParse(args[2], out threads)) {

                                threads = 1;

                            }

                            Work(args[1], threads);

                        }

                    }

                }

            }

        }

        static void Work(string filepath, int threads) {
            
            Performance perf = new Performance();
            perf.Start();

            SReader.ReadCSV(filepath, ',', (ws) => {

                long elapsed = perf.Stop();

                List<Result> res = new List<Result>();

                foreach(Worker w in ws.Workers) { res.AddRange(w.Results.ToArray()); }

                using (StreamWriter sw = File.CreateText("result.txt")) {

                    foreach (Result tc in res) {

                        sw.WriteLine(tc.Input + " -> " + tc.Output + ", Time Elapsed: " + tc.Elapsed + "ms, Thread: " + tc.Worker.Name);

                    }

                    sw.WriteLine("Total Time Elapsed: " + elapsed + "ms");

                }

            }, threads);

        }

        //static void TestCase(string filepath) {

        //    List<TestCase> tests = new List<TestCase>();

        //    Performance perf = new Performance();
        //    perf.Start();

        //    SReader.ReadCSV(filepath, ',', (split) => {

        //        Sudoku soduko = new Sudoku(split[0]);
        //        SolveResult res = Solver.Solve(soduko);

        //        bool test = split[1] == res.Solved.ToString();

        //        tests.Add(new TestCase() {
        //            Input = split[0],
        //            Output = res.Solved.ToString(),
        //            Success = test,
        //            Elapsed = res.Elapsed
        //        });

        //    });

        //    long elapsed = perf.Stop();

        //    using(StreamWriter sw = File.CreateText("test_cases.txt")) {

        //        foreach(TestCase tc in tests) {

        //            sw.WriteLine(tc.Input + " -> " + tc.Output + ", Success: " + tc.Success + ", Time Elapsed: " + tc.Elapsed + "ms");

        //        }

        //        sw.WriteLine("Total Time Elapsed: " + elapsed + "ms");

        //    }

        //}

    }

    public class Result {

        public string Input { get; set; }

        public string Output { get; set; }

        public long Elapsed { get; set; }

        public Worker Worker { get; set; }

    }

}
