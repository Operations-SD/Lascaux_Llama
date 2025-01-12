using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;

namespace IntelChat.Pages
{
    public partial class Remote700 : ComponentBase
    {
        private bool isImageVisible = false;
        private bool isDragging = false;
        private bool isInsideDropZone = false; // Track whether the cursor is inside the drop zone
        private (int X, int Y) imagePosition = (100, 100); // Initial image position
        private (int X, int Y) mouseStart = (0, 0);

        private readonly (int Width, int Height) dropZoneSize = (150, 150); // Drop zone dimensions
        private readonly (int X, int Y) dropZoneTopLeft = (400, 400);       // Drop zone top-left corner positioning
        private readonly (int Width, int Height) originalImageSize = (300, 300); // Original image dimensions
        private (int Width, int Height) resizedImageSize = (0, 0); // Dynamically resized dimensions for the drop zone
        
        private void OpenImage()
        {
            isImageVisible = true;
        }

        private void CloseImage()
        {
            isImageVisible = false;
        }

        private void StartDragging(MouseEventArgs e)
        {
            isDragging = true;
            mouseStart = ((int)e.ClientX - imagePosition.X, (int)e.ClientY - imagePosition.Y);
        }

        private void StopDragging(MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;

                // Check if the cursor is inside the drop zone
                bool isCursorInsideDropZone =
                    e.ClientX >= dropZoneTopLeft.X &&
                    e.ClientX <= (dropZoneTopLeft.X + dropZoneSize.Width) &&
                    e.ClientY >= dropZoneTopLeft.Y &&
                    e.ClientY <= (dropZoneTopLeft.Y + dropZoneSize.Height);


                if (isCursorInsideDropZone)
                {
                    SnapToDropZone();
                }
                else
                {
                    SaveCurrentPosition();
                    isInsideDropZone = false; // Reset drop zone state
                }
            }
        }

        private void OnMouseMove(MouseEventArgs e)
        {
            if (isDragging)
            {
                imagePosition = ((int)e.ClientX - mouseStart.X, (int)e.ClientY - mouseStart.Y);
            }
        }


        private void SnapToDropZone()
        {
            // Calculate resized dimensions to fit within the drop zone
            double widthRatio = (double)dropZoneSize.Width / originalImageSize.Width;
            double heightRatio = (double)dropZoneSize.Height / originalImageSize.Height;
            double scale = Math.Min(widthRatio, heightRatio); // Maintain aspect ratio

            resizedImageSize = (
                (int)(originalImageSize.Width * scale),
                (int)(originalImageSize.Height * scale)
            );

            // Snap the image to the center of the drop zone
            imagePosition = (
                dropZoneTopLeft.X + (dropZoneSize.Width - resizedImageSize.Width) / 2,
                dropZoneTopLeft.Y + (dropZoneSize.Height - resizedImageSize.Height) / 2
            );

            isInsideDropZone = true; // Mark as inside the drop zone
        }

        private void SaveCurrentPosition()
        {
            // Allow the image to stay where it is
            // No action needed because the image remains at its last position
        }
    }
}
