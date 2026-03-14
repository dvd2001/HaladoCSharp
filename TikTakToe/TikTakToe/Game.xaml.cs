using System;
using System.Windows;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private Marks[] board;
        private int boardSize;
        private int winBy;
        private bool isPlayerOneTurn;
        private bool isMultiplayer;
        private bool isEnded;

        public Game(int size, int win, bool isMulti)
        {
            InitializeComponent();
            NewGame(size, win, isMulti);
        }

        private void NewGame(int size, int win, bool isMulti)
        {
            throw new NotImplementedException();
        }
    }
}
