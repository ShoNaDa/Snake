using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake
{
    public class Apple
    {
        public int XIndex { get; private set; }
        public int YIndex { get; private set; }

        //генерация яблока
        public void AddApple(Window window, SnakeClass snakeClass, Map map)
        {
            string _nameSectionOfAllGrid = null;

            //рандомим индексы куда поставим яблоко (sleep шоб не одинаково было)
            int _xRand = new Random().Next(0, map.MatrixOrder);
            Thread.Sleep(10);
            int _yRand = new Random().Next(0, map.MatrixOrder);

            while (true)
            {
                window.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    _nameSectionOfAllGrid = map.AllGrid[_xRand, _yRand].Name.Substring(1);
                });

                //проверка - нет ли там куда мы ставим яблоко змейки
                if (!snakeClass.LocationSnake.Contains(_nameSectionOfAllGrid))
                {
                    map.EditMap(_xRand, _yRand, Brushes.Red, window);

                    XIndex = _xRand;
                    YIndex = _yRand;

                    break;
                }
            }
        }
    }
}