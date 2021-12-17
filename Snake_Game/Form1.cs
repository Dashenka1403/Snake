using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private int width = 700;
        private int height = 600;
        private int SizeCell = 40;
        private PictureBox Fruit; // фрук
        private PictureBox[] snake = new PictureBox[400]; // змейка
        private Label labelScore;

        public Form1()
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            Fruit = new PictureBox(); // создание фрукта
            Fruit.BackColor = Color.Red; //  цвет фрукта
            Fruit.Size = new Size(SizeCell, SizeCell); //  размер фрукта
            snake[0] = new PictureBox();// создание змейки
            snake[0].Location = new Point(200, 200); // размещение змейки
            snake[0].Size = new Size(SizeCell , SizeCell); // размер змейки 
            snake[0].BackColor = Color.Green; // цвет змейки 
            this.Controls.Add(snake[0]); // добавление змейки 
            GenerateMap();
            GenerateFruit();
            labelScore = new Label(); // создание счётчика
            labelScore.Text = "Score: 0"; // название
            labelScore.Location = new Point(610, 10); // размещение счетчика
            this.Controls.Add(labelScore); // добавление счетчика
        }
        private void GenerateMap() // создавание поля
        {
            for (int i = 0; i < width / SizeCell; i++) // горизонтальные линии
            {
                PictureBox HorizontalLine = new PictureBox();
                HorizontalLine.BackColor = Color.Black;
                HorizontalLine.Location = new Point(0, SizeCell * i);
                HorizontalLine.Size = new Size(width - 100, 1);
                this.Controls.Add(HorizontalLine);
            }
            for (int i = 0; i <= height / SizeCell; i++) // вертикальные линии
            {
                PictureBox VerticalLine = new PictureBox();
                VerticalLine.BackColor = Color.Black;
                VerticalLine.Location = new Point(SizeCell * i, 0);
                VerticalLine.Size = new Size(1, width);
                this.Controls.Add(VerticalLine);
            }
        }

        private void GenerateFruit() // размещение фрукта
        {
            Random r = new Random();
            rI = r.Next(0, height - SizeCell);
            int tempI = rI % SizeCell;
            rI -= tempI;
            rJ = r.Next(0, height - SizeCell);
            int tempJ = rJ % SizeCell;
            rJ -= tempJ;
            rI++;
            rJ++;
            Fruit.Location = new Point(rI, rJ);
            this.Controls.Add(Fruit);
        }
    }
}
