
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace PhotoViewer
{
    public delegate void SenderImg(ImgItem item);
    public enum ImageType
    {
      Photo,
      Video,
      Unknown
    }
   public class Presenter
    {
        private ImgItem ImgCurrent;
        private int Counter = 0;
       

        public ObservableCollection<ImgItem> ImgList { get; set; }

        public Presenter(int time)
        {
         
            ImgList = new ObservableCollection<ImgItem>();
            OpenFolderToImgFileList();
      
        }

        public ImgItem SelectedImg
        {
            get { return ImgCurrent; }
            set
            {
                ImgCurrent = value;
            }
        }

        private void OpenFolderToImgFileList()
        {
            var files = Directory.GetFiles("c:\\Test");
            foreach (var file in files) 
            {
                ImgList.Add(new ImgItem(file));
            }
        }
       

        public event SenderImg ImageSelected;
        public event SenderImg VideoSelected;
        public void OnPropertyChanged(ImgItem itemCurrent)
        {
            if ((ImageSelected != null) && (itemCurrent.ImageType == ImageType.Photo))
                ImageSelected(itemCurrent);
            else if ((ImageSelected != null) && (itemCurrent.ImageType == ImageType.Video))
                VideoSelected(itemCurrent);
        }

        public void ShowImages()
        {
            ImgCurrent = ImgList.ElementAt(Counter);
            OnPropertyChanged(ImgCurrent);
            Counter++;
            CheckForResetCounter();

        }

        private void CheckForResetCounter()
        {
            if (ImgList.Count == Counter)
                Counter = 0;
        }
    }
}
