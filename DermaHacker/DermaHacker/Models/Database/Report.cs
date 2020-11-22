using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DermaHacker.Models.Database
{
    public class Report
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NameAndSurname { get; set; }
        public DateTime Date { get; set; }
        public string StandardImagePath { get; set; }
       // public string ThermoImagePath { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Surface { get; set; }
        //public double WoundBaseTemperature { get; set; }
        //public double SurroundingsTemperature { get; set; }
        public double GranulationTissuePercentage { get; set; }
        public double SludgePercentage { get; set; }
        public double NecrosisPercentage { get; set; }

        public static Report CreateReport()
        {

            return new Report()
            {
                NameAndSurname = CurrentReport.Instance.NameAndSurname,
                Date = CurrentReport.Instance.Date,
                StandardImagePath = CurrentReport.Instance.StandardImagePath,
                //ThermoImagePath = CurrentReport.Instance.ThermoImagePath,
                Length = CurrentReport.Instance.Length,
                Width = CurrentReport.Instance.Width,
                Surface = CurrentReport.Instance.Surface,
                GranulationTissuePercentage = CurrentReport.Instance.GranulationTissuePercentage,
                SludgePercentage = CurrentReport.Instance.SludgePercentage,
                NecrosisPercentage = CurrentReport.Instance.NecrosisPercentage,
               // WoundBaseTemperature = CurrentReport.Instance.WoundBaseTemperature,
               // SurroundingsTemperature = CurrentReport.Instance.SurroundingsTemperature
            };
        }
    }
}
