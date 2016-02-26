using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MathGames
{
    public partial class MathPuzzels : Form
    {
        private readonly List<MathProblem> _problems;
        private List<PictureBox> _pictureBoxes;
        private readonly List<string> _availablePictures = new List<string>();
        private MathProblem _currentMathProblem;
        private readonly Random rand = new Random();
        private int _failedAttempts;
        private int _correctAnswers;

        public MathPuzzels()
        {
            InitializeComponent();
            _problems = new List<MathProblem>();
            LoadAvailablePictures();
            LoadNextImage();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tbProblem.Text = GetNextProblem(GetProblemType());
            tbAnswer.Focus();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            double testAnswer;
            if (double.TryParse(tbAnswer.Text, out testAnswer))
            {
                if (testAnswer == _currentMathProblem.Answer)
                {
                    tbProblem.Text = GetNextProblem(GetProblemType());
                    _correctAnswers++;
                    HideTile();
                    tbAnswer.Clear();
                }
                else
                {
                    MessageBox.Show("Incorrect, please try again.");
                    _failedAttempts++;
                }

                lblScore.Text = "Score:" + Environment.NewLine +
                                "          Right: " + _correctAnswers + Environment.NewLine +
                                "          Wrong:" + _failedAttempts;
            }
            else
            {
                MessageBox.Show("Please try again.");
            }
            tbAnswer.SelectAll();
            tbAnswer.Focus();
        }

        private void FillPictureBoxList()
        {
            _pictureBoxes = new List<PictureBox>
            {
                pb11, pb12, pb13, pb14,
                pb21, pb22, pb23, pb24,
                pb31, pb32, pb33, pb34,
                pb41, pb42, pb43, pb44,
                pb51, pb52, pb53, pb54,
            };

            HidePuzzle();
        }

        private void HidePuzzle()
        {
            foreach (var control in Controls)
            {
                var pictureBox = control as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.Visible = true;
                }
            }
        }

        private void LoadNextImage()
        {
            FillPictureBoxList();
            if (_availablePictures.Count == 0)
            {
                LoadAvailablePictures();
            }

            int randInt = rand.Next(_availablePictures.Count);
            pbMain.ImageLocation = _availablePictures[randInt];
            _availablePictures[randInt] = _availablePictures[_availablePictures.Count - 1];
            _availablePictures.RemoveAt(_availablePictures.Count - 1);
        }

        private void LoadAvailablePictures()
        {
            foreach (var picture in Directory.GetFiles(@"Img"))
            {
                _availablePictures.Add(picture);
            }
        }

        private void HideTile()
        {
            if (_pictureBoxes.Count == 0)
            {
                LoadNextImage();
            }

            int rantInt = rand.Next(_pictureBoxes.Count);

            _pictureBoxes[rantInt].Visible = false;
            _pictureBoxes[rantInt] = _pictureBoxes[_pictureBoxes.Count - 1];

            _pictureBoxes.RemoveAt(_pictureBoxes.Count - 1);
        }

        private string GetNextProblem(ProblemType type)
        {
            switch (type)
            {
                case ProblemType.Addition:
                    _currentMathProblem = new AdditionProblem(GetDifficultyLevel());
                    _problems.Add(_currentMathProblem);
                    break;
                case ProblemType.Subtraction:
                    _currentMathProblem = new SubtractionProblem(GetDifficultyLevel());
                    _problems.Add(_currentMathProblem);
                    break;
                case ProblemType.AddSubtract:
                    if (rand.Next(4) >= 2)
                    {
                        _currentMathProblem = new AdditionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    else
                    {
                        _currentMathProblem = new SubtractionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);   
                    }
                    break;
                case ProblemType.Multiplication:
                    _currentMathProblem = new MultiplicationProblem(GetDifficultyLevel());
                    _problems.Add(_currentMathProblem);
                    break;
                case ProblemType.Division:
                    _currentMathProblem = new DivisionProblem(GetDifficultyLevel());
                    _problems.Add(_currentMathProblem);
                    break;
                case ProblemType.MultiplyDivide:
                    if (rand.Next(4) >= 2)
                    {
                        _currentMathProblem = new MultiplicationProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    else
                    {
                        _currentMathProblem = new DivisionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    break;
                case ProblemType.All:
                    int randInt = rand.Next(8);
                    if (randInt >= 6)
                    {
                        _currentMathProblem = new MultiplicationProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    else if (randInt >= 4)
                    {
                        _currentMathProblem = new DivisionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    else if (randInt >= 2)
                    {
                        _currentMathProblem = new AdditionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    else
                    {
                        _currentMathProblem = new SubtractionProblem(GetDifficultyLevel());
                        _problems.Add(_currentMathProblem);
                    }
                    break;
            }

            return "Problem Number " + _problems.Count + ": " + Environment.NewLine + 
                Environment.NewLine + _currentMathProblem;
        }

        private int GetDifficultyLevel()
        {
            if (rbLevel1.Checked)
                return 1;
            if (rbLevel2.Checked)
                return 2;
            if (rbLevel3.Checked)
                return 3;
            if (rbLevel4.Checked)
                return 4;
            if (rbLevel5.Checked)
                return 5;

            return 0;
        }

        private ProblemType GetProblemType()
        {
            if (rbAddition.Checked)
                return ProblemType.Addition;
            if (rbSubtraction.Checked)
                return ProblemType.Subtraction;
            if (rbPlusMinus.Checked)
                return ProblemType.AddSubtract;
            if (rbMultiplication.Checked)
                return ProblemType.Multiplication;
            if (rbDivision.Checked)
                return ProblemType.Division;
            if (rbMultiplyDivide.Checked)
                return ProblemType.MultiplyDivide;
            if (rbAll.Checked)
                return ProblemType.All;

            return ProblemType.None;
        }

        private enum ProblemType
        {
            None,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            AddSubtract,
            MultiplyDivide,
            All,
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 80; i++)
            {
                GetNextProblem(ProblemType.Addition);
            }
            LoadNextImage();
        }
    }
}
