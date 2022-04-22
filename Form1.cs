using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoMemoria
{
    /*
     * 
     * meter todos los picture box en array y recorrer con foreach para cambiar el nombre
     * cambiar el nombre de las picture box al nombre de la imagen cada vez que se inicie el juego y comparar para comprobar si acierta 
     * 
     * 
     * 
     * 
     */
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.correct);
        bool allowClick = false;
        public Random random = new Random();
        public static int counter = 90;
        Timer clickTimer = new Timer();
        Timer timer = new Timer { Interval = 1000 };
        int n = 0;
        PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }
        PictureBox firstPick;


        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.img1,
                    Properties.Resources.img2,
                    Properties.Resources.img3,
                    Properties.Resources.img4,
                    Properties.Resources.img5,
                    Properties.Resources.img6,
                    Properties.Resources.img7,
                    Properties.Resources.img8,
                    Properties.Resources.img9,
                    Properties.Resources.img10,
                    Properties.Resources.img11,
                    Properties.Resources.img12,
                    Properties.Resources.img13,
                    Properties.Resources.img14,
                    Properties.Resources.img15,
                    Properties.Resources.img16,
                    Properties.Resources.img17,
                    Properties.Resources.img18,
                    Properties.Resources.img19,
                    Properties.Resources.img20,
                    Properties.Resources.img21,
                    Properties.Resources.img22,
                    Properties.Resources.img23,
                    Properties.Resources.img24

                };
            }
        }
        private void startTimer()
        {

            timer.Start();
            timer.Tick += delegate
            {
                counter--;


                if (counter <= 0)
                {
                    ResetImages();
                    labelGO.Image = Properties.Resources.gameover;
                    labelGO.Visible = true;
                    button1.Enabled = true;
                    customProgressBar1.Value = 0;
                    timer.Stop();
                    counter = 90;

                }
                TimeSpan ssTime = TimeSpan.FromSeconds(counter);
                label1.Text = "" + counter.ToString();
                customProgressBar1.PerformStep();
            };
        }


        private void ResetImages()
        {
            foreach (PictureBox pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }
        }

        private void HideImages()
        {
            foreach (PictureBox pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.cat;
            }

        }
        private PictureBox freeSlot()
        {
            int num;
            do
            {
                num = random.Next(0, pictureBoxes.Count());
            } while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }
        private void setPics()
        {
            foreach (var image in images)
            {
                freeSlot().Tag = image;
                freeSlot().Tag = image;

            }
        }
        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            HideImages();
            allowClick = true;
            clickTimer.Stop();
        }

        private void ClickImage(object sender, EventArgs e)
        {

            try
            {
                if (!allowClick) return;
                PictureBox pic = (PictureBox)sender;
                if (firstPick == null)
                {
                    firstPick = pic;
                    pic.Image = (Image)pic.Tag;
                    return;

                }
                pic.Image = (Image)pic.Tag;
                if (pic.Image == firstPick.Image && pic != firstPick)
                {
                    pic.Visible = firstPick.Visible = false;
                    {
                        firstPick = pic;
                    }
                    //pictureBoxPlus.Image = Properties.Resources.add1;
                    labelPlus.Visible = true;
                    counter += 2;
                    try
                    {
                        customProgressBar1.Value -= 2;
                        if (customProgressBar1.Value <= 0)
                        {
                            customProgressBar1.Value += 1;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {


                    }


                    player.Play();

                    HideImages();
                }
                else
                {
                    allowClick = false;
                    clickTimer.Start();
                }
                firstPick = null;
                if (pictureBoxes.Any(p => Visible)) return;
                {

                    ResetImages();

                }
                if (pictureBoxes.Any(p => !Visible))
                {
                    labelGO.Image = Properties.Resources.you;
                    labelGO.Visible = true;

                }



            }
            catch (Exception)
            {

                throw;
            }

        }

        public Form1()
        {
            InitializeComponent();
            dateTime();
            labelPlus.Visible = false;
            labelGO.Visible = false;

        }

        private void dateTime()
        {
            string hora = DateTime.Now.ToString("HH:mm:ss");
            labelday.Text = DateTime.Now.ToString("dd/MM/yyyy");
            labelhour.Text = hora;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            dateTime();



        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {




            n++;
            if (n % 2 == 0 && labelPlus.Visible == true)
            {
                labelPlus.Visible = false;

            }




            // Perform one step...



        }



        private void PictureBoxPlus_VisibleChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void startGame(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Enabled = true;
            clickTimer.Enabled = false;
            clickTimer.Enabled = true;
            allowClick = true;
            setPics();
            HideImages();
            startTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += ClickTimer_Tick;
            button1.Enabled = false;
        }
    }
}



