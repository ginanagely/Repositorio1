using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;


namespace WindowsFormsApplication5
{
    class funcion
    {
    public static byte[] ResizeImageFile(byte[] imageFile, int targetSize)
    {
        Image original = Image.FromStream(new MemoryStream(imageFile));
        int targetH, targetW;
        if (original.Height >= original.Width)
        {
            targetH = targetSize;
            targetW = (int)(original.Width * ((float)targetSize / (float)original.Height));
        }
        else
        {
            targetW = targetSize;
            targetH = (int)(original.Height * ((float)targetSize / (float)original.Width));
        }
        Image imgPhoto = Image.FromStream(new MemoryStream(imageFile));
        // Create a new blank canvas.  The resized image will be drawn on this canvas.
        Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(72, 72);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
        grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
        // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
        MemoryStream mm = new MemoryStream();
        bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
        original.Dispose();
        imgPhoto.Dispose();
        bmPhoto.Dispose();
        grPhoto.Dispose();
        return mm.GetBuffer();
}
    public static Image ResizeImage(Image srcImage, int newWidth, int newHeight)
    {
        using (Bitmap imagenBitmap =
           new Bitmap(newWidth, newHeight, PixelFormat.Format32bppRgb))
        {
            imagenBitmap.SetResolution(
               Convert.ToInt32(srcImage.HorizontalResolution),
               Convert.ToInt32(srcImage.HorizontalResolution));

            using (Graphics imagenGraphics =
                    Graphics.FromImage(imagenBitmap))
            {
                imagenGraphics.SmoothingMode =
                   SmoothingMode.AntiAlias;
                imagenGraphics.InterpolationMode =
                   InterpolationMode.HighQualityBicubic;
                imagenGraphics.PixelOffsetMode =
                   PixelOffsetMode.HighQuality;
                imagenGraphics.DrawImage(srcImage,
                   new Rectangle(0, 0, newWidth, newHeight),
                   new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                   GraphicsUnit.Pixel);
                MemoryStream imagenMemoryStream = new MemoryStream();
                imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                srcImage = Image.FromStream(imagenMemoryStream);
            }
        }
        return srcImage;
    }
    }
}
