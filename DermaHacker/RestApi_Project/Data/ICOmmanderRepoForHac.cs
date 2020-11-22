using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Dicom.Data
{
    public interface ICOmmanderRepoForHac
    {
        IEnumerable<ImageData> GetAllCommands();
        ImageData GetCommandById(int Id);
        void AddToList(ImageData imageCreate);
    }
}
