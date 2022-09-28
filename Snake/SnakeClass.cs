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
        public SnakeClass(List<string> _locationSnake = null)
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
        }

        //где сейчас змейка
        public List<string> LocationSnake { get; private set; }

        //метод движения змейки
        public void MoveSnake(MainWindow.Sides side, Map map, Window window)
        {
            //индексы головы
            int _x = Convert.ToInt32(LocationSnake[LocationSnake.Count - 1].Split('_')[0]);
            int _y = Convert.ToInt32(LocationSnake[LocationSnake.Count - 1].Split('_')[1]);

            string _nameSectionOfAllGrid = null;

            switch (side)
            {
                case MainWindow.Sides.RIGHT:

                    //проверка - есть ли стена там, куда мы идем | КОММЕНТАРИИ ТОЛЬКО НА ОДНОМ НАПИШУ
                    if (map.AllGrid[_x, _y + 1] != null)
                    {
                        

                        window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            _name = map.AllGrid[_x, _y + 1].Name.Substring(1);
                        });

                        //проверка - нет ли там куда мы идем частей нашей змейки (мы ж закрашиваем все, можно этим пользоваться)
                        foreach (var item in LocationSnake)
                        {
                            if (_name == item)
                            {
                                //останавливаем, если да
                                break;
                            }
                        }

                        //перемещаем все кроме головы
                        for (int i = 0; i < LocationSnake.Count - 1; i++)
                        {
                            LocationSnake[i] = LocationSnake[i + 1];
                        }

                        //перемещаем голову
                        LocationSnake[LocationSnake.Count - 1] = $"{_x}_{_y + 1}";
                    }
                    break;

                case MainWindow.Sides.LEFT:

                    if (map.AllGrid[_x, _y - 1] != null)
                    {
                        if (map.AllGrid[_x, _y - 1].Background != Brushes.Yellow)
                        {
                            for (int i = 0; i < LocationSnake.Count - 1; i++)
                            {
                                LocationSnake[i] = LocationSnake[i + 1];
                            }

                            LocationSnake[LocationSnake.Count - 1] = $"{_x}_{_y - 1}";
                        }
                    }
                    break;

                case MainWindow.Sides.TOP:

                    if (map.AllGrid[_x - 1, _y] != null)
                    {
                        if (map.AllGrid[_x - 1, _y].Background != Brushes.Yellow)
                        {
                            for (int i = 0; i < LocationSnake.Count - 1; i++)
                            {
                                LocationSnake[i] = LocationSnake[i + 1];
                            }

                            LocationSnake[LocationSnake.Count - 1] = $"{_x - 1}_{_y}";
                        }
                    }
                    break;

                case MainWindow.Sides.BOTTOM:

                    if (map.AllGrid[_x + 1, _y] != null)
                    {
                        if (map.AllGrid[_x + 1, _y].Background != Brushes.Yellow)
                        {
                            for (int i = 0; i < LocationSnake.Count - 1; i++)
                            {
                                LocationSnake[i] = LocationSnake[i + 1];
                            }

                            LocationSnake[LocationSnake.Count - 1] = $"{_x + 1}_{_y}";
                        }
                    }
                    break;
            }
        }
    }
}
