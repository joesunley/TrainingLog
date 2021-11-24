using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Net;

namespace Library.Maps
{
    public class DomaMap : IMapFile
    {
        #region -- Fields --
        private Image image,
            blankImage,
            thumbnail;
        private string url;
        #endregion

        #region -- Properties --
        public string URL => url;
        public Image Image => image;
        public Image BlankImage => blankImage;
        public Image Thumbnail => thumbnail;
        #endregion

        #region -- Methods --
        public PictureBox GetImageControl() {
            if (url == null || url == "")
                throw new ArgumentNullException("URL", "Url not set");

            return new PictureBox(){
                Image = image,
                Size = image.Size,
                BorderStyle = BorderStyle.None
            };
        }
        public PictureBox GetBlankImageControl() {
            if (url == null || url == "")
                throw new ArgumentNullException("URL", "Url not set");

            return new PictureBox()
            {
                Image = blankImage,
                Size = blankImage.Size,
                BorderStyle = BorderStyle.None
            };
        }
        public PictureBox GetThumbnailControl() {
            if (url == null || url == "")
                throw new ArgumentNullException("URL", "Url not set");

            return new PictureBox()
            {
                Image = thumbnail,
                Size = thumbnail.Size,
                BorderStyle = BorderStyle.None
            };
        }
        public void ShowMap() {
            GetForm().ShowDialog();
        }
        public Form GetForm() {
            throw new NotImplementedException();
        }
        public void GetFullControl() {

        }

        private string GetImgUrl() {
            int pos = url.IndexOf('/', 12);
            string s = url.Substring(0, pos);
            int index = GetIndex(url);

            return s + "/map_images/" + index.ToString() + ".jpg";
        }
        private string GetBlankUrl() {
            int pos = url.IndexOf('/', 12);
            string s = url.Substring(0, pos);
            int index = GetIndex(url);

            return s + "/map_images/" + index.ToString() + ".blank.jpg";
        }
        private string GetThumbUrl() {
            int pos = url.IndexOf('/', 12);
            string s = url.Substring(0, pos);
            int index = GetIndex(url);

            return s + "/map_images/" + index.ToString() + ".thumbnail.jpg";
        }
        private int GetIndex(string url) => Convert.ToInt32(url.Substring(url.LastIndexOf('=') + 1));
        #endregion

        #region -- Contructors --
        public DomaMap(string url) {
            this.url = url;

            byte[] imgData = new WebClient().DownloadData(GetImgUrl());
            image = Image.FromStream(new MemoryStream(imgData));

            byte[] blnkData = new WebClient().DownloadData(GetBlankUrl());
            blankImage = Image.FromStream(new MemoryStream(blnkData));

            byte[] thmbData = new WebClient().DownloadData(GetThumbUrl());
            thumbnail = Image.FromStream(new MemoryStream(thmbData));
        }
        #endregion
    }
}
