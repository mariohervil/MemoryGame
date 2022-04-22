using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace JuegoMemoria
{

    public enum TextPosition
    {
        Left, Right, Center, None
    }
    class CustomProgressBar : ProgressBar
    {

        private Color channelColor = Color.LightSteelBlue;
        private Color sliderColor = Color.RoyalBlue;
        private Color foreBackColor = Color.RoyalBlue;
        private int channelHeight = 6;
        private int sliderHeight = 6;
        private TextPosition showValue = TextPosition.Right;
        private Orientation o = Orientation.Vertical;

        private bool paintedBack = false;
        private bool stopPainting = false;

        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ForeColor = Color.White;
            this.o = Orientation.Vertical;
        }

        [Category("Mario Custom ProgressBar")]
        public Color ChannelColor { get { return channelColor; } set { channelColor = value; this.Invalidate(); } }
        [Category("Mario Custom ProgressBar")]
        public Color SliderColor { get { return sliderColor; } set { sliderColor = value; this.Invalidate(); } }
        [Category("Mario Custom ProgressBar")]
        public Color ForeBackColor { get => foreBackColor; set { foreBackColor = value; this.Invalidate(); } }
        [Category("Mario Custom ProgressBar")]
        public int ChannelHeight { get => channelHeight; set { channelHeight = value; this.Invalidate(); } }
        [Category("Mario Custom ProgressBar")]
        public int SliderHeight { get => sliderHeight; set { sliderHeight = value; this.Invalidate(); } }
        [Category("Mario Custom ProgressBar")]
        public TextPosition ShowValue { get => showValue; set { showValue = value; this.Invalidate(); } }

        [Category("Mario Custom ProgressBar")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Font Font { get => base.Font; set => base.Font = value; }
        [Category("Mario Custom ProgressBar")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        //pintar fondo
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (stopPainting == false)
            {
                if (paintedBack == false)
                {
                    Graphics graphics = pevent.Graphics;
                    Rectangle rectChannel = new Rectangle(0, 0, this.Width, ChannelHeight);
                    using (var brushChannel = new SolidBrush(channelColor))
                    {
                        if (channelHeight >= sliderHeight)
                        {
                            rectChannel.Y = this.Height - channelHeight;
                        }
                        else
                        {
                            rectChannel.Y = this.Height - ((channelHeight + sliderHeight) / 2);
                        }
                        graphics.Clear(this.Parent.BackColor);
                        graphics.FillRectangle(brushChannel, rectChannel);

                        if (this.DesignMode == false)
                        {
                            paintedBack = true;
                        }
                    }

                }
                if (this.Value == this.Maximum || this.Value == this.Minimum)
                {
                    paintedBack = false;
                }
                {

                }
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (stopPainting == false)
            {
                Graphics graphics = e.Graphics;
                double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
                int sliderWidth = (int)(this.Width * scaleFactor);
                Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight);
                using (var brushSlider = new SolidBrush(sliderColor))
                {
                    if (sliderHeight >= channelHeight)
                    {
                        rectSlider.Y = this.Height - sliderHeight;
                    }
                    else
                    {
                        rectSlider.Y = this.Height - ((sliderHeight + channelHeight) / 2);
                    }
                    if (sliderWidth > 1)
                    {
                        graphics.FillRectangle(brushSlider, rectSlider);
                    }
                    
                }
            }
        }

      
    }
}
