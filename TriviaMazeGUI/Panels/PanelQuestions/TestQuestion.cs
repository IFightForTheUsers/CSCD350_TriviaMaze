using System;
using System.Windows.Controls;

namespace TriviaMazeGUI.Panels.PanelQuestions
{
    [Serializable]
    class TestQuestion : PanelQuestion
    {
        protected override void Ask()
        {
            // stub to fill in with a question prompt
            StackPanel sp = new StackPanel();
            RadioButton lockDoor = new RadioButton { Name = "lockDoor", Content = "Lock Door" };
            RadioButton passDoor = new RadioButton
            {
                Name = "passDoor",
                Content = "pass thru Door"
            };
            Button submit = new Button
            {
                Name = "Submit",
                Content = "Submit"
            };
            submit.Click += (s, e) =>
            {
                if (lockDoor.IsChecked == true || passDoor.IsChecked == true)
                {
                    if (lockDoor.IsChecked == true)
                    {
                        this.locked = true;
                    }
                    else
                    {
                    }

                    UILock.Instance.Free();
                }
            };
            sp.Children.Add(lockDoor);
            sp.Children.Add(passDoor);
            sp.Children.Add(submit);

            MainWindow.Instance.Question.Children.Add(sp);
            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);
        }

        public TestQuestion(Panel wrapping) : base(wrapping)
        {
        }
    }
}
