using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SuperTank;
using System.ServiceModel;
using SuperTankWPF.Units;

namespace SuperTankWPF
{
    /// <summary>
    /// Interaction logic for SceneScene.xaml
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class SceneScene : UserControl, IRender  
    {
        private FactoryViewUnit factoryViewUnit = new FactoryViewUnit();

        public SceneScene()
        {
            InitializeComponent();
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            ViewUnit view = factoryViewUnit.Create(id, x, y, typeUnit, properties);
            board.Children.Add(view);
            //if (view != null) listDrowable.Add(view);
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    UnitDataForView data = collection[i];
            //    Add(data.ID, data.TypeUnit, data.X, data.Y, data.Properties);
            //}
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            try
            {
                //BaseView viewUpdate = listDrowable.FindByID(id);
                //switch (prop)
                //{
                //    case PropertiesType.X:
                //        viewUpdate.X = (int)value;
                //        break;
                //    case PropertiesType.Y:
                //        viewUpdate.Y = (int)value;
                //        break;
                //    //case PropertiesType.Direction:
                //    //case PropertiesType.IsStop:
                //    //case PropertiesType.Detonation:
                //    //case PropertiesType.Glide:
                //    default:
                //        viewUpdate.Properties[prop] = value;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
