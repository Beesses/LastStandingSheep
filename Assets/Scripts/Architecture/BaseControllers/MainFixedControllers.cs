namespace LastStandingSheep
{
    public sealed class MainFixedControllers : ControllersStart
    {
        #region ClassLifeCycles

        public MainFixedControllers(GameContext context)
        {
            AddInitializeControllers(context);
            Add(new InitializeInteractableObjectController(context));
            AddControllers(context);
        }

        #endregion


        #region Methods

        private void AddInitializeControllers(GameContext context)
        {
            Add(new SheepInitialize(context));
            Add(new WolfInitialize(context));
            Add(new PlayerInitialize(context));
        }

        private void AddControllers(GameContext context)
        {
            Add(new SheepController(context));
            Add(new WolfController(context));
            Add(new PlayerController(context));
        }

        #endregion
    }
}
