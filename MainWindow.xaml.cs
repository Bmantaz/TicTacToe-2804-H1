using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe_2804_H1
{
    public partial class MainWindow : Window

    {
        private readonly InfoBlock _info = new InfoBlock();
        private bool isXTurn = true;
        private Button[,] board;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _info;
            board = new Button[3, 3]
            {
                { Btn00, Btn01, Btn02 },
                { Btn10, Btn11, Btn12 },
                { Btn20, Btn21, Btn22 }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (isXTurn)
                btn.Content = "X";
            else
                btn.Content = "O";

            btn.IsEnabled = false;

            if (CheckWin())
            {

                _info.RegisterWin(isXTurn); // Registrer sejren.



                var result = MessageBox.Show(
                    $"Spiller {(isXTurn ? 'X' : 'O')} har vundet!\nVil du spille igen?",
                    "Game Over",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ResetBoard();
                  
                }
                else
                {
                    Application.Current.Shutdown();
                }

                return;
            }

            if (IsDraw())
            {
                MessageBox.Show("Uafgjort!", "Game Over");
                ResetBoard();
                return;
            }

            isXTurn = !isXTurn;

            if (CheckWin())
            {
                // Før du nulstiller brættet, registrer vinderen
                _info.RegisterWin(isXTurn);
                MessageBox.Show($"Spiller {(isXTurn ? 'X' : 'O')} har vundet!", "Game Over");
                ResetBoard();
                return;
            }
        }

        private bool CheckWin()
        {
            // Tjek rækker og kolonner
            for (int i = 0; i < 3; i++)
            {
                if (Match(board[i, 0], board[i, 1], board[i, 2]) ||
                    Match(board[0, i], board[1, i], board[2, i]))
                    return true;
            }
            // Tjek diagonaler
            if (Match(board[0, 0], board[1, 1], board[2, 2]) ||
                Match(board[0, 2], board[1, 1], board[2, 0]))
                return true;

            return false;
        }

        private bool Match(Button a, Button b, Button c)
        {
            return a.Content != null &&
                   a.Content.Equals(b.Content) &&
                   b.Content.Equals(c.Content);
        }

        private bool IsDraw()
        {
            foreach (var btn in board)
            {
                if (btn.IsEnabled)
                    return false;
            }
            return true;
        }

        private void ResetBoard()
        {
            foreach (var btn in board)
            {
                btn.Content = null;
                btn.IsEnabled = true;
            }
            isXTurn = true;
        }
    }
}