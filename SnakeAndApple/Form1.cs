namespace SnakeAndApple;

public partial class Form1 : Form
{
    private const int BaseSpeed = 4;      // başlanğıc sürət (piksel / tick)
    private const int MaxSpeed = 18;      // maksimum sürət
    private const double BombChance = 0.7; // bombanın ekranda görünmə ehtimalı

    private readonly Random rnd = new();
    private Image snakeRight = null!;
    private Image snakeLeft = null!;

    private int score;
    private int speed;
    private int dx, dy;   // hərəkət istiqaməti

    public Form1()
    {
        InitializeComponent();
        LoadImages();
    }

    private void LoadImages()
    {
        string dir = Path.Combine(AppContext.BaseDirectory, "Images");
        snakeRight = Image.FromFile(Path.Combine(dir, "snake.png"));
        snakeLeft = (Image)snakeRight.Clone();
        snakeLeft.RotateFlip(RotateFlipType.RotateNoneFlipX);

        pbApple.Image = Image.FromFile(Path.Combine(dir, "apple.png"));
        pbBomb.Image = Image.FromFile(Path.Combine(dir, "bomb.png"));
        pbSnake.Image = snakeRight;
    }

    private void Form1_Load(object? sender, EventArgs e) => StartNewGame();

    // ---------- Yeni oyun ----------
    private void StartNewGame()
    {
        score = 0;          // xal sıfırlanır
        speed = BaseSpeed;  // sürət sıfırlanır
        dx = 1; dy = 0;
        pbSnake.Image = snakeRight;
        pbSnake.Location = new Point(ClientSize.Width / 2 - pbSnake.Width / 2,
                                     ClientSize.Height - pbSnake.Height - 10);
        PlaceApple();
        PlaceBomb();
        UpdateInfo();
        gameTimer.Start();
        Focus();
    }

    private void UpdateInfo()
    {
        lblInfo.Text = $"Xal: {score}   Sürət: {speed - BaseSpeed + 1}";
    }

    // ---------- İdarəetmə ----------
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Left:  case Keys.A: dx = -1; dy = 0; pbSnake.Image = snakeLeft;  return true;
            case Keys.Right: case Keys.D: dx = 1;  dy = 0; pbSnake.Image = snakeRight; return true;
            case Keys.Up:    case Keys.W: dx = 0;  dy = -1; return true;
            case Keys.Down:  case Keys.S: dx = 0;  dy = 1;  return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    // ---------- Əsas oyun dövrü ----------
    private void gameTimer_Tick(object? sender, EventArgs e)
    {
        MoveSnake();

        Rectangle snakeRect = Rectangle.Inflate(pbSnake.Bounds, -25, -25);

        // Bomba yeyildi -> oyun bitdi
        if (pbBomb.Visible && snakeRect.IntersectsWith(Rectangle.Inflate(pbBomb.Bounds, -15, -15)))
        {
            GameOver();
            return;
        }

        // Alma yeyildi -> xal artır, sürət artır, alma təsadüfi yerə atılır
        if (snakeRect.IntersectsWith(Rectangle.Inflate(pbApple.Bounds, -15, -15)))
        {
            score++;
            if (speed < MaxSpeed) speed++;
            UpdateInfo();
            PlaceApple();
        }
    }

    private void MoveSnake()
    {
        int x = pbSnake.Left + dx * speed;
        int y = pbSnake.Top + dy * speed;

        // Ekranın kənarından çıxanda qarşı tərəfdən daxil olur
        if (x > ClientSize.Width) x = -pbSnake.Width;
        else if (x < -pbSnake.Width) x = ClientSize.Width;
        if (y > ClientSize.Height) y = -pbSnake.Height;
        else if (y < -pbSnake.Height) y = ClientSize.Height;

        pbSnake.Location = new Point(x, y);
    }

    // ---------- Alma / bomba yerləşdirmə ----------
    private void PlaceApple()
    {
        pbApple.Location = RandomFreeLocation(pbApple.Size, pbBomb.Visible ? pbBomb.Bounds : Rectangle.Empty);
        pbApple.Visible = true;
        appleTimer.Stop();   // 5 saniyəlik sayğac yenidən başlayır
        appleTimer.Start();
    }

    private void PlaceBomb()
    {
        // Bomba həmişə yox, təsadüfi olaraq görünür
        if (rnd.NextDouble() < BombChance)
        {
            pbBomb.Location = RandomFreeLocation(pbBomb.Size, pbApple.Bounds);
            pbBomb.Visible = true;
        }
        else
        {
            pbBomb.Visible = false;
        }
        bombTimer.Stop();
        bombTimer.Start();
    }

    // 5 saniyə ərzində yeyilmədi -> alma təsadüfi yerə atılır
    private void appleTimer_Tick(object? sender, EventArgs e) => PlaceApple();

    // 5 saniyə keçdi -> bomba yerini dəyişir (və ya ekrandan yox olur)
    private void bombTimer_Tick(object? sender, EventArgs e) => PlaceBomb();

    private Point RandomFreeLocation(Size size, Rectangle avoid)
    {
        int minY = lblInfo.Bottom + 5;
        Point p = Point.Empty;
        for (int i = 0; i < 50; i++)
        {
            p = new Point(rnd.Next(0, ClientSize.Width - size.Width),
                          rnd.Next(minY, ClientSize.Height - size.Height));
            Rectangle r = new(p, size);
            bool overlapsSnake = r.IntersectsWith(Rectangle.Inflate(pbSnake.Bounds, 30, 30));
            bool overlapsOther = !avoid.IsEmpty && r.IntersectsWith(Rectangle.Inflate(avoid, 10, 10));
            if (!overlapsSnake && !overlapsOther) break;
        }
        return p;
    }

    // ---------- Oyun bitdi ----------
    private void GameOver()
    {
        gameTimer.Stop();
        appleTimer.Stop();
        bombTimer.Stop();

        MessageBox.Show($"Bomba yeyildi! Oyun bitdi.\n\nNəticəniz: {score} xal",
                        "Oyun bitdi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        StartNewGame();   // OK basılanda yeni oyun başlayır
    }
}
