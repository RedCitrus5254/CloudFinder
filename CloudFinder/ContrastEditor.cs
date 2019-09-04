using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFinder
{
    /// <summary>
    /// Форма, в которой отображаются изображения: оригинальное, с изменённой гаммой, постеризированное
    /// </summary>
    public partial class ContrastEditor : Form
    {
        FindingClouds fc = new FindingClouds();
        int val;
        Thread contrastThread;
        public delegate void MyDelegate(int percent);
        public delegate void PixelValueDelegate();

        public delegate void PictureDelegate(Image image);

        private bool isPixelValueChanged = true;
        private bool isGammaValueChanged = true;

        int pixelValue;
        float gammaValue;

        /// <summary>
        /// Здесь запускается второй поток, в котором обрабатываются картинки
        /// </summary>
        public ContrastEditor()
        {
            InitializeComponent();
            originalImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            posterizeImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            gammaPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            contrastThread = new Thread(new ThreadStart(GoThread))
            {
                IsBackground = true
            };
            
            contrastThread.Start();
            
        }

        /// <summary>
        /// Выбираем квиклук, сразу отображаем все 3 изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pathToImageTextBox.Text = openFileDialog.FileName;

                        System.IO.FileInfo file = new System.IO.FileInfo(pathToImageTextBox.Text); //запрещаем открывать большие файлы
                        long size = file.Length;
                        if (size > 20000000)
                        {
                            MessageBox.Show("Не надо открывать большие файлы!!!");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Big terrible error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                    originalImagePictureBox.Image = null;
                    originalImagePictureBox.Image = Image.FromFile(pathToImageTextBox.Text);


                    gammaValue = gammaTrackBar.Value / 10.0f;
                    //gammaPictureBox.Image = null;
                    gammaPictureBox.Image = fc.ChangeGamma(originalImagePictureBox.Image, gammaValue);

                    pixelValue = (int)(contrastTrackBar.Value * 255 / 100);
                    posterizeImagePictureBox.Image = fc.Posterize(gammaPictureBox.Image, pixelValue);
                    cloudPercentLabel.Text = $"Процент облачности: {fc.CloudPercent}%";
                }
            }
        }

        /// <summary>
        /// вызывает сборщик мусора, меняет гамму и в потоке интерфейса выводит изменённое изображение
        /// </summary>
        /// <param name="inputImage"></param>
        /// <param name="gamma"></param>
        /// <returns></returns>
        private bool ChangeGamma(Image inputImage, float gamma)
        {
            if (gammaValue != 0)
            {
                GC.Collect();

                if (originalImagePictureBox.Image != null)
                {
                    Image outputImage = fc.ChangeGamma(inputImage, gamma);
                    //gammaPictureBox.Image = null;
                    //gammaPictureBox.Image = image;

                    gammaPictureBox.BeginInvoke(new PictureDelegate(SetGammaPictureBoxImage), outputImage);
                    
                }
            }
            else
            {
                return false;
            }
            if (findCloudinessCheckBox.Checked) //если прожат чекбокс, считаем облачность сразу. Иначе только при перемещении ползунка порога яркости
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// вызывает сборщик мусора, проводит постеризацию и в потоке интерфейса выводит изменённое изображение и процент облачности
        /// </summary>
        /// <param name="inputImage"></param>
        /// <param name="contrast"></param>
        private void ChangePosterization(Image inputImage, int contrast)
        {
            GC.Collect();

            if (originalImagePictureBox.Image != null)
            {
                Image outImage = fc.Posterize(inputImage, contrast);

                //contrastImagePictureBox.Image = null;
                //contrastImagePictureBox.Image = image;
                posterizeImagePictureBox.BeginInvoke(new PictureDelegate(SetPosterizePictureBoxImage), outImage);

                cloudPercentLabel.BeginInvoke(new MyDelegate(SetCloudPercent), fc.CloudPercent);

            }
        }

        /// <summary>
        /// Метод второго потока, который вызывает методы постеризации и гаммы, когда смещается тот или иной ползунок
        /// </summary>
        private void GoThread()
        {
            while (true)
            {
                if (isGammaValueChanged)
                {
                    //Console.WriteLine("gamma == true");
                    isGammaValueChanged = false;
                    if (ChangeGamma(originalImagePictureBox.Image, gammaValue))
                    {
                        //Console.WriteLine("pixel in gamma == true");
                        ChangePosterization(fc.bitmapForPosterize, pixelValue);
                        isPixelValueChanged = false;
                    }
                }
                if (isPixelValueChanged)
                {
                    //Console.WriteLine("pixel == true");
                    isPixelValueChanged = false;
                    ChangePosterization(gammaPictureBox.Image, pixelValue);
                }
            }
            
        }

        public void SetPosterizePictureBoxImage(Image image)
        {
            posterizeImagePictureBox.Image = image;
        }
        public void SetGammaPictureBoxImage(Image image)
        {
            //gammaPictureBox.Image = null;
            gammaPictureBox.Image = image;
        }

        private void ContrastTrackBar_Scroll(object sender, EventArgs e)
        {
            pixelValueLabel.Text = $"{(int)(contrastTrackBar.Value*2.55)}";

            pixelValue = (int)(contrastTrackBar.Value * 2.55);
            isPixelValueChanged = true;
            
        }

        private void SetPixelValue()
        {
            pixelValue = (int)(contrastTrackBar.Value * 2.55);
        }
        private void SetCloudPercent(int percent)
        {
            cloudPercentLabel.Text = $"Процент облачности: {percent}%";
        }

        private void GammaTrackBar_Scroll(object sender, EventArgs e)
        {
            gammaValueLabel.Text = $"{gammaTrackBar.Value/10.0f}";
            gammaValue = gammaTrackBar.Value / 10.0f;
            isGammaValueChanged = true;
        }

        /// <summary>
        /// если стоит галочка в чекбоксе "Искать облачность сразу", то после изменения гаммы сразу идёт постеризация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindCloudinessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (findCloudinessCheckBox.Checked)
            {
                fc.GammaAndPosterize = true;
            }
            else
            {
                fc.GammaAndPosterize = false;
            }
        }
    }
}
