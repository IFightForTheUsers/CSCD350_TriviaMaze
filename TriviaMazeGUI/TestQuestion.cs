using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TriviaMazeGUI
{
    class TestQuestion : PanelQuestion
    {
        public override Room knock(Room from)
        {
            // stub to fill in with a question prompt
            StackPanel sp = new StackPanel();
            RadioButton lockDoor = new RadioButton();
            lockDoor.Name = "lockDoor";
            lockDoor.Content = "Lock Door";
            RadioButton passDoor = new RadioButton();
            passDoor.Name = "passDoor";
            passDoor.Content = "pass thru Door";
            Button submit = new Button();
            submit.Name = "Submit";
            submit.Content = "Submit";
            submit.Click += (s, e) =>
            {
                if (lockDoor.IsChecked == true)
                {
                    this.locked = true;
                }
                else
                { }
            };
            sp.Children.Add(lockDoor);
            sp.Children.Add(passDoor);
            sp.Children.Add(submit);

            MainWindow.Instance.Question.Children.Add(sp);
            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);

            // need to wait for click here somehow

            //MainWindow.Instance.Question.Children.Clear();

            return base.knock(from);
        }

        public TestQuestion(Panel wrapping) : base(wrapping)
        {
        }
    }
}
