using System.Drawing.Text;
using System.Media;

namespace pong
{
    public partial class Form1 : Form
    {
        private const int PaddleWidth = 10;
        private const int PaddleHeight = 60;
        private const int PaddleOffset = 20;
        private const int BallSize = 10;
        private int playerPaddleY;
        private int opponentPaddleY;
        private int playerPaddleSpeed;
        private int opponentPaddleSpeed;
        private int ballX, ballY;
        private int ballSpeedX, ballSpeedY;
        private int playerScore = 0;
        private int opponentScore = 0;
        private int totalGames = 0;
        private string difficultyLevel = "Normal"; // Domy�lny poziom trudno�ci

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.KeyPreview = true; // Dodaj t� lini�, aby formularz odbiera� zdarzenia klawiatury

            // Wy�wietl formularz wyboru poziomu trudno�ci
            using (var difficultyForm = new DifficultySelectionForm())
            {
                if (difficultyForm.ShowDialog() == DialogResult.OK)
                {
                    difficultyLevel = difficultyForm.SelectedDifficulty;
                }
                else
                {
                    // Je�li u�ytkownik zamknie formularz bez wyboru, zamknij aplikacj�
                    Application.Exit();
                    return;
                }
            }

            LoadConfiguration();
            playerPaddleY = (this.ClientSize.Height - PaddleHeight) / 2;
            opponentPaddleY = (this.ClientSize.Height - PaddleHeight) / 2;
            ballX = (this.ClientSize.Width - BallSize) / 2;
            ballY = (this.ClientSize.Height - BallSize) / 2;
            ResetBall();
        }

        private void ShowResultsButton_Click(object? sender, EventArgs e)
        {
            ResultsForm resultsForm = new ResultsForm();
            resultsForm.Show();
        }

        private void LoadConfiguration()
        {
            string configPath = "config.txt";
            if (File.Exists(configPath))
            {
                var configLines = File.ReadAllLines(configPath);
                foreach (var line in configLines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        switch (parts[0])
                        {
                            case "playerPaddleSpeed":
                                playerPaddleSpeed = int.Parse(parts[1]);
                                break;
                            case "opponentPaddleSpeed":
                                opponentPaddleSpeed = int.Parse(parts[1]);
                                break;
                            case "ballSpeedX":
                                ballSpeedX = int.Parse(parts[1]);
                                break;
                            case "ballSpeedY":
                                ballSpeedY = int.Parse(parts[1]);
                                break;
                            case "difficultyLevel":
                                difficultyLevel = parts[1];
                                break;
                        }
                    }
                }
            }
            else
            {
                // Ustawienia domy�lne, je�li plik konfiguracyjny nie istnieje
                playerPaddleSpeed = 5;
                opponentPaddleSpeed = 5;
                ballSpeedX = 4;
                ballSpeedY = 4;
                difficultyLevel = "Normal";
            }

