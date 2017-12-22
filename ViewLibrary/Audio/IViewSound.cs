namespace SuperTank.Audio
{
    public interface IViewSound
    {
        /// <summary>
        /// Sound of the game over
        /// </summary>
        void GameOver();
        /// <summary>
        /// Sound of the level start
        /// </summary>
        void LevelStart();
        /// <summary>
        /// The sound of the tank increment (the number of points screen)
        /// </summary>
        void CountTankIncrement();
        /// <summary>
        /// Sound at a record
        /// </summary>
        void HighScore();
        void HighScoreStop();

        float Volume { get; set; }
    }
}
