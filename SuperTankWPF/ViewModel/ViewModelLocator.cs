using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SuperTank.Audio;
using SuperTankWPF.Model;
using SuperTankWPF.Units;
using SuperTankWPF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartScrenViewModel>();
            SimpleIoc.Default.Register<ScrenScoreViewModel>(() => new ScrenScoreViewModel
            {
                IsTwoPlayer = IsTwoPlayer
            });
            SimpleIoc.Default.Register<LevelInfoViewModel>(() => new LevelInfoViewModel());
            SimpleIoc.Default.Register<ScrenGameViewModel>(() => new ScrenGameViewModel
            {
                Level = 5,
                IsShowAnimationNewLevel = false,
                IsShowGameOver = false
            });
            SimpleIoc.Default.Register<ScrenRecordViewModel>(() => new ScrenRecordViewModel
            {
                CountPoints = 435435
            });
            SimpleIoc.Default.Register<ScrenSceneViewModel>();
            SimpleIoc.Default.Register<GameMenedger>();
            SimpleIoc.Default.Register<IImageSourceStor, ImageSourceStor>(); 
            SimpleIoc.Default.Register<IFactoryUnitView, FactoryUnitView>();
        }

        public static bool IsTwoPlayer { get; set; }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public StartScrenViewModel StartScren
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartScrenViewModel>();
            }
        }
        public ScrenScoreViewModel ScrenScore
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenScoreViewModel>();
            }
        }
        public LevelInfoViewModel LevelInfoLeve
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
            }
        }
        public ScrenGameViewModel ScrenGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenGameViewModel>();
            }
        }
        public ScrenRecordViewModel ScrenRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenRecordViewModel>();
            }
        }
        public ScrenSceneViewModel ScrenScene
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenSceneViewModel>();
            }
        }
        public IImageSourceStor ImageSource
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IImageSourceStor>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
