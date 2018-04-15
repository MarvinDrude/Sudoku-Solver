using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Common {

    [Serializable]
    public class Sudoku {

        public Line[] Rows { get; private set; }

        public Line[] Columns { get; private set; }

        public Box[] Boxes { get; private set; }

        public List<Cell> Emptys { get; private set; }

        public Sudoku(string text) {

            Rows = new Line[9];
            Columns = new Line[9];
            Boxes = new Box[9];

            Emptys = new List<Cell>();

            for(int i = 0; i < Rows.Length; i++) {
                Rows[i] = new Line(Columns.Length, i);
            }

            for(int i = 0; i < Columns.Length; i++) {
                Columns[i] = new Line(Rows.Length, i);
            }

            for(int i = 0; i < Boxes.Length; i++) {
                Boxes[i] = new Box(9);
            }

            for(int i = 0; i < text.Length; i++) {

                byte parse = 0;

                if(byte.TryParse(text[i].ToString(), out parse)) {

                    int rowPos = i % 9;
                    int row = (int)Math.Floor((decimal)i / 9);

                    int box = -1;

                    if(rowPos < 3) {

                        if(row < 3) {

                            box = 0;

                        } else if(row >= 3 && row < 6) {

                            box = 3;

                        } else if(row >= 6) {

                            box = 6;

                        }

                    } else if(rowPos >= 3 && rowPos < 6) {

                        if (row < 3) {

                            box = 1;

                        } else if (row >= 3 && row < 6) {

                            box = 4;

                        } else if (row >= 6) {

                            box = 7;

                        }

                    } else if (rowPos >= 6) {

                        if (row < 3) {

                            box = 2;

                        } else if (row >= 3 && row < 6) {

                            box = 5;

                        } else if (row >= 6) {

                            box = 8;

                        }

                    }

                    Line lrow = Rows[row];
                    Line lcol = Columns[rowPos];
                    Box lbox = Boxes[box];

                    Cell cell = new Cell() {
                        Row = lrow,
                        Column = lcol,
                        Box = lbox,
                        Number = parse,
                        Empty = parse == 0,
                        RowPosition = rowPos
                    };

                    lrow.Cells[rowPos] = cell;
                    lcol.Cells[row] = cell;
                    lbox._Cells.Add(cell);

                } else {

                    throw new Exception("Parse Error: Not a byte at: " + i);

                }

            }

            for (int i = 0; i < Boxes.Length; i++) {
                Boxes[i].Convert();
            }

        }

        public Cell GetLastEmptyCell() {

            if(Emptys.Count == 0) {

                return null;

            }

            Emptys.RemoveAt(Emptys.Count - 1);

            if(Emptys.Count == 0) { return null; }

            return Emptys[Emptys.Count - 1];

        }

        public Cell GetNextEmptyCell() {

            int lastRow = 0;
            int lastRowCount = 0;

            if(Emptys.Count != 0) {

                Cell last = Emptys[Emptys.Count - 1];

                lastRow = last.Row.Position;
                lastRowCount = last.RowPosition;

            }

            for(int i = lastRow; i < Rows.Length; i++) {

                Line row = Rows[i];

                for(int e = 0; e < row.Cells.Length; e++) {

                    Cell cell = row.Cells[e];

                    if(cell.Empty && cell.Number == 0) {

                        Emptys.Add(cell);

                        return cell;

                    }

                }

            }

            return null;

        }

        public override string ToString() {

            string res = "";

            for(int i = 0; i < Rows.Length; i++) {

                Line row = Rows[i];

                for (int e = 0; e < row.Cells.Length; e++) {

                    res += row.Cells[e].Number.ToString();

                }

            }

            return res;

        }

    }

}
