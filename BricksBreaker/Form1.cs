using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace BricksBreaker
{
    public partial class form1 : Form
    {
        Bricks []bricks = new Bricks[16];
        Bonus bonus = new Bonus();
        Slide slide = new Slide();
        Ball ball = new Ball();
        Settings settings = new Settings();
        int count = 0;
        public form1()
        {
            InitializeComponent();

            for (int i = 0; i < 16; i++)
            {
                bricks[i] = new Bricks();       //memory allocation line
            }

           // label2.Text = Settings.score.ToString();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics paint = e.Graphics;
            int soaob; //size_of_array_of_bricks
            int a = 0; //for bonus

            for (soaob = 0; soaob < 16; soaob++)
            {
                //condition of first brick of every row   
                if (((soaob == 0) || (soaob == 6) || (soaob == 10)) && (bricks[soaob].visiblity == true))
                {
                    if (soaob == 0) { bricks[soaob].brickXposition = 30; bricks[soaob].brickYposition = 50; }
                    if (soaob == 6) { bricks[soaob].brickXposition = 80; bricks[soaob].brickYposition = 90; }
                    if (soaob == 10) { bricks[soaob].brickXposition = 30; bricks[soaob].brickYposition = 130; }

                    if (soaob == bonus.BonusBrick)
                    {
                        bonus.BonusPositionX = bricks[soaob].brickXposition + (bricks[soaob].brickXposition / 2);
                        bonus.BonusPositionY = bricks[soaob].brickYposition;
                    }
                }
                //Condition of remaining bricks of row
                else if (((soaob != 0) || (soaob != 6) || (soaob != 10)) && (bricks[soaob].visiblity == true))
                {
                    bricks[soaob].brickXposition = bricks[soaob - 1].brickXposition + bricks[soaob].bricksGap;
                    if ((soaob > 0) && (soaob <= 5)) { bricks[soaob].brickYposition = 50; }
                    if ((soaob > 6) && (soaob <= 9)) { bricks[soaob].brickYposition = 90; }
                    if ((soaob > 10) && (soaob <= 15)) { bricks[soaob].brickYposition = 130; }
                    
                    if (soaob == bonus.BonusBrick)
                    {
                        bonus.BonusPositionX = bricks[soaob - 1].brickXposition + (bricks[soaob].brickXposition/2);
                        bonus.BonusPositionY = bricks[soaob].brickYposition;
                    }
                }
                if (bricks[soaob].visiblity == true)
                {
                    paint.FillRectangle(Brushes.Coral, bricks[soaob].brickXposition, bricks[soaob].brickYposition,
                        bricks[soaob].bricksizeX, bricks[soaob].bricksizeY);
                }

            }
           
            
            //************ Bonus Making **************
            if ((bricks[bonus.BonusBrick].visiblity == false) && (bonus.BonusVisibility == true))
            {
                e.Graphics.FillEllipse(Brushes.DarkMagenta, bonus.BonusPositionX, bonus.BonusPositionY,
                    bonus.BonusSizeX, bonus.BonusSizeY);
            }
                //********Slide Making*************
            paint.FillEllipse(Brushes.Cyan, slide.slideXPosition, slide.slideYPosition, slide.slideX, slide.slideY);
                //*******Ball Making****************
            paint.FillEllipse(Brushes.White, ball.ballXPosition, ball.ballYPosition, ball.ballX, ball.ballY);

        }//end of picture box scope


                                //******Game Timer********
        private void gametimer_Tick(object sender, EventArgs e)
        {
            //Collision Ball with Boundry Check
            CollisionWithBoundry();
            //Collision Ball with Bricks Check
            CollisionBallBrick();
            
            //Game timer interval
            gametimer.Interval = 5;
            
            
            // ********Bonus Position Update**********
            if ((bricks[bonus.BonusBrick].visiblity == false) && (bonus.BonusPositionY <= pictureBox.Size.Height) &&
                (bonus.BonusVisibility == true))
            {
                bonus.BonusPositionY += bonus.BonusMove;

                //Bonus collision with Slide
                if ((bonus.BonusPositionY + bonus.BonusSizeY >= slide.slideYPosition) && 
                (bonus.BonusPositionY <= slide.slideYPosition + slide.slideY) &&
                    (slide.slideXPosition <= bonus.BonusPositionX + bonus.BonusSizeX) &&
                    (slide.slideXPosition + slide.slideX >= bonus.BonusPositionX))
                {
                    Settings.score += Settings.BonusPoint;
                    label2.Text = Settings.score.ToString();
                    bonus.BonusVisibility = false;
                }
            }

            pictureBox.Invalidate();
        }




        //********Collision with Ball with Bricks Check************
        private void CollisionBallBrick()
        {
            for (int brickNumber = 0; brickNumber <= 15; brickNumber++)
            {
                //brick Bottom Side Collision Condition
                if ((ball.ballYPosition == bricks[brickNumber].brickYposition + bricks[brickNumber].bricksizeY) &&
                    (ball.ballXPosition + ball.ballX >= bricks[brickNumber].brickXposition) &&
                    (ball.ballXPosition <= bricks[brickNumber].brickXposition + bricks[brickNumber].bricksizeX) &&
                    (bricks[brickNumber].visiblity == true))
                {
                    if (((ball.PreviousSide == 1) && (ball.NextSide == 2)) ||((ball.PreviousSide == 0) && (ball.NextSide == 3)))
                    {
                        ball.PreviousSide = 2;
                        ball.NextSide = 3;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    if (((ball.PreviousSide == 3) && (ball.NextSide == 2)) || ((ball.PreviousSide == 0) && (ball.NextSide == 1)))
                    {
                        ball.PreviousSide = 2;
                        ball.NextSide = 1;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    break;
                }
                
                //brick Top Side Collision Condition
                if ((ball.ballYPosition + ball.ballY == bricks[brickNumber].brickYposition) &&
                    (ball.ballXPosition + ball.ballX >= bricks[brickNumber].brickXposition) &&
                    (ball.ballXPosition <= bricks[brickNumber].brickXposition + bricks[brickNumber].bricksizeX) &&
                    (bricks[brickNumber].visiblity == true))
                {
                    if (((ball.PreviousSide == 1) && (ball.NextSide == 0)) || ((ball.PreviousSide == 2) && (ball.NextSide == 3)))
                    {
                        ball.PreviousSide = 1;
                        ball.NextSide = 2;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    if (((ball.PreviousSide == 2) && (ball.NextSide == 1)) || ((ball.PreviousSide == 3) && (ball.NextSide == 0)))
                    {
                        ball.PreviousSide = 3;
                        ball.NextSide = 2;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    break;
                }
                
                //brick Left Side Collision Condition
                if ((ball.ballXPosition + ball.ballX == bricks[brickNumber].brickXposition) &&
                    (ball.ballYPosition + ball.ballY >= bricks[brickNumber].brickYposition) &&
                    (ball.ballYPosition <= bricks[brickNumber].brickYposition + bricks[brickNumber].bricksizeY) &&
                    (bricks[brickNumber].visiblity == true))
                {
                    if (((ball.PreviousSide == 1) && (ball.NextSide == 0)) || ((ball.PreviousSide == 2) && (ball.NextSide == 3)))
                    {
                        ball.PreviousSide = 2;
                        ball.NextSide = 1;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    if (((ball.PreviousSide == 1) && (ball.NextSide == 2)) || ((ball.PreviousSide == 0) && (ball.NextSide == 3)))
                    {
                        ball.PreviousSide = 0;
                        ball.NextSide = 1;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    break;
                }
                
                //brick Right Side Collision Condition
                if ((ball.ballXPosition == bricks[brickNumber].brickXposition + bricks[brickNumber].bricksizeX) &&
                    (ball.ballYPosition + ball.ballY >= bricks[brickNumber].brickYposition) &&
                    (ball.ballYPosition <= bricks[brickNumber].brickYposition + bricks[brickNumber].bricksizeY) &&
                    (bricks[brickNumber].visiblity == true))
                {
                    if (((ball.PreviousSide == 3) && (ball.NextSide == 0)) || ((ball.PreviousSide == 2) && (ball.NextSide == 1)))
                    {
                        ball.PreviousSide = 2;
                        ball.NextSide = 3;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    if (((ball.PreviousSide == 1) && (ball.NextSide == 2)) || ((ball.PreviousSide == 3) && (ball.NextSide == 2)))
                    {
                        ball.PreviousSide = 1;
                        ball.NextSide = 3;
                        ScoreUpdateAndVisibility(brickNumber);
                        break;
                    }
                    break;
                }
            }
        }

        //*****Update Screen*********
        private void ScoreUpdateAndVisibility(int brickNum)
        {
            count += 1;
            bricks[brickNum].visiblity = false;
            pictureBox.Refresh();
            Settings.score += Settings.point;
            label2.Text = Convert.ToString(Settings.score);
            if (count == 16)
            {
                GameWin();
            }
        }



        //********collision with Boundry Check************
        private void CollisionWithBoundry()
        {
            //All Collision with boundry of Bottom
            if ((ball.PreviousSide == 3) && (ball.NextSide == 0))
            {
                MoveRightToBottom(Settings.angle);
            }
            if ((ball.PreviousSide == 1) && (ball.NextSide == 0))
            {
                MoveLeftToBottom(Settings.angle);
            }
            if ((ball.PreviousSide == 2) && (ball.NextSide == 0))
            {
                MoveTopToBottom(Settings.angle);
            }
            
            //All Collision with boundry of Left
            if ((ball.PreviousSide == 0) && (ball.NextSide == 1))
            {
                MoveBottomToLeft(Settings.angle);
            }
            if ((ball.PreviousSide == 2) && (ball.NextSide == 1))
            {
                MoveTopToLeft(Settings.angle);
            }

            //All Collision with boundry of Top
            if ((ball.PreviousSide == 1) && (ball.NextSide == 2))
            {
                MoveLeftToTop(Settings.angle);
            }
            if ((ball.PreviousSide == 3) && (ball.NextSide == 2))
            {
                MoveRightToTop(Settings.angle);
            }

            //All Collision with boundry of Right
            if ((ball.PreviousSide == 0) && (ball.NextSide == 3))
            {
                MoveBottomToRight(Settings.angle);
            }
            if ((ball.PreviousSide == 2) && (ball.NextSide == 3))
            {
                MoveTopToRight(Settings.angle);
            }


            //top Left Corner
            if ((ball.ballXPosition == -10) && (ball.ballYPosition == -10))
            {
                ball.PreviousSide = 2;
                ball.NextSide = 3;
            }

            //top Right Corner
            if ((ball.ballXPosition + ball.ballX >= pictureBox.Size.Width) && (ball.ballYPosition <= -10))
            {
                ball.PreviousSide = 2;
                ball.NextSide = 1;
            }

            //Bottom Left Corner
            if ((ball.ballXPosition <= -10) && (ball.ballYPosition + ball.ballY >= 380))
            {
                CollisionWithSlideCheck();
            }
            
            //Bottom Right Corner
            if ((ball.ballXPosition + ball.ballX >= pictureBox.Size.Width) && (ball.ballYPosition + ball.ballY >= 380))
            {
                CollisionWithSlideCheck();
            }

            // Other Side Collision Check
            if ((ball.ballYPosition == 370) && (ball.ballXPosition >= -10) && (ball.ballXPosition <= pictureBox.Size.Width))
            {
                CollisionWithSlideCheck();
            }

            //Game Over Option
            if (ball.ballYPosition > 380)
            {
                Settings.GameOver = false;
            }   
        }
        


        //******Ball Movement Check********
        private void MoveBottomToLeft(int angle)
        {
            ball.ballXPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            //Move left to Top Condition
            if ((ball.ballXPosition <= -5) && (ball.ballYPosition > 0) && (ball.ballYPosition < 380))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 2;
            }
        }
        private void MoveLeftToTop(int angle)
        {
            ball.ballXPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //Top to Right Condition
            if ((ball.ballXPosition > 0) && (ball.ballXPosition + ball.ballX < pictureBox.Size.Width) &&
                (ball.ballYPosition <= -5))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 3;
            }
            //Left To Right Condition
            if ((ball.ballXPosition + ball.ballX == pictureBox.Width) && (ball.PreviousSide == 1) && (ball.NextSide == 2))
            {
                ball.PreviousSide = 3;
                ball.NextSide = 2;
            }
            
        }
        private void MoveTopToRight(int angle)
        {
            ball.ballXPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //Right to Bottom Condition
            if ((ball.ballXPosition + ball.ballX == pictureBox.Size.Width) && (ball.ballYPosition > -10))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 0;
            }
            //if ball hit the slide area 
            if (ball.ballYPosition + ball.ballY >= 380)
            {
                CollisionWithSlideCheck();
            }

        }
        private void MoveRightToBottom(int angle)
        {
            ball.ballXPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //Right to Left Condition
            if ((ball.ballXPosition <= -5) && (ball.PreviousSide == 3) && (ball.NextSide == 0))
            {
                ball.PreviousSide = 1;
                ball.NextSide = 0;
            }
            if (ball.ballYPosition >= 380)
            {
                CollisionWithSlideCheck();
            }
        }

        private void MoveLeftToBottom(int angle)
        {
            ball.ballXPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //Left To Right Condition
            if ((ball.ballXPosition >= pictureBox.Width) && (ball.PreviousSide == 1) && (ball.NextSide == 0))
            {
                ball.PreviousSide = 3;
                ball.NextSide = 0;
            }
            
            //Collision With Slide And Ball
            if (ball.ballYPosition >= 380) 
            {
                CollisionWithSlideCheck();
            }
        }

        private void MoveTopToLeft(int angle)
        {
            ball.ballXPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            if ((ball.ballXPosition <= 0) && (ball.ballYPosition > -10) && (ball.ballYPosition < 380))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 0;
            }
        }

        private void MoveBottomToRight(int angle)
        {
            ball.ballXPosition += gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //right to top condition
            if ((ball.ballXPosition + ball.ballX == pictureBox.Size.Width) && (ball.ballYPosition > 0) &&
                (ball.ballYPosition + ball.ballY < 370))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 2;
            }
        }

        private void MoveRightToTop(int angle)
        {
            ball.ballXPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));
            ball.ballYPosition -= gametimer.Interval * Convert.ToInt32(Math.Sin(angle));

            CollisionBallBrick();
            //Top to Left Condition
            if ((ball.ballXPosition  > -10) && (ball.ballXPosition + ball.ballX < pictureBox.Size.Width) &&
                (ball.ballYPosition <= 0))
            {
                ball.PreviousSide = ball.NextSide;
                ball.NextSide = 1;
            }
            //ball move right left
            if ((ball.ballXPosition <= 0) && (ball.PreviousSide == 3) && (ball.NextSide == 2))
            {
                ball.PreviousSide = 1;
                ball.NextSide = 2;
            }
        }

        private void MoveTopToBottom(int angle)
        {
            if (ball.ballYPosition >= 370)
            {
                CollisionWithSlideCheck();
            }
        }


       
        //******Detection With slide Check********
        private void CollisionWithSlideCheck()
        {
            //if ball hit on the slide 
            if ((ball.ballXPosition + ball.ballX >= slide.slideXPosition) && (ball.ballXPosition <= slide.slideXPosition + slide.slideX) &&
                (ball.ballYPosition + ball.ballY >= slide.slideYPosition) &&
                (ball.ballYPosition + ball.ballY <= slide.slideYPosition + slide.slideY))
            {
                if ((ball.PreviousSide == 3) && (ball.NextSide == 0))
                {
                    ball.PreviousSide = ball.NextSide;
                    ball.NextSide = 1;
                }
                if ((ball.PreviousSide == 2) && (ball.NextSide == 1))
                {
                    ball.PreviousSide = 0;
                    ball.NextSide = 1;
                }
                if ((ball.PreviousSide == 2) && (ball.NextSide == 3))
                {
                    ball.PreviousSide = 0;
                    ball.NextSide = 3;
                }
                if ((ball.PreviousSide == 1) && (ball.NextSide == 0))
                {
                    ball.PreviousSide = ball.NextSide;
                    ball.NextSide = 3;
                }
               
                //Bottom left corner 
                if ((ball.ballXPosition == -5) && (ball.ballYPosition >= 380))
                {
                    ball.NextSide = 2;
                    ball.PreviousSide = 1;
                }
                //Bottom right corner
                if ((ball.ballXPosition + ball.ballX >= pictureBox.Size.Width) && (ball.ballYPosition >= 380))
                {
                    ball.NextSide = 1;
                    ball.PreviousSide = 0;
                }
            }
            else if (ball.ballYPosition >= 400)
            {
                Settings.GameOver = true;
                GameOver();
            }
        }



        //******Button Check of slide Movement********
        private void leftbutton_Click(object sender, EventArgs e)
        {
            if (slide.slideXPosition >= 10)
            {
                slide.slideXPosition -= Settings.slide_speed;
            }
        }

        private void rightbutton_Click(object sender, EventArgs e)
        {
            if (slide.slideXPosition <= 280)
            {
                slide.slideXPosition += Settings.slide_speed;
            }
        }



        //**********Game Over Option*********
        private void GameOver()
        {
            gametimer.Stop();
            MessageBox.Show("Your Score is:  " + label2.Text, "Game Over", MessageBoxButtons.OK);
        }

        //*********Game Win Method********
        private void GameWin()
        {
            Settings.GameOver = true;
            gametimer.Stop();
            MessageBox.Show("Your Score is:  " + label2.Text, "You Win", MessageBoxButtons.OK);

        }
    }
}
