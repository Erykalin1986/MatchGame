using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Играть еще раз?";
            }
        }

        private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }

            if (timeTextBlock.Text.Contains(" - Играть еще раз?"))
            {
                SetUpGame();
            }
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
            Random rnd = new();

            //Находит каждый элемент TextBlock в сетке и повторяет следующие команды для каждого элемента
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    //Выбирает случайное число от 0 до количества эмодзи в списке и назначает ему имя «index»
                    int index = rnd.Next(animalEmoji.Count);
                    //Использует случайное число с именем «index» для получения случайного эмодзи из списка
                    string nextEmoji = animalEmoji[index];
                    //Обновляет TextBlock случайным эмодзи из списка
                    textBlock.Text = nextEmoji;
                    textBlock.Visibility = Visibility.Visible;
                    //Удаляет случайный эмодзи из списка
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        /// <summary>
        /// Этот признак определяет, щелкнул ли игрок на первом животном в паре, и теперь пытается найти для него пару.
        /// </summary>
        bool findingMatch = false;

        /// <summary>
        /// Если щелчок сделан на первом животном в паре, 
        /// сохранить информацию о том, на каком элементе TextBlock щелкнул пользователь, 
        /// и убрать животное с экрана.Если это второе животное в паре, либо убрать его с экрана(если животные составляют пару), 
        /// либо вернуть на экран первое животное(если животные разные).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock? textBlock = sender as TextBlock;

            // Игрок только что щелкнул на первом животном в паре, поэтому это животное становится невидимым,
            // а соответствующий элемент TextBlock сохраняется на случай, если его придется делать видимым снова.
            if (findingMatch == false)
            {
                textBlock!.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            // Игрок нашел пару! Второе животное в паре становится невидимым (а при дальнейших щелчках на нем ничего не происходит),
            // а признак findingMatch сбрасывается, чтобы следующее животное, на котором щелкнет игрок, снова считалось первым в паре.
            else if (textBlock!.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            // Игрок щелкнул на животном, которое не совпадает с первым,
            // поэтому первое выбранное животное снова становится видимым, а признак findingMatch сбрасывается.
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}