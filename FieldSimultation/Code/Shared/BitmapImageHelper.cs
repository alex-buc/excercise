

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FieldSimultation.Code.Shared;
public static class BitmapImageHelper {
    public static BitmapImage CreateInitialsImage(string initials, Color textColor = default, Color backgroundColor = default, int width = 100, int height = 100)
    {
        if (backgroundColor == default)
            backgroundColor = Colors.LightGray; // Default background color
        if (textColor == default)
            textColor = Colors.Black; // Default text color

        // Create a DrawingVisual
        DrawingVisual drawingVisual = new DrawingVisual();
        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
        {
            // Draw the background
            Rect rect = new Rect(0, 0, width, height);
            drawingContext.DrawRectangle(new SolidColorBrush(backgroundColor), null, rect);

            // Draw the text (initials)
            Typeface typeface = new Typeface("Segoe UI");
            FormattedText formattedText = new FormattedText(
                initials,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                35, // Font size
                new SolidColorBrush(textColor),
                96); // DPI scaling

            // Center the text
            double textX = (width - formattedText.Width) / 2;
            double textY = (height - formattedText.Height) / 2;
            drawingContext.DrawText(formattedText, new Point(textX, textY));
        }

        // Render the visual to a bitmap
        RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
        renderBitmap.Render(drawingVisual);

        // Convert RenderTargetBitmap to BitmapImage
        return ConvertRenderTargetBitmapToBitmapImage(renderBitmap);
    }

    private static BitmapImage ConvertRenderTargetBitmapToBitmapImage(RenderTargetBitmap renderBitmap)
    {
        // Save RenderTargetBitmap to a memory stream
        PngBitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
        using (var stream = new System.IO.MemoryStream())
        {
            encoder.Save(stream);
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            // Create a BitmapImage from the stream
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}