using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Cat_Trap
{
    internal static class StateMachine
    {
        public enum GameState
        {
            Awaiting,
            CatJumping,
            CatEscaping,
            Menu
        }
        public static GameState State = GameState.Awaiting; 
        public static void GameOver()
        {
            State = GameState.Menu;
        }
        public static void BeginJump()
        {
            State = GameState.CatJumping;
        }
        public static void EndJump()
        {
            State = GameState.Awaiting;
        }
        public static void Escaped()
        {
            State = GameState.CatEscaping;
        }
        public static void NewGame()
        {
            Game1.SetUp();
            State = GameState.Awaiting;
        }

    }
}
