using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FocusGuardApp
{
    public class RoundedPanel : Panel
    {
        private int borderRadius = 30;
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                UpdateRegion(); 
                this.Invalidate();
            }
        }

        private Color borderColor = Color.FromArgb(46, 204, 113);
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; this.Invalidate(); }
        }

        private int borderThickness = 2;
        public int BorderThickness
        {
            get => borderThickness;
            set { borderThickness = value; this.Invalidate(); }
        }

        public RoundedPanel()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
            this.DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (this.Width == 0 || this.Height == 0) return;
            using (GraphicsPath path = new GraphicsPath())
            {
                int r = BorderRadius;
                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(Width - r, 0, r, r, 270, 90);
                path.AddArc(Width - r, Height - r, r, r, 0, 90);
                path.AddArc(0, Height - r, r, r, 90, 90);
                path.CloseAllFigures();
                this.Region = new Region(path);
            }
        }
        // рамка
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                int r = BorderRadius;
                int w = this.Width - BorderThickness;
                int h = this.Height - BorderThickness;
                int t = BorderThickness / 2;

                path.AddArc(t, t, r, r, 180, 90);
                path.AddArc(w - r + t, t, r, r, 270, 90);
                path.AddArc(w - r + t, h - r + t, r, r, 0, 90);
                path.AddArc(t, h - r + t, r, r, 90, 90);
                path.CloseAllFigures();

                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}