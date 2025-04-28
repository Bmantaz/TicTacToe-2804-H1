using System.ComponentModel;

namespace TicTacToe_2804_H1
{
    internal class InfoBlock : INotifyPropertyChanged
    {
        private int _xWins;
        private int _oWins;

        public int XWins
        {
            get => _xWins;
            private set
            {
                if (_xWins != value)
                {
                    _xWins = value;
                    OnPropertyChanged(nameof(XWins));
                    OnPropertyChanged(nameof(ScoreText));
                }
            }
        }

        public int OWins
        {
            get => _oWins;
            private set
            {
                if (_oWins != value)
                {
                    _oWins = value;
                    OnPropertyChanged(nameof(OWins));
                    OnPropertyChanged(nameof(ScoreText));
                }
            }
        }

        // Den tekst vi binder til TextBlock.Text
        public string ScoreText => $"Player X: {XWins} | Player O: {OWins}";

        // Kaldes fra MainWindow, når X eller O vinder
        public void RegisterWin(bool isXPlayer)
        {
            if (isXPlayer) XWins++;
            else OWins++;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
