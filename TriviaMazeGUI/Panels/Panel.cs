namespace TriviaMazeGUI.Panels
{
    /// <summary>
    /// Common interface for things connecting rooms or blocking access from rooms.
    /// they all must be serializable
    /// </summary>
    interface Panel
    {
        bool locked { get; set; } // passable objects start unlocked. impassable objects starts locked.
        // passable objects may be set to locked but not unlocked without a cheat
        int depth { get; } // depth indicates how many are stringed together
        Room knock(Room from); // knock(from room): to room. if locked just returns from
        Room kick(Room from); // cheat that moves to next room always
        void rewire(Panel obj); // rewires the Panel's Rooms to point to the obj instead of it's self. mostly for decorators
        Room ghost(Room from); // ghosts to next room
    }
}
