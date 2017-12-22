using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.Model;
using SuperTankWPF.Units;
using SuperTankWPF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ViewLibrary.Audio;

namespace SuperTankWPF.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<SynchronizationContext>(() => SynchronizationContext.Current);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ScreenStartViewModel>();
            SimpleIoc.Default.Register<ScreenScoreViewModel>();
            SimpleIoc.Default.Register<LevelInfoViewModel>();
            SimpleIoc.Default.Register<ScreenGameViewModel>();
            SimpleIoc.Default.Register<ScreenRecordViewModel>();
            SimpleIoc.Default.Register<ScreenConstructionViewModel>();
            SimpleIoc.Default.Register<ScreenLockViewModel>();
            SimpleIoc.Default.Register<DialogIPViewModel>();

            SimpleIoc.Default.Register<ScreenSceneViewModel>();
            SimpleIoc.Default.Register<IRender>(() => ServiceLocator.Current.GetInstance<ScreenSceneViewModel>());

            SimpleIoc.Default.Register<GameMenedger>();

            SimpleIoc.Default.Register<GameInfo>();
            SimpleIoc.Default.Register<IGameInfo>(() => ServiceLocator.Current.GetInstance<GameInfo>());

            SimpleIoc.Default.Register<IImageSourceStor, ImageSourceStor>();
            SimpleIoc.Default.Register<IFactoryUnitView, FactoryUnitView>();

            SoundGame soundGame = new SoundGame();
            soundGame.OpenMedia(ConfigurationWPF.SoundPath);
            SimpleIoc.Default.Register<SoundGame>(() => soundGame);

            SimpleIoc.Default.Register<ISoundGame>(() => ServiceLocator.Current.GetInstance<SoundGame>());
            SimpleIoc.Default.Register<IViewSound>(() => ServiceLocator.Current.GetInstance<SoundGame>());

            SimpleIoc.Default.Register<ComunicationTCP>();
        }

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

        public ScreenStartViewModel ScreenStart
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenStartViewModel>();
            }
        }
        public ScreenScoreViewModel ScreenScore
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenScoreViewModel>();
            }
        }
        public LevelInfoViewModel LevelInfoLeve
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
            }
        }
        public ScreenGameViewModel ScreenGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenGameViewModel>();
            }
        }
        public ScreenRecordViewModel ScreenRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenRecordViewModel>();
            }
        }
        public ScreenSceneViewModel ScreenScene
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenSceneViewModel>();
            }
        }
        public ScreenConstructionViewModel ScreenConstruction
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenConstructionViewModel>();
            }
        }
        public ScreenLockViewModel ScreenLock
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenLockViewModel>();
            }
        }
        public DialogIPViewModel DialogIP
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DialogIPViewModel>();
            }
        }
        public IImageSourceStor ImageSource
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IImageSourceStor>();
            }
        }
        public IViewSound SoundGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IViewSound>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ServiceLocator.Current.GetInstance<GameMenedger>().Dispose();
            ServiceLocator.Current.GetInstance<SoundGame>().Dispose();
        }
    }
}
