using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    public static class Solver {

        public static SolveResult Solve(Sudoku sudoku) {

            Performance perf = new Performance();
            perf.Start();

            Sudoku so = new Sudoku(sudoku.ToString());
            SolveResult res = new SolveResult();
            res.Solved = so;

            bool finished = false;
            bool valid = true;
            bool extra = false;

            while(!finished) {

                Cell current = null;

                if (valid) {

                    current = so.GetNextEmptyCell();

                    if(current == null) {

                        finished = true;
                        break;

                    }

                    current.Number = 1;

                } else {

                    current = so.GetLastEmptyCell();

                    if(current == null) {
                        
                        break;

                    }

                    current.Number++;
                    
                    if(current.Number > 9) {
                        extra = true;
                    }

                }

                valid = current.Validate();

                while(!valid && current.Number < 10) {

                    current.Number++;
                    valid = current.Validate();

                }

                if(extra) {

                    current.Number = 0;
                    valid = false;
                    extra = false;

                } else if(!valid || current.Number > 9) {

                    current.Number = 0;
                    valid = false;

                }

            }

            res.Solvable = finished;
            res.Elapsed = perf.Stop();

            return res;

        }

    }

    public class SolveResult {

        public Sudoku Solved { get; set; }

        public bool Solvable { get; set; }

        public long Elapsed { get; set; }

    }

}
