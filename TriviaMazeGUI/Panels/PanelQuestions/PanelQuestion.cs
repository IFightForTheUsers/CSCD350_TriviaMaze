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

        public Room knock(Room from)
        {
            if (!asked)
            {
                asked = true;
                Ask();
                return wrapped.knock(from);
            }
            else
            {
                return wrapped.knock(from);
            }
        }

        protected abstract void Ask();

        public Room kick(Room from)
        {
            return wrapped.kick(from);
        }

        public Room ghost(Room from)
        {
            return wrapped.ghost(from);
        }

        public void rewire(Panel obj)
        {
            wrapped.rewire(obj);
        }

        protected PanelQuestion(Panel wrapping)
        {
            wrapped = wrapping;
            wrapped.rewire(this);
        }
    }
}
