namespace Screensaver
{
    public partial class MainForm : Form
    {

        int speedX = 5;
        int speedY = 5;

        public MainForm()
        {
            InitializeComponent();
            timer.Start();
            WindowState = FormWindowState.Maximized;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var loc = JumpingLabel.Location;
            var bounds = JumpingLabel.Bounds;
            if (bounds.Right > this.Width || bounds.Left < 0)
            {
                speedX = -speedX;
            }
            if (bounds.Top > this.Height || bounds.Bottom < 0)
            {
                speedY = -speedY;
            }


            var newLoc = new Point(loc.X + speedX, loc.Y + speedY);

            JumpingLabel.Location = newLoc;
        }
    }
}
