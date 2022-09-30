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
    public partial class SnakeClass
    {
        public SnakeClass(int _speedSnake, List<string> _locationSnake = null)
        {
            //если параметры не передали (т.е. змейка только запустилась), то мы создали ее в конкретном месте
            LocationSnake = _locationSnake ?? new List<string>
            {
                //хвост
                "5_0",
                "5_1",
                "5_2"
                //голова
            };

            SpeedSnake = _speedSnake;
        }

        //где сейчас змейка
        public List<string> LocationSnake { get; private set; }

        //скорость змейки
        public int SpeedSnake { get; private set; }

        //метод перемещения змейки
        private void SnakeDislocation(int _x, int _y)
        {
            //перемещаем все кроме головы
            for (int i = 0; i < LocationSnake.Count - 1; i++)
            {
                LocationSnake[i] = LocationSnake[i + 1];
            }

            //перемещаем голову
            LocationSnake[LocationSnake.Count - 1] = $"{_x}_{_y}";
        }

        //метод проверки - нет ли там куда мы идем частей нашей змейки
        private void NextMaySectionSnake(string _nameSectionOfAllGrid)
        {
            foreach (var item in LocationSnake)
            {
                if (_nameSectionOfAllGrid == item)
                {
                    GameFunctions.IsLoss = true;

                    break;
                }
            }
        }

        //метод движения змейки
        public void MoveSnake(MainWindow.Sides side, Map map, MainWindow window)
        {
            //индексы головы
            int _x = Convert.ToInt32(LocationSnake[LocationSnake.Count - 1].Split('_')[0]);
            int _y = Convert.ToInt32(LocationSnake[LocationSnake.Count - 1].Split('_')[1]);

            string _nameSectionOfAllGrid = null;

            switch (side)
            {
                case MainWindow.Sides.RIGHT:

                    //проверка - есть ли стена там, куда мы идем | КОММЕНТАРИИ ТОЛЬКО НА ОДНОМ НАПИШУ
                    if (_y < map.MatrixOrder - 1)
                    {
                        //вот эта нереальная штука какое-то волшебство делает
                        window.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            _nameSectionOfAllGrid = map.AllGrid[_x, _y + 1].Name.Substring(1);
                        });

                        //проверка - нет ли там куда мы идем частей нашей змейки
                        NextMaySectionSnake(_nameSectionOfAllGrid);

                        //перемещаем змейку
                        SnakeDislocation(_x, _y + 1);
                    }

                    else
                        GameFunctions.IsLoss = true;
                    break;

                case MainWindow.Sides.LEFT:

                    if (_y > 0)
                    {
                        window.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            _nameSectionOfAllGrid = map.AllGrid[_x, _y - 1].Name.Substring(1);
                        });

                        NextMaySectionSnake(_nameSectionOfAllGrid);

                        SnakeDislocation(_x, _y - 1);
                    }

                    else
                        GameFunctions.IsLoss = true;
                    break;

                case MainWindow.Sides.TOP:

                    if (_x > 0)
                    {
                        window.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            _nameSectionOfAllGrid = map.AllGrid[_x - 1, _y].Name.Substring(1);
                        });

                        NextMaySectionSnake(_nameSectionOfAllGrid);

                        SnakeDislocation(_x - 1, _y);
                    }

                    else
                        GameFunctions.IsLoss = true;
                    break;

                case MainWindow.Sides.BOTTOM:

                    if (_x < map.MatrixOrder - 1)
                    {
                        window.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            _nameSectionOfAllGrid = map.AllGrid[_x + 1, _y].Name.Substring(1);
                        });

                        NextMaySectionSnake(_nameSectionOfAllGrid);

                        SnakeDislocation(_x + 1, _y);
                    }

                    else
                        GameFunctions.IsLoss = true;
                    break;
            }
        }

        //метод роста змейки
        public void SnakeIsGrowing(string section)
        {
            LocationSnake.Insert(0, section);
        }
    }
}