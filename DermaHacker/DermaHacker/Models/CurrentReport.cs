using System;
using System.Collections.Generic;
using System.Text;

namespace DermaHacker.Models
{
    public  class CurrentReport
    {
        private static CurrentReport instance;

        public static CurrentReport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentReport();
                }

                return instance;
            }
        }
        private CurrentReport() { }

        public void ClearCurrentReport()
        {
            this.ID = -1;
            NameAndSurname = "";
            Date = DateTime.Today;
            StandardImagePath = "";
            ThermoImagePath = "";
            Length = 0;
            Width = 0;
            Surface = 0;
            WoundBaseTemperature = 0;
            SurroundingsTemperature = 0;
            GranulationTissuePercentage = 0;
            SludgePercentage = 0;
            NecrosisPercentage = 0;

        }

        public int ID { get; set; }
        public string NameAndSurname { get; set; }
        public DateTime Date { get; set; }
        public string StandardImagePath { get; set; }
        public string ThermoImagePath { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Surface { get; set; }
        public double WoundBaseTemperature { get; set; }
        public double SurroundingsTemperature { get; set; }
        public double GranulationTissuePercentage { get; set; }
        public double SludgePercentage { get; set; }
        public double NecrosisPercentage { get; set; }
    }
}
