using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RestarauntClient.ViewModels
{
    public class ServiceZonePageVM
    {
        ServiceZonePageVM()
        {
            Grid grid = new Grid();
            Viewbox viewbox = new Viewbox();
            
            grid.Children.Add(new Path { Data = Geometry.Parse("") });
        }
    }
}
