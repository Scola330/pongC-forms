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
        private int playerPaddleSpeed = 5;
        private int opponentPaddleSpeed = 5;
        private int ballX, ballY;
        private int ballSpeedX = 4, ballSpeedY = 4;
        private int playerScore = 0;
        private int opponentScore = 0;

        private SoundPlayer paddleHitSound;
        private SoundPlayer scoreSound;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            playerPaddleY = (this.ClientSize.Height - PaddleHeight) / 2;
            opponentPaddleY = (this.ClientSize.Height - PaddleHeight) / 2;
            ballX = (this.ClientSize.Width - BallSize) / 2;
            ballY = (this.ClientSize.Height - BallSize) / 2;
            ResetBall();

            paddleHitSound = new SoundPlayer("pong\\beepsound.wav");
            scoreSound = new SoundPlayer("pong\\beepsound.wav");
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
            playerPaddleY = Math.Max(0, Math.Min(this.ClientSize.Height - PaddleHeight, playerPaddleY));
            this.Invalidate();
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
                scoreSound.Play();
                ResetBall();
            }
            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                scoreSound.Play();
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
                paddleHitSound.Play();
            }
            if (ballX <= PaddleOffset + PaddleWidth && ballY + BallSize >= playerPaddleY && ballY <= playerPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
                paddleHitSound.Play();
            }

            if (ballX >= this.ClientSize.Width - PaddleOffset - PaddleWidth - BallSize && ballY + BallSize >= opponentPaddleY && ballY <= opponentPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
                paddleHitSound.Play();
            }

            if (ballX <= 0)
            {
                opponentScore++;
                scoreSound.Play();
                ResetBall();
            }

            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                scoreSound.Play();
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
                paddleHitSound.Play();
            }

            if (ballX <= PaddleOffset + PaddleWidth && ballY + BallSize >= playerPaddleY && ballY <= playerPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
                paddleHitSound.Play();
            }

            if (ballX >= this.ClientSize.Width - PaddleOffset - PaddleWidth - BallSize && ballY + BallSize >= opponentPaddleY && ballY <= opponentPaddleY + PaddleHeight)
            {
                ballSpeedX = -ballSpeedX;
                paddleHitSound.Play();
            }

            if (ballX <= 0)
            {
                opponentScore++;
                scoreSound.Play();
                ResetBall();
            }

            if (ballX >= this.ClientSize.Width - BallSize)
            {
                playerScore++;
                scoreSound.Play();
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
                g.DrawString($"PLAYER: {playerScore}", this.Font, brush, 10, 10);
                g.DrawString($"COMPUTER: {opponentScore}", this.Font, brush, this.ClientSize.Width - 100, 10);
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
            };
            timer.Start();
        }
    }
}
