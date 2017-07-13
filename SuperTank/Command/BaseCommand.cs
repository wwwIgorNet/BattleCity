namespace SuperTank.Command
{
    abstract class BaseCommand : ICommand
    {
        private Unit unit;

        public BaseCommand(Unit unit)
        {
            this.unit = unit;
        }

        public Unit Unit { get { return unit; } }

        public abstract void Execute();
    }
}
