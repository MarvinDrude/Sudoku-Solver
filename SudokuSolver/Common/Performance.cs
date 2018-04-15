using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    public class Performance {

        public long Started { get; set; }

        public void Start() {

            Started = Unix();

        }

        public long Stop() {

            return Unix() - Started;

        }

        public long Unix() {

            return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        }

    }

}
