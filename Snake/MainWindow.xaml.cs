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
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //направление
        public int lastSideIndex = 1;

        //от классов
        public Map map1;
        public SnakeClass snake1 = new SnakeClass();
        public Apple apple1 = new Apple();

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
            map1 = new Map(10, mapGrid);

            //отрисовываем яблоко
            apple1.AddApple(this, snake1, map1);

            //АСИНХРОННОСТЬ
            await Task.Run(() =>
            {
                //тут для теста просто пока что вправо 5 раз ее двигаю
                for (int i = 0; i < 100; i++)
                {
                    //отрисовываем змею
                    DrawSnake(map1);

                    //змейка ест яблоко?
                    if (EatAppleOfNo(map1, snake1))
                    {
                        apple1.AddApple(this, snake1, map1);
                    }

                    //это чисто уровень скорости
                    Thread.Sleep(500);

                    //ну и двигаем змейку
                    snake1.MoveSnake((Sides)lastSideIndex, map1, this);
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
                        else if (i == apple1.XIndex && j == apple1.YIndex)
                        {
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

        //метод управления змейкой
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:

                    lastSideIndex = 1;
                    break;

                case Key.A:

                    lastSideIndex = 0;
                    break;

                case Key.S:

                    lastSideIndex = 3;
                    break;

                case Key.W:

                    lastSideIndex = 2;
                    break;

            }
        }

        //метод для проверки - съели яблоко или нет
        private bool EatAppleOfNo(Map _map, SnakeClass _snakeClass)
        {
            //индексы головы
            int _xHead = Convert.ToInt32(_snakeClass.LocationSnake[_snakeClass.LocationSnake.Count - 1].Split('_')[0]);
            int _yHead = Convert.ToInt32(_snakeClass.LocationSnake[_snakeClass.LocationSnake.Count - 1].Split('_')[1]);

            if (_xHead == apple1.XIndex && _yHead == apple1.YIndex)
            {
                _map.EditMap(_xHead, _yHead, Brushes.Yellow, this);

                return true;
            };

            return false;
        }
    }
}