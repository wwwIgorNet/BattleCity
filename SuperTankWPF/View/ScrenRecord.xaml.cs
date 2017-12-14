using CommonServiceLocator;
using SuperTank.Audio;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for ScrenRecord.xaml
    /// </summary>
    public partial class ScrenRecord : UserControl
    {
        ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
        private int milisecondAnimation = 20;

        public ScrenRecord()
        {
            InitializeComponent();

            IsVisibleChanged += ScrenRecord_IsVisibleChanged;

            objectAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteObjectKeyFrame(Brushes.White, TimeSpan.FromMilliseconds(milisecondAnimation)));
            objectAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteObjectKeyFrame(Brushes.Blue, TimeSpan.FromMilliseconds(milisecondAnimation * 2)));
            objectAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteObjectKeyFrame(Brushes.Yellow, TimeSpan.FromMilliseconds(milisecondAnimation * 3)));
        }

        private void ScrenRecord_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                objectAnimationUsingKeyFrames.Completed += ColorAnimationUsingKeyFrames_Completed;
                textHiscore.BeginAnimation(TextBlock.ForegroundProperty, objectAnimationUsingKeyFrames);
            }
            else
                objectAnimationUsingKeyFrames.Completed -= ColorAnimationUsingKeyFrames_Completed;

        }

        private void ColorAnimationUsingKeyFrames_Completed(object sender, EventArgs e)
        {
            textHiscore.BeginAnimation(TextBlock.ForegroundProperty, objectAnimationUsingKeyFrames);
        }
    }
}
