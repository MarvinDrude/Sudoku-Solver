using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    [Serializable]
    public class Line {

        public Cell[] Cells { get; private set; } 

        public int Position { get; private set; }

        public Line(int count, int position) {

            Position = position;
            Cells = new Cell[count];

        }

        public bool Validate() {

            bool valid = true;

            for (int i = Cells.Length - 1; i >= 0; i--) {

                Cell a = Cells[i];

                for (int e = Cells.Length - 1; e >= 0; e--) {

                    Cell b = Cells[e];

                    if (i == e || b.Number == 0) { continue; }

                    if (a.Number == b.Number) {

                        valid = false;
                        break;

                    }

                }

            }

            return valid;

        }

    }

}
