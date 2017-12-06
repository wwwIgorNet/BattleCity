using System.Collections.Generic;
using System.Windows.Media.Imaging;
using SuperTank;
using System.Windows.Media;

namespace SuperTankWPF.Model
{
    public interface IImageSourceStor
    {
        ImageSource BrickWall { get; }
        ImageSource ConcreteWall { get; }
        ImageSource DashboardInfo { get; }
        ImageSource DashboardInfoIIPlayer { get; }
        ImageSource Eagle { get; }
        ImageSource Eagle2 { get; }
        ImageSourceStor.TankEnemyExtend Enemy { get; }
        ImageSource Forest { get; }
        ImageSource Ice { get; }
        ImageSource InformationTank { get; }
        ImageSource Invulnerable1 { get; }
        ImageSource Invulnerable2 { get; }
        ImageSourceStor.TankPlaeyr Plaeyr { get; }
        ImageSourceStor.TankPlaeyr Plaeyr2 { get; }
        ImageSource ShellDetonation1 { get; }
        ImageSource ShellDetonation2 { get; }
        ImageSource ShellDetonation3 { get; }
        ImageSource ShellDetonation4 { get; }
        ImageSource ShellDetonationBig { get; }
        ImageSource ShellDetonationBig2 { get; }
        ImageSource ShellDown { get; }
        ImageSource ShellLeft { get; }
        ImageSource ShellRight { get; }
        ImageSource ShellUp { get; }
        ImageSource Star1 { get; }
        ImageSource Star2 { get; }
        ImageSource Star3 { get; }
        ImageSource Star4 { get; }
        ImageSourceStor.TankEnemy TankRed { get; }
        ImageSource Water_1 { get; }
        ImageSource Water_2 { get; }
        ImageSource Water_3 { get; }

        Dictionary<Direction, ImageSource[]> GetImages(ImageSourceStor.Tank tank);
        Dictionary<Direction, ImageSource[]> GetImgesForRedTank(TypeUnit type);
        Dictionary<Direction, ImageSource[]> GetImgesForTank(TypeUnit type);
        Dictionary<Direction, ImageSource[]> GetImgesForTankIIPlayer(TypeUnit type);
    }
}