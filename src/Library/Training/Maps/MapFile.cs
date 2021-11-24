using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Maps
{
    public interface IMapFile
    {
        string URL { get; }
        Image Image { get; }

        PictureBox GetImageControl();
        public void ShowMap();
        public Form GetForm();
        public void GetFullControl();
    }
}
