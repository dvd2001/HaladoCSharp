using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public Game(int size, int win, bool isMulti)
        {
            board = new Marks[size * size];
            boardSize = size;
            winBy = win;
            isPlayerOneTurn = true;
            isMultiplayer = isMulti;
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = Marks.Free;
            }
            grdBoard.Children.Clear();
            grdBoard.RowDefinitions.Clear();
            grdBoard.ColumnDefinitions.Clear();
            for (int i = 0; i < boardSize; i++)
            {
                grdBoard.RowDefinitions.Add(new RowDefinition());
                grdBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Button newBtn = new Button();
                    newBtn.Background = Brushes.White;
                    newBtn.Foreground = Brushes.Blue;
                    newBtn.Name = $"btn{i}{j}";
                    newBtn.Click += Button_Click;
                    Grid.SetRow(newBtn, i);
                    Grid.SetColumn(newBtn, j);
                    grdBoard.Children.Add(newBtn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);
            int idx = col + row*boardSize;
            if (board[idx] != Marks.Free) return;
            board[idx] = isPlayerOneTurn ? Marks.Cross : Marks.Circle;
            btn.Content = isPlayerOneTurn ? "X" : "O";
            if (!isPlayerOneTurn) btn.Foreground = Brushes.Red;
            if (CheckWin())
            {
                MessageBox.Show($"Player {(isPlayerOneTurn ? "1" : "2")} wins!");
                this.Close();
                return;
            }
            if (!board.Any(m=>m==Marks.Free))
            {
                MessageBox.Show("It's a draw!");
                this.Close();
                return;
            }
            if (!isMultiplayer)
            {
                ComputerMove();
                if (CheckWin(true))
                {
                    MessageBox.Show("Computer wins!");
                    this.Close();
                    return;
                }
                if (!board.Cast<Marks>().Any(m=>m==Marks.Free))
                {
                    MessageBox.Show("It's a draw!");
                    this.Close();
                    return;
                }
            }
            else
            {
                isPlayerOneTurn = !isPlayerOneTurn;
            }
        }

        private void ComputerMove()
        {
            Random r = new Random();
            int rand = r.Next(boardSize * boardSize);
            while (board[rand] != Marks.Free) rand = r.Next(boardSize * boardSize);
            board[rand] = Marks.Circle;
            int col = rand%boardSize;
            int temp = rand - col;
            int row = temp/boardSize;
            Button btn = grdBoard.Children.Cast<Button>().First(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
            btn.Content = "O";
            btn.Foreground = Brushes.Red;
        }

        private bool CheckWin(bool isComputer = false)
        {
            List<string> rowsOrColomns = new List<string>(boardSize);
            List<string> diagnals = new List<string>(2 * boardSize - 1);
            for (int i = 0; i < boardSize; i++) rowsOrColomns.Add("");
            for (int i = 0; i < 2 * boardSize - 1; i++) diagnals.Add("");
            if (!isComputer)
            {
                //Check only the lines that could have been affected by the last move
                for (int i = 0; i < boardSize; i++)
                {
                    string result = "";
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[i * boardSize + j] == Marks.Cross) result += "X";
                        else if (board[i * boardSize + j] == Marks.Circle) result += "O";
                        else result += " ";
                    }
                    rowsOrColomns[i] = result;
                }
                for (int i = 0; i < boardSize; i++)
                {
                    int count = 0;
                    foreach (char c in rowsOrColomns[i])
                    {
                        if ((isPlayerOneTurn && c == 'X') || (!isPlayerOneTurn && c == 'O')) count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                for (int i = 0; i < boardSize; i++) rowsOrColomns[i] = "";
                //Check only the column that could have been affected by the last move
                for (int i = 0; i < boardSize; i++)
                {
                    string result = "";
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[j * boardSize + i] == Marks.Cross) result += "X";
                        else if (board[j * boardSize + i] == Marks.Circle) result += "O";
                        else result += " ";
                    }
                    rowsOrColomns[i] = result;
                }
                for (int i = 0; i < boardSize; i++)
                {
                    int count = 0;
                    foreach (char c in rowsOrColomns[i])
                    {
                        if ((isPlayerOneTurn && c == 'X') || (!isPlayerOneTurn && c == 'O')) count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                //Check diagonals
                for (int i = 0; i < board.Length; i++)
                {
                    string result;
                    if (board[i] == Marks.Cross) result = "X";
                    else if (board[i] == Marks.Circle) result = "O";
                    else result = " ";
                    diagnals[(i % boardSize) + (i / boardSize)] += result;
                }
                for (int i = 0; i < diagnals.Count(); i++)
                {
                    if (diagnals[i].Length < winBy) continue;
                    int count = 0;
                    foreach (char c in diagnals[i])
                    {
                        if ((isPlayerOneTurn && c == 'X') || (!isPlayerOneTurn && c == 'O')) count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                diagnals.Clear();
                for (int i = 0; i < board.Length; i++)
                {
                    string result;
                    if (board[i] == Marks.Cross) result = "X";
                    else if (board[i] == Marks.Circle) result = "O";
                    else result = " ";
                    if (i / boardSize == 0) diagnals.Add(result);
                    else if (i % boardSize == 0) diagnals.Insert(0, result);
                    else diagnals[i % boardSize] += result;
                }
                for (int i = 0; i < diagnals.Count(); i++)
                {
                    if (diagnals[i].Length < winBy) continue;
                    int count = 0;
                    foreach (char c in diagnals[i])
                    {
                        if ((isPlayerOneTurn && c == 'X') || (!isPlayerOneTurn && c == 'O')) count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
            }
            else
            {
                //Check only the lines that could have been affected by the last move
                for (int i = 0; i < boardSize; i++)
                {
                    string result = "";
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[i * boardSize + j] == Marks.Cross) result += "X";
                        else if (board[i * boardSize + j] == Marks.Circle) result += "O";
                        else result += " ";
                    }
                    rowsOrColomns[i] = result;
                }
                for (int i = 0; i < boardSize; i++)
                {
                    int count = 0;
                    foreach (char c in rowsOrColomns[i])
                    {
                        if (c == 'O') count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                for (int i = 0; i < boardSize; i++) rowsOrColomns[i] = "";
                //Check only the column that could have been affected by the last move
                for (int i = 0; i < boardSize; i++)
                {
                    string result = "";
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[j * boardSize + i] == Marks.Cross) result += "X";
                        else if (board[j * boardSize + i] == Marks.Circle) result += "O";
                        else result += " ";
                    }
                    rowsOrColomns[i] = result;
                }
                for (int i = 0; i < boardSize; i++)
                {
                    int count = 0;
                    foreach (char c in rowsOrColomns[i])
                    {
                        if (c == 'O') count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                //Check diagonals
                for (int i = 0; i < board.Length; i++)
                {
                    string result;
                    if (board[i] == Marks.Cross) result = "X";
                    else if (board[i] == Marks.Circle) result = "O";
                    else result = " ";
                    diagnals[(i % boardSize) + (i / boardSize)] += result;
                }
                for (int i = 0; i < diagnals.Count(); i++)
                {
                    if (diagnals[i].Length < winBy) continue;
                    int count = 0;
                    foreach (char c in diagnals[i])
                    {
                        if (c == 'O') count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
                diagnals.Clear();
                for (int i = 0; i < board.Length; i++)
                {
                    string result;
                    if (board[i] == Marks.Cross) result = "X";
                    else if (board[i] == Marks.Circle) result = "O";
                    else result = " ";
                    if (i / boardSize == 0) diagnals.Add(result);
                    else if (i % boardSize == 0) diagnals.Insert(0, result);
                    else diagnals[i % boardSize] += result;
                }
                for (int i = 0; i < diagnals.Count(); i++)
                {
                    if (diagnals[i].Length < winBy) continue;
                    int count = 0;
                    foreach (char c in diagnals[i])
                    {
                        if (c == 'O') count++;
                        else count = 0;
                        if (count == winBy) return true;
                    }
                }
            }
                return false;
        }
    }
}
