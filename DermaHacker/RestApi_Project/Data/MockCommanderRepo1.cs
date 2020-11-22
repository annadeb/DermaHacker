using RestApi_Dicom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Dicom.Data
{
    public class MockCommanderRepo1 : ICOmmanderRepoForHac
    {

      static List<ImageData> commands1 = new List<ImageData>()
        {

             new ImageData { Id = 1, Base64 = "EloJkaisRandom", CoordinateXY = new int []{2,2 } },
       
        };

        public void AddToList(ImageData imageCreate)
        {
            commands1.Add(imageCreate);
            if (commands1.Contains(imageCreate))
            {
                ImageProcess imageProcess = new ImageProcess();
                imageProcess.ReadImage(imageCreate);

            }
        }

        public IEnumerable<ImageData> GetAllCommands()
        {
            var commands = new List<ImageData>
            {

            };
            return commands1;
        }

        public ImageData GetCommandById(int Id)
        {
            return commands1.FirstOrDefault(x => x.Id == Id);
        }
    }
}
