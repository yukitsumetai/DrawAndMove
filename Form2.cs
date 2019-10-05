using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{

    //толщина линий, размер точек, цвет точек 

    public partial class Form2 : Form
    {
        Label l1, l2, l3, l4;
    ListBox  b4;
    RadioButton b3a, b3b;
    NumericUpDown b1, b2;
        Button b5;


        public Form2()
        {
            this.Size = new Size(400, 200);
            InitializeComponent();
            this.Text = "Параметры";


            b1 = new NumericUpDown()
            {
                Value = 2,
                Maximum = 5,
                Minimum = 1,
                Size = new Size(80, 50),
                Location = new Point(10, 25),

            };

            this.Controls.Add(b1);


            b2 = new NumericUpDown()
            {
                Value =2,
                Maximum=5,
                Minimum=1,
                Left = b1.Left+90,
                Top = b1.Top,
                Size = b1.Size
            };

            this.Controls.Add(b2);


         

            b3a = new RadioButton()
            {
                Text = "Хаотичное",
                Left = b2.Left + 90,
                Top = b1.Top,
                Size = b1.Size
            };

            b3b = new RadioButton()
            {
                Text = "В сторону",
                Left = b3a.Left,
                Top = b3a.Top+40,
                Size = b1.Size
            };

            

            this.Controls.Add(b3a);
            this.Controls.Add(b3b);
            b3a.Checked = true;


            b4 = new ListBox()
            {
                Text = "...",
                Left = b3a.Left+90,
                Top = b1.Top,
                Size = new Size(80, 80)
               
            };
            b4.Items.AddRange(new string[] { "Green", "Yellow", "Red", "Blue", "Black" });

          

            b4.SelectedItem = "Green";

            this.Controls.Add(b4);

            b5 = new Button()
            {
                Text = "Сохранить",
                Left = b4.Left,
                Top = b2.Top+80,
                Size = new Size(80, 30)
            };

            this.Controls.Add(b5);
            b5.Click += ButtonClick;

            l4 = new Label()
            {
                Text = "Цвет:",
                Location = new Point(275, 5),
            };
            Controls.Add(l4);

            l3 = new Label()
            {
                Text = "Движение:",
                Location = new Point(185, 5),
            };
            Controls.Add(l3);

            l2 = new Label()
            {
                Text = "Размер Точки:",
                Location = new Point(95, 5),
            };
            Controls.Add(l2);

            l1 = new Label()
            {
                Text = "Толщина линий:",
                Location = new Point(5, 5),
                BackColor = Color.Empty
            };
            Controls.Add(l1);

        }

        public void ButtonClick(object sender, EventArgs e)
        {
            (this.Owner as Form1).myPen.Width = (float)b1.Value;

            (this.Owner as Form1).radius = (int)b2.Value;

            if (b3a.Checked == true)
            {
                (this.Owner as Form1).chaotMovement = true;

            }
            else
            {
                (this.Owner as Form1).chaotMovement = false;

            }

            switch (b4.SelectedItem.ToString())
            {
                case "Green": (this.Owner as Form1).myPen.Color = Color.Green; (this.Owner as Form1).myBrush = Brushes.Green; break;
                case "Yellow": (this.Owner as Form1).myPen.Color = Color.Yellow; (this.Owner as Form1).myBrush = Brushes.Yellow; break;
                case "Red": (this.Owner as Form1).myPen.Color = Color.Red; (this.Owner as Form1).myBrush = Brushes.Red; break;
                case "Blue": (this.Owner as Form1).myPen.Color = Color.Blue; (this.Owner as Form1).myBrush = Brushes.Blue; break;
                case "Black": (this.Owner as Form1).myPen.Color = Color.Black; (this.Owner as Form1).myBrush = Brushes.Black; break;
            }
            (this.Owner as Form1).Refresh();
            Close();
            
        }
    }
    }

