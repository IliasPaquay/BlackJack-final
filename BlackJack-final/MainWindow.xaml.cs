using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BlackJack_final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        DropShadowEffect dropShadow = new DropShadowEffect();
        MediaPlayer backgroundMusic = new MediaPlayer();
        StringBuilder history = new StringBuilder();
        DispatcherTimer slideSpeler = new DispatcherTimer();
        DispatcherTimer slideBank = new DispatcherTimer();
        DispatcherTimer timerSpeler = new DispatcherTimer();
        DispatcherTimer timerBank = new DispatcherTimer();
        DispatcherTimer Tijd = new DispatcherTimer();
        DispatcherTimer shuffle = new DispatcherTimer();
        int winst;
        int hit = 1;
        int i = 10;
        int x = 10;
        int kaartDraai;
        int totaal = 1000;
        int inzet;
        int random;
        int scoreSpeler, scoreBank;
        int getal;
        bool buttonHit = false;
        bool buttonDeel = false;
        bool buttonStand = false;
        bool buttonDbDown = false;
        List<string> scoreHistory = new List<string>();
        List<string> kaarten = new List<string>
           {"ace_of_clubs.png", "2_of_clubs.png", "3_of_clubs.png", "4_of_clubs.png", "5_of_clubs.png", "6_of_clubs.png", "7_of_clubs.png", "8_of_clubs.png","9_of_clubs.png","10_of_clubs.png", "jack_of_clubs2.png", "queen_of_clubs2.png", "king_of_clubs2.png",
             "ace_of_diamonds.png", "2_of_diamonds.png", "3_of_diamonds.png", "4_of_diamonds.png", "5_of_diamonds.png", "6_of_diamonds.png", "7_of_diamonds.png", "8_of_diamonds.png","9_of_diamonds.png","10_of_diamonds.png", "jack_of_diamonds2.png", "queen_of_diamonds2.png", "king_of_diamonds2.png",
             "ace_of_spades.png", "2_of_spades.png", "3_of_spades.png", "4_of_spades.png", "5_of_spades.png", "6_of_spades.png", "7_of_spades.png", "8_of_spades.png","9_of_spades.png","10_of_spades.png", "jack_of_spades2.png", "queen_of_spades2.png", "king_of_spades2.png",
               "ace_of_hearts.png", "2_of_hearts.png", "3_of_hearts.png", "4_of_hearts.png", "5_of_hearts.png", "6_of_hearts.png", "7_of_hearts.png", "8_of_hearts.png","9_of_hearts.png","10_of_hearts.png", "jack_of_hearts2.png", "queen_of_hearts2.png", "king_of_hearts2.png"};

        public MainWindow()
        {
            InitializeComponent();
             BackgroundMusic();
            TxtTijd.Content = DateTime.Now.ToLocalTime();
            dropShadow.Color = Colors.AntiqueWhite;
            dropShadow.Direction = 315;
            dropShadow.ShadowDepth = 10;
            dropShadow.BlurRadius = 25;
            shuffle.Interval = TimeSpan.FromSeconds(0.01);
            slideSpeler.Interval = TimeSpan.FromMilliseconds(1);
            slideBank.Interval = TimeSpan.FromMilliseconds(1);
            timerSpeler.Interval = TimeSpan.FromSeconds(1);
            timerBank.Interval = TimeSpan.FromSeconds(1);

            Tijd.Interval = TimeSpan.FromSeconds(1);
            Tijd.Tick += TijdNu;
            Tijd.Start();
            timerSpeler.Tick += AddCardPlayer;
            timerBank.Tick += AddCardBank;
            shuffle.Tick += Shuffle;
            slideSpeler.Tick += MoveImageSpeler;
            slideBank.Tick += MoveImageBank;
        }


        private void TijdNu(object sender, EventArgs e)
        {
            TxtTijd.Content = DateTime.Now.ToLocalTime();
          
        }

       
        private void BankUp()
        {
            random = rnd.Next(0, kaarten.Count );
            Image lastImageSpeler = (Image)MyCanvas.Children[MyCanvas.Children.Count - hit];
            lastImageSpeler.Source = new BitmapImage(new Uri($@"\Assets\Cards\{kaarten[random]}", UriKind.Relative));
            lastImageSpeler.Width = 150;
            Canvas.SetTop(lastImageSpeler, 250);
            ScoreBerekenenBank(kaarten[random]);
            kaarten.RemoveAt(random);
           
        }

        private void Shuffle(object sender, EventArgs e)
        {
            ImgDeck.RenderTransform = new RotateTransform(kaartDraai, ImgDeck.Height/2, ImgDeck.Width/2);
            kaartDraai += 10;
            if(kaartDraai == 370)
            {
                shuffle.Stop();
                kaartDraai = 0;
            }
        }
        private void AddCardPlayer(object sender, EventArgs e)
        {
            if(kaarten.Count == 0)
            {
                kaarten = new List<string>

                { "ace_of_clubs.png", "2_of_clubs.png", "3_of_clubs.png", "4_of_clubs.png", "5_of_clubs.png", "6_of_clubs.png", "7_of_clubs.png", "8_of_clubs.png","9_of_clubs.png","10_of_clubs.png", "jack_of_clubs2.png", "queen_of_clubs2.png", "king_of_clubs2.png",
             "ace_of_diamonds.png", "2_of_diamonds.png", "3_of_diamonds.png", "4_of_diamonds.png", "5_of_diamonds.png", "6_of_diamonds.png", "7_of_diamonds.png", "8_of_diamonds.png","9_of_diamonds.png","10_of_diamonds.png", "jack_of_diamonds2.png", "queen_of_diamonds2.png", "king_of_diamonds2.png",
             "ace_of_spades.png", "2_of_spades.png", "3_of_spades.png", "4_of_spades.png", "5_of_spades.png", "6_of_spades.png", "7_of_spades.png", "8_of_spades.png","9_of_spades.png","10_of_spades.png", "jack_of_spades2.png", "queen_of_spades2.png", "king_of_spades2.png",
               "ace_of_hearts.png", "2_of_hearts.png", "3_of_hearts.png", "4_of_hearts.png", "5_of_hearts.png", "6_of_hearts.png", "7_of_hearts.png", "8_of_hearts.png","9_of_hearts.png","10_of_hearts.png", "jack_of_hearts2.png", "queen_of_hearts2.png", "king_of_hearts2.png"};
                shuffle.Start();
            }
           
             
            if (buttonDbDown == true)
            {
               timerSpeler.Stop();
                random = rnd.Next(0, kaarten.Count);
                Image img = new Image();
                img.Width = 150;
                img.Source = new BitmapImage(new Uri($@"\Assets\Cards\{kaarten[random]}", UriKind.RelativeOrAbsolute));
                Canvas.SetLeft(img, 1250);
                img.RenderTransform = new RotateTransform(90);

                img.Effect = dropShadow;
                MyCanvas.Children.Add(img);
                slideSpeler.Start();
            }
            else
            {
               
                random = rnd.Next(0, kaarten.Count);
                Image img = new Image();
                img.Width = 150;
                img.Source = new BitmapImage(new Uri($@"\Assets\Cards\{kaarten[random]}", UriKind.RelativeOrAbsolute));
                Canvas.SetLeft(img, 1250);
                img.Effect = dropShadow;
                MyCanvas.Children.Add(img);
                slideSpeler.Start();
            }
            if (getal == 1)
            {
                timerSpeler.Stop();
                timerBank.Start();
            }
            getal++;
            if (buttonHit == true)
            {
                timerSpeler.Stop();
            }
            ScoreBerekenen(kaarten[random]);
            kaarten.RemoveAt(random);
            

        }
        /// <summary>
        /// Slide de speler kaart naar het speel veld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveImageSpeler(object sender, EventArgs e)
        {
            Image lastImageSpeler = (Image)MyCanvas.Children[MyCanvas.Children.Count - 1];

            Canvas.SetLeft(lastImageSpeler, Canvas.GetLeft(lastImageSpeler) - 50);

            if (Canvas.GetLeft(lastImageSpeler) < i)
            {
                Canvas.SetLeft(lastImageSpeler, Canvas.GetLeft(lastImageSpeler));
                if (buttonDbDown == true)
                {
                    i += 150;
                    timerBank.Start();
                }
                else
                {

                    i += 50;
                }
                slideSpeler.Stop();
            }
        }



        /// <summary>
        /// voegt de kaarten voor bank toe aan het canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCardBank(object sender, EventArgs e)
        {
            if (kaarten.Count == 0)
            {
                kaarten = new List<string>

                { "ace_of_clubs.png", "2_of_clubs.png", "3_of_clubs.png", "4_of_clubs.png", "5_of_clubs.png", "6_of_clubs.png", "7_of_clubs.png", "8_of_clubs.png","9_of_clubs.png","10_of_clubs.png", "jack_of_clubs2.png", "queen_of_clubs2.png", "king_of_clubs2.png",
             "ace_of_diamonds.png", "2_of_diamonds.png", "3_of_diamonds.png", "4_of_diamonds.png", "5_of_diamonds.png", "6_of_diamonds.png", "7_of_diamonds.png", "8_of_diamonds.png","9_of_diamonds.png","10_of_diamonds.png", "jack_of_diamonds2.png", "queen_of_diamonds2.png", "king_of_diamonds2.png",
             "ace_of_spades.png", "2_of_spades.png", "3_of_spades.png", "4_of_spades.png", "5_of_spades.png", "6_of_spades.png", "7_of_spades.png", "8_of_spades.png","9_of_spades.png","10_of_spades.png", "jack_of_spades2.png", "queen_of_spades2.png", "king_of_spades2.png",
               "ace_of_hearts.png", "2_of_hearts.png", "3_of_hearts.png", "4_of_hearts.png", "5_of_hearts.png", "6_of_hearts.png", "7_of_hearts.png", "8_of_hearts.png","9_of_hearts.png","10_of_hearts.png", "jack_of_hearts2.png", "queen_of_hearts2.png", "king_of_hearts2.png"};
                shuffle.Start();
            }
            if (buttonDbDown == true)
            {
                BankUp();
            }

            if (scoreBank <= 17)
            {
                if (MyCanvas.Children.Count == 3)
                {
                    Image img = new Image();
                    img.Width = 182;
                    img.Source = new BitmapImage(new Uri($@"\Assets\Cards\card-back-red.png", UriKind.Relative));
                    Canvas.SetLeft(img, 1250);
                    Canvas.SetTop(img, 240);
                  
                    img.Effect = dropShadow;
                    MyCanvas.Children.Add(img);
                    slideBank.Start();
                }
                else if (MyCanvas.Children.Count != 3)
                {
                    random = rnd.Next(0, kaarten.Count);
                    Image img = new Image();
                    img.Width = 150;
                    img.Source = new BitmapImage(new Uri($@"\Assets\Cards\{kaarten[random]}", UriKind.Relative));
                  
                    img.Effect = dropShadow;
                    Canvas.SetLeft(img, 1250);
                    Canvas.SetTop(img, 250);
                    MyCanvas.Children.Add(img);
                    ScoreBerekenenBank(kaarten[random]);
                    kaarten.RemoveAt(random);
                    slideBank.Start();
                }
            }

            timerBank.Stop();
        }
        /// <summary>
        /// Slide de bank kaart naar het speel veld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveImageBank(object sender, EventArgs e)
        {
            Image lastImageBank = (Image)MyCanvas.Children[MyCanvas.Children.Count - 1];
            Canvas.SetLeft(lastImageBank, Canvas.GetLeft(lastImageBank) - 50);

            if (Canvas.GetLeft(lastImageBank) < x)
            {
                slideBank.Stop();
                Canvas.SetLeft(lastImageBank, Canvas.GetLeft(lastImageBank));
                x += 50;
                if (buttonDeel == true)
                {
                    slideBank.Stop();
                    buttonDeel = false;
                    timerBank.Start();


                }
            }

            if (buttonStand == true || buttonDbDown == true)
            {
                if (scoreBank <= 17)
                {
                    timerBank.Start();

                }
                if (scoreBank > 17)
                {
                    einde_ronde();
                }
            }
           
        }




        /// <summary>
        /// score berekenen voor speler
        /// </summary>
        /// <param name="kaart"></param>
        private void ScoreBerekenen(string kaart)
        {
            int waarde;
            if (kaart.StartsWith("10") || kaart.StartsWith("jack") || kaart.StartsWith("queen") || kaart.StartsWith("king"))
            {
                scoreSpeler += 10;
            }
            else if (int.TryParse(kaart.Substring(0, 1), out waarde))
            {
                //NORMAL
                scoreSpeler += waarde;
            }
            else
            { //ACE
                if (scoreSpeler <= 10)
                {
                    scoreSpeler += 11;
                }
                else scoreSpeler += 1;
            }


            TxtScoreSpeler.Text = $"Score Speler: {scoreSpeler.ToString()}";

        }
        /// <summary>
        /// score bereken voor bank
        /// </summary>
        /// <param name="kaart"></param>
        private void ScoreBerekenenBank(string kaart)
        {
            int waarde;
            if (kaart.StartsWith("10") || kaart.StartsWith("jack") || kaart.StartsWith("queen") || kaart.StartsWith("king"))    
            {
                scoreBank += 10;
            }
            else if (int.TryParse(kaart.Substring(0, 1), out waarde))
            {
                //NORMAL
                scoreBank += waarde;
            }
            else
            { //ACE
                if (scoreBank <= 10)
                {
                    scoreBank += 11;
                }
                else scoreBank += 1;
            }

            if(scoreBank > 17)
            {
                einde_ronde();
            }

            TxtScoreBank.Text = $"Score Bank: {scoreBank.ToString()}";
        }
        /// <summary>
        /// de background muziek als het spel opstart. default funk
        /// </summary>
        private void BackgroundMusic()
        {
            backgroundMusic.Open(new Uri(@"\Assets\Ketsa-Funk-Saviour.wav", UriKind.Relative));
            backgroundMusic.Play();
        }
        /// <summary>
        /// slider voor de volume van de achtergrond muziek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SldrMusic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)// slider voor muziek
        {
            backgroundMusic.Volume = SldrMusic.Value;
            LblVolume.Content = $"Volume: {SldrMusic.Value * 10}";
        }
        /// <summary>
        /// click event voor de menu items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Options(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            switch (menu.Header)
            {
                case "Jazz":
                    backgroundMusic.Open(new Uri(@"..\..\Assets\HoliznaRAPS-Flat-Lining-_Instrumental_.wav", UriKind.Relative));
                    break;
                case "Funk":
                    backgroundMusic.Open(new Uri(@"..\..\Assets\Ketsa-Funk-Saviour.wav", UriKind.Relative));
                    break;
                case "Restart":
                    Reset();
                    break;
                case "Close":
                    Close();
                    break;
            }
            backgroundMusic.Play();
        }// muziek veranderd als je op andere genre klinkt
        /// <summary>
        /// reset het veld als de ronde voorbij is
        /// </summary>
        private void NewGame()
        {
            MyCanvas.Children.Clear();
            
            i = 10;
            x = 10;
            scoreSpeler = 0;
            scoreBank = 0;
            TxtScoreBank.Text = $"Score Bank: {scoreBank.ToString()}";
            TxtScoreSpeler.Text = $"Score Speler: {scoreSpeler.ToString()}";
           
            ImgLogo.Visibility = Visibility.Visible;
            ImgBust.Visibility = Visibility.Hidden;
            ImgWin.Visibility = Visibility.Hidden;
            ImgPush.Visibility = Visibility.Hidden;
            getal = 0;
            hit = 1;
            
            buttonDbDown = false;
            buttonDeel = false;
            buttonHit = false;
            buttonStand = false;
           
         
            BtnDeel.IsEnabled = true;
            Btn10.IsEnabled = true;
            Btn100.IsEnabled = true;
            Btn50.IsEnabled = true;
            Btn200.IsEnabled = true;
            Btn500.IsEnabled = true;
            SldrInzet.IsEnabled = true;
        }
        private void Reset()
        {
            kaarten = new List<string>

                { "ace_of_clubs.png", "2_of_clubs.png", "3_of_clubs.png", "4_of_clubs.png", "5_of_clubs.png", "6_of_clubs.png", "7_of_clubs.png", "8_of_clubs.png","9_of_clubs.png","10_of_clubs.png", "jack_of_clubs2.png", "queen_of_clubs2.png", "king_of_clubs2.png",
             "ace_of_diamonds.png", "2_of_diamonds.png", "3_of_diamonds.png", "4_of_diamonds.png", "5_of_diamonds.png", "6_of_diamonds.png", "7_of_diamonds.png", "8_of_diamonds.png","9_of_diamonds.png","10_of_diamonds.png", "jack_of_diamonds2.png", "queen_of_diamonds2.png", "king_of_diamonds2.png",
             "ace_of_spades.png", "2_of_spades.png", "3_of_spades.png", "4_of_spades.png", "5_of_spades.png", "6_of_spades.png", "7_of_spades.png", "8_of_spades.png","9_of_spades.png","10_of_spades.png", "jack_of_spades2.png", "queen_of_spades2.png", "king_of_spades2.png",
               "ace_of_hearts.png", "2_of_hearts.png", "3_of_hearts.png", "4_of_hearts.png", "5_of_hearts.png", "6_of_hearts.png", "7_of_hearts.png", "8_of_hearts.png","9_of_hearts.png","10_of_hearts.png", "jack_of_hearts2.png", "queen_of_hearts2.png", "king_of_hearts2.png"};
            shuffle.Start();
            totaal = 1000;
            inzet = 0;
            history.Clear();
            scoreHistory.Clear();
            NewGame();
        }
        /// <summary>
        /// wincondities aan het einde van de ronde
        /// </summary>
        private void einde_ronde()
        { 
            if (scoreBank == scoreSpeler)
            {
                winst = 0;
                // PUSH  afeelding met push
                ImgLogo.Visibility = Visibility.Hidden;
                ImgWin.Visibility = Visibility.Hidden;
                ImgPush.Visibility = Visibility.Visible;
                BtmBar.Background = Brushes.Gray;
                totaal += inzet;
            }

          else  if(scoreSpeler <= 21 && scoreBank > 21)
            {
                winst = inzet;
                ImgLogo.Visibility = Visibility.Hidden;
                ImgWin.Visibility = Visibility.Visible;
                ImgPush.Visibility = Visibility.Hidden;
                BtmBar.Background = Brushes.Green;
                totaal += inzet*2;
            }
            else if (scoreSpeler < 21 && scoreBank == 21)
            {
                winst = -inzet;
                ImgWin.Visibility = Visibility.Hidden;
                ImgBust.Visibility = Visibility.Visible;
                ImgLogo.Visibility = Visibility.Hidden;
                BtmBar.Background = Brushes.Red;
            }
            else if (scoreSpeler > 21 )
            {
                winst = -inzet;
                ImgWin.Visibility = Visibility.Hidden;
                ImgBust.Visibility = Visibility.Visible;
                ImgLogo.Visibility = Visibility.Hidden;
                BtmBar.Background = Brushes.Red;             
            }
            else if(scoreSpeler == 21 && scoreBank < 21)
            {
                winst = inzet;
                ImgLogo.Visibility = Visibility.Hidden;
                ImgWin.Visibility = Visibility.Visible;
                ImgPush.Visibility = Visibility.Hidden;
                BtmBar.Background = Brushes.Green;
                totaal += inzet * 2;
            }
            else if(scoreSpeler < 21 && scoreBank < 21)
            {
                if( inzet - scoreSpeler < inzet - scoreBank)
                {
                    winst= inzet;
                    ImgLogo.Visibility = Visibility.Hidden;
                    ImgWin.Visibility = Visibility.Visible;
                    ImgPush.Visibility = Visibility.Hidden;
                    BtmBar.Background = Brushes.Green;
                    totaal += inzet*2;
                }
                else
                {
                    winst = -inzet;
                    ImgBust.Visibility = Visibility.Visible;
                    ImgLogo.Visibility = Visibility.Hidden;
                    BtmBar.Background = Brushes.Red;
                }
            }
            TxtHistory.Content = $"{winst:c} - {scoreSpeler}/{scoreBank}";
            


            scoreHistory.Add(TxtHistory.Content.ToString());
           
            inzet = 0;
            TxtInzet.Text = $"inzet: {inzet}";
            TxtTotaal.Text = $"totaal: {totaal}";
            SldrInzet.IsEnabled = true;
            

        }
        /// <summary>
        /// click event voor de verschillende knoppen op de kaarten te delen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "BtnHit":
                    hit++;
                    timerSpeler.Start();
                    buttonHit = true;
                    break;
                case "BtnDeel":
                    if(kaarten.Count != 52)
                    {
                        NewGame();
                    }
                    BtnHit.IsEnabled = true;
                    BtnStand.IsEnabled = true;
                    BtnDubble.IsEnabled = true;
                    BtnDeel.IsEnabled = false;
                    buttonDeel = true;
                    timerSpeler.Start();
                    break;
                case "BtnStand":       
                    BankUp();
                    timerBank.Start();
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDubble.IsEnabled = false;
                    SldrInzet.IsEnabled = true;
                    BtnConfirm.IsEnabled = true;
                    buttonStand = true;
                    break;
            }
        }
        /// <summary>
        /// slider voor de inzet te berekenen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SldrInzet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            inzet = (totaal/100)  * int.Parse(SldrInzet.Value.ToString());
            SldrInzet.ToolTip = inzet.ToString();
           
            TxtInzet.Text = $"Inzet : {inzet:c}";
            
        }
        /// <summary>
        /// historiek voor de vorige score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtHistory_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
          
            history.Clear();
            history.AppendLine("laatste 10 games gespeeld");

            for (int z = 0; z < scoreHistory.Count - 1; z++)
            {
                history.AppendLine(scoreHistory[z]);
                z++;
            }

            MessageBox.Show($"{history}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Inzet_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Btn10":
                    SldrInzet.Value = 10;
                    break;
                case "Btn50":
                    inzet += 50;
                    totaal -= 50;
                    break;
                case "Btn100":
                    inzet += 100;
                    totaal -= 100;
                    break;
                case "Btn200":
                    inzet += 200;
                    totaal -= 200;
                    break;
                case "Btn500":
                    inzet += 500;
                    totaal -= 500;
                    break;
                case "BtnConfirm":
                    BtnDeel.IsEnabled = true;
                    Btn10.IsEnabled = false;
                    Btn100.IsEnabled = false;
                    Btn50.IsEnabled = false;
                    Btn200.IsEnabled = false;
                    Btn500.IsEnabled = false;
                   SldrInzet.IsEnabled= false;
                    ImgBust.Visibility= Visibility.Hidden;
                    ImgPush.Visibility= Visibility.Hidden;
                    ImgWin.Visibility= Visibility.Hidden;
                    ImgLogo.Visibility= Visibility.Visible;
                    totaal -= inzet;
                    break;
                case "BtnDubble":
                    if (inzet * 2 <= totaal)
                    {
                        buttonDbDown = true;
                        totaal -= inzet;
                        inzet += inzet;
                        timerSpeler.Start();
                        hit++;
                    }
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDubble.IsEnabled = false;
                    BtnDeel.IsEnabled = true;
                    break;

            }
            if (totaal < 10)
            {
                Btn10.IsEnabled = false;
            }
            if (totaal < 50)
            {
                Btn50.IsEnabled = false;
            }
            if (totaal < 100)
            {
                Btn100.IsEnabled = false;
            }
            if (totaal < 200)
            {
                Btn200.IsEnabled = false;
            }
            if (totaal < 500)
            {
                Btn500.IsEnabled = false;
            }
            
            TxtTotaal.Text = $"Totaal : {totaal:c}";
        }
    }
}
