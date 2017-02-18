using UnityEngine;
using System.Collections;

public class StateManager {
    public enum State
    {
        PRODUCTION, BUILD, RANKUP,EVENT
    }

    public static State state { get; set; }

    StateManager()
    {
        state = State.PRODUCTION;
    }
}
