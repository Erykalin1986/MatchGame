using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            //Создает список из восьми пар эмодзи
            List<string> animalEmoji =
            [
                "🐙","🐙",
                "🐟","🐟",
                "🐘","🐘",
                "🐳","🐳",
                "🐪","🐪",
                "🦕","🦕",
                "🦍","🦍",
                "🦔","🦔",
            ];

            //Создает новый генератор случайных чисел
            Random rnd = new ();

            //Находит каждый элемент TextBlock в сетке и повторяет следующие команды для каждого элемента
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                //Выбирает случайное число от 0 до количества эмодзи в списке и назначает ему имя «index»
                int index = rnd.Next(animalEmoji.Count);
                //Использует случайное число с именем «index» для получения случайного эмодзи из списка
                string nextEmoji = animalEmoji[index];
                //Обновляет TextBlock случайным эмодзи из списка
                textBlock.Text = nextEmoji;
                //Удаляет случайный эмодзи из списка
                animalEmoji.RemoveAt(index);
            }
        }
    }
}