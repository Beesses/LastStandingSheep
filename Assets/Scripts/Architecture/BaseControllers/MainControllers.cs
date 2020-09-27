namespace LastStandingSheep
{
    public sealed class MainControllers : ControllersStart
    {
        #region ClassLifeCycles

        public MainControllers(GameContext context)
        {
            AddInitializeControllers(context);
            AddControllers(context);
        }

        #endregion


        #region Methods

        private void AddInitializeControllers(GameContext context) 
        {
            Add(new GameLevelInitialize(context));
        }

        private void AddControllers(GameContext context)
        {
            Add(new GameLevelController(context));
        }

        #endregion
    }
}
