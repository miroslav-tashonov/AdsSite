using System;
using System.Collections.Generic;
using System.Text;
using ImageMagick;

namespace AdSite.Services.Wrappers
{
    public static class MagiskImageWrapper
    {
        public static byte[] MakeThumbnailImage(byte[] imageAsByteArray)
        {
            const int size = 150;
            const int quality = 50;

            using (var image = new MagickImage(imageAsByteArray))
            {
                image.Resize(size, size);
                image.Strip();
                image.Quality = quality;
                return image.ToByteArray();
            }
        }
    }
}