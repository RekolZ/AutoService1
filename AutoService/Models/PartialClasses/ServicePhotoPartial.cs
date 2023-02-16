using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
