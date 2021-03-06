﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ImageUtils
    {
        public static Image ResizeImage(Image content, int size, bool sizeIsHeight)
        {
            int width;
            int height;

            if (sizeIsHeight)
            {
                height = size;
                width = content.Width * height / content.Height;
            }
            else
            {
                width = size;
                height = content.Height * width / content.Width;
            }

            var result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            result.SetResolution(content.HorizontalResolution,
                                    content.VerticalResolution);
            result.MakeTransparent();

            using (var grPhoto = Graphics.FromImage(result))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grPhoto.DrawImage(content,
                                  new Rectangle(-1, -1, width + 1, height + 1),
                                  new Rectangle(0, 0, content.Width, content.Height),
                                  GraphicsUnit.Pixel);
            }
            return result;

        }

    }
}
