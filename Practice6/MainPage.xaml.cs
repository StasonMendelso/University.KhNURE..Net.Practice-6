using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Practice6
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Board Board { get; set; }
        private char CurrentSymbol = 'X';
        private int FirstPlayerScore = 0;
        private int SecondPlayerScore = 0;
        private Button[] Buttons;
        public MainPage()
        {
            InitializeComponent();
            Buttons = new Button[]{ Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9};
            Board = new Board();
            currentSymbolTurn.Text = CurrentSymbol.ToString();
            firstPlayerScore.Text = FirstPlayerScore.ToString();
            secondPlayerScore.Text = SecondPlayerScore.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string argument = clickedButton.CommandParameter as string;
                string[] tokens = argument.Split(' ');
                int col = GetColumnIndex(tokens);
                int row = GetRowIndex(tokens);

                DrawButtonCell(clickedButton);

                HandleMove(col, row);
             
                ChangeCurrentSymbol();
            }

        }

        private void HandleMove(int col, int row)
        {
            Board.DrawCell(col, row, CurrentSymbol);

            if (Board.CheckWin(CurrentSymbol)) {
                GameOver();
            }
            if (Board.CheckTie())
            {
                GameOver(true);
            }
        }
        private void GameOver(bool tie = false)
        {
            string message = "";
            if (tie)
            {
                message = "Tie game!";
            }
            else
            {
                string winner = GetWinner();
                message = winner + " wins!";
                if (winner.Equals("Player 1")){
                    firstPlayerScore.Text = ""+ ++FirstPlayerScore;
                }
                if (winner.Equals("Player 2")){
                    secondPlayerScore.Text = ""+ ++SecondPlayerScore;
                }
            }
            var dialog = new ContentDialog()
            {
                Title = "Game Over",
                Content = message,
                CloseButtonText = "Play again"
            };
            BlockButtons();
            dialog.Closed += (sender, args) => { InitializeBoard(); };
            dialog.ShowAsync();
        }

        private string GetWinner()
        {
            return CurrentSymbol == 'X' ? "Player 1" : "Player 2";
        }

        private void InitializeBoard()
        {
            Board.CleanDesk();
            for(int i = 0; i < 9; i++)
            {
                Buttons[i].IsEnabled = true;
                Buttons[i].Content = ' ';
            }
        }
        private void BlockButtons() {
            for (int i = 0; i < 9; i++)
            {
                Buttons[i].IsEnabled = false;
            }
        }

        private void DrawButtonCell(Button clickedButton)
        {
            clickedButton.Content = CurrentSymbol;
            clickedButton.IsEnabled = false;
        }

        private void ChangeCurrentSymbol()
        {
            CurrentSymbol = CurrentSymbol=='O' ? 'X' : 'O';
            currentSymbolTurn.Text = CurrentSymbol.ToString();
        }
        private int GetRowIndex(string[] tokens)
        {
            foreach (string token in tokens)
            {
                if (token.StartsWith("row="))
                {
                    return int.Parse(token.Substring(4));
                }
            }
            throw new ArgumentException("The column index must be in CommandArguments as row=0");
        }

        private int GetColumnIndex(string[] tokens)
        {
            foreach (string token in tokens)
            {
                if (token.StartsWith("col="))
                {
                    return int.Parse(token.Substring(4));
                }
            }
            throw new ArgumentException("The column index must be in CommandArguments as col=0");
        }
    }
}

