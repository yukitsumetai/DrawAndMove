using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        
        enum flags { bInitial, bCurve, bPolygone, bBeizers, bFilled, bClear };
        flags choice;
        bool mClick, bDrag;
        int iDraggingPoint = 0;
        public int radius = 2;
        static int speed=2;
        float lines = 1;

        public Pen myPen;
        public Brush myBrush;
        public bool chaotMovement=false;
        


        List<Point> points = new List<Point>();
        List<Point> speeds = new List<Point>();
        Button bt1, bt4, bt5, bt6, bt7, bt8, bt9, bt10;
        

    Timer t = new Timer();

        public Form1()
        {
            myPen = new Pen(Color.Red, lines);
            myBrush = Brushes.Yellow;
            this.Size = new Size(800,500);
            this.Text = "Движение";
            InitializeComponent();

            #region КНОПКИ
            bt1 = new Button()
            {
                Text = "Точки",
                Size = new Size(90, 30),
                Location = new Point(10, 10),
              
        };

            

            this.Controls.Add(bt1);
            bt1.Click += ButtonClick;

            bt4 = new Button()
            {
                Text = "Параметры",
                Left = bt1.Left,
                Top = bt1.Top + 40,
                Size = bt1.Size
            };

            this.Controls.Add(bt4);
            bt4.Click += ButtonClick4;

            bt5 = new Button()
            {
                Text = "Кривая",
                Left = bt1.Left,
                Top = bt4.Top + 40,
                Size = bt1.Size
            };

            
            this.Controls.Add(bt5);
            bt5.Click += ButtonClick5;

            bt6 = new Button()
            {
                Text = "Ломаная",
                Left = bt1.Left,
                Top = bt5.Top + 40,
                Size = bt1.Size
            };

            this.Controls.Add(bt6);
            bt6.Click += ButtonClick6;

            bt7 = new Button()
            {
                Text = "Безьеры",
                Left = bt1.Left,
                Top = bt6.Top + 40,
                Size = bt1.Size
            };

            this.Controls.Add(bt7);
            bt7.Click += ButtonClick7;

            bt8 = new Button()
            {
                Text = "Закрашенная",
                Left = bt1.Left,
                Top = bt7.Top + 40,
                Size = bt1.Size
            };

            this.Controls.Add(bt8);
            bt8.Click += ButtonClick8;

            bt9 = new Button()
            {
                Text = "Движение",
                Left = bt1.Left,
                Top = bt8.Top + 40,
                Size = bt1.Size
            };

            this.Controls.Add(bt9);
            bt9.Click += ButtonClick9;

           bt10 = new Button()
            {
                Text = "Отчистить",
                Left = bt1.Left,
                Top = bt9.Top + 40,
                Size = bt1.Size
            };


     

            Refresh();


            this.Controls.Add(bt10);
            bt10.Click += ButtonClick10;
            #endregion

            t.Interval = 30;
            t.Tick += TickHandler;
            

            MouseClick += (o, e) =>
            {
                if (mClick)
                {
                    Point p = e.Location;
                    points.Add(p);
                    Random r = new Random();
                    speeds.Add(new Point(r.Next(-3, 3), r.Next(-3, 3)));
                    if (choice == flags.bBeizers) { choice = flags.bInitial;
                        bt7.BackColor = Color.Empty;
                    }


                            Refresh();
                }
            };
            MouseDown += Form1_MouseDown;

            void Form1_MouseDown(object sender, MouseEventArgs e)
            { if (e.Button==MouseButtons.Left)

                { for (int i = 0; i < points.Count; i++)
                    {
                        Point p = points[i];

                        // Сравниваем положение курсора мышки с
                        // координатами точки p (пользовательский метод)
                        if (IsOnPoint(e.Location, p))
                        {
                            // Сохраняем индекс перемещаемой точки
                            iDraggingPoint = i;
                            // Устанавливаем флаг перемещения точки
                            bDrag = true;
                        }
                    }
                } };

                void Form1_MouseMove(object sender, MouseEventArgs e)
                {
                    // Проверяем флаг перемещения
                    if (bDrag)
                    {
                        // Изменяем координаты выбранной точки
                        points[iDraggingPoint] = new Point(e.Location.X, e.Location.Y);
                        Refresh();
                    }
                };

            MouseMove += Form1_MouseMove;

            void Form1_MouseUp(object sender, MouseEventArgs e)
            {
                // Сбрасываем флаг перемещения
                bDrag = false;
            }

            MouseUp += Form1_MouseUp;

            bool IsOnPoint(Point a, Point e) {

                return (a.X<= e.X+20 && a.X >= e.X-20 && a.Y <= e.Y+20 && a.Y >= e.Y - 20);
            }

                Paint += Form1_Paint;

             KeyPreview = true;
            KeyDown += Form1_KeyDown;

            DoubleBuffered = true;
            //Mouse
        }

        #region Movement

        public void TickHandler(object sender, EventArgs e)
        {
            if (chaotMovement) movePointsChaotic();
            else movePoints();
        }

        int maxX() {
            int maxX = points[0].X;
            int maxXi = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (maxX < points[i].X) { maxX = points[i].X; maxXi = i; }
               
            }
            return maxXi;
        }

        int minX()
        {
          
            int minX = points[0].X;
            int minXi = 0;
            for (int i = 0; i < points.Count; i++)
            {
              
                if (minX > points[i].X) { minX = points[i].X; minXi = i; }
            }
            return minXi;
        }
        int maxY()
        {
            int maxY = points[0].Y;
            int maxYi = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (maxY < points[i].Y) { maxY = points[i].Y; maxYi = i; }

            }
            return maxYi;
        }

        int minY()
        {

            int minY = points[0].Y;
            int minYi = 0;
            for (int i = 0; i < points.Count; i++)
            {

                if (minY > points[i].Y) { minY = points[i].Y; minYi = i; }
            }
            return minYi;
        }

        void movePoints()
        {
           
                if (speed >= 0)
                { if (points[maxX()].X >= (this.Width - 25)) speed=-speed; }
                else { if (points[minX()].X <= 0) speed = -speed; }

            


                for (int i = 0; i < points.Count; i++)
                {

                    points[i] = new Point()
                    {
                        X = points[i].X + speed,
                        Y = points[i].Y
                    };
                }
            Refresh();
 
        }

        void movePointsChaotic()
        {
          
            Boolean[,] speedFlags = new Boolean[points.Count(),2];
            for (int i = 0; i < points.Count; i++)
            {
                
                if (speeds[i].X >= 0)
                { if (points[i].X >= (this.Width-25)) speeds[i] = new Point(-speeds[i].X, speeds[i].Y); }
                else { if (points[i].X <=0) speeds[i] = new Point(-speeds[i].X, speeds[i].Y); }

                if (speeds[i].Y >= 0)
                { if (points[i].Y >= (this.Height-40)) speeds[i] = new Point(speeds[i].X, -speeds[i].Y); }
                else { if (points[i].Y<=0) speeds[i] = new Point(speeds[i].X, -speeds[i].Y); }
            }
                      
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = new Point()
                    {
                       X = points[i].X + speeds[i].X,
                       Y= points[i].Y + speeds[i].Y
                     };

                }
            Refresh();     
        }

        void movementIncremental(int X, int Y, int X2, int Y2) {
            
            for (int i = 0; i < points.Count; i++)
            {
                int tX = points[i].X, tY = points[i].Y;
                if (points[maxX()].X <= (this.Width-20) && X != 0)
                {
                        tX = points[i].X + X;
                }
                if (points[minX()].X >= 0 && X2 != 0)
                {
                        tX = points[i].X - X2;
                }
                if (points[maxY()].Y <= (this.Height-40) && Y != 0)
                {
                        tY = points[i].Y + Y;
                }
                if (points[minY()].Y >= 0 && Y2 != 0)
                {
                        tY = points[i].Y - Y2;
                }

                points[i] = new Point()
                {
                    X =  tX,
                    Y = tY
                };
            }
            Refresh();
        }
        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var p in points)
            {
                e.Graphics.DrawEllipse(myPen, p.X - radius, p.Y - radius, radius*2, radius * 2);
            }

            switch (choice)
            {
                case flags.bPolygone:
                    if (points.Count > 1) e.Graphics.DrawPolygon(myPen, points.ToArray()); break;
                    
                case flags.bCurve:
                    if (points.Count > 2) e.Graphics.DrawClosedCurve(myPen, points.ToArray());  break;
                case flags.bBeizers: e.Graphics.DrawBeziers(myPen, points.ToArray()); break;

                case flags.bFilled:
                    if (points.Count > 2) e.Graphics.FillClosedCurve(myBrush, points.ToArray()); break;

                default: break;
            }

        }

   
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
      if (t.Enabled==false && points.Count>0)
            {
                if (keyData == Keys.Left)
                {
                    movementIncremental(0, 0, 2, 0);
                    Refresh();
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    movementIncremental(2, 0, 0, 0);
                    Refresh();
                    return true;
                }
                if (keyData == Keys.Up)
                {
                    movementIncremental(0, 0, 0, 2);
                    Refresh();
                    return true;
                }
                if (keyData == Keys.Down)
                {
                    movementIncremental(0, 2, 0, 0);
                    Refresh();
                    return true;
                }
            }

            if (t.Enabled == true)
            {
                if (keyData == Keys.Left || keyData == Keys.Down)
                {
                    SpeedDecrease();
                    return true;
                }
                if (keyData == Keys.Right || keyData == Keys.Up)
                {
                    SpeedIncrease();
                    return true;
                }
              
            }
            return base.ProcessCmdKey(ref msg, keyData);
  }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonClick10(bt10, e);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                ButtonClick9(bt9, e);
                e.Handled=true;
            }
            if (t.Enabled)
            {
                if (e.KeyCode == Keys.Add)
                {
                    SpeedIncrease();
                }
                if (e.KeyCode == Keys.Subtract)
                {
                    SpeedDecrease();
                }
            }
    
        }

        void SpeedIncrease() {
            if (speed > 0) if (speed < 10) speed++;
                else if (speed < -10) speed--;
        }

        void SpeedDecrease()
        {
            if (speed > 1) speed--;
            else if (speed < -1) speed++;
        }


        #region Buttons functions


        public void ButtonClick(object sender, EventArgs e)
        {
            if (bt1.BackColor != Color.Green)
            {
                bt1.BackColor = Color.Green;
                bt5.BackColor = Color.Empty;
                bt7.BackColor = Color.Empty;
                bt6.BackColor = Color.Empty;
                bt8.BackColor = Color.Empty;
            }


            else{ bt1.BackColor = Color.Empty;
            bt5.BackColor = Color.Empty;
            bt7.BackColor = Color.Empty;
            bt6.BackColor = Color.Empty;
            bt8.BackColor = Color.Empty;
        }
        mClick = !mClick;
            choice = flags.bInitial;
            Refresh();
        }

        public void ButtonClick4(object sender, EventArgs e)
        {
           Form2 f = new Form2();
            f.Show(this);
            Refresh();

        }

    public void ButtonClick5(object sender, EventArgs e)
        {
            if (bt5.BackColor != Color.Green){ bt5.BackColor = Color.Green;
            bt7.BackColor = Color.Empty;
            bt6.BackColor = Color.Empty;
            bt8.BackColor = Color.Empty;
        }
            else bt5.BackColor = Color.Empty;
         if (choice != flags.bCurve)   choice = flags.bCurve;
            else choice = flags.bInitial;

            Refresh();
        }
        public void ButtonClick6(object sender, EventArgs e)
        {
            if (bt6.BackColor != Color.Green){ bt6.BackColor = Color.Green;
            bt7.BackColor = Color.Empty;
            bt8.BackColor = Color.Empty;
            bt5.BackColor = Color.Empty;
        }
            else bt6.BackColor = Color.Empty;
           if (choice != flags.bPolygone) choice = flags.bPolygone;
             else choice = flags.bInitial;
            Refresh();
        }

        public void ButtonClick7(object sender, EventArgs e)
        {
            if (points.Count == 4)
            {
                if (bt7.BackColor != Color.Green)
                { bt7.BackColor = Color.Green;

                    bt8.BackColor = Color.Empty;
                    bt6.BackColor = Color.Empty;
                    bt5.BackColor = Color.Empty;
                }
                else bt7.BackColor = Color.Empty;

                if (choice != flags.bBeizers) choice = flags.bBeizers;
                else choice = flags.bInitial;
                Refresh();
               
            }
        }

        public void ButtonClick8(object sender, EventArgs e)
        {
            if (bt8.BackColor != Color.Green) { bt8.BackColor = Color.Green;
                bt7.BackColor = Color.Empty;
                bt6.BackColor = Color.Empty;
                bt5.BackColor = Color.Empty;
            }
            else bt8.BackColor = Color.Empty;
           if (choice != flags.bFilled) choice = flags.bFilled;
             else choice = flags.bInitial;
            Refresh();
        }

        public void ButtonClick9(object sender, EventArgs e)
        { if (points.Count > 0)
            {
                if (bt9.BackColor != Color.Green) bt9.BackColor = Color.Green;
                else bt9.BackColor = Color.Empty;
                t.Enabled = !t.Enabled;
                Refresh();
            }
        }

        public void ButtonClick10(object sender, EventArgs e)
        {
            
            choice = flags.bClear;
            t.Enabled = false;
            points.Clear();
            speeds.Clear();

            bt5.BackColor = Color.Empty;
            bt7.BackColor = Color.Empty;
            bt6.BackColor = Color.Empty;
            bt8.BackColor = Color.Empty;
            bt9.BackColor = Color.Empty;

            Refresh();
        }
#endregion
    }
}