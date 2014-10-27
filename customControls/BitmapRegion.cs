using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace BitmapRegionTest
{
     public static class BitmapRegion
     {
          /// <summary>
          /// Create and apply the region on the supplied control
          /// </summary>
          /// <param name="i_Control">The Control object to apply the region to</param>
          /// <param name="i_FilterByFirst">if true - region composed of pixels different from first pixel
          ///                               if false - region is composed of pixels similar to first pixel</param>
          public static void CreateControlRegion(Control i_Control, bool i_FilterByFirst)
          {
               // Set our control's size to be the same as the bitmap
               i_Control.Width = i_Control.BackgroundImage.Width;
               i_Control.Height = i_Control.BackgroundImage.Height;

               Bitmap bitmap = i_Control.BackgroundImage as Bitmap;

               // Calculate the graphics path based on the bitmap supplied
               GraphicsPath graphicsPath = CalculateGraphicsPath(bitmap, i_FilterByFirst);

               // Apply new region
               i_Control.Region = new Region(graphicsPath);
          }

          /// <summary>
          /// Calculate the graphics path that representing the figure in the bitmap 
          /// excluding the transparent color which is the top left pixel.
          /// </summary>
          /// <param name="bitmap">The Bitmap object to calculate our graphics path from</param>
          /// <returns>Calculated graphics path</returns>
          private static GraphicsPath CalculateGraphicsPath(Bitmap i_Bitmap, bool i_FilterByFirstPixel)
          {
               // Create GraphicsPath for our bitmap calculation
               GraphicsPath graphicsPath = new GraphicsPath();

               // Use the top left pixel as our transparent color
               Color FirstPixel = i_Bitmap.GetPixel(0, 0);

               // This is to store the column value where an opaque pixel is first found.
               // This value will determine where we start scanning for trailing opaque pixels.
               int colOpaquePixelIdx = 0;

               // Go through all rows (Y axis)
               for (int row = 0; row < i_Bitmap.Height; row++)
               {
                    // Reset value
                    colOpaquePixelIdx = 0;

                    // Go through all columns (X axis)
                    for (int col = 0; col < i_Bitmap.Width; col++)
                    {
                         // If this is an opaque pixel, mark it and search for anymore trailing behind
                         if (i_FilterByFirstPixel)
                         {
                              if (i_Bitmap.GetPixel(col, row) != FirstPixel)
                              {
                                   // Opaque pixel found, mark current position
                                   colOpaquePixelIdx = col;

                                   // Create another variable to set the current pixel position
                                   int colNext = col;

                                   // Starting from current found opaque pixel, search for anymore opaque pixels 
                                   // trailing behind, until a transparent pixel is found or minimum width is reached
                                   for (colNext = colOpaquePixelIdx; colNext < i_Bitmap.Width; colNext++)
                                   {
                                        if (i_Bitmap.GetPixel(colNext, row) == FirstPixel)
                                        {
                                             break;
                                        }
                                   }

                                   // Form a rectangle for line of opaque pixels found and add it to our graphics path
                                   graphicsPath.AddRectangle(new Rectangle(colOpaquePixelIdx, row, colNext - colOpaquePixelIdx, 1));

                                   // No need to scan the line of opaque pixels just found
                                   col = colNext;
                              }
                         }
                         //// add pixels same as first pixel
                         else
                         {
                              if (i_Bitmap.GetPixel(col, row) == FirstPixel)
                              {
                                   // Opaque pixel found, mark current position
                                   colOpaquePixelIdx = col;

                                   // Create another variable to set the current pixel position
                                   int colNext = col;

                                   // Starting from current found opaque pixel, search for anymore opaque pixels 
                                   // trailing behind, until a transparent pixel is found or minimum width is reached
                                   for (colNext = colOpaquePixelIdx; colNext < i_Bitmap.Width; colNext++)
                                   {
                                        if (i_Bitmap.GetPixel(colNext, row) != FirstPixel)
                                        {
                                             break;
                                        }
                                   }

                                   // Form a rectangle for line of opaque pixels found and add it to our graphics path
                                   graphicsPath.AddRectangle(new Rectangle(colOpaquePixelIdx, row, colNext - colOpaquePixelIdx, 1));

                                   // No need to scan the line of opaque pixels just found
                                   col = colNext;
                              }
                         }
                    }
               }

               // Return calculated graphics path
               return graphicsPath;
          }

          public static void paintBoarders(Bitmap i_Bitmap, Color i_Color)
          {
               for (int col = 0; col < i_Bitmap.Width; col++)
               {
                    i_Bitmap.SetPixel(col, 0, i_Color);
                    i_Bitmap.SetPixel(col, i_Bitmap.Height - 1, i_Color);
               }

               for (int col = 1; col < i_Bitmap.Width; col++)
               {
                    i_Bitmap.SetPixel(col, 0, i_Color);
                    i_Bitmap.SetPixel(col, i_Bitmap.Height - 1, i_Color);
               }

               for (int col = 2; col < i_Bitmap.Width; col++)
               {
                    i_Bitmap.SetPixel(col, 0, i_Color);
                    i_Bitmap.SetPixel(col, i_Bitmap.Height - 1, i_Color);
               }

               for (int row = 0; row < i_Bitmap.Height; row++)
               {
                    i_Bitmap.SetPixel(0, row, i_Color);
                    i_Bitmap.SetPixel(i_Bitmap.Width - 1, row, i_Color);
               }
          }
     }
}