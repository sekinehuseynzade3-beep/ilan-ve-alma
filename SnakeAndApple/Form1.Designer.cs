namespace SnakeAndApple;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private PictureBox pbSnake;
    private PictureBox pbApple;
    private PictureBox pbBomb;
    private Label lblInfo;
    private System.Windows.Forms.Timer gameTimer;
    private System.Windows.Forms.Timer appleTimer;
    private System.Windows.Forms.Timer bombTimer;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        pbSnake = new PictureBox();
        pbApple = new PictureBox();
        pbBomb = new PictureBox();
        lblInfo = new Label();
        gameTimer = new System.Windows.Forms.Timer(components);
        appleTimer = new System.Windows.Forms.Timer(components);
        bombTimer = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)pbSnake).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pbApple).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pbBomb).BeginInit();
        SuspendLayout();

        // pbSnake
        pbSnake.BackColor = Color.Transparent;
        pbSnake.Location = new Point(340, 400);
        pbSnake.Size = new Size(110, 110);
        pbSnake.SizeMode = PictureBoxSizeMode.Zoom;
        pbSnake.TabStop = false;

        // pbApple
        pbApple.BackColor = Color.Transparent;
        pbApple.Location = new Point(100, 80);
        pbApple.Size = new Size(95, 95);
        pbApple.SizeMode = PictureBoxSizeMode.Zoom;
        pbApple.TabStop = false;

        // pbBomb
        pbBomb.BackColor = Color.Transparent;
        pbBomb.Location = new Point(700, 200);
        pbBomb.Size = new Size(85, 85);
        pbBomb.SizeMode = PictureBoxSizeMode.Zoom;
        pbBomb.TabStop = false;

        // lblInfo
        lblInfo.AutoSize = true;
        lblInfo.BackColor = Color.Transparent;
        lblInfo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblInfo.Location = new Point(10, 8);
        lblInfo.Text = "Xal: 0   Sürət: 1";

        // gameTimer
        gameTimer.Interval = 20;
        gameTimer.Tick += gameTimer_Tick;

        // appleTimer (alma 5 saniyə ekranda qalır)
        appleTimer.Interval = 5000;
        appleTimer.Tick += appleTimer_Tick;

        // bombTimer (bomba 5 saniyə ekranda qalır)
        bombTimer.Interval = 5000;
        bombTimer.Tick += bombTimer_Tick;

        // Form1
        AutoScaleMode = AutoScaleMode.None;
        BackColor = Color.White;
        ClientSize = new Size(950, 520);
        Controls.Add(lblInfo);
        Controls.Add(pbBomb);
        Controls.Add(pbApple);
        Controls.Add(pbSnake);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Snake and apple";
        Load += Form1_Load;

        ((System.ComponentModel.ISupportInitialize)pbSnake).EndInit();
        ((System.ComponentModel.ISupportInitialize)pbApple).EndInit();
        ((System.ComponentModel.ISupportInitialize)pbBomb).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
