using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        //10x10|15x15
        private static int matrixOrder;

        //путь к json
        private readonly string PATH;

        //для работы с json файлом
        private readonly FileIOService fileIO;

        public RecordsWindow(Map _map, string score)
        {
            InitializeComponent();

            matrixOrder = _map.MatrixOrder;

            PATH = $@"C:\recordsMap{matrixOrder}x{matrixOrder}.json";

            fileIO = new FileIOService(PATH);

            //очки
            scoreLabel.Content = score;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerNameTextBox.Text != string.Empty)
            {
                Player player1 = new Player
                {
                    PlayerName = playerNameTextBox.Text,
                    Score = scoreLabel.Content.ToString(),
                    PlayingMap = matrixOrder.ToString()
                };

                fileIO.SaveData(player1);

                //сортируем список по счету
                List<Player> listRecords = fileIO.LoadData().OrderByDescending(o => o.Score).ToList();

                //счетчик
                int i = 1;

                //записываем рекорды
                foreach (Player item in listRecords)
                {
                    recordsListBox.Items.Add($"{i}. {item.PlayerName} - {item.Score}");

                    i++;
                }

                saveButton.IsEnabled = false;
                playerNameTextBox.IsEnabled = false;
            }
        }
    }
}
