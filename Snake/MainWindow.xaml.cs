using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //сетка
        public readonly int matrixOrder = 10;

        //от классов
        public Map map1;
        public SnakeClass snake1 = new SnakeClass();

        //стороны
        public enum Sides
        {
            LEFT,
            RIGHT,
            TOP,
            BOTTOM
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //отрисовываю карту
            map1 = new Map(matrixOrder, mapGrid);

            //АСИНХРОННОСТЬ
            await Task.Run(() =>
            {
                //тут для теста просто пока что вправо 5 раз ее двигаю
                for (int i = 0; i < 5; i++)
                {
                    DrawSnake(map1);
                    Thread.Sleep(1000);
                    snake1.MoveSnake(Sides.RIGHT, map1, this);
                }
            });
        }

        //метод для отрисовки змейки
        public void DrawSnake(Map _map)
        {
            //проходимся по всей сетке
            for (int i = 0; i < _map.AllGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _map.AllGrid.GetLength(1); j++)
                {
                    //теперь каждую ячейку нужно проверить - есть ли тут часть змейки
                    foreach (string item in snake1.LocationSnake)
                    {
                        //если в ячейке если часть змейки, то мы перекрашиваем в серый (цвет змейки)
                        if (i == Convert.ToInt32(item.Split('_')[0]) && j == Convert.ToInt32(item.Split('_')[1]))
                        {
                            _map.EditMap(i, j, Brushes.Yellow, this);
                            break;
                        }
                        else //иначе закрашиваем в белый, вдруг там цвет змейки
                        {
                            _map.EditMap(i, j, Brushes.White, this);
                        }
                    }
                }
            }
        }
    }
}
