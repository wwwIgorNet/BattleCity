using GalaSoft.MvvmLight;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.ViewModel
{
    public class ScrenScoreViewModel : ObservableObject
    {
        private string textStage;
        private int level;
        private string stage;
        private PlayerDeteils player1;
        private PlayerDeteils player2;
        private bool isTwoPlayer;

        public ScrenScoreViewModel()
        {
            Init();
        }

        private void TestInit1P()
        {
            Level = 4;

            Player1.TotalCountPoints = 33333;

            Plain.Points1Player = 33;
            Plain.CountTanl1Player = 4;

            ArmoredPersonnelCarrier.Points1Player = 55;
            ArmoredPersonnelCarrier.CountTanl1Player = 15;

            QuickFire.Points1Player = 0;
            QuickFire.CountTanl1Player = 0;

            Armored.Points1Player = 111;
            Armored.CountTanl1Player = 1;

            Player1.TotalCountTank = 14;
        }
        private void TestInit2P()
        {
            Player2.TotalCountPoints = 123456;

            Plain.Points2Player = 1000;
            Plain.CountTanl2Player = 20;

            ArmoredPersonnelCarrier.Points2Player = 66;
            ArmoredPersonnelCarrier.CountTanl2Player = 9;

            QuickFire.Points2Player = 7;
            QuickFire.CountTanl2Player = 1;

            Armored.Points2Player = 44;
            Armored.CountTanl2Player = 2;

            Player2.TotalCountTank = 7;
        }

        public void Init()
        {
            Player1 = new PlayerDeteils();
            Player2 = new PlayerDeteils();
            TankDetils.InitIsTwoPlayer(IsTwoPlayer);

            ListDeteils.Add(Plain);
            ListDeteils.Add(ArmoredPersonnelCarrier);
            ListDeteils.Add(QuickFire);
            ListDeteils.Add(Armored);
            
            textStage = "STAGE";

            Player1.TextPlayer = " I PLAYER";
            Player1.TextTotal = "TOTAL";

            InitTextIsTwoPlayer();
            Clear();
        }

        private void InitTextIsTwoPlayer()
        {
            if (IsTwoPlayer)
            {
                Player2.TextPlayer = "II PLAYER";
                Player2.TextTotal = "TOTAL";
            }
            else
            {
                Player2.TextPlayer = new string(' ', Player1.TextPlayer.Length);
                Player2.TotalCountPoints = -1;
                Player2.TextTotal = new string(' ', Player1.TextTotal.Length);
                Player2.TotalCountTank = -1;
            }
        }

        public void Clear()
        {
            foreach (var item in ListDeteils)
                item.Clear();
        }

        public bool IsTwoPlayer
        {
            get { return isTwoPlayer; }
            set
            {
                isTwoPlayer = value;
                InitTextIsTwoPlayer();
                TankDetils.InitIsTwoPlayer(value);

                TestInit1P();// todo delite test metods
                if(value == true) TestInit2P();
            }
        }

        public int Level
        {
            get { return level; }
            set { Stage = textStage + " " + value; }
        }

        public string Stage
        {
            get { return stage; }
            set { Set(nameof(Stage), ref stage, value); }
        }
        public PlayerDeteils Player1
        {
            get { return player1; }
            set { Set(nameof(Player1), ref player1, value); }
        }
        public PlayerDeteils Player2
        {
            get { return player2; }
            set { Set(nameof(Player2), ref player2, value); }
        }

        public TankDetils Plain { get; } = new TankDetils();
        public TankDetils ArmoredPersonnelCarrier { get; } = new TankDetils();
        public TankDetils QuickFire { get; } = new TankDetils();
        public TankDetils Armored { get; } = new TankDetils();

        public ObservableCollection<TankDetils> ListDeteils { get; } = new ObservableCollection<TankDetils>();

        public class TankDetils : ObservableObject
        {
            private static string pts1p;
            private static string pts2p;
            private static string arrowLeft;
            private static string arrowRight;

            private int points1Player;
            private int points2Player;
            private int сountTanl1Player;
            private int сountTanl2Player;

            public TankDetils()
            {
                Clear();
            }

            public string PTS1P
            {
                get { return pts1p; }
                set { Set(nameof(PTS1P), ref pts1p, value); }
            }
            public string PTS2P
            {
                get { return pts2p; }
                set { Set(nameof(PTS2P), ref pts2p, value); }
            }
            public string ArrowLeft
            {
                get { return arrowLeft; }
                set { Set(nameof(ArrowLeft), ref arrowLeft, value); }
            }
            public string ArrowRight
            {
                get { return arrowRight; }
                set { Set(nameof(ArrowRight), ref arrowRight, value); }
            }

            public int Points1Player
            {
                get { return points1Player; }
                set { Set(nameof(Points1Player), ref points1Player, value); }
            }
            public int Points2Player
            {
                get { return points2Player; }
                set { Set(nameof(Points2Player), ref points2Player, value); }
            }

            public int CountTanl1Player
            {
                get { return сountTanl1Player; }
                set { Set(nameof(CountTanl1Player), ref сountTanl1Player, value); }
            }
            public int CountTanl2Player
            {
                get { return сountTanl2Player; }
                set { Set(nameof(CountTanl2Player), ref сountTanl2Player, value); }
            }

            public static void InitIsTwoPlayer(bool isTwoPlayer)
            {
                pts1p = "PTS";
                arrowLeft = "←";

                if (isTwoPlayer)
                {
                    pts2p = "PTS";
                    arrowRight = "→";
                }
                else
                {
                    pts2p = new string(' ', pts1p.Length);
                    arrowRight = new string(' ', arrowLeft.Length);
                }
            }

            public void Clear()
            {
                Points1Player = -1;
                Points2Player = -1;
                CountTanl1Player = -1;
                CountTanl2Player = -1;
            }
        }

        public class PlayerDeteils : ObservableObject
        {
            private string textPlayer;
            private int totalCountPoints;
            private string textTotal;
            private int totalCountTank;

            public string TextPlayer
            {
                get { return textPlayer; }
                set { Set(nameof(TextPlayer), ref textPlayer, value); }
            }
            public int TotalCountPoints
            {
                get { return totalCountPoints; }
                set { Set(nameof(TotalCountPoints), ref totalCountPoints, value); }
            }
            public string TextTotal
            {
                get { return textTotal; }
                set { Set(nameof(TextTotal), ref textTotal, value); }
            }
            public int TotalCountTank
            {
                get { return totalCountTank; }
                set { Set(nameof(TotalCountTank), ref totalCountTank, value); }
            }
        }
    }
}
