using System.Windows;
using System.Windows.Media;

namespace LabWork25
{
    class CustomButton : FrameworkElement
    {
        private VisualCollection _children;
        private DrawingVisual _visual;
            
        private bool _isHovered;
        private bool _isPressed;

        private double _scale = 1.0;
        private double _targetScale = 1.0;

        private Point buttonPoint;

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CustomButton));

        public Color ShadowColor { get; set; } = Color.FromRgb(0, 0, 0);
        public Color Background { get; set; } = Color.FromRgb(184, 138, 199);
        public string Text { get; set; } = "";
        public Brush Foreground { get; set; } = Brushes.Brown;
        public Color BorderColor { get; set; } = Color.FromRgb(106, 68, 56);
        public double BorderSize { get; set; } = 2;
        public FontFamily FontFamily { get; set; } = new FontFamily("Impact");
        public FontStyle FontStyle { get; set; } = FontStyles.Italic;
        public FontWeight FontWeight { get; set; } = FontWeights.Bold;
        public FontStretch FontStretch { get; set; } = FontStretches.Medium;

        private double _pressedX = 0;
        private double _pressedY = 0;

        private double _targetPressedX = 5;
        private double _targetPressedY = 5;


        protected override int VisualChildrenCount => _children.Count;
        protected override Visual GetVisualChild(int index) => _children[index];

        public CustomButton()
        {
            _children = new VisualCollection(this);
            _visual = new DrawingVisual();
            _children.Add(_visual);

            CompositionTarget.Rendering += OnRenderFrame;

            MouseEnter += CustomButton_MouseEnter;
            MouseLeave += CustomButton_MouseLeave;
            MouseDown += CustomButton_MouseDown;
            MouseUp += CustomButton_MouseUp;
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        private void CustomButton_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent, this));

            _targetScale = 1.0;
            _isPressed = false;
            _targetPressedX = 5;
            _targetPressedY = 5;
        }

        private void CustomButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _targetScale = 0.95;
            _isPressed = !_isPressed;
            _targetPressedX = 0;
            _targetPressedY = 0;
        }

        private void CustomButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _targetScale = 1.0;
            _isPressed = false;
        }

        private void CustomButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _targetScale = 1.1;
            _isHovered = !_isHovered;
        }

        private void OnRenderFrame(object? sender, EventArgs e)
        {
            _scale += (_targetScale - _scale) * 0.15;
            _pressedX += (_targetPressedX - _pressedX) * 0.15;
            _pressedY += (_targetPressedY - _pressedY) * 0.15;

            Draw();
        }

        private void Draw()
        {
            buttonPoint = new Point(Width / 2, Height / 2);

            using (var dc = _visual.RenderOpen())
            {
                dc.PushTransform(new ScaleTransform(
                    _scale, _scale, buttonPoint.X, buttonPoint.Y));

                dc.DrawRoundedRectangle(new SolidColorBrush(ShadowColor),
                    null,
                    new Rect(_pressedX, _pressedY, Width + 1, Height + 1),
                    10, 10);

                dc.DrawRoundedRectangle(new SolidColorBrush(Background),
                    new Pen(new SolidColorBrush(BorderColor), BorderSize),
                    new Rect(0, 0, Width, Height),
                    10, 10);

                var text = new FormattedText(
                    Text ?? "",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                    20,
                    Foreground,
                    1.25);


                double textX = Width / 2 - text.Width / 2;
                double textY = Height / 2 - text.Height / 2;

                dc.DrawText(text, new Point(textX, textY));

                dc.Pop();
            }
        }
    }
}
