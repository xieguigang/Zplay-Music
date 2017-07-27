Imports System.Drawing
Imports libZPlay.App
Imports Microsoft.VisualBasic.Imaging

Public Module ArtVisual

    Public ReadOnly Highlight As Color = Color.FromArgb(197, 226, 253)

    Public Function DrawHighlight(file As MediaFile) As Image
        Return __draw(file, Highlight)
    End Function

    Private Function __draw(file As MediaFile, back As Color) As Image
        Using gdi As Graphics2D = New Size(250, 100).CreateGDIDevice(back)
            Dim res As Image = If(
                file.Id3v2.Picture.Bitmap.IsNullOrNothing,
                My.Resources._default,
                file.Id3v2.Picture.Bitmap)

            Call gdi.Graphics.DrawImage(res, 8, 8, 85, 85)
            Call gdi.Graphics.DrawString(file.Id3v2.Title, New Font(FontFace.MicrosoftYaHei, 8), Brushes.Black, 103, 25)
            Call gdi.Graphics.DrawString(file.StreamInfo.Length.FormatTime, New Font(FontFace.MicrosoftYaHei, 8), Brushes.Gray, 103, 35)

            Return gdi.ImageResource
        End Using
    End Function

    Public Function DrawNormal(file As MediaFile) As Image
        Return __draw(file, Color.White)
    End Function
End Module
