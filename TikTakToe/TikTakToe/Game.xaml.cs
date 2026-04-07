using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
                    Border newBrd = new Border();
                    newBrd.BorderBrush = Brushes.Black;
                    newBrd.BorderThickness = new Thickness(1);
                    Canvas newCnvs = new Canvas();
                    newCnvs.Background = Brushes.White;
                    newCnvs.Name = $"Cnvs{i}{j}";
                    newCnvs.MouseLeftButtonDown += Canvas_Click;
                    newBrd.Child = newCnvs;
                    Grid.SetRow(newBrd, i);
                    Grid.SetColumn(newBrd, j);
                    grdBoard.Children.Add(newBrd);
                }
            }
        }

        private void Canvas_Click(object sender, RoutedEventArgs e)
        {
            Canvas cnvs = sender as Canvas;
            Border parent = cnvs.Parent as Border;
            int row = Grid.GetRow(parent);
            int col = Grid.GetColumn(parent);
            int idx = col + row * boardSize;
            if (board[idx] != Marks.Free) return;
            board[idx] = isPlayerOneTurn ? Marks.Cross : Marks.Circle;
            if (isPlayerOneTurn) DrawCross(cnvs);
            else DrawCircle(cnvs);
            if (CheckWin())
            {
                MessageBox.Show($"Player {(isPlayerOneTurn ? "1" : "2")} wins!");
                this.Close();
                return;
            }
            if (!board.Any(m => m == Marks.Free))
            {
                MessageBox.Show("It's a draw!");
                this.Close();
                return;
            }
            if (!isMultiplayer)
            {
                ComputerMove();
                if (CheckWin())
                {
                    MessageBox.Show("Computer wins!");
                    this.Close();
                    return;
                }
                if (!board.Cast<Marks>().Any(m => m == Marks.Free))
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
            int col = rand % boardSize;
            int temp = rand - col;
            int row = temp / boardSize;
            Border brd = grdBoard.Children.Cast<Border>().First(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
            Canvas cnvs = brd.Child as Canvas;
            DrawCircle(cnvs);
        }

        private bool CheckWin()
        {
            string winX = "", winO = "";
            List<string> rowsOrColomns = new List<string>(boardSize);
            List<string> diagnals = new List<string>(2 * boardSize - 1);
            for (int i = 0; i < winBy; i++)
            {
                winX += "X";
                winO += "O";
            }
            for (int i = 0; i < boardSize; i++) rowsOrColomns.Add("");
            for (int i = 0; i < 2 * boardSize - 1; i++) diagnals.Add("");
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
                for (int j = 0; j <= boardSize - winBy; j++)
                {
                    string s = rowsOrColomns[i].Substring(j, winBy);
                    if (s.Equals(winX) || s.Equals(winO)) return true;
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
                for (int j = 0; j <= boardSize - winBy; j++)
                {
                    string s = rowsOrColomns[i].Substring(j, winBy);
                    if (s.Equals(winX) || s.Equals(winO)) return true;
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
                for (int j = 0; j <= diagnals[i].Length - winBy; j++)
                {
                    string s = diagnals[i].Substring(j, winBy);
                    if (s.Equals(winX) || s.Equals(winO)) return true;
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
                for (int j = 0; j <= diagnals[i].Length - winBy; j++)
                {
                    string s = diagnals[i].Substring(j, winBy);
                    if (s.Equals(winX) || s.Equals(winO)) return true;
                }
            }
            return false;
        }

        private void DrawCross(Canvas cnvs)
        {
            Line line1 = new Line();
            line1.X1 = 10;
            line1.Y1 = 10;
            line1.X2 = cnvs.ActualWidth - 10;
            line1.Y2 = cnvs.ActualHeight - 10;
            line1.Stroke = Brushes.Blue;
            line1.StrokeThickness = 2;
            cnvs.Children.Add(line1);
            Line line2 = new Line();
            line2.X1 = cnvs.ActualWidth - 10;
            line2.Y1 = 10;
            line2.X2 = 10;
            line2.Y2 = cnvs.ActualHeight - 10;
            line2.Stroke = Brushes.Blue;
            line2.StrokeThickness = 2;
            cnvs.Children.Add(line2);
            return;
        }

        private void DrawCircle(Canvas cnvs)
        {
            Ellipse elp = new Ellipse();
            elp.Width = cnvs.ActualWidth - 20;
            elp.Height = cnvs.ActualHeight - 20;
            Canvas.SetLeft(elp, 10);
            Canvas.SetTop(elp, 10);
            Canvas.SetRight(elp, 10);
            Canvas.SetBottom(elp, 10);
            elp.Stroke = Brushes.Red;
            elp.StrokeThickness = 2;
            cnvs.Children.Add(elp);
            return;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Border brd in grdBoard.Children)
            {
                Canvas cnvs = brd.Child as Canvas;
                cnvs.Children.Clear();
                int row = Grid.GetRow(brd);
                int col = Grid.GetColumn(brd);
                int idx = col + row * boardSize;
                if (board[idx] == Marks.Cross) DrawCross(cnvs);
                else if (board[idx] == Marks.Circle) DrawCircle(cnvs);
            }
            return;
        }
    }
}
