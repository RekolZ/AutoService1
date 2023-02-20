using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoService.Models
{
    public partial class ServicePhoto
    {
        public byte[] MainImage { get
            {

                return
                    File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\Assets\\img\\{PhotoPath}");

                ;
                    }
        }
        public Visibility VisibilityAdminElements
        {
            get
            {

                if (App.CurrentUser.RoleId == 1)
                {
                   return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
                return VisibilityAdminElements;
            }
        }
    }
}
