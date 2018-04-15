using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Threads {
    
    public class WorkerStack {

        public List<Worker> Workers { get; set; }

        public delegate void FinishHandler(WorkerStack stack);
        public event FinishHandler OnFinish;

        public WorkerStack() {

            Workers = new List<Worker>();

        }

        public void Finish() {

            lock(Workers) {

                bool finish = true;

                for(int i = Workers.Count - 1; i >= 0; i--) {

                    if(!Workers[i].Finished) {
                        finish = false;
                        break;
                    }

                }

                if(finish) {
                    OnFinish?.Invoke(this);
                }

            }

        }

    }

}
