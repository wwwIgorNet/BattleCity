using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartScrenViewModel>();
            SimpleIoc.Default.Register<ScrenScoreViewModel>(() => new ScrenScoreViewModel(IsTwoPlayer));
            SimpleIoc.Default.Register<LevelInfoViewModel>(() => new LevelInfoViewModel(IsTwoPlayer));
            SimpleIoc.Default.Register<ScrenGameViewModel>();
        }

        public bool IsTwoPlayer { get; set; } = true;

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

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
