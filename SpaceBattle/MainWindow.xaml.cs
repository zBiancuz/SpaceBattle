using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
namespace SpaceBattle
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool MuoviSinistra1, MuoviDestra1, MuoviSinistra2, MuoviDestra2;
        List<Rectangle> itemRemover = new List<Rectangle>();

        Random rand = new Random();

        int enemySpriteCounter = 0;
        int enemyCounter = 100;
        int playerSpeed = 50;
        int limit = 50;
        int score1 = 0;
        int score2 = 0;
        int totalScore = 0;
        int damage = 0;
        int enemySpeed = 10;

        Rect playerHitBox1, playerHitBox2;


        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(29);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            MyCanvas.Focus();

            ImageBrush bg = new ImageBrush();



            //SFONDI E IMMAGINI GIOCATORE 

            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = bg;

            ImageBrush playerImage1 = new ImageBrush();
            playerImage1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/player1.png"));
            player1.Fill = playerImage1;

            ImageBrush playerImage2 = new ImageBrush();
            playerImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/player2.png"));
            player2.Fill = playerImage2;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            playerHitBox1 = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);
            playerHitBox2 = new Rect(Canvas.GetLeft(player2), Canvas.GetTop(player2), player2.Width, player2.Height);

            enemyCounter -= 1;

            scoreText1.Content = "Score P1: " + score1;
            scoreText2.Content = "Score P2: " + score2;
            TotalScorePlayer.Content = "Score TOT:" + totalScore;

            damageText.Content = "Damage: " + damage;

            if (enemyCounter < 0)
            {
                GeneraNemici();
                enemyCounter = limit;
            }
            

            if (MuoviSinistra1 == true && Canvas.GetLeft(player1) > 0)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - playerSpeed);
            }
            if (MuoviDestra1 == true && Canvas.GetLeft(player1) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + playerSpeed);
            }
            if (MuoviSinistra2 == true && Canvas.GetLeft(player2) > 0)
            {
                Canvas.SetLeft(player2, Canvas.GetLeft(player2) - playerSpeed);
            }
            if (MuoviDestra2 == true && Canvas.GetLeft(player2) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player2, Canvas.GetLeft(player2) + playerSpeed);
            }


            
            MuoviSinistra1 = false;
            MuoviDestra1 = false;                  //TASTI PER FAR MUOVERE  
            MuoviSinistra2 = false;                   //DA COMMENTARE
            MuoviDestra2 = false;
            


            //PROIETTILI ROSSI GIOCATORE 1
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bulletRed")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    
                    if (Canvas.GetTop(x) < 10)
                    {
                        itemRemover.Add(x);
                    }

                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect (Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                score1 += 10;
                                totalScore += 10;
                            }
                        }
                    }

                }

                //PROIETTILI ROSSI GIOCATORE 2
                if (x is Rectangle && (string)x.Tag == "bulletBlue")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemRemover.Add(x);
                    }
                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                score2 += 10;
                                totalScore += 10;
                            }
                        }
                    }

                }

                //NEMICI
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);
                        damage += 10;
                    }
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox1.IntersectsWith(enemyHitBox) && playerHitBox2.IntersectsWith(enemyHitBox))
                    {
                        itemRemover.Add(x);
                        damage += 5;
                    }
                }

            }


            foreach (Rectangle i in itemRemover)
            {
                MyCanvas.Children.Remove(i);
            }





            //FINE GIOCO E MESSAGGIO FINE PER IRCOMINCIARE 

            if (totalScore > 200 )
            {
                limit = 20;
                enemySpeed = 15;
            }

            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Damage: 100";
                damageText.Foreground = Brushes.Red;
                MessageBox.Show("Hai distrutto " + totalScore / 10 + " navicelle aliene " + Environment.NewLine + "Premi OK per giocare di nuovo");

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }




        }
        //TASTI PER FAR MUOVERE LE NAVICELLE
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                MuoviSinistra1 = true;
            }
            if (e.Key == Key.D)
            {
                MuoviDestra1 = true;
            }
            if (e.Key == Key.Left)
            {
                MuoviSinistra2 = true;
            }
            if (e.Key == Key.Right)
            {
                MuoviDestra2 = true;
            }
            if (e.Key == Key.W)
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bulletRed",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player1) + player1.Width / 2);
                Canvas.SetTop(newBullet, Canvas.GetTop(player1) - newBullet.Height);

                MyCanvas.Children.Add(newBullet);    
            }

            if (e.Key == Key.Up) 
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bulletBlue",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Blue,
                };
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player2) + player2.Width / 2);
                Canvas.SetTop(newBullet, Canvas.GetTop(player2) - newBullet.Height);

                MyCanvas.Children.Add(newBullet);
            }
        }




        private void onKeyUp(object sender, KeyEventArgs e)
        {

        }





        private void GeneraNemici()
        {
            ImageBrush enemySprite = new ImageBrush();
            enemySpriteCounter = rand.Next(1, 5);
            switch(enemySpriteCounter)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri ("pack://application:,,,/image/1.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/2.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/3.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/4.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/5.png"));
                    break;

            }

            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite
            };

            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));
            MyCanvas.Children.Add(newEnemy);

        }
    }
}
