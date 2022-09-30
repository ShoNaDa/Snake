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
        //от классов
        public readonly Map map1;
        private readonly SnakeClass snake1;
        private readonly Apple apple1 = new Apple();

        //направление
        private int lastSideIndex = 1;

        //конец хвоста
        public string lastSectionSnake;

        //переменная, чтоб знать можно ли увеличить змейку
        private bool mayAddSection = false;

        //стороны
        public enum Sides
        {
            LEFT,
            RIGHT,
            TOP,
            BOTTOM
        }

        public MainWindow(int _matrixOrder, int _speedSnake)
        {
            InitializeComponent();

            //отрисовываю карту
            map1 = new Map(_matrixOrder, mapGrid);

            //создаем змейку
            snake1 = new SnakeClass(_speedSnake);

            //отрисовываем яблоко
            apple1.AddApple(this, snake1, map1);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //АСИНХРОННОСТЬ
            await Task.Run(() =>
            {
                //тут для теста просто пока что вправо 5 раз ее двигаю
                while (!new GameFunctions().Lossing())
                {
                    //отрисовываем змею
                    new GameFunctions().DrawSnake(map1, snake1, apple1, this);

                    //змейка ест яблоко?
                    if (new GameFunctions().EatAppleOrNo(map1, snake1, apple1, this))
                    {
                        apple1.AddApple(this, snake1, map1);

                        mayAddSection = true;

                        Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            scoreLabel.Content = new GameFunctions().SetScore(snake1, scoreLabel, this);
                        });
                    }

                    //это чисто уровень скорости
                    Thread.Sleep(new GameFunctions().GetSpeedSnake(snake1));

                    //ну и двигаем змейку
                    snake1.MoveSnake((Sides)lastSideIndex, map1, this);

                    //добавляем часть змейки, если нужно
                    if (mayAddSection)
                    {
                        snake1.SnakeIsGrowing(lastSectionSnake);

                        mayAddSection = false;
                    }
                }
            });

            new RecordsWindow(map1, scoreLabel.Content.ToString()).Show();
            Close();
        }

        //метод управления змейкой
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:

                    if (lastSideIndex != 0)
                        lastSideIndex = 1;

                    break;

                case Key.A:

                    if (lastSideIndex != 1)
                        lastSideIndex = 0;
                    break;

                case Key.S:

                    if (lastSideIndex != 2)
                        lastSideIndex = 3;
                    break;

                case Key.W:

                    if (lastSideIndex != 3)
                        lastSideIndex = 2;
                    break;

            }
        }
    }
}