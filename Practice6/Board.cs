using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6
{
    public class Board
    {
        public char[,] Desk { get; set; }
        public Board() {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            Desk = new char[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Desk[i, j] = ' ';
                }
            }
        }

        public bool CheckWin(char currentSymbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Desk[i, 0] == currentSymbol && Desk[i, 1] == currentSymbol && Desk[i, 2] == currentSymbol)
                {
                    return true;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (Desk[0, j] == currentSymbol && Desk[1, j] == currentSymbol && Desk[2, j] == currentSymbol)
                {
                    return true;
                }
            }

            if (Desk[0, 0] == currentSymbol && Desk[1, 1] == currentSymbol && Desk[2, 2] == currentSymbol)
            {
                return true;
            }
            if (Desk[0, 2] == currentSymbol && Desk[1, 1] == currentSymbol && Desk[2, 0] == currentSymbol)
            {
                return true;
            }
            return false;
        }

        public bool CheckTie()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Desk[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void DrawCell(int col, int row, char currentSymbol)
        {
            Desk[col,row] = currentSymbol;
        }
        public void CleanDesk()
        {
            InitializeDeck();
        }
    }
}
