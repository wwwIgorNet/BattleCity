using GalaSoft.MvvmLight;
using SuperTank;
using SuperTank.Audio;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.ViewModel
{
    public class ScreenScoreViewModel : ObservableObject
    {
        private string textStage;
        private string stage;
        private PlayerDeteils player1;
        private PlayerDeteils player2;
        private bool isTwoPlayer;
        private IViewSound sound;

        public ScreenScoreViewModel(IViewSound sound)
        {
            this.sound = sound;
            Init();
        }

        public void Set(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr,
                                   int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            Clear();

            if (isTwoPlayer)
                ShowInfo(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
            else
                ShowInfo(level, countPointsIPlayer, destrouTanksIPlaeyr);
        }

        private async void ShowInfo(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr)
        {
            SetLevel(level);
            Player1.TotalCountPoints = countPointsIPlayer;

            foreach (var item in ListDeteils)
            {
                int count = destrouTanksIPlaeyr[item.Key];
                for (int i = 0; i <= count; i++)
                {
                    item.Value.CountTanl1Player = i;
                    item.Value.Points1Player = i * ConfigurationWPF.GetCountPoints(item.Key);
                    if (i != 0)
                        sound.CountTankIncrement();
                    await Task.Delay(150);
                }
            }
            player1.TotalCountTank = destrouTanksIPlaeyr.Sum(kv => kv.Value);
        }

        private async void ShowInfo(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            SetLevel(level);
            Player1.TotalCountPoints = countPointsIPlayer;
            Player2.TotalCountPoints = countPointsIIPlayer;

            foreach (var item in ListDeteils)
            {
                int countIPlayer = destrouTanksIPlaeyr[item.Key];
                int countIIPlayer = destrouTanksIIPlaeyr[item.Key];
                for (int i = 0, j = 0; ;)
                {
                    if (i <= countIPlayer)
                    {
                        item.Value.CountTanl1Player = i;
                        item.Value.Points1Player = i * ConfigurationWPF.GetCountPoints(item.Key);
                        i++;
                    }
                    if (j <= countIIPlayer)
                    {
                        item.Value.CountTanl2Player = j;
                        item.Value.Points2Player = j * ConfigurationWPF.GetCountPoints(item.Key);
                        j++;
                    }

                    if (i > 1 || j > 1)
                        sound.CountTankIncrement();

                    await Task.Delay(150);

                    if (i > countIPlayer && j > countIIPlayer) break;
                }
            }
            player1.TotalCountTank = destrouTanksIPlaeyr.Sum(kv => kv.Value);
            player2.TotalCountTank = destrouTanksIIPlaeyr.Sum(kv => kv.Value);
        }

        public void Init()
        {
            Player1 = new PlayerDeteils();
            Player2 = new PlayerDeteils();
            TankDetils.InitIsTwoPlayer(IsTwoPlayer);

            ListDeteils.Add(TypeUnit.PlainTank, Plain);
            ListDeteils.Add(TypeUnit.ArmoredPersonnelCarrierTank, ArmoredPersonnelCarrier);
            ListDeteils.Add(TypeUnit.QuickFireTank, QuickFire);
            ListDeteils.Add(TypeUnit.ArmoredTank, Armored);
            
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
                item.Value.Clear();

            player1.TotalCountTank = -1;
            Player2.TotalCountTank = -1;
        }

        public bool IsTwoPlayer
        {
            get { return isTwoPlayer; }
            set
            {
                isTwoPlayer = value;
                InitTextIsTwoPlayer();
                TankDetils.InitIsTwoPlayer(value);
                foreach (var item in ListDeteils)
                    item.Value.Init();
            }
        }

        public void SetLevel(int level)
        {
            Stage = textStage + " " + level;
        }

        public string Stage
        {
            get { return stage; }
            private set { Set(nameof(Stage), ref stage, value); }
        }
        public PlayerDeteils Player1
        {
            get { return player1; }
            private set { Set(nameof(Player1), ref player1, value); }
        }
        public PlayerDeteils Player2
        {
            get { return player2; }
            private set { Set(nameof(Player2), ref player2, value); }
        }

        public TankDetils Plain { get; } = new TankDetils();
        public TankDetils ArmoredPersonnelCarrier { get; } = new TankDetils();
        public TankDetils QuickFire { get; } = new TankDetils();
        public TankDetils Armored { get; } = new TankDetils();

        public Dictionary<TypeUnit, TankDetils> ListDeteils { get; } = new Dictionary<TypeUnit, TankDetils>();

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
                private set { Set(nameof(PTS1P), ref pts1p, value); }
            }
            public string PTS2P
            {
                get { return pts2p; }
                private set { RaisePropertyChanged(nameof(PTS2P)); }
            }
            public string ArrowLeft
            {
                get { return arrowLeft; }
                private set { Set(nameof(ArrowLeft), ref arrowLeft, value); }
            }
            public string ArrowRight
            {
                get { return arrowRight; }
                private set { RaisePropertyChanged(nameof(ArrowRight)); }
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

            public void Init()
            {
                this.PTS2P = pts2p;
                this.ArrowRight = arrowRight;
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
