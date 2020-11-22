using RestApi_Dicom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Dicom.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {

        List<Command> commands1 = new List<Command>()
        {
            
             new Command { Id = 1, HowTo = "Boil an egg", Line = "Boil water", Platform = "Surfing & Pacan" },
        new Command { Id = 2, HowTo = "Cut bread", Line = "Boil water", Platform = "Surfing & Pacan2" },
                new Command { Id = 3, HowTo = "Make cup of tea", Line = "Boil water", Platform = "Surfing & Pacan3" },
                new Command { Id = 4, HowTo = "Make 444 of tea", Line = "Boil water", Platform = "Działa Lista" }
        };
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
            
            };
            return commands1;
        }

        public Command GetCommandById(int Id)
        {
            return commands1.FirstOrDefault(x => x.Id == Id);
        }
    }
}
