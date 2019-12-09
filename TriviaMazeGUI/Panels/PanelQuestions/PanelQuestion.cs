using System;

namespace TriviaMazeGUI.Panels.PanelQuestions
{
    [Serializable]
    abstract class PanelQuestion : Panel
    {
        private Panel wrapped;
        public Boolean asked { get; private set; }

        public bool locked
        {
            get => wrapped.locked;
            set => wrapped.locked = value;
        }
        public int depth => wrapped.depth + 1;

        public Room Knock(Room from)
        {
            if (!asked)
            {
                asked = true;
                Ask();
                return wrapped.Knock(from);
            }
            else
            {
                return wrapped.Knock(from);
            }
        }

        protected abstract void Ask();

        public Room Kick(Room from)
        {
            return wrapped.Kick(from);
        }

        public Room Ghost(Room from)
        {
            return wrapped.Ghost(from);
        }

        public void Rewire(Panel obj)
        {
            wrapped.Rewire(obj);
        }

        protected PanelQuestion(Panel wrapping)
        {
            wrapped = wrapping;
            wrapped.Rewire(this);
        }
    }
}
