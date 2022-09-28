using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake
{
    public class Map
    {
        public Map(int _matrixOrder, Grid _grid)
        {
            matrixOrder = _matrixOrder;

            GenerateMap(_grid);
        }

        //какой порядок у матрицы
        private readonly int matrixOrder;

        //тут будет лежать вся сетка, чтоб с ней работать
        public Label[,] AllGrid { get; private set; }

        //генерируем карту
        private void GenerateMap(Grid grid)
        {
            AllGrid = new Label[matrixOrder, matrixOrder];

            for (int j = 0; j < matrixOrder; j++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            }

            for (int j = 0; j < matrixOrder; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            }

            for (int i = 0; i < matrixOrder; i++)
            {
                for (int j = 0; j < matrixOrder; j++)
                {
                    Label childLabel = new Label
                    {
                        Name = $"l{i}_{j}",
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    };

                    grid.Children.Add(childLabel);
                    Grid.SetRow(childLabel, i);
                    Grid.SetColumn(childLabel, j);

                    AllGrid[i, j] = childLabel;
                }
            }
        }

        //редактирование карты (перекраска) | YELLOW - ЗМЕЙКА | WHITE - СЕТКА | RED - ЯБЛОКО
        public void EditMap(int _i, int _j, Brush brush, Window window)
        {
            window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                AllGrid[_i, _j].Background = brush;
            });
        }
    }
}
