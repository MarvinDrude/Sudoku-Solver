using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    [Serializable]
    public class Box {

        public Cell[] Cells { get; private set; }

        public List<Cell> _Cells { get; private set; }

        public Box(int count) {
            
            _Cells = new List<Cell>();

        }

        public void Convert() {

            Cells = _Cells.ToArray();

        }

        public bool Validate() {

            bool valid = true;

            for(int i = Cells.Length - 1; i >= 0; i--) {

                Cell a = Cells[i];

                for(int e = Cells.Length - 1; e >= 0; e--) {

                    Cell b = Cells[e];

                    if(i == e || b.Number == 0) { continue; }

                    if(a.Number == b.Number) {

                        valid = false;
                        break;

                    }

                }

            }

            return valid;

        }

    }

}
