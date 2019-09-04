using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFinder
{
    /// <summary>
    /// Главный класс
    /// Меняет гамму и постеризует изображение
    /// </summary>
    class FindingClouds
    {
        //const string DIRECTORY = @"C:\Users\rez\Pictures\Saburov\forQuicklooks\";
        //const string IMAGE_NAME = "L1C_T37VDH_A020410_20190520T084602";
        //const string imagePath = DIRECTORY + IMAGE_NAME + ".jpg";

        //static Bitmap image = new Bitmap(imagePath);
        //Bitmap grayImage = new Bitmap(image.Width, image.Height);
        //double cloudPercent;
        //Bitmap contrastImage = new Bitmap(image.Width, image.Height);

        //int redPixels = 0; //пиксели, которые не входят в изображение

        //const string DIRECTORY = @"C:\Users\rez\Pictures\Saburov\forQuicklooks\";
        //const string IMAGE_NAME = "L1C_T37VDH_A020410_20190520T084602";
        //const string imagePath = DIRECTORY + IMAGE_NAME + ".jpg";

        //Image img = Image.FromFile(@"D:\фотки\я2.jpg");

        //static Bitmap image = new Bitmap(imagePath);
        //Bitmap grayImage = new Bitmap(image.Width, image.Height);
        //double cloudPercent;
        //Bitmap contrastImage = new Bitmap(image.Width, image.Height);

        //int redPixels = 0; //пиксели, которые не входят в изображение

        int numOfWhitePixels;
        int otherPixels;

        public Bitmap bitmapForPosterize;
        public bool GammaAndPosterize { get; set; } = true;



        public int CloudPercent { get; set; }

        ~FindingClouds()
        {

        }

        /// <summary>
        /// Меняем гамму. При увеличении параметра gamma, тёмные пиксели становятся сильно темнее, а светлые становятся темнее совсем немного;
        /// Неуправляемый код
        /// </summary>
        /// <param name="image"></param>
        /// <param name="gamma">гамма 1-10</param>
        /// <returns></returns>
        public Image ChangeGamma(Image image, float gamma)
        {
            Bitmap bitmap = new Bitmap(image);
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr Scan0 = bmData.Scan0;

            float pixel;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int j = 0; j < bitmap.Height; j++)
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        if ((p[0] == p[1]) && (p[1] == p[2]) && (p[2] == 0))
                        {
                            p += 3;
                            continue;
                        }
                        pixel = p[0] / 255.0f;
                        p[0] = (byte)(255 * Math.Pow(pixel, gamma));
                        if (p[0] == 0) //устанавливаем значение одного канала = 1, чтобы при постеризации не закрасить изображение красным 
                        {
                            p[0] = 1;
                        }

                        pixel = p[1] / 255.0f;
                        p[1] = (byte)(255 * Math.Pow(pixel, gamma));

                        pixel = p[2] / 255.0f;
                        p[2] = (byte)(255 * Math.Pow(pixel, gamma));

                        p += 3;
                    }

                }

                bitmap.UnlockBits(bmData);
                //b.Save(@"C:\Users\rez\Pictures\Saburov\cosmoPictures\cloudGamma.jpg");

                if(GammaAndPosterize == true)
                {
                    bitmapForPosterize = new Bitmap(bitmap);
                }
                return bitmap;

            }
        }

        /// <summary>
        /// Не работает и работает очень долго
        /// </summary>
        /// <param name="image"></param>
        /// <param name="sigma"></param>
        /// <returns></returns>
        public Image GaussianFilter(Image image, int sigma)
        {
            Bitmap b = new Bitmap(image);
            BitmapData bmData1 = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride1 = bmData1.Stride;
            System.IntPtr Scan01 = bmData1.Scan0;

            Bitmap newBmp = new Bitmap(image.Width, image.Height);
            BitmapData bmData2 = newBmp.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride2 = bmData2.Stride;
            System.IntPtr Scan02 = bmData2.Scan0;

            double sum;

            unsafe
            {
                byte* pix1 = (byte*)(void*)Scan01;
                byte* pix2 = (byte*)(void*)Scan02;

                int nOffset = stride1 - b.Width * 3;

                double[] window = new double[3 * sigma];
                window[0] = 1;
                for(int i = 1; i < window.Length; i++)
                {
                    window[i] = Math.Pow(Math.E, (-i * i) / (2 * sigma * sigma));
                }
                //проходим по строкам
                for(int j = 0; j < b.Height; j++)
                {
                    for(int i=0; i < b.Width; i++)
                    {
                        sum = 0;
                        pix2[0] = pix2[1] = pix2[2] = 0;

                        
                        for(int k = 0; k < window.Length; k++)
                        {
                            if (i + k < b.Width)
                            {
                                pix1 = pix1 + k * 3;
                                pix2[0] = (byte)(pix2[0] + pix1[0] * window[k]);
                                pix2[1] = (byte)(pix2[1] + pix1[1] * window[k]);
                                pix2[2] = (byte)(pix2[2] + pix1[2] * window[k]);
                                sum += window[k];
                                pix1 = pix1 - k * 3;
                            }

                            if (i - k >= 0)
                            {
                                pix1 = pix1 - k * 3;
                                pix2[0] = (byte)(pix2[0] + pix1[0] * window[k]);
                                pix2[1] = (byte)(pix2[1] + pix1[1] * window[k]);
                                pix2[2] = (byte)(pix2[2] + pix1[2] * window[k]);
                                sum += window[k];
                                pix1 = pix1 + k * 3;
                            }
                        }
                        pix2[0] = (byte)(pix2[0] / sum);
                        pix2[1] = (byte)(pix2[1] / sum);
                        pix2[2] = (byte)(pix2[2] / sum);
                        //Console.WriteLine("sum = " + sum);
                        pix1 += 3;
                        pix2 += 3;
                    }
                    pix1 += nOffset;
                    pix2 += nOffset;
                }

                pix1 = (byte*)(void*)Scan01;
                pix2 = (byte*)(void*)Scan02;
                //проходим по столбцам
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        sum = 0;
                        pix2[0] = pix2[1] = pix2[2] = 0;


                        for (int k = 0; k < window.Length; k++)
                        {
                            //Console.WriteLine("i: "+i+" j: "+j+" k: "+k);
                            if (i + k < b.Height)
                            {
                                pix1 = pix1 + nOffset * k;
                                pix2[0] = (byte)(pix2[0] + pix1[0] * window[k]);
                                pix2[1] = (byte)(pix2[1] + pix1[1] * window[k]);
                                pix2[2] = (byte)(pix2[2] + pix1[2] * window[k]);
                                sum += window[k];
                                pix1 = pix1 - nOffset * k;
                            }

                            if (i - k >= 0)
                            {
                                pix1 = pix1 - nOffset * k;
                                pix2[0] = (byte)(pix2[0] + pix1[0] * window[k]);
                                pix2[1] = (byte)(pix2[1] + pix1[1] * window[k]);
                                pix2[2] = (byte)(pix2[2] + pix1[2] * window[k]);
                                sum += window[k];
                                pix1 = pix1 + nOffset * k;
                            }
                        }
                        pix2[0] = (byte)(pix2[0] / sum);
                        pix2[1] = (byte)(pix2[1] / sum);
                        pix2[2] = (byte)(pix2[2] / sum);
                        pix1 += 3;
                        pix2 += 3;
                    }
                    pix1 += nOffset;
                    pix2 += nOffset;
                }

            }
            b.UnlockBits(bmData2);
            newBmp.UnlockBits(bmData1);
            Console.WriteLine("done");
            return b;
        }

        /// <summary>
        /// Постеризация. Считает процент облачности (NODATA область вычитается из общей площади квиклука);
        /// Неуправляемый код
        /// </summary>
        /// <param name="pic">Обработанное по гамме изображение</param>
        /// <param name="intensity">пороговая интенсивность пикселя. пиксели выше порога становятся белыми, ниже -- чёрными</param>
        /// <returns></returns>
        public Image Posterize(Image pic, int intensity) 
        {
            numOfWhitePixels = 0;
            otherPixels = 0;

            Bitmap bitmap = new Bitmap(pic);

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;


            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                //int nOffset = stride - b.Width * 3;
                //Console.WriteLine("offset = " + nOffset);

                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        //blue = p[0];
                        //green = p[1];
                        //red = p[2];
                        if (p[0] == p[1] && p[1] == p[2] && p[2] == 0)
                        {
                            p[2] = 255;
                            otherPixels++;
                        }
                        else
                        {
                            if (((p[0] + p[1] + p[2]) / 3) <= intensity)
                            {
                                p[0] = p[1] = p[2] = 0;
                            }
                            else
                            {
                                p[0] = p[1] = p[2] = 255;
                                numOfWhitePixels++;
                            }
                        }

                        //pixel = red / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[2] = (byte)pixel;

                        //pixel = green / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[1] = (byte)pixel;

                        //pixel = blue / 255.0;
                        //pixel -= 0.5;
                        //pixel *= contrast;
                        //pixel += 0.5;
                        //pixel *= 255;
                        //if (pixel < 0) pixel = 0;
                        //if (pixel > 255) pixel = 255;
                        //p[0] = (byte)pixel;

                        p += 3;
                    }
                }
            }

            CloudPercent = numOfWhitePixels * 100 / ((pic.Width * pic.Height) - otherPixels);

            bitmap.UnlockBits(bmData);
            //b.Save(@"C:\Users\rez\Pictures\Saburov\cosmoPictures\cloud.jpg");
            //newBmp = new Bitmap(b);
            //newBmp.Save(@"C:\Users\rez\Pictures\Saburov\cosmoPictures\cloud.jpg");

            return bitmap;
        }

        /// <summary>
        /// Работает долго
        /// </summary>
        /// <param name="image"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public Image Smoothing(Image image, int radius)
        {
            Bitmap b1 = new Bitmap(image);
            BitmapData bmData1 = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride1 = bmData1.Stride;
            System.IntPtr Scan01 = bmData1.Scan0;

            Bitmap newBmp = new Bitmap(image.Width, image.Height);
            BitmapData bmData2 = newBmp.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride2 = bmData2.Stride;
            System.IntPtr Scan02 = bmData2.Scan0;

            int square = (2 * radius + 1) * (2 * radius + 1);
            int redPixels = 0;

            unsafe
            {
                byte* pix1 = (byte*)(void*)Scan01;
                byte* pix2 = (byte*)(void*)Scan02;
                int nOffset = stride1 - b1.Width * 3;

                int numOfWhitePixels = 0;
                for(int j = 0; j < image.Height; j++)
                {
                    for(int i = 0; i < image.Width; i++)
                    {
                        //Console.WriteLine(i+" высота: " +j);
                        if (pix1[0] == 255)
                        {
                            pix2[0] = pix2[1] = pix2[2] = 255;
                        }
                        else if(pix1[2] == 255&&pix1[0]==0)
                        {
                            pix2[2] = 255;
                            pix2[0] = 0;
                            pix2[1] = 0;
                        }
                        else
                        {
                            pix1 = pix1 - radius * 3;
                            pix1 = pix1 - radius * nOffset;
                            //pix2 = pix2 - radius * 3;
                            //pix2 = pix2 - radius * nOffset;
                            for (int k = j - radius; k <= j + radius; k++)
                            {
                                for(int p = i - radius; p <= i + radius; p++)
                                {
                                    if (k >= 0 && k < image.Height && p >= 0 && p < image.Width)
                                    {
                                        if (pix1[0] == 255)
                                        {
                                            numOfWhitePixels++;
                                        }
                                        if (pix1[2] == 255)
                                        {
                                            redPixels++;
                                        }

                                    }

                                    pix1 += 3;
                                }
                                pix1 = pix1 - 3 * (2 * radius + 1);
                                pix1 += nOffset;
                            }
                            pix1 = pix1 + radius * 3;
                            pix1 = pix1 - (radius+1) * nOffset;

                            if (numOfWhitePixels > (square - redPixels) / 2.3)
                            {
                                pix2[0] = pix2[2] = 0;
                                pix2[1] = 255;
                            }

                            numOfWhitePixels = 0;
                            redPixels = 0;
                        }


                        pix1 += 3;
                        pix2 += 3;
                    }
                    pix1 += nOffset;
                    pix2 += nOffset;
                }
            }
            newBmp.UnlockBits(bmData2);
            newBmp.Save(@"C:\Users\rez\Pictures\Saburov\cosmoPictures\cloudBlur.jpg");
            return newBmp;

        }
 



        //public void CreateGrayimage()
        //{
        //    for (int j = 0; j < image.Height; j++)
        //    {
        //        for (int i = 0; i < image.Width; i++)
        //        {
        //            // получаем (i, j) пиксель
        //            UInt32 pixel = (UInt32)(image.GetPixel(i, j).ToArgb());
        //            // получаем компоненты цветов пикселя
        //            float R = (float)((pixel & 0x00FF0000) >> 16); // красный
        //            float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
        //            float B = (float)(pixel & 0x000000FF); // синий
        //                                                   // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
        //            R = G = B = (R + G + B) / 3.0f;
        //            // собираем новый пиксель по частям (по каналам)
        //            UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
        //            // добавляем его в Bitmap нового изображения
        //            grayImage.SetPixel(i, j, Color.FromArgb((int)newPixel));
        //        }
        //    }
        //    grayImage.Save(DIRECTORY + IMAGE_NAME + "Gray.jpg");
        //}

        //public void ChangeContrast(int p) //p -- на сколько процентов увеличим контрастность
        //{
        //    int N = (100 / 100) * p;  //можно вместо нижней 100 поставить любое число, которое будет являться максимальным значением контраста
        //    for (int j = 0; j < grayImage.Height; j++)
        //    {
        //        for (int i = 0; i < grayImage.Width; i++)
        //        {
        //            int I = grayImage.GetPixel(i, j).R; //интенсивность пикселя от 0 до 255
        //                                                //Console.WriteLine(I);

        //            I = (I * 100 - 128 * N) / (100 - N);
        //            if (I > 255) I = 255;
        //            if (I < 0) I = 0;

        //            UInt32 newPixel = 0xFF000000 | ((UInt32)I << 16) | ((UInt32)I << 8) | ((UInt32)I);
        //            contrastImage.SetPixel(i, j, Color.FromArgb((int)newPixel));

        //        }
        //    }
        //    contrastImage.Save(DIRECTORY + IMAGE_NAME + "Contrast.jpg");
        //}

        //Bitmap blackImage = new Bitmap(image.Width, image.Height);
        //public void CheckBlackPixels()
        //{
        //    for (int j = 0; j < grayImage.Height; j++)
        //    {
        //        for (int i = 0; i < grayImage.Width; i++)
        //        {
        //            if (grayImage.GetPixel(i, j).R == 0)
        //            {
        //                UInt32 newPixel = 0xFF000000 | ((UInt32)255 << 16) | ((UInt32)0 << 8) | ((UInt32)0);
        //                blackImage.SetPixel(i, j, Color.FromArgb((int)newPixel));

        //                redPixels++; //пиксели, которые не входят в изображение

        //            }
        //            else
        //            {
        //                blackImage.SetPixel(i, j, Color.FromArgb(grayImage.GetPixel(i, j).ToArgb()));
        //            }

        //        }
        //    }
        //    blackImage.Save(DIRECTORY + IMAGE_NAME + "black.jpg");

        //}
        //public void CalculateCloudPercent()
        //{
        //    int k = 0; //считаем количество белых пикселей

        //    for (int j = 0; j < blackImage.Height; j++)
        //    {
        //        for (int i = 0; i < blackImage.Width; i++)
        //        {
        //            byte R = blackImage.GetPixel(i, j).G; //оттенок зелёного (серого)
        //            if (R > 230) //если оттенок близок к 255
        //            {
        //                k++;
        //            }
        //        }
        //    }

        //    cloudPercent = (double)k / (double)(blackImage.Width * blackImage.Height - redPixels) * 100.0d;
        //    Console.WriteLine("cloud percent = " + cloudPercent);
        //}

    }
}
