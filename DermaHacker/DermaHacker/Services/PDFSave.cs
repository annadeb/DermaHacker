using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Webkit;
using DermaHacker.Services;
using DermaHacker.ViewModels;
using Java.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Application = Android.App.Application;
using File = System.IO.File;

[assembly: Dependency(typeof(PDFSave))]
namespace DermaHacker.Services
{
    public class PDFSave : ISave
    {
        public Intent SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context, Context appctx)
        {
            string exception = string.Empty;
            string root = null;

           

            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/PDFFiles");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
                return null;
            }

            if (file.Exists() && contentType != "application/html")
            {
                string extension = MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(appctx,Android.App.Application.Context.PackageName + ".fileprovider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                return intent;
                //switch (context)
                //{
                //    case PDFOpenContext.InApp:
                //        appctx.StartActivity(intent);
                //        break;
                //    case PDFOpenContext.ChooseApp:
                //        appctx.StartActivity(Intent.CreateChooser(intent, "Choose App"));
                //        break;
                //    default:
                //        break;
                //}
            }
            else
            {
                return null;
            }
        }
    }
}
