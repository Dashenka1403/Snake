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
        private int dirX, dirY;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            Fruit = new PictureBox(); // создание фрукта
            Fruit.BackColor = Color.Red; //  цвет фрукта
            Fruit.Size = new Size(SizeCell, SizeCell); //  размер фрукта
            snake[0] = new PictureBox();// создание змейки
            snake[0].Location = new Point(201, 201); // размещение змейки
            snake[0].Size = new Size(SizeCell-1 , SizeCell-1); // размер змейки 
            snake[0].BackColor = Color.Green; // цвет змейки 
            this.Controls.Add(snake[0]); // добавление змейки 
            GenerateMap();
            GenerateFruit();
            labelScore = new Label(); // создание счётчика
            labelScore.Text = "Score: 0"; // название
            labelScore.Location = new Point(610, 10); // размещение счетчика
            this.Controls.Add(labelScore); // добавление счетчика
            timer.Tick += new EventHandler(Update);
            timer.Interval = 200; // интервал с каким двигается фрукт
            timer.Start();
            this.KeyDown += new KeyEventHandler(Movement);
            dirX = 1;
            dirY = 0;
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
        private void CheckBorders() // удаление хвоста, если натыкается на границы
        {
            if (snake[0].Location.X < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }
            if (snake[0].Location.X > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }
            if (snake[0].Location.Y > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }

        private void EatItself() // поедание змейки самой себя
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                        this.Controls.Remove(snake[_j]);
                    score = score - (score - _i + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }

        private void EatFruit() // поедание фрукта
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(SizeCell - 1, SizeCell - 1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                GenerateFruit();
            }
        }

        private void MoveSnake() // перемещение змейки
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (SizeCell), snake[0].Location.Y + dirY * (SizeCell));
            EatItself();
        }


        private void Update(Object myObject, EventArgs eventsArgs)
        {
            CheckBorders();
            EatFruit();
            MoveSnake();
        }

        private void Movement(object sender, KeyEventArgs e) // управление с клавиатуры 
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }
    }
}
