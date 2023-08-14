using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random randomizer = new Random();

        int addend1, addend2;
        int minuend, subtrahend;
        int multiplicand, multiplier;
        int dividend, divisor;

        int timeLeft;

        private void answer_Enter(object sender, EventArgs e)
        {   
            // The 'sender' parameter refers to the object whose event is firing
            
            // Casts or converts thse 'sender' from a generic object to a NumericUpDown control  
            NumericUpDown answerBox = sender as NumericUpDown;

            // Verifies wheter answerBox was successfuly cast as a NumericUpDown control
            if (answerBox != null)
            {   
                // Determines the lenght of the answer that's currently in the numericUpDown
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                // Uses the answer lenght to select the current value in control
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void hintSound_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;

            if (numericUpDown != null)
            {
                int value = (int)numericUpDown.Value;

                int resultSum = addend1 + addend2;
                int resultDifference = minuend - subtrahend;
                int resultProduct = multiplicand * multiplier;
                int resultQuotient = dividend / divisor;

                if (value == resultSum || 
                    value == resultDifference ||
                    value == resultProduct ||
                    value == resultQuotient)
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        public void StartTheQuiz()
        {   
            // Fill in the addition problem
            // Generate two random numbers
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the generated numbers into strings to be displayed in the labels controls
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
             
            // Makes sure the 'sum' NumericUpDown's value starts at zero
            sum.Value = 0;

            // Fill in the subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = timeLeft.ToString() + " seconds";
            timer1.Start();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startBtn.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            // Verify if the answers are correct
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer()) 
            { 
                // If CheckTheAnswer() return true, then the user got all the answer right
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startBtn.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer returns false and there's still time, keep counting down.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";

                // Changes the color of the time label
                if (timeLeft == 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else 
            {
                // If the user ran out of time, stop the timer and show a MessageBox, and fill the answers
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");

                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                timeLabel.BackColor = SystemColors.Control;
                startBtn.Enabled = true;
            }
        }
    }
}
