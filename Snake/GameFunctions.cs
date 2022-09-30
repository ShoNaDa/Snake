using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Snake
{
    public class GameFunctions
    {
        //счетчик очков
        public string Score;

        //свойство, отвечающее за проигрыш
        static public bool IsLoss { get; set; } = false;

        //метод для отрисовки змейки
        public void DrawSnake(Map _map, SnakeClass snakeClass, Apple apple, MainWindow window)
        {
            //проходимся по всей сетке
            for (int i = 0; i < _map.AllGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _map.AllGrid.GetLength(1); j++)
                {
                    //теперь каждую ячейку нужно проверить - есть ли тут часть змейки
                    foreach (string item in snakeClass.LocationSnake)
                    {
                        //если в ячейке если часть змейки, то мы перекрашиваем в серый (цвет змейки)
                        if (i == Convert.ToInt32(item.Split('_')[0]) && j == Convert.ToInt32(item.Split('_')[1]))
                        {
                            _map.EditMap(i, j, Brushes.Yellow, window);
                            break;
                        }
                        else if (i == apple.XIndex && j == apple.YIndex)
                        {
                            break;
                        }
                        else //иначе закрашиваем в белый, вдруг там цвет змейки
                        {
                            _map.EditMap(i, j, Brushes.White, window);
                        }
                    }
                }
            }

            //записываем хвост змейки
            window.lastSectionSnake = snakeClass.LocationSnake[0];
        }

        //метод для проверки - съели яблоко или нет
        public bool EatAppleOrNo(Map _map, SnakeClass _snakeClass, Apple apple, Window window)
        {
            //индексы головы
            int _xHead = Convert.ToInt32(_snakeClass.LocationSnake[_snakeClass.LocationSnake.Count - 1].Split('_')[0]);
            int _yHead = Convert.ToInt32(_snakeClass.LocationSnake[_snakeClass.LocationSnake.Count - 1].Split('_')[1]);

            if (_xHead == apple.XIndex && _yHead == apple.YIndex)
            {
                _map.EditMap(_xHead, _yHead, Brushes.Yellow, window);

                return true;
            };

            return false;
        }

        //метод проигрыша (0, если не проиграли, 1 если проиграли)
        public bool Lossing()
        {
            return IsLoss;
        }

        //метод подсчета очков
        public int GetScore(SnakeClass snakeClass)
        {
            return 1000 - ((snakeClass.SpeedSnake - 1) * 100);
        }
    }
}
