using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    [Serializable]
    public class Cell {

        public byte Number { get; set; } = 0;

        public Line Row { get; set; }

        public int RowPosition { get; set; }

        public Line Column { get; set; }

        public Box Box { get; set; }

        public bool Empty { get; set; } = false;

        public bool Validate() {

            return Row.Validate() && Column.Validate() && Box.Validate();

        }

    }

}