            // Dostosuj pr�dko�ci na podstawie poziomu trudno�ci
            switch (difficultyLevel)
            {
                case "Easy":
                    playerPaddleSpeed = 4;
                    opponentPaddleSpeed = 3;
                    ballSpeedX = 3;
                    ballSpeedY = 3;
                    break;
                case "Normal":
                    playerPaddleSpeed = 5;
                    opponentPaddleSpeed = 5;
                    ballSpeedX = 4;
                    ballSpeedY = 4;
                    break;
                case "Hard":
                    playerPaddleSpeed = 6;
                    opponentPaddleSpeed = 7;
                    ballSpeedX = 5;
                    ballSpeedY = 5;
                    break;
            }
        }

        private bool moveUp;
        private bool moveDown;

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                moveUp = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                moveDown = true;
            }
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                moveUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                moveDown = false;
            }
        }

        private void UpdatePaddlePosition()
        {
            if (moveUp && playerPaddleY > 0)
            {
                playerPaddleY -= playerPaddleSpeed;
            }
            if (moveDown && playerPaddleY < this.ClientSize.Height - PaddleHeight)
            {
                playerPaddleY += playerPaddleSpeed;
            }
            if (ballX <= 0)
            {
                opponentScore++;
                ResetBall();
            }
            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                ResetBall();
            }
            UpdateOpponentPaddlePosition();
            Invalidate();
        }

        private void UpdateOpponentPaddlePosition()
        {
            if (ballY < opponentPaddleY + PaddleHeight / 2 && opponentPaddleY > 0)
            {
                opponentPaddleY -= opponentPaddleSpeed;
            }
            else if (ballY > opponentPaddleY + PaddleHeight / 2 && opponentPaddleY < this.ClientSize.Height - PaddleHeight)
            {
                opponentPaddleY += opponentPaddleSpeed;
            }
        }

        private void ResetBall()
        {
            ballX = (this.ClientSize.Width - BallSize) / 2;
            ballY = (this.ClientSize.Height - BallSize) / 2;
            if (ballY <= 0 || ballY >= this.ClientSize.Height - BallSize)
            {
                ballSpeedY = -ballSpeedY;
            }
            if (ballX <= PaddleOffset + PaddleWidth && ballY + BallSize >= playerPaddleY && ballY <= playerPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX >= this.ClientSize.Width - PaddleOffset - PaddleWidth - BallSize && ballY + BallSize >= opponentPaddleY && ballY <= opponentPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX <= 0)
            {
                opponentScore++;
                ResetBall();
            }

            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                ResetBall();
            }

            Invalidate();
        }

        private void UpdateBallPosition()
        {
            ballX += ballSpeedX;
            ballY += ballSpeedY;

            if (ballY <= 0 || ballY >= this.ClientSize.Height - BallSize)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (ballX <= PaddleOffset + PaddleWidth && ballY + BallSize >= playerPaddleY && ballY <= playerPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX >= this.ClientSize.Width - PaddleOffset - PaddleWidth - BallSize && ballY + BallSize >= opponentPaddleY && ballY <= opponentPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX <= 0)
            {
                opponentScore++;
                ResetBall();
            }

            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                ResetBall();
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPaddles(e.Graphics);
            DrawBall(e.Graphics);
            DrawScores(e.Graphics);
        }

        private void DrawPaddles(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.White))
            {
                g.FillRectangle(brush, PaddleOffset, playerPaddleY, PaddleWidth, PaddleHeight);
                g.FillRectangle(brush, this.ClientSize.Width - PaddleOffset - PaddleWidth, opponentPaddleY, PaddleWidth, PaddleHeight);
            }
        }

        private void DrawBall(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.White))
            {
                g.FillEllipse(brush, ballX, ballY, BallSize, BallSize);
            }
        }

        private void DrawScores(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.White))
            {
                using (Font dosFont = new Font("Consolas", 12, FontStyle.Bold))
                {
                    g.DrawString($"PLAYER: {playerScore}", dosFont, brush, 10, 10);
                    g.DrawString($"COMPUTER: {opponentScore}", dosFont, brush, this.ClientSize.Width - 150, 10);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var timer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            timer.Tick += (s, ev) =>
            {
                UpdatePaddlePosition();
                UpdateBallPosition();
                CheckGameEnd();
            };
            timer.Start();
        }

        private void CheckGameEnd()
        {
            // Przyk�adowy warunek zako�czenia gry
            if (playerScore >= 3 || opponentScore >= 3)
            {
                totalGames++; // Zwi�ksz liczb� rozgrywek o 1
                SaveResultsToFile();
                Application.Exit();
            }
        }

        private void SaveResultsToFile()
        {
            
            string filePath = "game_results.txt";
            List<string> results = new List<string>();

            // Wczytaj istniej�ce wyniki
            if (File.Exists(filePath))
            {
                results.AddRange(File.ReadAllLines(filePath));
            }

            // Dodaj nowy wynik z ustawieniami
            results.Add($"Gra {totalGames + 1}:");
            results.Add($"Poziom trudno�ci: {difficultyLevel}");
            results.Add($"Pr�dko�� paletki gracza: {playerPaddleSpeed}");
            results.Add($"Pr�dko�� paletki przeciwnika: {opponentPaddleSpeed}");
            results.Add($"Pr�dko�� pi�ki (X, Y): ({ballSpeedX}, {ballSpeedY})");
            results.Add($"Wynik gracza: {playerScore}");
            results.Add($"Wynik przeciwnika: {opponentScore}");
            results.Add($"Liczba rozgrywek: {totalGames}");
            results.Add(""); // Pusta linia dla oddzielenia wynik�w

            // Zachowaj tylko trzy ostatnie wyniki
            if (results.Count > 24) // 8 linii na gr�, wi�c 3 gry to 24 linii
            {
                results = results.Skip(results.Count - 24).ToList();
            }

            // Zapisz wyniki do pliku
            File.WriteAllLines(filePath, results);
            
        }
    }
}
