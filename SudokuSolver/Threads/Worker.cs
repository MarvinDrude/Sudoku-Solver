using SudokuSolver.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolver.Threads {

    public class Worker {

        public string Name { get; set; }

        public Thread Thread { get; set; }

        public bool Finished { get; set; } = false;

        public WorkerStack WS { get; set; }

        public string[] Work { get; set; }

        public Action<string[]> Run { get; set; }

        public char Delimiter { get; set; }

        public List<Result> Results { get; set; }

        public void _Run() {

            Results = new List<Result>();

            for(int i = Work.Length - 1; i >= 0; i--) {

                string[] split = Work[i].Split(Delimiter);

                Sudoku soduko = new Sudoku(split[0]);
                SolveResult res = Solver.Solve(soduko);

                Results.Add(new Result() {
                    Input = split[0],
                    Output = res.Solved.ToString(),
                    Elapsed = res.Elapsed,
                    Worker = this
                });

            }

            Finished = true;
            WS.Finish();

        }

    }

}
