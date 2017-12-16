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

namespace SuperTankWPF.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<SynchronizationContext>(() => SynchronizationContext.Current);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartScrenViewModel>();
            SimpleIoc.Default.Register<ScrenScoreViewModel>();
            SimpleIoc.Default.Register<LevelInfoViewModel>();
            SimpleIoc.Default.Register<ScrenGameViewModel>();
            SimpleIoc.Default.Register<ScrenRecordViewModel>();
            SimpleIoc.Default.Register<ScrenConstructionViewModel>();

            SimpleIoc.Default.Register<ScrenSceneViewModel>();
            SimpleIoc.Default.Register<IRender>(() => ServiceLocator.Current.GetInstance<ScrenSceneViewModel>());

            SimpleIoc.Default.Register<GameMenedger>();

            SimpleIoc.Default.Register<IPlayerGameManedger>();
            SimpleIoc.Default.Register<IGameInfo>(() => ServiceLocator.Current.GetInstance<IPlayerGameManedger>());
            
            SimpleIoc.Default.Register<IImageSourceStor, ImageSourceStor>();
            SimpleIoc.Default.Register<IFactoryUnitView, FactoryUnitView>();

            SimpleIoc.Default.Register<Model.SoundGame>(() =>
            {
                Model.SoundGame soundGame = new Model.SoundGame(ServiceLocator.Current.GetInstance<SynchronizationContext>());
                soundGame.OpenMedia(ConfigurationWPF.SoundPath, UriKind.Relative);
                return soundGame;
            });
            SimpleIoc.Default.Register<ISoundGame>(() => ServiceLocator.Current.GetInstance<Model.SoundGame>());
            SimpleIoc.Default.Register<IViewSound>(() => ServiceLocator.Current.GetInstance<Model.SoundGame>());

            SimpleIoc.Default.Register<Comunication>();
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
        public ScrenConstructionViewModel ScrenConstruction
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenConstructionViewModel>();
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
            ServiceLocator.Current.GetInstance<GameMenedger>().Dispose();
            ServiceLocator.Current.GetInstance<Model.SoundGame>().Dispose();
        }
    }
}
