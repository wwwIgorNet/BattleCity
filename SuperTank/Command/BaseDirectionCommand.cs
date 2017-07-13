namespace SuperTank.Command
{
    abstract class BaseDirectionCommand : BaseCommand
    {
        public BaseDirectionCommand(Unit unit) : base(unit)
        {
        }

        public Direction Direction
        {
            get { return (Direction)Unit.Properties[PropertiesType.Direction]; }
            set { Unit.Properties[PropertiesType.Direction] = value; }
        }
    }
}
